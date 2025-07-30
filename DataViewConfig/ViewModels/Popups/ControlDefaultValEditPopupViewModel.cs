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
using SqlSugar;

namespace DataViewConfig.ViewModels
{
    internal class ControlDefaultValEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
       
        public int DefaultID { get; set; }
        public ObservableCollection<DbModels.dv_control_conf> ControlLst { get; set; }
        public string ControlInternalName { get; set; }

        public bool IsFillDrawComboxList { get; set; }

        public bool FillWithoutCondition { get; set; }
        public bool FillAllCraneByDb { get; set; }
        public bool FillAllRcsByDb { get; set; }
        public bool FillAllBlockByDb { get; set; }
        public bool FillAllBayByDb { get; set; }
        public string FillDbSheetName { get; set; }
        public ObservableCollection<DbModels.dv_tag> TagLst { get; set; }
        public string DefaultConditionTagInternalName { get; set; }
        public bool IsdefaultConditionTagJson { get; set; }
        public string DefaultConditionVal { get; set; }
        public string DefaultConditionJsonPath { get; set; }
        public DbModels.dv_tag DefaultContentTag { get; set; }
        public bool IsdefaultContentTagJson { get; set; }
        public string DefaultContentJsonPath { get; set; }
        public bool IsAddNew { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }
        #endregion
        public ControlDefaultValEditPopupViewModel(DbModels.dv_control_default_value dvControlDefault)
        {
            var tagNameLst = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();

            var controlNameLst = DbHelper.db.Queryable<DbModels.dv_control_conf>().ToList();
            TagLst = new ObservableCollection<DbModels.dv_tag>(tagNameLst);
            ControlLst = new ObservableCollection<DbModels.dv_control_conf>(controlNameLst);
            if (dvControlDefault == null)
            {
                this.IsAddNew = true;
                this.FillWithoutCondition = true;
            }
            else
            {
                this.IsAddNew = false;
                this.DefaultID = dvControlDefault.id;
                this.ControlInternalName = dvControlDefault.control_internal_name;
                this.IsFillDrawComboxList = dvControlDefault.fill_type == "db";
                if (this.IsFillDrawComboxList)
                {
                    this.FillDbSheetName = dvControlDefault.fill_condition;
                    
                }
                else
                {
                    if (string.IsNullOrEmpty(dvControlDefault.fill_condition))
                    {
                        this.FillWithoutCondition = true;
                    }
                    else
                    {
                        var fillCondition = JsonConvert.DeserializeObject<DataView_Configuration.ControlDefaultValueModel.FillCondition>(dvControlDefault.fill_condition);
                        this.DefaultConditionTagInternalName = fillCondition.tag_internal_name;
                        this.IsdefaultConditionTagJson = fillCondition.is_json_tag;
                        this.DefaultConditionJsonPath = fillCondition.tag_json_path;
                        this.DefaultConditionVal = fillCondition.tag_val;
                    }
                    var fillContent= JsonConvert.DeserializeObject<DataView_Configuration.ControlDefaultValueModel.FillContent>(dvControlDefault.fill_content);
                    this.DefaultContentTag = tagNameLst.Where(x=>x.tag_internal_name== fillContent.tag_internal_name).FirstOrDefault();
                    this.IsdefaultContentTagJson = fillContent.is_json_tag;
                    this.DefaultContentJsonPath= fillContent.tag_json_path;
                }

            }
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
           
        }
        
        private void Confirm(object o)
        {
           
            DbModels.dv_control_default_value dvControlDefault= new DbModels.dv_control_default_value();
            if (string.IsNullOrEmpty(this.ControlInternalName))
            {
                System.Windows.MessageBox.Show("请选择控件名称！");return;
            }
            dvControlDefault.control_internal_name = this.ControlInternalName;
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
                }
            }
            else
            {
                dvControlDefault.fill_type = "tag";
                if (!this.FillWithoutCondition)
                {
                    if (string.IsNullOrEmpty(this.DefaultConditionTagInternalName) || string.IsNullOrEmpty(DefaultConditionVal))
                    {
                        System.Windows.MessageBox.Show("请输入条件中的点名和点值！");return;
                    }
                    var fillCondition = new DataView_Configuration.ControlDefaultValueModel.FillCondition();
                    fillCondition.tag_internal_name = this.DefaultConditionTagInternalName;
                    fillCondition.is_json_tag = this.IsdefaultConditionTagJson;
                    fillCondition.tag_json_path = this.DefaultConditionJsonPath;
                    fillCondition.tag_val = this.DefaultConditionVal.ToString();
                    dvControlDefault.fill_condition = JsonConvert.SerializeObject(fillCondition);
                }
                else
                {
                    dvControlDefault.fill_condition = "";
                }
                var fillContent = new DataView_Configuration.ControlDefaultValueModel.FillContent();
                fillContent.tag_internal_name = this.DefaultContentTag.tag_internal_name;
                fillContent.is_json_tag = this.IsdefaultContentTagJson;
                fillContent.tag_json_path = this.DefaultContentJsonPath;
                dvControlDefault.fill_content = JsonConvert.SerializeObject(fillContent);
            }
           
            int affectedRow = 0;
            //添加新的
            if (this.IsAddNew)
            {
                affectedRow = DbHelper.db.Insertable<DbModels.dv_control_default_value>(dvControlDefault)
              .ExecuteCommand();
            }
            //更新已有的
            else
            {
                dvControlDefault.id = this.DefaultID;
                affectedRow = DbHelper.db.Updateable<DbModels.dv_control_default_value>(dvControlDefault)
               .Where(x => x.id ==DefaultID).ExecuteCommand();
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
