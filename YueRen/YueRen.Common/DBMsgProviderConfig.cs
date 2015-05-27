using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YueRen.Common.Config;

namespace YueRen.Common
{
    /// <summary>
    /// Summary description for Rings.
    /// </summary>
    [Serializable]
    [XmlRoot("Errors")]
    public class DBMsgProviderCfg
    {
        const string fileName = "DBMsgProviderConfig.xml";
        private static DBMsgProviderCfg instance = null;

        public static DBMsgProviderCfg Instance
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
                        DBMsgProviderCfg cfg = new DBMsgProviderCfg();
                        cfg.FromXml(xml);
                        instance = cfg;
                        return instance;
                    }
                }
            }
        }

        public DBMsgProvider GetConfigByNameCode(string spName, int code)
        {
            foreach (DBMsgProvider cp in listRing)
            {
                if (cp.SPName.ToLower() == spName.ToLower() && cp.Code == code)
                    return cp;
            }

            return null;
        }
        public DBMsgProvider GetConfigByCode(int code)
        {
            foreach (DBMsgProvider cp in listRing)
            {
                if (cp.Code == code)
                    return cp;
            }

            return null;
        }

        public string GetErrorMessage(int code)
        {
            DBMsgProvider provider = this.GetConfigByCode(code);
            if (provider != null)
            {
                return provider.Message;
            }
            else
            {
                return "系统错误";
            }
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

        public DBMsgProviderCfg()
        {
            listRing = new ArrayList();
        }

        [XmlElement("Error")]
        public DBMsgProvider[] Items
        {
            get
            {
                DBMsgProvider[] items = new DBMsgProvider[listRing.Count];
                listRing.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;

                DBMsgProvider[] items = (DBMsgProvider[])value;
                listRing.Clear();

                foreach (DBMsgProvider item in items)
                    listRing.Add(item);
            }
        }


        public int AddItem(DBMsgProvider item)
        {
            return listRing.Add(item);
        }

        /// <summary>
        /// 从xml文件获取实例
        /// </summary>
        /// <param name="xml"></param>
        public void FromXml(string xml)
        {
            DBMsgProviderCfg rings;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);

            XmlSerializer s = new XmlSerializer(typeof(DBMsgProviderCfg));

            MemoryStream ms = new MemoryStream(data);

            TextReader reader = new StreamReader(ms, System.Text.Encoding.UTF8);

            rings = s.Deserialize(reader) as DBMsgProviderCfg;
            reader.Close();


            if (rings == null)
                throw new ApplicationException("数据格式不正确");

            this.listRing = rings.listRing;
        }

    }

    public class DBMsgProvider
    {
        [XmlAttribute("Code")]
        public int Code;

        [XmlAttribute("SPName")]
        public string SPName;

        [XmlAttribute("Message")]
        public string Message;

        [XmlAttribute("Type")]
        public string Type;
    }
}

