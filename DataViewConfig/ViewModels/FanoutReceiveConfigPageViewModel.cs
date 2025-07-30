using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig
{
    internal class FanoutReceiveConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //private ObservableCollection<FanoutReceiveModel> receiveFanoutLst;
        public ObservableCollection<FanoutReceiveModel> ReceiveFanoutLst { get; set; }
        private string selectedDirectTagInternalName;
        #region  Command
        public Command QueryCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command RefreshCommand { get; set; }
        #endregion

        public FanoutReceiveConfigPageViewModel()
        {
            RefreshGrid();
            QueryCommand = new Command(Query);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            AddNewCommand = new Command(AddNew);
        }
        private void RefreshGrid(string searchTxt=null)
        {
            ReceiveFanoutLst = new ObservableCollection<FanoutReceiveModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_receive_fanout>()
                .ToList();
            int i = 1;
            foreach (var item in curRequests)
            {
                FanoutReceiveModel rf = new FanoutReceiveModel();
                rf.Id = item.id;
                rf.MsgType = item.msg_type;
                //rf.SystemName = (RequestSystemEnum)Enum.Parse(typeof(RequestSystemEnum), item.system_name, true);
                rf.SystemId = item.system_id;
                rf.DeviceField = item.device_field;
                rf.ReceiveType = (DvReceiveTypeEnum)Enum.Parse(typeof(DvReceiveTypeEnum), item.receive_type_id.ToString(), true);
                rf.ReceiveWriteType = (DvReceiveWriteTypeEnum)Enum.Parse(typeof(DvReceiveWriteTypeEnum), item.write_type_id.ToString(), true); ;
                rf.ReceiveStoreType = (DvReceiveStoreTypeEnum)Enum.Parse(typeof(DvReceiveStoreTypeEnum), item.store_type_id.ToString(), true);
                rf.CacheWriteCondition = JsonConvert.DeserializeObject<ReceiveCacheWriteConditionModel>(item.cache_write_condition);
                rf.CacheTagInternalName = item.cache_tag_internal_name;
                rf.FullStoreTagInternalName = item.full_store_tag_internal_name;
                rf.FanoutDesc = item.dv_fanout_desc;
                //模糊搜索，接口msg_type/描述
                if (string.IsNullOrEmpty(searchTxt))
                {
                    ReceiveFanoutLst.Add(rf);
                }
                else
                {
                    if (Utli.StringContains(rf.MsgType, searchTxt)
                         || Utli.StringContains(rf.FanoutDesc, searchTxt)
                        )
                    {
                        ReceiveFanoutLst.Add(rf);
                    }
                }
                i++;
            }
        }
        //搜索
        private void Query(object o)
        {
            var searchtxt = o.ToString();
            //模糊搜索，接口msg_type/描述
            RefreshGrid(searchtxt.ToLower());
        }
        private void AddNew(object o)
        {

            FanoutEditPopup ce = new FanoutEditPopup(null);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                MainWindow.RemoveMask();
                RefreshGrid();

            }
        }
        private void Edit(object o)
        {
            var selectedFanout = o as FanoutReceiveModel;
            FanoutEditPopup ce = new FanoutEditPopup(selectedFanout);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                ReceiveFanoutLst[ReceiveFanoutLst.IndexOf(selectedFanout)].CacheTagInternalName= (ce.DataContext as ViewModels.FanoutEditPopupViewModel).CacheTagInternalName;

                RefreshGrid();
            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedCrane = o as FanoutReceiveModel;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该Fanout吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_receive_fanout>().Where(x=>x.id==selectedCrane.Id).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                ReceiveFanoutLst.Remove(selectedCrane);
            }
        }
        private void OpenPage(object o)
        {
            //反射创建
            //Type type = Assembly.GetExecutingAssembly().GetType("DataViewConfig.Pages." + o.ToString());
            ////避免重复创建UIElement实例
            //if (!PageDict.ContainsKey(o.ToString()))
            //{
            //    PageDict.Add(o.ToString(), (UIElement)Activator.CreateInstance(type));
            //}
            //MainContent = PageDict[o.ToString()];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MainContent"));
        }
        private void SaveRow(object o)
        {
            var receiveModel = o as FanoutReceiveModel;
            var cacheWriteCondition = JsonConvert.SerializeObject(receiveModel.CacheWriteCondition);
            var result = DbHelper.db
                .Updateable<DbModels.dv_receive_fanout>()
                .SetColumns(it => new DbModels.dv_receive_fanout() 
                    { 
                    system_id = receiveModel.SystemId,
                    device_field=receiveModel.DeviceField,
                    receive_type_id=(byte)receiveModel.ReceiveType,
                    write_type_id= (byte)receiveModel.ReceiveWriteType,
                    store_type_id= (byte)receiveModel.ReceiveStoreType,
                    cache_tag_internal_name=receiveModel.CacheTagInternalName,
                    full_store_tag_internal_name=receiveModel.FullStoreTagInternalName,
                })
                .Where(it => it.msg_type == receiveModel.MsgType).ExecuteCommand();
            if (result > 0)
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void DeleteRow(object o)
        {
            var requestModel = o as FanoutReceiveModel;
            int affectedRow=DbHelper.db.Deleteable<DbModels.dv_receive_fanout>()
                .Where(it=>it.id==requestModel.Id).ExecuteCommand();
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
    public class FanoutReceiveModel
    {
        public int Id { get; set; }
        public string FanoutName { get; set; }
        public string MsgType { get; set; }
        public int SystemId { get; set; }
        public string DeviceField { get; set; }
        public DvReceiveTypeEnum ReceiveType { get; set; }
        public DvReceiveStoreTypeEnum ReceiveStoreType { get; set; }
        public DvReceiveWriteTypeEnum ReceiveWriteType { get; set; }
        public ReceiveCacheWriteConditionModel CacheWriteCondition { get; set; }
        public string CacheTagInternalName { get; set; }
        public string FullStoreTagInternalName { get; set; }
        public string FanoutDesc { get; set; }
        public string RedisKey { get; set; }
        //public class ReturnValueModel
        //{
        //    public int  return_value { get; set; }
        //    public string  return_desc { get; set; }
        //}

    }
}
