using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel.Message;

namespace WebApi.Controllers
{
    /// <summary>
    /// 投诉建议服务
    /// </summary>
    public class MessageController : ApiController
    {
        private readonly MessageService messageService;

        public MessageController()
        {
            messageService = new MessageService();
        }

        /// <summary>
        /// 添加投诉或建议
        /// </summary>
        /// <param name="input"></param>
        public void Add(AddMessageInput input)
        {
            messageService.Add(input);
        }

        /// <summary>
        /// 获取投诉列表
        /// </summary>
        /// <returns></returns>
        public List<MessageForView> GetComplaintList()
        {
            return messageService.GetComplaintList();
        }

        /// <summary>
        /// 获取建议列表
        /// </summary>
        /// <returns></returns>
        public List<MessageForView> GetAdviseList()
        {
            return messageService.GetAdviseList();
        }
    }
}