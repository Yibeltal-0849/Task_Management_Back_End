

using System.Data;
using Microsoft.Data.SqlClient;
using Task_Management.Domain.Entities;
using Task_Management.Persistence.Data;
using Task_Management.Infrastructure.Security;


namespace Task_Management.Persistence.Repositories
{
    public class UserRepository
    {
        private readonly DbHelper _db;

        public UserRepository(DbHelper db)
        {
            _db = db;
        }

        // ✅ Create user
        public void AddUser(UserModel user)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("InsertUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    // ✅ Hash the password before inserting
            string hashedPassword = PasswordHelper.HashPassword(user.PasswordHash);
            cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    
                    cmd.Parameters.AddWithValue("@Role", string.IsNullOrEmpty(user.Role) ? (object)DBNull.Value : user.Role);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(user.Status) ? "Active" : user.Status);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Login user
      public UserModel ValidateUserLogin(string email, string password)
{
    using (SqlConnection conn = _db.GetConnection())
    {
        using (SqlCommand cmd = new SqlCommand("ValidateUserLogin", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            string hashedPassword = PasswordHelper.HashPassword(password);

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new UserModel
                    {
                        UserID = (int)reader["UserID"],
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Role = reader["Role"].ToString(),
                        Status = reader["Status"].ToString()
                    };
                }
                else
                {
                    return null; // Invalid email or password
                }
            }
        }
    }
}


        // ✅ Read all users
        public List<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();

            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetAllUsers", conn))
                {
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
                            Role = reader["Role"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }

            return users;
        }

        public UserModel GetUserById(int id)
{
    using (SqlConnection conn = _db.GetConnection())
    {
        using (SqlCommand cmd = new SqlCommand("GetUserById", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", id);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new UserModel
                {
                    UserID = (int)reader["UserID"],
                    UserName = reader["UserName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Role = reader["Role"].ToString(),
                    Status = reader["Status"].ToString()
                };
            }
        }
    }

    return null; // or throw an exception if preferred
}


        // ✅ Update user
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
                    cmd.Parameters.AddWithValue("@Role", updatedUser.Role);
                    cmd.Parameters.AddWithValue("@Status", updatedUser.Status);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Delete user
        public void DeleteUser(int userId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("DeleteUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
