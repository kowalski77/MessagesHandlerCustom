namespace MessagesHandlerCustom.Domain;

public interface IStudentRepository
{
    Task<Student?> GetAsync(Guid id);
}