using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WisdomGuide
{
    public class AddOrEditViewSpotDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 所关联智慧导览Id
        /// </summary>
        public int WisdomGuideId { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string ViewSpotName { get; set; }

        ///// <summary>
        ///// 景点描述（目前用于距离描述）
        ///// </summary>
        //public string ViewSpotDescribe { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 小图地址
        /// </summary>
        public string SmallImgUrl { get; set; }

        /// <summary>
        /// 大图地址
        /// </summary>
        public string BigImgUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<string> Contents { get; set; }

        /// <summary>
        /// 视频列表
        /// </summary>
        public List<ViewSpotVideoDto> VideoList { get; set; }
    }
}
