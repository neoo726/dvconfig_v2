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
    internal class ScreenEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        public int ScreenID { get; set; }
        public string SreenInternalName { get; set; }
        public string ScreenCswName { get; set; }
        public string ScreenDesc { get; set; }

        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectCswCommand { get; set; }
        #endregion
        public ScreenEditPopupViewModel(DbModels.dv_screen_conf screen)
        {
            this.ScreenID = screen.dv_screen_id;
            this.SreenInternalName = screen.dv_screen_internal_name;
            this.ScreenCswName = screen.dv_screen_csw_name;
            this.ScreenDesc = screen.dv_screen_desc;
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            SelectCswCommand = new Command(OpenSelectCswWin);
        }
        private void OpenSelectCswWin(object o)
        {
            ScreenCswSelectPopup scs = new ScreenCswSelectPopup(this.ScreenCswName);

            if (scs.ShowDialog() == true)
            {
                var vm = scs.DataContext as ScreenCswSelectPopupViewModel;
                ScreenCswName = string.Join(",", vm.SelectedCswNameLst);
            }
            else
            {

            }
        }
        private void Confirm(object o)
        {
            DbModels.dv_screen_conf c = new DbModels.dv_screen_conf()
            {
                 dv_screen_id = (short)this.ScreenID,
                 dv_screen_internal_name= this.SreenInternalName,
                 dv_screen_csw_name=this.ScreenCswName,
                 dv_screen_desc=this.ScreenDesc,
            };
            int affectedRow = DbHelper.db.Updateable<DbModels.dv_screen_conf>(c)
               .Where(x => x.dv_screen_id == c.dv_screen_id).ExecuteCommand();
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
