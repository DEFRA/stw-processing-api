namespace STW.ProcessingApi.Function.Models;

public class ValidationError
{
    public ValidationError(string errorMessage, int errorId, int? errorTradeLineItem = default)
    {
        ErrorMessage = errorMessage;
        ErrorId = errorId;
        ErrorTradeLineItem = errorTradeLineItem;
    }

    public string ErrorMessage { get; }

    public int? ErrorId { get; }

    public int? ErrorTradeLineItem { get; }

    public override string ToString()
    {
        return $"{ErrorMessage}";
    }
}
