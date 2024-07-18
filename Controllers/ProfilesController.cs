using Microsoft.AspNetCore.Mvc;
using StudentReg.Models.Student;
using StudentReg.Validations;

namespace StudentReg.Controllers;

public class ProfilesController : Controller
{
    private string _errorMessage = "";

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult _ProfileList()
    {
        if (StudentStore.Profiles == null)
        {
            StudentStore.Profiles = new List<Profile>();
            var dob = DateTime.Now.AddYears(-16).Date;
            StudentStore.Profiles.Add(new Profile
            {
                DateOfBirth = new DateOnly(dob.Year, dob.Month, dob.Day),
                Email = "someone@yahoo.com",
                FirstName = "Smart-James",
                Gender = Gender.Male,
                Id = 1,
                OtherNames = "Somebody",
                Surname = "Akeem"
            });

            StudentStore.Profiles.Add(new Profile
            {
                DateOfBirth = new DateOnly(dob.AddYears(-5).Year, dob.AddYears(-5).Month, dob.AddYears(-5).Day),
                Email = "abigael@yahoo.com",
                FirstName = "Smart-James",
                Gender = Gender.Female,
                Id = 2,
                OtherNames = "Abigael",
                Surname = "Israel"
            });

            StudentStore.Profiles.Add(new Profile
            {
                DateOfBirth = new DateOnly(dob.AddYears(-2).Year, dob.AddYears(-2).Month, dob.AddYears(-2).Day),
                Email = "okon@yahoo.com",
                FirstName = "Smart-James",
                Gender = Gender.Male,
                Id = 3,
                OtherNames = "Okon",
                Surname = "Okon"
            });
        }
        var list = StudentStore.Profiles ?? [];
        return View(list);
    }
    public IActionResult _AddProfile()
    {
        return View(new Profile());
    }

    public IActionResult _EditProfile(int id)
    {
        var model = new ProfileVM();
        if (id < 1)
        {
            model.Error = "Invalid Selection";
            return View(model);
        }

        var list = StudentStore.Profiles ?? new List<Profile>();
        if (list.Count < 1)
        {
            model.Error = "Invalid / Empty";
            return View(model);
        }

        var item = list.FirstOrDefault(m => m.Id == id);
        if (item == null || item.Id < 1)
        {
            model.Error = "Selected Item does not exist in the list";
            return View(model);
        }

        return View(MapToVM(item));
    }

    [HttpPost]
    public JsonResult AddProfile(Profile profile)
    {
        //Do Checks
        if (!IsProfileValid(profile))
        {
            return Json(new { IsSuccessful = false, Error = _errorMessage });
        }
        if (StudentStore.Profiles == null)
        {
            StudentStore.Profiles = [];
        }
        profile.Id = StudentStore.Profiles.Count + 1;
        StudentStore.Profiles.Add(profile);

        return Json(new { IsSuccessful = true, Error = "" });
    }

    [HttpPost]
    public JsonResult EditProfile(ProfileVM profileV)
    {
        //Do Checks
        if (profileV == null || profileV.Id < 1)
        {
            return Json(new { IsSuccessful = false, Error = "Invalid Profile Item" });
        }
        var profile = MapFromVM(profileV);
        if (!IsProfileValid(profile))
        {
            return Json(new { IsSuccessful = false, Error = _errorMessage });
        }

        var list = StudentStore.Profiles ?? new List<Profile>();
        if (list.Count < 1)
        {
            return Json(new { IsSuccessful = false, Error = "Invalid / Empty Profile List" });
        }

        for (int i = 0; i < StudentStore.Profiles!.Count; i++)
        {
            if (StudentStore.Profiles[i].Id == profile.Id)
            {
                StudentStore.Profiles[i] = profile;
                break;
            }
        }

        return Json(new { IsSuccessful = true, Error = "" });
    }

    [HttpGet]
    public JsonResult RemoveProfile(int id)
    {

        if (StudentStore.Profiles == null || StudentStore.Profiles.Count < 1)
        {
            return Json(new { IsSuccessful = false, Error = "Invalid / Empty Profile List" });
        }

        var count = StudentStore.Profiles.Count;
        StudentStore.Profiles = StudentStore.Profiles.FindAll(m => m.Id != id);

        if (count <= StudentStore.Profiles.Count)
        {
            return Json(new { IsSuccessful = false, Error = "Process Failed! Please try again" });
        }

        return Json(new { IsSuccessful = true, Error = "" });
    }


    private bool IsProfileValid(Profile profile)
    {
        _errorMessage = "";
        if (profile == null)
        {
            _errorMessage = "Invalid Profile Information";
            return false;
        }

        if (string.IsNullOrEmpty(profile.Surname) || profile.Surname.Length < 2)
        {
            _errorMessage = P_Validations.SURNAME_REQUIRED;
            return false;
        }

        if (string.IsNullOrEmpty(profile.FirstName) || profile.FirstName.Length < 2)
        {
            _errorMessage = P_Validations.FIRST_NAME_REQUIRED;
            return false;
        }

        if (string.IsNullOrEmpty(profile.Email) || profile.Email.Length < 5)
        {
            _errorMessage = "Valid Email Address is required";
            return false;
        }

        if (profile.DateOfBirth == DateOnly.MinValue)
        {
            _errorMessage = "Kindly select Date of Birth";
            return false;
        }

        var dif = (DateTime.Now.Year - profile.DateOfBirth.Year);
        if (dif < 16)
        {
            _errorMessage = "Student must be at least 16 years of age";
            return false;
        }

        return true;
    }

    private ProfileVM MapToVM(Profile profile)
    {
        if (profile == null) return new ProfileVM();
        return new ProfileVM
        {
            DateOfBirth = profile.DateOfBirth,
            FirstName = profile.FirstName,
            Email = profile.Email,
            Gender = profile.Gender,
            Id = profile.Id,
            Error = "",
            OtherNames = profile.OtherNames,
            Surname = profile.Surname,
        };
    }

    private Profile MapFromVM(ProfileVM profile)
    {
        if (profile == null) return new Profile();
        return new Profile
        {
            DateOfBirth = profile.DateOfBirth,
            FirstName = profile.FirstName,
            Email = profile.Email,
            Gender = profile.Gender,
            Id = profile.Id,
            OtherNames = profile.OtherNames,
            Surname = profile.Surname,
        };
    }
}