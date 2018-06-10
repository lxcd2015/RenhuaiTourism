using BLL;
using System;
using System.Web.Http;
using ViewModel.Common;
using ViewModel.Introduce;
using Common;

namespace WebApi.Controllers
{
    /// <summary>
    /// 仁怀简介服务
    /// </summary>
    public class IntroduceController : BaseApiController
    {
        private readonly IntroduceService bll;

        public IntroduceController()
        {
            bll = new IntroduceService();
        }

        /// <summary>
        /// 编辑酒都简介
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GeneralResult Edit(IntroduceInput input)
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
        /// 获取酒都简介
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