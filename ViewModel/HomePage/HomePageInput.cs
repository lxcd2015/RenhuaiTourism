using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel.HomePage
{
    public class HomePageInput
    {

        /// <summary>
        /// 第一个位置图片
        /// </summary>
        public string FirstImgUrl { get; set; }

        /// <summary>
        /// 第二个位置图片（轮播图片）
        /// </summary>
        public List<string> SecondImgUrlList { get; set; }

        /// <summary>
        /// 第三个位置图片
        /// </summary>
        public string ThirdImgUrl { get; set; }
    }
}