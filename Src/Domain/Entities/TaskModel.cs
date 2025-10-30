namespace Task_Management.Domain.Entities
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int? AssignedTo { get; set; }  
        public string Status { get; set; } = "Pending";
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
