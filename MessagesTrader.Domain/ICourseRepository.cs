namespace MessagesTrader.Domain;

public interface ICourseRepository
{
    Task<Course?> GetAsync(Guid id);
}