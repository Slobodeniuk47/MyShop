using Data.MyShop.Entities;
using Data.MyShop.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop
{
    public class ApplicationDbContext : IdentityDbContext
        <
           UserEntity, RoleEntity, long, IdentityUserClaim<long>,
           PermissionsEntity, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>
        >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CommentEntity> Comment { get; set; }
        public DbSet<CommentImageEntity> CommentImages { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //new DbInitializer(modelBuilder).Seed();
            modelBuilder.Entity<PermissionsEntity>(permissions =>
            {
                permissions.HasKey(perm => new { perm.UserId, perm.RoleId });
                //Role
                permissions.HasOne(perm => perm.Role)
                   .WithMany(perm => perm.Permissions)
                   .HasForeignKey(perm => perm.RoleId)
                   .IsRequired();

                //User
                permissions.HasOne(perm => perm.User)
                   .WithMany(perm => perm.Permissions)
                   .HasForeignKey(perm => perm.UserId)
                   .IsRequired();
            });
        }
    }
}
