namespace Task_Management.Domain.Entities
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty; // joined from Users
    }
}