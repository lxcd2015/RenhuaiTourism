using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Common
{
    public class ResourcePath
    {
        public static string HomePage
        {
            get
            {
                return Path.Combine(ConfigurationInfo.GetResourceAddress, AddressConsts.HomePage);
            }
        }
        public static string Introduce
        {
            get
            {
                return Path.Combine(ConfigurationInfo.GetResourceAddress, AddressConsts.Introduce);
            }
        }
        public static string TouristInformation
        {
            get
            {
                return Path.Combine(ConfigurationInfo.GetResourceAddress, AddressConsts.TouristInformation);
            }
        }
        public static string TouristRoute
        {
            get
            {
                return Path.Combine(ConfigurationInfo.GetResourceAddress, AddressConsts.TouristRoute);
            }
        }
        public static string WisdomGuide
        {
            get
            {
                return Path.Combine(ConfigurationInfo.GetResourceAddress, AddressConsts.WisdomGuide);
            }
        }

        public static string Voice
        {
            get
            {
                return Path.Combine(ConfigurationInfo.GetResourceAddress, AddressConsts.Voice);
            }
        }
    }
}
