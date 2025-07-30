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
    internal class BayEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
       
        public string BayID { get; set; }
        public int GantryPosition { get; set; }
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public bool IsAddNew { get; set; }
        public DbModels.bay originBay { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }
        #endregion
        public BayEditPopupViewModel(BayModel curBay)
        {
            if (curBay == null)
            {
                IsAddNew = true;
            }
            else
            {
                originBay = DbHelper.db.Queryable<DbModels.bay>().Where(x=>x.bay_no==curBay.BayID&&x.block_id==curBay.BlockID).First();
                BayID = curBay.BayID;
                GantryPosition = curBay.GantryPosition;

                BlockID = curBay.BlockID;
                BlockName = curBay.BlockName;
            }
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            if (string.IsNullOrEmpty(BayID))
            {
                System.Windows.MessageBox.Show("请输入贝位ID！"); return;
            }
            if (GantryPosition==default)
            {
                System.Windows.MessageBox.Show("请输入贝位标定大车位置！"); return;
            }
            var newBay = new DbModels.bay();
            newBay.bay_no = BayID;
            newBay.gantry_position = GantryPosition;
            newBay.block_id = BlockID;
           
            int affectedRow = 0;
            //添加新的
            if (this.IsAddNew)
            {
                var existedBlockCount = DbHelper.db.Queryable<DbModels.bay>().Count(x => x.block_id == BlockID && x.bay_no == BayID);
                if (existedBlockCount>0)
                {
                    System.Windows.MessageBox.Show("已存在重复的贝位ID！"); return;
                }
                affectedRow = DbHelper.db.Insertable<DbModels.bay>(newBay)
              .ExecuteCommand();
            }
            //更新已有的
            else
            {
                affectedRow = DbHelper.db.Updateable<DbModels.bay>(newBay)
               .Where(x=>x.id==originBay.id).ExecuteCommand();
            }
            if (affectedRow > 0)
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
