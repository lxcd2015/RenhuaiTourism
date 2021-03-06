﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewSpotDetailOutput
    {

        /// <summary>
        /// 图片地址
        /// </summary>
        public string BigImgUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<string> Contents { get; set; }
    }
}
