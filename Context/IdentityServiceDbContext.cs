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
        public required DbSet<Permission> Permission { get; set; }
        public required DbSet<Role> Role { get; set; }
        public required DbSet<Group> Group { get; set; }
        public required DbSet<UserPermission> UserPermission { get; set; }
        public required DbSet<UserRole> UserRole { get; set; }
        public required DbSet<UserGroup> UserGroup { get; set; }
        public required DbSet<RoleGroup> RoleGroup { get; set; }
        public required DbSet<RolePermission> RolePermission { get; set; }
        public required DbSet<UserPermissionView> UserPermissionView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPermissionView>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("User_Permissions");

                entity.Property(e => e.UserId).HasColumnName("User_Id");
                entity.Property(e => e.PermissionName).HasColumnName("Permission_Name");
            });
        }
    }
}