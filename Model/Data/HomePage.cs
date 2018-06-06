using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class HomePage
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 第一张图片地址
        /// </summary>
        public string FirstImgUrl { get; set; }

        /// <summary>
        /// 第二张图片地址
        /// </summary>
        public string SecondImgUrl { get; set; }

        /// <summary>
        /// 第三张图片地址
        /// </summary>
        public string ThirdImgUrl { get; set; }
    }
}
