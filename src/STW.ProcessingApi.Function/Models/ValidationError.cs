namespace STW.ProcessingApi.Function.Models;

public class ValidationError
{
    public string ErrorMessage { get; private init; }

    public int? ErrorId { get; private init; }

    public int? ErrorTradeLineItem { get; private init; }

    public ValidationError(string errorMessage, int errorId, int? errorTradeLineItem = default)
    {
        ErrorMessage = errorMessage;
        ErrorId = errorId;
        ErrorTradeLineItem = errorTradeLineItem;
    }
}
