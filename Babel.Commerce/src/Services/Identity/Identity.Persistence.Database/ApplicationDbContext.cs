using Identity.Domain;
using Identity.Persistence.Database.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Database
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser,                  
        ApplicationRole,                       
        string,                               
        IdentityUserClaim<string>,             
        ApplicationUserRole,                  
        IdentityUserLogin<string>,            
        IdentityRoleClaim<string>,            
        IdentityUserToken<string>              
    >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // 👇 Necesario para que EF Core reconozca ApplicationUserRole como entidad
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema (opcional, por organización)
            builder.HasDefaultSchema("Identity");

            // Model constraints
            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ApplicationUserConfiguration(modelBuilder.Entity<ApplicationUser>());
            new ApplicationRoleConfiguration(modelBuilder.Entity<ApplicationRole>());
            new ApplicationUserRoleConfiguration(modelBuilder.Entity<ApplicationUserRole>());
        }
    }
}
