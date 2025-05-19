# C# Attribute Programming

### Read OpenTypeFont file

### 1.Example
~~~cS
[FontTable("head")]
[ClassTypeSelect(ClassValueKind.FieldPath, "majorVersion, minorVersion", null)]
[ClassTypeCondition(typeof(headTableVersion10), AttributeConditionKind.Equal, ClassValueKind.Unsigned, "0x0001, 0x0000")]
[Invalid]
[TypeName("head - Font Header Table")]
class headTable
{
    [ValueFormat(0, ValueFormatKind.Hex)]
    [Description(0, "Major version number")]
    public uint16 majorVersion;

    [ValueFormat(0, ValueFormatKind.Hex)]
    [Description(0, "Minor version number")]
    public uint16 minorVersion;
}

[TypeName("head - Font Header Table")]
class headTableVersion10
{
    [ValueFormat(0, ValueFormatKind.Hex)]
    [Description(0, "Major version number")]
    public uint16 majorVersion;

    [ValueFormat(0, ValueFormatKind.Hex)]
    [Description(0, "Minor version number")]
    public uint16 minorVersion;

    [Description(0, "Set by font manufacturer.")]
    public Fixed fontRevision;
    ...
}
~~~
#### TableAttribute(String tagName)
Loader of model searches font table class by tagName

#### ClassTypeSelectAttribute
Loader of model searches concrete class by majorVersion/minorVersion

#### ClassTypeConditionAttribute
Loader of model selects concrete class by "1,0"

#### TypeNameAttribute
ViewModel uses this  for type name

#### ValueFormatAttribute
ViewModel uses this for field value format

#### DescripptionAttribute
ViewModel uses this to display Description
~~~cs
class TableDirectory_Version10
{
    public uint32 sfntVersion;  
    public uint16 numTables;
    public uint16 searchRange;
    public uint16 entrySelector;
    public uint16 rangeShift;

    [Count(0, FieldValueKind.Path, "numTables")]
    public TableRecord[] tableRecords;
}
~~~
#### CountAttribute
Loader sets element count

### 2.Feature
 Models do not use individual table informations and ViewModels also do not use them. Models read table values from given table type using Attribute and ViewModels display their values using Attributes.

 Tables are Annotated using Attribute. I think this format improve readabilities.

see files in TTFViewer/DataTypes/Attrs, TTFViewer/Tables directories
