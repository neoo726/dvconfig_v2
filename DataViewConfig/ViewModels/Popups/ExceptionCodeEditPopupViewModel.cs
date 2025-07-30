using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class ExceptionCodeEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        private string originalExceptionCode { get; set; }
        public string ExceptionCode { get; set; }
        public int DvScreenID { get; set; }
        public string DvScreenName { get; set; }
        public string ExceptionDesc { get; set; }
        public ObservableCollection<DbModels.dv_screen_conf> DvScreenLst { get; set; }
        public string SelectedDvScreenName { get; set; }

        public bool IsAddNew { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }
        #endregion
        public ExceptionCodeEditPopupViewModel(ExceptionCodeModel curExceptionCode)
        {
            if (curExceptionCode == null)
            {
                IsAddNew = true;
            }
            else
            {
                originalExceptionCode = curExceptionCode.ExceptionCode;
                ExceptionCode = curExceptionCode.ExceptionCode;
                DvScreenID = curExceptionCode.DvScreenID;
                DvScreenName = curExceptionCode.DvScreenName;
                ExceptionDesc = curExceptionCode.ExceptionDesc;
                Task.Factory.StartNew(new Action(() =>
                {
                    var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>().ToList();
                    DvScreenLst = new ObservableCollection<DbModels.dv_screen_conf>(dvScreenLst);
                    var scr = DvScreenLst.Where(x => x.dv_screen_id == this.DvScreenID).FirstOrDefault();
                    if (scr == null)
                    {
                        SelectedDvScreenName = null;
                    }
                    else
                    {
                        SelectedDvScreenName = scr.dv_screen_internal_name;
                    }
                }));
            }
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void Confirm(object o)
        {
            if (string.IsNullOrEmpty(ExceptionCode))
            {
                System.Windows.MessageBox.Show("请输入异常代码！"); return;
            }
            if (ExceptionDesc == default)
            {
                System.Windows.MessageBox.Show("请输入异常代码描述！"); return;
            }
            var newExceptionCode = new DbModels.dv_exception_screen_map();
            newExceptionCode.exception_code = ExceptionCode;
            newExceptionCode.exception_desc = ExceptionDesc;
            newExceptionCode.dv_screen_id = this.DvScreenLst.Where(x=>x.dv_screen_internal_name== SelectedDvScreenName).FirstOrDefault().dv_screen_id;
           
            int affectedRow = 0;
            //添加新的
            if (this.IsAddNew)
            {
                var existedExceptionCodeCount = DbHelper.db.Queryable<DbModels.dv_exception_screen_map>().Count(x => x.exception_code == ExceptionCode );
                if (existedExceptionCodeCount > 0)
                {
                    System.Windows.MessageBox.Show("已存在重复的异常代码！"); return;
                }
                affectedRow = DbHelper.db.Insertable<DbModels.dv_exception_screen_map>(newExceptionCode)
              .ExecuteCommand();
            }
            //更新已有的
            else
            {
                if (originalExceptionCode != ExceptionCode)
                {
                    //修改了exceptionID
                    var existedExceptionCodeCount = DbHelper.db.Queryable<DbModels.dv_exception_screen_map>().Count(x => x.exception_code == ExceptionCode);
                    if (existedExceptionCodeCount > 0)
                    {
                        System.Windows.MessageBox.Show("已存在重复的异常代码！"); return;
                    }
                }
                DbHelper.db.BeginTran();
                DbHelper.db.Deleteable<DbModels.dv_exception_screen_map>().Where(x => x.exception_code == originalExceptionCode).ExecuteCommand();
                affectedRow = DbHelper.db.Insertable<DbModels.dv_exception_screen_map>(newExceptionCode).ExecuteCommand();
                DbHelper.db.CommitTran();

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
