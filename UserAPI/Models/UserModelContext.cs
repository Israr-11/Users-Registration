using Microsoft.EntityFrameworkCore;

namespace UserAPI.Models
{
    public class UserModelContext : DbContext
    {
        public UserModelContext(DbContextOptions<UserModelContext> options) : base(options) { }

        public DbSet<UserModel> UserTable1 { get; set; }
        //usersModel is just the name of the model in Visual Studio
        //UserTable1 is the table created in VS
    }
}
