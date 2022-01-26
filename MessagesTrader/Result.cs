namespace MessagesTrader;

public class Result
{
    private Result(bool success, string message)
    {
        this.Success = success;
        this.Message = message;
    }

    public bool Success { get; }

    public string Message { get; }

    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }
}