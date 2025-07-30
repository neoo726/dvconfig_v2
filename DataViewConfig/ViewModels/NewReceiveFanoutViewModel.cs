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
    internal class NewReceiveFanoutViewModel : INotifyPropertyChanged
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
        #region 输入值 
        private ObservableCollection<string> cacheTagLst;
        public ObservableCollection<string> CacheTagLst
        {
            get => cacheTagLst;
            set { cacheTagLst = value; OnPropertyChanged("CacheTagLst"); }
        }
        private ObservableCollection<string> fullStoreTagLst;
        public ObservableCollection<string> FullStoreTagLst
        {
            get => fullStoreTagLst;
            set { fullStoreTagLst = value; OnPropertyChanged("FullStoreTagLst"); }
        }
        private string receiveMsgType;
        public string ReceiveMsgType
        {
            get => receiveMsgType;
            set { receiveMsgType = value; OnPropertyChanged("ReceiveMsgType"); }
        }
        private string deviceFiledName;
        public string DeviceFiledName
        {
            get => deviceFiledName;
            set { deviceFiledName = value; OnPropertyChanged("DeviceFiledName"); }
        }
        private string cacheTagInternalName;
        public string CacheTagInternalName
        {
            get => cacheTagInternalName;
            set { cacheTagInternalName = value; OnPropertyChanged("CacheTagInternalName"); }
        }
        private string fullStoreTagInternalName;
        public string FullStoreTagInternalName
        {
            get => fullStoreTagInternalName;
            set { fullStoreTagInternalName = value; OnPropertyChanged("FullStoreTagInternalName"); }
        }
        private DvReceiveTypeEnum receiveTypeName;
        public DvReceiveTypeEnum ReceiveTypeName
        {
            get => receiveTypeName;
            set { receiveTypeName = value; OnPropertyChanged("ReceiveTypeName"); }
        }
        private RequestSystemEnum receiveSystemName;
        public RequestSystemEnum ReceiveSystemName
        {
            get => receiveSystemName;
            set { receiveSystemName = value; OnPropertyChanged("ReceiveSystemName"); }
        }
        private DvReceiveStoreTypeEnum receiveStorType;
        public DvReceiveStoreTypeEnum ReceiveStorType
        {
            get => receiveStorType;
            set { receiveStorType = value; OnPropertyChanged("ReceiveStorType"); }
        }
        private DvReceiveWriteTypeEnum receiveWriteType;
        public DvReceiveWriteTypeEnum ReceiveWriteType
        {
            get => receiveWriteType;
            set { receiveWriteType = value; OnPropertyChanged("ReceiveWriteType"); }
        }
        private string cacheWriteCondition;
        public string CacheWriteCondition
        {
            get => cacheWriteCondition;
            set { cacheWriteCondition = value; OnPropertyChanged("CacheWriteCondition"); }
        }
        #endregion

        public NewReceiveFanoutViewModel()
        {
            ReceiveSystemName = RequestSystemEnum.RCCS;
            ReceiveStorType = DvReceiveStoreTypeEnum.CACHE;
            ReceiveWriteType = DvReceiveWriteTypeEnum.WRITE_BY_TAG_INTERNAL_NAME;

            //获取参数点
            var dvTagLst = DbHelper.db.Queryable<DbModels.dv_tag>()
              .ToList();
            CacheTagLst = new ObservableCollection<string>();
            FullStoreTagLst = new ObservableCollection<string>();
            foreach (var item in dvTagLst)
            {
                if (item.tag_internal_name.Contains("ms_"))
                {
                    CacheTagLst.Add(item.tag_internal_name);
                    FullStoreTagLst.Add(item.tag_internal_name);
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
            //this.ParamIdLst =
            //    String.Join(",", (paramSelectPopup.DataContext as ViewModels.ParamSelectedPopupViewModel).paramIdLst.ToArray());   
        }
        private void EditReturnVal(object o)
        {
            Pages.Popups.RequestReturnPopup rrp = new Pages.Popups.RequestReturnPopup();
            rrp.ShowDialog();
            var returValLst = (rrp.DataContext as ViewModels.RequestReturnPopupViewModel).ReturnValLst;
            //this.RequestReturnVal = returValLst.Count>0? JsonConvert.SerializeObject(returValLst):"";
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddInterface(object o)
        {
            var dv_receive = new DbModels.dv_receive_fanout();
            if (string.IsNullOrEmpty(receiveMsgType))
            {
                MessageBox.Show("接口名称不能为空！");
                return;
            }
            dv_receive.msg_type = this.ReceiveMsgType;
            dv_receive.device_field = this.DeviceFiledName;
           
            dv_receive.receive_type_id = (byte)this.ReceiveTypeName;
            dv_receive.store_type_id = (byte)this.ReceiveStorType;
            dv_receive.write_type_id = (byte)this.ReceiveWriteType;
            dv_receive.cache_write_condition = JsonConvert.SerializeObject(this.CacheWriteCondition);
            dv_receive.cache_tag_internal_name = this.CacheTagInternalName;
            dv_receive.full_store_tag_internal_name = this.FullStoreTagInternalName;
            
            int affectedRow=DbHelper.db.Insertable<DbModels.dv_receive_fanout>(dv_receive).ExecuteCommand();
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
