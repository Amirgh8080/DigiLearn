using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.Core
{
    public class UserModuleDirectories
    {
        public static string UserAvatar = "wwwroot/user/images/avatar";
        public static string GetUserAvatar(string imageName) => $"{UserAvatar.Replace("wwwroot","")}/{imageName}";
    }
}
