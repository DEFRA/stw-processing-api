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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((ValidationError)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ErrorMessage, ErrorId, ErrorTradeLineItem);
    }

    private bool Equals(ValidationError other)
    {
        return ErrorMessage == other.ErrorMessage && ErrorId == other.ErrorId && ErrorTradeLineItem == other.ErrorTradeLineItem;
    }
}
