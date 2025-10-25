using Task_Management.Data;
using System.Data;
using Task_Management.Models;
using Microsoft.Data.SqlClient;

namespace Task_Management.Repositories
{
    public class UserRepository
    {
        private readonly DbHelper _db;

        public UserRepository(DbHelper db)
        {
            _db = db;
        }

        // ✅ Add a new user
        public void AddUser(UserModel user)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("AddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ✅ Get all users
        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();

            using (SqlConnection conn = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetAllUsers", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserModel
                    {
                        UserID = (int)reader["UserID"],
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        //PasswordHash = reader["PasswordHash"].ToString(),
                        Status = reader["Status"].ToString() // ✅ Add this if column exists
                    });
                }
            }

            return users;
        }

       
        // ✅ Update user information (name, email, status, etc.)
        public void UpdateUser(int userId, UserModel updatedUser)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@UserName", updatedUser.UserName);
                    cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", updatedUser.PasswordHash);
                    cmd.Parameters.AddWithValue("@Status", updatedUser.Status);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
