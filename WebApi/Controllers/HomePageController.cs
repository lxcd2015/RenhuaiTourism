using BLL;
using Common;
using System;
using System.Web.Http;
using ViewModel.Common;
using ViewModel.HomePage;

namespace WebApi.Controllers
{
    /// <summary>
    /// 首页图片服务
    /// </summary>
    public class HomePageController : BaseApiController
    {
        private HomePageService bll = new HomePageService();

        /// <summary>
        /// 编辑首页图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GeneralResult Edit(HomePageInput input)
        {
            var result = new GeneralResult();
            try
            {
                bll.Edit(input);
                result.State = 0;
                result.Msg = "操作成功";
            }
            catch (RTException e)
            {
                result = RTExceptionHandle(e);
            }
            catch (Exception e1)
            {
                result = ExceptionHandle(e1);
            }
            return result;
        }

        /// <summary>
        /// 获取首页图片
        /// </summary>
        /// <returns></returns>
        public GeneralResult Detail()
        {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.Detail();
                result.State = 0;
                result.Msg = "操作成功";
            }
            catch (RTException e)
            {
                result = RTExceptionHandle(e);
            }
            catch (Exception e1)
            {
                result = ExceptionHandle(e1);
            }
            return result;
        }
    }
}