namespace STW.ProcessingApi.Function.UnitTests.TestHelpers;

using Models;

public static class SpsCertificateTestHelper
{
    public static SpsCertificate BuildSpsCertificateWithTradeLineItems(List<IncludedSpsTradeLineItem> tradeLineItems)
    {
        return new SpsCertificate
        {
            SpsConsignment = new SpsConsignment
            {
                IncludedSpsConsignmentItem = new List<IncludedSpsConsignmentItem>
                {
                    new IncludedSpsConsignmentItem
                    {
                        IncludedSpsTradeLineItem = tradeLineItems
                    }
                }
            }
        };
    }

    public static SpsCertificate BuildSpsCertificateWithIncludedSpsNotes(List<SpsNoteType> spsNoteTypes)
    {
        return new SpsCertificate
        {
            SpsExchangedDocument = new SpsExchangedDocument
            {
                IncludedSpsNote = spsNoteTypes
            }
        };
    }

    public static SequenceNumeric BuildSequenceNumericWithValue(int value)
    {
        return new SequenceNumeric
        {
            Value = value
        };
    }

    public static MeasureType BuildMeasureTypeWithValue(double value)
    {
        return new MeasureType
        {
            Value = value
        };
    }

    public static IdType BuildIdTypeWithValue(string value)
    {
        return new IdType
        {
            Value = value
        };
    }

    public static ProcessTypeCodeType BuildProcessTypeCodeTypeWithNameAndValue(string name, string value)
    {
        return new ProcessTypeCodeType
        {
            Name = name,
            Value = value
        };
    }

    public static CodeType BuildCodeTypeWithValue(string value)
    {
        return new CodeType
        {
            Value = value
        };
    }

    public static TextType BuildTextTypeWithValue(string value)
    {
        return new TextType
        {
            Value = value
        };
    }

    public static RoleCode BuildRoleCodeWithNameAndValue(string name, RoleCodeValue value)
    {
        return new RoleCode
        {
            Name = name,
            Value = value
        };
    }

    public static SpsNoteType BuildSpsNoteTypeWithSubjectCodeAndContent(string subjectCode, string content)
    {
        return new SpsNoteType
        {
            SubjectCode = new CodeType
            {
                Value = subjectCode
            },
            Content = new List<TextType>
            {
                new TextType
                {
                    Value = content
                }
            }
        };
    }

    public static SpsCountryType BuildSpsCountryTypeWithId(string id)
    {
        return new SpsCountryType
        {
            Id = new IdType
            {
                Value = id
            }
        };
    }

    public static SpsCountryType BuildSpsCountryTypeWithIdAndName(string id, string name)
    {
        return new SpsCountryType
        {
            Id = new IdType
            {
                Value = id
            },
            Name = new List<TextType>
            {
                new TextType
                {
                    Value = name
                }
            }
        };
    }

    public static SpsCountrySubDivisionType BuildSpsCountrySubDivisionTypeWithName(string name)
    {
        return new SpsCountrySubDivisionType
        {
            Name = new List<TextType>
            {
                new TextType
                {
                    Value = name
                }
            }
        };
    }

    public static SpsCountryType BuildSpsCountryTypeWithIdAndSubDivisionName(string id, string subdivisionName)
    {
        return new SpsCountryType
        {
            Id = new IdType
            {
                Value = id
            },
            SubordinateSpsCountrySubDivision = new List<SpsCountrySubDivisionType>
            {
                new SpsCountrySubDivisionType
                {
                    Name = new List<TextType>
                    {
                        new TextType
                        {
                            Value = subdivisionName
                        }
                    }
                }
            }
        };
    }

    public static SpsNoteType BuildSpsNoteTypeWithSubjectCodeAndContentAndContentCode(string subjectCode, string content, string contentCode)
    {
        return new SpsNoteType
        {
            SubjectCode = new CodeType
            {
                Value = subjectCode
            },

            Content = new List<TextType>
            {
                new TextType
                {
                    Value = content
                }
            },

            ContentCode = new List<CodeType>
            {
                new CodeType
                {
                    Value = contentCode
                }
            }
        };
    }

    public static IncludedSpsClause BuildIncludedSpsClauseWithIdAndContent(string idValue, string contentValue)
    {
        return new IncludedSpsClause
        {
            Id = new IdType
            {
                Value = idValue
            },
            Content = new List<TextType>
            {
                new TextType
                {
                    Value = contentValue
                }
            }
        };
    }

    public static SpsCountryType BuildSpsCountryTypeWithIdAndActivityAuthorizedSpsPartyId(string idValue, string authorizedSpsPartyIdValue)
    {
        return new SpsCountryType
        {
            Id = new IdType
            {
                Value = idValue
            },
            SubordinateSpsCountrySubDivision = new List<SpsCountrySubDivisionType>
            {
                new SpsCountrySubDivisionType
                {
                    ActivityAuthorizedSpsParty = new List<SpsPartyType>
                    {
                        new SpsPartyType
                        {
                            Id = new IdType
                            {
                                Value = authorizedSpsPartyIdValue
                            }
                        }
                    }
                }
            }
        };
    }
}
