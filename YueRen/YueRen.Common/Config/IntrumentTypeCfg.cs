using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YueRen.Common.Config
{
    /// <summary>
    /// Summary description for Rings.
    /// </summary>
    [Serializable]
    [XmlRoot("Instruments")]
    public class InstrumentCfg
    {
        const string fileName = "InstrumentType.xml";
        private static InstrumentCfg instance = null;

        public static InstrumentCfg Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    object obj = new object();
                    lock (obj)
                    {
                        if (instance != null) return instance;

                        string xml = LoadXml(fileName);
                        InstrumentCfg cfg = new InstrumentCfg();
                        cfg.FromXml(xml);
                        instance = cfg;
                        return instance;
                    }
                }
            }
        }

        public InstrumentType GetConfigByNameCode(string spName, int code)
        {
            foreach (InstrumentType cp in listRing)
            {
                if (cp.Name.ToLower() == spName.ToLower() && cp.Code == code)
                    return cp;
            }

            return null;
        }
        public InstrumentType GetInstrumentNameByCode(int code)
        {
            foreach (InstrumentType cp in listRing)
            {
                if (cp.Code == code)
                    return cp;
            }

            return null;
        }

        /// <summary>
        /// 获取乐器名
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetInstrumentName(int code)
        {
            InstrumentType provider = this.GetConfigByCode(code);
            if (provider != null)
            {
                return provider.Name;
            }
            else
            {
                return "未获取到乐器名";
            }
        }

        public InstrumentType GetConfigByCode(int code)
        {
            foreach (InstrumentType cp in listRing)
            {
                if (cp.Code == code)
                    return cp;
            }

            return null;
        }

        private static string LoadXml(string fileName)
        {
            string root = System.AppDomain.CurrentDomain.BaseDirectory;
            string webPath = YueRenConfig.Instance.AppSetting.DBMsgConfigPath;
            FileStream fs = null;
            StreamReader rs = null;
            string xml = string.Empty;
            try
            {


                fs = new FileStream(webPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                rs = new StreamReader(fs, System.Text.Encoding.UTF8);

                xml = rs.ReadToEnd();
            }

            finally
            {
                if (rs != null) rs.Close();

                if (fs != null) fs.Close();
            }

            return xml;

        }

        private ArrayList listRing;

        public InstrumentCfg()
        {
            listRing = new ArrayList();
        }

        [XmlElement("Error")]
        public InstrumentType[] Items
        {
            get
            {
                InstrumentType[] items = new InstrumentType[listRing.Count];
                listRing.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;

                InstrumentType[] items = (InstrumentType[])value;
                listRing.Clear();

                foreach (InstrumentType item in items)
                    listRing.Add(item);
            }
        }


        public int AddItem(InstrumentType item)
        {
            return listRing.Add(item);
        }

        /// <summary>
        /// 从xml文件获取实例
        /// </summary>
        /// <param name="xml"></param>
        public void FromXml(string xml)
        {
            InstrumentCfg rings;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);

            XmlSerializer s = new XmlSerializer(typeof(InstrumentCfg));

            MemoryStream ms = new MemoryStream(data);

            TextReader reader = new StreamReader(ms, System.Text.Encoding.UTF8);

            rings = s.Deserialize(reader) as InstrumentCfg;
            reader.Close();


            if (rings == null)
                throw new ApplicationException("数据格式不正确");

            this.listRing = rings.listRing;
        }

    }

    public class InstrumentType
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlAttribute("Code")]
        public int Code;
    }
}

