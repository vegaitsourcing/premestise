using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Persistence.Interfaces.Entites.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Exceptions;

namespace VegaIT.PremestiSE
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, exception.Message);

            if (exception is EntityNotFoundException)
            {
                context.HttpContext.Response.StatusCode = 404;
            }
            else if (exception is HashIdException)
            {
                context.HttpContext.Response.StatusCode = 400;
            }


            var result = JsonConvert.SerializeObject(new { StatusCode = context.HttpContext.Response.StatusCode, Error = exception.Message });

            context.Result = new ObjectResult(result);
        }

    }
}
