using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.TouristRoute
{
    /// <summary>
    /// 添加或编辑旅游路线信息
    /// </summary>
    public class AddOrEditTouristRouteInput
    {
        public int? Id { get; set; }
        
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 所需天数
        /// </summary>
        public int NeedDays { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public List<string> Contents { get; set; }
    }
}
