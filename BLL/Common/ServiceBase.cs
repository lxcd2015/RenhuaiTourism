using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Common
{
    public class ServiceBase
    {
        private const string HttpProtocol = "http://";

        protected string HttpPathCombine(string path1,string path2)
        {
            if (path1 == null || path1 == "") return path1;
            if (path2 == null || path2 == "") return path2;

            if (path2.IndexOf(HttpProtocol) >-1) return path2;

            var path= Path.Combine(path1, path2);

            path = Path.Combine(HttpProtocol, path);
            path = path.Replace("\\", "/");
          
            return path;
        }
    }
}
