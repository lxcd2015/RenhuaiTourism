using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WisdomGuide
{
    public class ViewSpotInfoOutput
    {
        /// <summary>
        /// 地图图片地址
        /// </summary>
        public string MapImgUrl { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string ViewSpotName { get; set; }

        /// <summary>
        /// 景点描述（目前用于距离描述）
        /// </summary>
        public string ViewSpotDescribe { get; set; }

        /// <summary>
        /// 是否有更多视频
        /// </summary>
        public bool HasMoreView { get; set; }

        /// <summary>
        /// 其中一个视频信息
        /// </summary>
        public ViewSpotVideoDto FirstVideo { get; set; }
    }
}
