using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel.Common;
using ViewModel.Message;

namespace WebApi.Controllers
{
    /// <summary>
    /// 投诉建议服务
    /// </summary>
    public class MessageController : BaseApiController
    {
        private readonly MessageService bll;

        public MessageController()
        {
            bll = new MessageService();
        }

        /// <summary>
        /// 添加投诉或建议
        /// </summary>
        /// <param name="input"></param>
        public GeneralResult Add(AddMessageInput input)
        {
            var result = new GeneralResult();
            try
            {
                bll.Add(input);
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
        /// 获取投诉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetComplaintList(DateTimeCondition input)
        {
            var result = new GeneralResult();
            try
            {
                result.Data= bll.GetComplaintList(input);
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
        /// 获取建议列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetAdviseList(DateTimeCondition input)
        {
            var result = new GeneralResult();
            try
            {
                result.Data= bll.GetAdviseList(input);
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