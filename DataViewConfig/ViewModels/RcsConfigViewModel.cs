using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using CMSCore;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class RcsConfigViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #region Command
        public Command OpenPageCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCraneCommand { get; set; }
        public Command QuickCreateCommand { get; set; }
        public Command QueryCommand { get; set; }
        public Command ResetCommand { get; set; }
        #endregion
        private ObservableCollection<DbModels.rcs> rcsLst;
        public ObservableCollection<DbModels.rcs> RcsLst 
        {
            get => rcsLst;
            set { rcsLst = value; OnPropertyChanged("RcsLst"); }
        }
        private string rcsPrefixFormat;
        public string RcsPrefixFormat
        {
            get => rcsPrefixFormat;
            set { rcsPrefixFormat = value; OnPropertyChanged("RcsPrefixFormat"); }
        }
        private int rcsCount;
        public int RcsCount
        {
            get => rcsCount;
            set { rcsCount = value; OnPropertyChanged("RcsCount"); }
        }
       
        public RcsConfigViewModel()
        {

             RefreshRcsLst();
            //OpenPageCommand = new Command(OpenPage);
            this.RcsPrefixFormat = "RCS{00}";
            QuickCreateCommand = new Command(QuickCreateRcs);
            EditCommand = new Command(EditRcs);
            DeleteCraneCommand = new Command(DeleteRcs);
            QueryCommand = new Command(QueryCrane);
            ResetCommand = new Command(QueryCrane);
        }

        private void QueryCrane(object o)
        {
            RefreshRcsLst();
        }
        private void RefreshRcsLst()
        {
            var rcsLst = DbHelper.db.Queryable<DbModels.rcs>().ToList();
            RcsLst = new ObservableCollection<DbModels.rcs>(rcsLst);
            RcsCount = RcsLst.Count;
        }
        private void EditRcs(object o)
        {
            var rcs = o as DbModels.rcs;
            RcsEditPopup ce = new RcsEditPopup(rcs);
            if (ce.ShowDialog() == true)
            {
                RefreshRcsLst();
            }
        }
        private void DeleteRcs(object o)
        {
            var selectedRcs = o as DbModels.rcs;
            if (selectedRcs == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该操作台吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.rcs>(selectedRcs).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除操作台失败！");
            }
            else
            {
                MessageBox.Show("删除操作台成功！");
                RcsLst.Remove(selectedRcs);
                EventBus.Instance.PublishConfigChange();


            }
        }
        private void QuickCreateRcs(object o )
        {
            try
            {
                if (string.IsNullOrEmpty(o.ToString()) || o.ToString().Equals("0"))
                {
                    MessageBox.Show("待生成的操作台数量不能为空！"); return;
                }
                if (!int.TryParse(o.ToString(), out int inputCraneCount))
                {
                    MessageBox.Show("输入格式错误！"); return;
                }
                Regex rg = new Regex(@"^[a-zA-z]+\{[0-9]+\}$");
                if (!rg.IsMatch(this.RcsPrefixFormat))
                {
                    MessageBox.Show("操作台名称前缀格式错误！");
                    return;
                }
                RcsLst = new ObservableCollection<DbModels.rcs>();
                var regexResult = Regex.Match(this.RcsPrefixFormat, @"\{(.*)\}", RegexOptions.IgnorePatternWhitespace);
                var formatStr = regexResult.Value;//结果中包含大括号
                var prefixStr = this.RcsPrefixFormat.Remove(this.RcsPrefixFormat.IndexOf("{"));

                for (int i = 0; i < inputCraneCount; i++)
                {

                    RcsLst.Add(new DbModels.rcs()
                    {
                        rcs_id = i + 1,
                        rcs_name = prefixStr + (i + 1).ToString().PadLeft(formatStr.Length - 2, '0'),
                    });
                }

                var curExitsRcsLst = DbHelper.db.Queryable<DbModels.rcs>().ToList();
                try
                {
                    DbHelper.db.BeginTran();//涉及到删除和添加，使用事务
                    int affectedRow;
                    if (curExitsRcsLst.Count > 0)
                    {
                        //先删再添加
                        var iRet = DbHelper.db.Deleteable<DbModels.rcs>(curExitsRcsLst).ExecuteCommand();
                    }
                    affectedRow = DbHelper.db.Insertable<DbModels.rcs>(new List<DbModels.rcs>(RcsLst)).ExecuteCommand();
                    DbHelper.db.CommitTran();
                    MessageBox.Show("操作台生成成功！");
                    EventBus.Instance.PublishConfigChange();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"操作台生成失败！{ex.ToString()}");
                    DbHelper.db.Ado.RollbackTran();
                    this.RcsLst = new ObservableCollection<DbModels.rcs>(curExitsRcsLst);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作台生成失败！{ex.ToString()}");
                LogHelper.Error($"[QuickCreateRcs]{ex.ToString()}");
            }
        }
    }
  
}
