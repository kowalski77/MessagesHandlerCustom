namespace MessagesHandlerCustom.App;

public class Endpoints
{
    private readonly MessagesDispatcher messagesDispatcher;

    public Endpoints(MessagesDispatcher messagesDispatcher)
    {
        this.messagesDispatcher = messagesDispatcher;
    }

    public async Task EnrollStudentAsync(EnrollStudentDto enrollStudentDto)
    {
        var command = new EnrollStudentCommand(enrollStudentDto.StudentId, enrollStudentDto.CourseId);
        var result = await this.messagesDispatcher.DispatchAsync(command);

        Console.WriteLine($"Success: {result.Success} - {result.Message}");
    }
}