using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;
using DataView_Configuration;
using DataViewConfig.Models;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class ParameterConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<ParamModel> paramLst;
        public ObservableCollection<ParamModel> ParamLst
        {
            get => paramLst;
            set { paramLst = value; OnPropertyChanged("ParamLst"); }
        }
        private string paramName;
        public string ParamName
        {
            get => paramName;
            set { paramName = value; OnPropertyChanged("ParamName"); }
        }
        private RequestParamSource curRequestParamSource;
        public RequestParamSource CurRequestParamSource
        {
            get => curRequestParamSource;
            set { curRequestParamSource = value; OnPropertyChanged("CurRequestParamSource"); }
        }
        private string controlInternalName;
        public string ControlInternalName
        {
            get => controlInternalName;
            set { controlInternalName = value; OnPropertyChanged("ControlInternalName"); }
        }
        private string constValue;
        public string ConstValue
        {
            get => constValue;
            set { constValue = value; OnPropertyChanged("ConstValue"); }
        }
        private string tagInternalName;
        public string TagInternalName
        {
            get => tagInternalName;
            set { tagInternalName = value; OnPropertyChanged("TagInternalName"); }
        }
        private string tagValueJsonPath;
        public string TagValueJsonPath
        {
            get => tagValueJsonPath;
            set { tagValueJsonPath = value; OnPropertyChanged("TagValueJsonPath"); }
        }
        private string jsonVariableName;
        public string JsonVariableName
        {
            get => jsonVariableName;
            set { jsonVariableName = value; OnPropertyChanged("JsonVariableName"); }
        }
        
       
        private string tagJsonPath;
        public string TagJsonPath
        {
            get => tagJsonPath;
            set
            {
                tagJsonPath = value; OnPropertyChanged("TagJsonPath");
            }
        }
        private string curMacroName;
        public string CurMacroName
        {
            get => curMacroName;
            set
            {
                curMacroName = value; OnPropertyChanged("CurMacroName");
            }
        }
        private string arrayParamIdLst;
        public string ArrayParamIdLst
        {
            get => arrayParamIdLst;
            set
            {
                arrayParamIdLst = value; OnPropertyChanged("ArrayParamIdLst");
            }
        }
        private string spliceParamIdLst;
        public string SpliceParamIdLst
        {
            get => spliceParamIdLst;
            set
            {
                spliceParamIdLst = value; OnPropertyChanged("SpliceParamIdLst");
            }
        }
        private string childrenParamIdLst;
        public string ChildrenParamIdLst
        {
            get => childrenParamIdLst;
            set
            {
                childrenParamIdLst = value; OnPropertyChanged("ChildrenParamIdLst");
            }
        }
        private string curDbSheetName;
        public string CurDbSheetName
        {
            get => curDbSheetName;
            set
            {
                curDbSheetName = value; OnPropertyChanged("CurDbSheetName");
            }
        }
        private ParamTargetType curParamTargetType;
        public ParamTargetType CurParamTargetType
        {
            get => curParamTargetType;
            set { curParamTargetType = value; OnPropertyChanged("CurParamTargetType"); }
        }
        #region Command
        public Command QueryCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        #endregion
        public ParameterConfigPageViewModel()
        {
            RefreshParameterLst();
            QueryCommand = new Command(Query); 
            AddNewCommand = new Command(AddNew);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
        }
        private void RefreshParameterLst(string searchTxt=null)
        {
            ParamLst = new ObservableCollection<ParamModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_request_param>()
                .LeftJoin<DbModels.dv_request_param_source>((o, cus) => o.param_source == cus.source_id)
                .ToList();

            foreach (var item in curRequests)
            {
                var paramContent = JsonConvert.DeserializeObject<DataView_Configuration.RequestParamContentModel>(item.param_content);
                var pm = new ParamModel()
                {
                    ParamID = item.param_id,
                    ParamName = item.param_name,
                    ParamSource = (RequestParamSource)Enum.Parse(typeof(RequestParamSource), item.param_source.ToString(), true),
                    JsonVariableName = item.param_json_variable_name,
                    TargetValType = (ParamTargetType)Enum.Parse(typeof(ParamTargetType), item.value_type, true),
                    ControlInternalName = paramContent.control_internal_name == null ? "" : paramContent.control_internal_name,
                    ControlComboxIndexOrTextType = paramContent.control_combox_data_type,
                    TagInternalName = paramContent.tag_internal_name == null ? "" : paramContent.tag_internal_name,
                    TagValueJsonPath = paramContent.tag_value_json_path == null ? "" : paramContent.tag_value_json_path,
                    ConstValue = paramContent.constant_value == null ? "" : paramContent.constant_value,
                    MacroName = paramContent.macro_name == null ? "" : paramContent.macro_name,
                    ReportColumnName = paramContent.report_column_name == null ? "" : paramContent.report_column_name,
                    ArrayParamIdLst = paramContent.array_param_id_list == null ? "" : paramContent.array_param_id_list,
                    SpliceParamIdLst = paramContent.splice_param_id_list == null ? "" : paramContent.splice_param_id_list,
                    DbSheetName = paramContent.db_sheet_name == null ? "" : paramContent.db_sheet_name,
                    ChildrenParamId = paramContent.children_param_id == null ? "" : paramContent.children_param_id,
                    ParamDesc=item.param_desc,
                };
                
                //模糊搜索，参数名称，json变量名称，参数描述
                if (string.IsNullOrEmpty(searchTxt))
                {
                    ParamLst.Add(pm);
                }
                else
                {
                    if (Utli.StringContains(pm.ControlInternalName, searchTxt)
                         || Utli.StringContains(pm.JsonVariableName, searchTxt)
                         || Utli.StringContains(pm.ParamDesc, searchTxt)
                         
                         )
                    {
                        ParamLst.Add(pm);
                    }
                }
            }
        }
        //搜索
        private void Query(object o)
        {
            var searchtxt = o.ToString();
            //模糊搜索，
            RefreshParameterLst(searchtxt.ToLower());
        }
        private void Edit(object o)
        {
            var selectedParam = o as ParamModel;

            //Pages.Popups.ParamEditPopup ce = new Pages.Popups.ParamEditPopup(selectedParam);
            //MainWindow.AddMask();
            //if (ce.ShowDialog() == true)
            //{
                
            //    RefreshParameterLst();

            //}
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedParameter = o as ParamModel;
            if (selectedParameter == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该参数吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_request_param>().Where(x=>x.param_id== selectedParameter.ParamID).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                ParamLst.Remove(selectedParameter);
            }
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddNew(object o)
        {

            ParamEditPopup ce = new ParamEditPopup(null);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                MainWindow.RemoveMask();
                RefreshParameterLst();
            }

        }
    }
    //public class ParamModel
    //{
    //    public int ParamID { get; set; }
    //    public string ParamName { get; set; }
    //    public RequestParamSource ParamSource { get; set; }
    //    public string JsonVariableName { get; set; }
    //    public string ConstValue { get; set; }
    //    public string MacroName { get; set; }
    //    public string ReportColumnName { get; set; }
    //    public string ReportColumnCount { get; set; }
    //    public string DbSheetName { get; set; }
    //    public string ChildrenParamId { get; set; }
    //    public string SpliceParamIdLst { get; set; }
    //    public string ControlInternalName { get; set; }
    //    public int ControlComboxIndexOrTextType { get; set; }
    //    public string TagInternalName { get; set; }
    //    public string TagValueJsonPath { get; set; }
    //    public string ArrayParamIdLst { get; set; }
    //    public ParamTargetType TargetValType { get; set; }
    //    public List<RequestSpecialReturnValueModel> ReturnValueLst { get; set; }
    //    public string ParamDesc { get; set; }
    //    public bool IsSelected { get; set; }
    //    public bool IsParamValidationCheck { get; set; }
    //    public List<RequestParamContentModel.ValidationRule> ValidationRuleLst { get; set; }
    //    public string ExpressionStr { get; set; }
    //    //public class ReturnValueModel
    //    //{
    //    //    public int  return_value { get; set; }
    //    //    public string  return_desc { get; set; }
    //    //}

    //}
}
