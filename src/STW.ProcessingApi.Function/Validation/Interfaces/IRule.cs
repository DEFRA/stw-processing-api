namespace STW.ProcessingApi.Function.Validation.Interfaces;

public interface IRule
{
    bool Validate(string input);
}
