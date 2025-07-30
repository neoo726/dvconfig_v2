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
    internal class TagSelectPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        public string TagInternalName { get; set; }
        
      
        public string TagDesc { get; set; }


        public string TagName { get; set; }


        public TagDataType CurTagDataType { get; set; }


        public TagPostfixType CurTagPostfixType { get; set; }


        public string RelatedMacroName { get; set; }


        public string RelatedTagInternalName { get; set; }
        public ObservableCollection<TagModel> TagLst { get; set; }
        public string  SelectedTagInternalName { get; set; }
        public ObservableCollection<DbModels.dv_system> DvSystemLst { get; set; }
        public DbModels.dv_system  SelectedDvSystem { get; set; }
        private string curTagInternalName { get; set; }
        #region Commnad
        public Command QueryCommand { get; set; }
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command ResetCommand { get; set; }
        #endregion
        public TagSelectPopupViewModel(string tagInternalName)
        {
            curTagInternalName = tagInternalName;
            var tagLst = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();
            TagLst = new ObservableCollection<TagModel>();
            foreach (var item in tagLst)
            {
                var tag = new TagModel();
                tag.TagInternalName = item.tag_internal_name;
                tag.TagName = item.tag_name;
                tag.Desc = item.tag_desc;
                tag.IsSelected = item.tag_internal_name == curTagInternalName;
                TagLst.Add(tag);
            }

            var systemLst = DbHelper.db.Queryable<DbModels.dv_system>().ToList();
            DvSystemLst = new ObservableCollection<DbModels.dv_system>(systemLst);
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            QueryCommand = new Command(QueryTag);
            ResetCommand = new Command(ResetSearchInput);
        }
        private void ResetSearchInput(object o)
        {
            this.SelectedDvSystem = null;
        }
        private void RefreshTagLst(string searchStrTxt=null,int systemId=0)
        {
            var tagLst = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();
            TagLst = new ObservableCollection<TagModel>();
            foreach (var item in tagLst)
            {
                if (!string.IsNullOrEmpty(searchStrTxt))
                {
                    if (!item.tag_internal_name.Contains(searchStrTxt) && !item.tag_name.Contains(searchStrTxt) && !item.tag_desc.Contains(searchStrTxt))
                    {
                        continue;
                    }
                }
                if (systemId != 0 && item.related_system_id!=null&&!item.related_system_id.Contains(systemId.ToString()))
                {
                    continue;
                }
                var tag = new TagModel();
                tag.TagInternalName = item.tag_internal_name;
                tag.TagName = item.tag_name;
                tag.Desc = item.tag_desc;
                tag.IsSelected = item.tag_internal_name == curTagInternalName;
                TagLst.Add(tag);
            }
            TagLst.OrderBy(x => x.IsSelected);
        }
        private void QueryTag(object o)
        {
            RefreshTagLst(o.ToString(), SelectedDvSystem!=null?SelectedDvSystem.system_id:0);
        }
        private void Confirm(object o)
        {
            var selectedTagModel = new List<TagModel>(this.TagLst).Where(x => x.IsSelected).FirstOrDefault();
            if (selectedTagModel == null)
            {
                System.Windows.MessageBox.Show("请选择点名！");return;
            }
            SelectedTagInternalName = selectedTagModel.TagInternalName;
            var win = o as Window;
            win.DialogResult = true;
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = false;
        }
    }
}
