using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    /// <summary>
    /// 智慧导览地图
    /// </summary>
    public class WisdomGuideMap
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 所关联智慧导览Id
        /// </summary>
        public int WisdomGuideId { get; set; }

        /// <summary>
        /// 地图地址
        /// </summary>
        public string ImgUrl { get; set; }
    }
}
