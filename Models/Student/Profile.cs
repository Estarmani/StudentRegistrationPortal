namespace StudentReg.Models.Student;
public enum Gender
{
    Male = 1,
    Female
}

public class Profile
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string OtherNames { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
}
