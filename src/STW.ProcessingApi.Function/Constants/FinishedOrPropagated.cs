namespace STW.ProcessingApi.Function.Constants;

public class FinishedOrPropagated
{
    public const string Finished = "FINISHED";
    public const string Propagated = "PROPAGATED";

    public static readonly IReadOnlyList<string> Values =
    [
        Finished,
        Propagated
    ];
}
