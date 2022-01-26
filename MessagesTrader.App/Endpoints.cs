using MessagesTrader.Domain;
using MTrading;

namespace MessagesTrader.App;

public class Endpoints
{
    private readonly IMessageTrader messagesDispatcher;

    public Endpoints(IMessageTrader messagesDispatcher)
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