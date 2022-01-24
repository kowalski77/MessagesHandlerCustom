using MessagesHandlerCustom.Utils;

namespace MessagesHandlerCustom.Domain;

public class EnrollStudentCommand : ICommand
{
    public EnrollStudentCommand(Guid studentId, Guid courseId)
    {
        this.StudentId = studentId;
        this.CourseId = courseId;
    }

    public Guid StudentId { get; }

    public Guid CourseId { get; }
}