namespace STW.ProcessingApi.Function.Constants;

public static class RuleErrorMessage
{
    public const string SystemIdMissing = "Each trade line item must include \"applicableSpsClassification\" with a \"systemID\" of \"CN\"";
    public const string NetWeightLessThanMinWeight = "\"value\" for \"netWeightMeasure\" must be 0.001 or more";
    public const string NetWeightTooManyDecimals = "\"value\" for \"netWeightMeasure\" cannot have more than 3 decimals";
    public const string SequenceNumericOrder = "SequenceNumeric is out of order or contains duplicate values.";
    public const string NetWeightTooManyDigits = "\"value\" for \"netWeightMeasure\" cannot have more than 16 digits, including decimals";
    public const string ApprovedEstablishmentMissingActivityName = "Approved establishment of origin is missing an activity name";
    public const string ApprovedEstablishmentMissingActivityCode = "Approved establishment of origin is missing an activity code";
    public const string ApprovedEstablishmentMissingCountryCode = "Approved establishment of origin is missing an ISO country code";
    public const string ApprovedEstablishmentMissingCountryName = "Approved establishment of origin is missing a country name";
    public const string ApprovedEstablishmentMissingApprovalNumber = "Approved establishment of origin is missing an approval number";
    public const string ApprovedEstablishmentMissingOperatorName = "Approved establishment of origin is missing an operator name";
    public const string ApprovedEstablishmentMissingSection = "Approved establishment of origin is missing a section";
    public const string ApprovedEstablishmentIncorrectRoleCode = "Approved establishment of origin roleCode must be \"ZZZ\"";
    public const string ConformsToEuNoteNotFound = "CONFORMS_TO_EU Note Not Found";
    public const string ConformsToEuNoteInvalidValue = "CONFORMS_TO_EU must either be \"TRUE\" or \"FALSE\" – check the value is spelt correctly";
    public const string RegisteredNumberMissingCustomsWarehouseNumber = "\"NON_CONFORMING_GOODS_DESTINATION_REGISTERED_NUMBER\" must have a customs warehouse registered number";
    public const string RegisteredNumberMissingFreeZoneNumber = "\"NON_CONFORMING_GOODS_DESTINATION_REGISTERED_NUMBER\" is missing, enter a free zone registered number";
    public const string NonConformingGoodsCannotConformToEu = "You cannot send a consignment that \"CONFORMS_TO_EU\" to a \"NON_CONFORMING_GOODS_DESTINATION_TYPE\"";
    public const string NonConformingGoodsShipNameMissing = "\"NON_CONFORMING_GOODS_DESTINATION_SHIP_NAME\" is missing, enter the name of the ship";
    public const string NonConformingGoodsShipPortMissing = "\"NON_CONFORMING_GOODS_DESTINATION_SHIP_PORT\" is missing, enter the name of the port";
    public const string ConformsToEuMustBeTrueForReEntry = "\"CONFORMS_TO_EU\" must be \"TRUE\" for re-entry to be purpose of consignment";
    public const string GoodsCertifiedAsValueIsInvalid = "\"GOODS_CERTIFIED_AS\" in “includedSpsClause” is not recognised, please check the value provided";
    public const string ConformsToEuRequiredForInternalMarket = "You cannot set purpose to \"Internal Market\" for a consignment that does not \"CONFORMS_TO_EU\"";
    public const string NonConformingConsignmentMissingDestinationType = "\"NON_CONFORMING_GOODS_DESTINATION_TYPE\" is missing, enter a purpose for this non-conforming consignment";
    public const string MissingImportSpsCountry = "\"importSpsCountry\" is missing, enter ISO country code and name for the country where this consignment will be transhipped";
    public const string MissingFinalBip = "\"activityAuthorizedSpsParty\" is missing, enter TRACES code ID and name for a border control post";
    public const string TransitingCountriesMax = "You cannot have more than {0} countries of transit within \"transitSpsCountry\"";
    public const string DuplicateTransitingCountries = "You have listed the same transit country twice within \"transitSpsCountry\"";
    public const string ThirdCountryMissing = "\"importSpsCountry\" is missing, enter ISO country code for the country of transit";
    public const string MissingExitBcp = "\"activityAuthorizedSpsParty\" is missing, enter the TRACES code ID for a border control post";
    public const string PurposeMustBeInternalMarket = "Purpose must be “INTERNAL_MARKET” - check it is spelt correctly";
    public const string ScientificNameOrEppoCodeMissing = "Trade line item {0} is missing a scientific name or EPPO code - you must provide one for each trade line item";
    public const string InvalidQuantityType = "Quantity type \"contentCode\" not recognised - check it is spelt correctly.";
    public const string MissingQuantityType = "Quantity type is missing \"content\" and \"contentCode\".";
    public const string QuantityMustBeOneOrMore = "Quantity must be 1 or more.";
    public const string ClassMissing = "You must enter class if you have variety.";
    public const string VarietyMissing = "You must enter variety if you have class.";
    public const string VarietyEmpty = "Enter value for variety.";
    public const string ClassEmpty = "Enter value for class.";
    public const string CountryOfOriginMissing = "Mandatory field Country of origin is missing";
    public const string MoreThanOneRegionOfOriginInTradeLineItem = "More than one Region of origin found in the trade line {0}";
    public const string InvalidRegionOfOrigin = "Invalid Region of origin for the consignment: {0}";
    public const string MoreThanOneCountryOfOrigin = "More than one Region of origin found across trade lines: {0}";
    public const string MoreThanOneRegionOfOriginInConsignment = "More than one Region of origin found across trade lines: {0}";
    public const string ChedTypeMissing = "\"CHED_TYPE\" is missing, must be one of \"CHEDA\", \"CHEDP\", \"CHEDD\", or \"CHEDPP\"";
    public const string ChedTypeInvalid = "\"CHED_TYPE\" must be one of \"CHEDA\", \"CHEDP\", \"CHEDD\", or \"CHEDPP\"";
    public const string InvalidBcpCode = "Invalid BCP with code {0} for CHED type {1}";
    public const string BcpSuspended = "BCP with code {0} for CHED type {1} is suspended";
    public const string BcpServiceError = "Unable to check BCP validity: {0}";
}
