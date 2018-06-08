using BLL;
using System.Web.Http;
using ViewModel.Introduce;

namespace WebApi.Controllers
{
    /// <summary>
    /// 仁怀简介服务
    /// </summary>
    public class IntroduceController : ApiController
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
        public bool Edit(IntroduceInput input)
        {
            return bll.Edit(input);
        }

        /// <summary>
        /// 获取酒都简介
        /// </summary>
        /// <returns></returns>
        public IntroduceInput Detail()
        {
            return bll.Detail();
        }
    }
}