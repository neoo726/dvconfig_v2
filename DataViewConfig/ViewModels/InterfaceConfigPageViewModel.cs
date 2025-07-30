using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using DataView_Configuration;
using DataViewConfig.Pages;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class InterfaceConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<RequestInterfaceModel> requestInterfaceLst;
        public ObservableCollection<RequestInterfaceModel> RequestInterfaceLst
        {
            get => requestInterfaceLst;
            set
            {
                requestInterfaceLst = value; OnPropertyChanged("RequestInterfaceLst");
            }
        }
        private string queryInputStr;
        public string QueryInputStr
        {
            get => queryInputStr;
            set
            {
                queryInputStr = value; OnPropertyChanged("QueryInputStr");
            }
        }
        #region Command
        public Command QueryCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        #endregion

        public InterfaceConfigPageViewModel()
        {
            RefreshInterfaceLst();
            QueryCommand = new Command(Query);
            AddNewCommand = new Command(AddNew);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
           
        }
        private void RefreshInterfaceLst(string searchTxt=null)
        {
            RequestInterfaceLst = new ObservableCollection<RequestInterfaceModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_request_interface>()
              .ToList();
            int i = 1;
            foreach (var item in curRequests)
            {
                RequestInterfaceModel rf = new RequestInterfaceModel();
                rf.Id += i;
                rf.ParamIdLst = item.request_param_list;
                rf.RequestInternalName = item.request_internal_name;
                //rf.DestSystemName = (RequestSystemEnum)Enum.Parse(typeof(RequestSystemEnum), item.dest_system_name, true);
                rf.SystemId = item.system_id;
                if (item.param_format.ToLower() == "json")
                {
                    rf.EcsComm = ECSCommType.MQ;
                }
                else
                {
                    rf.ParamSeparator = item.param_separtor;
                    rf.EcsComm = ECSCommType.OPC;
                    rf.DestTagName = JsonConvert.DeserializeObject<RequestDestTagModel>(item.dest_tag_name);
                }
                if (!string.IsNullOrEmpty(item.return_value))
                {
                    rf.ReturnValueLst = JsonConvert.DeserializeObject<List<RequestSpecialReturnValueModel>>(item.return_value);
                }
                rf.MsgType = item.msg_type;
                rf.PreCondition = (RequestPreConditionType)Enum.Parse(typeof(RequestPreConditionType), item.precondition_id.ToString(), true);
                rf.IsSuccessShowTips = item.success_tips_show ;
                rf.IsFailedShowTips = item.failed_tips_show ;
                rf.IsSpecialReturnValue=item.special_return_value ;
                //模糊搜索，接口名称，描述，参数列表，msg_type
                if (string.IsNullOrEmpty(searchTxt))
                {
                    RequestInterfaceLst.Add(rf);
                }
                else
                {
                    if(Utli.StringContains(rf.RequestInternalName, searchTxt)
                         || Utli.StringContains(rf.RequestDesc, searchTxt)
                         || Utli.StringContains(rf.MsgType, searchTxt)
                         || Utli.StringContains(rf.ParamIdLst, searchTxt)
                         )
                    {
                        RequestInterfaceLst.Add(rf);
                    }
                }
                i++;
            }
        }
        //搜索
        private void Query(object o)
        {
            var searchtxt = o.ToString();
            //模糊搜索
            RefreshInterfaceLst(searchtxt.ToLower());
        }
        private void AddNew(object o)
        {
            
            InterfaceEditPopup ce = new InterfaceEditPopup(null);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
               
                RefreshInterfaceLst();

            }
            MainWindow.RemoveMask();
        }
        private void Edit(object o)
        {
            var selectedInterface = o as RequestInterfaceModel;
           
            InterfaceEditPopup ce = new InterfaceEditPopup(selectedInterface);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                
                RefreshInterfaceLst();

            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedCrane = o as RequestInterfaceModel;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该Request吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_request_interface>().Where(x=>x.request_internal_name==selectedCrane.RequestInternalName).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                RequestInterfaceLst.Remove(selectedCrane);
            }
        }
        private void SaveRow(object o)
        {
            //var requestModel = o as RequestInterfaceModel;
            //requestModel.UpdateParam();
            //var destTagNameJson = JsonConvert.SerializeObject(requestModel.DestSystemName);
            //var returnValueJjson = JsonConvert.SerializeObject(requestModel.ReturnValueLst);
            //var result = DbHelper.db
            //    .Updateable<DbModels.dv_request_interface>()
            //    .SetColumns(it => new DbModels.dv_request_interface() 
            //        { 
            //        request_param_list = requestModel.ParamIdLst,
            //        msg_type = requestModel.MsgType,
                   
            //        param_format= requestModel.ParamFormat,
            //        param_separtor=requestModel.ParamSeparator,
            //        dest_tag_name= destTagNameJson,
            //        return_value=returnValueJjson,
            //        precondition_id=(int)requestModel.PreCondition,
            //    })
            //    .Where(it => it.request_internal_name == requestModel.RequestInternalName).ExecuteCommand();
            //if (result > 0)
            //{
            //    MessageBox.Show("保存成功！");
            //}
            //else
            //{
            //    MessageBox.Show("保存失败！");
            //}
        }

        private void DeleteRow(object o)
        {
            var requestModel = o as RequestInterfaceModel;
            int affectedRow=DbHelper.db.Deleteable<DbModels.dv_request_interface>()
                .Where(new DbModels.dv_request_interface() { request_internal_name = requestModel.RequestInternalName }).ExecuteCommand();
            if (affectedRow > 0)
            {
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }
    }
    public class RequestInterfaceModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string RequestInternalName { get; set; }
        public int SystemId { get; set; }
        public string RequestDesc { get; set; }
        public ECSCommType EcsComm { get; set; }
       
        public RequestPreConditionType PreCondition { get; set; }
        public string ParamIdLst { get; set; }
        public string MsgType { get; set; }
         public string ParamFormat { get; set; }
        public string ParamSeparator { get; set; }
        public bool IsSpecialReturnValue { get; set; }
        public bool IsSuccessShowTips { get; set; }
        public bool IsFailedShowTips { get; set; }
        public RequestDestTagModel DestTagName { get; set; }
        public List<RequestSpecialReturnValueModel> ReturnValueLst { get; set; }

        public void UpdateParam()
        {
            if(this.EcsComm== ECSCommType.MQ)
            {
                this.ParamFormat = "json";
            }
            else
            {
                this.ParamFormat = "string";
            }
        }
        //public class ReturnValueModel
        //{
        //    public int  return_value { get; set; }
        //    public string  return_desc { get; set; }
        //}

    }
}
