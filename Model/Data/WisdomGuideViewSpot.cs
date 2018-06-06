using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class WisdomGuideViewSpot
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 所关联智慧导览Id
        /// </summary>
        public int WisdomGuideId { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string ViewSpotName { get; set; }

        /// <summary>
        /// 景点描述（目前用于距离描述）
        /// </summary>
        public string ViewSpotDescribe { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
