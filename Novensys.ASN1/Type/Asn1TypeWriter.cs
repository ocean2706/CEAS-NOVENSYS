// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeWriter
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using Novensys.ASN1;
using Novensys.ASN1.Util;
using System;
using System.Collections;
using System.IO;

namespace Novensys.ASN1.Type
{
  [Serializable]
  public abstract class Asn1TypeWriter : IEncoder
  {
    protected string _internalErrorLogDir = (string) null;
    protected bool _isEncodingDefaultValues = false;
    protected bool _isValidating = false;
    public const string KEY_ENCODING_DEFAULT_VALUES = "encodingDefaultValues";
    public const string KEY_INTERNAL_ERROR_LOG_DIR = "internalErrorLogDir";
    public const string KEY_VALIDATING = "validating";
    private MemoryStream _byteArrayOutputStream;

    public byte[] Data
    {
      get
      {
        if (this._byteArrayOutputStream != null)
          return this._byteArrayOutputStream.ToArray();
        else
          return (byte[]) null;
      }
    }

    public string InternalErrorLogDir
    {
      get
      {
        return this._internalErrorLogDir;
      }
      set
      {
        this._internalErrorLogDir = value;
      }
    }

    public bool IsValidating
    {
      get
      {
        return this._isValidating;
      }
      set
      {
        this._isValidating = value;
      }
    }

    public virtual void Encode(Asn1Type type)
    {
      this._byteArrayOutputStream = new MemoryStream(512);
      this.Encode(type, (Stream) this._byteArrayOutputStream);
    }

    public virtual void Encode(Asn1Type type, Stream outputStream)
    {
      if (type == null || outputStream == null)
        throw new ArgumentException("parameter is null");
      try
      {
        if (this._isValidating)
          type.Validate();
        this.encodeImpl(type, outputStream);
      }
      catch (StackOverflowException ex)
      {
        throw new Asn1Exception(98, ex.Message);
      }
      catch (OutOfMemoryException ex)
      {
        throw new Asn1Exception(98, ex.Message);
      }
      catch (Asn1Exception ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        Report.logException(this._internalErrorLogDir, ex);
        throw new Asn1Exception(99, ex.Message);
      }
      finally
      {
        this.reset();
      }
    }

    protected abstract void encodeImpl(Asn1Type type, Stream outputStream);

    protected abstract void flush();

    public abstract string GetProperty(string key);

    protected abstract void init(Stream output);

    public virtual IList PropertyNames()
    {
      return (IList) null;
    }

    protected abstract void reset();

    public virtual void SetProperties(Hashtable properties)
    {
      if (properties == null)
        return;
      foreach (string key in (IEnumerable) properties.Keys)
        this.SetProperty(key, (string) properties[(object) key]);
    }

    public abstract void SetProperty(string key, string property);
  }
}
