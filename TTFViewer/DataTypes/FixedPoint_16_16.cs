using System;

namespace TTFViewer.DataTypes
{
    public class FixedPoint_16_16
    {
        Int32 Value;

        public FixedPoint_16_16(Int16 integer, UInt16 fraction)
        {
            Value = (integer << 16) + fraction;
        }

        FixedPoint_16_16(Int32 value)
        {
            Value = value;
        }


        public Int32 GetIntPart()
        {
            return (Int16)(Value >> 16);
        }

        public void GetValue(out Int16 integer, out UInt16 fraction)
        {
            integer = (Int16)(Value >> 16);
            fraction = (UInt16)(Value & 0xffff);
        }


        #region Convert

        public static implicit operator Int32(FixedPoint_16_16 n)
        {
            return n.Value >> 16; ;
        }

        public override string ToString()
        {
            //Int16 intpart = (Int16)(array[1] << 8 | array[2]);
            //UInt16 fracpart = (UInt16)(array[3] | array[4]);
            //return $"{Value / Int16.MaxValue:F6}";
            return ToString("F");
        }
        
        public string ToString(string fmt)
        {
            if (string.IsNullOrEmpty(fmt))
                fmt = "F";
            switch(fmt.ToUpperInvariant())
            {
                case "D":
                    return $"{Value:x8}";
                default:
                    var d = ((double)(Math.Abs(Value))) / (double)(1 << 16);
                    if (Value < 0)
                        d = -d;
                    return $"{d:F6}";
            }
        }
       
        #endregion // Convert


        #region Compare

        public override bool Equals(object obj)
        {
            return obj is FixedPoint_16_16 f && f.Value == Value;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public static bool operator ==(FixedPoint_16_16 lhs, FixedPoint_16_16 rhs)
        {
            // stackoverflow 
            if (lhs is null)
                return rhs is null;
            else
                return !(rhs is null) && lhs.Value == rhs.Value;
        }

        public static bool operator !=(FixedPoint_16_16 lhs, FixedPoint_16_16 rhs)
        {
            return !(lhs == rhs);
        }


        public static explicit operator bool(FixedPoint_16_16 n)
        {
            return n.Value != 0;
        }

        #endregion // Compare

        
        #region math
        public static FixedPoint_16_16 operator -(FixedPoint_16_16 n)
        {
            return new FixedPoint_16_16(-n.Value);
        }

        public FixedPoint_16_16 Abs()
        {
            //return FieldValue >= 0 ? this : -this;
            return new FixedPoint_16_16(Math.Abs(Value));
        }

        static Random RandomGenerator = new Random();
        public static FixedPoint_16_16 Random()
        {
            var v = RandomGenerator.Next();
            return new FixedPoint_16_16((Int16)(v & 0xffff), 0);
        }

        public FixedPoint_16_16 Sqrt()
        {
            return new FixedPoint_16_16((Int32)Math.Sqrt(Value));
        }

        public static FixedPoint_16_16 operator + (FixedPoint_16_16 lhs, FixedPoint_16_16 rhs)
        {
            return new FixedPoint_16_16(lhs.Value + rhs.Value);
        }

        public static FixedPoint_16_16 operator - (FixedPoint_16_16 lhs, FixedPoint_16_16 rhs)
        {
            return new FixedPoint_16_16(lhs.Value - rhs.Value);
        }

        public static FixedPoint_16_16 operator *(FixedPoint_16_16 lhs, FixedPoint_16_16 rhs)
        {
            return new FixedPoint_16_16((Int32)(((Int64)lhs.Value * (Int64)rhs.Value) >> 16));
        }

        public static FixedPoint_16_16 operator / (FixedPoint_16_16 lhs, FixedPoint_16_16 rhs)
        {
            //return new FixedPoint_16_16(((Int32)(((Int64)lhs.FieldValue / (Int64)rhs.FieldValue) << 16));
            return new FixedPoint_16_16((Int32)((((Int64)lhs.Value) << 16) / (Int64)rhs.Value));
        }

        #endregion // Math
    }
}
