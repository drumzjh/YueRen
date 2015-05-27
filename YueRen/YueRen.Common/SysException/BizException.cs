using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.SysException
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BizException : BaseException
    {

        public BizException()
            : base(null)
        {


        }

        public BizException(int code, string message)
            : base(code, message)
        { }



        public BizException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public BizException(int code, string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
