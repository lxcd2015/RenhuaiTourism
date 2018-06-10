using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RTException: Exception
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; } = 1;

        public RTException():base()
        {
        }
 
        public RTException(string message):base(message)
        {
        }

        public RTException(string message, Exception innerException):base(message,innerException)
        {
        }
    }
}
