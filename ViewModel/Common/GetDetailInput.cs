using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Common
{
    public class GetDetailInput
    {

        /// <summary>
        /// 分类类型（如旅游信息下的：酒店）
        /// </summary>
        public int Classify { get; set; }

        /// <summary>
        /// 具体项目Id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
