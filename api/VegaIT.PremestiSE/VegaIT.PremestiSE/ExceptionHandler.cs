﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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


        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

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
