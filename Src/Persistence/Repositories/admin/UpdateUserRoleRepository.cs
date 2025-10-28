using System.Data;
using Microsoft.Data.SqlClient;
//using Task_Management.Domain.Entities;//note needed b/c it is connect with storeprocedur directly
using Task_Management.Persistence.Data;

namespace Persistence.Repositories.admin
{
    public class UpdateUserRoleRepository
    {
           private readonly DbHelper _db;

        public UpdateUserRoleRepository(DbHelper db)
        {
            _db = db;
        }

    
    public void UpdateUserRole(int userId, string newRole)
{
    using (SqlConnection conn = _db.GetConnection())
    {
        using (SqlCommand cmd = new SqlCommand("UpdateUserRole", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@NewRole", newRole);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
}
}