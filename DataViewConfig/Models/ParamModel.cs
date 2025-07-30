using DataView_Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewConfig.Models
{
    public class ParamModel
    {
        public int ParamID { get; set; }
        public string ParamName { get; set; }
        public RequestParamSource ParamSource { get; set; }
        /// <summary>
        /// 参数对应的JSON字段名称
        /// </summary>
        public string JsonVariableName { get; set; }
        public string ConstValue { get; set; }
        public string MacroName { get; set; }
        public string ReportColumnName { get; set; }
        public string ReportColumnCount { get; set; }
        public string DbSheetName { get; set; }
        public string ChildrenParamId { get; set; }
        public string SpliceParamIdLst { get; set; }
        public string ControlInternalName { get; set; }
        public int ControlComboxIndexOrTextType { get; set; }
        public string TagInternalName { get; set; }
        /// <summary>
        /// 参数值在JSON点位中的字段名（用于读取参数值）
        /// </summary>
        public string TagValueJsonPath { get; set; }
        public string ArrayParamIdLst { get; set; }
        public ParamTargetType TargetValType { get; set; }
        public List<RequestSpecialReturnValueModel> ReturnValueLst { get; set; }
        public string ParamDesc { get; set; }
        public bool IsSelected { get; set; }
        public bool IsParamValidationCheck { get; set; }
        public List<RequestParamContentModel.ValidationRule> ValidationRuleLst { get; set; }
        public string ExpressionStr { get; set; }
        
        public string ParamValueDetail
        {
            get
            {
                switch (ParamSource)
                {
                    case RequestParamSource.CONSTANT:
                        return ConstValue;
                    case RequestParamSource.CONTROL:
                        return ControlInternalName;
                    case RequestParamSource.MACRO:
                        return MacroName;
                    case RequestParamSource.NORMAL_TAG:
                        return TagInternalName;
                    case RequestParamSource.JSON_TAG:
                        return $"{TagInternalName} ['{TagValueJsonPath}']";
                    default:
                        return "";
                }
            }
        }
    }
    //public class ParamInfoModel
    //{
    //    public int ParamID { get; set; }

    //    public string ParamName { get; set; }
    //    public RequestParamSource ParamSource { get; set; }
    //    public string JsonVariableName { get; set; }
    //    public string ConstValue { get; set; }
    //    public string MacroName { get; set; }
    //    public string ControlInternalName { get; set; }
    //    public string TagInternalName { get; set; }
    //    public string TagValueJsonPath { get; set; }
    //    public ParamTargetType TargetValueType { get; set; }
    //    public string ParamDesc { get; set; }
    //    /// <summary>
    //    /// 参数值信息（不同类型的参数的统一值描述，方便显示）
    //    /// </summary>
    //    public string ParamValueDetail
    //    {
    //        get
    //        {
    //            switch (ParamSource)
    //            {
    //                case RequestParamSource.CONSTANT:
    //                    return ConstValue;
    //                case RequestParamSource.CONTROL:
    //                    return ControlInternalName;
    //                case RequestParamSource.MACRO:
    //                    return MacroName;
    //                case RequestParamSource.NORMAL_TAG:
    //                    return TagInternalName;
    //                case RequestParamSource.JSON_TAG:
    //                    return $"{TagInternalName} ['{TagValueJsonPath}']";
    //                default:
    //                    return "";
    //            }
    //        }
    //    }
    //}
}
