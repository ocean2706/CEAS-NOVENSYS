// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.ASN1.ASN1Helper
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Type;
using Novensys.eCard.SDK;
using Novensys.eCard.SDK.ASN1;
using Novensys.eCard.SDK.ASN1.EHCDG1;
using Novensys.eCard.SDK.ASN1.EHCDG11;
using Novensys.eCard.SDK.ASN1.EHCDG2;
using Novensys.eCard.SDK.ASN1.EHCDG3;
using Novensys.eCard.SDK.ASN1.EHCDG4;
using Novensys.eCard.SDK.ASN1.EHCDG5;
using Novensys.eCard.SDK.ASN1.EHCDG6;
using Novensys.eCard.SDK.ASN1.EHCDG7;
using Novensys.eCard.SDK.ASN1.EHCDG8;
using Novensys.eCard.SDK.ASN1.EHCDG9;
using Novensys.eCard.SDK.ASN1.EHCTECH;
using Novensys.eCard.SDK.ASN1.EHDG10;
using Novensys.eCard.SDK.Entities.SmartCard;
using Novensys.eCard.SDK.PCSC.Apdu;
using Novensys.eCard.SDK.Utils.Crypto;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Novensys.eCard.SDK.Utils.ASN1
{
  public class ASN1Helper
  {
    public static byte[] EncodeActivateStatus(int activateStatus, int activateStatusExtern)
    {
      ASN_EHCFactory.EncodingRules = "BER";
      IEncoder encoder = ASN_EHCFactory.CreateEncoder();
      TECH tech = new TECH();
      tech.ActivateStatus = Convert.ToString(activateStatus);
      tech.ActivateStatusExtern = Convert.ToString(activateStatusExtern);
      MemoryStream memoryStream = new MemoryStream();
      encoder.Encode((Asn1Type) tech, (Stream) memoryStream);
      return memoryStream.ToArray();
    }

    public static ActivateStatus DecodeActivateStatus(byte[] data)
    {
      IDecoder decoder = ASN_EHCFactory.CreateDecoder();
      TECH tech = new TECH();
      decoder.Decode(data, (Asn1Type) tech);
      return (ActivateStatus) Convert.ToInt16(tech.ActivateStatus);
    }

    public static ActivateStatusExtern DecodeActivateStatusExtern(byte[] data)
    {
      IDecoder decoder = ASN_EHCFactory.CreateDecoder();
      TECH tech = new TECH();
      decoder.Decode(data, (Asn1Type) tech);
      return (ActivateStatusExtern) Convert.ToInt16(tech.ActivateStatusExtern);
    }

    public static string DecodeCardNumber(byte[] data)
    {
      IDecoder decoder = ASN_EHCFactory.CreateDecoder();
      DG1 dg1 = new DG1();
      decoder.Decode(data, (Asn1Type) dg1);
      return dg1.CardNumber;
    }

    public static Org.BouncyCastle.Asn1.Cms.ContentInfo BuildSignedData(byte[] buffer, Org.BouncyCastle.Asn1.Cms.SignerInfo signerInfo)
    {
      Asn1EncodableVector v1 = new Asn1EncodableVector(new Asn1Encodable[0]);
      Asn1EncodableVector v2 = new Asn1EncodableVector(new Asn1Encodable[0]);
      Asn1Set certificates = (Asn1Set) null;
      Asn1Set crls = (Asn1Set) null;
      Org.BouncyCastle.Asn1.Cms.ContentInfo contentInfo = new Org.BouncyCastle.Asn1.Cms.ContentInfo(new DerObjectIdentifier("1.2.840.113549.1.7.1"), (Asn1Encodable) new BerOctetString(buffer));
      v1.Add((Asn1Encodable) signerInfo.DigestAlgorithm);
      v2.Add((Asn1Encodable) signerInfo);
      return new Org.BouncyCastle.Asn1.Cms.ContentInfo(new DerObjectIdentifier("1.2.840.113549.1.7.2"), (Asn1Encodable) new Org.BouncyCastle.Asn1.Cms.SignedData((Asn1Set) new DerSet(v1), contentInfo, certificates, crls, (Asn1Set) new DerSet(v2)));
    }

    public static Org.BouncyCastle.Asn1.Cms.SignerInfo BuildSignerInfo(Attributes signedAttr, byte[] signatureBytes, X509Certificate cert)
    {
      Asn1Set unauthenticatedAttributes = (Asn1Set) null;
      TbsCertificateStructure instance = TbsCertificateStructure.GetInstance((object) Asn1Object.FromByteArray(DotNetUtilities.FromX509Certificate(cert).GetTbsCertificate()));
      SignerIdentifier sid = new SignerIdentifier(new Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber(instance.Issuer, instance.SerialNumber.Value));
      AlgorithmIdentifier digAlgorithm = new AlgorithmIdentifier(new DerObjectIdentifier("2.16.840.1.101.3.4.2.1"), (Asn1Encodable) DerNull.Instance);
      AlgorithmIdentifier digEncryptionAlgorithm = new AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.1.1"), (Asn1Encodable) DerNull.Instance);
      return new Org.BouncyCastle.Asn1.Cms.SignerInfo(sid, digAlgorithm, (Asn1Set) Asn1Object.FromByteArray(signedAttr.GetDerEncoded()), digEncryptionAlgorithm, (Asn1OctetString) new DerOctetString(signatureBytes), unauthenticatedAttributes);
    }

    public static Attributes GetSignedAttributes(byte[] buffer, byte[] certBytes)
    {
      Asn1EncodableVector v = new Asn1EncodableVector(new Asn1Encodable[0]);
      Org.BouncyCastle.Asn1.Cms.Attribute attribute1 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.ContentType, (Asn1Set) new DerSet((Asn1Encodable) new DerObjectIdentifier("1.2.840.113549.1.7.1")));
      v.Add((Asn1Encodable) attribute1);
      Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.SigningTime, (Asn1Set) new DerSet((Asn1Encodable) new Org.BouncyCastle.Asn1.Cms.Time(DateTime.UtcNow)));
      v.Add((Asn1Encodable) attribute2);
      SigningCertificate signingCertificate = new SigningCertificate(new EssCertID(CryptoHelper.SHA1ComputeHash(certBytes)));
      Org.BouncyCastle.Asn1.Cms.Attribute attribute3 = new Org.BouncyCastle.Asn1.Cms.Attribute(PkcsObjectIdentifiers.IdAASigningCertificate, (Asn1Set) new DerSet((Asn1Encodable) signingCertificate));
      v.Add((Asn1Encodable) attribute3);
      Org.BouncyCastle.Asn1.Cms.Attribute attribute4 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.MessageDigest, (Asn1Set) new DerSet((Asn1Encodable) new DerOctetString(CryptoHelper.SHA256ComputeHash(buffer))));
      v.Add((Asn1Encodable) attribute4);
      return new Attributes(v);
    }

    public static void DecodeFile(byte[] data, CoduriFisiereCard cardFileCode, CardData cardData)
    {
      ASN_EHCFactory.EncodingRules = cardFileCode != CoduriFisiereCard.CERT && cardFileCode != CoduriFisiereCard.CERT_MAI ? "BER" : "DER";
      IDecoder decoder = ASN_EHCFactory.CreateDecoder();
      switch (cardFileCode)
      {
        case CoduriFisiereCard.DG1:
          ASN1Helper.DecodeDG1(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG2:
          ASN1Helper.DecodeDG2(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG3:
          ASN1Helper.DecodeDG3(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG4:
          ASN1Helper.DecodeDG4(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG5:
          ASN1Helper.DecodeDG5(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG6:
          ASN1Helper.DecodeDG6(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG7:
          ASN1Helper.DecodeDG7(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG8:
          ASN1Helper.DecodeDG8(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG9:
          ASN1Helper.DecodeDG9(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG10:
          ASN1Helper.DecodeDG10(data, cardData, decoder);
          break;
        case CoduriFisiereCard.DG11:
          ASN1Helper.DecodeDG11(data, cardData, decoder);
          break;
      }
    }

    private static void DecodeDG11(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG11 dg11 = new DG11();
      decoder.Decode(data, (Asn1Type) dg11);
    }

    private static void DecodeDG10(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG10 dg10 = new DG10();
      decoder.Decode(data, (Asn1Type) dg10);
    }

    private static void DecodeDG9(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG9 dg9 = new DG9();
      decoder.Decode(data, (Asn1Type) dg9);
    }

    private static void DecodeDG8(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG8 dg8 = new DG8();
      decoder.Decode(data, (Asn1Type) dg8);
    }

    private static void DecodeDG7(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG7 dg7 = new DG7();
      decoder.Decode(data, (Asn1Type) dg7);
    }

    private static void DecodeDG6(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG6 dg6 = new DG6();
      decoder.Decode(data, (Asn1Type) dg6);
      List<string> list1 = new List<string>();
      for (int index = 0; index < dg6.Diagnostics.Count; ++index)
        list1.Add(dg6.Diagnostics[index].Code);
      cardData.DateClinicePrimare.DiagnosticeMedicaleCuRiscVital.Valoare = (object) list1;
      List<string> list2 = new List<string>();
      for (int index = 0; index < dg6.ChronicDiseases.Count; ++index)
        list2.Add(dg6.ChronicDiseases[index].Code);
      cardData.DateClinicePrimare.BoliCronice.Valoare = (object) list2;
    }

    private static void DecodeDG5(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG5 dg5 = new DG5();
      decoder.Decode(data, (Asn1Type) dg5);
    }

    private static void DecodeDG3(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG3 dg3 = new DG3();
      decoder.Decode(data, (Asn1Type) dg3);
    }

    private static void DecodeDG2(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG2 dg2 = new DG2();
      decoder.Decode(data, (Asn1Type) dg2);
    }

    private static void DecodeDG4(byte[] data, CardData cardData, IDecoder decoder)
    {
      byte[] data1 = ApduUtil.RemoveTrailingNulls(data);
      DG4 dg4 = new DG4();
      decoder.Decode(data1, (Asn1Type) dg4);
      cardData.DateAdministrative.NumeMedicFamilie.Valoare = (object) dg4.DoctorLastName;
      cardData.DateAdministrative.PrenumeMedicFamilie.Valoare = (object) dg4.DoctorFirstName;
      cardData.DateAdministrative.CodMedicFamilie.Valoare = (object) dg4.DoctorReference;
      cardData.DateAdministrative.TelefonMedicFamilie.Valoare = (object) dg4.DoctorPhone;
      List<PersoanaContact> list = new List<PersoanaContact>();
      for (int index = 0; index < dg4.ContactPersons.Count; ++index)
      {
        PersoanaContact persoanaContact = new PersoanaContact(dg4.ContactPersons[index].FullName, dg4.ContactPersons[index].Phone);
        list.Add(persoanaContact);
      }
      cardData.DateAdministrative.PersoaneContact.Valoare = (object) list;
      cardData.DateClinicePrimare.GrupaSanguina.Valoare = (object) dg4.BloodType;
      cardData.DateClinicePrimare.RH.Valoare = (object) dg4.BloodRH;
      cardData.DateClinicePrimare.StatusDonatorOrgane.Valoare = (object) dg4.OrganDonorStatus;
    }

    private static void DecodeDG1(byte[] data, CardData cardData, IDecoder decoder)
    {
      DG1 dg1 = new DG1();
      decoder.Decode(data, (Asn1Type) dg1);
      cardData.DeviceData.NumarCard.Valoare = (object) dg1.CardNumber;
      cardData.DeviceData.Versiune.Valoare = (object) dg1.ProfileVersion;
      cardData.DateIdentificare.Nume.Valoare = (object) dg1.LastName;
      cardData.DateIdentificare.Prenume.Valoare = (object) dg1.FirstName;
      cardData.DateIdentificare.DataNastere.Valoare = (object) dg1.DateOfBirth;
      cardData.DateAdministrative.NumarAsigurat.Valoare = (object) dg1.InsuranceID;
      cardData.DateIdentificare.CNP.Valoare = (object) dg1.CardHolderID;
    }

    public static byte[] EncodeFile(List<CoduriCampuriCard> campuriDeEditatPeFisier, FisierCard cardFile, CardData cardData, CardData originalCardData)
    {
      byte[] numArray = (byte[]) null;
      ASN_EHCFactory.EncodingRules = cardFile.Cod != CoduriFisiereCard.CERT && cardFile.Cod != CoduriFisiereCard.CERT_MAI ? "BER" : "DER";
      IEncoder encoder = ASN_EHCFactory.CreateEncoder();
      switch (cardFile.Cod)
      {
        case CoduriFisiereCard.DG4:
          numArray = ASN1Helper.EncodeDG4(encoder, campuriDeEditatPeFisier, cardFile, cardData, originalCardData);
          break;
        case CoduriFisiereCard.DG6:
          numArray = ASN1Helper.EncodeDG6(encoder, campuriDeEditatPeFisier, cardFile, cardData, originalCardData);
          break;
      }
      return numArray;
    }

    private static byte[] EncodeDG4(IEncoder encoder, List<CoduriCampuriCard> campuriEditate, FisierCard cardFile, CardData cardData, CardData originalCardData)
    {
      DG4 dg4 = new DG4();
      MemoryStream memoryStream = new MemoryStream();
      ASN1Helper.FillOriginalDataDG4(dg4, originalCardData);
      foreach (CoduriCampuriCard camp in campuriEditate)
        ASN1Helper.UpdateDataDG4(camp, dg4, cardData);
      encoder.Encode((Asn1Type) dg4, (Stream) memoryStream);
      return memoryStream.ToArray();
    }

    private static void FillOriginalDataDG4(DG4 dg4, CardData originalCardData)
    {
      if (originalCardData.DateAdministrative.NumeMedicFamilie.Valoare != null)
        dg4.DoctorLastName = Convert.ToString(originalCardData.DateAdministrative.NumeMedicFamilie.Valoare);
      if (originalCardData.DateAdministrative.PrenumeMedicFamilie.Valoare != null)
        dg4.DoctorFirstName = Convert.ToString(originalCardData.DateAdministrative.PrenumeMedicFamilie.Valoare);
      if (originalCardData.DateAdministrative.CodMedicFamilie.Valoare != null)
        dg4.DoctorReference = Convert.ToString(originalCardData.DateAdministrative.CodMedicFamilie.Valoare);
      if (originalCardData.DateAdministrative.TelefonMedicFamilie.Valoare != null)
        dg4.DoctorPhone = Convert.ToString(originalCardData.DateAdministrative.TelefonMedicFamilie.Valoare);
      if (originalCardData.DateAdministrative.PersoaneContact.Valoare != null)
      {
        foreach (PersoanaContact persoanaContact in originalCardData.DateAdministrative.PersoaneContact.Valoare as List<PersoanaContact>)
        {
          if (!string.IsNullOrEmpty(persoanaContact.NumeSiPrenume) || !string.IsNullOrEmpty(persoanaContact.Telefon))
          {
            ContactPerson element = new ContactPerson();
            if (!string.IsNullOrEmpty(persoanaContact.NumeSiPrenume))
              element.FullName = persoanaContact.NumeSiPrenume;
            if (!string.IsNullOrEmpty(persoanaContact.Telefon))
              element.Phone = persoanaContact.Telefon;
            dg4.ContactPersons.Add(element);
          }
        }
      }
      if (originalCardData.DateClinicePrimare.GrupaSanguina.Valoare != null)
        dg4.BloodType = Convert.ToString(originalCardData.DateClinicePrimare.GrupaSanguina.Valoare);
      if (originalCardData.DateClinicePrimare.RH.Valoare != null)
        dg4.BloodRH = Convert.ToString(originalCardData.DateClinicePrimare.RH.Valoare);
      if (originalCardData.DateClinicePrimare.StatusDonatorOrgane.Valoare == null)
        return;
      dg4.OrganDonorStatus = Convert.ToString(originalCardData.DateClinicePrimare.StatusDonatorOrgane.Valoare);
    }

    private static void UpdateDataDG4(CoduriCampuriCard camp, DG4 dg4, CardData cardData)
    {
      switch (camp)
      {
        case CoduriCampuriCard.C4:
          dg4.DoctorLastName = Convert.ToString(cardData.DateAdministrative.NumeMedicFamilie.Valoare);
          break;
        case CoduriCampuriCard.C5:
          dg4.DoctorFirstName = Convert.ToString(cardData.DateAdministrative.PrenumeMedicFamilie.Valoare);
          break;
        case CoduriCampuriCard.C6:
          dg4.DoctorReference = Convert.ToString(cardData.DateAdministrative.CodMedicFamilie.Valoare);
          break;
        case CoduriCampuriCard.C7:
          dg4.DoctorPhone = Convert.ToString(cardData.DateAdministrative.TelefonMedicFamilie.Valoare);
          break;
        case CoduriCampuriCard.C8:
          dg4.ContactPersons.Clear();
          if (cardData.DateAdministrative.PersoaneContact.Valoare == null)
            break;
          foreach (PersoanaContact persoanaContact in cardData.DateAdministrative.PersoaneContact.Valoare as List<PersoanaContact>)
          {
            if (!string.IsNullOrEmpty(persoanaContact.NumeSiPrenume) || !string.IsNullOrEmpty(persoanaContact.Telefon))
            {
              ContactPerson element = new ContactPerson();
              if (!string.IsNullOrEmpty(persoanaContact.NumeSiPrenume))
                element.FullName = persoanaContact.NumeSiPrenume;
              if (!string.IsNullOrEmpty(persoanaContact.Telefon))
                element.Phone = persoanaContact.Telefon;
              dg4.ContactPersons.Add(element);
            }
          }
          break;
        case CoduriCampuriCard.D1:
          dg4.BloodType = Convert.ToString(cardData.DateClinicePrimare.GrupaSanguina.Valoare);
          break;
        case CoduriCampuriCard.D2:
          dg4.BloodRH = Convert.ToString(cardData.DateClinicePrimare.RH.Valoare);
          break;
        case CoduriCampuriCard.D4:
          dg4.OrganDonorStatus = Convert.ToString(cardData.DateClinicePrimare.StatusDonatorOrgane.Valoare);
          break;
      }
    }

    private static void FillOriginalDataDG6(DG6 dg6, CardData originalCardData)
    {
      if (originalCardData.DateClinicePrimare.DiagnosticeMedicaleCuRiscVital.Valoare != null)
      {
        foreach (string str in originalCardData.DateClinicePrimare.DiagnosticeMedicaleCuRiscVital.Valoare as List<string>)
          dg6.Diagnostics.Add(new Diagnostic()
          {
            Code = str
          });
      }
      if (originalCardData.DateClinicePrimare.BoliCronice.Valoare == null)
        return;
      foreach (string str in originalCardData.DateClinicePrimare.BoliCronice.Valoare as List<string>)
        dg6.ChronicDiseases.Add(new ChronicDesease()
        {
          Code = str
        });
    }

    private static void UpdateDataDG6(CoduriCampuriCard camp, DG6 dg6, CardData cardData)
    {
      switch (camp)
      {
        case CoduriCampuriCard.D6:
          dg6.Diagnostics.Clear();
          if (cardData.DateClinicePrimare.DiagnosticeMedicaleCuRiscVital.Valoare == null)
            break;
          foreach (string str in cardData.DateClinicePrimare.DiagnosticeMedicaleCuRiscVital.Valoare as List<string>)
            dg6.Diagnostics.Add(new Diagnostic()
            {
              Code = str
            });
          break;
        case CoduriCampuriCard.D7:
          dg6.ChronicDiseases.Clear();
          if (cardData.DateClinicePrimare.BoliCronice.Valoare == null)
            break;
          foreach (string str in cardData.DateClinicePrimare.BoliCronice.Valoare as List<string>)
            dg6.ChronicDiseases.Add(new ChronicDesease()
            {
              Code = str
            });
          break;
      }
    }

    private static byte[] EncodeDG6(IEncoder encoder, List<CoduriCampuriCard> campuriEditate, FisierCard cardFile, CardData cardData, CardData originalCardData)
    {
      DG6 dg6 = new DG6();
      MemoryStream memoryStream = new MemoryStream();
      ASN1Helper.FillOriginalDataDG6(dg6, originalCardData);
      foreach (CoduriCampuriCard camp in campuriEditate)
        ASN1Helper.UpdateDataDG6(camp, dg6, cardData);
      encoder.Encode((Asn1Type) dg6, (Stream) memoryStream);
      return memoryStream.ToArray();
    }
  }
}
