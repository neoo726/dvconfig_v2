using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewConfig
{
    public enum GlobalLanguage
    {

        enUS=1,
        zhCN=2,

    }
    public enum ParamChangeType
    {
        SelectOther=1,
        AddNew=2,
        EditExist=3,
    }
    public enum UserLevelType
    {
        Operator=1,
        Administrator=2,
    }
    /// <summary>
    /// 模板类型 
    /// </summary>
    public enum TemplateTypeEnum
    {
        RXG = 1,
        STS = 2,
    }
}
