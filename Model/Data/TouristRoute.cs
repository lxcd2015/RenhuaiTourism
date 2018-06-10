using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    /// <summary>
    /// 旅游路线
    /// </summary>
    public class TouristRoute
    {
        [Key]
        public int Id { get; set; }

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

        ///// <summary>
        ///// 描述信息
        ///// </summary>
        //public string Content { get; set; }
    }
}
