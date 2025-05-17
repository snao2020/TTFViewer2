using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TTFViewer.DataTypes
{
#pragma warning disable IDE1006

    public interface ITTFPrimitive
    {
        void Load(BinaryReader reader);
    }


//------------------------------------------------


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct uint8 : ITTFPrimitive, IFormattable
    {
        Byte Value;

        public void Load(BinaryReader reader)
        {
            Value = reader.ReadByte();
        }


        public static implicit operator uint8(Byte b)
        {
            var ret = default(uint8);
            ret.Value = b;
            return ret;
        }

        public static implicit operator Byte(uint8 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}2";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct int8 : ITTFPrimitive, IFormattable
    {
        SByte Value;

        public void Load(BinaryReader reader)
        {
            Value = reader.ReadSByte();
        }


        public static implicit operator SByte(int8 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}2";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct uint16 : ITTFPrimitive, IFormattable
    {
        UInt16 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadUInt16(reader);
        }


        public static implicit operator UInt16(uint16 v)
        {
            return v.Value;
        }


        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}4";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct int16 : ITTFPrimitive, IFormattable
    {
        Int16 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadInt16(reader);
        }


        public static implicit operator Int16(int16 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}4";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct uint24 : ITTFPrimitive, IFormattable
    {
        Byte V0;
        Byte V1;
        Byte V2;

        public void Load(BinaryReader reader)
        {
            Byte[] bytes = new Byte[3];
            reader.Read(bytes, 0, 3);
            V0 = bytes[0];
            V1 = bytes[1];
            V2 = bytes[2];
        }


        public static implicit operator UInt32(uint24 v)
        {
            Byte[] bytes = new Byte[4];
            bytes[0] = v.V2;
            bytes[1] = v.V1;
            bytes[2] = v.V0;
            bytes[3] = 0;        
            return BitConverter.ToUInt32(bytes, 0);
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}6";
            UInt32 value = this;
            return value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct uint32 : ITTFPrimitive, IFormattable
    {
        UInt32 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadUInt32(reader);
        }


        public static implicit operator UInt32(uint32 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {   
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}8";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct int32 : ITTFPrimitive, IFormattable
    {
        Int32 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadInt32(reader);
        }


        public static implicit operator Int32(int32 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}8";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Fixed : ITTFPrimitive, IFormattable
    {
        Int32 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadInt32(reader);
        }


        public static implicit operator double(Fixed v)
        {
            return (double)v.Value / (double)(1 << 16);
        }

        public override string ToString()
        {
            return ToString("F");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            double value = this;
            return value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FWORD : ITTFPrimitive, IFormattable
    {
        Int16 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadInt16(reader);
        }


        public static implicit operator Int16(FWORD v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}4";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UFWORD : ITTFPrimitive, IFormattable
    {
        UInt16 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadUInt16(reader);
        }


        public static implicit operator UInt16(UFWORD v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("D");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}4";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct F2DOT14 : ITTFPrimitive, IFormattable
    {
        Int16 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadInt16(reader);
        }


        public static implicit operator double(F2DOT14 v)
        {
            double x = (double)v.Value / (1 << 14);
            return x;
        }

        public override string ToString()
        {
            return ToString("F");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            double v = this;
            return v.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LONGDATETIME : ITTFPrimitive, IFormattable
    {
        Int64 Value; // SecondsSince1904

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadInt64(reader);
        }


        public static implicit operator Int64(LONGDATETIME v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if(format == "X" || format == "x" || format == "D" || format== "d")
                return Value.ToString(format);
            DateTime start = new DateTime(1904, 1, 1, 0, 0, 0);
            return start.AddSeconds(Value).ToString();
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Tag : ITTFPrimitive, IFormattable
    {
        public static readonly Tag Null = string.Empty;

        UInt32 Value;   // ex. 'ttcf'(0x74746366)

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadUInt32(reader);
        }


        public static implicit operator UInt32(Tag tag)
        {
            return tag.Value;
        }


        public static implicit operator Tag(string text)
        {
            Tag result;

            if (text.Length == 4)
            {
                Byte[] bytes = Encoding.ASCII.GetBytes(text);
                Array.Reverse(bytes);
                result.Value = BitConverter.ToUInt32(bytes, 0);
            }
            else
                result.Value = 0;

            return result;
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x" || format == "D" || format == "d")
                return Value.ToString(format);
            Byte[] bytes = BitConverter.GetBytes(Value);
            Array.Reverse(bytes);
            string result;
            try
            {
                result = Encoding.ASCII.GetString(bytes);
            }
            catch (Exception)
            {
                result = $"(0x{Value:X8})";
            }
            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Offset8 : ITTFPrimitive, IFormattable
    {
        Byte Value;

        public void Load(BinaryReader reader)
        {
            Value = reader.ReadByte();
        }


        public static implicit operator Byte(Offset8 v)
        {
            return v.Value;
        }


        public override string ToString()
        {
            return $"0x{ToString("X")}";
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}2";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Offset16 : ITTFPrimitive, IFormattable
    {
        UInt16 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadUInt16(reader);
        }


        public static implicit operator UInt16(Offset16 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return $"0x{ToString("X")}";
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}4";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Offset24 : ITTFPrimitive, IFormattable
    {
        Byte V0;
        Byte V1;
        Byte V2;

        public void Load(BinaryReader reader)
        {
            Byte[] bytes = new Byte[3];
            reader.Read(bytes, 0, 3);
            V0 = bytes[0];
            V1 = bytes[1];
            V2 = bytes[2];
        }


        public static implicit operator UInt32(Offset24 v)
        {
            Byte[] bytes = new Byte[4];
            bytes[0] = v.V2;
            bytes[1] = v.V1;
            bytes[2] = v.V0;
            bytes[3] = 0;
            return BitConverter.ToUInt32(bytes, 0);
        }


        public override string ToString()
        {
            return $"0x{ToString("X")}";
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}6";
            UInt32 value = this;
            return value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Offset32 : ITTFPrimitive, IFormattable
    {
        UInt32 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadUInt32(reader);
        }


        public static implicit operator UInt32(Offset32 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return $"0x{ToString("X")}";
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}8";
            return Value.ToString(format);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Version16Dot16 : ITTFPrimitive, IFormattable
    {
        UInt32 Value;

        public void Load(BinaryReader reader)
        {
            Value = BigEndian.ReadUInt32(reader);
        }


        public static implicit operator UInt32(Version16Dot16 v)
        {
            return v.Value;
        }

        public override string ToString()
        {
            return ToString("X");
        }

        public String ToString(String format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "X" || format == "x")
                format = $"{format}8";
            return Value.ToString(format);
        }
    }


    static class BigEndian
    {
        public static string ReadAscii(BinaryReader reader, int count)
        {
            Byte[] bytes = reader.ReadBytes(count);
            return Encoding.ASCII.GetString(bytes);
        }


        public static Int16 ReadInt16(BinaryReader reader)
        {
            Byte[] bytes = reader.ReadBytes(sizeof(Int16));
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }


        public static UInt16 ReadUInt16(BinaryReader reader)
        {
            Byte[] bytes = reader.ReadBytes(sizeof(UInt16));
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }


        public static Int32 ReadInt32(BinaryReader reader)
        {
            Byte[] bytes = reader.ReadBytes(sizeof(Int32));
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }


        public static UInt32 ReadUInt32(BinaryReader reader)
        {
            Byte[] bytes = reader.ReadBytes(sizeof(UInt32));
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }


        public static Int64 ReadInt64(BinaryReader reader)
        {
            Byte[] bytes = reader.ReadBytes(sizeof(Int64));
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }


        public static UInt64 ReadUInt64(BinaryReader reader)
        {
            Byte[] bytes = reader.ReadBytes(sizeof(UInt64));
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }
    }

#pragma warning restore IDE1006 
}
