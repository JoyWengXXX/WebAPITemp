using webAPITemp.Models.Entities.DefaultDB;

namespace webAPITemp.Models.DTOs.DefaultDB
{
    public class UserDTO 
    {
        public int SerialNum { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
