using BLL;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel.Common;
using ViewModel.TouristInformation;

namespace WebApi.Controllers
{
    public class TouristInformationController : ApiController
    {
        private readonly TouristInformationService bll;

        public TouristInformationController()
        {
            bll = new TouristInformationService();
        }

        /// <summary>
        /// 添加旅游信息
        /// </summary>
        /// <param name="input"></param>
        public void Add(AddOrEditTouristInformation input)
        {
            bll.Add(input);
        }

        /// <summary>
        /// 编辑旅游信息
        /// </summary>
        /// <param name="input"></param>
        public void Edit(AddOrEditTouristInformation input)
        {
            bll.Edit(input);
        }

        /// <summary>
        /// 删除旅游信息
        /// </summary>
        /// <param name="input"></param>
        public void Delete(RTEntity<int> input)
        {
            bll.Delete(input);
        }

        /// <summary>
        /// 获取旅游信息用于编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public AddOrEditTouristInformation GetInformationForEdit(RTEntity<int> input)
        {
            return bll.GetInformationForEdit(input);
        }

        /// <summary>
        /// 获取旅游信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public List<InformationForView> GetList(RTEntity<TouristInformationType> input)
        {
            return bll.GetList(input);
        }

        /// <summary>
        /// 获取酒店详细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public InformationDetail GetDetail(RTEntity<int> input)
        {
            return bll.GetDetail(input);
        }
    }
}