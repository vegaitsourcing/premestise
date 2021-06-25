using System.Collections.Generic;

namespace Persistence.Interfaces.Entites
{
    public class MatchEmailInformation
    {
        public string ToEmail { get; set; }
        public int ChainLength { get; set; }
        public string AgeGroup { get; set; }
        public string TopBannerLogo { get; set; }
        public string FooterLogo { get; set; }
        public List<MatchInformation> Matches { get; set; } = new List<MatchInformation>();
    }
}
