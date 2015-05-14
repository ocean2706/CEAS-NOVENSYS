// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Terminal.TerminalDataStorage
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.eCard.SDK.Entities.SmartCard;
using Novensys.eCard.SDK.Entities.Terminal;
using Novensys.eCard.SDK.Utils.Crypto;
using Novensys.eCard.SDK.Utils.Hex;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Novensys.eCard.SDK.Terminal
{
  internal class TerminalDataStorage
  {
    public static readonly TerminalDataStorage Instance = new TerminalDataStorage();

    public string StoragePath
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Novensys.eCard.SDK\\TerminalData");
      }
    }

    public bool Exists(TerminalDataIdentifier id)
    {
      if (!Directory.Exists(this.StoragePath))
        Directory.CreateDirectory(this.StoragePath);
      return File.Exists(this.GetFilePath(id));
    }

    public TerminalData Read(TerminalDataIdentifier id)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      if (!Directory.Exists(this.StoragePath))
        Directory.CreateDirectory(this.StoragePath);
      return TerminalDataStorage.Deserialize(DESEncryptor.Decrypt(File.ReadAllBytes(this.GetFilePath(id)), Encoding.ASCII.GetBytes("lka@xx1-"), HexEncoding.GetBytes("0000000000000000"), CipherMode.CBC, PaddingMode.PKCS7));
    }

    public void Write(TerminalDataIdentifier id, TerminalData data)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      if (data == null)
        throw new ArgumentNullException("data");
      byte[] bytes = DESEncryptor.Encrypt(TerminalDataStorage.Serialize(data), Encoding.ASCII.GetBytes("lka@xx1-"), HexEncoding.GetBytes("0000000000000000"), CipherMode.CBC, PaddingMode.PKCS7);
      if (!Directory.Exists(this.StoragePath))
        Directory.CreateDirectory(this.StoragePath);
      File.WriteAllBytes(this.GetFilePath(id), bytes);
    }

    private string GetFilePath(TerminalDataIdentifier id)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      return Path.Combine(this.StoragePath, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}.tdx", new object[1]
      {
        (object) id.Hash
      }));
    }

    internal static byte[] Serialize(TerminalData data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream, Encoding.UTF8))
        {
          binaryWriter.Write(data.TerminalId ?? string.Empty);
          binaryWriter.Write(data.Serie ?? string.Empty);
          binaryWriter.Write(data.CheieCriptarePIN ?? string.Empty);
          binaryWriter.Write(data.EsteMediuTestare);
          int num1 = data.Profile == null ? 0 : data.Profile.Count;
          binaryWriter.Write(num1);
          if (data.Profile != null)
          {
            foreach (Profil profil in data.Profile)
            {
              binaryWriter.Write((int) profil.ProfilCard);
              int num2 = profil.Cheie == null ? 0 : profil.Cheie.Length;
              binaryWriter.Write(num2);
              if (profil.Cheie != null)
                binaryWriter.Write(profil.Cheie);
            }
          }
          binaryWriter.Flush();
          byte[] numArray = new byte[memoryStream.Length];
          Array.Copy((Array) memoryStream.GetBuffer(), (Array) numArray, numArray.Length);
          return numArray;
        }
      }
    }

    internal static TerminalData Deserialize(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      using (MemoryStream memoryStream = new MemoryStream(data))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream, Encoding.UTF8))
        {
          TerminalData terminalData = new TerminalData();
          terminalData.TerminalId = binaryReader.ReadString();
          terminalData.Serie = binaryReader.ReadString();
          terminalData.CheieCriptarePIN = binaryReader.ReadString();
          terminalData.EsteMediuTestare = binaryReader.ReadBoolean();
          if (string.IsNullOrEmpty(terminalData.TerminalId))
            terminalData.TerminalId = (string) null;
          if (string.IsNullOrEmpty(terminalData.Serie))
            terminalData.Serie = (string) null;
          if (string.IsNullOrEmpty(terminalData.CheieCriptarePIN))
            terminalData.CheieCriptarePIN = (string) null;
          int num = binaryReader.ReadInt32();
          if (num < 0 || num > 1000)
            throw new Exception("Structura fisierului este invalida (1).");
          for (int index = 0; index < num; ++index)
          {
            Profil profil = new Profil();
            profil.ProfilCard = (ProfileCard) binaryReader.ReadInt32();
            int count = binaryReader.ReadInt32();
            if (count < 0 || count > 1000)
              throw new Exception("Structura fisierului este invalida (2).");
            if (count > 0)
              profil.Cheie = binaryReader.ReadBytes(count);
            terminalData.Profile.Add(profil);
          }
          return terminalData;
        }
      }
    }
  }
}
