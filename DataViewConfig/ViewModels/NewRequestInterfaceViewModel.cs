using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using DataView_Configuration;
using Newtonsoft.Json;

namespace DataViewConfig
{
    internal class NewRequestInterfaceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        #region 命令

        public Command OpenPageCommand { get; set; }
        public Command AddCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command EditReturnCommand { get; set; }
        #endregion
        private ObservableCollection<string> argTagLst;
        public ObservableCollection<string> ArgTagLst 
        {
            get => argTagLst;
            set { argTagLst = value; OnPropertyChanged("ArgTagLst"); }
        }
        private ObservableCollection<string> eventTagLst;
        public ObservableCollection<string> EventTagLst
        {
            get => eventTagLst;
            set { eventTagLst = value; OnPropertyChanged("EventTagLst"); }
        }
        private ObservableCollection<string> returnTagLst;
        public ObservableCollection<string> ReturnTagLst
        {
            get => returnTagLst;
            set { returnTagLst = value; OnPropertyChanged("ReturnTagLst"); }
        }
        #region 输入值 
        private string requestReturnVal;
        public string RequestReturnVal
        {
            get => requestReturnVal;
            set { requestReturnVal = value; OnPropertyChanged("RequestReturnVal"); }
        }
        private string requestInternalName;
        public string RequestInternalName
        {
            get => requestInternalName;
            set { requestInternalName = value; OnPropertyChanged("RequestInternalName"); }
        }
        private string paramSeparator;
        public string ParamSeparator
        {
            get => paramSeparator;
            set { paramSeparator = value; OnPropertyChanged("ParamSeparator"); }
        }
        private string paramIdLst;
        public string ParamIdLst
        {
            get => paramIdLst;
            set { paramIdLst = value; OnPropertyChanged("ParamIdLst"); }
        }
        private string msgType;
        public string MsgType
        {
            get => msgType;
            set { msgType = value; OnPropertyChanged("MsgType"); }
        }

        private string argsTagInternalName;
        public string ArgsTagInternalName
        {
            get => argsTagInternalName;
            set { argsTagInternalName = value; OnPropertyChanged("ArgsTagInternalName"); }
        }
        private string eventTagInternalName;
        public string EventTagInternalName
        {
            get => eventTagInternalName;
            set { eventTagInternalName = value; OnPropertyChanged("EventTagInternalName"); }
        }
        private string returnTagInternalName;
        public string ReturnTagInternalName
        {
            get => returnTagInternalName;
            set { returnTagInternalName = value; OnPropertyChanged("ReturnTagInternalName"); }
        }
        private RequestSystemEnum requestSystemName;
        public RequestSystemEnum RequestSystemName
        {
            get => requestSystemName;
            set { requestSystemName = value; OnPropertyChanged("RequestSystemName"); }
        }
        private RequestPreConditionType preconditionTyep;
        public RequestPreConditionType PreconditionTyep
        {
            get => preconditionTyep;
            set { preconditionTyep = value; OnPropertyChanged("PreconditionTyep"); }
        }
        private ECSCommType ecsComm;
        public ECSCommType EcsComm
        {
            get => ecsComm;
            set { ecsComm = value; OnPropertyChanged("EcsComm"); }
        }
        private bool isGlobalReturn;
        public bool IsGlobalReturn
        {
            get => isGlobalReturn;
            set { isGlobalReturn = value; OnPropertyChanged("IsGlobalReturn"); }
        }
        #endregion

        public NewRequestInterfaceViewModel()
        {
            RequestSystemName = RequestSystemEnum.RCCS;
            ArgTagLst = new ObservableCollection<string>();
            EventTagLst = new ObservableCollection<string>();
            ReturnTagLst = new ObservableCollection<string>();
            this.paramSeparator = ",";
            this.isGlobalReturn = true;
            //获取参数点
            var dvTagLst  = DbHelper.db.Queryable<DbModels.dv_tag>()
              .ToList();
            foreach (var item in dvTagLst)
            {
                if (item.tag_internal_name.Contains("_args"))
                {
                    ArgTagLst.Add(item.tag_internal_name);
                }
                else if (item.tag_internal_name.Contains("_event"))
                {
                    EventTagLst.Add(item.tag_internal_name);
                }
                else  if (item.tag_internal_name.Contains("_return"))
                {
                    ReturnTagLst.Add(item.tag_internal_name);
                }
            }
            OpenPageCommand = new Command(OpenPage);
            AddCommand = new Command(AddInterface);
            SelectParamCommand = new Command(SelectParam);
            EditReturnCommand = new Command(EditReturnVal);
        }
        private void OpenPage(object o)
        {
        }
        private void SelectParam(object o)
        {
            Pages.Popups.ParamSelectedPopup paramSelectPopup = new Pages.Popups.ParamSelectedPopup(null);
            paramSelectPopup.ShowDialog();
            this.ParamIdLst =
                String.Join(",", (paramSelectPopup.DataContext as ViewModels.ParamSelectedPopupViewModel).paramIdLst.ToArray());   
        }
        private void EditReturnVal(object o)
        {
            Pages.Popups.RequestReturnPopup rrp = new Pages.Popups.RequestReturnPopup();
            rrp.ShowDialog();
            var returValLst = (rrp.DataContext as ViewModels.RequestReturnPopupViewModel).ReturnValLst;
            this.RequestReturnVal = returValLst.Count>0? JsonConvert.SerializeObject(returValLst):"";
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddInterface(object o)
        {
            var dv_request = new DbModels.dv_request_interface();
            if (string.IsNullOrEmpty(requestInternalName))
            {
                MessageBox.Show("接口名称不能为空！");
                return;
            }
            dv_request.request_internal_name = this.requestInternalName;
            
            dv_request.dest_tag_name = JsonConvert.SerializeObject(new RequestDestTagModel()
            {
                args_tag_internal_name = this.argsTagInternalName,
                event_tag_internal_name = this.eventTagInternalName,
                return_tag_internal_name = this.returnTagInternalName,
                return_type="int_array",
            });
            dv_request.request_param_list = this.paramIdLst;
            dv_request.param_format = this.ecsComm == ECSCommType.MQ ? "json" : "string";
            dv_request.param_separtor = this.paramSeparator;
            dv_request.return_value = this.RequestReturnVal;
            dv_request.precondition_id = (int)this.PreconditionTyep;
            dv_request.success_tips_show = true;
            dv_request.failed_tips_show = true;
            int affectedRow=DbHelper.db.Insertable<DbModels.dv_request_interface>(dv_request).ExecuteCommand();
            if(affectedRow != 0)
            {
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
}
