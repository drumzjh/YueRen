using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Common.Util;

namespace YueRen.Common
{
    /// <summary>
    ///  关于本类的说明
    /// </summary>
    public class UploadImageInfo
    {
        private string _name;

        /// <summary>
        /// 图像名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _base64Image;

        /// <summary>
        /// Base64编码的图像内容
        /// </summary>
        public string Base64Image
        {
            get { return _base64Image; }
            set { _base64Image = value; }
        }

        private string _directory;

        /// <summary>
        /// 保存的目录，默认
        /// Web服务器根目录，
        /// </summary>
        public string Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }
        private long _userId;

        public long UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private string _type;

        /// <summary>
        /// 图片用途类别
        /// 1:用户头像 2：club
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _imgFormat;

        /// <summary>
        /// 图片类型
        /// </summary>
        public string ImgFormatType
        {
            get { return _imgFormat; }
            set { _imgFormat = value; }
        }

        public UploadImageInfo()
        { }


        public UploadImageInfo(string name, long userId, string imageData, string type, string directory)
        {
            this.Name = name;
            this.UserId = userId;
            this.Base64Image = imageData;
            this.Type = type;
            this.Directory = directory;
        }

    }

    public class UploadImage
    {
        public static void Upload(UploadImageInfo imageInfo)
        {
            SDMSMQ.SendObject(YueRenConfig.Instance.AppSetting.UploadImageMqPath, imageInfo, imageInfo.Name);
        }



        public static void Upload(string name, long userId, Stream imageStream, string type, string directory)
        {
            Byte[] imageData = new byte[imageStream.Length];

            imageStream.Read(imageData, 0, (int)imageStream.Length);

            MemoryStream ms = new MemoryStream(imageData, false);


            string base64String = Convert.ToBase64String(ms.ToArray());


            UploadImageInfo imageInfo = new UploadImageInfo(name, userId, base64String, type, directory);

            Upload(imageInfo);
        }
    }
}


