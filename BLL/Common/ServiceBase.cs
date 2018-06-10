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
        protected string PathCombine(string path1,string path2)
        {
            if (path1 == null || path1 == "") return path1;
            if (path2 == null || path2 == "") return path2;
            return Path.Combine(path1,path2);
        }
    }
}
