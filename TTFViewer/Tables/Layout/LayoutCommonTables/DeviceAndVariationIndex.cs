//var1.9.1
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TTFViewer.DataTypes;
using TTFViewer.Loaders;


namespace TTFViewer.Tables
{
#pragma warning disable IDE1006

    enum DeltaFormat
    {
        LOCAL_2_BIT_DELTAS = 0x0001, // Signed 2-bit value, 8 values per uint16
        LOCAL_4_BIT_DELTAS = 0x0002, // Signed 4-bit value, 4 values per uint16
        LOCAL_8_BIT_DELTAS = 0x0003, // Signed 8-bit value, 2 values per uint16
        VARIATION_INDEX = 0x8000, // VariationIndex table, contains a delta-set index pair.
        Reserved = 0x7FFC, // For future use — set to 0
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Device
    {
        [Description(0, "Smallest size to correct, in ppem")]
        public uint16 startSize;

        [Description(0, "Largest size to correct, in ppem")]
        public uint16 endSize;

        [Description(0, "Format of deltaValue array data: 0x0001, 0x0002, or 0x0003")]
        public uint16 deltaFormat;

        [Count(0, "deltaValueCount")]
        [Description(0, "Array of compressed data")]
        public IList<uint16> deltaValue;

        private static Int32 deltaValueCount(IAttributeService service)
        {
            var values = service.GetValues(FieldValueKind.Path, "startSize, endSize, deltaFormat");
            if (values[0] is uint16 startSize
                && values[1] is uint16 endSize
                && values[2] is uint16 deltaFormat)
            {
                if (deltaFormat < (int)DeltaFormat.VARIATION_INDEX)
                {
                    Int32 den = Marshal.SizeOf(typeof(uint16)) / (1 << deltaFormat);
                    return ((endSize - startSize + 1) + (den - 1)) / den;
                }
            }
            return 0;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class VariationIndex
    {
        [Description(0, "A delta-set outer index — used to select an item variation data subtable within the item variation store")]
        public uint16 deltaSetOuterIndex;

        [Description(0, "A delta-set inner index — used to select a delta-set row within an item variation data subtable")]
        public uint16 deltaSetInnerIndex;

        [Description(0, "Format, = 0x8000")]
        public uint16 deltaFormat;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(FeatureVariations_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    class FeatureVariations
    {
        [Description(0, "Major version of the FeatureVariations table — set to 1")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the FeatureVariations table — set to 0")]
        public uint16 minorVersion;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("FeatureVariations")]
    [BaseName("FeatureVariations")]
    class FeatureVariations_Version10
    {
        [Description(0, "Major version of the FeatureVariations table — set to 1")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the FeatureVariations table — set to 0")]
        public uint16 minorVersion;

        [Description(0, "Number of feature variation records")]
        public uint32 featureVariationRecordCount;

        [Count(0, FieldValueKind.Path, "featureVariationRecordCount")]
        [Description(0, "Array of feature variation records")]
        public IList<FeatureVariationRecord> featureVariationRecords;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class FeatureVariationRecord
    {
        [TableType(typeof(ConditionSet))]
        [Description(0, "Offset to a condition set table, from beginning of FeatureVariations table")]
        public Offset32 conditionSetOffset;

        [TableType(typeof(FeatureTableSubstitution))]
        [Description(0, "Offset to a feature table substitution table, from beginning of the FeatureVariations table")]
        public Offset32 featureTableSubstitutionOffset;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ConditionSet
    {
        [Description(0, "Number of conditions for this condition set")]
        public uint16 conditionCount;

        [Count(0, FieldValueKind.Path, "conditionCount")]
        [TableType(typeof(Condition))]
        [Description(0, "Array of offsets to condition tables, from beginning of the ConditionSet table")]
        public IList<Offset32> conditionOffsets;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "format", null)]
    [ClassTypeCondition(typeof(ConditionFormat1), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "1")]
    [Invalid]
    class Condition
    {
        [Description(0, "Format = 1")]
        public uint16 format;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [BaseName("Condition")]
    class ConditionFormat1
    {
        [Description(0, "Format = 1")]
        public uint16 format;

        [Description(0, "Index (zero-based) for the variation axis within the 'fvar' table")]
        public uint16 axisIndex;

        [Description(0, "Minimum value of the font variation instances that satisfy this condition")]
        public F2DOT14 filterRangeMinValue;

        [Description(0, "Maximum value of the font variation instances that satisfy this condition")]
        public F2DOT14 filterRangeMaxValue;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
    [ClassTypeCondition(typeof(FeatureTableSubstitution_Version10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
    [Invalid]
    class FeatureTableSubstitution
    {
        [Description(0, "Major version of the feature table substitution table — set to 1")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the feature table substitution table — set to 0")]
        public uint16 minorVersion;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [TypeName("FeatureTableSubstitution")]
    [BaseName("FeatureTableSubstitution")]
    class FeatureTableSubstitution_Version10
    {
        [Description(0, "Major version of the feature table substitution table — set to 1")]
        public uint16 majorVersion;

        [Description(0, "Minor version of the feature table substitution table — set to 0")]
        public uint16 minorVersion;

        [Description(0, "Number of feature table substitution records")]
        public uint16 substitutionCount;

        [Count(0, FieldValueKind.Path, "substitutionCount")]
        [Description(0, "Array of feature table substitution records")]
        public IList<FeatureTableSubstitutionRecord> substitutions;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [UniformType]
    class FeatureTableSubstitutionRecord
    {
        [Description(0, "The feature table index to match")]
        public uint16 featureIndex;

        [TableType(typeof(Feature))]
        [Description(0, "Offset to an alternate feature table, from start of the FeatureTableSubstitution table")]
        public Offset32 alternateFeatureOffset;
    }
#pragma warning restore IDE1006
}
