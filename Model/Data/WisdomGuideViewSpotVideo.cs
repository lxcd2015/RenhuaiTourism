using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class WisdomGuideViewSpotVideo
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public int WisdomGuideViewSpotId{get;set;}

        /// <summary>
        /// 音频名称
        /// </summary>
        public string VoiceName { get; set; }

        /// <summary>
        /// 视频封面图片路径
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 音频播放地址
        /// </summary>
        public string VoiceUrl { get; set; }
    }
}
