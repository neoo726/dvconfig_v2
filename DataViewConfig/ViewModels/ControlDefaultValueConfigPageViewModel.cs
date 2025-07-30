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
    internal class ControlDefaultValueConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<DbModels.dv_control_default_value> controlDefaultValLst;
        public ObservableCollection<DbModels.dv_control_default_value> ControlDefaultValLst
        {
            get => controlDefaultValLst;
            set
            {
                controlDefaultValLst = value; OnPropertyChanged("ControlDefaultValLst");
            }
        }
        #region Command
        public Command QueryCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        #endregion

        public ControlDefaultValueConfigPageViewModel()
        {
            RefreshInterfaceLst();
            QueryCommand = new Command(Query);
            AddNewCommand = new Command(AddNew);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
           
        }
        private void RefreshInterfaceLst(string searchTxt=null)
        {
            var curControlDefaultVals = DbHelper.db.Queryable<DbModels.dv_control_default_value>()
                .ToList();

            ControlDefaultValLst = new ObservableCollection<DbModels.dv_control_default_value>();
            
            foreach (var item in curControlDefaultVals)
            {
                
                //模糊搜索，控件名称
                if (string.IsNullOrEmpty(searchTxt))
                {
                    ControlDefaultValLst.Add(item);
                }
                else
                {
                    if(Utli.StringContains(item.control_internal_name, searchTxt))
                    {
                        ControlDefaultValLst.Add(item);
                    }
                }
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
            
            ControlDefaultValEditPopup ce = new ControlDefaultValEditPopup(null);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                RefreshInterfaceLst();
            }
            MainWindow.RemoveMask();
        }
        private void Edit(object o)
        {
            var selectedInterface = o as DbModels.dv_control_default_value;
           
            ControlDefaultValEditPopup ce = new ControlDefaultValEditPopup(selectedInterface);
            MainWindow.AddMask();
          
            if (ce.ShowDialog() == true)
            {
                RefreshInterfaceLst();
            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedCrane = o as DbModels.dv_control_default_value;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该Request吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_control_default_value>().Where(x=>x.control_internal_name==selectedCrane.control_internal_name)
                .ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                controlDefaultValLst.Remove(selectedCrane);
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
    public class RequestInterfaceModel1
    {
        public int Id { get; set; }
        public string RequestInternalName { get; set; }
        public string DestSystemName { get; set; }
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
