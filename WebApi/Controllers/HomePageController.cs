﻿using BLL;
using System.Web.Http;
using ViewModel.HomePage;

namespace WebApi.Controllers
{
    /// <summary>
    /// 首页图片服务
    /// </summary>
    public class HomePageController : ApiController
    {
        private HomePageService bll = new HomePageService();

        /// <summary>
        /// 编辑首页图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Edit(HomePageInput input)
        {
            return bll.Edit(input);
        }

        /// <summary>
        /// 获取首页图片
        /// </summary>
        /// <returns></returns>
        public HomePageInput Detail()
        {
            return bll.Detail();
        }
    }
}