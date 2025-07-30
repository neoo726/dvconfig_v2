using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using DbModels;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class DvTipEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        public string TipsInternalName { get; set; }
      
        public string TipsEn { get; set; }
        public string TipsZh { get; set; }
       

        private int tipId { get; set; }
        private List<DbModels.dv_tips> dv_Tips { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectImageCommand { get; set; }
        public Command PreviewImageCommand { get; set; }
        #endregion
        public DvTipEditPopupViewModel(DbModels.dv_tips tipModel)
        {
            if (tipModel == null) return;

            tipId = tipModel.id;
            TipsInternalName = tipModel.tips_internal_name;
            TipsEn= tipModel.tips_en;
            TipsZh = tipModel.tips_zh;
            dv_Tips=DbHelper.db.Queryable<DbModels.dv_tips>().ToList(); 
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        
        private void Confirm(object o)
        {
            if (string.IsNullOrEmpty(TipsInternalName))
            {
                System.Windows.MessageBox.Show("提示名称不能空！");return;
            }
            else if (string.IsNullOrEmpty(TipsEn)|| string.IsNullOrEmpty(TipsZh))
            {
                System.Windows.MessageBox.Show("提示内容不能空！"); return;
            }
            else if (dv_Tips.Exists(x => x.tips_internal_name == TipsInternalName && x.id != tipId))
            {
                System.Windows.MessageBox.Show("提示名称不能重复！"); return;
            }
            DbModels.dv_tips c = new DbModels.dv_tips()
            {
                id = tipId,
                tips_internal_name = TipsInternalName,
                tips_zh = TipsZh,
                tips_en=TipsEn,
            };
            int affectedRow = DbHelper.db.Updateable<DbModels.dv_tips>(c)
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
