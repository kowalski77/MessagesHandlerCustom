namespace MessagesHandlerCustom.Domain;

public class Course
{
    public Course(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; protected set; }

    public string Name { get; private set; }

    public IReadOnlyList<Student> Students { get; protected set; } = new List<Student>();

    public void ChangeName(string name)
    {
        Name = name;
    }
}