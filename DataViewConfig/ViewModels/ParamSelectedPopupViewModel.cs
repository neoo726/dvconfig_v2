using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Models;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class ParamSelectedPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<ParamModel> ParamLst { get; set; }
        private  RequestSystemEnum requestSystemName;
        public RequestSystemEnum RequestSystemName
        {
            get => requestSystemName;
            set { requestSystemName = value;OnPropertyChanged("RequestSystemName"); }
        }
        //已选参数列表
        public List<short> existedParamIdLst;

        public Command OpenPageCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command CancelCommand { get; set; }

        public   List<string> paramIdLst = new List<string>(); 
        public ParamSelectedPopupViewModel(short[] paramIdArray)
        {
            if (paramIdArray!=null){
                this.existedParamIdLst = new List<short>(paramIdArray);
            }
            ParamLst = new ObservableCollection<ParamModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_request_param>()
                .LeftJoin<DbModels.dv_request_param_source>((o, cus) => o.param_source == cus.source_id)
                .ToList();
            //.Select((o, cus) => new ParamModel
            // {
            //     ParamInternalName = o.param_name,
            //     ParamSource = (RequestParamSource)Enum.Parse(typeof(RequestParamSource), o.param_source.ToString()),
            //     ParamJsonVariableName = o.param_json_variable_name,
            //     TargetValType = (ParamTargetType)Enum.Parse(typeof(ParamTargetType), o.value_type),

            // })
            int i = 1;
            foreach (var item in curRequests)
            {
                var paramContent = JsonConvert.DeserializeObject<DataView_Configuration.RequestParamContentModel>(item.param_content);
                bool isSelected = false;
                if (existedParamIdLst!=null&&existedParamIdLst.Contains((short)item.param_id))
                {
                    isSelected = true;
                }
                ParamLst.Add(new ParamModel()
                {
                    ParamID = item.param_id,
                    ParamName = item.param_name,
                    ParamSource = (RequestParamSource)Enum.Parse(typeof(RequestParamSource), item.param_source.ToString(), true),
                    JsonVariableName = item.param_json_variable_name,
                    TargetValType = (ParamTargetType)Enum.Parse(typeof(ParamTargetType), item.value_type, true),
                    ControlInternalName = paramContent.control_internal_name == null ? "" : paramContent.control_internal_name,
                    ControlComboxIndexOrTextType=paramContent.control_combox_data_type,
                    TagInternalName = paramContent.tag_internal_name == null ? "" : paramContent.tag_internal_name,
                    TagValueJsonPath = paramContent.tag_value_json_path == null ? "" : paramContent.tag_value_json_path,
                    ConstValue = paramContent.constant_value == null ? "" : paramContent.constant_value,
                    MacroName = paramContent.macro_name == null ? "" : paramContent.macro_name,
                    ReportColumnName = paramContent.report_column_name == null ? "" : paramContent.report_column_name,
                    ArrayParamIdLst = paramContent.array_param_id_list == null ? "" : paramContent.array_param_id_list,
                    SpliceParamIdLst = paramContent.splice_param_id_list == null ? "" : paramContent.splice_param_id_list,
                    DbSheetName = paramContent.db_sheet_name == null ? "" : paramContent.db_sheet_name,
                    ChildrenParamId = paramContent.children_param_id == null ? "" : paramContent.children_param_id,
                    IsSelected=isSelected,
                });
                
            }
            ParamLst = new ObservableCollection<ParamModel>(ParamLst.OrderByDescending(x => x.IsSelected == true));
            OpenPageCommand = new Command(OpenPage);
            ConfirmCommand = new Command(ConfirmSelection);
            CancelCommand = new Command(CancelSelection);

        }
        private void OpenPage(object o)
        {
        }
        private void ConfirmSelection(object o)
        {
          paramIdLst = new List<string>(); 
            
            foreach (var item in ParamLst) 
            {
                if (item.IsSelected)
                {
                    paramIdLst.Add(item.ParamID.ToString());
                }
            }
            if (paramIdLst.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("请选择至少一个参数！");
                return;
            }
            var win = o as Window;
            win.DialogResult = true;
        }
        private void CancelSelection(object o)
        {
            var win = o as Window;
            win.DialogResult = false;
        }
    }
}
