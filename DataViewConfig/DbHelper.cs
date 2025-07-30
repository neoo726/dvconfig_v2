using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewConfig
{
    public sealed class DbHelper
    {
        #region 单例
        private static  readonly DbHelper instance =new DbHelper();
        private DbHelper() { }
        public static DbHelper GetInsance()
        {
            return instance;
        }
        #endregion
        public static SqlSugar.SqlSugarScope db ;
    }
}
