// Decompiled with JetBrains decompiler
// Type: Novensys.eCard.SDK.Utils.Hex.HexEncoding
// Assembly: Novensys.eCard.SDK, Version=1.1.56.0, Culture=neutral, PublicKeyToken=null
// MVID: 59F9E456-D3DA-4066-A4A4-692B516A775E
// Assembly location: C:\Program Files (x86)\CNAS\SIUI-SPITAL\Novensys.eCard.SDK.dll

using System;

namespace Novensys.eCard.SDK.Utils.Hex
{
  public class HexEncoding
  {
    private static bool _ReverseLenValueByteOrder = false;
    private static int countPostFixTockenLENPlaceholder = 0;
    private static int countPreFixTockenLENPlaceholder = 0;
    private static uint lenPlaceholderValue = 0U;
    private static int MAX_LEN_PLACEHOLDER = 4;

    public static bool ReverseLenValueByteOrder
    {
      get
      {
        return HexEncoding._ReverseLenValueByteOrder;
      }
      set
      {
        HexEncoding._ReverseLenValueByteOrder = value;
      }
    }

    private HexEncoding()
    {
    }

    public static int GetByteCount(string hexString)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      HexEncoding.countPreFixTockenLENPlaceholder = 0;
      HexEncoding.countPostFixTockenLENPlaceholder = 0;
      if (string.IsNullOrEmpty(hexString))
        throw new ArgumentNullException("hexString");
      if (hexString.Equals(""))
        return 0;
      for (int startindex = 0; startindex < hexString.Length; ++startindex)
      {
        char ch = hexString[startindex];
        if (num8 == 0)
        {
          if (!HexUtil.IsHexChar(ch))
          {
            if (!HexEncoding.IsBlankChar(ch) && !HexEncoding.IsCRChar(ch) && !HexEncoding.IsLFChar(ch))
            {
              if (!HexEncoding.IsDoubleQuoteChar(ch))
              {
                if (HexEncoding.IsLenPlaceholderChar(ch))
                {
                  if (HexEncoding.countPreFixTockenLENPlaceholder != 0)
                  {
                    if (num7 != 1)
                      throw new ArgumentException(" ) expected");
                    ++HexEncoding.countPostFixTockenLENPlaceholder;
                  }
                  else
                  {
                    ++HexEncoding.countPreFixTockenLENPlaceholder;
                    num8 = 2;
                  }
                }
                else if (!HexEncoding.IsOpenBracketChar(ch) || num6 != 0)
                {
                  if (!HexEncoding.IsCloseBracketChar(ch) || num7 != 0)
                  {
                    if (!HexEncoding.IsCommentChar(ch))
                      throw new ArgumentException("Incorrect character " + ch.ToString() + " detected within the hexString.");
                    else
                      break;
                  }
                  else
                  {
                    ++num7;
                    HexEncoding.lenPlaceholderValue = (uint) ((num1 - num3) / 2);
                    HexEncoding.lenPlaceholderValue += (uint) (num2 - num4);
                  }
                }
                else
                {
                  ++num6;
                  num3 = num1;
                  num4 = num2;
                }
              }
              else
              {
                ++num5;
                num8 = 1;
              }
            }
          }
          else
            ++num1;
        }
        else if (num8 == 1)
        {
          if (HexEncoding.IsDoubleQuoteChar(ch))
          {
            ++num5;
            num8 = 0;
          }
          else if (!HexEncoding.IsCRChar(ch) && !HexEncoding.IsLFChar(ch))
          {
            if (HexEncoding.IsEscapeChar(hexString, startindex))
            {
              ++startindex;
              ++num2;
            }
            else
              ++num2;
          }
        }
        else
        {
          if (num8 != 2)
            throw new ApplicationException("HexEncoding unknown error");
          if (HexEncoding.IsLenPlaceholderChar(ch) && HexEncoding.countPreFixTockenLENPlaceholder < HexEncoding.MAX_LEN_PLACEHOLDER)
            ++HexEncoding.countPreFixTockenLENPlaceholder;
          else if (HexUtil.IsHexChar(ch))
          {
            ++num1;
            num8 = 0;
          }
          else if (!HexEncoding.IsBlankChar(ch) && !HexEncoding.IsCRChar(ch) && !HexEncoding.IsLFChar(ch))
          {
            if (!HexEncoding.IsDoubleQuoteChar(ch))
            {
              if (!HexEncoding.IsOpenBracketChar(ch) || num6 != 0)
              {
                if ((int) ch == 35)
                  throw new ArgumentException("The maximal number of # length placeholder characters is " + (object) HexEncoding.MAX_LEN_PLACEHOLDER + ".");
                else
                  throw new ArgumentException(" ( expected");
              }
              else
              {
                ++num6;
                num3 = num1;
                num4 = num2;
                num8 = 0;
              }
            }
            else
            {
              ++num5;
              num8 = 1;
            }
          }
        }
      }
      if (num1 % 2 != 0)
        throw new ArgumentException("Odd number of hex digits required.");
      if (num5 % 2 != 0)
        throw new ArgumentException("\" expected");
      if (HexEncoding.countPreFixTockenLENPlaceholder > HexEncoding.MAX_LEN_PLACEHOLDER)
        throw new ArgumentException("Incorrect Number of # length placeholder characters.");
      if (HexEncoding.countPreFixTockenLENPlaceholder > 0 && num6 != 1)
        throw new ArgumentException(" ( expected");
      if (HexEncoding.countPreFixTockenLENPlaceholder > 0 && num7 != 1)
        throw new ArgumentException(" ) expected");
      if (HexEncoding.countPostFixTockenLENPlaceholder > 0 && HexEncoding.countPostFixTockenLENPlaceholder != HexEncoding.countPreFixTockenLENPlaceholder)
        throw new ArgumentException(" The number of Postfix # and Prefix # must be equal.");
      else
        return num1 / 2 + HexEncoding.countPreFixTockenLENPlaceholder + HexEncoding.countPostFixTockenLENPlaceholder + num2;
    }

    public static byte[] GetBytes(string hexString)
    {
      int num1 = 0;
      int dstOffset = 0;
      bool flag = false;
      if (hexString.Equals((string) null))
        throw new ArgumentNullException("hexString");
      if (hexString.Equals(""))
        return (byte[]) null;
      byte[] numArray1 = new byte[HexEncoding.GetByteCount(hexString)];
      for (int startindex = 0; startindex < hexString.Length; ++startindex)
      {
        char ch = hexString[startindex];
        switch (num1)
        {
          case 0:
            if (!HexEncoding.IsBlankChar(ch) && !HexEncoding.IsCRChar(ch) && !HexEncoding.IsLFChar(ch) && !HexEncoding.IsOpenBracketChar(ch))
            {
              if (!HexUtil.IsHexChar(ch))
              {
                if (!HexEncoding.IsDoubleQuoteChar(ch))
                {
                  if (!HexEncoding.IsLenPlaceholderChar(ch))
                  {
                    if (!HexEncoding.IsCloseBracketChar(ch))
                    {
                      if (!HexEncoding.IsCommentChar(ch))
                        throw new ArgumentException("Incorrect character " + ch.ToString() + " detected within the hexString.");
                      else
                        return numArray1;
                    }
                    else
                      flag = false;
                  }
                  else if (!flag)
                  {
                    byte[] numArray2 = HexUtil.UintToByteArray(HexEncoding.lenPlaceholderValue, HexEncoding._ReverseLenValueByteOrder);
                    if (numArray2.Length > HexEncoding.countPreFixTockenLENPlaceholder)
                      throw new ArgumentException("To less # length placeholder characters.");
                    int num2 = HexEncoding.countPreFixTockenLENPlaceholder - numArray2.Length;
                    if (!HexEncoding._ReverseLenValueByteOrder)
                      Buffer.BlockCopy((Array) numArray2, 0, (Array) numArray1, dstOffset + num2, numArray2.Length);
                    else
                      Buffer.BlockCopy((Array) numArray2, 0, (Array) numArray1, dstOffset, numArray2.Length);
                    dstOffset += HexEncoding.countPreFixTockenLENPlaceholder;
                    flag = true;
                  }
                }
                else
                  num1 = 1;
              }
              else
              {
                if (!HexUtil.IsHexChar(hexString[startindex + 1]))
                  throw new ArgumentException("Correct format of hex digits required.");
                numArray1[dstOffset++] = HexUtil.HexToByte(hexString, startindex++);
              }
              break;
            }
            else
              break;
          case 1:
            if (HexEncoding.IsDoubleQuoteChar(ch))
            {
              num1 = 0;
              break;
            }
            else if (!HexEncoding.IsCRChar(ch) && !HexEncoding.IsLFChar(ch))
            {
              if (HexEncoding.IsEscapeChar(hexString, startindex))
              {
                switch (hexString[++startindex])
                {
                  case 'f':
                    numArray1[dstOffset++] = (byte) 12;
                    continue;
                  case 'n':
                    numArray1[dstOffset++] = (byte) 10;
                    continue;
                  case 'r':
                    numArray1[dstOffset++] = (byte) 13;
                    continue;
                  case 's':
                  case 'u':
                    continue;
                  case 't':
                    numArray1[dstOffset++] = (byte) 9;
                    continue;
                  case 'v':
                    numArray1[dstOffset++] = (byte) 11;
                    continue;
                  case '\\':
                    numArray1[dstOffset++] = (byte) 92;
                    continue;
                  case 'a':
                    numArray1[dstOffset++] = (byte) 7;
                    continue;
                  case 'b':
                    numArray1[dstOffset++] = (byte) 8;
                    continue;
                  case '"':
                    numArray1[dstOffset++] = (byte) 34;
                    continue;
                  case '\'':
                    numArray1[dstOffset++] = (byte) 39;
                    continue;
                  case '0':
                    numArray1[dstOffset++] = (byte) 0;
                    continue;
                  default:
                    continue;
                }
              }
              else
              {
                numArray1[dstOffset++] = Convert.ToByte(hexString[startindex]);
                break;
              }
            }
            else
              break;
        }
      }
      return numArray1;
    }

    private static bool IsBlankChar(char value)
    {
      return (int) value == 32;
    }

    private static bool IsCloseBracketChar(char value)
    {
      return (int) value == 41;
    }

    private static bool IsCommentChar(char value)
    {
      char[] anyOf = new char[1]
      {
        '/'
      };
      return value.ToString().IndexOfAny(anyOf) >= 0;
    }

    private static bool IsCRChar(char value)
    {
      return (int) value == 13;
    }

    private static bool IsDoubleQuoteChar(char value)
    {
      return (int) value == 34;
    }

    private static bool IsEscapeChar(string value, int startindex)
    {
      char[] anyOf = new char[11]
      {
        '0',
        'a',
        'b',
        't',
        'n',
        'v',
        'f',
        'r',
        '"',
        '\'',
        '\\'
      };
      if (startindex + 1 > value.Length || (int) value[startindex] != 92)
        return false;
      else
        return value[startindex + 1].ToString().IndexOfAny(anyOf) >= 0;
    }

    private static bool IsLenPlaceholderChar(char value)
    {
      return (int) value == 35;
    }

    private static bool IsLFChar(char value)
    {
      return (int) value == 10;
    }

    private static bool IsOpenBracketChar(char value)
    {
      return (int) value == 40;
    }
  }
}
