using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ViewModel;
using ViewModel.Common;
using ViewModel.WisdomGuide;

namespace WebApi.Controllers
{
    /// <summary>
    /// 智慧导览服务
    /// </summary>
    public class WisdomGuideController : BaseApiController
    {
        private readonly WisdomGuideService bll;

        public WisdomGuideController()
        {
            bll = new WisdomGuideService();
        }

        /// <summary>
        /// 修改地图地址
        /// </summary>
        public GeneralResult ChangeMap(ChangeMapInput input) {
            var result = new GeneralResult();
            try
            {
                bll.ChangeMap(input);
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
        /// 获取地图及景点列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetMapInofo()
        {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.GetMapInofo();
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
        /// 添加景点
        /// </summary>
        public GeneralResult AddViewSpot(AddOrEditViewSpotDto input) {
            var result = new GeneralResult();
            try
            {
                bll.AddViewSpot(input);
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
        /// 编辑景点
        /// </summary>
        public GeneralResult EditViewSpot(AddOrEditViewSpotDto input) {
            var result = new GeneralResult();
            try
            {
                bll.EditViewSpot(input);
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
        /// 获取用于编辑景点的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public GeneralResult GetViewSpotForEdit(RTEntity<int> input) {
            var result = new GeneralResult();
            try
            {
                result.Data= bll.GetViewSpotForEdit(input);
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
        /// 获取地图景点简介信息
        /// </summary>
        public GeneralResult ViewSpotInfo(ViewSpotInfoInput input) {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.ViewSpotInfo(input);
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
        /// 获取景点详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GeneralResult ViewSpotDetail(RTEntity<int> input) {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.ViewSpotDetail(input);
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
        /// 获取视频列表
        /// </summary>
        [HttpPost]
        public GeneralResult GetVideoList(RTEntity<int> input) {
            var result = new GeneralResult();
            try
            {
                result.Data = bll.GetVideoList(input);
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
