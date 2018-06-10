using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Web.Http;
using ViewModel.Common;
using ViewModel.TouristRoute;

namespace WebApi.Controllers
{
    public class TouristRouteController : BaseApiController
    {
        private readonly TouristRouteService bll;

        public TouristRouteController()
        {
            bll = new TouristRouteService();
        }

        /// <summary>
        /// 添加旅游线路
        /// </summary>
        /// <param name="input"></param>
        public GeneralResult Add(AddOrEditTouristRouteInput input) {
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
        /// 编辑旅游线路
        /// </summary>
        /// <param name="input"></param>
        public GeneralResult Edit(AddOrEditTouristRouteInput input) {
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
        /// 获取旅游线路列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetList() {
            var result = new GeneralResult();
            try
            {
                result.Data= bll.GetList();
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
        /// 根据Id删除旅游线路
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        public GeneralResult Delete(RTEntity<int> input) {
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
        /// 根据Id获取单个旅游线路
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetTouristRoute(RTEntity<int> input) {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.GetTouristRoute(input);
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