using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    /// <summary>
    /// 详情表
    /// </summary>
    public class Detail
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 模块类型(1：酒都简介；2：智慧导览；3：旅游信息；4：精品线路；5：痛客行)
        /// </summary>
        public ModularType ModularType { get; set; }

        /// <summary>
        /// 分类类型（如旅游信息下的：酒店）
        /// </summary>
        public int Classify { get; set; }

        /// <summary>
        /// 具体项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 详情图片路径
        /// </summary>
        public string ImgUrl{get;set;}
    }
}
