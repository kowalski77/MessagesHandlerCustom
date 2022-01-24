using MessagesHandlerCustom.Domain;
using MessagesHandlerCustom.Utils;

namespace MessagesHandlerCustom.Application;

public class EnrollStudentCommandHandler : ICommandHandler<EnrollStudentCommand>
{
    private readonly ICourseRepository courseRepository;
    private readonly IStudentRepository studentRepository;

    public EnrollStudentCommandHandler(IStudentRepository studentRepository, ICourseRepository courseRepository)
    {
        this.studentRepository = studentRepository;
        this.courseRepository = courseRepository;
    }

    public async Task<Result> Handle(EnrollStudentCommand command)
    {
        var student = await this.studentRepository.GetAsync(command.StudentId);
        if (student is null)
        {
            return Result.Fail("Student not found");
        }

        var course = await this.courseRepository.GetAsync(command.CourseId);
        if (course is null)
        {
            return Result.Fail("Course not found");
        }

        student.Enroll(course);

        return Result.Ok();
    }
}