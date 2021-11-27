﻿namespace MessagesHandlerCustom.Utils;

public class Result
{
    private Result(bool success, string message)
    {
        Success = success;
        Message = message;
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