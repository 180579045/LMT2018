using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
    public static class NPECheckRulesHelper
    {
        public static bool CheckNetPlanData(MAP_DEVTYPE_DEVATTRI mapMib_this, int equipType, out string falseTip)
        {
            NPECheckRulesDeal rulesHelper = new NPECheckRulesDeal(mapMib_this, equipType);
            bool res = rulesHelper.CheckAllPlanData(out falseTip);
            return res;
        }
    }
}
