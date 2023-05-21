using Microsoft.EntityFrameworkCore;
using UserModule.Data.Entities.Notifications;
using UserModule.Data.Entities.Roles;
using UserModule.Data.Entities.Users;

namespace UserModule.Data;

public class UserContext:DbContext
{
	public UserContext(DbContextOptions<UserContext> options):base(options)
	{
	}


	public DbSet<User> Users { get; set; }
	public DbSet<UserRoles> UserRoles { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<RolesPerimission> RolesPerimissions { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }

}
