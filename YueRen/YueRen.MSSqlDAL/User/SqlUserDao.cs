using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDAO;
using YueRen.Entity;
using YueRen.IDAL.User;

namespace YueRen.MSSqlDAL.User
{
    public class SqlUserDao : UserDao
    {
        public SqlUserDao(){ }

        public override YueRenUserEntity GetUser(int userID)
        {
            YueRenUserEntity user = null;
            SqlParameter[] paramArray = new SqlParameter[]{
		        new SqlParameter("UserID",userID)
	        };

            var resRecord = 0;
            try
            {
                var reader = SqlHelper.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, "exec procYueRenUserGet", paramArray);
                while (reader.Read())
                {
                    if (resRecord == 0)
                    {
                        user = new YueRenUserEntity();
                        user.Address = Field.GetString(reader, "Address");
                        user.Age = Field.GetInt(reader, "Age");
                        user.City = Field.GetInt(reader, "City");
                        user.Country = Field.GetInt(reader, "Country");
                        user.District = Field.GetString(reader, "District");
                        user.Gender = Field.GetInt(reader, "Gender");
                        user.Head_Image = Field.GetString(reader, "Head_Image");
                        user.Mail = Field.GetString(reader, "Mail");
                        user.Name = Field.GetString(reader, "Name");
                        user.NickName = Field.GetString(reader, "NickName");
                        user.Province = Field.GetInt(reader, "Province");
                        user.UserID = userID;
                        resRecord++;
                    }
                }
            }
            catch (DataException e)
            {
                throw new DataException("获取作者(authorId=" + userID + ")信息失败", e);
            }

            return user;
        }

        public override int UpdateUser(Entity.YueRenUserEntity user)
        {
            throw new NotImplementedException();
        }

        public override int InsertUser(Entity.YueRenUserEntity user)
        {
            throw new NotImplementedException();
        }

        public override int InsertUserInstrumentType(List<int> types)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetUserInstumentType(List<int> types)
        {
            throw new NotImplementedException();
        }

        public override Entity.User.UserToken GetUserToken(long userId)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserValid(long userId, string passWord)
        {
            throw new NotImplementedException();
        }
    }
}
