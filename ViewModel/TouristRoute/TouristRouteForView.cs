using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.TouristRoute
{
    /// <summary>
    /// 旅游路线信息
    /// </summary>
    public class TouristRouteForView
    {
        public int Id { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Content { get; set; }
    }
}
