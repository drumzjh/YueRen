using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YueRen.Common.Config
{
    public partial class AppSetting
    {
        [XmlAttribute]
        public string ClassConfigPath = @"C:\ClassProviderConfig.xml";

        /// <summary>
        /// 章节图片文件分发服务器
        /// </summary>
        /// <remarks>
        /// 前后顺序一定要注意
        /// </remarks>
        [XmlAttribute]
        public string ChapterImageNetFileTransferServer = "http://file1.qidian.com/,tcp://file1.qdintra.com:8085/FileServer;http://file2.qidian.com/,tcp://file2.qdintra.com:8086/FileServer";

        /// <summary>
        /// 图像相关
        /// </summary>
        [XmlAttribute]
        public string UploadImageMqPath = "";

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string DBMsgConfigPath = "";

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string ConnectionString = "";

        /// <summary>
        /// 乐器类型相关
        /// </summary>
        [XmlAttribute]
        public string InstrumentTypePath = "";

        /// <summary>
        /// 音乐风格相关
        /// </summary>
        [XmlAttribute]
        public string MusicStylePath = "";
    }
}
