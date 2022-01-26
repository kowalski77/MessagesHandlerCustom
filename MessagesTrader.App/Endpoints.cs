using MessagesTrader.Domain;

namespace MessagesTrader.App;

public class Endpoints
{
    private readonly IMessagesDispatcher messagesDispatcher;

    public Endpoints(IMessagesDispatcher messagesDispatcher)
    {
        this.messagesDispatcher = messagesDispatcher ?? throw new ArgumentNullException(nameof(messagesDispatcher));
    }

    public async Task EnrollStudentAsync(EnrollStudentDto enrollStudentDto)
    {
        var command = new EnrollStudentCommand(enrollStudentDto.StudentId, enrollStudentDto.CourseId);
        var result = await this.messagesDispatcher.DispatchAsync(command);

        Console.WriteLine($"Success: {result.Success} - {result.Message}");
    }
}