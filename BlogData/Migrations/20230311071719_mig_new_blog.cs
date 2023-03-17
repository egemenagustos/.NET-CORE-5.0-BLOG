﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogData.Migrations
{
    public partial class mig_new_blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                  "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('.NET5 ile Gelen Yenilikler','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk C# Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'C# Developer','C# 9.0 .NET5','C# F# .NET CORE .NET ASP.NET MVC5',0,1)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('C++ ile Algoritma Yapıları','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk C++ Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'C++ Developer','C++ 99, 11 20, Linked List Veri Yapıları','C++ 99, 11 20, Linked List Veri Yapıları',0,10)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Asenkron JavaScript','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk JavaScript Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'JavaScript Developer','Javascript ES6-ES7-ES8','Javascript ES6-ES7-ES8',0,25)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Python ile Veri Analizi','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk Python Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'Python Developer','Python ile Algoritmalar ve Veri Analizi','Python ile Algoritmalar ve Veri Analizi',0,99)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Java ile Android ve Mobil Programlama','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk Java Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'Java Developer','Java, Kotlin, Android','Java, Kotlin, Android',0,235)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Dart ve Flutter ile IOS & Android Programlama','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk Java Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'Dart Developer','Dart, Flutter, Android, IOS, Mobil','Dart, Flutter, Android, IOS, Mobil',0,666)");
            migrationBuilder.Sql(
                            "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('.NET5 ile Gelen Yenilikler 2','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk C# Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'C# Developer','C# 9.0 .NET5','C# F# .NET CORE .NET ASP.NET MVC5',0,1)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('C++ ile Algoritma Yapıları 2','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk C++ Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'C++ Developer','C++ 99, 11 20, Linked List Veri Yapıları','C++ 99, 11 20, Linked List Veri Yapıları',0,10)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Asenkron JavaScript 2','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk JavaScript Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'JavaScript Developer','Javascript ES6-ES7-ES8','Javascript ES6-ES7-ES8',0,25)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Python ile Veri Analizi 2','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk Python Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'Python Developer','Python ile Algoritmalar ve Veri Analizi','Python ile Algoritmalar ve Veri Analizi',0,99)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Java ile Android ve Mobil Programlama 2','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk Java Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'Java Developer','Java, Kotlin, Android','Java, Kotlin, Android',0,235)");
            migrationBuilder.Sql(
                        "INSERT INTO [Blog].dbo.Articles (Title,[Content],Note,Thumbnail,[Date],[CreatedDate],CreatedByName,ModifiedDate,ModifiedByName,IsActive,IsDeleted,UserId,CategoryId,SeoAuthor,SeoDescription,SeoTags,CommentCount,ViewsCount) VALUES ('Dart ve Flutter ile IOS & Android Programlama 2','Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia daki Hampden-Sydney College dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan consectetur sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malorum (iyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500 lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir.Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H.Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.','İlk Java Paylaşımı','postImages/defaultThumbnail.jpg',GETDATE(),GETDATE(),'Migration',GETDATE(),'Migration',1,0,1,(SELECT TOP 1 Categories.Id FROM dbo.Categories),'Dart Developer','Dart, Flutter, Android, IOS, Mobil','Dart, Flutter, Android, IOS, Mobil',0,666)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
