using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class CommonReturnEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int returnCodeID;
        public int ReturnCodeID
        {
            get => returnCodeID;
            set {
                returnCodeID = value; OnPropertyChanged("ReturnCodeID");
            }
        }
        private string returnVal;
        public string ReturnVal
        {
            get => returnVal;
            set
            {
                returnVal = value; OnPropertyChanged("ReturnVal");
            }
        }
        private string returnDescZh;
        public string ReturnDescZh
        {
            get => returnDescZh;
            set
            {
                returnDescZh = value; OnPropertyChanged("ReturnDescZh");
            }
        }
        private string returnDescEn;
        public string ReturnDescEn
        {
            get => returnDescEn;
            set
            {
                returnDescEn = value; OnPropertyChanged("ReturnDescEn");
            }
        }
        private ObservableCollection<DbModels.dv_system> dvSystemLst { get; set; }
        public ObservableCollection<DbModels.dv_system> DvSystemLst
        {
            get => dvSystemLst;
            set { dvSystemLst = value; OnPropertyChanged("DvSystemLst"); }
        }
        private int systemId;
        public int SystemId
        {
            get => systemId;
            set
            {
                systemId = value; OnPropertyChanged("SystemId");
            }
        }
        private bool isSuccessFlag;
        public bool IsSuccessFlag
        {
            get => isSuccessFlag;
            set
            {
                isSuccessFlag = value; OnPropertyChanged("IsSuccessFlag");
            }
        }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        #endregion
        public CommonReturnEditPopupViewModel(DbModels.dv_request_return_code returnCode)
        {
            this.ReturnCodeID = returnCode.id;
            this.ReturnVal = returnCode.return_value;
            this.ReturnDescEn = returnCode.return_desc_en;
            this.ReturnDescZh = returnCode.return_desc_zh;
            //this.SystemName = Utli.ConvertToEnum<RequestSystemEnum>(returnCode.system_name);
            this.isSuccessFlag = returnCode.is_success;

            var curSystemLst = DbHelper.db.Queryable<DbModels.dv_system>().ToList();
            DvSystemLst = new ObservableCollection<DbModels.dv_system>(curSystemLst);

            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            DbModels.dv_request_return_code c = new DbModels.dv_request_return_code()
            {
                id = this.returnCodeID,
                return_value=this.ReturnVal,
                return_desc_en=this.returnDescEn,
                return_desc_zh=this.returnDescZh,
               
                system_id=this.systemId,
                is_success= this.isSuccessFlag,
            };
            int affectedRow = DbHelper.db.Updateable<DbModels.dv_request_return_code>(c)
               .Where(x => x.id == c.id).ExecuteCommand();
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
            win.DialogResult = true;
        }
    }
}
