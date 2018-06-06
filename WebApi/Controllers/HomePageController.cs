using BLL;
using System.Web.Http;
using ViewModel.HomePage;

namespace WebApi.Controllers
{
    public class HomePageController : ApiController
    {
        private HomePageService bll = new HomePageService();

        public bool Edit(HomePageInput input)
        {
            return bll.Edit(input);
        }

        public HomePageInput Detail()
        {
            return bll.Detail();
        }
    }
}