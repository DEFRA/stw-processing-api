namespace STW.ProcessingApi.Function.Models;

public class ValidationError
{
    public ValidationError(string message, int id, int? affectedTradeLineItem)
    {
        Message = message;
        Id = id;
        AffectedTradeLineItem = affectedTradeLineItem;
    }

    public ValidationError(string message)
    {
        Message = message;
    }

    public string Message { get; private init; }

    public int Id { get; private init; }

    public int? AffectedTradeLineItem { get; private init; }

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

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((ValidationError)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Message, Id, AffectedTradeLineItem);
    }

    protected bool Equals(ValidationError other)
    {
        return Message == other.Message && Id == other.Id && AffectedTradeLineItem == other.AffectedTradeLineItem;
    }
}
