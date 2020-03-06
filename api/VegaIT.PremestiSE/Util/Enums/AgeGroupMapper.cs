using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Enums
{
    public class AgeGroupMapper
    {
        public string mapGroupToText(AgeGroup group)
        {
            if (group == AgeGroup.BebeGrupe)
                return "Bebe grupa";

            if (group == AgeGroup.MladjaJaslena)
                return "Mlađa jaslena grupa";

            if (group == AgeGroup.StarijaJaslena)
                return "Starija jaslena grupa";

            if (group == AgeGroup.MladjaGrupa)
                return "Mlađa grupa";

            if (group == AgeGroup.SrednjaGrupa)
                return "Srednja grupa";

            if (group == AgeGroup.StarijaGrupa)
                return "Starija grupa";

            if (group == AgeGroup.Predskolska)
                return "Predškolska grupa";

            return "";
        }
    }
}
