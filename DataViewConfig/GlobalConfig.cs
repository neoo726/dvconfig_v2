using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewConfig
{
   
    public static  class GlobalConfig
    {


        public static GlobalLanguage CurLanguage = GlobalLanguage.zhCN;
        public static bool IsDataViewConfig = true;
        public static UserLevelType CurUserLevel=  UserLevelType.Administrator;
        public static Dictionary<string, TipsModel> ConfigToolTipsDict = new Dictionary<string, TipsModel>();

    }
    public class TipsModel
    {
        public string tips_name { get; set; }
        public int tips_type { get; set; } //1=text,2=img
        public string tips_zh { get; set; }
        public string tips_en { get; set; }
        public string tips_img_url { get; set; }
    }
}
