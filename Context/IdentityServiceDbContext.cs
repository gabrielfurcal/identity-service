using identity_service.Models;
using Microsoft.EntityFrameworkCore;

namespace identity_service.Context
{
    public class IdentityServiceDbContext : DbContext
    {
        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options)
            : base(options)
        {
        }

        public required DbSet<User> User { get; set; }
        public required DbSet<RefreshToken> RefreshToken { get; set; }
        public required DbSet<Role> Role { get; set; }
        public required DbSet<Group> Group { get; set; }
        public required DbSet<UserRole> UserRole { get; set; }
        public required DbSet<UserGroup> UserGroup { get; set; }
        public required DbSet<RoleGroup> RoleGroup { get; set; }
        public required DbSet<UserRoleView> UserRoleView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleView>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("User_Roles");

                entity.Property(e => e.UserId).HasColumnName("User_Id");
                entity.Property(e => e.RoleName).HasColumnName("Role_Name");
            });
        }
    }
}