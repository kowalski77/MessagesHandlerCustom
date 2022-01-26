using MessagesTrader.Domain;
using MTrading;

namespace MessagesTrader.Application;

public class GetStudentNameByIdQueryHandler : IQueryHandler<GetStudentNameByIdQuery, string>
{
    private readonly IStudentRepository studentRepository;

    public GetStudentNameByIdQueryHandler(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
    }

    public async Task<string> Handle(GetStudentNameByIdQuery query)
    {
        var student = await this.studentRepository.GetAsync(query.Id);

        return student is null ?
            "student not found" :
            student.Name;
    }
}