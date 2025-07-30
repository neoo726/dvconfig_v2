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
    internal class BlockEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public string BlockDesc { get; set; }

        public int BlockMaxPos { get; set; }
        public int BlockMinPos { get; set; }
        public int BlockBayCount { get; set; }
        public bool IsAddNew { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }
        #endregion
        public BlockEditPopupViewModel(BlockModel curBlock)
        {
            if (curBlock == null)
            {
                IsAddNew = true;
            }
            else
            {
                BlockID = curBlock.BlockID;
                BlockName = curBlock.BlockName;
                BlockMaxPos = curBlock.BlockMaxPos;
                BlockMinPos = curBlock.BlockMinPos;
                BlockDesc = curBlock.BlockDesc;
            }
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            if (BlockID == default)
            {
                System.Windows.MessageBox.Show("请输入堆场ID！"); return;
            }
            if (string.IsNullOrEmpty(BlockName))
            {
                System.Windows.MessageBox.Show("请输入堆场名称！"); return;
            }
            var newBlock = new DbModels.block();
            newBlock.block_name = BlockName;
            newBlock.block_plc_id = BlockID;
            newBlock.block_id = BlockID;
            newBlock.block_desc = BlockDesc;
            newBlock.block_pos_min = BlockMinPos;
            newBlock.block_pos_max = BlockMaxPos;
            
            int affectedRow = 0;
            //添加新的
            if (this.IsAddNew)
            {
                var existedBlockCount = DbHelper.db.Queryable<DbModels.block>().Count(x => x.block_id == BlockID || x.block_name == BlockName);
                if (existedBlockCount>0)
                {
                    System.Windows.MessageBox.Show("已存在重复的堆场ID或堆场名称！"); return;
                }
                affectedRow = DbHelper.db.Insertable<DbModels.block>(newBlock)
              .ExecuteCommand();
            }
            //更新已有的
            else
            {
                affectedRow = DbHelper.db.Updateable<DbModels.block>(newBlock)
               .Where(x => x.block_id ==BlockID).ExecuteCommand();
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
