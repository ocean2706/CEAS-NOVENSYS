// Decompiled with JetBrains decompiler
// Type: Novensys.ASN1.Type.Asn1TypeReader
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
  public abstract class Asn1TypeReader : IDecoder
  {
    protected string _internalErrorLogDir = (string) null;
    protected bool _isContentToBeResolved = false;
    protected bool _isResolvingContent = true;
    protected bool _isValidating = false;
    public const string KEY_INTERNAL_ERROR_LOG_DIR = "internalErrorLogDir";
    public const string KEY_RESOLVING_CONTENT = "resolvingContent";
    public const string KEY_VALIDATING = "validating";
    private MemoryStream _byteArrayInputStream;

    public byte[] Data
    {
      set
      {
        if (value == null)
          throw new ArgumentException("value is null");
        this._byteArrayInputStream = new MemoryStream(value);
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

    public bool IsResolvingContent
    {
      get
      {
        return this._isResolvingContent;
      }
      set
      {
        this._isResolvingContent = value;
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

    protected abstract void close();

    public virtual void Decode(Asn1Type type)
    {
      if (this._byteArrayInputStream == null)
        throw new ArgumentException("data parameter has not been set");
      this.Decode((Stream) this._byteArrayInputStream, type);
    }

    public virtual void Decode(Stream inputStream, Asn1Type type)
    {
      if (inputStream == null)
        throw new ArgumentException("inputStream parameter is null");
      if (type == null)
        throw new ArgumentException("type parameter is null");
      try
      {
        type.ResetType();
        this.decodeImpl(inputStream, type);
        if (this._isContentToBeResolved && this._isResolvingContent)
          type.ResolveContent();
        if (!this._isValidating)
          return;
        type.Validate();
      }
      catch (StackOverflowException ex)
      {
        this.close();
        throw new Asn1Exception(98, ex.Message, this.UsedBytes());
      }
      catch (OutOfMemoryException ex)
      {
        this.close();
        throw new Asn1Exception(98, ex.Message, this.UsedBytes());
      }
      catch (Asn1ValidationException ex)
      {
        this.close();
        throw ex;
      }
      catch (Asn1Exception ex)
      {
        this.close();
        throw new Asn1Exception(ex, this.UsedBytes(), ex.InnerException);
      }
      catch (Exception ex)
      {
        this.close();
        Report.logException(this._internalErrorLogDir, ex);
        throw new Asn1Exception(99, ex.Message);
      }
    }

    public virtual void Decode(byte[] data, Asn1Type type)
    {
      if (data == null)
        throw new ArgumentException("data parameter is null");
      this.Decode((Stream) new MemoryStream(data), type);
    }

    public virtual void Decode(byte[] data, int offset, int length, Asn1Type type)
    {
      if (data == null)
        throw new ArgumentException("data parameter is null");
      if (offset < 0 || offset + length > data.Length)
        throw new ArgumentException("offset (" + (object) offset + ") and length (" + (string) (object) length + ") are not correct for data.Length (" + (string) (object) data.Length + ").");
      else
        this.Decode((Stream) new MemoryStream(data, offset, length), type);
    }

    protected abstract void decodeImpl(Stream inputStream, Asn1Type type);

    public abstract string GetProperty(string key);

    protected abstract void init(Stream input);

    public virtual IList PropertyNames()
    {
      return (IList) null;
    }

    public virtual void SetProperties(Hashtable properties)
    {
      if (properties == null)
        return;
      foreach (string key in (IEnumerable) properties.Keys)
        this.SetProperty(key, (string) properties[(object) key]);
    }

    public abstract void SetProperty(string key, string property);

    public abstract int UsedBytes();
  }
}
