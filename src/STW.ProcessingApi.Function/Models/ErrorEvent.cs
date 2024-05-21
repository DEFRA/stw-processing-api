namespace STW.ProcessingApi.Function.Models;

public class ErrorEvent
{
    public string ErrorMessage { get; private init; }

    public int? ErrorId { get; private init; }

    public int? ErrorTradeLineItem { get; private init; }

    public ErrorEvent(string errorMessage, int errorId, int? errorTradeLineItem)
    {
        ErrorMessage = errorMessage;
        ErrorId = errorId;
        ErrorTradeLineItem = errorTradeLineItem;
    }

    public ErrorEvent(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
