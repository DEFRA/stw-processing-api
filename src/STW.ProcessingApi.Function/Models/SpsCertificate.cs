namespace STW.ProcessingApi.Function.Models;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#pragma warning disable SA1649
#pragma warning disable SA1601
#pragma warning disable SA1402
#pragma warning disable SA1201

public class AddressTypeCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class GovernmentActionCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class SequenceNumeric
{
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

public class SpsCountrySubDivisionType
{
    [JsonPropertyName("activityAuthorizedSpsParty")]
    public IList<SpsPartyType> ActivityAuthorizedSpsParty { get; set; } = new List<SpsPartyType>();

    [JsonPropertyName("functionTypeCode")]
    public FunctionTypeCode? FunctionTypeCode { get; set; }

    [JsonPropertyName("hierarchicalLevelCode")]
    public CodeType HierarchicalLevelCode { get; set; }

    [JsonPropertyName("id")]
    public IdType? Id { get; set; }

    [JsonPropertyName("name")]
    public IList<TextType> Name { get; set; } = new List<TextType>();

    [JsonPropertyName("subordinateSpsCountrySubDivision")]
    public IList<SpsCountrySubDivisionType> SubordinateSpsCountrySubDivision { get; set; } = new List<SpsCountrySubDivisionType>();

    [JsonPropertyName("superordinateSpsCountrySubDivision")]
    public IList<SpsCountrySubDivisionType> SuperordinateSpsCountrySubDivision { get; set; } = new List<SpsCountrySubDivisionType>();
}

public class DocumentCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listURI")]
    public string? ListURI { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class SpsTransportEquipmentType
{
    [JsonPropertyName("affixedSpsSeal")]
    public IList<AffixedSpsSeal> AffixedSpsSeal { get; set; } = new List<AffixedSpsSeal>();

    [JsonPropertyName("id")]
    public IdType Id { get; set; }

    [JsonPropertyName("settingSpsTemperature")]
    public IList<SettingSpsTemperature> SettingSpsTemperature { get; set; } = new List<SettingSpsTemperature>();
}

public class ItemQuantity
{
    [JsonPropertyName("unitCode")]
    public string? UnitCode { get; set; }

    [JsonPropertyName("unitCodeListAgencyID")]
    public string? UnitCodeListAgencyId { get; set; }

    [JsonPropertyName("unitCodeListAgencyName")]
    public string? UnitCodeListAgencyName { get; set; }

    [JsonPropertyName("unitCodeListID")]
    public string? UnitCodeListId { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }
}

public class SettingSpsTemperature
{
    [JsonPropertyName("maximumValueMeasure")]
    public MeasureType? MaximumValueMeasure { get; set; }

    [JsonPropertyName("minimumValueMeasure")]
    public MeasureType? MinimumValueMeasure { get; set; }

    [JsonPropertyName("typeCode")]
    public TemperatureTypeCodeType? TypeCode { get; set; }

    [JsonPropertyName("valueMeasure")]
    public MeasureType? ValueMeasure { get; set; }
}

public class PhysicalSpsPackage
{
    [JsonPropertyName("itemQuantity")]
    public ItemQuantity ItemQuantity { get; set; }

    [JsonPropertyName("levelCode")]
    public CodeType LevelCode { get; set; }

    [JsonPropertyName("nominalGrossVolumeMeasure")]
    public MeasureType? NominalGrossVolumeMeasure { get; set; }

    [JsonPropertyName("nominalGrossWeightMeasure")]
    public MeasureType? NominalGrossWeightMeasure { get; set; }

    [JsonPropertyName("physicalSpsShippingMarks")]
    public IList<PhysicalSpsShippingMark> PhysicalSpsShippingMarks { get; set; } = new List<PhysicalSpsShippingMark>();

    [JsonPropertyName("typeCode")]
    public PackageTypeCodeType TypeCode { get; set; }
}

public class CargoTypeClassificationCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class NatureIdentificationSpsCargo
{
    [JsonPropertyName("typeCode")]
    public CargoTypeClassificationCodeType TypeCode { get; set; }
}

public class MainCarriageSpsTransportMovement
{
    [JsonPropertyName("id")]
    public IdType? Id { get; set; }

    [JsonPropertyName("modeCode")]
    public ModeCode ModeCode { get; set; }

    [JsonPropertyName("usedSpsTransportMeans")]
    public UsedSpsTransportMeans? UsedSpsTransportMeans { get; set; }
}

public class SpsEventType
{
    [JsonPropertyName("occurrenceSpsLocation")]
    public SpsLocationType OccurrenceSpsLocation { get; set; }
}

public class ProcessTypeCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class SpsNoteType
{
    [JsonPropertyName("content")]
    public IList<TextType> Content { get; set; } = new List<TextType>();

    [JsonPropertyName("contentCode")]
    public IList<CodeType> ContentCode { get; set; } = new List<CodeType>();

    [JsonPropertyName("subject")]
    public TextType? Subject { get; set; }

    [JsonPropertyName("subjectCode")]
    public CodeType? SubjectCode { get; set; }
}

public class SubmittedBy
{
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
}

public class ApplicableSpsProcessCharacteristic
{
    [JsonPropertyName("description")]
    public IList<TextType> Description { get; set; } = new List<TextType>();

    [JsonPropertyName("maximumValueMeasure")]
    public MeasureType? MaximumValueMeasure { get; set; }

    [JsonPropertyName("minimumValueMeasure")]
    public MeasureType? MinimumValueMeasure { get; set; }

    [JsonPropertyName("typeCode")]
    public MeasuredAttributeCodeType? TypeCode { get; set; }

    [JsonPropertyName("valueMeasure")]
    public MeasureType? ValueMeasure { get; set; }
}

public class ApplicableSpsClassification
{
    [JsonPropertyName("classCode")]
    public CodeType? ClassCode { get; set; }

    [JsonPropertyName("className")]
    public IList<TextType> ClassName { get; set; } = new List<TextType>();

    [JsonPropertyName("systemID")]
    public IdType? SystemId { get; set; }

    [JsonPropertyName("systemName")]
    public IList<TextType> SystemName { get; set; } = new List<TextType>();
}

public class IncludedSpsClause
{
    [JsonPropertyName("content")]
    public IList<TextType> Content { get; set; } = new List<TextType>();

    [JsonPropertyName("id")]
    public IdType Id { get; set; }
}

public class DateTimeType
{
    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; set; }

    [JsonPropertyName("dateTimeString")]
    public DateTimeString? DateTimeString { get; set; }
}

public class SpsReferencedDocumentType
{
    [JsonPropertyName("attachmentBinaryObject")]
    public IList<AttachmentBinaryObject> AttachmentBinaryObject { get; set; } = new List<AttachmentBinaryObject>();

    [JsonPropertyName("id")]
    public IdType Id { get; set; }

    [JsonPropertyName("information")]
    public TextType? Information { get; set; }

    [JsonPropertyName("issueDateTime")]
    public string? IssueDateTime { get; set; }

    [JsonPropertyName("relationshipTypeCode")]
    public RelationshipTypeCode RelationshipTypeCode { get; set; }

    [JsonPropertyName("typeCode")]
    public DocumentCodeType? TypeCode { get; set; }
}

public class PackageTypeCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class ModeCode
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class IdType
{
    [JsonPropertyName("schemeAgencyID")]
    public string? SchemeAgencyId { get; set; }

    [JsonPropertyName("schemeAgencyName")]
    public string? SchemeAgencyName { get; set; }

    [JsonPropertyName("schemeDataURI")]
    public string? SchemeDataURI { get; set; }

    [JsonPropertyName("schemeID")]
    public string? SchemeId { get; set; }

    [JsonPropertyName("schemeName")]
    public string? SchemeName { get; set; }

    [JsonPropertyName("schemeURI")]
    public string? SchemeURI { get; set; }

    [JsonPropertyName("schemeVersionID")]
    public string? SchemeVersionId { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class CodeType
{
    [JsonPropertyName("languageID")]
    public string? LanguageId { get; set; }

    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listAgencyName")]
    public string? ListAgencyName { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listName")]
    public string? ListName { get; set; }

    [JsonPropertyName("listSchemeURI")]
    public string? ListSchemeURI { get; set; }

    [JsonPropertyName("listURI")]
    public string? ListURI { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class IndicatorType
{
    [JsonPropertyName("indicator")]
    public bool? Indicator { get; set; }

    [JsonPropertyName("indicatorString")]
    public IndicatorString? IndicatorString { get; set; }
}

public class FunctionTypeCode
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoleCodeValue
{
    [EnumMember(Value = "AA")]
    AA,
    [EnumMember(Value = "AB")]
    AB,
    [EnumMember(Value = "AE")]
    AE,
    [EnumMember(Value = "AF")]
    AF,
    [EnumMember(Value = "AG")]
    AG,
    [EnumMember(Value = "AH")]
    AH,
    [EnumMember(Value = "AI")]
    AI,
    [EnumMember(Value = "AJ")]
    AJ,
    [EnumMember(Value = "AK")]
    AK,
    [EnumMember(Value = "AL")]
    AL,
    [EnumMember(Value = "AM")]
    AM,
    [EnumMember(Value = "AN")]
    AN,
    [EnumMember(Value = "AO")]
    AO,
    [EnumMember(Value = "AP")]
    AP,
    [EnumMember(Value = "AQ")]
    AQ,
    [EnumMember(Value = "AR")]
    AR,
    [EnumMember(Value = "AS")]
    AS,
    [EnumMember(Value = "AT")]
    AT,
    [EnumMember(Value = "AU")]
    AU,
    [EnumMember(Value = "AV")]
    AV,
    [EnumMember(Value = "AW")]
    AW,
    [EnumMember(Value = "AX")]
    AX,
    [EnumMember(Value = "AY")]
    AY,
    [EnumMember(Value = "AZ")]
    AZ,
    [EnumMember(Value = "B_1")]
    B_1,
    [EnumMember(Value = "B_2")]
    B_2,
    [EnumMember(Value = "BA")]
    BA,
    [EnumMember(Value = "BB")]
    BB,
    [EnumMember(Value = "BC")]
    BC,
    [EnumMember(Value = "BD")]
    BD,
    [EnumMember(Value = "BE")]
    BE,
    [EnumMember(Value = "BF")]
    BF,
    [EnumMember(Value = "BG")]
    BG,
    [EnumMember(Value = "BH")]
    BH,
    [EnumMember(Value = "BI")]
    BI,
    [EnumMember(Value = "BJ")]
    BJ,
    [EnumMember(Value = "BK")]
    BK,
    [EnumMember(Value = "BL")]
    BL,
    [EnumMember(Value = "BM")]
    BM,
    [EnumMember(Value = "BN")]
    BN,
    [EnumMember(Value = "BO")]
    BO,
    [EnumMember(Value = "BP")]
    BP,
    [EnumMember(Value = "BQ")]
    BQ,
    [EnumMember(Value = "BS")]
    BS,
    [EnumMember(Value = "BT")]
    BT,
    [EnumMember(Value = "BU")]
    BU,
    [EnumMember(Value = "BV")]
    BV,
    [EnumMember(Value = "BW")]
    BW,
    [EnumMember(Value = "BX")]
    BX,
    [EnumMember(Value = "BY")]
    BY,
    [EnumMember(Value = "BZ")]
    BZ,
    [EnumMember(Value = "C_1")]
    C_1,
    [EnumMember(Value = "C_2")]
    C_2,
    [EnumMember(Value = "CA")]
    CA,
    [EnumMember(Value = "CB")]
    CB,
    [EnumMember(Value = "CC")]
    CC,
    [EnumMember(Value = "CD")]
    CD,
    [EnumMember(Value = "CE")]
    CE,
    [EnumMember(Value = "CF")]
    CF,
    [EnumMember(Value = "CG")]
    CG,
    [EnumMember(Value = "CH")]
    CH,
    [EnumMember(Value = "CI")]
    CI,
    [EnumMember(Value = "CJ")]
    CJ,
    [EnumMember(Value = "CK")]
    CK,
    [EnumMember(Value = "CL")]
    CL,
    [EnumMember(Value = "CM")]
    CM,
    [EnumMember(Value = "CN")]
    CN,
    [EnumMember(Value = "CNX")]
    CNX,
    [EnumMember(Value = "CNY")]
    CNY,
    [EnumMember(Value = "CNZ")]
    CNZ,
    [EnumMember(Value = "CO")]
    CO,
    [EnumMember(Value = "COA")]
    COA,
    [EnumMember(Value = "COB")]
    COB,
    [EnumMember(Value = "COC")]
    COC,
    [EnumMember(Value = "COD")]
    COD,
    [EnumMember(Value = "COE")]
    COE,
    [EnumMember(Value = "COF")]
    COF,
    [EnumMember(Value = "COG")]
    COG,
    [EnumMember(Value = "COH")]
    COH,
    [EnumMember(Value = "COI")]
    COI,
    [EnumMember(Value = "COJ")]
    COJ,
    [EnumMember(Value = "COK")]
    COK,
    [EnumMember(Value = "COL")]
    COL,
    [EnumMember(Value = "COM")]
    COM,
    [EnumMember(Value = "CON")]
    CON,
    [EnumMember(Value = "COO")]
    COO,
    [EnumMember(Value = "COP")]
    COP,
    [EnumMember(Value = "COQ")]
    COQ,
    [EnumMember(Value = "COR")]
    COR,
    [EnumMember(Value = "COS")]
    COS,
    [EnumMember(Value = "COT")]
    COT,
    [EnumMember(Value = "COU")]
    COU,
    [EnumMember(Value = "COV")]
    COV,
    [EnumMember(Value = "COW")]
    COW,
    [EnumMember(Value = "COX")]
    COX,
    [EnumMember(Value = "COY")]
    COY,
    [EnumMember(Value = "COZ")]
    COZ,
    [EnumMember(Value = "CP")]
    CP,
    [EnumMember(Value = "CPA")]
    CPA,
    [EnumMember(Value = "CPB")]
    CPB,
    [EnumMember(Value = "CPC")]
    CPC,
    [EnumMember(Value = "CPD")]
    CPD,
    [EnumMember(Value = "CPE")]
    CPE,
    [EnumMember(Value = "CPF")]
    CPF,
    [EnumMember(Value = "CPG")]
    CPG,
    [EnumMember(Value = "CPH")]
    CPH,
    [EnumMember(Value = "CPI")]
    CPI,
    [EnumMember(Value = "CPJ")]
    CPJ,
    [EnumMember(Value = "CPK")]
    CPK,
    [EnumMember(Value = "CPL")]
    CPL,
    [EnumMember(Value = "CPM")]
    CPM,
    [EnumMember(Value = "CPN")]
    CPN,
    [EnumMember(Value = "CPO")]
    CPO,
    [EnumMember(Value = "CQ")]
    CQ,
    [EnumMember(Value = "CR")]
    CR,
    [EnumMember(Value = "CS")]
    CS,
    [EnumMember(Value = "CT")]
    CT,
    [EnumMember(Value = "CU")]
    CU,
    [EnumMember(Value = "CV")]
    CV,
    [EnumMember(Value = "CW")]
    CW,
    [EnumMember(Value = "CX")]
    CX,
    [EnumMember(Value = "CY")]
    CY,
    [EnumMember(Value = "CZ")]
    CZ,
    [EnumMember(Value = "DA")]
    DA,
    [EnumMember(Value = "DB")]
    DB,
    [EnumMember(Value = "DC")]
    DC,
    [EnumMember(Value = "DCP")]
    DCP,
    [EnumMember(Value = "DCQ")]
    DCQ,
    [EnumMember(Value = "DCR")]
    DCR,
    [EnumMember(Value = "DCS")]
    DCS,
    [EnumMember(Value = "DCT")]
    DCT,
    [EnumMember(Value = "DCU")]
    DCU,
    [EnumMember(Value = "DCV")]
    DCV,
    [EnumMember(Value = "DCW")]
    DCW,
    [EnumMember(Value = "DCX")]
    DCX,
    [EnumMember(Value = "DCY")]
    DCY,
    [EnumMember(Value = "DCZ")]
    DCZ,
    [EnumMember(Value = "DD")]
    DD,
    [EnumMember(Value = "DDA")]
    DDA,
    [EnumMember(Value = "DDB")]
    DDB,
    [EnumMember(Value = "DDC")]
    DDC,
    [EnumMember(Value = "DDD")]
    DDD,
    [EnumMember(Value = "DDE")]
    DDE,
    [EnumMember(Value = "DDF")]
    DDF,
    [EnumMember(Value = "DDG")]
    DDG,
    [EnumMember(Value = "DDH")]
    DDH,
    [EnumMember(Value = "DDI")]
    DDI,
    [EnumMember(Value = "DDJ")]
    DDJ,
    [EnumMember(Value = "DDK")]
    DDK,
    [EnumMember(Value = "DDL")]
    DDL,
    [EnumMember(Value = "DDM")]
    DDM,
    [EnumMember(Value = "DDN")]
    DDN,
    [EnumMember(Value = "DDO")]
    DDO,
    [EnumMember(Value = "DDP")]
    DDP,
    [EnumMember(Value = "DDQ")]
    DDQ,
    [EnumMember(Value = "DDR")]
    DDR,
    [EnumMember(Value = "DDS")]
    DDS,
    [EnumMember(Value = "DDT")]
    DDT,
    [EnumMember(Value = "DDU")]
    DDU,
    [EnumMember(Value = "DDV")]
    DDV,
    [EnumMember(Value = "DDW")]
    DDW,
    [EnumMember(Value = "DDX")]
    DDX,
    [EnumMember(Value = "DDY")]
    DDY,
    [EnumMember(Value = "DDZ")]
    DDZ,
    [EnumMember(Value = "DE")]
    DE,
    [EnumMember(Value = "DEA")]
    DEA,
    [EnumMember(Value = "DEB")]
    DEB,
    [EnumMember(Value = "DEC")]
    DEC,
    [EnumMember(Value = "DED")]
    DED,
    [EnumMember(Value = "DEE")]
    DEE,
    [EnumMember(Value = "DEF")]
    DEF,
    [EnumMember(Value = "DEG")]
    DEG,
    [EnumMember(Value = "DEH")]
    DEH,
    [EnumMember(Value = "DEI")]
    DEI,
    [EnumMember(Value = "DEJ")]
    DEJ,
    [EnumMember(Value = "DEK")]
    DEK,
    [EnumMember(Value = "DEL")]
    DEL,
    [EnumMember(Value = "DEM")]
    DEM,
    [EnumMember(Value = "DEN")]
    DEN,
    [EnumMember(Value = "DEO")]
    DEO,
    [EnumMember(Value = "DEP")]
    DEP,
    [EnumMember(Value = "DEQ")]
    DEQ,
    [EnumMember(Value = "DER")]
    DER,
    [EnumMember(Value = "DES")]
    DES,
    [EnumMember(Value = "DET")]
    DET,
    [EnumMember(Value = "DEU")]
    DEU,
    [EnumMember(Value = "DEV")]
    DEV,
    [EnumMember(Value = "DEW")]
    DEW,
    [EnumMember(Value = "DEX")]
    DEX,
    [EnumMember(Value = "DEY")]
    DEY,
    [EnumMember(Value = "DEZ")]
    DEZ,
    [EnumMember(Value = "DF")]
    DF,
    [EnumMember(Value = "DFA")]
    DFA,
    [EnumMember(Value = "DFB")]
    DFB,
    [EnumMember(Value = "DFC")]
    DFC,
    [EnumMember(Value = "DFD")]
    DFD,
    [EnumMember(Value = "DFE")]
    DFE,
    [EnumMember(Value = "DFF")]
    DFF,
    [EnumMember(Value = "DFG")]
    DFG,
    [EnumMember(Value = "DFH")]
    DFH,
    [EnumMember(Value = "DFI")]
    DFI,
    [EnumMember(Value = "DFJ")]
    DFJ,
    [EnumMember(Value = "DFK")]
    DFK,
    [EnumMember(Value = "DFL")]
    DFL,
    [EnumMember(Value = "DFM")]
    DFM,
    [EnumMember(Value = "DFN")]
    DFN,
    [EnumMember(Value = "DFO")]
    DFO,
    [EnumMember(Value = "DFP")]
    DFP,
    [EnumMember(Value = "DFQ")]
    DFQ,
    [EnumMember(Value = "DFR")]
    DFR,
    [EnumMember(Value = "DFS")]
    DFS,
    [EnumMember(Value = "DFT")]
    DFT,
    [EnumMember(Value = "DFU")]
    DFU,
    [EnumMember(Value = "DFV")]
    DFV,
    [EnumMember(Value = "DFW")]
    DFW,
    [EnumMember(Value = "DFX")]
    DFX,
    [EnumMember(Value = "DFY")]
    DFY,
    [EnumMember(Value = "DFZ")]
    DFZ,
    [EnumMember(Value = "DG")]
    DG,
    [EnumMember(Value = "DGA")]
    DGA,
    [EnumMember(Value = "DGB")]
    DGB,
    [EnumMember(Value = "DGC")]
    DGC,
    [EnumMember(Value = "DGD")]
    DGD,
    [EnumMember(Value = "DGE")]
    DGE,
    [EnumMember(Value = "DH")]
    DH,
    [EnumMember(Value = "DI")]
    DI,
    [EnumMember(Value = "DJ")]
    DJ,
    [EnumMember(Value = "DK")]
    DK,
    [EnumMember(Value = "DL")]
    DL,
    [EnumMember(Value = "DM")]
    DM,
    [EnumMember(Value = "DN")]
    DN,
    [EnumMember(Value = "DO")]
    DO,
    [EnumMember(Value = "DP")]
    DP,
    [EnumMember(Value = "DQ")]
    DQ,
    [EnumMember(Value = "DR")]
    DR,
    [EnumMember(Value = "DS")]
    DS,
    [EnumMember(Value = "DT")]
    DT,
    [EnumMember(Value = "DU")]
    DU,
    [EnumMember(Value = "DV")]
    DV,
    [EnumMember(Value = "DW")]
    DW,
    [EnumMember(Value = "DX")]
    DX,
    [EnumMember(Value = "DY")]
    DY,
    [EnumMember(Value = "DZ")]
    DZ,
    [EnumMember(Value = "EA")]
    EA,
    [EnumMember(Value = "EB")]
    EB,
    [EnumMember(Value = "EC")]
    EC,
    [EnumMember(Value = "ED")]
    ED,
    [EnumMember(Value = "EE")]
    EE,
    [EnumMember(Value = "EF")]
    EF,
    [EnumMember(Value = "EG")]
    EG,
    [EnumMember(Value = "EH")]
    EH,
    [EnumMember(Value = "EI")]
    EI,
    [EnumMember(Value = "EJ")]
    EJ,
    [EnumMember(Value = "EK")]
    EK,
    [EnumMember(Value = "EL")]
    EL,
    [EnumMember(Value = "EM")]
    EM,
    [EnumMember(Value = "EN")]
    EN,
    [EnumMember(Value = "EO")]
    EO,
    [EnumMember(Value = "EP")]
    EP,
    [EnumMember(Value = "EQ")]
    EQ,
    [EnumMember(Value = "ER")]
    ER,
    [EnumMember(Value = "ES")]
    ES,
    [EnumMember(Value = "ET")]
    ET,
    [EnumMember(Value = "EU")]
    EU,
    [EnumMember(Value = "EV")]
    EV,
    [EnumMember(Value = "EW")]
    EW,
    [EnumMember(Value = "EX")]
    EX,
    [EnumMember(Value = "EY")]
    EY,
    [EnumMember(Value = "EZ")]
    EZ,
    [EnumMember(Value = "FA")]
    FA,
    [EnumMember(Value = "FB")]
    FB,
    [EnumMember(Value = "FC")]
    FC,
    [EnumMember(Value = "FD")]
    FD,
    [EnumMember(Value = "FE")]
    FE,
    [EnumMember(Value = "FF")]
    FF,
    [EnumMember(Value = "FG")]
    FG,
    [EnumMember(Value = "FH")]
    FH,
    [EnumMember(Value = "FI")]
    FI,
    [EnumMember(Value = "FJ")]
    FJ,
    [EnumMember(Value = "FK")]
    FK,
    [EnumMember(Value = "FL")]
    FL,
    [EnumMember(Value = "FM")]
    FM,
    [EnumMember(Value = "FN")]
    FN,
    [EnumMember(Value = "FO")]
    FO,
    [EnumMember(Value = "FP")]
    FP,
    [EnumMember(Value = "FQ")]
    FQ,
    [EnumMember(Value = "FR")]
    FR,
    [EnumMember(Value = "FS")]
    FS,
    [EnumMember(Value = "FT")]
    FT,
    [EnumMember(Value = "FU")]
    FU,
    [EnumMember(Value = "FV")]
    FV,
    [EnumMember(Value = "FW")]
    FW,
    [EnumMember(Value = "FX")]
    FX,
    [EnumMember(Value = "FY")]
    FY,
    [EnumMember(Value = "FZ")]
    FZ,
    [EnumMember(Value = "GA")]
    GA,
    [EnumMember(Value = "GB")]
    GB,
    [EnumMember(Value = "GC")]
    GC,
    [EnumMember(Value = "GD")]
    GD,
    [EnumMember(Value = "GE")]
    GE,
    [EnumMember(Value = "GF")]
    GF,
    [EnumMember(Value = "GH")]
    GH,
    [EnumMember(Value = "GI")]
    GI,
    [EnumMember(Value = "GJ")]
    GJ,
    [EnumMember(Value = "GK")]
    GK,
    [EnumMember(Value = "GL")]
    GL,
    [EnumMember(Value = "GM")]
    GM,
    [EnumMember(Value = "GN")]
    GN,
    [EnumMember(Value = "GO")]
    GO,
    [EnumMember(Value = "GP")]
    GP,
    [EnumMember(Value = "GQ")]
    GQ,
    [EnumMember(Value = "GR")]
    GR,
    [EnumMember(Value = "GS")]
    GS,
    [EnumMember(Value = "GT")]
    GT,
    [EnumMember(Value = "GU")]
    GU,
    [EnumMember(Value = "GV")]
    GV,
    [EnumMember(Value = "GW")]
    GW,
    [EnumMember(Value = "GX")]
    GX,
    [EnumMember(Value = "GY")]
    GY,
    [EnumMember(Value = "GZ")]
    GZ,
    [EnumMember(Value = "HA")]
    HA,
    [EnumMember(Value = "HB")]
    HB,
    [EnumMember(Value = "HC")]
    HC,
    [EnumMember(Value = "HD")]
    HD,
    [EnumMember(Value = "HE")]
    HE,
    [EnumMember(Value = "HF")]
    HF,
    [EnumMember(Value = "HG")]
    HG,
    [EnumMember(Value = "HH")]
    HH,
    [EnumMember(Value = "HI")]
    HI,
    [EnumMember(Value = "HJ")]
    HJ,
    [EnumMember(Value = "HK")]
    HK,
    [EnumMember(Value = "HL")]
    HL,
    [EnumMember(Value = "HM")]
    HM,
    [EnumMember(Value = "HN")]
    HN,
    [EnumMember(Value = "HO")]
    HO,
    [EnumMember(Value = "HP")]
    HP,
    [EnumMember(Value = "HQ")]
    HQ,
    [EnumMember(Value = "HR")]
    HR,
    [EnumMember(Value = "HS")]
    HS,
    [EnumMember(Value = "HT")]
    HT,
    [EnumMember(Value = "HU")]
    HU,
    [EnumMember(Value = "HV")]
    HV,
    [EnumMember(Value = "HW")]
    HW,
    [EnumMember(Value = "HX")]
    HX,
    [EnumMember(Value = "HY")]
    HY,
    [EnumMember(Value = "HZ")]
    HZ,
    [EnumMember(Value = "I_1")]
    I_1,
    [EnumMember(Value = "I_2")]
    I_2,
    [EnumMember(Value = "IB")]
    IB,
    [EnumMember(Value = "IC")]
    IC,
    [EnumMember(Value = "ID")]
    ID,
    [EnumMember(Value = "IE")]
    IE,
    [EnumMember(Value = "IF")]
    IF,
    [EnumMember(Value = "IG")]
    IG,
    [EnumMember(Value = "IH")]
    IH,
    [EnumMember(Value = "II")]
    II,
    [EnumMember(Value = "IJ")]
    IJ,
    [EnumMember(Value = "IL")]
    IL,
    [EnumMember(Value = "IM")]
    IM,
    [EnumMember(Value = "IN")]
    IN,
    [EnumMember(Value = "IO")]
    IO,
    [EnumMember(Value = "IP")]
    IP,
    [EnumMember(Value = "IQ")]
    IQ,
    [EnumMember(Value = "IR")]
    IR,
    [EnumMember(Value = "IS")]
    IS,
    [EnumMember(Value = "IT")]
    IT,
    [EnumMember(Value = "IU")]
    IU,
    [EnumMember(Value = "IV")]
    IV,
    [EnumMember(Value = "IW")]
    IW,
    [EnumMember(Value = "IX")]
    IX,
    [EnumMember(Value = "IY")]
    IY,
    [EnumMember(Value = "IZ")]
    IZ,
    [EnumMember(Value = "JA")]
    JA,
    [EnumMember(Value = "JB")]
    JB,
    [EnumMember(Value = "JC")]
    JC,
    [EnumMember(Value = "JD")]
    JD,
    [EnumMember(Value = "JE")]
    JE,
    [EnumMember(Value = "JF")]
    JF,
    [EnumMember(Value = "JG")]
    JG,
    [EnumMember(Value = "JH")]
    JH,
    [EnumMember(Value = "LA")]
    LA,
    [EnumMember(Value = "LB")]
    LB,
    [EnumMember(Value = "LC")]
    LC,
    [EnumMember(Value = "LD")]
    LD,
    [EnumMember(Value = "LE")]
    LE,
    [EnumMember(Value = "LF")]
    LF,
    [EnumMember(Value = "LG")]
    LG,
    [EnumMember(Value = "LH")]
    LH,
    [EnumMember(Value = "LI")]
    LI,
    [EnumMember(Value = "LJ")]
    LJ,
    [EnumMember(Value = "LK")]
    LK,
    [EnumMember(Value = "LL")]
    LL,
    [EnumMember(Value = "LM")]
    LM,
    [EnumMember(Value = "LN")]
    LN,
    [EnumMember(Value = "LO")]
    LO,
    [EnumMember(Value = "LP")]
    LP,
    [EnumMember(Value = "LQ")]
    LQ,
    [EnumMember(Value = "LR")]
    LR,
    [EnumMember(Value = "LS")]
    LS,
    [EnumMember(Value = "LT")]
    LT,
    [EnumMember(Value = "LU")]
    LU,
    [EnumMember(Value = "LV")]
    LV,
    [EnumMember(Value = "MA")]
    MA,
    [EnumMember(Value = "MAD")]
    MAD,
    [EnumMember(Value = "MDR")]
    MDR,
    [EnumMember(Value = "MF")]
    MF,
    [EnumMember(Value = "MG")]
    MG,
    [EnumMember(Value = "MI")]
    MI,
    [EnumMember(Value = "MOP")]
    MOP,
    [EnumMember(Value = "MP")]
    MP,
    [EnumMember(Value = "MR")]
    MR,
    [EnumMember(Value = "MS")]
    MS,
    [EnumMember(Value = "MT")]
    MT,
    [EnumMember(Value = "N_2")]
    N_2,
    [EnumMember(Value = "NI")]
    NI,
    [EnumMember(Value = "OA")]
    OA,
    [EnumMember(Value = "OB")]
    OB,
    [EnumMember(Value = "OC")]
    OC,
    [EnumMember(Value = "OD")]
    OD,
    [EnumMember(Value = "OE")]
    OE,
    [EnumMember(Value = "OF")]
    OF,
    [EnumMember(Value = "OG")]
    OG,
    [EnumMember(Value = "OH")]
    OH,
    [EnumMember(Value = "OI")]
    OI,
    [EnumMember(Value = "OJ")]
    OJ,
    [EnumMember(Value = "OK")]
    OK,
    [EnumMember(Value = "OL")]
    OL,
    [EnumMember(Value = "OM")]
    OM,
    [EnumMember(Value = "ON")]
    ON,
    [EnumMember(Value = "OO")]
    OO,
    [EnumMember(Value = "OP")]
    OP,
    [EnumMember(Value = "OQ")]
    OQ,
    [EnumMember(Value = "OR")]
    OR,
    [EnumMember(Value = "OS")]
    OS,
    [EnumMember(Value = "OT")]
    OT,
    [EnumMember(Value = "OU")]
    OU,
    [EnumMember(Value = "OV")]
    OV,
    [EnumMember(Value = "OW")]
    OW,
    [EnumMember(Value = "OX")]
    OX,
    [EnumMember(Value = "OY")]
    OY,
    [EnumMember(Value = "OZ")]
    OZ,
    [EnumMember(Value = "P_1")]
    P_1,
    [EnumMember(Value = "P_2")]
    P_2,
    [EnumMember(Value = "P_3")]
    P_3,
    [EnumMember(Value = "P_4")]
    P_4,
    [EnumMember(Value = "PA")]
    PA,
    [EnumMember(Value = "PB")]
    PB,
    [EnumMember(Value = "PC")]
    PC,
    [EnumMember(Value = "PD")]
    PD,
    [EnumMember(Value = "PE")]
    PE,
    [EnumMember(Value = "PF")]
    PF,
    [EnumMember(Value = "PG")]
    PG,
    [EnumMember(Value = "PH")]
    PH,
    [EnumMember(Value = "PI")]
    PI,
    [EnumMember(Value = "PJ")]
    PJ,
    [EnumMember(Value = "PK")]
    PK,
    [EnumMember(Value = "PM")]
    PM,
    [EnumMember(Value = "PN")]
    PN,
    [EnumMember(Value = "PO")]
    PO,
    [EnumMember(Value = "POA")]
    POA,
    [EnumMember(Value = "PQ")]
    PQ,
    [EnumMember(Value = "PR")]
    PR,
    [EnumMember(Value = "PS")]
    PS,
    [EnumMember(Value = "PT")]
    PT,
    [EnumMember(Value = "PW")]
    PW,
    [EnumMember(Value = "PX")]
    PX,
    [EnumMember(Value = "PY")]
    PY,
    [EnumMember(Value = "PZ")]
    PZ,
    [EnumMember(Value = "RA")]
    RA,
    [EnumMember(Value = "RB")]
    RB,
    [EnumMember(Value = "RCA")]
    RCA,
    [EnumMember(Value = "RCR")]
    RCR,
    [EnumMember(Value = "RE")]
    RE,
    [EnumMember(Value = "RF")]
    RF,
    [EnumMember(Value = "RH")]
    RH,
    [EnumMember(Value = "RI")]
    RI,
    [EnumMember(Value = "RL")]
    RL,
    [EnumMember(Value = "RM")]
    RM,
    [EnumMember(Value = "RP")]
    RP,
    [EnumMember(Value = "RS")]
    RS,
    [EnumMember(Value = "RV")]
    RV,
    [EnumMember(Value = "RW")]
    RW,
    [EnumMember(Value = "SB")]
    SB,
    [EnumMember(Value = "SE")]
    SE,
    [EnumMember(Value = "SF")]
    SF,
    [EnumMember(Value = "SG")]
    SG,
    [EnumMember(Value = "SI")]
    SI,
    [EnumMember(Value = "SN")]
    SN,
    [EnumMember(Value = "SO")]
    SO,
    [EnumMember(Value = "SPC")]
    SPC,
    [EnumMember(Value = "SR")]
    SR,
    [EnumMember(Value = "SS")]
    SS,
    [EnumMember(Value = "ST")]
    ST,
    [EnumMember(Value = "SU")]
    SU,
    [EnumMember(Value = "SX")]
    SX,
    [EnumMember(Value = "SY")]
    SY,
    [EnumMember(Value = "SZ")]
    SZ,
    [EnumMember(Value = "TA")]
    TA,
    [EnumMember(Value = "TB")]
    TB,
    [EnumMember(Value = "TC")]
    TC,
    [EnumMember(Value = "TCP")]
    TCP,
    [EnumMember(Value = "TCR")]
    TCR,
    [EnumMember(Value = "TD")]
    TD,
    [EnumMember(Value = "TE")]
    TE,
    [EnumMember(Value = "TF")]
    TF,
    [EnumMember(Value = "TG")]
    TG,
    [EnumMember(Value = "TH")]
    TH,
    [EnumMember(Value = "TI")]
    TI,
    [EnumMember(Value = "TJ")]
    TJ,
    [EnumMember(Value = "TK")]
    TK,
    [EnumMember(Value = "TL")]
    TL,
    [EnumMember(Value = "TM")]
    TM,
    [EnumMember(Value = "TN")]
    TN,
    [EnumMember(Value = "TO")]
    TO,
    [EnumMember(Value = "TP")]
    TP,
    [EnumMember(Value = "TQ")]
    TQ,
    [EnumMember(Value = "TR")]
    TR,
    [EnumMember(Value = "TS")]
    TS,
    [EnumMember(Value = "TT")]
    TT,
    [EnumMember(Value = "TU")]
    TU,
    [EnumMember(Value = "TV")]
    TV,
    [EnumMember(Value = "TW")]
    TW,
    [EnumMember(Value = "TX")]
    TX,
    [EnumMember(Value = "TY")]
    TY,
    [EnumMember(Value = "TZ")]
    TZ,
    [EnumMember(Value = "UA")]
    UA,
    [EnumMember(Value = "UB")]
    UB,
    [EnumMember(Value = "UC")]
    UC,
    [EnumMember(Value = "UD")]
    UD,
    [EnumMember(Value = "UE")]
    UE,
    [EnumMember(Value = "UF")]
    UF,
    [EnumMember(Value = "UG")]
    UG,
    [EnumMember(Value = "UH")]
    UH,
    [EnumMember(Value = "UHP")]
    UHP,
    [EnumMember(Value = "UI")]
    UI,
    [EnumMember(Value = "UJ")]
    UJ,
    [EnumMember(Value = "UK")]
    UK,
    [EnumMember(Value = "UL")]
    UL,
    [EnumMember(Value = "UM")]
    UM,
    [EnumMember(Value = "UN")]
    UN,
    [EnumMember(Value = "UO")]
    UO,
    [EnumMember(Value = "UP")]
    UP,
    [EnumMember(Value = "UQ")]
    UQ,
    [EnumMember(Value = "UR")]
    UR,
    [EnumMember(Value = "US")]
    US,
    [EnumMember(Value = "UT")]
    UT,
    [EnumMember(Value = "UU")]
    UU,
    [EnumMember(Value = "UV")]
    UV,
    [EnumMember(Value = "UW")]
    UW,
    [EnumMember(Value = "UX")]
    UX,
    [EnumMember(Value = "UY")]
    UY,
    [EnumMember(Value = "UZ")]
    UZ,
    [EnumMember(Value = "VA")]
    VA,
    [EnumMember(Value = "VB")]
    VB,
    [EnumMember(Value = "VC")]
    VC,
    [EnumMember(Value = "VE")]
    VE,
    [EnumMember(Value = "VF")]
    VF,
    [EnumMember(Value = "VG")]
    VG,
    [EnumMember(Value = "VH")]
    VH,
    [EnumMember(Value = "VI")]
    VI,
    [EnumMember(Value = "VJ")]
    VJ,
    [EnumMember(Value = "VK")]
    VK,
    [EnumMember(Value = "VL")]
    VL,
    [EnumMember(Value = "VM")]
    VM,
    [EnumMember(Value = "VN")]
    VN,
    [EnumMember(Value = "VO")]
    VO,
    [EnumMember(Value = "VP")]
    VP,
    [EnumMember(Value = "VQ")]
    VQ,
    [EnumMember(Value = "VR")]
    VR,
    [EnumMember(Value = "VS")]
    VS,
    [EnumMember(Value = "VT")]
    VT,
    [EnumMember(Value = "VU")]
    VU,
    [EnumMember(Value = "VV")]
    VV,
    [EnumMember(Value = "VW")]
    VW,
    [EnumMember(Value = "VX")]
    VX,
    [EnumMember(Value = "VY")]
    VY,
    [EnumMember(Value = "VZ")]
    VZ,
    [EnumMember(Value = "WA")]
    WA,
    [EnumMember(Value = "WB")]
    WB,
    [EnumMember(Value = "WC")]
    WC,
    [EnumMember(Value = "WD")]
    WD,
    [EnumMember(Value = "WE")]
    WE,
    [EnumMember(Value = "WF")]
    WF,
    [EnumMember(Value = "WG")]
    WG,
    [EnumMember(Value = "WH")]
    WH,
    [EnumMember(Value = "WI")]
    WI,
    [EnumMember(Value = "WJ")]
    WJ,
    [EnumMember(Value = "WK")]
    WK,
    [EnumMember(Value = "WL")]
    WL,
    [EnumMember(Value = "WM")]
    WM,
    [EnumMember(Value = "WN")]
    WN,
    [EnumMember(Value = "WO")]
    WO,
    [EnumMember(Value = "WP")]
    WP,
    [EnumMember(Value = "WPA")]
    WPA,
    [EnumMember(Value = "WQ")]
    WQ,
    [EnumMember(Value = "WR")]
    WR,
    [EnumMember(Value = "WS")]
    WS,
    [EnumMember(Value = "WT")]
    WT,
    [EnumMember(Value = "WU")]
    WU,
    [EnumMember(Value = "WV")]
    WV,
    [EnumMember(Value = "WW")]
    WW,
    [EnumMember(Value = "WX")]
    WX,
    [EnumMember(Value = "WY")]
    WY,
    [EnumMember(Value = "WZ")]
    WZ,
    [EnumMember(Value = "ZZZ")]
    ZZZ,
}

public class RoleCode
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public RoleCodeValue? Value { get; set; }
}

public class StatusCode
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listURI")]
    public string? ListURI { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class SpsCertificate
{
    [JsonPropertyName("spsConsignment")]
    public SpsConsignment SpsConsignment { get; set; }

    [JsonPropertyName("spsExchangedDocument")]
    public SpsExchangedDocument SpsExchangedDocument { get; set; }
}

public class UsedSpsTransportMeans
{
    [JsonPropertyName("name")]
    public TextType Name { get; set; }
}

public class DateTimeString
{
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class PhysicalSpsShippingMark
{
    [JsonPropertyName("marking")]
    public TextType Marking { get; set; }
}

public class AffixedSpsSeal
{
    [JsonPropertyName("id")]
    public IdType Id { get; set; }

    [JsonPropertyName("issuingSpsParty")]
    public SpsPartyType? IssuingSpsParty { get; set; }

    [JsonPropertyName("maximumID")]
    public IdType? MaximumId { get; set; }
}

public class SpecifiedSpsPerson
{
    [JsonPropertyName("attainedSpsQualification")]
    public IList<AttainedSpsQualification> AttainedSpsQualification { get; set; } = new List<AttainedSpsQualification>();

    [JsonPropertyName("name")]
    public TextType Name { get; set; }
}

public class IncludedSpsConsignmentItem
{
    [JsonPropertyName("includedSpsTradeLineItem")]
    public IList<IncludedSpsTradeLineItem> IncludedSpsTradeLineItem { get; set; } = new List<IncludedSpsTradeLineItem>();

    [JsonPropertyName("natureIdentificationSpsCargo")]
    public IList<NatureIdentificationSpsCargo> NatureIdentificationSpsCargo { get; set; } = new List<NatureIdentificationSpsCargo>();
}

public class SpecifiedSpsAddress
{
    [JsonPropertyName("cityID")]
    public IdType? CityId { get; set; }

    [JsonPropertyName("cityName")]
    public TextType? CityName { get; set; }

    [JsonPropertyName("countryID")]
    public IdType? CountryId { get; set; }

    [JsonPropertyName("countryName")]
    public TextType? CountryName { get; set; }

    [JsonPropertyName("countrySubDivisionID")]
    public IdType? CountrySubDivisionId { get; set; }

    [JsonPropertyName("countrySubDivisionName")]
    public TextType? CountrySubDivisionName { get; set; }

    [JsonPropertyName("lineFive")]
    public TextType? LineFive { get; set; }

    [JsonPropertyName("lineFour")]
    public TextType? LineFour { get; set; }

    [JsonPropertyName("lineOne")]
    public TextType? LineOne { get; set; }

    [JsonPropertyName("lineThree")]
    public TextType? LineThree { get; set; }

    [JsonPropertyName("lineTwo")]
    public TextType? LineTwo { get; set; }

    [JsonPropertyName("postcodeCode")]
    public CodeType? PostcodeCode { get; set; }

    [JsonPropertyName("typeCode")]
    public AddressTypeCodeType? TypeCode { get; set; }
}

public class SpsAuthenticationType
{
    [JsonPropertyName("actualDateTime")]
    public DateTimeType ActualDateTime { get; set; }

    [JsonPropertyName("includedSpsClause")]
    public IList<IncludedSpsClause> IncludedSpsClause { get; set; } = new List<IncludedSpsClause>();

    [JsonPropertyName("issueSpsLocation")]
    public SpsLocationType? IssueSpsLocation { get; set; }

    [JsonPropertyName("locationProviderSpsParty")]
    public SpsPartyType? LocationProviderSpsParty { get; set; }

    [JsonPropertyName("providedSpsParty")]
    public SpsPartyType ProviderSpsParty { get; set; }

    [JsonPropertyName("typeCode")]
    public GovernmentActionCodeType? TypeCode { get; set; }
}

public class SpsLocationType
{
    [JsonPropertyName("id")]
    public IdType? Id { get; set; }

    [JsonPropertyName("name")]
    public IList<TextType> Name { get; set; } = new List<TextType>();
}

public class DefinedSpsContact
{
    [JsonPropertyName("personName")]
    public TextType PersonName { get; set; }
}

public class AttainedSpsQualification
{
    [JsonPropertyName("abbreviatedName")]
    public TextType? AbbreviatedName { get; set; }

    [JsonPropertyName("name")]
    public IList<TextType> Name { get; set; } = new List<TextType>();
}

public class AttachmentBinaryObject
{
    [JsonPropertyName("characterSetCode")]
    public string? CharacterSetCode { get; set; }

    [JsonPropertyName("encodingCode")]
    public string? EncodingCode { get; set; }

    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("mimeCode")]
    public string? MimeCode { get; set; }

    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    [JsonPropertyName("value")]
    public IList<string> Value { get; set; } = new List<string>();
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RelationshipTypeCodeValue
{
    [EnumMember(Value = "AAA")]
    AAA,
    [EnumMember(Value = "AAB")]
    AAB,
    [EnumMember(Value = "AAC")]
    AAC,
    [EnumMember(Value = "AAD")]
    AAD,
    [EnumMember(Value = "AAE")]
    AAE,
    [EnumMember(Value = "AAF")]
    AAF,
    [EnumMember(Value = "AAG")]
    AAG,
    [EnumMember(Value = "AAH")]
    AAH,
    [EnumMember(Value = "AAI")]
    AAI,
    [EnumMember(Value = "AAJ")]
    AAJ,
    [EnumMember(Value = "AAK")]
    AAK,
    [EnumMember(Value = "AAL")]
    AAL,
    [EnumMember(Value = "AAM")]
    AAM,
    [EnumMember(Value = "AAN")]
    AAN,
    [EnumMember(Value = "AAO")]
    AAO,
    [EnumMember(Value = "AAP")]
    AAP,
    [EnumMember(Value = "AAQ")]
    AAQ,
    [EnumMember(Value = "AAR")]
    AAR,
    [EnumMember(Value = "AAS")]
    AAS,
    [EnumMember(Value = "AAT")]
    AAT,
    [EnumMember(Value = "AAU")]
    AAU,
    [EnumMember(Value = "AAV")]
    AAV,
    [EnumMember(Value = "AAW")]
    AAW,
    [EnumMember(Value = "AAX")]
    AAX,
    [EnumMember(Value = "AAY")]
    AAY,
    [EnumMember(Value = "AAZ")]
    AAZ,
    [EnumMember(Value = "ABA")]
    ABA,
    [EnumMember(Value = "ABB")]
    ABB,
    [EnumMember(Value = "ABC")]
    ABC,
    [EnumMember(Value = "ABD")]
    ABD,
    [EnumMember(Value = "ABE")]
    ABE,
    [EnumMember(Value = "ABF")]
    ABF,
    [EnumMember(Value = "ABG")]
    ABG,
    [EnumMember(Value = "ABH")]
    ABH,
    [EnumMember(Value = "ABI")]
    ABI,
    [EnumMember(Value = "ABJ")]
    ABJ,
    [EnumMember(Value = "ABK")]
    ABK,
    [EnumMember(Value = "ABL")]
    ABL,
    [EnumMember(Value = "ABM")]
    ABM,
    [EnumMember(Value = "ABN")]
    ABN,
    [EnumMember(Value = "ABO")]
    ABO,
    [EnumMember(Value = "ABP")]
    ABP,
    [EnumMember(Value = "ABQ")]
    ABQ,
    [EnumMember(Value = "ABR")]
    ABR,
    [EnumMember(Value = "ABS")]
    ABS,
    [EnumMember(Value = "ABT")]
    ABT,
    [EnumMember(Value = "ABU")]
    ABU,
    [EnumMember(Value = "ABV")]
    ABV,
    [EnumMember(Value = "ABW")]
    ABW,
    [EnumMember(Value = "ABX")]
    ABX,
    [EnumMember(Value = "ABY")]
    ABY,
    [EnumMember(Value = "ABZ")]
    ABZ,
    [EnumMember(Value = "AC")]
    AC,
    [EnumMember(Value = "ACA")]
    ACA,
    [EnumMember(Value = "ACB")]
    ACB,
    [EnumMember(Value = "ACC")]
    ACC,
    [EnumMember(Value = "ACD")]
    ACD,
    [EnumMember(Value = "ACE")]
    ACE,
    [EnumMember(Value = "ACF")]
    ACF,
    [EnumMember(Value = "ACG")]
    ACG,
    [EnumMember(Value = "ACH")]
    ACH,
    [EnumMember(Value = "ACI")]
    ACI,
    [EnumMember(Value = "ACJ")]
    ACJ,
    [EnumMember(Value = "ACK")]
    ACK,
    [EnumMember(Value = "ACL")]
    ACL,
    [EnumMember(Value = "ACN")]
    ACN,
    [EnumMember(Value = "ACO")]
    ACO,
    [EnumMember(Value = "ACP")]
    ACP,
    [EnumMember(Value = "ACQ")]
    ACQ,
    [EnumMember(Value = "ACR")]
    ACR,
    [EnumMember(Value = "ACT")]
    ACT,
    [EnumMember(Value = "ACU")]
    ACU,
    [EnumMember(Value = "ACV")]
    ACV,
    [EnumMember(Value = "ACW")]
    ACW,
    [EnumMember(Value = "ACX")]
    ACX,
    [EnumMember(Value = "ACY")]
    ACY,
    [EnumMember(Value = "ACZ")]
    ACZ,
    [EnumMember(Value = "ADA")]
    ADA,
    [EnumMember(Value = "ADB")]
    ADB,
    [EnumMember(Value = "ADC")]
    ADC,
    [EnumMember(Value = "ADD")]
    ADD,
    [EnumMember(Value = "ADE")]
    ADE,
    [EnumMember(Value = "ADF")]
    ADF,
    [EnumMember(Value = "ADG")]
    ADG,
    [EnumMember(Value = "ADI")]
    ADI,
    [EnumMember(Value = "ADJ")]
    ADJ,
    [EnumMember(Value = "ADK")]
    ADK,
    [EnumMember(Value = "ADL")]
    ADL,
    [EnumMember(Value = "ADM")]
    ADM,
    [EnumMember(Value = "ADN")]
    ADN,
    [EnumMember(Value = "ADO")]
    ADO,
    [EnumMember(Value = "ADP")]
    ADP,
    [EnumMember(Value = "ADQ")]
    ADQ,
    [EnumMember(Value = "ADT")]
    ADT,
    [EnumMember(Value = "ADU")]
    ADU,
    [EnumMember(Value = "ADV")]
    ADV,
    [EnumMember(Value = "ADW")]
    ADW,
    [EnumMember(Value = "ADX")]
    ADX,
    [EnumMember(Value = "ADY")]
    ADY,
    [EnumMember(Value = "ADZ")]
    ADZ,
    [EnumMember(Value = "AE")]
    AE,
    [EnumMember(Value = "AEA")]
    AEA,
    [EnumMember(Value = "AEB")]
    AEB,
    [EnumMember(Value = "AEC")]
    AEC,
    [EnumMember(Value = "AED")]
    AED,
    [EnumMember(Value = "AEE")]
    AEE,
    [EnumMember(Value = "AEF")]
    AEF,
    [EnumMember(Value = "AEG")]
    AEG,
    [EnumMember(Value = "AEH")]
    AEH,
    [EnumMember(Value = "AEI")]
    AEI,
    [EnumMember(Value = "AEJ")]
    AEJ,
    [EnumMember(Value = "AEK")]
    AEK,
    [EnumMember(Value = "AEL")]
    AEL,
    [EnumMember(Value = "AEM")]
    AEM,
    [EnumMember(Value = "AEN")]
    AEN,
    [EnumMember(Value = "AEO")]
    AEO,
    [EnumMember(Value = "AEP")]
    AEP,
    [EnumMember(Value = "AEQ")]
    AEQ,
    [EnumMember(Value = "AER")]
    AER,
    [EnumMember(Value = "AES")]
    AES,
    [EnumMember(Value = "AET")]
    AET,
    [EnumMember(Value = "AEU")]
    AEU,
    [EnumMember(Value = "AEV")]
    AEV,
    [EnumMember(Value = "AEW")]
    AEW,
    [EnumMember(Value = "AEX")]
    AEX,
    [EnumMember(Value = "AEY")]
    AEY,
    [EnumMember(Value = "AEZ")]
    AEZ,
    [EnumMember(Value = "AF")]
    AF,
    [EnumMember(Value = "AFA")]
    AFA,
    [EnumMember(Value = "AFB")]
    AFB,
    [EnumMember(Value = "AFC")]
    AFC,
    [EnumMember(Value = "AFD")]
    AFD,
    [EnumMember(Value = "AFE")]
    AFE,
    [EnumMember(Value = "AFF")]
    AFF,
    [EnumMember(Value = "AFG")]
    AFG,
    [EnumMember(Value = "AFH")]
    AFH,
    [EnumMember(Value = "AFI")]
    AFI,
    [EnumMember(Value = "AFJ")]
    AFJ,
    [EnumMember(Value = "AFK")]
    AFK,
    [EnumMember(Value = "AFL")]
    AFL,
    [EnumMember(Value = "AFM")]
    AFM,
    [EnumMember(Value = "AFN")]
    AFN,
    [EnumMember(Value = "AFO")]
    AFO,
    [EnumMember(Value = "AFP")]
    AFP,
    [EnumMember(Value = "AFQ")]
    AFQ,
    [EnumMember(Value = "AFR")]
    AFR,
    [EnumMember(Value = "AFS")]
    AFS,
    [EnumMember(Value = "AFT")]
    AFT,
    [EnumMember(Value = "AFU")]
    AFU,
    [EnumMember(Value = "AFV")]
    AFV,
    [EnumMember(Value = "AFW")]
    AFW,
    [EnumMember(Value = "AFX")]
    AFX,
    [EnumMember(Value = "AFY")]
    AFY,
    [EnumMember(Value = "AFZ")]
    AFZ,
    [EnumMember(Value = "AGA")]
    AGA,
    [EnumMember(Value = "AGB")]
    AGB,
    [EnumMember(Value = "AGC")]
    AGC,
    [EnumMember(Value = "AGD")]
    AGD,
    [EnumMember(Value = "AGE")]
    AGE,
    [EnumMember(Value = "AGF")]
    AGF,
    [EnumMember(Value = "AGG")]
    AGG,
    [EnumMember(Value = "AGH")]
    AGH,
    [EnumMember(Value = "AGI")]
    AGI,
    [EnumMember(Value = "AGJ")]
    AGJ,
    [EnumMember(Value = "AGK")]
    AGK,
    [EnumMember(Value = "AGL")]
    AGL,
    [EnumMember(Value = "AGM")]
    AGM,
    [EnumMember(Value = "AGN")]
    AGN,
    [EnumMember(Value = "AGO")]
    AGO,
    [EnumMember(Value = "AGP")]
    AGP,
    [EnumMember(Value = "AGQ")]
    AGQ,
    [EnumMember(Value = "AGR")]
    AGR,
    [EnumMember(Value = "AGS")]
    AGS,
    [EnumMember(Value = "AGT")]
    AGT,
    [EnumMember(Value = "AGU")]
    AGU,
    [EnumMember(Value = "AGV")]
    AGV,
    [EnumMember(Value = "AGW")]
    AGW,
    [EnumMember(Value = "AGX")]
    AGX,
    [EnumMember(Value = "AGY")]
    AGY,
    [EnumMember(Value = "AGZ")]
    AGZ,
    [EnumMember(Value = "AHA")]
    AHA,
    [EnumMember(Value = "AHB")]
    AHB,
    [EnumMember(Value = "AHC")]
    AHC,
    [EnumMember(Value = "AHD")]
    AHD,
    [EnumMember(Value = "AHE")]
    AHE,
    [EnumMember(Value = "AHF")]
    AHF,
    [EnumMember(Value = "AHG")]
    AHG,
    [EnumMember(Value = "AHH")]
    AHH,
    [EnumMember(Value = "AHI")]
    AHI,
    [EnumMember(Value = "AHJ")]
    AHJ,
    [EnumMember(Value = "AHK")]
    AHK,
    [EnumMember(Value = "AHL")]
    AHL,
    [EnumMember(Value = "AHM")]
    AHM,
    [EnumMember(Value = "AHN")]
    AHN,
    [EnumMember(Value = "AHO")]
    AHO,
    [EnumMember(Value = "AHP")]
    AHP,
    [EnumMember(Value = "AHQ")]
    AHQ,
    [EnumMember(Value = "AHR")]
    AHR,
    [EnumMember(Value = "AHS")]
    AHS,
    [EnumMember(Value = "AHT")]
    AHT,
    [EnumMember(Value = "AHU")]
    AHU,
    [EnumMember(Value = "AHV")]
    AHV,
    [EnumMember(Value = "AHX")]
    AHX,
    [EnumMember(Value = "AHY")]
    AHY,
    [EnumMember(Value = "AHZ")]
    AHZ,
    [EnumMember(Value = "AIA")]
    AIA,
    [EnumMember(Value = "AIB")]
    AIB,
    [EnumMember(Value = "AIC")]
    AIC,
    [EnumMember(Value = "AID")]
    AID,
    [EnumMember(Value = "AIE")]
    AIE,
    [EnumMember(Value = "AIF")]
    AIF,
    [EnumMember(Value = "AIG")]
    AIG,
    [EnumMember(Value = "AIH")]
    AIH,
    [EnumMember(Value = "AII")]
    AII,
    [EnumMember(Value = "AIJ")]
    AIJ,
    [EnumMember(Value = "AIK")]
    AIK,
    [EnumMember(Value = "AIL")]
    AIL,
    [EnumMember(Value = "AIM")]
    AIM,
    [EnumMember(Value = "AIN")]
    AIN,
    [EnumMember(Value = "AIO")]
    AIO,
    [EnumMember(Value = "AIP")]
    AIP,
    [EnumMember(Value = "AIQ")]
    AIQ,
    [EnumMember(Value = "AIR")]
    AIR,
    [EnumMember(Value = "AIS")]
    AIS,
    [EnumMember(Value = "AIT")]
    AIT,
    [EnumMember(Value = "AIU")]
    AIU,
    [EnumMember(Value = "AIV")]
    AIV,
    [EnumMember(Value = "AIW")]
    AIW,
    [EnumMember(Value = "AIX")]
    AIX,
    [EnumMember(Value = "AIY")]
    AIY,
    [EnumMember(Value = "AIZ")]
    AIZ,
    [EnumMember(Value = "AJA")]
    AJA,
    [EnumMember(Value = "AJB")]
    AJB,
    [EnumMember(Value = "AJC")]
    AJC,
    [EnumMember(Value = "AJD")]
    AJD,
    [EnumMember(Value = "AJE")]
    AJE,
    [EnumMember(Value = "AJF")]
    AJF,
    [EnumMember(Value = "AJG")]
    AJG,
    [EnumMember(Value = "AJH")]
    AJH,
    [EnumMember(Value = "AJI")]
    AJI,
    [EnumMember(Value = "AJJ")]
    AJJ,
    [EnumMember(Value = "AJK")]
    AJK,
    [EnumMember(Value = "AJL")]
    AJL,
    [EnumMember(Value = "AJM")]
    AJM,
    [EnumMember(Value = "AJN")]
    AJN,
    [EnumMember(Value = "AJO")]
    AJO,
    [EnumMember(Value = "AJP")]
    AJP,
    [EnumMember(Value = "AJQ")]
    AJQ,
    [EnumMember(Value = "AJR")]
    AJR,
    [EnumMember(Value = "AJS")]
    AJS,
    [EnumMember(Value = "AJT")]
    AJT,
    [EnumMember(Value = "AJU")]
    AJU,
    [EnumMember(Value = "AJV")]
    AJV,
    [EnumMember(Value = "AJW")]
    AJW,
    [EnumMember(Value = "AJX")]
    AJX,
    [EnumMember(Value = "AJY")]
    AJY,
    [EnumMember(Value = "AJZ")]
    AJZ,
    [EnumMember(Value = "AKA")]
    AKA,
    [EnumMember(Value = "AKB")]
    AKB,
    [EnumMember(Value = "AKC")]
    AKC,
    [EnumMember(Value = "AKD")]
    AKD,
    [EnumMember(Value = "AKE")]
    AKE,
    [EnumMember(Value = "AKF")]
    AKF,
    [EnumMember(Value = "AKG")]
    AKG,
    [EnumMember(Value = "AKH")]
    AKH,
    [EnumMember(Value = "AKI")]
    AKI,
    [EnumMember(Value = "AKJ")]
    AKJ,
    [EnumMember(Value = "AKK")]
    AKK,
    [EnumMember(Value = "AKL")]
    AKL,
    [EnumMember(Value = "AKM")]
    AKM,
    [EnumMember(Value = "AKN")]
    AKN,
    [EnumMember(Value = "AKO")]
    AKO,
    [EnumMember(Value = "AKP")]
    AKP,
    [EnumMember(Value = "AKQ")]
    AKQ,
    [EnumMember(Value = "AKR")]
    AKR,
    [EnumMember(Value = "AKS")]
    AKS,
    [EnumMember(Value = "AKT")]
    AKT,
    [EnumMember(Value = "AKU")]
    AKU,
    [EnumMember(Value = "AKV")]
    AKV,
    [EnumMember(Value = "AKW")]
    AKW,
    [EnumMember(Value = "AKX")]
    AKX,
    [EnumMember(Value = "AKY")]
    AKY,
    [EnumMember(Value = "AKZ")]
    AKZ,
    [EnumMember(Value = "ALA")]
    ALA,
    [EnumMember(Value = "ALB")]
    ALB,
    [EnumMember(Value = "ALC")]
    ALC,
    [EnumMember(Value = "ALD")]
    ALD,
    [EnumMember(Value = "ALE")]
    ALE,
    [EnumMember(Value = "ALF")]
    ALF,
    [EnumMember(Value = "ALG")]
    ALG,
    [EnumMember(Value = "ALH")]
    ALH,
    [EnumMember(Value = "ALI")]
    ALI,
    [EnumMember(Value = "ALJ")]
    ALJ,
    [EnumMember(Value = "ALK")]
    ALK,
    [EnumMember(Value = "ALL")]
    ALL,
    [EnumMember(Value = "ALM")]
    ALM,
    [EnumMember(Value = "ALN")]
    ALN,
    [EnumMember(Value = "ALO")]
    ALO,
    [EnumMember(Value = "ALP")]
    ALP,
    [EnumMember(Value = "ALQ")]
    ALQ,
    [EnumMember(Value = "ALR")]
    ALR,
    [EnumMember(Value = "ALS")]
    ALS,
    [EnumMember(Value = "ALT")]
    ALT,
    [EnumMember(Value = "ALU")]
    ALU,
    [EnumMember(Value = "ALV")]
    ALV,
    [EnumMember(Value = "ALW")]
    ALW,
    [EnumMember(Value = "ALX")]
    ALX,
    [EnumMember(Value = "ALY")]
    ALY,
    [EnumMember(Value = "ALZ")]
    ALZ,
    [EnumMember(Value = "AMA")]
    AMA,
    [EnumMember(Value = "AMB")]
    AMB,
    [EnumMember(Value = "AMC")]
    AMC,
    [EnumMember(Value = "AMD")]
    AMD,
    [EnumMember(Value = "AME")]
    AME,
    [EnumMember(Value = "AMF")]
    AMF,
    [EnumMember(Value = "AMG")]
    AMG,
    [EnumMember(Value = "AMH")]
    AMH,
    [EnumMember(Value = "AMI")]
    AMI,
    [EnumMember(Value = "AMJ")]
    AMJ,
    [EnumMember(Value = "AMK")]
    AMK,
    [EnumMember(Value = "AML")]
    AML,
    [EnumMember(Value = "AMM")]
    AMM,
    [EnumMember(Value = "AMN")]
    AMN,
    [EnumMember(Value = "AMO")]
    AMO,
    [EnumMember(Value = "AMP")]
    AMP,
    [EnumMember(Value = "AMQ")]
    AMQ,
    [EnumMember(Value = "AMR")]
    AMR,
    [EnumMember(Value = "AMS")]
    AMS,
    [EnumMember(Value = "AMT")]
    AMT,
    [EnumMember(Value = "AMU")]
    AMU,
    [EnumMember(Value = "AMV")]
    AMV,
    [EnumMember(Value = "AMW")]
    AMW,
    [EnumMember(Value = "AMX")]
    AMX,
    [EnumMember(Value = "AMY")]
    AMY,
    [EnumMember(Value = "AMZ")]
    AMZ,
    [EnumMember(Value = "ANA")]
    ANA,
    [EnumMember(Value = "ANB")]
    ANB,
    [EnumMember(Value = "ANC")]
    ANC,
    [EnumMember(Value = "AND")]
    AND,
    [EnumMember(Value = "ANE")]
    ANE,
    [EnumMember(Value = "ANF")]
    ANF,
    [EnumMember(Value = "ANG")]
    ANG,
    [EnumMember(Value = "ANH")]
    ANH,
    [EnumMember(Value = "ANI")]
    ANI,
    [EnumMember(Value = "ANJ")]
    ANJ,
    [EnumMember(Value = "ANK")]
    ANK,
    [EnumMember(Value = "ANL")]
    ANL,
    [EnumMember(Value = "ANM")]
    ANM,
    [EnumMember(Value = "ANN")]
    ANN,
    [EnumMember(Value = "ANO")]
    ANO,
    [EnumMember(Value = "ANP")]
    ANP,
    [EnumMember(Value = "ANQ")]
    ANQ,
    [EnumMember(Value = "ANR")]
    ANR,
    [EnumMember(Value = "ANS")]
    ANS,
    [EnumMember(Value = "ANT")]
    ANT,
    [EnumMember(Value = "ANU")]
    ANU,
    [EnumMember(Value = "ANV")]
    ANV,
    [EnumMember(Value = "ANW")]
    ANW,
    [EnumMember(Value = "ANX")]
    ANX,
    [EnumMember(Value = "ANY")]
    ANY,
    [EnumMember(Value = "AOA")]
    AOA,
    [EnumMember(Value = "AOD")]
    AOD,
    [EnumMember(Value = "AOE")]
    AOE,
    [EnumMember(Value = "AOF")]
    AOF,
    [EnumMember(Value = "AOG")]
    AOG,
    [EnumMember(Value = "AOH")]
    AOH,
    [EnumMember(Value = "AOI")]
    AOI,
    [EnumMember(Value = "AOJ")]
    AOJ,
    [EnumMember(Value = "AOK")]
    AOK,
    [EnumMember(Value = "AOL")]
    AOL,
    [EnumMember(Value = "AOM")]
    AOM,
    [EnumMember(Value = "AON")]
    AON,
    [EnumMember(Value = "AOO")]
    AOO,
    [EnumMember(Value = "AOP")]
    AOP,
    [EnumMember(Value = "AOQ")]
    AOQ,
    [EnumMember(Value = "AOR")]
    AOR,
    [EnumMember(Value = "AOS")]
    AOS,
    [EnumMember(Value = "AOT")]
    AOT,
    [EnumMember(Value = "AOU")]
    AOU,
    [EnumMember(Value = "AOV")]
    AOV,
    [EnumMember(Value = "AOW")]
    AOW,
    [EnumMember(Value = "AOX")]
    AOX,
    [EnumMember(Value = "AOY")]
    AOY,
    [EnumMember(Value = "AOZ")]
    AOZ,
    [EnumMember(Value = "AP")]
    AP,
    [EnumMember(Value = "APA")]
    APA,
    [EnumMember(Value = "APB")]
    APB,
    [EnumMember(Value = "APC")]
    APC,
    [EnumMember(Value = "APD")]
    APD,
    [EnumMember(Value = "APE")]
    APE,
    [EnumMember(Value = "APF")]
    APF,
    [EnumMember(Value = "APG")]
    APG,
    [EnumMember(Value = "APH")]
    APH,
    [EnumMember(Value = "API")]
    API,
    [EnumMember(Value = "APJ")]
    APJ,
    [EnumMember(Value = "APK")]
    APK,
    [EnumMember(Value = "APL")]
    APL,
    [EnumMember(Value = "APM")]
    APM,
    [EnumMember(Value = "APN")]
    APN,
    [EnumMember(Value = "APO")]
    APO,
    [EnumMember(Value = "APP")]
    APP,
    [EnumMember(Value = "APQ")]
    APQ,
    [EnumMember(Value = "APR")]
    APR,
    [EnumMember(Value = "APS")]
    APS,
    [EnumMember(Value = "APT")]
    APT,
    [EnumMember(Value = "APU")]
    APU,
    [EnumMember(Value = "APV")]
    APV,
    [EnumMember(Value = "APW")]
    APW,
    [EnumMember(Value = "APX")]
    APX,
    [EnumMember(Value = "APY")]
    APY,
    [EnumMember(Value = "APZ")]
    APZ,
    [EnumMember(Value = "AQA")]
    AQA,
    [EnumMember(Value = "AQB")]
    AQB,
    [EnumMember(Value = "AQC")]
    AQC,
    [EnumMember(Value = "AQD")]
    AQD,
    [EnumMember(Value = "AQE")]
    AQE,
    [EnumMember(Value = "AQF")]
    AQF,
    [EnumMember(Value = "AQG")]
    AQG,
    [EnumMember(Value = "AQH")]
    AQH,
    [EnumMember(Value = "AQI")]
    AQI,
    [EnumMember(Value = "AQJ")]
    AQJ,
    [EnumMember(Value = "AQK")]
    AQK,
    [EnumMember(Value = "AQL")]
    AQL,
    [EnumMember(Value = "AQM")]
    AQM,
    [EnumMember(Value = "AQN")]
    AQN,
    [EnumMember(Value = "AQO")]
    AQO,
    [EnumMember(Value = "AQP")]
    AQP,
    [EnumMember(Value = "AQQ")]
    AQQ,
    [EnumMember(Value = "AQR")]
    AQR,
    [EnumMember(Value = "AQS")]
    AQS,
    [EnumMember(Value = "AQT")]
    AQT,
    [EnumMember(Value = "AQU")]
    AQU,
    [EnumMember(Value = "AQV")]
    AQV,
    [EnumMember(Value = "AQW")]
    AQW,
    [EnumMember(Value = "AQX")]
    AQX,
    [EnumMember(Value = "AQY")]
    AQY,
    [EnumMember(Value = "AQZ")]
    AQZ,
    [EnumMember(Value = "ARA")]
    ARA,
    [EnumMember(Value = "ARB")]
    ARB,
    [EnumMember(Value = "ARC")]
    ARC,
    [EnumMember(Value = "ARD")]
    ARD,
    [EnumMember(Value = "ARE")]
    ARE,
    [EnumMember(Value = "ARF")]
    ARF,
    [EnumMember(Value = "ARG")]
    ARG,
    [EnumMember(Value = "ARH")]
    ARH,
    [EnumMember(Value = "ARI")]
    ARI,
    [EnumMember(Value = "ARJ")]
    ARJ,
    [EnumMember(Value = "ARK")]
    ARK,
    [EnumMember(Value = "ARL")]
    ARL,
    [EnumMember(Value = "ARM")]
    ARM,
    [EnumMember(Value = "ARN")]
    ARN,
    [EnumMember(Value = "ARO")]
    ARO,
    [EnumMember(Value = "ARP")]
    ARP,
    [EnumMember(Value = "ARQ")]
    ARQ,
    [EnumMember(Value = "ARR")]
    ARR,
    [EnumMember(Value = "ARS")]
    ARS,
    [EnumMember(Value = "ART")]
    ART,
    [EnumMember(Value = "ARU")]
    ARU,
    [EnumMember(Value = "ARV")]
    ARV,
    [EnumMember(Value = "ARW")]
    ARW,
    [EnumMember(Value = "ARX")]
    ARX,
    [EnumMember(Value = "ARY")]
    ARY,
    [EnumMember(Value = "ARZ")]
    ARZ,
    [EnumMember(Value = "ASA")]
    ASA,
    [EnumMember(Value = "ASB")]
    ASB,
    [EnumMember(Value = "ASC")]
    ASC,
    [EnumMember(Value = "ASD")]
    ASD,
    [EnumMember(Value = "ASE")]
    ASE,
    [EnumMember(Value = "ASF")]
    ASF,
    [EnumMember(Value = "ASG")]
    ASG,
    [EnumMember(Value = "ASH")]
    ASH,
    [EnumMember(Value = "ASI")]
    ASI,
    [EnumMember(Value = "ASJ")]
    ASJ,
    [EnumMember(Value = "ASK")]
    ASK,
    [EnumMember(Value = "ASL")]
    ASL,
    [EnumMember(Value = "ASM")]
    ASM,
    [EnumMember(Value = "ASN")]
    ASN,
    [EnumMember(Value = "ASO")]
    ASO,
    [EnumMember(Value = "ASP")]
    ASP,
    [EnumMember(Value = "ASQ")]
    ASQ,
    [EnumMember(Value = "ASR")]
    ASR,
    [EnumMember(Value = "ASS")]
    ASS,
    [EnumMember(Value = "AST")]
    AST,
    [EnumMember(Value = "ASU")]
    ASU,
    [EnumMember(Value = "ASV")]
    ASV,
    [EnumMember(Value = "ASW")]
    ASW,
    [EnumMember(Value = "ASX")]
    ASX,
    [EnumMember(Value = "ASY")]
    ASY,
    [EnumMember(Value = "ASZ")]
    ASZ,
    [EnumMember(Value = "ATA")]
    ATA,
    [EnumMember(Value = "ATB")]
    ATB,
    [EnumMember(Value = "ATC")]
    ATC,
    [EnumMember(Value = "ATD")]
    ATD,
    [EnumMember(Value = "ATE")]
    ATE,
    [EnumMember(Value = "ATF")]
    ATF,
    [EnumMember(Value = "ATG")]
    ATG,
    [EnumMember(Value = "ATH")]
    ATH,
    [EnumMember(Value = "ATI")]
    ATI,
    [EnumMember(Value = "ATJ")]
    ATJ,
    [EnumMember(Value = "ATK")]
    ATK,
    [EnumMember(Value = "ATL")]
    ATL,
    [EnumMember(Value = "ATM")]
    ATM,
    [EnumMember(Value = "ATN")]
    ATN,
    [EnumMember(Value = "ATO")]
    ATO,
    [EnumMember(Value = "ATP")]
    ATP,
    [EnumMember(Value = "ATQ")]
    ATQ,
    [EnumMember(Value = "ATR")]
    ATR,
    [EnumMember(Value = "ATS")]
    ATS,
    [EnumMember(Value = "ATT")]
    ATT,
    [EnumMember(Value = "ATU")]
    ATU,
    [EnumMember(Value = "ATV")]
    ATV,
    [EnumMember(Value = "ATW")]
    ATW,
    [EnumMember(Value = "ATX")]
    ATX,
    [EnumMember(Value = "ATY")]
    ATY,
    [EnumMember(Value = "ATZ")]
    ATZ,
    [EnumMember(Value = "AU")]
    AU,
    [EnumMember(Value = "AUA")]
    AUA,
    [EnumMember(Value = "AUB")]
    AUB,
    [EnumMember(Value = "AUC")]
    AUC,
    [EnumMember(Value = "AUD")]
    AUD,
    [EnumMember(Value = "AUE")]
    AUE,
    [EnumMember(Value = "AUF")]
    AUF,
    [EnumMember(Value = "AUG")]
    AUG,
    [EnumMember(Value = "AUH")]
    AUH,
    [EnumMember(Value = "AUI")]
    AUI,
    [EnumMember(Value = "AUJ")]
    AUJ,
    [EnumMember(Value = "AUK")]
    AUK,
    [EnumMember(Value = "AUL")]
    AUL,
    [EnumMember(Value = "AUM")]
    AUM,
    [EnumMember(Value = "AUN")]
    AUN,
    [EnumMember(Value = "AUO")]
    AUO,
    [EnumMember(Value = "AUP")]
    AUP,
    [EnumMember(Value = "AUQ")]
    AUQ,
    [EnumMember(Value = "AUR")]
    AUR,
    [EnumMember(Value = "AUS")]
    AUS,
    [EnumMember(Value = "AUT")]
    AUT,
    [EnumMember(Value = "AUU")]
    AUU,
    [EnumMember(Value = "AUV")]
    AUV,
    [EnumMember(Value = "AUW")]
    AUW,
    [EnumMember(Value = "AUX")]
    AUX,
    [EnumMember(Value = "AUY")]
    AUY,
    [EnumMember(Value = "AUZ")]
    AUZ,
    [EnumMember(Value = "AV")]
    AV,
    [EnumMember(Value = "AVA")]
    AVA,
    [EnumMember(Value = "AVB")]
    AVB,
    [EnumMember(Value = "AVC")]
    AVC,
    [EnumMember(Value = "AVD")]
    AVD,
    [EnumMember(Value = "AVE")]
    AVE,
    [EnumMember(Value = "AVF")]
    AVF,
    [EnumMember(Value = "AVG")]
    AVG,
    [EnumMember(Value = "AVH")]
    AVH,
    [EnumMember(Value = "AVI")]
    AVI,
    [EnumMember(Value = "AVJ")]
    AVJ,
    [EnumMember(Value = "AVK")]
    AVK,
    [EnumMember(Value = "AVL")]
    AVL,
    [EnumMember(Value = "AVM")]
    AVM,
    [EnumMember(Value = "AVN")]
    AVN,
    [EnumMember(Value = "AVO")]
    AVO,
    [EnumMember(Value = "AVP")]
    AVP,
    [EnumMember(Value = "AVQ")]
    AVQ,
    [EnumMember(Value = "AVR")]
    AVR,
    [EnumMember(Value = "AVS")]
    AVS,
    [EnumMember(Value = "AVT")]
    AVT,
    [EnumMember(Value = "AVU")]
    AVU,
    [EnumMember(Value = "AVV")]
    AVV,
    [EnumMember(Value = "AVW")]
    AVW,
    [EnumMember(Value = "AVX")]
    AVX,
    [EnumMember(Value = "AVY")]
    AVY,
    [EnumMember(Value = "AVZ")]
    AVZ,
    [EnumMember(Value = "AWA")]
    AWA,
    [EnumMember(Value = "AWB")]
    AWB,
    [EnumMember(Value = "AWC")]
    AWC,
    [EnumMember(Value = "AWD")]
    AWD,
    [EnumMember(Value = "AWE")]
    AWE,
    [EnumMember(Value = "AWF")]
    AWF,
    [EnumMember(Value = "AWG")]
    AWG,
    [EnumMember(Value = "AWH")]
    AWH,
    [EnumMember(Value = "AWI")]
    AWI,
    [EnumMember(Value = "AWJ")]
    AWJ,
    [EnumMember(Value = "AWK")]
    AWK,
    [EnumMember(Value = "AWL")]
    AWL,
    [EnumMember(Value = "AWM")]
    AWM,
    [EnumMember(Value = "AWN")]
    AWN,
    [EnumMember(Value = "AWO")]
    AWO,
    [EnumMember(Value = "AWP")]
    AWP,
    [EnumMember(Value = "AWQ")]
    AWQ,
    [EnumMember(Value = "AWR")]
    AWR,
    [EnumMember(Value = "AWS")]
    AWS,
    [EnumMember(Value = "AWT")]
    AWT,
    [EnumMember(Value = "AWU")]
    AWU,
    [EnumMember(Value = "AWV")]
    AWV,
    [EnumMember(Value = "AWW")]
    AWW,
    [EnumMember(Value = "AWX")]
    AWX,
    [EnumMember(Value = "AWY")]
    AWY,
    [EnumMember(Value = "AWZ")]
    AWZ,
    [EnumMember(Value = "AXA")]
    AXA,
    [EnumMember(Value = "AXB")]
    AXB,
    [EnumMember(Value = "AXC")]
    AXC,
    [EnumMember(Value = "AXD")]
    AXD,
    [EnumMember(Value = "AXE")]
    AXE,
    [EnumMember(Value = "AXF")]
    AXF,
    [EnumMember(Value = "AXG")]
    AXG,
    [EnumMember(Value = "AXH")]
    AXH,
    [EnumMember(Value = "AXI")]
    AXI,
    [EnumMember(Value = "AXJ")]
    AXJ,
    [EnumMember(Value = "AXK")]
    AXK,
    [EnumMember(Value = "AXL")]
    AXL,
    [EnumMember(Value = "AXM")]
    AXM,
    [EnumMember(Value = "AXN")]
    AXN,
    [EnumMember(Value = "AXO")]
    AXO,
    [EnumMember(Value = "AXP")]
    AXP,
    [EnumMember(Value = "AXQ")]
    AXQ,
    [EnumMember(Value = "AXR")]
    AXR,
    [EnumMember(Value = "BA")]
    BA,
    [EnumMember(Value = "BC")]
    BC,
    [EnumMember(Value = "BD")]
    BD,
    [EnumMember(Value = "BE")]
    BE,
    [EnumMember(Value = "BH")]
    BH,
    [EnumMember(Value = "BM")]
    BM,
    [EnumMember(Value = "BN")]
    BN,
    [EnumMember(Value = "BO")]
    BO,
    [EnumMember(Value = "BR")]
    BR,
    [EnumMember(Value = "BT")]
    BT,
    [EnumMember(Value = "BW")]
    BW,
    [EnumMember(Value = "CAS")]
    CAS,
    [EnumMember(Value = "CAT")]
    CAT,
    [EnumMember(Value = "CAU")]
    CAU,
    [EnumMember(Value = "CAV")]
    CAV,
    [EnumMember(Value = "CAW")]
    CAW,
    [EnumMember(Value = "CAX")]
    CAX,
    [EnumMember(Value = "CAY")]
    CAY,
    [EnumMember(Value = "CAZ")]
    CAZ,
    [EnumMember(Value = "CBA")]
    CBA,
    [EnumMember(Value = "CBB")]
    CBB,
    [EnumMember(Value = "CD")]
    CD,
    [EnumMember(Value = "CEC")]
    CEC,
    [EnumMember(Value = "CED")]
    CED,
    [EnumMember(Value = "CFE")]
    CFE,
    [EnumMember(Value = "CFF")]
    CFF,
    [EnumMember(Value = "CFO")]
    CFO,
    [EnumMember(Value = "CG")]
    CG,
    [EnumMember(Value = "CH")]
    CH,
    [EnumMember(Value = "CK")]
    CK,
    [EnumMember(Value = "CKN")]
    CKN,
    [EnumMember(Value = "CM")]
    CM,
    [EnumMember(Value = "CMR")]
    CMR,
    [EnumMember(Value = "CN")]
    CN,
    [EnumMember(Value = "CNO")]
    CNO,
    [EnumMember(Value = "COF")]
    COF,
    [EnumMember(Value = "CP")]
    CP,
    [EnumMember(Value = "CR")]
    CR,
    [EnumMember(Value = "CRN")]
    CRN,
    [EnumMember(Value = "CS")]
    CS,
    [EnumMember(Value = "CST")]
    CST,
    [EnumMember(Value = "CT")]
    CT,
    [EnumMember(Value = "CU")]
    CU,
    [EnumMember(Value = "CV")]
    CV,
    [EnumMember(Value = "CW")]
    CW,
    [EnumMember(Value = "CZ")]
    CZ,
    [EnumMember(Value = "DA")]
    DA,
    [EnumMember(Value = "DAN")]
    DAN,
    [EnumMember(Value = "DB")]
    DB,
    [EnumMember(Value = "DI")]
    DI,
    [EnumMember(Value = "DL")]
    DL,
    [EnumMember(Value = "DM")]
    DM,
    [EnumMember(Value = "DQ")]
    DQ,
    [EnumMember(Value = "DR")]
    DR,
    [EnumMember(Value = "EA")]
    EA,
    [EnumMember(Value = "EB")]
    EB,
    [EnumMember(Value = "ED")]
    ED,
    [EnumMember(Value = "EE")]
    EE,
    [EnumMember(Value = "EI")]
    EI,
    [EnumMember(Value = "EN")]
    EN,
    [EnumMember(Value = "EQ")]
    EQ,
    [EnumMember(Value = "ER")]
    ER,
    [EnumMember(Value = "ERN")]
    ERN,
    [EnumMember(Value = "ET")]
    ET,
    [EnumMember(Value = "EX")]
    EX,
    [EnumMember(Value = "FC")]
    FC,
    [EnumMember(Value = "FF")]
    FF,
    [EnumMember(Value = "FI")]
    FI,
    [EnumMember(Value = "FLW")]
    FLW,
    [EnumMember(Value = "FN")]
    FN,
    [EnumMember(Value = "FO")]
    FO,
    [EnumMember(Value = "FS")]
    FS,
    [EnumMember(Value = "FT")]
    FT,
    [EnumMember(Value = "FV")]
    FV,
    [EnumMember(Value = "FX")]
    FX,
    [EnumMember(Value = "GA")]
    GA,
    [EnumMember(Value = "GC")]
    GC,
    [EnumMember(Value = "GD")]
    GD,
    [EnumMember(Value = "GDN")]
    GDN,
    [EnumMember(Value = "GN")]
    GN,
    [EnumMember(Value = "HS")]
    HS,
    [EnumMember(Value = "HWB")]
    HWB,
    [EnumMember(Value = "IA")]
    IA,
    [EnumMember(Value = "IB")]
    IB,
    [EnumMember(Value = "ICA")]
    ICA,
    [EnumMember(Value = "ICE")]
    ICE,
    [EnumMember(Value = "ICO")]
    ICO,
    [EnumMember(Value = "II")]
    II,
    [EnumMember(Value = "IL")]
    IL,
    [EnumMember(Value = "INB")]
    INB,
    [EnumMember(Value = "INN")]
    INN,
    [EnumMember(Value = "INO")]
    INO,
    [EnumMember(Value = "IP")]
    IP,
    [EnumMember(Value = "IS")]
    IS,
    [EnumMember(Value = "IT")]
    IT,
    [EnumMember(Value = "IV")]
    IV,
    [EnumMember(Value = "JB")]
    JB,
    [EnumMember(Value = "JE")]
    JE,
    [EnumMember(Value = "LA")]
    LA,
    [EnumMember(Value = "LAN")]
    LAN,
    [EnumMember(Value = "LAR")]
    LAR,
    [EnumMember(Value = "LB")]
    LB,
    [EnumMember(Value = "LC")]
    LC,
    [EnumMember(Value = "LI")]
    LI,
    [EnumMember(Value = "LO")]
    LO,
    [EnumMember(Value = "LRC")]
    LRC,
    [EnumMember(Value = "LS")]
    LS,
    [EnumMember(Value = "MA")]
    MA,
    [EnumMember(Value = "MB")]
    MB,
    [EnumMember(Value = "MF")]
    MF,
    [EnumMember(Value = "MG")]
    MG,
    [EnumMember(Value = "MH")]
    MH,
    [EnumMember(Value = "MR")]
    MR,
    [EnumMember(Value = "MRN")]
    MRN,
    [EnumMember(Value = "MS")]
    MS,
    [EnumMember(Value = "MSS")]
    MSS,
    [EnumMember(Value = "MWB")]
    MWB,
    [EnumMember(Value = "NA")]
    NA,
    [EnumMember(Value = "NF")]
    NF,
    [EnumMember(Value = "OH")]
    OH,
    [EnumMember(Value = "OI")]
    OI,
    [EnumMember(Value = "ON")]
    ON,
    [EnumMember(Value = "OP")]
    OP,
    [EnumMember(Value = "OR")]
    OR,
    [EnumMember(Value = "PB")]
    PB,
    [EnumMember(Value = "PC")]
    PC,
    [EnumMember(Value = "PD")]
    PD,
    [EnumMember(Value = "PE")]
    PE,
    [EnumMember(Value = "PF")]
    PF,
    [EnumMember(Value = "PI")]
    PI,
    [EnumMember(Value = "PK")]
    PK,
    [EnumMember(Value = "PL")]
    PL,
    [EnumMember(Value = "POR")]
    POR,
    [EnumMember(Value = "PP")]
    PP,
    [EnumMember(Value = "PQ")]
    PQ,
    [EnumMember(Value = "PR")]
    PR,
    [EnumMember(Value = "PS")]
    PS,
    [EnumMember(Value = "PW")]
    PW,
    [EnumMember(Value = "PY")]
    PY,
    [EnumMember(Value = "RA")]
    RA,
    [EnumMember(Value = "RC")]
    RC,
    [EnumMember(Value = "RCN")]
    RCN,
    [EnumMember(Value = "RE")]
    RE,
    [EnumMember(Value = "REN")]
    REN,
    [EnumMember(Value = "RF")]
    RF,
    [EnumMember(Value = "RR")]
    RR,
    [EnumMember(Value = "RT")]
    RT,
    [EnumMember(Value = "SA")]
    SA,
    [EnumMember(Value = "SB")]
    SB,
    [EnumMember(Value = "SD")]
    SD,
    [EnumMember(Value = "SE")]
    SE,
    [EnumMember(Value = "SEA")]
    SEA,
    [EnumMember(Value = "SF")]
    SF,
    [EnumMember(Value = "SH")]
    SH,
    [EnumMember(Value = "SI")]
    SI,
    [EnumMember(Value = "SM")]
    SM,
    [EnumMember(Value = "SN")]
    SN,
    [EnumMember(Value = "SP")]
    SP,
    [EnumMember(Value = "SQ")]
    SQ,
    [EnumMember(Value = "SRN")]
    SRN,
    [EnumMember(Value = "SS")]
    SS,
    [EnumMember(Value = "STA")]
    STA,
    [EnumMember(Value = "SW")]
    SW,
    [EnumMember(Value = "SZ")]
    SZ,
    [EnumMember(Value = "TB")]
    TB,
    [EnumMember(Value = "TCR")]
    TCR,
    [EnumMember(Value = "TE")]
    TE,
    [EnumMember(Value = "TF")]
    TF,
    [EnumMember(Value = "TI")]
    TI,
    [EnumMember(Value = "TIN")]
    TIN,
    [EnumMember(Value = "TL")]
    TL,
    [EnumMember(Value = "TN")]
    TN,
    [EnumMember(Value = "TP")]
    TP,
    [EnumMember(Value = "UAR")]
    UAR,
    [EnumMember(Value = "UC")]
    UC,
    [EnumMember(Value = "UCN")]
    UCN,
    [EnumMember(Value = "UN")]
    UN,
    [EnumMember(Value = "UO")]
    UO,
    [EnumMember(Value = "URI")]
    URI,
    [EnumMember(Value = "VA")]
    VA,
    [EnumMember(Value = "VC")]
    VC,
    [EnumMember(Value = "VGR")]
    VGR,
    [EnumMember(Value = "VM")]
    VM,
    [EnumMember(Value = "VN")]
    VN,
    [EnumMember(Value = "VON")]
    VON,
    [EnumMember(Value = "VOR")]
    VOR,
    [EnumMember(Value = "VP")]
    VP,
    [EnumMember(Value = "VR")]
    VR,
    [EnumMember(Value = "VS")]
    VS,
    [EnumMember(Value = "VT")]
    VT,
    [EnumMember(Value = "VV")]
    VV,
    [EnumMember(Value = "WE")]
    WE,
    [EnumMember(Value = "WM")]
    WM,
    [EnumMember(Value = "WN")]
    WN,
    [EnumMember(Value = "WR")]
    WR,
    [EnumMember(Value = "WS")]
    WS,
    [EnumMember(Value = "WY")]
    WY,
    [EnumMember(Value = "XA")]
    XA,
    [EnumMember(Value = "XC")]
    XC,
    [EnumMember(Value = "XP")]
    XP,
    [EnumMember(Value = "ZZZ")]
    ZZZ,
}

public class RelationshipTypeCode
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public RelationshipTypeCodeValue Value { get; set; }
}

public class SpsCountryType
{
    [JsonPropertyName("id")]
    public IdType Id { get; set; }

    [JsonPropertyName("name")]
    public IList<TextType> Name { get; set; } = new List<TextType>();

    [JsonPropertyName("subordinateSpsCountrySubDivision")]
    public IList<SpsCountrySubDivisionType> SubordinateSpsCountrySubDivision { get; set; } = new List<SpsCountrySubDivisionType>();
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MeasuredAttributeCodeTypeValue
{
    [EnumMember(Value = "A")]
    A,
    [EnumMember(Value = "AAA")]
    AAA,
    [EnumMember(Value = "AAB")]
    AAB,
    [EnumMember(Value = "AAC")]
    AAC,
    [EnumMember(Value = "AAD")]
    AAD,
    [EnumMember(Value = "AAF")]
    AAF,
    [EnumMember(Value = "AAG")]
    AAG,
    [EnumMember(Value = "AAH")]
    AAH,
    [EnumMember(Value = "AAI")]
    AAI,
    [EnumMember(Value = "AAJ")]
    AAJ,
    [EnumMember(Value = "AAK")]
    AAK,
    [EnumMember(Value = "AAM")]
    AAM,
    [EnumMember(Value = "AAN")]
    AAN,
    [EnumMember(Value = "AAO")]
    AAO,
    [EnumMember(Value = "AAP")]
    AAP,
    [EnumMember(Value = "AAQ")]
    AAQ,
    [EnumMember(Value = "AAR")]
    AAR,
    [EnumMember(Value = "AAS")]
    AAS,
    [EnumMember(Value = "AAT")]
    AAT,
    [EnumMember(Value = "AAU")]
    AAU,
    [EnumMember(Value = "AAV")]
    AAV,
    [EnumMember(Value = "AAW")]
    AAW,
    [EnumMember(Value = "AAX")]
    AAX,
    [EnumMember(Value = "AAY")]
    AAY,
    [EnumMember(Value = "AAZ")]
    AAZ,
    [EnumMember(Value = "ABA")]
    ABA,
    [EnumMember(Value = "ABB")]
    ABB,
    [EnumMember(Value = "ABC")]
    ABC,
    [EnumMember(Value = "ABD")]
    ABD,
    [EnumMember(Value = "ABE")]
    ABE,
    [EnumMember(Value = "ABF")]
    ABF,
    [EnumMember(Value = "ABG")]
    ABG,
    [EnumMember(Value = "ABH")]
    ABH,
    [EnumMember(Value = "ABI")]
    ABI,
    [EnumMember(Value = "ABJ")]
    ABJ,
    [EnumMember(Value = "ABK")]
    ABK,
    [EnumMember(Value = "ABL")]
    ABL,
    [EnumMember(Value = "ABM")]
    ABM,
    [EnumMember(Value = "ABN")]
    ABN,
    [EnumMember(Value = "ABO")]
    ABO,
    [EnumMember(Value = "ABP")]
    ABP,
    [EnumMember(Value = "ABS")]
    ABS,
    [EnumMember(Value = "ABT")]
    ABT,
    [EnumMember(Value = "ABX")]
    ABX,
    [EnumMember(Value = "ABY")]
    ABY,
    [EnumMember(Value = "ABZ")]
    ABZ,
    [EnumMember(Value = "ACA")]
    ACA,
    [EnumMember(Value = "ACE")]
    ACE,
    [EnumMember(Value = "ACG")]
    ACG,
    [EnumMember(Value = "ACN")]
    ACN,
    [EnumMember(Value = "ACP")]
    ACP,
    [EnumMember(Value = "ACS")]
    ACS,
    [EnumMember(Value = "ACV")]
    ACV,
    [EnumMember(Value = "ACW")]
    ACW,
    [EnumMember(Value = "ACX")]
    ACX,
    [EnumMember(Value = "ADR")]
    ADR,
    [EnumMember(Value = "ADS")]
    ADS,
    [EnumMember(Value = "ADT")]
    ADT,
    [EnumMember(Value = "ADU")]
    ADU,
    [EnumMember(Value = "ADV")]
    ADV,
    [EnumMember(Value = "ADW")]
    ADW,
    [EnumMember(Value = "ADX")]
    ADX,
    [EnumMember(Value = "ADY")]
    ADY,
    [EnumMember(Value = "ADZ")]
    ADZ,
    [EnumMember(Value = "AEA")]
    AEA,
    [EnumMember(Value = "AEB")]
    AEB,
    [EnumMember(Value = "AEC")]
    AEC,
    [EnumMember(Value = "AED")]
    AED,
    [EnumMember(Value = "AEE")]
    AEE,
    [EnumMember(Value = "AEF")]
    AEF,
    [EnumMember(Value = "AEG")]
    AEG,
    [EnumMember(Value = "AEH")]
    AEH,
    [EnumMember(Value = "AEI")]
    AEI,
    [EnumMember(Value = "AEJ")]
    AEJ,
    [EnumMember(Value = "AEK")]
    AEK,
    [EnumMember(Value = "AEL")]
    AEL,
    [EnumMember(Value = "AEM")]
    AEM,
    [EnumMember(Value = "AEN")]
    AEN,
    [EnumMember(Value = "AEO")]
    AEO,
    [EnumMember(Value = "AEP")]
    AEP,
    [EnumMember(Value = "AEQ")]
    AEQ,
    [EnumMember(Value = "AER")]
    AER,
    [EnumMember(Value = "AES")]
    AES,
    [EnumMember(Value = "AET")]
    AET,
    [EnumMember(Value = "AEU")]
    AEU,
    [EnumMember(Value = "AEV")]
    AEV,
    [EnumMember(Value = "AEW")]
    AEW,
    [EnumMember(Value = "AEX")]
    AEX,
    [EnumMember(Value = "AEY")]
    AEY,
    [EnumMember(Value = "AEZ")]
    AEZ,
    [EnumMember(Value = "AF")]
    AF,
    [EnumMember(Value = "AFA")]
    AFA,
    [EnumMember(Value = "AFB")]
    AFB,
    [EnumMember(Value = "AFC")]
    AFC,
    [EnumMember(Value = "AFD")]
    AFD,
    [EnumMember(Value = "AFE")]
    AFE,
    [EnumMember(Value = "AFF")]
    AFF,
    [EnumMember(Value = "AFG")]
    AFG,
    [EnumMember(Value = "AFH")]
    AFH,
    [EnumMember(Value = "AFI")]
    AFI,
    [EnumMember(Value = "AFJ")]
    AFJ,
    [EnumMember(Value = "AFK")]
    AFK,
    [EnumMember(Value = "AFL")]
    AFL,
    [EnumMember(Value = "AFM")]
    AFM,
    [EnumMember(Value = "AFN")]
    AFN,
    [EnumMember(Value = "AFO")]
    AFO,
    [EnumMember(Value = "AFP")]
    AFP,
    [EnumMember(Value = "AFQ")]
    AFQ,
    [EnumMember(Value = "AFR")]
    AFR,
    [EnumMember(Value = "AFS")]
    AFS,
    [EnumMember(Value = "AFT")]
    AFT,
    [EnumMember(Value = "AFU")]
    AFU,
    [EnumMember(Value = "AFV")]
    AFV,
    [EnumMember(Value = "AFW")]
    AFW,
    [EnumMember(Value = "AFX")]
    AFX,
    [EnumMember(Value = "B")]
    B,
    [EnumMember(Value = "BL")]
    BL,
    [EnumMember(Value = "BMY")]
    BMY,
    [EnumMember(Value = "BMZ")]
    BMZ,
    [EnumMember(Value = "BNA")]
    BNA,
    [EnumMember(Value = "BNB")]
    BNB,
    [EnumMember(Value = "BNC")]
    BNC,
    [EnumMember(Value = "BND")]
    BND,
    [EnumMember(Value = "BNE")]
    BNE,
    [EnumMember(Value = "BNF")]
    BNF,
    [EnumMember(Value = "BNG")]
    BNG,
    [EnumMember(Value = "BNH")]
    BNH,
    [EnumMember(Value = "BNI")]
    BNI,
    [EnumMember(Value = "BNJ")]
    BNJ,
    [EnumMember(Value = "BNK")]
    BNK,
    [EnumMember(Value = "BNL")]
    BNL,
    [EnumMember(Value = "BNM")]
    BNM,
    [EnumMember(Value = "BNN")]
    BNN,
    [EnumMember(Value = "BNO")]
    BNO,
    [EnumMember(Value = "BNP")]
    BNP,
    [EnumMember(Value = "BNQ")]
    BNQ,
    [EnumMember(Value = "BNR")]
    BNR,
    [EnumMember(Value = "BNS")]
    BNS,
    [EnumMember(Value = "BNT")]
    BNT,
    [EnumMember(Value = "BNU")]
    BNU,
    [EnumMember(Value = "BNV")]
    BNV,
    [EnumMember(Value = "BNW")]
    BNW,
    [EnumMember(Value = "BNX")]
    BNX,
    [EnumMember(Value = "BNY")]
    BNY,
    [EnumMember(Value = "BNZ")]
    BNZ,
    [EnumMember(Value = "BR")]
    BR,
    [EnumMember(Value = "BRA")]
    BRA,
    [EnumMember(Value = "BRB")]
    BRB,
    [EnumMember(Value = "BRC")]
    BRC,
    [EnumMember(Value = "BRD")]
    BRD,
    [EnumMember(Value = "BRE")]
    BRE,
    [EnumMember(Value = "BRF")]
    BRF,
    [EnumMember(Value = "BRG")]
    BRG,
    [EnumMember(Value = "BRH")]
    BRH,
    [EnumMember(Value = "BRI")]
    BRI,
    [EnumMember(Value = "BRJ")]
    BRJ,
    [EnumMember(Value = "BRK")]
    BRK,
    [EnumMember(Value = "BRL")]
    BRL,
    [EnumMember(Value = "BS")]
    BS,
    [EnumMember(Value = "BSW")]
    BSW,
    [EnumMember(Value = "BW")]
    BW,
    [EnumMember(Value = "CHN")]
    CHN,
    [EnumMember(Value = "CM")]
    CM,
    [EnumMember(Value = "CT")]
    CT,
    [EnumMember(Value = "CV")]
    CV,
    [EnumMember(Value = "CZ")]
    CZ,
    [EnumMember(Value = "D")]
    D,
    [EnumMember(Value = "DI")]
    DI,
    [EnumMember(Value = "DL")]
    DL,
    [EnumMember(Value = "DN")]
    DN,
    [EnumMember(Value = "DP")]
    DP,
    [EnumMember(Value = "DR")]
    DR,
    [EnumMember(Value = "DS")]
    DS,
    [EnumMember(Value = "DW")]
    DW,
    [EnumMember(Value = "E")]
    E,
    [EnumMember(Value = "EA")]
    EA,
    [EnumMember(Value = "F")]
    F,
    [EnumMember(Value = "FI")]
    FI,
    [EnumMember(Value = "FL")]
    FL,
    [EnumMember(Value = "FN")]
    FN,
    [EnumMember(Value = "FV")]
    FV,
    [EnumMember(Value = "GG")]
    GG,
    [EnumMember(Value = "GW")]
    GW,
    [EnumMember(Value = "HF")]
    HF,
    [EnumMember(Value = "HM")]
    HM,
    [EnumMember(Value = "HT")]
    HT,
    [EnumMember(Value = "IB")]
    IB,
    [EnumMember(Value = "ID")]
    ID,
    [EnumMember(Value = "L")]
    L,
    [EnumMember(Value = "LM")]
    LM,
    [EnumMember(Value = "LN")]
    LN,
    [EnumMember(Value = "LND")]
    LND,
    [EnumMember(Value = "M")]
    M,
    [EnumMember(Value = "MO")]
    MO,
    [EnumMember(Value = "MW")]
    MW,
    [EnumMember(Value = "N")]
    N,
    [EnumMember(Value = "OD")]
    OD,
    [EnumMember(Value = "PRS")]
    PRS,
    [EnumMember(Value = "PTN")]
    PTN,
    [EnumMember(Value = "RA")]
    RA,
    [EnumMember(Value = "RF")]
    RF,
    [EnumMember(Value = "RJ")]
    RJ,
    [EnumMember(Value = "RMW")]
    RMW,
    [EnumMember(Value = "RP")]
    RP,
    [EnumMember(Value = "RUN")]
    RUN,
    [EnumMember(Value = "RY")]
    RY,
    [EnumMember(Value = "SQ")]
    SQ,
    [EnumMember(Value = "T")]
    T,
    [EnumMember(Value = "TC")]
    TC,
    [EnumMember(Value = "TH")]
    TH,
    [EnumMember(Value = "TN")]
    TN,
    [EnumMember(Value = "TT")]
    TT,
    [EnumMember(Value = "VGM")]
    VGM,
    [EnumMember(Value = "VH")]
    VH,
    [EnumMember(Value = "VW")]
    VW,
    [EnumMember(Value = "WA")]
    WA,
    [EnumMember(Value = "WD")]
    WD,
    [EnumMember(Value = "WM")]
    WM,
    [EnumMember(Value = "WU")]
    WU,
    [EnumMember(Value = "XH")]
    XH,
    [EnumMember(Value = "XQ")]
    XQ,
    [EnumMember(Value = "XZ")]
    XZ,
    [EnumMember(Value = "YS")]
    YS,
    [EnumMember(Value = "ZAL")]
    ZAL,
    [EnumMember(Value = "ZAS")]
    ZAS,
    [EnumMember(Value = "ZB")]
    ZB,
    [EnumMember(Value = "ZBI")]
    ZBI,
    [EnumMember(Value = "ZC")]
    ZC,
    [EnumMember(Value = "ZCA")]
    ZCA,
    [EnumMember(Value = "ZCB")]
    ZCB,
    [EnumMember(Value = "ZCE")]
    ZCE,
    [EnumMember(Value = "ZCL")]
    ZCL,
    [EnumMember(Value = "ZCO")]
    ZCO,
    [EnumMember(Value = "ZCR")]
    ZCR,
    [EnumMember(Value = "ZCU")]
    ZCU,
    [EnumMember(Value = "ZFE")]
    ZFE,
    [EnumMember(Value = "ZFS")]
    ZFS,
    [EnumMember(Value = "ZGE")]
    ZGE,
    [EnumMember(Value = "ZH")]
    ZH,
    [EnumMember(Value = "ZK")]
    ZK,
    [EnumMember(Value = "ZMG")]
    ZMG,
    [EnumMember(Value = "ZMN")]
    ZMN,
    [EnumMember(Value = "ZMO")]
    ZMO,
    [EnumMember(Value = "ZN")]
    ZN,
    [EnumMember(Value = "ZNA")]
    ZNA,
    [EnumMember(Value = "ZNB")]
    ZNB,
    [EnumMember(Value = "ZNI")]
    ZNI,
    [EnumMember(Value = "ZO")]
    ZO,
    [EnumMember(Value = "ZP")]
    ZP,
    [EnumMember(Value = "ZPB")]
    ZPB,
    [EnumMember(Value = "ZS")]
    ZS,
    [EnumMember(Value = "ZSB")]
    ZSB,
    [EnumMember(Value = "ZSE")]
    ZSE,
    [EnumMember(Value = "ZSI")]
    ZSI,
    [EnumMember(Value = "ZSL")]
    ZSL,
    [EnumMember(Value = "ZSN")]
    ZSN,
    [EnumMember(Value = "ZTA")]
    ZTA,
    [EnumMember(Value = "ZTE")]
    ZTE,
    [EnumMember(Value = "ZTI")]
    ZTI,
    [EnumMember(Value = "ZV")]
    ZV,
    [EnumMember(Value = "ZW")]
    ZW,
    [EnumMember(Value = "ZWA")]
    ZWA,
    [EnumMember(Value = "ZZN")]
    ZZN,
    [EnumMember(Value = "ZZR")]
    ZZR,
    [EnumMember(Value = "ZZZ")]
    ZZZ,
}

public class MeasuredAttributeCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public MeasuredAttributeCodeTypeValue? Value { get; set; }
}

public class MeasureType
{
    [JsonPropertyName("unitCode")]
    public string? UnitCode { get; set; }

    [JsonPropertyName("unitCodeListVersionID")]
    public string? UnitCodeListVersionId { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }
}

public class TemperatureTypeCodeType
{
    [JsonPropertyName("listAgencyID")]
    public string? ListAgencyId { get; set; }

    [JsonPropertyName("listID")]
    public string? ListId { get; set; }

    [JsonPropertyName("listVersionID")]
    public string? ListVersionId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class DateTime
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class SpsConsignment
{
    [JsonPropertyName("availabilityDueDateTime")]
    public DateTimeType? AvailabilityDueDateTime { get; set; }

    [JsonPropertyName("carrierSpsParty")]
    public SpsPartyType? CarrierSpsParty { get; set; }

    [JsonPropertyName("consigneeReceiptSpsLocation")]
    public SpsLocationType? ConsigneeReceiptSpsLocation { get; set; }

    [JsonPropertyName("consigneeSpsParty")]
    public SpsPartyType ConsigneeSpsParty { get; set; }

    [JsonPropertyName("consignorSpsParty")]
    public SpsPartyType ConsignorSpsParty { get; set; }

    [JsonPropertyName("customsTransitAgentSpsParty")]
    public SpsPartyType? CustomsTransitAgentSpsParty { get; set; }

    [JsonPropertyName("deliverySpsParty")]
    public SpsPartyType? DeliverySpsParty { get; set; }

    [JsonPropertyName("despatchSpsParty")]
    public SpsPartyType? DespatchSpsParty { get; set; }

    [JsonPropertyName("examinationSpsEvent")]
    public SpsEventType ExaminationSpsEvent { get; set; }

    [JsonPropertyName("exportExitDateTime")]
    public DateTimeType? ExportExitDateTime { get; set; }

    [JsonPropertyName("exportSpsCountry")]
    public SpsCountryType ExportSpsCountry { get; set; }

    [JsonPropertyName("id")]
    public IdType? Id { get; set; }

    [JsonPropertyName("importSpsCountry")]
    public SpsCountryType ImportSpsCountry { get; set; }

    [JsonPropertyName("includedSpsConsignmentItem")]
    public IList<IncludedSpsConsignmentItem> IncludedSpsConsignmentItem { get; set; } = new List<IncludedSpsConsignmentItem>();

    [JsonPropertyName("loadingBaseportSpsLocation")]
    public SpsLocationType? LoadingBaseportSpsLocation { get; set; }

    [JsonPropertyName("mainCarriageSpsTransportMovement")]
    public IList<MainCarriageSpsTransportMovement> MainCarriageSpsTransportMovement { get; set; } = new List<MainCarriageSpsTransportMovement>();

    [JsonPropertyName("reExportSpsCountry")]
    public IList<SpsCountryType> ReExportSpsCountry { get; set; } = new List<SpsCountryType>();

    [JsonPropertyName("shipStoresIndicator")]
    public IndicatorType? ShipStoresIndicator { get; set; }

    [JsonPropertyName("storageSpsEvent")]
    public IList<SpsEventType> StorageSpsEvent { get; set; } = new List<SpsEventType>();

    [JsonPropertyName("transitSpsCountry")]
    public IList<SpsCountryType> TransitSpsCountry { get; set; } = new List<SpsCountryType>();

    [JsonPropertyName("transitSpsLocation")]
    public IList<SpsLocationType> TransitSpsLocation { get; set; } = new List<SpsLocationType>();

    [JsonPropertyName("unloadingBaseportSpsLocation")]
    public SpsLocationType? UnloadingBaseportSpsLocation { get; set; }

    [JsonPropertyName("utilizedSpsTransportEquipment")]
    public IList<SpsTransportEquipmentType> UtilizedSpsTransportEquipment { get; set; } = new List<SpsTransportEquipmentType>();
}

public class SpsPartyType
{
    [JsonPropertyName("definedSpsContact")]
    public IList<DefinedSpsContact> DefinedSpsContact { get; set; } = new List<DefinedSpsContact>();

    [JsonPropertyName("id")]
    public IdType? Id { get; set; }

    [JsonPropertyName("name")]
    public TextType Name { get; set; }

    [JsonPropertyName("roleCode")]
    public RoleCode? RoleCode { get; set; }

    [JsonPropertyName("specifiedSpsAddress")]
    public SpecifiedSpsAddress? SpecifiedSpsAddress { get; set; }

    [JsonPropertyName("specifiedSpsPerson")]
    public SpecifiedSpsPerson? SpecifiedSpsPerson { get; set; }

    [JsonPropertyName("typeCode")]
    public IList<CodeType> TypeCode { get; set; } = new List<CodeType>();
}

public class CompletionSpsPeriod
{
    [JsonPropertyName("durationMeasure")]
    public MeasureType? DurationMeasure { get; set; }

    [JsonPropertyName("endDateTime")]
    public DateTimeType? EndDateTime { get; set; }

    [JsonPropertyName("startDateTime")]
    public DateTimeType StartDateTime { get; set; }
}

public class IncludedSpsTradeLineItem
{
    [JsonPropertyName("additionalInformationSpsNote")]
    public IList<SpsNoteType> AdditionalInformationSpsNote { get; set; } = new List<SpsNoteType>();

    [JsonPropertyName("applicableSpsClassification")]
    public IList<ApplicableSpsClassification> ApplicableSpsClassification { get; set; } = new List<ApplicableSpsClassification>();

    [JsonPropertyName("appliedSpsProcess")]
    public IList<AppliedSpsProcess> AppliedSpsProcess { get; set; } = new List<AppliedSpsProcess>();

    [JsonPropertyName("assertedSpsAuthentication")]
    public IList<SpsAuthenticationType> AssertedSpsAuthentication { get; set; } = new List<SpsAuthenticationType>();

    [JsonPropertyName("associatedSpsTransportEquipment")]
    public IList<SpsTransportEquipmentType> AssociatedSpsTransportEquipment { get; set; } = new List<SpsTransportEquipmentType>();

    [JsonPropertyName("commonName")]
    public IList<TextType> CommonName { get; set; } = new List<TextType>();

    [JsonPropertyName("description")]
    public IList<TextType> Description { get; set; } = new List<TextType>();

    [JsonPropertyName("expiryDateTime")]
    public IList<DateTimeType> ExpiryDateTime { get; set; } = new List<DateTimeType>();

    [JsonPropertyName("grossVolumeMeasure")]
    public MeasureType? GrossVolumeMeasure { get; set; }

    [JsonPropertyName("grossWeightMeasure")]
    public MeasureType? GrossWeightMeasure { get; set; }

    [JsonPropertyName("intendedUse")]
    public IList<TextType> IntendedUse { get; set; } = new List<TextType>();

    [JsonPropertyName("netVolumeMeasure")]
    public MeasureType? NetVolumeMeasure { get; set; }

    [JsonPropertyName("netWeightMeasure")]
    public MeasureType? NetWeightMeasure { get; set; }

    [JsonPropertyName("originSpsCountry")]
    public IList<SpsCountryType> OriginSpsCountry { get; set; } = new List<SpsCountryType>();

    [JsonPropertyName("originSpsLocation")]
    public IList<SpsLocationType> OriginSpsLocation { get; set; } = new List<SpsLocationType>();

    [JsonPropertyName("physicalSpsPackage")]
    public IList<PhysicalSpsPackage> PhysicalSpsPackage { get; set; } = new List<PhysicalSpsPackage>();

    [JsonPropertyName("productionBatchID")]
    public IList<IdType> ProductionBatchId { get; set; } = new List<IdType>();

    [JsonPropertyName("referenceSpsReferencedDocument")]
    public IList<SpsReferencedDocumentType> ReferenceSpsReferencedDocument { get; set; } = new List<SpsReferencedDocumentType>();

    [JsonPropertyName("scientificName")]
    public IList<TextType> ScientificName { get; set; } = new List<TextType>();

    [JsonPropertyName("sequenceNumeric")]
    public SequenceNumeric SequenceNumeric { get; set; }
}

public class TextType
{
    [JsonPropertyName("languageID")]
    public string? LanguageId { get; set; }

    [JsonPropertyName("languageLocaleID")]
    public string? LanguageLocaleId { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class SpsExchangedDocument
{
    [JsonPropertyName("copyIndicator")]
    public IndicatorType? CopyIndicator { get; set; }

    [JsonPropertyName("description")]
    public IList<TextType> Description { get; set; } = new List<TextType>();

    [JsonPropertyName("id")]
    public IdType Id { get; set; }

    [JsonPropertyName("includedSpsNote")]
    public IList<SpsNoteType> IncludedSpsNote { get; set; } = new List<SpsNoteType>();

    [JsonPropertyName("issueDateTime")]
    public DateTimeType IssueDateTime { get; set; }

    [JsonPropertyName("issuerSpsParty")]
    public SpsPartyType? IssuerSpsParty { get; set; }

    [JsonPropertyName("name")]
    public IList<TextType> Name { get; set; } = new List<TextType>();

    [JsonPropertyName("recipientSpsParty")]
    public IList<SpsPartyType> RecipientSpsParty { get; set; } = new List<SpsPartyType>();

    [JsonPropertyName("referenceSpsReferencedDocument")]
    public IList<SpsReferencedDocumentType> ReferenceSpsReferencedDocument { get; set; } = new List<SpsReferencedDocumentType>();

    [JsonPropertyName("signatorySpsAuthentication")]
    public IList<SpsAuthenticationType> SignatorySpsAuthentication { get; set; } = new List<SpsAuthenticationType>();

    [JsonPropertyName("statusCode")]
    public StatusCode StatusCode { get; set; }

    [JsonPropertyName("typeCode")]
    public DocumentCodeType TypeCode { get; set; }

    [JsonPropertyName("submittedBy")]
    public SubmittedBy? SubmittedBy { get; set; }
}

public class IndicatorString
{
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class AppliedSpsProcess
{
    [JsonPropertyName("applicableSpsProcessCharacteristic")]
    public IList<ApplicableSpsProcessCharacteristic> ApplicableSpsProcessCharacteristic { get; set; } = new List<ApplicableSpsProcessCharacteristic>();

    [JsonPropertyName("completionSpsPeriod")]
    public CompletionSpsPeriod? CompletionSpsPeriod { get; set; }

    [JsonPropertyName("operationSpsCountry")]
    public SpsCountryType? OperationSpsCountry { get; set; }

    [JsonPropertyName("operatorSpsParty")]
    public SpsPartyType? OperatorSpsParty { get; set; }

    [JsonPropertyName("typeCode")]
    public ProcessTypeCodeType TypeCode { get; set; }
}
