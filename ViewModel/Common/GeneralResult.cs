using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Common
{
    public class GeneralResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }
    }
}
