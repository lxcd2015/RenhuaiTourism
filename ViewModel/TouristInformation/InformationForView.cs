﻿using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.TouristInformation
{
    public class InformationForView
    {
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string SmallImgUrl { get; set; }

        /// <summary>
        /// 距离值（单位米）
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// 距离描述（单位米/千米）
        /// </summary>
        public string DistanceDescription { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 价格描述
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
    }
}
