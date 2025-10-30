
using System.Data;
using Microsoft.Data.SqlClient;
using Task_Management.Domain.Entities;
using Task_Management.Persistence.Data;

namespace Task_Management.Persistence.Repositories
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
        SqlCommand cmd = new SqlCommand("InsertTask", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Title", task.Title);
        cmd.Parameters.AddWithValue("@Description", task.Description);
        cmd.Parameters.AddWithValue("@CreatedBy", task.CreatedBy);
        cmd.Parameters.AddWithValue("@AssignedTo", (object?)task.AssignedTo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(task.Status) ? "Pending" : task.Status);
        cmd.Parameters.AddWithValue("@CreatedDate", task.CreatedDate); // ✅ Added this line
        cmd.Parameters.AddWithValue("@DueDate", (object?)task.DueDate ?? DBNull.Value);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}


        //using storeprocedure
        // public List<TaskModel> GetTasks()
        // {
        //     var tasks = new List<TaskModel>();

        //     using (SqlConnection conn = _db.GetConnection())
        //     {
        //         SqlCommand cmd = new SqlCommand("GetAllTask", conn);
        //         cmd.CommandType = CommandType.StoredProcedure;

        //         conn.Open();
        //         SqlDataReader reader = cmd.ExecuteReader();
        //         while (reader.Read())
        //         {
        //             tasks.Add(new TaskModel
        //             {
        //                 TaskID = (int)reader["TaskId"],
        //                 Title = reader["Title"].ToString(),
        //                 Description = reader["Description"].ToString(),
        //                 CreatedBy = (int)reader["CreatedBy"],
        //                 AssignedTo = reader["AssignedTo"] != DBNull.Value ? (int?)reader["AssignedTo"] : null,
        //                 Status = reader["Status"].ToString(),
        //                 CreatedDate = (DateTime)reader["CreatedDate"],
        //                 DueDate = reader["DueDate"] != DBNull.Value ? (DateTime?)reader["DueDate"] : null
        //             });
        //         }
        //     }

        //     return tasks;
        // }


        //using view
public List<TaskModel> GetTasks()
{
    var tasks = new List<TaskModel>();

    using (SqlConnection conn = _db.GetConnection())
    {
        string query = "SELECT * FROM vw_AllTasks";
        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            tasks.Add(new TaskModel
            {
                TaskID = (int)reader["TaskId"],
                Title = reader["Title"].ToString(),
                Description = reader["Description"].ToString(),
                CreatedBy = (int)reader["CreatedBy"],
                AssignedTo = reader["AssignedTo"] != DBNull.Value ? (int?)reader["AssignedTo"] : null,
                Status = reader["Status"].ToString(),
                CreatedDate = (DateTime)reader["CreatedDate"],
                DueDate = reader["DueDate"] != DBNull.Value ? (DateTime?)reader["DueDate"] : null
            });
        }
    }

    return tasks;
}



        public TaskModel GetTaskById(int id)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetTaskById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskId", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new TaskModel
                    {
                        TaskID = (int)reader["TaskId"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        CreatedBy = (int)reader["CreatedBy"],
                        AssignedTo = reader["AssignedTo"] != DBNull.Value ? (int?)reader["AssignedTo"] : null,
                        Status = reader["Status"].ToString(),
                        CreatedDate = (DateTime)reader["CreatedDate"],
                        DueDate = reader["DueDate"] != DBNull.Value ? (DateTime?)reader["DueDate"] : null
                    };
                }
            }

            return null; // or throw an exception if preferred
        }


        public void UpdateTask(TaskModel task)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TaskId", task.TaskID);
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@CreatedBy", task.CreatedBy);
                cmd.Parameters.AddWithValue("@AssignedTo", (object?)task.AssignedTo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(task.Status) ? "Pending" : task.Status);
                cmd.Parameters.AddWithValue("@CreatedDate", task.CreatedDate);
                cmd.Parameters.AddWithValue("@DueDate", (object?)task.DueDate ?? DBNull.Value);

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