namespace STW.ProcessingApi.Function.Models.Bcp;

public record Bcp
{
    public long Id { get; init; }

    public string Name { get; init; }

    public string Code { get; init; }

    public bool Suspended { get; init; }
}
