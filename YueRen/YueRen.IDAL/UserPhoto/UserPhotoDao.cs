using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YueRen.Common.Config;
using YueRen.Entity.Photo;

namespace YueRen.IDAL.UserPhoto
{
    public abstract class UserPhotoDao:BaseDao
    {
        public override string ConnectionString
        {
            get { return YueRenConfig.Instance.AppSetting.ConnectionString; }
        }

         /// <summary>
        /// 构造函数，实现adoHelper初始化
        /// </summary>
        public UserPhotoDao()
        {
            this.adoHelper = base.GetDefaultProvider(typeof(UserPhotoDao));
        }

        /// <summary>
        /// 根据相册ID获取相册所有信息
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        public abstract UserAlbumEntity GetUserAlbum(long albumID);

        /// <summary>
        /// 根据用户ID获取所有相册信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public abstract List<UserAlbumEntity> GetUserAlbums(long userID);

        /// <summary>
        /// 根据相册ID获取所有照片地址
        /// </summary>
        /// <param name="albumID">相册ID</param>
        /// <returns></returns>
        public abstract List<UserPhotoEntity> GetUserPhotos(long albumID);

        /// <summary>
        /// 用户照片实体
        /// </summary>
        /// <param name="userPhotoEntity">用户照片实体</param>
        /// <returns></returns>
        public abstract int InsertPhoto(UserPhotoEntity userPhotoEntity);

        /// <summary>
        /// 照片Id
        /// </summary>
        /// <param name="PhotoID">照片ID</param>
        /// <returns></returns>
        public abstract int DeletePhoto(long PhotoID);

        /// <summary>
        /// 删除相册
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public abstract int DeleteAlbum(long albumId);
    }
}
