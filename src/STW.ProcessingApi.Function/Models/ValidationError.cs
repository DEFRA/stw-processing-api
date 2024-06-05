namespace STW.ProcessingApi.Function.Models;

public class ValidationError
{
    public ValidationError(string message, int id, int? tradeLineItem = default)
    {
        Message = message;
        Id = id;
        TradeLineItem = tradeLineItem;
    }

    public string Message { get; }

    public int? Id { get; }

    public int? TradeLineItem { get; }

    public override string ToString()
    {
        return $"{Message}";
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
        return HashCode.Combine(Message, Id, TradeLineItem);
    }

    private bool Equals(ValidationError other)
    {
        return Message == other.Message && Id == other.Id && TradeLineItem == other.TradeLineItem;
    }
}
