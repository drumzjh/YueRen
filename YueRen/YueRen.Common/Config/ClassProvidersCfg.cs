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
    [XmlRoot("ClassProviders")]
    public class ClassProvidersCfg
    {
        private const string fileName = "D:\\ClassProviderConfig.xml";
        private static ClassProvidersCfg instance = null;
        private static object snycRoot = new object();

        public static ClassProvidersCfg Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    lock (snycRoot)
                    {
                        if (instance != null)
                        {
                            return instance;
                        }

                        string xml = LoadXml(fileName);
                        ClassProvidersCfg cfg = new ClassProvidersCfg();
                        cfg.FromXml(xml);
                        instance = cfg;
                        return instance;
                    }
                }
            }
        }

        public ClassProvider GetConfigByName(string className)
        {
            foreach (ClassProvider cp in listRing)
            {
                if (cp.ClassName.ToLower() == className.ToLower())
                {
                    return cp;
                }
            }

            return null;
        }


        private static string LoadXml(string fileName)
        {
            string root = System.AppDomain.CurrentDomain.BaseDirectory;
            string webPath = root + "\\Config\\" + fileName;
            webPath = YueRenConfig.Instance.AppSetting.ClassConfigPath;
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

        public ClassProvidersCfg()
        {
            listRing = new ArrayList();
        }

        [XmlElement("ClassProvider")]
        public ClassProvider[] Items
        {
            get
            {
                ClassProvider[] items = new ClassProvider[listRing.Count];
                listRing.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                ClassProvider[] items = (ClassProvider[])value;
                listRing.Clear();

                foreach (ClassProvider item in items)
                {
                    listRing.Add(item);
                }
            }
        }


        public int AddItem(ClassProvider item)
        {
            return listRing.Add(item);
        }

        /// <summary>
        /// 从xml文件获取实例
        /// </summary>
        /// <param name="xml"></param>
        public void FromXml(string xml)
        {
            ClassProvidersCfg rings;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);

            XmlSerializer s = new XmlSerializer(typeof(ClassProvidersCfg));

            MemoryStream ms = new MemoryStream(data);

            TextReader reader = new StreamReader(ms, System.Text.Encoding.UTF8);

            rings = s.Deserialize(reader) as ClassProvidersCfg;
            reader.Close();

            if (rings == null)
            {
                throw new ApplicationException("数据格式不正确");
            }

            this.listRing = rings.listRing;
        }

    }

    public class ClassProvider
    {
        [XmlAttribute("ClassName")]
        public string ClassName;

        [XmlAttribute("Provider")]
        public string Provider;

        [XmlAttribute("Desc")]
        public string Desc = "PTB";

    }
}

