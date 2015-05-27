using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.Config
{
    public class AppConfig<T>
    {

        private static Random rand = null;


        public static Random Rand
        {
            get
            {
                if (rand == null)
                {
                    rand = new Random(DateTime.Now.Second);
                }
                return rand;
            }
        }

        private static T instance = default(T);

        public static T Instance
        {
            get
            {
                if (instance == null || instance.Equals(default(T)))
                {
                    instance = (T)ConfigurationManager.GetSection("ApplicationSetting");
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
    }
}
