using System.ComponentModel.DataAnnotations;

namespace LastTask.Model
{
    public class UserRigModel
    {
        public string username { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z]).{8,}$", ErrorMessage = "The password must be at least 8 characters long and contain at least one character.")]
        public string password { get; set; }
        public string confpassword { get; set; }
    }
}
