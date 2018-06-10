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
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        ///// <summary>
        ///// 内容
        ///// </summary>
        //public string Content { get; set; }
    }
}
