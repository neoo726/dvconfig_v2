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
using DbModels;
using Newtonsoft.Json;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DataViewConfig.ViewModels
{
    internal class CraneEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        public int CraneID { get; set; }
        public string CraneName { get; set; }
        public bool CraneTypeEnable { get; set; }
        public bool CraneGroupEnable { get; set; }
        public bool CraneIpEnable { get; set; }
        public DbModels.crane_type SelectedCraneType { get; set; }
        public DbModels.crane_group SelectedCraneGroup { get; set; }
        public string SelectedCraneTypeName { get; set; }
        public string SelectedCraneGroupName { get; set; }
        public ObservableCollection<string> CraneTypeNameLst { get; set; }
        public ObservableCollection<string> CraneGroupNameLst { get; set; }
        public string CraneIp { get; set; }
        private bool IsNew = false;
        private int originalCraneId = 0;
        public ObservableCollection<DbModels.crane_type> CraneTypeLst { get; set; }
        public ObservableCollection<DbModels.crane_group> CraneGroupLst { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        #endregion
        public CraneEditPopupViewModel(CraneModel crane,bool CraneTypeEnable,bool CraneGroupEnable,bool CraneIpEnable)
        {
            if (crane != null)
            {
                this.CraneID = crane.CraneID;
                this.CraneName = crane.CraneName;
                this.CraneIp = crane.CraneIP;
                this.originalCraneId = crane.CraneID;
            }
            else
            {
                IsNew = true;
            }
            this.CraneTypeEnable = CraneTypeEnable;
            this.CraneIpEnable = CraneIpEnable;
            this.CraneGroupEnable = CraneGroupEnable;
            if (CraneTypeEnable)
            {
                var craneTypeLst = DbHelper.db.Queryable<DbModels.crane_type>().ToList();
                CraneTypeLst = new ObservableCollection<DbModels.crane_type>(craneTypeLst);
                CraneTypeNameLst =new ObservableCollection<string>(CraneTypeLst.Select(x => x.crane_type_name).ToList());

                SelectedCraneType = craneTypeLst.Where(x => crane!=null&&x.crane_type_id == crane.CraneTypeID).FirstOrDefault();
                SelectedCraneTypeName = SelectedCraneType?.crane_type_name;
            }
            if (CraneGroupEnable) 
            {
                var craneGroupLst = DbHelper.db.Queryable<DbModels.crane_group>().ToList();
                CraneGroupLst = new ObservableCollection<DbModels.crane_group>(craneGroupLst);
                CraneGroupNameLst = new ObservableCollection<string>(CraneGroupLst.Select(x => x.group_name).ToList());

                SelectedCraneGroup = craneGroupLst.Where(x => crane != null && x.group_id == crane.CraneGroupID).FirstOrDefault();
                SelectedCraneGroupName = SelectedCraneGroup?.group_name;
            } 
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            try
            {
                DbModels.crane c = new DbModels.crane()
                {
                    crane_id = this.CraneID,
                    crane_name = this.CraneName                    
                };
                if (CraneTypeEnable)
                {
                    if (SelectedCraneTypeName == null)
                    {
                        MessageBox.Show("请选择设备机型！"); return;
                    }
                    c.crane_type_id = CraneTypeLst.Where(x => x.crane_type_name == SelectedCraneTypeName).FirstOrDefault().crane_type_id;
                }
                if (CraneGroupEnable)
                {
                    if (SelectedCraneGroupName == null)
                    {
                        MessageBox.Show("请选择设备类别！"); return;
                    }
                    c.group_id = CraneGroupLst.Where(x => x.group_name == SelectedCraneGroupName).FirstOrDefault().group_id;
                }
                if (CraneIpEnable)
                {
                    c.crane_ip = this.CraneIp;
                }
                if (!IsNew)
                {
                    //crane id有修改，需要检查是否有重复
                    if (originalCraneId != c.crane_id)
                    {
                        if (DbHelper.db.Queryable<DbModels.crane>().Where(x => x.crane_id == c.crane_id&&x.group_id==c.group_id).Count() > 0)
                        {
                            System.Windows.MessageBox.Show("已存在类型相同、ID重复的设备！"); return;
                        }
                    }
                    int affectedRow = DbHelper.db.Updateable<DbModels.crane>(c)
                   .Where(x => x.crane_id == originalCraneId).ExecuteCommand();
                    if (affectedRow > 0)
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
                //添加新的
                else
                {
                    if (DbHelper.db.Queryable<DbModels.crane>().Where(x => x.crane_id == c.crane_id && x.crane_name == c.crane_name).Count() > 0)
                    {
                        System.Windows.MessageBox.Show("已存在名称重复的设备！");return;
                    }
                    int affectedRow = DbHelper.db.Insertable<DbModels.crane>(c)
                   .ExecuteCommand();
                    if (affectedRow > 0)
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
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("UNIQUE constraint failed"))
                {
                    System.Windows.MessageBox.Show("保存设备ID时发生错误！可能是当前使用的配置数据库版本过低导致！");
                }
            }
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
    }
}
