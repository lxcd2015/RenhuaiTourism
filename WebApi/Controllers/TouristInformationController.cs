using BLL;
using Common;
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
    public class TouristInformationController : BaseApiController
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
        public GeneralResult Add(AddOrEditTouristInformation input)
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
        /// 编辑旅游信息
        /// </summary>
        /// <param name="input"></param>
        public GeneralResult Edit(AddOrEditTouristInformation input)
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
        /// 删除旅游信息
        /// </summary>
        /// <param name="input"></param>
        public GeneralResult Delete(RTEntity<int> input)
        {
            var result = new GeneralResult();
            try
            {
                bll.Delete(input);
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
        /// 获取旅游信息用于编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetInformationForEdit(RTEntity<int> input)
        {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.GetInformationForEdit(input);
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
        /// 获取旅游信息列表（1：酒店；2：餐饮；3：特产；4：旅行社；5：车辆服务；6：旅游购物）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetList(GetInformationListInput input)
        {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.GetList(input);
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
        /// 获取酒店详细信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetDetail(GetInformationDetail input)
        {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.GetDetail(input);
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