using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel.Common;

namespace WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 操作异常
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected GeneralResult RTExceptionHandle(RTException e)
        {
            return new GeneralResult
            {
                State = 1,
                Msg = e.Message
            };
        }

        /// <summary>
        /// 系统内部异常
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected GeneralResult ExceptionHandle(Exception e)
        {
            return new GeneralResult
            {
                State = 2,
                Msg = "系统内部异常"
            };
        }
    }
}