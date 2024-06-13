namespace STW.ProcessingApi.Function.Constants;

public static class RuleErrorId
{
    public const int SystemIdMissing = 1;
    public const int ScientificNameOrEppoCodeMissing = 4;
    public const int ClassMissing = 7;
    public const int VarietyMissing = 8;
    public const int VarietyEmpty = 10;
    public const int ClassEmpty = 11;
    public const int QuantityMustBeOneOrMore = 22;
    public const int InvalidQuantityType = 26;
    public const int MissingQuantityType = 27;
    public const int InvalidEppoCode = 32;
    public const int InvalidScientificName = 33;
    public const int MissingFinishedOrPropagated = 38;
    public const int SequenceNumericOrder = 41;
    public const int FinishedOrPropagatedNotRequired = 52;
    public const int NetWeightTooManyDigits = 54;
    public const int GoodsCertifiedAsValueIsInvalid = 56;
    public const int ConformsToEuRequiredForInternalMarket = 57;
    public const int ConformsToEuNoteNotFound = 58;
    public const int ConformsToEuNoteInvalidValue = 59;
    public const int ApprovedEstablishmentMissingActivityName = 60;
    public const int ApprovedEstablishmentMissingActivityCode = 61;
    public const int ApprovedEstablishmentMissingCountryCode = 62;
    public const int ApprovedEstablishmentMissingCountryName = 63;
    public const int ApprovedEstablishmentMissingApprovalNumber = 64;
    public const int ApprovedEstablishmentMissingOperatorName = 65;
    public const int ApprovedEstablishmentMissingSection = 66;
    public const int ApprovedEstablishmentIncorrectRoleCode = 67;
    public const int RegisteredNumberMissingFreeZoneNumber = 68;
    public const int NonConformingGoodsCannotConformToEu = 69;
    public const int ConformsToEuMustBeTrueForReEntry = 70;
    public const int NonConformingGoodsShipNameMissing = 71;
    public const int NonConformingGoodsShipPortMissing = 72;
    public const int RegisteredNumberMissingCustomsWarehouseNumber = 73;
    public const int NonConformingConsignmentMissingDestinationType = 74;
    public const int MissingImportSpsCountry = 75;
    public const int MissingFinalBcp = 76;
    public const int TransitingCountriesMax = 77;
    public const int DuplicateTransitingCountries = 78;
    public const int ThirdCountryMissing = 79;
    public const int MissingExitBcp = 80;
    public const int PurposeMustBeInternalMarket = 81;
    public const int ChedTypeMissing = 82;
    public const int ChedTypeInvalid = 83;
    public const int CountryOfOriginMissing = 84;
    public const int MoreThanOneRegionOfOriginInTradeLineItem = 85;
    public const int InvalidRegionOfOrigin = 86;
    public const int MoreThanOneCountryOfOrigin = 87;
    public const int MoreThanOneRegionOfOriginInConsignment = 88;
    public const int ApprovedEstablishmentNotFound = 89;
    public const int ApprovedEstablishmentClientError = 90;
    public const int InvalidBcpCode = 91;
    public const int BcpSuspended = 92;
    public const int BcpServiceError = 93;
    public const int SpeciesClassNameNotRecognised = 94;
    public const int SpeciesFamilyNameNotRecognised = 95;
    public const int SpeciesNameNotRecognised = 96;
    public const int CommodityCodeClientError = 97;
    public const int CommodityCategoryDataNotFound = 98;
    public const int SpeciesTypeNameNotRecognised = 99;
    public const int CountriesClientError = 100;
    public const int ThirdCountryNotFound = 101;
    public const int TransitingCountryNotFound = 102;
    public const int ExitBcpNotFound = 103;
    public const int InvalidInternalMarketPurposeError = 94;
    public const int FieldConfigServiceError = 95;
}
