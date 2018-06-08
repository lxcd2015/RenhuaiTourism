using BLL;
using System.Collections.Generic;
using System.Web.Http;
using ViewModel.Common;
using ViewModel.TouristRoute;

namespace WebApi.Controllers
{
    public class TouristRouteController : ApiController
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
        public void Add(AddOrEditTouristRouteInput input) {
            bll.Add(input);
        }

        /// <summary>
        /// 编辑旅游线路
        /// </summary>
        /// <param name="input"></param>
        public void Edit(AddOrEditTouristRouteInput input) {
            bll.Edit(input);
        }

        /// <summary>
        /// 获取旅游线路列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<TouristRouteForView> GetList() {
           return bll.GetList();
        }

        /// <summary>
        /// 根据Id删除旅游线路
        /// </summary>
        /// <param name="input"></param>
        public void Delete(RTEntity<int> input) {
            bll.Delete(input);
        }

        /// <summary>
        /// 根据Id获取单个旅游线路
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public TouristRouteForView GetTouristRoute(RTEntity<int> input) {
            return bll.GetTouristRoute(input);
        }
    }
}