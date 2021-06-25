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
                FirstParentEmail = "first@email.com",
                FirstParentName = "First Parent",
                FirstParentPhone = "0523412",
                SecondParentEmail = "second@email.com",
                SecondParentName = "Second Parent",
                SecondParentPhone = "05234 234 123",
                FromKindergarden = "First Kindergarden",
                ToKindergarden = "Second kindergarden"
            };
            model.Matches.Add(first);

            MatchInformation second = new MatchInformation
            {
                FirstParentEmail = "Second@email.com",
                FirstParentName = "Second Parent",
                FirstParentPhone = "0523412",
                SecondParentEmail = "Third@email.com",
                SecondParentName = "Third Parent",
                SecondParentPhone = "05234 234 123",
                FromKindergarden = "Second Kindergarden",
                ToKindergarden = "Third kindergarden"
            };
            model.Matches.Add(second);

            MatchInformation third = new MatchInformation
            {
                FirstParentEmail = "third@email.com",
                FirstParentName = "third Parent",
                FirstParentPhone = "0523412",
                SecondParentEmail = "fourth@email.com",
                SecondParentName = "Fourth Parent",
                SecondParentPhone = "05234 234 123",
                FromKindergarden = "Third Kindergarden",
                ToKindergarden = "Fourth kindergarden"
            };
            model.Matches.Add(third);

            MatchInformation fouth = new MatchInformation
            {
                FirstParentEmail = "fourth@email.com",
                FirstParentName = "fourth Parent",
                FirstParentPhone = "0523412",
                SecondParentEmail = "first@email.com",
                SecondParentName = "First Parent",
                SecondParentPhone = "05234 234 123",
                FromKindergarden = "Fourth Kindergarden",
                ToKindergarden = "First kindergarden"
            };
            model.Matches.Add(fouth);

            string compiled = templateService.GetEmailTemplate(text, model);
        }
    }
}
