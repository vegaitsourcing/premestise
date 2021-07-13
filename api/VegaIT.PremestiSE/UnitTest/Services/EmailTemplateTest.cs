using Core.Services;
using Persistence.Interfaces.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Xunit;
namespace UnitTest.Services
{
    public class EmailTemplateTest
    {
        private string _emailText = @"";
        private EmailTemplateService templateService = new EmailTemplateService();

        [Fact]
        public void TestEmail()
        {
            string text = File.ReadAllText(@"C:\Work\Vega\premestise\api\VegaIT.PremestiSE\Core\Templates\circular.htm");
            MatchEmailInformation model = new MatchEmailInformation
            {
                AgeGroup = "Bebe",
                ChainLength = 5,
                TopBannerLogo = "omg",
                FooterLogo = "omg"
            };
            MatchInformation first = new MatchInformation
            {
                Email = "first@email.com",
                Name = "First Parent",
                Phone = "0523412",
                FromKindergarden = "First Kindergarden",
                ToKindergarden = "Second kindergarden"
            };
            model.Matches.Add(first);

            MatchInformation second = new MatchInformation
            {
                Email = "Second@email.com",
                Name = "Second Parent",
                Phone = "0523412",
                FromKindergarden = "Second Kindergarden",
                ToKindergarden = "Third kindergarden"
            };
            model.Matches.Add(second);

            MatchInformation third = new MatchInformation
            {
                Email = "third@email.com",
                Name = "third Parent",
                Phone = "0523412",
                FromKindergarden = "Third Kindergarden",
                ToKindergarden = "Fourth kindergarden"
            };
            model.Matches.Add(third);

            MatchInformation fouth = new MatchInformation
            {
                Email = "fourth@email.com",
                Name = "fourth Parent",
                Phone = "0523412",
                FromKindergarden = "Fourth Kindergarden",
                ToKindergarden = "First kindergarden"
            };
            model.Matches.Add(fouth);

            string compiled = templateService.GetEmailTemplate(text, model);
        }
    }
}
