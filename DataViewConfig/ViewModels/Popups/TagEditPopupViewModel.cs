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
    internal class TagEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string tagInternalName;
        public string TagInternalName
        {
            get => tagInternalName;
            set {
                tagInternalName = value; OnPropertyChanged("TagInternalName");
            }
        }
        private string tagDesc;
        public string TagDesc
        {
            get => tagDesc;
            set
            {
                tagDesc = value; OnPropertyChanged("TagDesc");
            }
        }
        private string tagName;
        public string TagName
        {
            get => tagName;
            set
            {
                tagName = value; OnPropertyChanged("TagName");
            }
        }
        private TagDataType curTagDataType;
        public TagDataType CurTagDataType
        {
            get => curTagDataType;
            set
            {
                curTagDataType = value; OnPropertyChanged("CurTagDataType");
            }
        }
        private TagPostfixType curTagPostfixType;
        public TagPostfixType CurTagPostfixType
        {
            get => curTagPostfixType;
            set
            {
                curTagPostfixType = value; OnPropertyChanged("CurTagPostfixType");
            }
        }
        private string relatedMacroName;
        public string RelatedMacroName
        {
            get => relatedMacroName;
            set
            {
                relatedMacroName = value; OnPropertyChanged("RelatedMacroName");
            }
        }
        private string relatedTagInternalName;
        public string RelatedTagInternalName
        {
            get => relatedTagInternalName;
            set
            {
                relatedTagInternalName = value; OnPropertyChanged("RelatedTagInternalName");
            }
        }
        private int tagId;
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        #endregion
        public TagEditPopupViewModel(TagModel tagModel)
        {
            this.tagId = tagModel.Id;
            this.TagInternalName = tagModel.TagInternalName;
            this.TagName = tagModel.TagName;
            this.TagDesc = tagModel.Desc;
            this.CurTagPostfixType = tagModel.Postfix;
            this.CurTagDataType=tagModel.DataType;
            this.RelatedMacroName = tagModel.RelatedMacroName;
            this.RelatedTagInternalName = tagModel.RelatedTagInternalName;

            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            DbModels.dv_tag c =new DbModels.dv_tag()
            {
                 tag_internal_name=this.tagInternalName,
                 tag_name=this.TagName,
                  tag_desc=this.TagDesc,
                  data_type_id=(int)this.CurTagDataType,
                  postfix_type_id=(int)this.CurTagPostfixType,
                  related_macro_name=this.RelatedMacroName,
                  related_tag_internal_name=this.RelatedTagInternalName,
            };
            int affectedRow = DbHelper.db.Updateable<DbModels.dv_tag>(c)
               .Where(x => x.id == this.tagId).ExecuteCommand();
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
