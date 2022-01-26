namespace MessagesTrader.Domain;

public interface IStudentRepository
{
    Task<Student?> GetAsync(Guid id);
}