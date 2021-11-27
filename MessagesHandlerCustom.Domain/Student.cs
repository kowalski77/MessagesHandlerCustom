namespace MessagesHandlerCustom.Domain;

public class Student
{
    private readonly List<Course> courses = new();

    public Student(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public IReadOnlyList<Course> Courses => courses;

    public void Enroll(Course course)
    {
        if (courses.Contains(course)) throw new InvalidOperationException("Student already enrolled this course");

        courses.Add(course);
    }

    public void ChangeName(string name)
    {
        Name = name;
    }
}