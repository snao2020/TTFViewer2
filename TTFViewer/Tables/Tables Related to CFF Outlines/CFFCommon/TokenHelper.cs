using System;
using System.Collections.Generic;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
    enum DICTTokenKind
    {
        None = 0,
        Operand = 1,
        Operator = 2,
        Error = 3,
    }

    [Flags]
    enum CFFTokenKind
    {
        Operand = 0x00010000,
        Operator = 0x00020000,
        ReadError = 0x00040000,

        Integer1 = Operand | 1,  // one byte
        Integer20 = Operand | 2, // two bytes positive
        Integer21 = Operand | 3, // two bytes negetive
        Integer3 = Operand | 4,  // three bytes, two byte integer 
        Integer5 = Operand | 5, // DICT only, five types, four bytes integer

        Fixed = Operand | 6, // Charstring only, 5 bytes, 

        Float = Operand | 7, // DICT only

        Operator1 = Operator | 1,
        Operator2 = Operator | 2,
    }

    static class TokenHelper
    {
        public static IEnumerable<uint8> GetEnumerable(PrimitiveReader reader, UInt32 filePosition, UInt32 fileLength)
        {
            var bytes = reader.CreatePrimitiveArray(filePosition, typeof(uint8), (Int32)(fileLength));
            foreach(var b in bytes)
            {
                yield return (uint8)b;
            }
            yield break;
        }

        public static uint8? ReadByte(IEnumerator<uint8> reader)
        {
            if (reader.MoveNext())
                return reader.Current;
            return null;
        }

        public static uint8[] ReadToken(IEnumerator<uint8> reader, uint8 b0, Int32 length)
        {
            var list = new List<uint8> { b0 };
            for (int i = 1; i < length; i++)
            {
                if (ReadByte(reader) is uint8 u8)
                    list.Add(u8);
                else
                    return null;
            }
            return list.ToArray();
        }


        public static uint8[] ReadBytes(IEnumerator<uint8> reader, Int32 length)
        {
            var array = new uint8[length];
            for (int i = 0; i < length; i++)
            {
                if (ReadByte(reader) is uint8 u8)
                    array[i] = u8;
                else
                    return null;
            }
            return array;
        }
    }
}

