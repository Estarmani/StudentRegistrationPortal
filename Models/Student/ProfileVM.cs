namespace StudentReg.Models.Student;

public class ProfileVM
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string OtherNames { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string Error { get; set; }
}
