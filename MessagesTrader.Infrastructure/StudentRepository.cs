using MessagesTrader.Domain;

namespace MessagesTrader.Infrastructure;

public class StudentRepository : IStudentRepository
{
    public Task<Student?> GetAsync(Guid id)
    {
        return Task.FromResult<Student?>(new Student(id, "John"));
    }
}