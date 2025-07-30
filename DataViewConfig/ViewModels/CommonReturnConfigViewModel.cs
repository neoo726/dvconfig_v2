using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using DataView_Configuration;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class CommonReturnConfigViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private  ObservableCollection<DbModels.dv_request_return_code> returnValLst { get; set; }
        public ObservableCollection<DbModels.dv_request_return_code> ReturnValLst
        {
            get => returnValLst;
            set { returnValLst = value; OnPropertyChanged("ReturnValLst"); }
        }
        private ObservableCollection<DbModels.dv_system> dvSystemLst { get; set; }
        public ObservableCollection<DbModels.dv_system> DvSystemLst
        {
            get => dvSystemLst;
            set { dvSystemLst = value; OnPropertyChanged("DvSystemLst"); }
        }
        private int returnCodeID;
        public int ReturnCodeID
        {
            get => returnCodeID;
            set
            {
                returnCodeID = value; OnPropertyChanged("ReturnCodeID");
            }
        }
        private string returnVal;
        public string ReturnVal
        {
            get => returnVal;
            set
            {
                returnVal = value; OnPropertyChanged("ReturnVal");
            }
        }
        private string returnDescZh;
        public string ReturnDescZh
        {
            get => returnDescZh;
            set
            {
                returnDescZh = value; OnPropertyChanged("ReturnDescZh");
            }
        }
        private string returnDescEn;
        public string ReturnDescEn
        {
            get => returnDescEn;
            set
            {
                returnDescEn = value; OnPropertyChanged("ReturnDescEn");
            }
        }
        private int systemId;
        public int SystemId
        {
            get => systemId;
            set
            {
                systemId = value; OnPropertyChanged("SystemId");
            }
        }
        private bool isSuccessFlag;
        public bool IsSuccessFlag
        {
            get => isSuccessFlag;
            set
            {
                isSuccessFlag = value; OnPropertyChanged("IsSuccessFlag");
            }
        }
        #region COmmand
        public Command QueryCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command AddNewCommand { get; set; }
        #endregion
        public CommonReturnConfigViewModel()
        {
            RefreshCommonReturnValLst();
            QueryCommand = new Command(Query);
            AddNewCommand = new Command(AddNew);
            EditCommand =new Command(Edit);
            DeleteCommand = new Command(Delete);

        }
        private void RefreshCommonReturnValLst(string searchTxt=null)
        {
            var curReturnValLst = DbHelper.db.Queryable<DbModels.dv_request_return_code>().ToList();
            ReturnValLst = new ObservableCollection<DbModels.dv_request_return_code>();
            var curSystemLst = DbHelper.db.Queryable<DbModels.dv_system>().ToList();
            DvSystemLst = new ObservableCollection<DbModels.dv_system>(curSystemLst);
            foreach (var item in curReturnValLst)
            {
                //模糊搜索，输入返回值数值/描述
                if (string.IsNullOrEmpty(searchTxt))
                {
                    ReturnValLst.Add(item);
                }
                else
                {
                    if (Utli.StringContains(item.return_value, searchTxt)
                         || Utli.StringContains(item.return_desc_zh, searchTxt)
                         || Utli.StringContains(item.return_desc_en, searchTxt))
                    {
                        ReturnValLst.Add(item);
                    }
                }
            }
            
        }
        //搜索
        private void Query(object o)
        {
            var searchtxt = o.ToString();
            RefreshCommonReturnValLst(searchtxt.ToLower());
        }
        private void Edit(object o)
        {
            var t = o as DbModels.dv_request_return_code;

            Pages.Popups.CommonReturnEditPopup tagEdit = new Pages.Popups.CommonReturnEditPopup(t);
            MainWindow.AddMask();
            if (tagEdit.ShowDialog() == true)
            {
               
                RefreshCommonReturnValLst();

            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedCrane = o as DbModels.dv_request_return_code;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该返回值吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_request_return_code>().Where(x=>x.id==selectedCrane.id).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                ReturnValLst.Remove(selectedCrane);
            }
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddNew(object o)
        {
            if (string.IsNullOrEmpty(this.ReturnVal)|| string.IsNullOrEmpty(this.returnDescZh))
            {
                MessageBox.Show("输入信息不能为空！"); return;
            }
            DbModels.dv_request_return_code c = new DbModels.dv_request_return_code()
            {
              
                return_value = this.ReturnVal,
                return_desc_en = this.returnDescEn,
                return_desc_zh = this.returnDescZh,
               
                system_id=this.systemId,
                is_success = this.isSuccessFlag,
            };

            int affectedRow = DbHelper.db.Insertable<DbModels.dv_request_return_code>(c).ExecuteCommand();
            if (affectedRow != 0)
            {
                RefreshCommonReturnValLst();
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
    
}
