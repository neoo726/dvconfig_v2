using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using CMSCore;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;
using static DataView_Configuration.ControlDefaultValueModel;

namespace DataViewConfig.ViewModels
{
    internal class NewControlDefaultValEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        
        public int DefaultID { get; set; }
        public ObservableCollection<DbModels.dv_control_conf> ControlInternalNameLst { get; set; }
        public DbModels.dv_control_conf SelectedControl { get; set; }
        public string SelectedControlInternalName { get; set; }
        public bool IsFillDrawComboxList { get; set; }
        public bool FillWithoutCondition { get; set; }
        public bool FillAllCraneByDb { get; set; }
        public bool FillAllRcsByDb { get; set; }
        public bool FillAllBlockByDb { get; set; }
        public bool FillAllBayByDb { get; set; }
        public string FillDbSheetName { get; set; }
        public ObservableCollection<DbModels.dv_tag> TagInternalNameLst { get; set; }
        public string DefaultConditionTagInternalName { get; set; }
      
        public string DefaultConditionVal { get; set; }
        public string DefaultConditionJsonPath { get; set; }
        public DbModels.dv_tag DefaultContentTag { get; set; }
        public string DefaultConditionTagArrayMacro { get; set; }
        public string DefaultContentTagArrayMacro { get; set; }
       
        public string DefaultContentJsonPath { get; set; }
        public bool HasCondition { get; set; }
        public bool IsDefaultConditionTagArray { get; set; }
        public bool IsDefaultConditionTagJson { get; set; }
        public bool IsDefaultContentTagArray { get; set; }
        public bool IsDefaultContentTagJson { get; set; }
        public bool IsAddNew { get; set; }
        public int m_SystemId { get; set; }
        private List<DbModels.dv_tag> dbTagLst { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }

        public Command SelectTagCommand { get; set; }
        #endregion
        public NewControlDefaultValEditPopupViewModel(DbModels.dv_control_default_value dvControlDefault,int systemId=0)
        {
            m_SystemId = systemId;
            dbTagLst = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();

            var controlNameLst = DbHelper.db.Queryable<DbModels.dv_control_conf>().ToList();
            TagInternalNameLst = new ObservableCollection<DbModels.dv_tag>(dbTagLst);
            ControlInternalNameLst = new ObservableCollection<DbModels.dv_control_conf>(controlNameLst);
            //新增
            if (dvControlDefault == null)
            {
                this.IsAddNew = true;
                this.FillWithoutCondition = true;
                this.HasCondition = false;
            }
            //编辑已有
            else
            {
                this.IsAddNew = false;
                this.DefaultID = dvControlDefault.id;
                this.SelectedControl = controlNameLst.Where(x=>x.dv_control_internal_name== dvControlDefault.control_internal_name).First();
                if (this.SelectedControl != null)
                {
                    SelectedControlInternalName = this.SelectedControl.dv_control_internal_name;
                }
                //填充类型为数据库
                this.IsFillDrawComboxList = dvControlDefault.fill_type == "db";
                if (this.IsFillDrawComboxList)
                {
                    this.FillDbSheetName = dvControlDefault.fill_condition;
                    switch (this.FillDbSheetName.ToLower())
                    {
                        case "crane":
                            FillAllCraneByDb = true;
                            FillAllRcsByDb = false;
                            FillAllBlockByDb = false;
                            FillAllBayByDb = false;
                            break;
                        case "block":
                            FillAllCraneByDb = false;
                            FillAllRcsByDb = false;
                            FillAllBlockByDb = true;
                            FillAllBayByDb = false;
                            break;
                        case "bay":
                            FillAllCraneByDb = false;
                            FillAllRcsByDb = false;
                            FillAllBlockByDb = false;
                            FillAllBayByDb = true;
                            break;
                        case "rcs":
                            FillAllCraneByDb = false;
                            FillAllRcsByDb = true;
                            FillAllBlockByDb = false;
                            FillAllBayByDb = false;
                            break;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(dvControlDefault.fill_condition))
                    {
                        this.FillWithoutCondition = true;
                        HasCondition = false;
                    }
                    else
                    {
                        try
                        {
                            var fillCondition = JsonConvert.DeserializeObject<DataView_Configuration.ControlDefaultValueModel.FillCondition>(dvControlDefault.fill_condition);
                            HasCondition = true;
                            this.DefaultConditionTagInternalName = fillCondition.tag_internal_name;
                            this.IsDefaultConditionTagJson = fillCondition.is_json_tag;
                            this.DefaultConditionJsonPath = fillCondition.tag_json_path;
                            this.DefaultConditionVal = fillCondition.tag_val;
                            this.IsDefaultConditionTagArray = fillCondition.is_array_tag;
                            this.DefaultConditionTagArrayMacro = fillCondition.array_index_macro;
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show($"无法解析数据库中存储的填充条件！{ex.ToString()}");
                        }
                    }
                    try
                    {
                        var fillContent = JsonConvert.DeserializeObject<DataView_Configuration.ControlDefaultValueModel.FillContent>(dvControlDefault.fill_content);
                        
                        this.DefaultContentTag = dbTagLst.Where(x=>x.tag_internal_name == fillContent.tag_internal_name).FirstOrDefault();
                        this.IsDefaultContentTagJson = fillContent.is_json_tag;
                        this.DefaultContentJsonPath = fillContent.tag_json_path;
                        this.IsDefaultContentTagArray = fillContent.is_array_tag;
                        this.DefaultContentTagArrayMacro = fillContent.array_index_macro;
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show($"无法解析数据库中存储的填充内容！{ex.ToString()}");
                    }
                }
            }
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);

            SelectTagCommand = new Command(SelectTag);
        }
        private void SelectTag(object o)
        {
            var tagName = "";
            var selectedNewTagInternalName = "";
            switch (o.ToString().ToLower())
            {
                case "condition_tag":
                    tagName = DefaultConditionTagInternalName;
                    break;
                case "content_tag":
                    tagName = DefaultContentTag.tag_internal_name;
                    break;
            }
            TagSelectPopup ce = new TagSelectPopup(tagName);
            //MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                selectedNewTagInternalName = (ce.DataContext as ViewModels.TagSelectPopupViewModel).SelectedTagInternalName;
                switch (o.ToString().ToLower())
                {
                    case "condition_tag":
                      DefaultConditionTagInternalName = selectedNewTagInternalName;
                        break;
                    case "content_tag":
                        DefaultContentTag = dbTagLst.Where(x => x.tag_internal_name == selectedNewTagInternalName).FirstOrDefault();
                        break;
                }
            }
            //MainWindow.RemoveMask();

        }
        private void Confirm(object o)
        {

            try
            {
                DbModels.dv_control_default_value dvControlDefault = new DbModels.dv_control_default_value();
                if (this.SelectedControl == null)
                {
                    System.Windows.MessageBox.Show("请选择控件名称！"); return;
                }
                dvControlDefault.control_internal_name = this.SelectedControl.dv_control_internal_name;
                dvControlDefault.system_id = m_SystemId;
                if (this.IsFillDrawComboxList)
                {
                    dvControlDefault.fill_type = "db";
                    if (this.FillAllCraneByDb)
                    {
                        dvControlDefault.fill_condition = "crane";
                    }
                    else if (this.FillAllRcsByDb)
                    {
                        dvControlDefault.fill_condition = "rcs";
                    }
                    else if (this.FillAllBlockByDb)
                    {
                        dvControlDefault.fill_condition = "block";
                    }
                    else if (this.FillAllBayByDb)
                    {
                        dvControlDefault.fill_condition = "bay";
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("请选择要填充的数据类型！");
                        return;
                    }
                }
                else
                {
                    dvControlDefault.fill_type = "tag";
                    //有条件
                    if (!this.FillWithoutCondition)
                    {
                        if (string.IsNullOrEmpty(this.DefaultConditionTagInternalName) || string.IsNullOrEmpty(DefaultConditionVal))
                        {
                            System.Windows.MessageBox.Show("请输入条件中的点名和点值！"); return;
                        }
                        if (this.IsDefaultConditionTagArray && string.IsNullOrEmpty(this.DefaultConditionTagArrayMacro))
                        {
                            System.Windows.MessageBox.Show("请输入数组索引相关的宏名称！"); return;
                        }
                        var fillCondition = new DataView_Configuration.ControlDefaultValueModel.FillCondition();
                        fillCondition.tag_internal_name = this.DefaultConditionTagInternalName;
                        fillCondition.is_json_tag = this.IsDefaultConditionTagJson;
                        fillCondition.tag_json_path = this.DefaultConditionJsonPath;
                        fillCondition.tag_val = this.DefaultConditionVal.ToString();
                        fillCondition.is_array_tag = this.IsDefaultConditionTagArray;
                        fillCondition.array_index_macro = this.DefaultConditionTagArrayMacro;
                        dvControlDefault.fill_condition = JsonConvert.SerializeObject(fillCondition);
                    }
                    //无条件
                    else
                    {
                        if (this.DefaultContentTag == null)
                        {
                            System.Windows.MessageBox.Show("请选择点名！"); return;
                        }
                        dvControlDefault.fill_condition = "";
                        if (this.IsDefaultContentTagArray && string.IsNullOrEmpty(this.DefaultContentTagArrayMacro))
                        {
                            System.Windows.MessageBox.Show("请输入数组索引相关的宏名称！"); return;
                        }
                    }
                    //填充内容
                    var fillContent = new DataView_Configuration.ControlDefaultValueModel.FillContent();
                    fillContent.tag_internal_name = this.DefaultContentTag.tag_internal_name;
                    fillContent.is_json_tag = this.IsDefaultContentTagJson;
                    if (fillContent.is_json_tag && string.IsNullOrEmpty(this.DefaultContentJsonPath))
                    {
                        System.Windows.MessageBox.Show("请输入JSON字段路径！"); return;
                    }
                    fillContent.tag_json_path = this.DefaultContentJsonPath;
                    fillContent.is_array_tag = this.IsDefaultContentTagArray;
                    if (fillContent.is_array_tag && string.IsNullOrEmpty(this.DefaultContentTagArrayMacro))
                    {
                        System.Windows.MessageBox.Show("请输入数组索引相关的宏名称！"); return;
                    }
                    fillContent.array_index_macro = this.DefaultContentTagArrayMacro;
                    dvControlDefault.fill_content = JsonConvert.SerializeObject(fillContent);
                }

                int affectedRow = 0;
                //添加新的
                if (this.IsAddNew)
                {
                    if (this.SelectedControl == null)
                    {
                        System.Windows.MessageBox.Show("请选择控件名称（内部）！"); return;
                    }
                    affectedRow = DbHelper.db.Insertable<DbModels.dv_control_default_value>(dvControlDefault)
                  .ExecuteCommand();
                }
                //更新已有的
                else
                {
                    if (this.SelectedControl == null)
                    {
                        System.Windows.MessageBox.Show("请选择控件名称（内部）！"); return;
                    }
                    dvControlDefault.id = this.DefaultID;
                    affectedRow = DbHelper.db.Updateable<DbModels.dv_control_default_value>(dvControlDefault)
                   .Where(x => x.id == DefaultID).ExecuteCommand();
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
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("保存失败！" + ex.ToString());
                LogHelper.Error($"[Confirm Contol Edit]{ex.ToString()}");
            }  
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = false;
        }
    }
}
