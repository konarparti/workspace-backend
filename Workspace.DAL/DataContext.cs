using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.DAL.Entities;
using File = Workspace.DAL.Entities.File;

namespace Workspace.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //роли пользователей
            builder.Entity<UserRole>().HasData
            (
                new List<UserRole>
                {
                    new() { Id = 1, Name = "Admin" },
                    new() { Id = 2, Name = "User" },
                }
            );

            //пользователи
            builder.Entity<User>().HasIndex(prop => prop.Login).IsUnique();

            builder.Entity<User>().HasData
            (
                new List<User>
                {
                    new()
                    {
                        Id = 1,
                        Login = "Admin",
                        Email = "Admin@mail.ru",
                        FullName = "Администратор",
                        Status = 1, // активен
                        UserRoleId = 1,
                        Password = BCrypt.Net.BCrypt.HashPassword("Admin"),
                        Created = DateTimeOffset.UtcNow,
                        ProfileImage = "",
                    },
                    new()
                    {
                        Id = 2,
                        Login = "User",
                        Email = "User@mail.ru",
                        FullName = "Пользователь",
                        Status = 1, // активен
                        UserRoleId = 2,
                        Password = BCrypt.Net.BCrypt.HashPassword("User"),
                        Created = DateTimeOffset.UtcNow,
                        ProfileImage = "",
                    },
                }
            );
        }
    }
}
