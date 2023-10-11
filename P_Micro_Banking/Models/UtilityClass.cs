using System.ComponentModel.DataAnnotations;

namespace P_Micro_Banking.Models
{
    public class UtilityClass
    {
        public string code { get; set; }
        public string item { get; set; }
        public string item1 { get; set; }
    }

    public class UtilityUserClass
    {
        public string item1 { get; set; }
        public string item2 { get; set; }
        public string item3 { get; set; }
        public string item4 { get; set; }
        public string item5 { get; set; }
        public string item6 { get; set; }
        public string item7 { get; set; }
        public string item8 { get; set; }
    }

    public class UtilityUserClassParam
    {
        public string UserName { get; set; }

    }

    public enum UserRoleName
    {
        Auditee = 1,
        Field_Auditor = 2,
        State_Auditor = 3,
        IT_Auditor = 4,
        Zone_Auditor = 5,
        Head_Office = 6,
        Management = 7,
        Sub_Admin = 8,
        Admin = 9
    }

    public enum StatusID
    {
        Ongoing = 1,
        Resolved = 2,
        Pending = 3,
        Completed = 4
    }

    public class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
    public class State
    {
        [Key]
        public string Sate_Name { get; set; }

    }
    
}
