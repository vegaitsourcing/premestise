using Persistence.Interfaces.Entites;
using RazorEngine;
using RazorEngine.Templating;
using System;

namespace Core.Services
{
    public class EmailTemplateService
    {
        public string GetEmailTemplate(string emailText, MatchEmailInformation model)
        {
            string result = Engine.Razor.RunCompile(emailText, "templateKey", typeof(MatchEmailInformation), model);
            return result;
        }
    }
}
