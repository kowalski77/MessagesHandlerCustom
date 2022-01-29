using MessagesTrader.Domain;
using MTrading;

namespace MessagesTrader.App;

public class Endpoints
{
    private readonly IMessageTrader messageTrader;

    public Endpoints(IMessageTrader messageTrader)
    {
        this.messageTrader = messageTrader ?? throw new ArgumentNullException(nameof(messageTrader));
    }

    public async Task EnrollStudentAsync(EnrollStudentDto enrollStudentDto)
    {
        var command = new EnrollStudentCommand(enrollStudentDto.StudentId, enrollStudentDto.CourseId);
        var result = await this.messageTrader.ExecuteAsync(command);

        Console.WriteLine($"Success: {result.Success} - {result.Message}");
    }

    public async Task GetStudentById(Guid id)
    {
        var query = new GetStudentNameByIdQuery(id);
        var result = await this.messageTrader.QueryAsync(query);

        Console.WriteLine($"Query result: {result}");
    }
}