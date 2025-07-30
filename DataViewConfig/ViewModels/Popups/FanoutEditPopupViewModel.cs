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
    internal class FanoutEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        public ECSCommType ECSComm { get; set; }
        public int FanoutID { get; set; }        
        public int RequestSystemId { get; set; }      
        public string MsgType { get; set; }
        public RequestSystemEnum SystemName { get; set; }
        public ObservableCollection<DbModels.dv_system> RequestSystemLst { get; set; }
        private string DeviceField { get; set; }

        public DvReceiveTypeEnum ReceiveType { get; set; }


        public DvReceiveStoreTypeEnum ReceiveStoreType { get; set; }


        public DvReceiveWriteTypeEnum ReceiveWriteType { get; set; }


        public string CacheTagInternalName { get; set; }

        public string FullStoreTagInternalName { get; set; }

        public ObservableCollection<string> TagNameLst { get; set; }

        public ObservableCollection<string> FullStoreTagLst { get; set; }

        public string CacheWriteConditionDeviceName { get; set; }

        public ObservableCollection<ReceiveCacheWriteConditionModel.MatchTag> CacheWriteConditionLst { get; set; }

        public bool IsAddNew { get; set; }

        public string FanoutDesc { get; set; }


        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command AddNewConditionCommand { get; set; }
        public Command DeleteConditionCommand { get; set; }
        #endregion
        public FanoutEditPopupViewModel(FanoutReceiveModel fanout)
        {
            var allTag = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();
            //初始化下拉框列表
            FullStoreTagLst = new ObservableCollection<string>(allTag.Where(x => x.data_type_id == (int)TagDataType.STRING_ARRAY).Select(x => x.tag_internal_name).ToList());

            this.TagNameLst = new ObservableCollection<string>(allTag.Where(x => x.tag_internal_name.Contains("ms_")).Select(x=>x.tag_internal_name).ToList());
            //add new
            if (fanout == null)
            {
                this.IsAddNew = true;
            }
            else
            {
                this.IsAddNew = false;

                FanoutID = fanout.Id;
                MsgType = fanout.MsgType;
                RequestSystemId = fanout.SystemId;
                DeviceField = fanout.DeviceField;
                ReceiveType = fanout.ReceiveType;
                ReceiveStoreType = fanout.ReceiveStoreType;
                ReceiveWriteType = fanout.ReceiveWriteType;
                CacheWriteConditionDeviceName = fanout.CacheWriteCondition!=null? fanout.CacheWriteCondition.field_name:"";
                CacheWriteConditionLst= fanout.CacheWriteCondition != null&& fanout.CacheWriteCondition.match_tag!=null ? new ObservableCollection<ReceiveCacheWriteConditionModel.MatchTag>(fanout.CacheWriteCondition.match_tag) : null;
                CacheTagInternalName = fanout.CacheTagInternalName;
                FullStoreTagInternalName = fanout.FullStoreTagInternalName;
                FanoutDesc = fanout.FanoutDesc;
            }
            RequestSystemLst = new ObservableCollection<DbModels.dv_system>(DbHelper.db.Queryable<DbModels.dv_system>()
              .ToList());

            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            AddNewConditionCommand = new Command(AddNewCondition);
            DeleteConditionCommand = new Command(DeleteCondition);
        }
        private void DeleteCondition(object o)
        {
            var conditionModel = o as ReceiveCacheWriteConditionModel.MatchTag;
            if (CacheWriteConditionLst == null || CacheWriteConditionLst.Count == 0)
            {
                return;
            }
            this.CacheWriteConditionLst.Remove(conditionModel);
        }
        private void AddNewCondition(object o)
        {
            if (CacheWriteConditionLst == null || CacheWriteConditionLst.Count == 0)
            {
                this.CacheWriteConditionLst = new ObservableCollection<ReceiveCacheWriteConditionModel.MatchTag>();
            }
            this.CacheWriteConditionLst.Add(new ReceiveCacheWriteConditionModel.MatchTag()
            {
                //value="pt",
                //tag_internal_name="ms_qcms_pt_task",
            });
        }
        private void Confirm(object o)
        {
            int affectedRow;
            var receiveCacheCondtion = new ReceiveCacheWriteConditionModel();
            if (this.ReceiveWriteType == DvReceiveWriteTypeEnum.WRITE_BY_WRITE_CONDITION)
            {
                receiveCacheCondtion.field_name = this.CacheWriteConditionDeviceName;
                receiveCacheCondtion.match_tag = new List<ReceiveCacheWriteConditionModel.MatchTag>(this.CacheWriteConditionLst);
            }
            //新增
            if (IsAddNew)
            {


                DbModels.dv_receive_fanout c = new DbModels.dv_receive_fanout()
                {
                    msg_type = this.MsgType,
                    device_field = this.DeviceField,
                    receive_type_id = (byte)this.ReceiveType,
                    store_type_id = (byte)this.ReceiveStoreType,
                    write_type_id = (byte)this.ReceiveWriteType,
                    cache_tag_internal_name = this.CacheTagInternalName,
                    cache_write_condition = JsonConvert.SerializeObject(receiveCacheCondtion),
                    dv_fanout_desc = this.FanoutDesc,
                    system_id = this.RequestSystemId,
                };
                affectedRow = DbHelper.db.Insertable<DbModels.dv_receive_fanout>(c).ExecuteCommand();
            }
            //修改已有的
            else
            {
                DbModels.dv_receive_fanout c = new DbModels.dv_receive_fanout()
                {
                    id = this.FanoutID,
                    msg_type = this.MsgType,
                    system_id = this.RequestSystemId,
                    device_field = this.DeviceField,
                    receive_type_id = (byte)this.ReceiveType,
                    store_type_id = (byte)this.ReceiveStoreType,
                    write_type_id = (byte)this.ReceiveWriteType,
                    cache_tag_internal_name = this.CacheTagInternalName,
                    cache_write_condition = JsonConvert.SerializeObject(receiveCacheCondtion),
                    dv_fanout_desc = this.FanoutDesc,
                };
                affectedRow = DbHelper.db.Updateable<DbModels.dv_receive_fanout>(c)
                   .Where(x => x.id == c.id).ExecuteCommand();
            }
            if (affectedRow >0)
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
            win.DialogResult = false;
        }
    }
}
