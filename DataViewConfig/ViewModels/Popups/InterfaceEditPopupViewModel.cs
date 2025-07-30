using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class InterfaceEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int requestID;
        public int RequestID
        {
            get => requestID;
            set {
                requestID = value; OnPropertyChanged("RequestID");
            }
        }
        private string requestInternalName;
        public string RequestInternalName
        {
            get => requestInternalName;
            set
            {
                requestInternalName = value; OnPropertyChanged("RequestInternalName");
            }
        }
        private int requestSystemId;
        public int RequestSystemId
        {
            get => requestSystemId;
            set
            {
                requestSystemId = value; OnPropertyChanged("RequestSystemId");
            }
        }
        private ObservableCollection<DbModels.dv_system> requestSystemLst;
        public ObservableCollection<DbModels.dv_system> RequestSystemLst
        {
            get => requestSystemLst;
            set
            {
                requestSystemLst = value; OnPropertyChanged("RequestSystemLst");
            }
        }
        private RequestPreConditionType requestPrecondition;
        public RequestPreConditionType RequestPrecondition
        {
            get => requestPrecondition;
            set
            {
                requestPrecondition = value; OnPropertyChanged("RequestPrecondition");
            }
        }
        private string paramIdLst;
        public string ParamIdLst
        {
            get => paramIdLst;
            set
            {
                paramIdLst = value; OnPropertyChanged("ParamIdLst");
            }
        }
        private ECSCommType ecsComm;
        public ECSCommType EcsComm
        {
            get => ecsComm;
            set
            {
                ecsComm = value; OnPropertyChanged("EcsComm");
            }
        }
        private string paramSeperator;
        public string ParamSeperator
        {
            get => paramSeperator;
            set
            {
                paramSeperator = value; OnPropertyChanged("ParamSeperator");
            }
        }
        private string msgType;
        public string MsgType
        {
            get => msgType;
            set
            {
                msgType = value; OnPropertyChanged("MsgType");
            }
        }
        private string requestDesc;
        public string RequestDesc
        {
            get => requestDesc;
            set
            {
                requestDesc = value; OnPropertyChanged("RequestDesc");
            }
        }
        private int destTagType;
        public int DestTagType
        {
            get => destTagType;
            set
            {
                destTagType = value; OnPropertyChanged("DestTagType");
            }
        }
        private string requestTagInternalName;
        public string RequestTagInternalName
        {
            get => requestTagInternalName;
            set
            {
                requestTagInternalName = value; OnPropertyChanged("RequestTagInternalName");
            }
        }
        private string requestFeedbackTagInternalName;
        public string RequestFeedbackTagInternalName
        {
            get => requestFeedbackTagInternalName;
            set
            {
                requestFeedbackTagInternalName = value; OnPropertyChanged("RequestFeedbackTagInternalName");
            }
        }
        private string argsTagName;
        public string ArgsTagName
        {
            get => argsTagName;
            set
            {
                argsTagName = value; OnPropertyChanged("ArgsTagName");
            }
        }
        private string eventTagName;
        public string EventTagName
        {
            get => eventTagName;
            set
            {
                eventTagName = value; OnPropertyChanged("EventTagName");
            }
        }
        private string returnTagName;
        public string ReturnTagName
        {
            get => returnTagName;
            set
            {
                returnTagName = value; OnPropertyChanged("ReturnTagName");
            }
        }
        private ObservableCollection<RequestSpecialReturnValueModel> returnValueLst;
        public ObservableCollection<RequestSpecialReturnValueModel> ReturnValueLst
        {
            get => returnValueLst;
            set
            {
                returnValueLst = value; OnPropertyChanged("ReturnValueLst");
            }
        }
        private bool isAddNew;
        public bool IsAddNew
        {
            get => isAddNew;
            set
            {
                isAddNew = value; OnPropertyChanged("IsAddNew");
            }
        }
        private bool isReturnIntArray;
        public bool IsReturnIntArray
        {
            get => isReturnIntArray;
            set
            {
                isReturnIntArray = value; OnPropertyChanged("IsReturnIntArray");
            }
        }
        private bool isReturnLong;
        public bool IsReturnLong
        {
            get => isReturnLong;
            set
            {
                isReturnLong = value; OnPropertyChanged("IsReturnLong");
            }
        }

        private bool isCommReturnVal;
        public bool IsCommReturnVal
        {
            get => isCommReturnVal;
            set
            {
                isCommReturnVal = value; OnPropertyChanged("IsCommReturnVal");
            }
        }
        private bool isSpecialReturnVal;
        public bool IsSpecialReturnVal
        {
            get => isSpecialReturnVal;
            set
            {
                isSpecialReturnVal = value; OnPropertyChanged("IsSpecialReturnVal");
            }
        }
        private bool isSuccessShowTips;
        public bool IsSuccessShowTips
        {
            get => isSuccessShowTips;
            set
            {
                isSuccessShowTips = value; OnPropertyChanged("IsSuccessShowTips");
            }
        }
        private bool isFailedShowTips;
        public bool IsFailedShowTips
        {
            get => isFailedShowTips;
            set
            {
                isFailedShowTips = value; OnPropertyChanged("IsFailedShowTips");
            }
        }
        private ObservableCollection<string> plcRequestTagNameLst;
        public ObservableCollection<string> PlcRequestTagNameLst
        {
            get => plcRequestTagNameLst;
            set
            {
                plcRequestTagNameLst = value; OnPropertyChanged("PlcRequestTagNameLst");
            }
        }
        private ObservableCollection<string> plcFeedbackTagNameLst;
        public ObservableCollection<string> PlcFeedbackTagNameLst
        {
            get => plcFeedbackTagNameLst;
            set
            {
                plcFeedbackTagNameLst = value; OnPropertyChanged("PlcFeedbackTagNameLst");
            }
        }
        private ObservableCollection<string> argsTagNameLst;
        public ObservableCollection<string> ArgsTagNameLst
        {
            get => argsTagNameLst;
            set
            {
                argsTagNameLst = value; OnPropertyChanged("ArgsTagNameLst");
            }
        }
        private ObservableCollection<string> eventTagNameLst;
        public ObservableCollection<string> EventTagNameLst
        {
            get => eventTagNameLst;
            set
            {
                eventTagNameLst = value; OnPropertyChanged("EventTagNameLst");
            }
        }
        private ObservableCollection<string> returnTagNameLst;
        public ObservableCollection<string> ReturnTagNameLst
        {
            get => returnTagNameLst;
            set
            {
                returnTagNameLst = value; OnPropertyChanged("ReturnTagNameLst");
            }
        }
        private ObservableCollection<string> paramInternalNameLst;
        public ObservableCollection<string> ParamInternalNameLst
        {
            get => paramInternalNameLst;
            set
            {
                paramInternalNameLst = value; OnPropertyChanged("ParamInternalNameLst");
            }
        }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }
        #endregion
        public InterfaceEditPopupViewModel(RequestInterfaceModel request)
        {
            this.ParamSeperator = ",";
            var tagNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_tag>()
                .Select(x => x.tag_internal_name).ToList());
            this.ArgsTagNameLst=new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("args")).ToList());
            this.EventTagNameLst = new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("event")).ToList());
            this.ReturnTagNameLst = new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("return")).ToList());
            PlcRequestTagNameLst = new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("plc")).ToList());
            PlcFeedbackTagNameLst = new ObservableCollection<string>(tagNameLst.Where(x => x.Contains("feedback")).ToList());
            ParamInternalNameLst = new ObservableCollection<string>(DbHelper.db.Queryable<DbModels.dv_request_param>()
                .Select(x => x.param_name).ToList());

            RequestSystemLst = new ObservableCollection<DbModels.dv_system>(DbHelper.db.Queryable<DbModels.dv_system>()
                .ToList());
            if (request == null)
            {
                this.IsAddNew = true;
                this.IsCommReturnVal = true;
                this.IsSpecialReturnVal = false;
                this.isReturnIntArray = true;
                this.DestTagType = 1;//默认args event return格式
            }
            else
            {
                this.IsAddNew = false;
                this.requestInternalName = request.RequestInternalName;
                this.RequestSystemId = request.SystemId;
                this.RequestPrecondition = request.PreCondition;
                this.ParamIdLst = request.ParamIdLst;
                this.IsFailedShowTips = request.IsFailedShowTips;
                this.isSuccessShowTips = request.IsSuccessShowTips;
                this.EcsComm = request.EcsComm;
                if(this.EcsComm== ECSCommType.MQ)
                {
                    this.MsgType = request.MsgType;
                }
                else if(this.EcsComm== ECSCommType.OPC)
                {
                    this.ParamSeperator = request.ParamSeparator;
                    this.ArgsTagName = request.DestTagName.args_tag_internal_name;
                    this.EventTagName = request.DestTagName.event_tag_internal_name;
                    this.ReturnTagName = request.DestTagName.return_tag_internal_name;
                    this.isReturnIntArray = request.DestTagName.return_type==null||request.DestTagName.return_type.ToLower()=="int_array";
                    this.isReturnLong = request.DestTagName.return_type != null&&request.DestTagName.return_type.ToLower() == "long";

                    this.DestTagType = request.DestTagName.dest_tag_type;
                    this.RequestTagInternalName = request.DestTagName.request_tag_internal_name;
                    this.RequestFeedbackTagInternalName = request.DestTagName.feedback_tag_internal_name;
                }
                if (!request.IsSpecialReturnValue)
                {
                    this.IsCommReturnVal = true;
                    this.IsSpecialReturnVal = false;
                }
                else
                {
                    this.isCommReturnVal = false;
                    this.IsSpecialReturnVal = true;
                    this.ReturnValueLst = new ObservableCollection<RequestSpecialReturnValueModel>(request.ReturnValueLst);
                }
            }
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            SelectParamCommand = new Command(SelectParam);
            AddNewReturnValCommand = new Command(AddNewReturnVal);
            DeleteReturnValCommand = new Command(DeleteReturnVal);
        }
        private void DeleteReturnVal(object o)
        {
            var returnVal = o as RequestSpecialReturnValueModel;
            if (ReturnValueLst == null || ReturnValueLst.Count == 0)
            {
                return;
            }
            this.ReturnValueLst.Remove(returnVal);
        }
        private void AddNewReturnVal(object o)
        {
            if (ReturnValueLst == null || ReturnValueLst.Count == 0)
            {
                this.ReturnValueLst = new ObservableCollection<RequestSpecialReturnValueModel>();
            }
            this.ReturnValueLst.Add(new RequestSpecialReturnValueModel()
            {
                return_value="1",
                return_desc_zh="test",
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">参数列表</param>
        private void SelectParam(object o)
        {
            short[] paramIdArray = null;
            if (o!=null&&!string.IsNullOrEmpty(o.ToString()))
            {
               paramIdArray = Array.ConvertAll<string, short>(o.ToString().Split(','), s => Convert.ToInt16(s));
            }
            Pages.Popups.ParamSelectedPopup paramSelectPopup = new Pages.Popups.ParamSelectedPopup(paramIdArray);
            var bRet=paramSelectPopup.ShowDialog();
            if (bRet.Value)
            {
                this.ParamIdLst =
               String.Join(",", (paramSelectPopup.DataContext as ViewModels.ParamSelectedPopupViewModel).paramIdLst.ToArray());
            }
        }
        private void Confirm(object o)
        {
            if(this.EcsComm== ECSCommType.OPC && this.DestTagType == 1)
            {
                if (this.IsSpecialReturnVal && (this.ReturnValueLst == null || this.ReturnValueLst.Count == 0))
                {
                    System.Windows.MessageBox.Show("当前选择的接口交互类型，不支持单独定义返回值，请选择通用返回值！"); return;
                }
            }
            if (this.IsSpecialReturnVal&&(this.ReturnValueLst == null|| this.ReturnValueLst.Count==0))
            {
                System.Windows.MessageBox.Show("已选择单独定义返回值，返回值列表不能为空！");return;
            }
            DataView_Configuration.RequestDestTagModel rd = new RequestDestTagModel();
            rd.dest_tag_type = this.DestTagType;
            rd.request_tag_internal_name = this.RequestInternalName;
            rd.feedback_tag_internal_name = this.RequestFeedbackTagInternalName;
            rd.args_tag_internal_name = this.ArgsTagName;
            rd.event_tag_internal_name = this.EventTagName;
            rd.return_tag_internal_name = this.ReturnTagName;
            rd.return_type = this.IsReturnIntArray ? "int_array" : "long";

            DbModels.dv_request_interface c = new DbModels.dv_request_interface()
            {
                request_internal_name = this.RequestInternalName,
                msg_type = this.MsgType,
                param_separtor = this.paramSeperator,
                param_format = this.EcsComm == ECSCommType.MQ ? "json" : "string",
                system_id = this.RequestSystemId,
                dest_tag_name = JsonConvert.SerializeObject(rd),
                precondition_id=(int)this.RequestPrecondition,
                request_param_list=this.ParamIdLst,
                return_value= this.isCommReturnVal?"":JsonConvert.SerializeObject(this.ReturnValueLst),
                failed_tips_show = this.IsFailedShowTips,
                success_tips_show = this.IsSuccessShowTips,
                special_return_value=this.IsSpecialReturnVal,
                request_desc=this.RequestDesc,
            };
            int affectedRow = 0;
            //添加新的
            if (this.IsAddNew)
            {
                affectedRow = DbHelper.db.Insertable<DbModels.dv_request_interface>(c)
              .ExecuteCommand();
            }
            //更新已有的
            else
            {
                affectedRow = DbHelper.db.Updateable<DbModels.dv_request_interface>(c)
               .Where(x => x.request_internal_name == c.request_internal_name).ExecuteCommand();
            }
            if (affectedRow > 0)
            {
                System.Windows.MessageBox.Show("保存成功！");
                var win = o as Window;
                win.DialogResult = true;
            }
            else
            {
                System.Windows.MessageBox.Show("保存失败！");
            }
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
    }
}
