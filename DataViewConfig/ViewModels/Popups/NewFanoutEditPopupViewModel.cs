using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using CMSCore;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DataViewConfig.ViewModels
{
    internal class NewFanoutEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
       
        public int FanoutID { get; set; }
        public ECSCommType ECSComm { get; set; }
        public string FanoutName { get; set; }
        //public int RequestSystemId { get; set; }
        public string MsgType { get; set; }
        public RequestSystemEnum SystemName { get; set; }
        public ObservableCollection<DbModels.dv_system> RequestSystemLst { get; set; }
        public string DeviceField { get; set; }
        //public DvReceiveTypeEnum ReceiveType { get; set; }
        public string StrReceiveType { get; set; }
        public DvReceiveStoreTypeEnum ReceiveStoreType { get; set; }
        public DvReceiveWriteTypeEnum ReceiveWriteType { get; set; }
        public string CacheTagInternalName { get; set; }
        public string FullStoreTagInternalName { get; set; }
        public string DirectTagInternalName { get; set; }
        public DbModels.dv_tag WriteTag { get; set; }
        public ObservableCollection<string> TagNameLst { get; set; }
        public ObservableCollection<DbModels.dv_tag> TagLst { get; set; }
        public ObservableCollection<string> FullStoreTagLst { get; set; }
        public string SelectedParamInternalName { get; set; }
        public string CacheWriteConditionDeviceName { get; set; }
        public ObservableCollection<ReceiveCacheWriteConditionModel.MatchTag> CacheWriteConditionLst { get; set; }
        public ObservableCollection<ConditionMatchWriteTag> ConditionMatchTagLst { get; set; }
        public bool IsAddNew { get; set; }
        public string FanoutDesc { get; set; }
        public int SystemId { get; set; }
        //public string RestRouteUrl { get; set; } //接口地址
        public string RedisKey { get; set; } //Redis中的key，与prefix_key拼接而成。
        //public int RestPollingInterval { get; set; }  //轮询周期
        public bool ShowRestConfigs { get; set; }
        public bool ShowRedisConfigs { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command DeleteConditionCommand { get; set; }
        public Command AddNewConditionCommand { get; set; }
        
        public Command SelectTagCommand { get; set; }
        public Command SelectConditionWriteTagCommand { get; set; }
        #endregion
        public NewFanoutEditPopupViewModel(FanoutReceiveModel fanout,ECSCommType ECSComm,int systemId)
        {
            SystemId = systemId;
            this.ECSComm = ECSComm;
            ShowRestConfigs = ECSComm == ECSCommType.Rest;
            ShowRedisConfigs=ECSComm==ECSCommType.Redis;
            ConditionMatchTagLst = new ObservableCollection<ConditionMatchWriteTag>();
            var allTag = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();
            //初始化下拉框列表
            //FullStoreTagLst = new ObservableCollection<string>(allTag.Where(x => x.data_type_id == (int)TagDataType.STRING_ARRAY).Select(x => x.tag_internal_name).ToList());
            TagLst = new ObservableCollection<DbModels.dv_tag>(DbHelper.db.Queryable<DbModels.dv_tag>().OrderBy(x => x.tag_internal_name).ToList());
            this.TagNameLst = new ObservableCollection<string>(allTag.Where(x => x.tag_internal_name.Contains("ms_")).Select(x=>x.tag_internal_name).ToList());
            //add new
            if (fanout == null)
            {
                this.IsAddNew = true;
                ReceiveWriteType = DvReceiveWriteTypeEnum.WRITE_BY_TAG_INTERNAL_NAME;
            }
            else
            {
                this.IsAddNew = false;
                FanoutID = fanout.Id;
                FanoutName = fanout.FanoutName;
                FanoutDesc = fanout.FanoutDesc;
                MsgType = fanout.MsgType;
                //RequestSystemId = fanout.SystemId;
                DeviceField = fanout.DeviceField;
                StrReceiveType = fanout.ReceiveType.ToString();
                //ReceiveStoreType = fanout.ReceiveStoreType;
                ReceiveWriteType = fanout.ReceiveWriteType;
                CacheWriteConditionDeviceName = fanout.CacheWriteCondition!=null? fanout.CacheWriteCondition.field_name:"";
                CacheWriteConditionLst= fanout.CacheWriteCondition != null&& fanout.CacheWriteCondition.match_tag!=null ? new ObservableCollection<ReceiveCacheWriteConditionModel.MatchTag>(fanout.CacheWriteCondition.match_tag) : null;
                if(CacheWriteConditionLst!=null&&CacheWriteConditionLst.Count>0)
                {
                    foreach (var item in CacheWriteConditionLst)
                    {
                        ConditionMatchTagLst.Add(new ConditionMatchWriteTag()
                        {
                            FieldName = fanout.CacheWriteCondition.field_name,
                            FieldValue = item.field_value,
                            TargetTag = TagLst.Where(x => x.tag_internal_name == item.target_tag_internal_name).FirstOrDefault(),
                        });
                    }                    
                }
                CacheTagInternalName = fanout.CacheTagInternalName;
                DirectTagInternalName = fanout.CacheTagInternalName;
                WriteTag = TagLst.Where(x=>x.tag_internal_name== fanout.CacheTagInternalName).FirstOrDefault();
                //FullStoreTagInternalName = fanout.FullStoreTagInternalName;
                this.RedisKey = fanout.RedisKey;
            }
            RequestSystemLst = new ObservableCollection<DbModels.dv_system>(DbHelper.db.Queryable<DbModels.dv_system>()
              .ToList());

            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            AddNewConditionCommand = new Command(AddNewCondition);
            DeleteConditionCommand = new Command(DeleteCondition);

            SelectTagCommand = new Command(SelectTag);
            SelectConditionWriteTagCommand = new Command(SelectConditionTag);
        }
        private void SelectConditionTag(object o)
        {
            var id = Convert.ToInt16(o);
            var matchTag = this.ConditionMatchTagLst[id - 1];
            TagSelectPopup ce = new TagSelectPopup(matchTag.TargetTagInternalName);
            //MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                var selectedNewTagInternalName = (ce.DataContext as ViewModels.TagSelectPopupViewModel).SelectedTagInternalName;
              
                this.ConditionMatchTagLst[id - 1].TargetTagInternalName = selectedNewTagInternalName;
                var tmpConditionTagLst = ConditionMatchTagLst;
                ConditionMatchTagLst = null;
                ConditionMatchTagLst = tmpConditionTagLst;
            }
            //MainWindow.RemoveMask();

        }
        private void SelectTag(object o)
        {
            var tagName = "";
            var selectedNewTagInternalName = "";
            switch (o.ToString().ToLower())
            {
                case "direct_write_tag":
                    tagName = WriteTag.tag_internal_name ;
                    break;
            }
            TagSelectPopup ce = new TagSelectPopup(tagName);
            //MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                selectedNewTagInternalName = (ce.DataContext as ViewModels.TagSelectPopupViewModel).SelectedTagInternalName;
                switch (o.ToString().ToLower())
                {
                    case "direct_write_tag":
                        this.WriteTag = TagLst.Where(x => x.tag_internal_name == selectedNewTagInternalName).FirstOrDefault();
                        this.DirectTagInternalName = selectedNewTagInternalName;
                        this.CacheTagInternalName = selectedNewTagInternalName;
                        break;
                }
            }
            //MainWindow.RemoveMask();

        }
        private void DeleteCondition(object o)
        {
            var conditionModel = o as ConditionMatchWriteTag;
            if (CacheWriteConditionLst == null || CacheWriteConditionLst.Count == 0)
            {
                return;
            }
            this.ConditionMatchTagLst.Remove(conditionModel);
        }
        private void AddNewCondition(object o)
        {
            if (ConditionMatchTagLst == null || ConditionMatchTagLst.Count == 0)
            {
                this.ConditionMatchTagLst = new ObservableCollection<ConditionMatchWriteTag>();
            }
            this.ConditionMatchTagLst.Add(new ConditionMatchWriteTag()
            {
                Id=this.ConditionMatchTagLst.Count+1,
                FieldName = "field_name",
                FieldValue = "value",
                TargetTagInternalName = "target_tag_internal_name"
            }); 
        }
        private void Confirm(object o)
        {
            try
            {
                if (string.IsNullOrEmpty(FanoutName))
                {
                    MessageBox.Show("接口名称不能为空！"); return;
                }
                if (string.IsNullOrEmpty(MsgType))
                {
                    if(this.ECSComm == ECSCommType.MQ)
                    {
                        MessageBox.Show("msg_type不能为空！"); return;
                    }
                    else
                    {
                        MsgType = Utli.AddTimestamp(Utli.GenerateRandomString(10));
                    }
                }
                int affectedRow;
                var receiveCacheCondtion = new ReceiveCacheWriteConditionModel();
                if (this.ReceiveWriteType == DvReceiveWriteTypeEnum.WRITE_BY_WRITE_CONDITION)
                {
                    if (ConditionMatchTagLst.Count == 0)
                    {
                        MessageBox.Show("写入条件至少要有一个！"); return;
                    }
                    if (this.ConditionMatchTagLst.GroupBy(i => i.FieldValue).Any(g => g.Count() > 1))
                    {
                        MessageBox.Show("条件值不能重复！"); return;
                    }
                    if (this.ConditionMatchTagLst.GroupBy(i => i.TargetTag).Any(g => g.Count() > 1))
                    {
                        MessageBox.Show("写入点名不能重复！"); return;
                    }
                    receiveCacheCondtion.field_name = this.ConditionMatchTagLst[0].FieldName;
                    receiveCacheCondtion.match_tag = new List<ReceiveCacheWriteConditionModel.MatchTag>();
                    foreach (var item in ConditionMatchTagLst)
                    {
                        if (item.TargetTag == null)
                        {
                            MessageBox.Show("请选择正确的点名！"); return;
                        }
                        receiveCacheCondtion.match_tag.Add(new ReceiveCacheWriteConditionModel.MatchTag()
                        {
                            field_value = item.FieldValue,
                            target_tag_internal_name = item.TargetTag.tag_internal_name,
                        });
                    }
                }
                else
                {
                    if (this.WriteTag == null)
                    {
                        MessageBox.Show("请选择正确的点名！"); return;
                    }
                }
                if (IsAddNew)
                {
                    //判断FanoutName是否为空
                    if (DbHelper.db.Queryable<DbModels.dv_receive_fanout>().Where(x => x.fanout_name == this.FanoutName).Count() > 0)
                    {
                        MessageBox.Show("已存在重复的Fanout消息！"); return;
                    }
                    else if (DbHelper.db.Queryable<DbModels.dv_receive_fanout>().Where(x => x.system_id == this.SystemId && x.msg_type == this.MsgType).Count() > 0)
                    {
                        MessageBox.Show("在该交互对象下，已有重复的Msg type存在！"); return;
                    }
                    DbModels.dv_receive_fanout c = new DbModels.dv_receive_fanout()
                    {
                        fanout_name = this.FanoutName,
                        msg_type = this.MsgType,
                        device_field = this.DeviceField,
                        receive_type_id = this.StrReceiveType==null?(byte)DvReceiveTypeEnum.CRANE_ID:(byte)Utli.ConvertToEnum<DvReceiveTypeEnum>(this.StrReceiveType),
                        //store_type_id = (byte)this.ReceiveStoreType,
                        write_type_id = (byte)this.ReceiveWriteType,
                        cache_tag_internal_name = this.WriteTag?.tag_internal_name,
                        cache_write_condition = JsonConvert.SerializeObject(receiveCacheCondtion),
                        dv_fanout_desc = this.FanoutDesc,
                        system_id = this.SystemId,
                        metadata = JsonConvert.SerializeObject(new ReceiveFanoutMetadataModel
                        {
                             redis_postfix_key=this.RedisKey,
                        }),
                    };
                    affectedRow = DbHelper.db.Insertable<DbModels.dv_receive_fanout>(c).ExecuteCommand();
                }
                else
                {
                    DbModels.dv_receive_fanout c = new DbModels.dv_receive_fanout()
                    {
                        fanout_name = this.FanoutName,
                        id = this.FanoutID,
                        msg_type = this.MsgType,
                        system_id = this.SystemId,
                        device_field = this.DeviceField,
                        receive_type_id = (byte)Utli.ConvertToEnum<DvReceiveTypeEnum>(this.StrReceiveType),
                        //store_type_id = (byte)this.ReceiveStoreType,
                        write_type_id = (byte)this.ReceiveWriteType,
                        cache_tag_internal_name = this.WriteTag?.tag_internal_name,
                        cache_write_condition = JsonConvert.SerializeObject(receiveCacheCondtion),
                        dv_fanout_desc = this.FanoutDesc,
                        metadata = JsonConvert.SerializeObject(new ReceiveFanoutMetadataModel
                        {
                            redis_postfix_key = this.RedisKey,
                        }),
                    };
                    affectedRow = DbHelper.db.Updateable<DbModels.dv_receive_fanout>(c)
                       .Where(x => x.id == c.id).ExecuteCommand();
                }
                if (affectedRow > 0)
                {
                    System.Windows.MessageBox.Show("保存成功！");
                    var win = o as Window;
                    win.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("保存失败！" + ex.ToString());
                LogHelper.Error($"[Confirm Fanout Interface]{ex.ToString()}");
            }
            
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
    }
    public class ConditionMatchWriteTag
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string TargetTagInternalName { get; set; }
        public DbModels.dv_tag TargetTag { get; set; }
    }
}
