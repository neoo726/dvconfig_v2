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
    internal class ControlEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int controlID;
        public int ControlID
        {
            get => controlID;
            set
            {
                controlID = value; OnPropertyChanged("ControlID");
            }
        }
        private int controlAccessID;
        public int ControlAccessID
        {
            get => controlAccessID;
            set {
                controlAccessID = value; OnPropertyChanged("ControlAccessID");
            }
        }
        private string controlInternalName;
        public string ControlInternalName
        {
            get => controlInternalName;
            set
            {
                controlInternalName = value; OnPropertyChanged("ControlInternalName");
            }
        }
        private string controlDesc;
        public string ControlDesc
        {
            get => controlDesc;
            set
            {
                controlDesc = value; OnPropertyChanged("ControlDesc");
            }
        }
        private ControlType curControlType;
        public ControlType CurControlType
        {
            get => curControlType;
            set
            {
                curControlType = value; OnPropertyChanged("CurControlType");
            }
        }
        private string curScreenInternalName;
        public string CurScreenInternalName
        {
            get => curScreenInternalName;
            set
            {
                curScreenInternalName = value; OnPropertyChanged("CurScreenInternalName");
            }
        }
        private ObservableCollection<DbModels.dv_screen_conf> screenNameLst;
        public ObservableCollection<DbModels.dv_screen_conf> ScreenNameLst
        {
            get => screenNameLst;
            set { screenNameLst = value; OnPropertyChanged("ScreenNameLst"); }
        }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        #endregion
        public ControlEditPopupViewModel(ControlModel control)
        {
            //获取参数点
            var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
              .Where(x => !x.dv_screen_csw_name.Contains(","))
              .ToList();
            ScreenNameLst = new ObservableCollection<DbModels.dv_screen_conf>(dvScreenLst);
            this.ControlID = control.ControlID;
            this.ControlAccessID = control.AccessID;
            this.ControlInternalName = control.ControlInternalName;
            this.CurControlType=control.ControlType;
            this.CurScreenInternalName = control.ScreenInternalName;
            this.ControlDesc = control.ControlDesc;
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            var screenId = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
                .Where(x => x.dv_screen_internal_name == this.CurScreenInternalName).ToList()[0].dv_screen_id;

            DbModels.dv_control_conf c = new DbModels.dv_control_conf()
            {
                dv_control_id = this.ControlID,
                 dv_control_access_id= this.ControlAccessID,
                 dv_control_internal_name=this.ControlInternalName,
                 dv_screen_id= screenId,
                 dv_control_type_id=(int)this.CurControlType,
                 dv_control_desc=this.ControlDesc,
            };
            int affectedRow = DbHelper.db.Updateable<DbModels.dv_control_conf>(c)
               .Where(x => x.dv_control_id == c.dv_control_id).ExecuteCommand();
            if (affectedRow >0)
            {
                System.Windows.MessageBox.Show("保存成功！");
                var win = o as Window;
                win.DialogResult = true;
            }
            else
            {
                var win = o as Window;
                win.DialogResult = false;
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
