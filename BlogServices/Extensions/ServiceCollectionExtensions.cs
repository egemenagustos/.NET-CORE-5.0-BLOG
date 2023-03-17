using BlogData.Abstract;
using BlogData.Concrete;
using BlogData.Concrete.EntitiyFramework.Context;
using BlogEntities.Concrete;
using BlogServices.Abstract;
using BlogServices.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogServices.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollections, string connectionStrings)
        {
            serviceCollections.AddDbContext<Context>(opt =>
            {
                opt.UseSqlServer(connectionStrings);
            });
            serviceCollections.AddIdentity<User, Role>(opt =>
            {
                //Şifre ayarları
                opt.Password.RequireDigit = false; //rakam olsun mu, olmasın mı
                opt.Password.RequiredLength = 5; //şifre kaç karakter olmalı
                opt.Password.RequiredUniqueChars = 0; //özel karakter kaç tane olmalı
                opt.Password.RequireNonAlphanumeric = false; //@ ! ? $ işaretleri olsun mu
                opt.Password.RequireLowercase = false; //karakterler küçük olsun mu
                opt.Password.RequireUppercase = false; //karakterler büyük olsun mu

                //Kullanıcı adı ve mail ayarları
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; //kullanıcı adı oluşturuken izin verilen karakterler
                opt.User.RequireUniqueEmail = true; //kayıt edilen mail adresinden sadece 1 tane olmasını sağlarız
            }).AddEntityFrameworkStores<Context>();
            serviceCollections.Configure<SecurityStampValidatorOptions>(opt =>
            {
                opt.ValidationInterval = TimeSpan.FromMinutes(15);
            });
            serviceCollections.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollections.AddScoped<ICategoryService, CategoryManager>();
            serviceCollections.AddScoped<IArticleService, ArticleManager>();
            serviceCollections.AddScoped<ICommentService, CommentManager>();
            serviceCollections.AddSingleton<IMailService, MailManager>();
            return serviceCollections;
        }
    }
}
