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
    internal class TipEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        public string TipsName { get; set; }
        public bool TxtTipsChecked { get; set; }
        public string TipsContent { get; set; }
        public string TipsImageUrl { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        private int tipId { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectImageCommand { get; set; }
        public Command PreviewImageCommand { get; set; }
        #endregion
        public TipEditPopupViewModel(DbModels.config_tool_tips tipModel)
        {
            if (tipModel == null) return;
            ImageWidth = 50;
            ImageHeight = 50;
            tipId = tipModel.id;
            TipsName = tipModel.tips_name;
            TipsContent = tipModel.tips_zh;
            TipsImageUrl = tipModel.tips_zh_img_url;
            TxtTipsChecked = tipModel.tips_type == 1;

            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);

            SelectImageCommand = new Command(OpenImageSelection);
            PreviewImageCommand = new Command(PreviewImage);
        }
        private void PreviewImage(object p)
        {
            if (string.IsNullOrEmpty(TipsImageUrl))
            {
                return;
            }
            Controls.ImagePopup imgP = new Controls.ImagePopup(TipsImageUrl);
            imgP.Show();
        }
        private void Confirm(object o)
        {
            if (string.IsNullOrEmpty(TipsName))
            {
                System.Windows.MessageBox.Show("提示名称不能空！");return;
            }
            else if (TxtTipsChecked&&string.IsNullOrEmpty(TipsContent))
            {
                System.Windows.MessageBox.Show("提示内容不能空！"); return;
            }
            else if (!TxtTipsChecked && string.IsNullOrEmpty(TipsImageUrl))
            {
                System.Windows.MessageBox.Show("提示内容不能空！"); return;
            }
            DbModels.config_tool_tips c = new DbModels.config_tool_tips()
            {
                id = tipId,
                tips_name = TipsName,
                tips_zh = TipsContent,
                tips_zh_img_url = TipsImageUrl,
                tips_type = TxtTipsChecked ? 1 : 2,
            };
           
            int affectedRow = DbHelper.db.Updateable<DbModels.config_tool_tips>(c)
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
        //打开选择图片
        private void OpenImageSelection(object o)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            var appDirectory = System.Environment.CurrentDirectory.ToString();
            var imagePath = System.Environment.CurrentDirectory.ToString() + "\\Images";
            fileDialog.InitialDirectory = imagePath;
            //fileDialog.FileName = "Image|*.png;";
            fileDialog.Filter = "Image|*.png;*.jpg;*.svg;";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.TipsImageUrl = fileDialog.FileName.Replace(imagePath+"\\", "");
            }
        }
    }
}
