using MessagesTrader.Domain;

namespace MessagesTrader.Infrastructure;

public class CourseRepository : ICourseRepository
{
    public Task<Course?> GetAsync(Guid id)
    {
        return Task.FromResult<Course?>(new Course(id, "Maths"));
    }
}