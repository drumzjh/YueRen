using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace YueRen.Common.Config
{
    class AppConfigHandler
    {
        #region IConfigurationSectionHandler 成员 : 获取配置信息（通用接口）

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XPathNavigator nav = section.CreateNavigator();
            string typename = (string)nav.Evaluate("string(@type)");
            Type t = Type.GetType(typename);
            //利用XML反序列化的方式将配置和属性配对
            XmlSerializer ser = new XmlSerializer(t);
            return ser.Deserialize(new XmlNodeReader(section));
        }
        #endregion
    }
}
