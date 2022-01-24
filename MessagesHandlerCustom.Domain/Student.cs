namespace MessagesHandlerCustom.Domain;

public class Student
{
    private readonly List<Course> courses = new();

    public Student(Guid id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public IReadOnlyList<Course> Courses => this.courses;

    public void Enroll(Course course)
    {
        if (this.courses.Contains(course))
        {
            throw new InvalidOperationException("Student already enrolled this course");
        }

        this.courses.Add(course);
    }

    public void ChangeName(string name)
    {
        this.Name = name;
    }
}