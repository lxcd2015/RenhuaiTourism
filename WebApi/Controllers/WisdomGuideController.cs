using BLL;
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
    public class WisdomGuideController : ApiController
    {
        private readonly WisdomGuideService wisdomGuideService;

        public WisdomGuideController()
        {
            wisdomGuideService = new WisdomGuideService();
        }

        /// <summary>
        /// 修改地图地址
        /// </summary>
        public void ChangeMap(ChangeMapInput input) {
            wisdomGuideService.ChangeMap(input);
        }

        /// <summary>
        /// 添加景点
        /// </summary>
        public void AddViewSpot(AddOrEditViewSpotDto input) {
            wisdomGuideService.AddViewSpot(input);
        }

        /// <summary>
        /// 编辑景点
        /// </summary>
        public void EditViewSpot(AddOrEditViewSpotDto input) {
            wisdomGuideService.EditViewSpot(input);
        }

        /// <summary>
        /// 获取用于编辑景点的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public AddOrEditViewSpotDto GetViewSpotForEdit(RTEntity<int> input) {
            return wisdomGuideService.GetViewSpotForEdit(input);
        }

        /// <summary>
        /// 获取地图景点简介信息
        /// </summary>
        public ViewSpotInfoOutput ViewSpotInfo(ViewSpotInfoInput input) {
           return wisdomGuideService.ViewSpotInfo(input);
        }

        /// <summary>
        /// 获取景点详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ViewSpotDetailOutput ViewSpotDetail(RTEntity<int> input) {
            return wisdomGuideService.ViewSpotDetail(input);
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        [HttpPost]
        public List<ViewSpotVideoDto> GetVideoList(RTEntity<int> input) {
            return wisdomGuideService.GetVideoList(input);
        }
    }

}
