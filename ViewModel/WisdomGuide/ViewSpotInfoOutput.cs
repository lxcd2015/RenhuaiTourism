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
        /// 小图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        ///// <summary>
        ///// 景点描述（目前用于距离描述）
        ///// </summary>
        //public string ViewSpotDescribe { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// 距离描述
        /// </summary>
        public string DistanceDescription { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        ///联系电话 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否有更多视频
        /// </summary>
        public bool HasMoreView { get; set; }

        /// <summary>
        /// 其中一个视频信息
        /// </summary>
        public ViewSpotVideoDto FirstVoice { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }

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
        public List<ViewSpotVideoDto> VoiceList { get; set; }
    }
}
