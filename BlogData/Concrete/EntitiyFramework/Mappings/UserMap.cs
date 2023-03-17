using BlogEntities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BlogData.Concrete.EntitiyFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {

            b.Property(x => x.Picture).IsRequired().HasMaxLength(250);

            // Social Media Links
            b.Property(u => u.YoutubeLink).HasMaxLength(250);
            b.Property(u => u.TwitterLink).HasMaxLength(250);
            b.Property(u => u.InstagramLink).HasMaxLength(250);
            b.Property(u => u.FacebookLink).HasMaxLength(250);
            b.Property(u => u.LinkedInLink).HasMaxLength(250);
            b.Property(u => u.GitHubLink).HasMaxLength(250);
            b.Property(u => u.WebsiteLink).HasMaxLength(250);
            // About
            b.Property(u => u.FirstName).HasMaxLength(30);
            b.Property(u => u.LastName).HasMaxLength(30);
            b.Property(u => u.About).HasMaxLength(1000);

            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            b.ToTable("Users");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(50);
            b.Property(u => u.NormalizedUserName).HasMaxLength(50);
            b.Property(u => u.Email).HasMaxLength(100);
            b.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var adminUser = new User
            {
                Id = 1,
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "adminuser@gmail.com",
                NormalizedEmail = "ADMINUSER@GMAIL.COM",
                PhoneNumber = "+905555555555",
                Picture = "/userImages/defaultUser.png",
                FirstName = "Admin",
                LastName = "User",
                About = "Admin User of ProgrammersBlog",
                TwitterLink = "https://twitter.com/adminuser",
                InstagramLink = "https://instagram.com/adminuser",
                YoutubeLink = "https://youtube.com/adminuser",
                GitHubLink = "https://github.com/adminuser",
                LinkedInLink = "https://linkedin.com/adminuser",
                WebsiteLink = "https://programmersblog.com/",
                FacebookLink = "https://facebook.com/adminuser",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = CreatePasswordHash(adminUser, "adminuser");
            var editorUser = new User
            {
                Id = 2,
                UserName = "editoruser",
                NormalizedUserName = "EDITORUSER",
                Email = "editoruser@gmail.com",
                NormalizedEmail = "EDITORUSER@GMAIL.COM",
                PhoneNumber = "+905555555555",
                Picture = "/userImages/defaultUser.png",
                FirstName = "Admin",
                LastName = "User",
                About = "Editor User of ProgrammersBlog",
                TwitterLink = "https://twitter.com/editoruser",
                InstagramLink = "https://instagram.com/editoruser",
                YoutubeLink = "https://youtube.com/editoruser",
                GitHubLink = "https://github.com/editoruser",
                LinkedInLink = "https://linkedin.com/editoruser",
                WebsiteLink = "https://programmersblog.com/",
                FacebookLink = "https://facebook.com/editoruser",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            editorUser.PasswordHash = CreatePasswordHash(editorUser, "editoruser");

            b.HasData(adminUser, editorUser);
        }

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}


