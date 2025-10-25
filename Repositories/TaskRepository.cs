
using Task_Management.Data;
using Task_Management.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Task_Management.Repositories
{
    public class TaskRepository
    {
         private readonly DbHelper _db;

        public TaskRepository(DbHelper db)
        {
            _db = db;
        }

        public void AddTask(TaskModel task)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@Status", task.Status);
                cmd.Parameters.AddWithValue("@UserID", task.UserID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<TaskModel> GetTasks()
        {
            var tasks = new List<TaskModel>();

            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetTasks", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new TaskModel
                    {
                        TaskID = (int)reader["TaskID"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Status = reader["Status"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        UserID = (int)reader["UserID"], 
                        UserName = reader["UserName"].ToString()
                    });
                }
            }

            return tasks;
        }

        public void UpdateTaskStatus(int taskId, string status)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateTaskStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskID", taskId);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int taskId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeleteTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskID", taskId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}