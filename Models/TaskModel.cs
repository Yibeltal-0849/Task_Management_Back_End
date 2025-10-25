using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Management.Models
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