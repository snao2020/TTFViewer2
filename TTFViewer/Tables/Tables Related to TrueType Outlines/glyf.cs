// ver 1.9.1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;
using TTFViewer.ViewModel;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [FontTable("glyf")]
    [ClassTypeSelect(ClassValueKind.FontTableType, "loca", null)]
    [ClassTypeCondition(typeof(glyfTableShortVersion), AttributeConditionKind.Equal, ClassValueKind.Type, "TTFViewer.Tables.locaTableShortFormat")]
    [ClassTypeCondition(typeof(glyfTableLongVersion), AttributeConditionKind.Equal, ClassValueKind.Type, "TTFViewer.Tables.locaTableLongFormat")]
    [Invalid]
    [TypeName("glyf - Glyph Data")]
    [BaseName("glyf")]
    class glyfTable
    {
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("glyf - Glyph Datas")]
    [BaseName("glyf")]
    class glyfTableShortVersion
    {
        [Offsets(0, "\\", FieldValueKind.FontTableValue, "loca\\offsets", "Mul:2")]
        [TypeName("")]
        [FieldName(0, "glyph descriptions")]
        [FieldName(1, FieldNameKind.Method, "glyphDescriptionName")]
        public IList<GlyphDescription> glyphDescriptions;

        static String glyphDescriptionName(IItemValueService ivs)
        {
            var index = TablePathHelper.GetLastIndex(ivs.Name);
            return $"glyphID#{index}";
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("glyf - Glyph Data")]
    [BaseName("glyf")]
    class glyfTableLongVersion
    {
        [Offsets(0, "\\", FieldValueKind.FontTableValue, "loca\\offsets", null)]
        [TypeName("")]
        [FieldName(0, "glyph descriptions")]
        [FieldName(1, FieldNameKind.Method, "glyphDescriptionName")]
        public IList<GlyphDescription> glyphDescriptions;

        static String glyphDescriptionName(IItemValueService ivs)
        {
            var index = TablePathHelper.GetLastIndex(ivs.Name);
            return $"glyphID#{index}";
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "Header\\numberOfContours", "Mask:0x8000")]
    [ClassTypeCondition(typeof(SimpleGlyphDescription), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0")]
    [ClassTypeCondition(typeof(CompositeGlyphDescription), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x8000")]
    [TypeName("glyph description")]
    class GlyphDescription
    {
        [FieldName(0, null)]
        public GlyphHeader Header;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Glyph Header")]
    class GlyphHeader
    {
        [Description(0, "If the number of contours is greater than or equal to zero, this is a simple glyph.If negative, this is a composite glyph — the value -1 should be used for composite glyphs.")]
        public int16 numberOfContours;

        [Description(0, "Minimum x for coordinate data.")]
        public int16 xMin;

        [Description(0, "Minimum y for coordinate data.")]
        public int16 yMin;

        [Description(0, "Maximum x for coordinate data.")]
        public int16 xMax;

        [Description(0, "Maximum y for coordinate data.")]
        public int16 yMax;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Simple glyph description")]
    class SimpleGlyphDescription : GlyphDescription
    {
        //public GlyphHeader Header;

        [FieldName(0, null)]
        public SimpleGlyph Glyph;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Simple Glyph")]
    class SimpleGlyph
    {
        [Count(0, FieldValueKind.Path, "..\\Header\\numberOfContours")]
        [Description(0, "Array of point indices for the last point of each contour, in increasing numeric order.")]
        public uint16[] endPtsOfContours;

        [Description(0, "Total number of bytes for instructions.If instructionLength is zero, no instructions are present for this glyph, and this field is followed directly by the flags field.")]
        public uint16 instructionLength;

        [Count(0, FieldValueKind.Path, "instructionLength")]
        [Description(0, "Array of instruction byte code for the glyph.")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(1, DescriptionKind.Instruction, null)]
        public uint8[] instructions;
        
        [Values(0, "flagsMethod")]
        [Description(0, "Array of flag elements. See below for details regarding the number of flag array elements.")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(1, DescriptionKind.Method, "flagsDescription")]
        public uint8[] flags;
        
        [Offsets(0, "xCoordinatePositionsMethod")]
        [TypeSelect(FieldValueKind.ParentConstraint, null, null)]
        [TypeCondition(typeof(uint8), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]
        [TypeCondition(typeof(int16), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "2")]
        [TypeName("uint8 or int16")]
        [ValueFormat(1, ValueFormatKind.Default, Option = ValueFormatOption.RawType)]
        [Description(0, "Contour point y-coordinates. See below for details regarding the number of coordinate array elements. Coordinate for the first point is relative to (0,0); others are relative to previous point")]
        public IList<ITTFPrimitive> xCoordinates; // [variable]  Contour point y-coordinates. See below for details regarding the number of coordinate array elements. Coordinate for the first point is relative to (0,0); others are relative to previous point

        [Offsets(0, "yCoordinatePositionsMethod")]
        [TypeSelect(FieldValueKind.ParentConstraint, null, null)]
        [TypeCondition(typeof(uint8), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "1")]
        [TypeCondition(typeof(int16), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "2")]
        [TypeName("uint8 or int16")]
        [ValueFormat(1, ValueFormatKind.Default, Option = ValueFormatOption.RawType)]
        [Description(0, "Contour point y-coordinates. See below for details regarding the number of coordinate array elements. Coordinate for the first point is relative to (0,0); others are relative to previous point.")]
        public IList<ITTFPrimitive> yCoordinates;

        static (UInt32,IEnumerable<uint8>)? flagsMethod(IAttributeService service)
        {
            if (service.GetValues(FieldValueKind.Path, "endPtsOfContours").SingleValue(0) is IList<uint16> endPtsOfContours)
            {
                var reader = service.PrimitiveReader;
                UInt32 startPosition = service.FilePosition;
                UInt32 filePosition = startPosition;
                var list = new List<uint8>();

                int pointCount = (int)endPtsOfContours.Last() + 1;
                while (pointCount > 0)
                {
                    pointCount--;

                    uint8 flag = reader.Read<uint8>(filePosition++);
                    list.Add(flag);

                    if ((flag & (Byte)SimpleGlyphFlags.REPEAT_FLAG) != 0)
                    {
                        uint8 repeat = reader.Read<uint8>(filePosition++);
                        list.Add(repeat);
                        pointCount -= repeat;
                    }
                }
                if (list.Count > 0)
                {
                    //var array = list.ToArray();
                    //return i => array;
                    return (filePosition - startPosition, list);
                }
            }
            return null;
        }

        static string flagsDescription(IItemValueService ivp)
        {
            if (ivp.Value is uint8 flag)
            {
                Int32 index = Int32.Parse(ivp.Name.Substring(1, ivp.Name.Length - 2));
                if (index > 0)
                {
                    if (ivp.Parent.Value is uint8[] array && (array[index - 1] & (Int32)SimpleGlyphFlags.REPEAT_FLAG) != 0)
                    {
                        return "repeat count";
                    }
                }
                return ItemValueHelper.GetEnumItemName(typeof(SimpleGlyphFlags), flag);
            }
            return null;
        }

        static IList<UInt32> xCoordinatePositionsMethod(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.Path, "flags");
            var result = GetCoordinatePositions(service.Path, service.FilePosition, values, SimpleGlyphFlags.X_SHORT_VECTOR, SimpleGlyphFlags.X_IS_SAME_OR_POSITIVE_X_SHORT_VECTOR);
            return result;
        }

        static IList<UInt32> yCoordinatePositionsMethod(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.Path, "flags");
            var result = GetCoordinatePositions(service.Path, service.FilePosition, values, SimpleGlyphFlags.Y_SHORT_VECTOR, SimpleGlyphFlags.Y_IS_SAME_OR_POSITIVE_Y_SHORT_VECTOR);
            return result;
        }

        static IList<UInt32> GetCoordinatePositions(string path, UInt32 filePosition, object[] values, SimpleGlyphFlags shortVectorFlag, SimpleGlyphFlags isSameOrPositiveShortVectorFlag)
        {
            IList<UInt32> result = null;

            if (values.SingleValue(0) is IList<uint8> flags)
            {
                result = new List<UInt32> { filePosition };
                for (int i = 0; i < flags.Count; i++)
                {
                    uint8 flag = flags[i];
                    int repeatCount = 1;

                    if ((flag & (Byte)SimpleGlyphFlags.REPEAT_FLAG) != 0)
                    {
                        uint8 repeat = flags[++i];
                        repeatCount += repeat;
                    }

                    UInt32 length = 0;
                    if ((flag & (Byte)shortVectorFlag) != 0)
                    {
                        length = 1;
                    }
                    else if ((flag & (Byte)isSameOrPositiveShortVectorFlag) == 0)
                    {
                        length = 2 ;
                    }
                    if (length != 0)
                    {
                        for (int j = 0; j < repeatCount; j++)
                        {
                            result.Add(result.Last() + length);
                        }
                    }
                }
            }
            return result;
        }
    }


    [Flags]
    [TypeName("Simple Glyph")]
    enum SimpleGlyphFlags
    {
        ON_CURVE_POINT = 0x01, // Bit 0: If set, the point is on the curve; otherwise, it is off the curve.
        X_SHORT_VECTOR = 0x02, // Bit 1: If set, the corresponding x-coordinate is 1 byte long. If not set, it is two bytes long. For the sign of this value, see the description of the X_IS_SAME_OR_POSITIVE_X_SHORT_VECTOR flag.
        Y_SHORT_VECTOR = 0x04, // Bit 2: If set, the corresponding y-coordinate is 1 byte long. If not set, it is two bytes long. For the sign of this value, see the description of the Y_IS_SAME_OR_POSITIVE_Y_SHORT_VECTOR flag.
        REPEAT_FLAG = 0x08, // Bit 3: If set, the next byte (read as unsigned) specifies the number of additional times this flag byte is to be repeated in the logical flags array — that is, the number of additional logical flag entries inserted after this entry. (In the expanded logical array, this bit is ignored.) In this way, the number of flags listed can be smaller than the number of points in the glyph description.
        X_IS_SAME_OR_POSITIVE_X_SHORT_VECTOR = 0x10, // Bit 4: This flag has two meanings, depending on how the X_SHORT_VECTOR flag is set. If X_SHORT_VECTOR is set, this bit describes the sign of the value, with 1 equalling positive and 0 negative. If X_SHORT_VECTOR is not set and this bit is set, then the current x-coordinate is the same as the previous x-coordinate. If X_SHORT_VECTOR is not set and this bit is also not set, the current x-coordinate is a signed 16-bit delta vector.
        Y_IS_SAME_OR_POSITIVE_Y_SHORT_VECTOR = 0x20, // Bit 5: This flag has two meanings, depending on how the Y_SHORT_VECTOR flag is set. If Y_SHORT_VECTOR is set, this bit describes the sign of the value, with 1 equalling positive and 0 negative. If Y_SHORT_VECTOR is not set and this bit is set, then the current y-coordinate is the same as the previous y-coordinate. If Y_SHORT_VECTOR is not set and this bit is also not set, the current y-coordinate is a signed 16-bit delta vector.
        OVERLAP_SIMPLE = 0x40, // Bit 6: If set, contours in the glyph description may overlap. Use of this flag is not required in OpenType — that is, it is valid to have contours overlap without having this flag set. It may affect behaviors in some platforms, however. (See the discussion of “Overlapping contours” in Apple’s specification for details regarding behavior in Apple platforms.) When used, it must be set on the first flag byte for the glyph. See additional details below.
        Reserved = 0x80, // Bit 7 is reserved: set to zero.    
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Composite glyph description")]
    class CompositeGlyphDescription : GlyphDescription
    {
        //public GlyphHeader Header;

        [Offsets(0, "ComponentGlyphTablesMethod")]
        [FieldName(0, null)]
        public ComponentGlyph[] ComponentGlyphTables;

        [TypeSelect(FieldValueKind.Path, "ComponentGlyphTables\\[LAST]\\flags", "Mask:0x0100")] // 0x0100=WE_HAVE_INSTRUCTIONS
        [TypeCondition(typeof(uint16), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.CompositeGlyphFlags.WE_HAVE_INSTRUCTIONS")]
        [TypeName("uint16")]
        [Description(0, "\"instructionLength\" is not documented")]
        public uint16? instructionLength;

        [Count(0, FieldValueKind.Path, "instructionLength")]
        [Description(0, "\"instructions\" is not documented")]
        [ValueFormat(1, ValueFormatKind.Hex)]
        [Description(1, DescriptionKind.Instruction, null)]
        public IList<uint8> instructions;

        static IList<UInt32> ComponentGlyphTablesMethod(IAttributeService service)
        {
            var filePosition = service.FilePosition;
            var result = new List<UInt32> { filePosition };
            var primitiveReader = service.PrimitiveReader;
            for (; ; )
            {
                uint16 flags = primitiveReader.Read<uint16>(filePosition);
                filePosition += ComponentGlyphTableLength(flags);

                result.Add(filePosition);
                
                bool moreComponents = ((Int32)flags & (Int32)CompositeGlyphFlags.MORE_COMPONENTS) != 0;
                if (!moreComponents)
                    break;
            }
            return result;
        }

        static UInt32 ComponentGlyphTableLength(uint16 flags)
        {
            UInt32 result = 0;

            // add sizeof flags
            result += (UInt32)Marshal.SizeOf(typeof(uint16));

            // add sizeeof glyphIndex
            result += (UInt32)Marshal.SizeOf(typeof(uint16));

            if ((flags & (Int32)CompositeGlyphFlags.ARG_1_AND_2_ARE_WORDS) != 0)
                result += (UInt32)Marshal.SizeOf(typeof(int16)) * 2;
            else
                result += (UInt32)Marshal.SizeOf(typeof(int8)) * 2;

            UInt32 transformationOptionCount = 0;
            if ((flags & (Int32)CompositeGlyphFlags.WE_HAVE_A_SCALE) != 0)
                transformationOptionCount = 1;
            else if ((flags & (Int32)CompositeGlyphFlags.WE_HAVE_AN_X_AND_Y_SCALE) != 0)
                transformationOptionCount = 2;
            else if ((flags & (Int32)CompositeGlyphFlags.WE_HAVE_A_TWO_BY_TWO) != 0)
                transformationOptionCount = 4;
            var transformationOptionSize = (UInt32)Marshal.SizeOf(typeof(F2DOT14)) * transformationOptionCount;
            result += transformationOptionSize;

            return result;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("Component Glyph")]
    class ComponentGlyph
    {
        [ValueFormat(0, ValueFormatKind.Hex)]
        //[Description(0, DescriptionKind.Method, "flagsDescription")]
        [Description(0, typeof(CompositeGlyphFlags))]
        public uint16 flags;

        [Description(0, "glyph index of component")]
        public uint16 glyphIndex;

        [TypeSelect(FieldValueKind.Path, "flags", "Mask:0x0003")]
        [TypeCondition(typeof(uint8), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0")]
        [TypeCondition(typeof(int8), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.CompositeGlyphFlags.ARGS_ARE_XY_VALUES")]
        [TypeCondition(typeof(uint16), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.CompositeGlyphFlags.ARG_1_AND_2_ARE_WORDS")]
        [TypeCondition(typeof(int16), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.CompositeGlyphFlags.ARG_1_AND_2_ARE_WORDS | TTFViewer.Tables.CompositeGlyphFlags.ARGS_ARE_XY_VALUES")]
        [TypeName("uint8,int8,uint16 or int16")]
        [ValueFormat(0, ValueFormatKind.Default, Option = ValueFormatOption.RawType)]
        public ITTFPrimitive argument1;

        [TypeSelect(FieldValueKind.Path, "flags", "Mask:0x0003")]
        [TypeCondition(typeof(uint8), AttributeConditionKind.Equal, FieldValueKind.Unsigned, "0")]
        [TypeCondition(typeof(int8), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.CompositeGlyphFlags.ARGS_ARE_XY_VALUES")]
        [TypeCondition(typeof(uint16), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.CompositeGlyphFlags.ARG_1_AND_2_ARE_WORDS")]
        [TypeCondition(typeof(int16), AttributeConditionKind.Equal, FieldValueKind.Enum, "TTFViewer.Tables.CompositeGlyphFlags.ARG_1_AND_2_ARE_WORDS | TTFViewer.Tables.CompositeGlyphFlags.ARGS_ARE_XY_VALUES")]
        [TypeName("uint8,int8,uint16 or int16")]
        [ValueFormat(0, ValueFormatKind.Default, Option = ValueFormatOption.RawType)]
        public ITTFPrimitive argument2;

        [Count(0, "TransformationOptionMethod")]
        [FieldName(0, "Transformation Option")]
        public F2DOT14[] TransformationOption;

        private static Int32 TransformationOptionMethod(IAttributeService service)
        {
            Int32 result = 0;

            var values = service.GetValues(FieldValueKind.Path, "flags");
            if(values[0] is uint16 flags)
            {
                if ((flags & (Int32)(CompositeGlyphFlags.WE_HAVE_A_SCALE)) != 0)
                    result = 1;
                else if ((flags & (Int32)(CompositeGlyphFlags.WE_HAVE_AN_X_AND_Y_SCALE)) != 0)
                    result = 2;
                else if ((flags & (Int32)(CompositeGlyphFlags.WE_HAVE_A_TWO_BY_TWO)) != 0)
                    result = 4;
            }
            return result;
        }       
    }


    [Flags]
    [TypeName("Composite Glyph")]
    enum CompositeGlyphFlags
    {
        ARG_1_AND_2_ARE_WORDS = 0x0001, // Bit 0: If this is set, the arguments are 16-bit (uint16 or int16); otherwise, they are bytes (uint8 or int8).
        ARGS_ARE_XY_VALUES = 0x0002, // Bit 1: If this is set, the arguments are signed xy values; otherwise, they are unsigned point numbers.
        ROUND_XY_TO_GRID = 0x0004, // Bit 2: For the xy values if the preceding is true.
        WE_HAVE_A_SCALE = 0x0008, // Bit 3: This indicates that there is a simple scale for the component. Otherwise, scale = 1.0.
        MORE_COMPONENTS = 0x0020, // Bit 5: Indicates at least one more glyph after this one.
        WE_HAVE_AN_X_AND_Y_SCALE = 0x0040, // Bit 6: The x direction will use a different scale from the y direction.
        WE_HAVE_A_TWO_BY_TWO = 0x0080, // Bit 7: There is a 2 by 2 transformation that will be used to scale the component.
        WE_HAVE_INSTRUCTIONS = 0x0100, // Bit 8: Following the last component are instructions for the composite character.
        USE_MY_METRICS = 0x0200, // Bit 9: If set, this forces the aw and lsb (and rsb) for the composite to be equal to those from this original glyph. This works for hinted and unhinted characters.
        OVERLAP_COMPOUND = 0x0400, // Bit 10: If set, the components of the compound glyph overlap. Use of this flag is not required in OpenType — that is, it is valid to have components overlap without having this flag set. It may affect behaviors in some platforms, however. (See Apple’s specification for details regarding behavior in Apple platforms.) When used, it must be set on the flag word for the first component. See additional remarks, above, for the similar OVERLAP_SIMPLE flag used in simple-glyph descriptions.
        SCALED_COMPONENT_OFFSET = 0x0800, // Bit 11: The composite is designed to have the component offset scaled.
        UNSCALED_COMPONENT_OFFSET = 0x1000, // Bit 12: The composite is designed not to have the component offset scaled.
        Reserved = 0xE010, // Bits 4, 13, 14 and 15 are reserved: set to 0.
    }

#pragma warning restore IDE1006
}
