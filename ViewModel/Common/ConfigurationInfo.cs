using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModel.Common
{
    public class ConfigurationInfo
    {
        public static string GetResourceAddress
        {
            get
            {
                return HttpContext.Current.Request.Url.Authority+ConfigurationManager.AppSettings["ResourceAddress"];
            }
        }
    }
}
