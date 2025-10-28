namespace Task_Management.Domain.Entities
{
    public class  UserModel
    {
        
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string Status { get; set; } = "Active";
    }
}