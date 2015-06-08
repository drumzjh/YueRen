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
        public string ChapterImageNetFileTransferServer = "";
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
