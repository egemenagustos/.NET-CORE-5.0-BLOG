using BlogShared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using System.Data.SqlTypes;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.Extensions.Logging;

namespace BlogMvc.Filters
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly IModelMetadataProvider _metadataProvider;
        private readonly ILogger _logger;

        public MvcExceptionFilter(IHostEnvironment environment, IModelMetadataProvider metadataProvider, ILogger<MvcExceptionFilter> logger)
        {
            _environment = environment;
            _metadataProvider = metadataProvider;
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (_environment.IsDevelopment())
            {
                filterContext.ExceptionHandled = true;
                var mvcErrorModel = new MvcErrorModel();
                ViewResult result;
                switch (filterContext.Exception)
                {
                    case SqlNullValueException:
                        mvcErrorModel.Message = $"Üzgünüz işleminiz sırasında beklenmedik bir veritabanı hatası oluştu. Sorunu en kısa sürede çözeceğiz.";
                        mvcErrorModel.Detail = filterContext.Exception.Message;
                        result = new ViewResult { ViewName = "_Error" };
                        result.StatusCode = 500;
                        _logger.LogError(filterContext.Exception, filterContext.Exception.Message);
                        break;
                    case NullReferenceException:
                        mvcErrorModel.Message = $"Üzgünüz işleminiz sırasında beklenmedik bir null veriye rastlandı. Sorunu en kısa sürede çözeceğiz.";
                        mvcErrorModel.Detail = filterContext.Exception.Message;
                        result = new ViewResult { ViewName = "_Error" };
                        result.StatusCode = 403;
                        _logger.LogError(filterContext.Exception, filterContext.Exception.Message);
                        break;
                    default:
                        mvcErrorModel.Message = $"Üzgünüz işleminiz sırasında beklenmedik bir hata oluştu. Sorunu en kısa sürede çözeceğiz.";
                        result = new ViewResult { ViewName = "_Error" };
                        result.StatusCode = 500;
                        _logger.LogError(filterContext.Exception, filterContext.Exception.Message);
                        break;
                }
                result.ViewData = new ViewDataDictionary(_metadataProvider, filterContext.ModelState);
                result.ViewData.Add("MvcErrorModel", mvcErrorModel);
                filterContext.Result = result;
            }
        }
    }
}
