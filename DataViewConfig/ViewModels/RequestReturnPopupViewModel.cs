using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Models;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class RequestReturnPopupViewModel : INotifyPropertyChanged
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

        public Command OpenPageCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        private ObservableCollection<DataView_Configuration.RequestSpecialReturnValueModel> returnValLst;
        public ObservableCollection<DataView_Configuration.RequestSpecialReturnValueModel> ReturnValLst
        {
            get => returnValLst;
            set { returnValLst = value; OnPropertyChanged("returnValLst"); }
        }
        public RequestReturnPopupViewModel()
        {
            returnValLst = new ObservableCollection<RequestSpecialReturnValueModel>();
            returnValLst.Add(new RequestSpecialReturnValueModel()
            {
                return_value = "2",
                return_desc_zh = "工况不允许"
            });
            returnValLst.Add(new RequestSpecialReturnValueModel()
            {
                return_value = "3",
                return_desc_zh = "设备已被其他操作台连接"
            });
          
            //.Select((o, cus) => new ParamModel
            // {
            //     ParamInternalName = o.param_name,
            //     ParamSource = (RequestParamSource)Enum.Parse(typeof(RequestParamSource), o.param_source.ToString()),
            //     ParamJsonVariableName = o.param_json_variable_name,
            //     TargetValType = (ParamTargetType)Enum.Parse(typeof(ParamTargetType), o.value_type),

            // })

            OpenPageCommand = new Command(OpenPage);
            ConfirmCommand = new Command(ConfirmSelection);
            CancelCommand = new Command(CancelSelection);

        }
        private void OpenPage(object o)
        {
        }
        private void ConfirmSelection(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
        private void CancelSelection(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
    }
    
}
