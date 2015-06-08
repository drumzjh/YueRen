using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.MSSqlDAL
{
    public class Field
    {
        public const int EXP_RETURN = -1;
        private const bool NULL_BOOL = false;
        private static readonly DateTime NULL_DATETIME = new DateTime(0L);
        [DecimalConstant(2, 0, (uint)0, (uint)0, (uint)0)]
        private static readonly decimal NULL_DECIMAL = 0.00M;
        private const float NULL_FLOAT = 0f;
        private const short NULL_INT16 = 0;
        private const int NULL_INT32 = 0;
        private const long NULL_INT64 = 0L;
        public const int NULL_PARAM_INT = 0;
        public const string NULL_PRARAM_STR = "";
        private const string NULL_STRING = "";

        private Field()
        {
        }

        public static bool GetBoolean(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return false;
            }
            return rec.GetBoolean(fldnum);
        }

        public static bool GetBoolean(IDataRecord rec, string fldname)
        {
            return GetBoolean(rec, rec.GetOrdinal(fldname));
        }

        public static byte GetByte(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return 0;
            }
            return rec.GetByte(fldnum);
        }

        public static byte GetByte(IDataRecord rec, string fldname)
        {
            return GetByte(rec, rec.GetOrdinal(fldname));
        }

        public static DateTime GetDateTime(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return NULL_DATETIME;
            }
            return rec.GetDateTime(fldnum);
        }

        public static DateTime GetDateTime(IDataRecord rec, string fldname)
        {
            return GetDateTime(rec, rec.GetOrdinal(fldname));
        }

        public static decimal GetDecimal(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return 0.00M;
            }
            return rec.GetDecimal(fldnum);
        }

        public static decimal GetDecimal(IDataRecord rec, string fldname)
        {
            return GetDecimal(rec, rec.GetOrdinal(fldname));
        }

        public static double GetDouble(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return 0.0;
            }
            return rec.GetDouble(fldnum);
        }

        public static double GetDouble(IDataRecord rec, string fldname)
        {
            return GetDouble(rec, rec.GetOrdinal(fldname));
        }

        public static float GetFloat(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return 0f;
            }
            return rec.GetFloat(fldnum);
        }

        public static float GetFloat(IDataRecord rec, string fldname)
        {
            return GetFloat(rec, rec.GetOrdinal(fldname));
        }

        public static Guid GetGuid(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return Guid.Empty;
            }
            return rec.GetGuid(fldnum);
        }

        public static Guid GetGuid(IDataRecord rec, string fldname)
        {
            return GetGuid(rec, rec.GetOrdinal(fldname));
        }

        public static int GetInt(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return 0;
            }
            return rec.GetInt32(fldnum);
        }

        public static int GetInt(IDataRecord rec, string fldname)
        {
            return GetInt(rec, rec.GetOrdinal(fldname));
        }

        public static short GetInt16(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return 0;
            }
            return rec.GetInt16(fldnum);
        }

        public static short GetInt16(IDataRecord rec, string fldname)
        {
            return GetInt16(rec, rec.GetOrdinal(fldname));
        }

        public static int GetInt32(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return 0;
            }
            return rec.GetInt32(fldnum);
        }

        public static int GetInt32(IDataRecord rec, string fldname)
        {
            return GetInt32(rec, rec.GetOrdinal(fldname));
        }

        public static long GetInt64(IDataRecord rec, int fldnum)
        {
            if (!rec.IsDBNull(fldnum))
            {
                object obj2 = rec.GetInt64(fldnum);
                if (obj2 is long)
                {
                    return rec.GetInt64(fldnum);
                }
            }
            return 0L;
        }

        public static long GetInt64(IDataRecord rec, string fldname)
        {
            return GetInt64(rec, rec.GetOrdinal(fldname));
        }

        public static double GetOutPutParam(IDataParameter param, double defaultValue)
        {
            if ((!(param.Value is DBNull) && (param.Value != null)) && (param.Value != DBNull.Value))
            {
                return (double)param.Value;
            }
            return defaultValue;
        }

        public static int GetOutPutParam(IDataParameter param, int defaultValue)
        {
            if ((!(param.Value is DBNull) && (param.Value != null)) && (param.Value != DBNull.Value))
            {
                return (int)param.Value;
            }
            return defaultValue;
        }

        public static long GetOutPutParam(IDataParameter param, long defaultValue)
        {
            long result = -1L;
            if (((param.Value is DBNull) || (param.Value == null)) || (param.Value == DBNull.Value))
            {
                return defaultValue;
            }
            if (!long.TryParse(param.Value.ToString(), out result))
            {
                result = 0L;
            }
            return result;
        }

        public static string GetOutPutParam(IDataParameter param, string defaultValue)
        {
            if (!(param.Value is DBNull) && (param.Value != null))
            {
                return param.Value.ToString();
            }
            return defaultValue;
        }

        public static DateTime GetOutPutParam(IDataParameter param, string defaultValue, bool isDate)
        {
            if (!(param.Value is DBNull) && (param.Value != null))
            {
                return DateTime.Parse(param.Value.ToString());
            }
            return DateTime.MinValue;
        }

        public static int GetReturnPram(IDataParameter param)
        {
            if ((!(param.Value is DBNull) && (param.Value != null)) && (param.Value != DBNull.Value))
            {
                return (int)param.Value;
            }
            return -1;
        }

        public static string GetString(IDataRecord rec, int fldnum)
        {
            if (rec.IsDBNull(fldnum))
            {
                return "";
            }
            return rec.GetString(fldnum);
        }

        public static string GetString(IDataRecord rec, string fldname)
        {
            return GetString(rec, rec.GetOrdinal(fldname));
        }
    }
}

