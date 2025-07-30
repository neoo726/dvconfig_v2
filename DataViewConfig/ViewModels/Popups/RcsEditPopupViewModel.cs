using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using MiniExcelLibs;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class RcsEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int rcsID;
        public int RcsID
        {
            get => rcsID;
            set {
                rcsID = value; OnPropertyChanged("RcsID");
            }
        }
        private string rcsName;
        public string RcsName
        {
            get => rcsName;
            set
            {
                rcsName = value; OnPropertyChanged("RcsName");
            }
        }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        #endregion
        public RcsEditPopupViewModel(DbModels.rcs rcs)
        {
            this.RcsID = rcs.rcs_id;
            this.RcsName = rcs.rcs_name;
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            DbModels.rcs rcs =new DbModels.rcs()
            {
                 rcs_id= this.RcsID,
                 rcs_name=this.RcsName
            };
            int affectedRow = DbHelper.db.Updateable<DbModels.rcs>(rcs)
               .Where(x => x.rcs_id == rcs.rcs_id).ExecuteCommand();
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
