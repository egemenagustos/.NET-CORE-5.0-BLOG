﻿using BlogEntities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogData.Concrete.EntitiyFramework.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);
            builder.ToTable("Categories");
            builder.HasData(
                 new Category
                 {
                     Id = 1,
                     Name = "C#",
                     Description = "C# Programlama Dili ile İlgili En Güncel Bilgiler",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedByName = "InitialCreate",
                     CreatedDate = DateTime.Now,
                     ModifiedByName = "InitialCreate",
                     ModifiedDate = DateTime.Now,
                     Note = "C# Blog Kategorisi",
                 },
                new Category
                {
                    Id = 2,
                    Name = "C++",
                    Description = "C++ Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ Blog Kategorisi",
                },

                new Category
                {
                    Id = 3,
                    Name = "JavaScript",
                    Description = "JavaScript Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "JavaScript Blog Kategorisi",
                },
                new Category
                {
                    Id = 4,
                    Name = "Typescript",
                    Description = "Typescript Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Typescript Blog Kategorisi",
                }
                ,
                new Category
                {
                    Id = 5,
                    Name = "Java",
                    Description = "Java Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Java Blog Kategorisi",
                }
                ,
                new Category
                {
                    Id = 6,
                    Name = "Python",
                    Description = "Python Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Python Blog Kategorisi",
                }
                ,
                new Category
                {
                    Id = 7,
                    Name = "Php",
                    Description = "Php Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Php Blog Kategorisi",
                }
                ,
                new Category
                {
                    Id = 8,
                    Name = "Kotlin",
                    Description = "Kotlin Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Kotlin Blog Kategorisi",
                }
                ,
                new Category
                {
                    Id = 9,
                    Name = "Swift",
                    Description = "Swift Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Swift Blog Kategorisi",
                }
                ,
                new Category
                {
                    Id = 10,
                    Name = "Ruby",
                    Description = "Ruby Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Ruby Blog Kategorisi",
                }
            );
        }
    }
}
