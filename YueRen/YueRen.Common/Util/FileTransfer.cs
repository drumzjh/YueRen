using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using YueRen.Common.Log;
using YueRen.Common.Config;

namespace YueRen.Common.Util
{
    /// <summary>
    /// 文件传输 方式上传图片文件
    /// </summary>
    public class NetFileTransferUploadImage
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileServer">文件服务器, 根据 GetFileServerIndexByFileName(文件路径) 得到</param>
        /// <param name="filePath">文件路径,不能为http://开头的路径</param>
        /// <param name="imageStream">文件流</param>
        public static bool Upload(NetFileTransferServerConfig.FileServer fileServer, string filePath, Stream imageStream)
        {
            using (NetFileTransferClient client = new NetFileTransferClient())
            {
                if (fileServer == null)
                {
                    LogHelper.Error("NetFileTransferUploadImage-fileServer 不能为空:");
                    throw new ArgumentNullException("fileServer", "fileServer 不能为空");
                }
                client.NoticeRegisterMethodName = fileServer.MethodName;
                client.Connect();
                var buffer = new byte[imageStream.Length];
                imageStream.Read(buffer, 0, buffer.Length);
                //LogHelper.Error("NoticeRegisterMethodName={0},buffer.Length={1},filePath={2}", client.NoticeRegisterMethodName, buffer.Length, filePath);
                return client.SendFileBytes(buffer, filePath);
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileServer">文件服务器, 根据 GetFileServerIndexByFileName(文件路径) 得到</param>
        /// <param name="filePath">文件路径,不能为http://开头的路径</param>
        /// <returns></returns>
        public static bool DelFile(NetFileTransferServerConfig.FileServer fileServer, string filePath)
        {
            using (NetFileTransferClient client = new NetFileTransferClient())
            {
                if (fileServer == null)
                {
                    LogHelper.Error("NetFileTransferUploadImage-fileServer 不能为空:");
                    throw new ArgumentNullException("fileServer", "fileServer 不能为空");
                }
                client.NoticeRegisterMethodName = fileServer.MethodName;
                client.Connect();
                return client.DelFile(filePath);
            }
        }
    }
    /// <summary>
    /// 文件传输配置文件
    /// </summary>
    public class NetFileTransferServerConfig
    {
        static NetFileTransferServerConfig()
        {
            _LoadConfig();
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        private static void _LoadConfig()
        {
            string tSer = YueRenConfig.Instance.AppSetting.ChapterImageNetFileTransferServer;
            if (!string.IsNullOrEmpty(tSer))
            {
                try
                {
                    string[] tSerArray = tSer.Split(';');
                    string[] tSerItemArray = null;
                    ServerList = new FileServer[tSerArray.Length];
                    int iIndex = 0;
                    foreach (var item in tSerArray)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            tSerItemArray = item.Split(',');
                            ServerList[iIndex] = new FileServer()
                            {
                                SvrID = iIndex,
                                SvrUrl = tSerItemArray[0],
                                MethodName = tSerItemArray[1]
                            };
                            iIndex++;
                        }
                    }
                }
                catch
                {
                    LogHelper.Error("Config.ChapterImageNetFileTransferServer 配置有问题");
                    throw new ArgumentException("Config.ChapterImageNetFileTransferServer 配置有问题");
                }
            }
            else
            {
                LogHelper.Error("Config.ChapterImageNetFileTransferServer 必须配置");
                throw new ArgumentNullException("Config.ChapterImageNetFileTransferServer 必须配置");
            }
        }
        /// <summary>
        /// 文件传输 服务器地址
        /// </summary>
        public static FileServer[] ServerList { get; set; }
        /// <summary>
        /// 文件传输 服务器地址
        /// </summary>
        public class FileServer
        {
            /// <summary>
            /// 服务器ID
            /// </summary>
            public int SvrID { get; set; }
            /// <summary>
            /// 当前服务器HTTP地址
            /// </summary>
            public string SvrUrl { get; set; }
            /// <summary>
            /// 当前服务器 Remoting 地址
            /// </summary>
            public string MethodName { get; set; }
        }
        #region 文件传输静态方法
        /// <summary>
        /// 得到字符串的HASH
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static uint Hash(string key)
        {
            return BitConverter.ToUInt32(new ModifiedFNV1_32().ComputeHash(Encoding.UTF8.GetBytes(key)), 0);
        }
        /// <summary>
        /// 文件服务器哈希算法
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        private static int GetFileServerIndex(long fileID)
        {
            int _index;
            int _length = NetFileTransferServerConfig.ServerList.Length;
            long _offIndex = fileID % _length;
            _index = (int)_offIndex;
            return _index;
        }
        /// <summary>
        /// 根据文件保存路径名得到保存文件服务器路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int GetFileServerIndexByFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return 0;
            return GetFileServerIndex(Hash(fileName));
        }
        /// <summary>
        /// 根据文件保存路径名得到保存文件服务器
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static NetFileTransferServerConfig.FileServer GetFileServerByFileName(string fileName)
        {
            return NetFileTransferServerConfig.ServerList[GetFileServerIndexByFileName(fileName)];
        }
        /// <summary>
        /// 根据httpPath 返回文件服务器, 默认返回 0 索引的服务器
        /// </summary>
        /// <param name="httpPath"></param>
        /// <returns></returns>
        public static NetFileTransferServerConfig.FileServer GetGetFileServerByHttpPath(string httpPath)
        {
            int serverIndex = 0;
            if (string.IsNullOrEmpty(httpPath))
                serverIndex = 0;
            else if (!httpPath.ToLower().StartsWith("http://"))
                serverIndex = 0;
            else
            {
                if (!httpPath.EndsWith("/"))
                    httpPath += "/";
                NetFileTransferServerConfig.FileServer fileServer =
                    Array.Find(NetFileTransferServerConfig.ServerList, x => x.SvrUrl == httpPath);
                if (fileServer == null)
                    return NetFileTransferServerConfig.ServerList[0];
                else
                    return fileServer;
            }
            return NetFileTransferServerConfig.ServerList[serverIndex];
        }
        #endregion
        #region HASH函数相关
        /// <summary>
        /// 修改版FNV1_32 哈希函数
        /// </summary>
        /// <remarks>
        /// FNV1_32 哈希函数
        /// Fowler-Noll-Vo hash, variant 1, 32-bit version.
        /// http://www.isthe.com/chongo/tech/comp/fnv/
        /// 修改版FNV1_32 哈希函数
        /// Modified Fowler-Noll-Vo hash, 32-bit version.
        /// http://home.comcast.net/~bretm/hash/6.html
        /// </remarks>
        class ModifiedFNV1_32 : HashAlgorithm
        {
            private static readonly uint FNV_prime = 16777619;
            private static readonly uint offset_basis = 2166136261;

            protected uint hash;

            public ModifiedFNV1_32()
            {
                HashSizeValue = 32;
            }

            public override void Initialize()
            {
                hash = offset_basis;
            }

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                int length = ibStart + cbSize;
                for (int i = ibStart; i < length; i++)
                {
                    hash = (hash * FNV_prime) ^ array[i];
                }
            }

            protected override byte[] HashFinal()
            {
                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return BitConverter.GetBytes(hash);
            }
        }
        #endregion
    }
    /// <summary>
    /// 文件传输客户端
    /// </summary>
    public class NetFileTransferClient : IDisposable
    {
        #region 文件客户端
        private YueRen.Common.Utility.NetFileTransfer nft = null;
        private bool _Active = false;
        private string _NoticeRegisterMethodName = "tcp://localhost:8085/FileService";

        /// <summary>
        /// 
        /// </summary>
        public NetFileTransferClient()
            : base()
        {
        }

        /// <summary>
        /// 绑定注册方法名
        /// </summary>
        public string NoticeRegisterMethodName
        {
            get
            {
                return this._NoticeRegisterMethodName;
            }
            set
            {
                this._NoticeRegisterMethodName = value;
            }
        }

        /// <summary>
        /// 获取激活状态
        /// </summary>
        public bool Active
        {
            get
            {
                return this._Active;
            }
        }
        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <returns>状态</returns>
        public bool Connect()
        {
            try
            {
                nft = (YueRen.Common.Utility.NetFileTransfer)Activator.GetObject(typeof(YueRen.Common.Utility.NetFileTransfer), _NoticeRegisterMethodName);
                if (nft != null && nft.ToString().Length > 1)
                {
                    this._Active = true;
                    return true;
                }
                else
                {
                    this._Active = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                this._Active = false;
                LogHelper.Error("NetFileTransferClient-Connect-异常:ex:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 停止连接
        /// </summary>
        public void Disconnection()
        {
            nft = null;
            _Active = false;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="RemoteFilePath">文件路径</param>
        /// <returns>文件数组</returns>
        public byte[] GetFileBytes(string RemoteFilePath)
        {
            if (!_Active) return null;
            try
            {
                return nft.GetFileBytes(RemoteFilePath);
            }
            catch (Exception ex)
            {
                LogHelper.Error("NetFileTransferClient-GetFileBytes-异常:ex:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取文件，并保存
        /// </summary>
        /// <param name="RemoteFilePath">远程文件路径</param>
        /// <param name="LocalSavePath">本地保存路径</param>
        /// <returns>状态</returns>
        public bool GetFile(string RemoteFilePath, string LocalSavePath)
        {
            if (!_Active) return true;
            try
            {
                byte[] filebytes = nft.GetFileBytes(RemoteFilePath);
                if (filebytes != null)
                {
                    FileStream fs = new FileStream(LocalSavePath, FileMode.CreateNew, FileAccess.Write, FileShare.Write);
                    fs.Write(filebytes, 0, filebytes.Length);
                    fs.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("NetFileTransferClient-GetFile-异常:ex:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="fileBytes">文件数组</param>
        /// <param name="RemoteSavePath">保存路径</param>
        /// <param name="SizeList"></param>
        /// <returns>保存状态</returns>
        public bool SendFileBytes(byte[] fileBytes, string RemoteSavePath, params string[] SizeList)
        {
            if (!_Active) return false;
            try
            {
                return nft.SendFileBytes(fileBytes, RemoteSavePath, SizeList);
            }
            catch (Exception ex)
            {
                this._Active = false;
                LogHelper.Error("NetFileTransferClient-SendFileBytes-异常:ex:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="ImgFile"></param>
        /// <param name="sImgPath"></param>
        /// <param name="PointX"></param>
        /// <param name="PointY"></param>
        /// <param name="CutWidth"></param>
        /// <param name="CutHeight"></param>
        /// <param name="SizeList"></param>
        /// <returns></returns>
        public bool CutImg(string ImgFile, string sImgPath, int PointX, int PointY, int CutWidth, int CutHeight, params string[] SizeList)
        {
            try
            {
                return nft.CutImg(ImgFile, sImgPath, PointX, PointY, CutWidth, CutHeight, SizeList);
            }
            catch (Exception ex)
            {
                this._Active = false;
                YueRen.Common.Log.LogHelper.Error("NetFileTransferClient-CutImg-异常:ex:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="SizeList"></param>
        /// <returns></returns>
        public bool ResizeImg(string filePath, string[] SizeList)
        {
            throw new NotImplementedException("ResizeImg");
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool DelFile(string filePath)
        {
            try
            {
                return nft.DelFile(filePath);
            }
            catch (Exception ex)
            {
                this._Active = false;
                YueRen.Common.Log.LogHelper.Error("NetFileTransferClient-DelFile-异常:ex:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 发送文件，并保存到主机
        /// </summary>
        /// <param name="LocalFilePath">本地文件</param>
        /// <param name="RemoteSavePath">远端保存路径</param>
        /// <returns>是否成功</returns>
        public bool SendFile(string LocalFilePath, string RemoteSavePath)
        {
            if (!_Active) return false;
            try
            {
                if (!File.Exists(LocalFilePath)) return false;

                FileStream fs = new FileStream(LocalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                return nft.SendFileBytes(buffer, RemoteSavePath);
            }
            catch (Exception ex)
            {
                this._Active = false;
                LogHelper.Error("NetFileTransferClient-SendFile-异常:ex:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 删除图片(图片重命名，只重命名原图)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tmpExt"></param>
        /// <returns></returns>
        public void DeleteImg(string filePath, string tmpExt)
        {
            throw new NotImplementedException("ResizeImg");
        }

        /// <summary>
        /// 恢复图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tmpExt"></param>
        /// <returns></returns>
        public void RecoverImg(string filePath, string tmpExt)
        {
            throw new NotImplementedException("ResizeImg");
        }
        #endregion

        #region IDisposable 成员
        private bool disposed = false;
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Disconnection();
                }
            }
            disposed = true;
        }
        /// <summary>
        /// 
        /// </summary>
        ~NetFileTransferClient()
        {
            Dispose(false);
        }
        #endregion
    }

}

namespace YueRen.Common.Utility
{
    /// <summary>
    /// 文件传输接口
    /// </summary>
    /// <remarks>
    /// INFO 本处没有引用 Snda.Qidian.SNS.Utility 配件,Remoting时需要接口匹配
    /// </remarks>
    internal class NetFileTransfer : MarshalByRefObject
    {

        public NetFileTransfer()
            : base()
        {
        }

        /// <summary>
        /// 获取文件的数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>数组（默认null）</returns>
        public byte[] GetFileBytes(string filePath)
        {
            throw new NotImplementedException();
        }

        //裁剪图片
        public bool CutImg(string ImgFile, string sImgPath, int PointX, int PointY, int CutWidth, int CutHeight, params string[] SizeList)
        {
            throw new NotImplementedException();
        }

        //删除文件
        public bool DelFile(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除图片(图片重命名，只重命名原图)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tmpExt"></param>
        /// <returns></returns>
        public void DeleteImg(string filePath, string tmpExt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 恢复图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tmpExt"></param>
        /// <returns></returns>
        public void RecoverImg(string filePath, string tmpExt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="savePath">保存路径</param>
        /// <param name="SizeList"></param>
        /// <returns>状态</returns>
        public bool SendFileBytes(byte[] fileBytes, string savePath, params string[] SizeList)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="SizeList"></param>
        /// <returns></returns>
        public bool ResizeImg(string filePath, string[] SizeList)
        {
            throw new NotImplementedException();
        }
    }
}


