using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.Config
{
    public class YueRenConfig
    {
        private readonly static Random rand = new Random(DateTime.Now.Second);

        public static ApplicationSetting Instance
        {
            get { return AppConfig<ApplicationSetting>.Instance; }
        }

        public static Random Rand
        {
            get { return rand; }
        }
    }
}
