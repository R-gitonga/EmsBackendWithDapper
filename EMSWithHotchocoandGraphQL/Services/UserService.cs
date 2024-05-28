using Dapper;
using EMSWithHotchocoandGraphQL.Data_Table_Objects;
using GreenDonut;
using HotChocolate.Types.Pagination;
using System.Data;
using System.Data.SqlClient;

namespace EMSWithHotchocoandGraphQL.Services
{
    public class UserService:IUserService
    {
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DBCS"));
            }
        }
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public UsersDto GetFirstPerson()
        {
            using (var conn = Connection)
            {
                var query = "Select top 1 * from Users";
                var users = conn.Query<UsersDto>(query).FirstOrDefault();
                return users;
            }
        }
        public List<UsersDto> GetAllPersons() 
        {
            using (var conn = Connection)
            {
                var result = conn.Query<UsersDto>("GetAllUsers", commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }
        public List<UsersDto> GetByUserName(string userName)
        {
            using (var conn = Connection)
            {
                var result = conn.Query<UsersDto>("GetByUserName", param: new { userName }, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<UsersDto> GetUserRole()
        {
            using (var conn = Connection)
            {
                var query = @"
                            select * from Users
                            select * from Roles";
                using var reader = conn.QueryMultiple(query);
                var users = reader.Read<UsersDto>().ToList();
                var roles = reader.Read<UserRoleDto>().ToList();

                foreach (var user in users)
                {
                    var UserRole = roles.FirstOrDefault(role => role.RoleId == user.RoleId);
                    user.Role = UserRole;

                    // If the user doesn't have a role assigned, assign the default role ID
                    if (user.Role == null)
                    {
                        user.Role = roles.FirstOrDefault(r => r.RoleId == 1);
                    }
                }
                return users;
            }
        }

        public int SaveUser(UsersDto users)
        {
            using (var conn = Connection)
            {
                var command = @"INSERT INTO Users (
                        UserName, Email, Phone, UserPassword, Created_at, Updated_at, LastLogin, RegistrationDate, CreatedBy, UpdatedBy, RoleId
                    ) VALUES (
                        @UserName, @Email, @Phone, @UserPassword, GETDATE(), GETDATE(), GETDATE(), GETDATE(), @CreatedBy, @UpdatedBy, @RoleId
                    )";

                return conn.Execute(command, new
                {
                    users.UserName,
                    users.Email,
                    users.Phone,
                    users.UserPassword,
                    users.CreatedBy,
                    users.UpdatedBy,
                    users.RoleId
                });
            }
        }
        //BUlk operation for delete
        public string BulkDeleteUsers(List<int> userIds)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();

                    using (var command = new SqlCommand("BulkUsersDelete", (SqlConnection)conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Create Table-Valued-Parameter. this allows us to pass a list of integers to our stored procedure
                        var param = command.Parameters.AddWithValue("@UserIds", CreateDataTable(userIds));
                        param.SqlDbType = SqlDbType.Structured;
                        param.TypeName = "dbo.UserIdsList"; // Name of the TVP type in SQL Server

                        command.ExecuteNonQuery();
                    }

                    // Close the connection
                    conn.Close();

                    return "Bulk deletion successful";
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return $"Error: {ex.Message}";
            }
        }

        // Helper method to create DataTable for TVP
        private DataTable CreateDataTable(List<int> userIds)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UserId", typeof(int));

            foreach (var userId in userIds)
            {
                dataTable.Rows.Add(userId);
            }

            return dataTable;
        }




    }
}
