using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WisdomGuide
{
    public class ViewSpotVideoDto
    {

        /// <summary>
        /// 景点Id
        /// </summary>
        public int WisdomGuideViewSpotId { get; set; }

        /// <summary>
        /// 视频名称
        /// </summary>
        public string VideoName { get; set; }

        /// <summary>
        /// 视频封面图片路径
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 视频播放地址
        /// </summary>
        public string VideoUrl { get; set; }
    }
}
