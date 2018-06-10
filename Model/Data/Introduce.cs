using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class Introduce
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 标题（如【酒都简介】）
        /// </summary>
        public string Title { get; set; }

        ///// <summary>
        ///// 图片
        ///// </summary>
        //public string ImgUrl { get; set; }

        ///// <summary>
        ///// 内容
        ///// </summary>
        //public string Content { get; set; } 
    }
}
