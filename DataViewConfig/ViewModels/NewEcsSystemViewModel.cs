using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using DataView_Configuration;
using Newtonsoft.Json;

namespace DataViewConfig
{
    internal class NewEcsSystemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        #region 命令

        public Command OpenPageCommand { get; set; }
        public Command AddCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command EditReturnCommand { get; set; }
        #endregion
        private ObservableCollection<DbModels.dv_system> dvSystemLst;
        public ObservableCollection<DbModels.dv_system> DvSystemLst
        {
            get => dvSystemLst;
            set { dvSystemLst = value; OnPropertyChanged("DvSystemLst"); }
        }
       
        #region 输入值 
        private string ecsSystemName;
        public string EcsSystemName
        {
            get => ecsSystemName;
            set { ecsSystemName = value; OnPropertyChanged("EcsSystemName"); }
        }
        private string ecsSystemDesc;
        public string EcsSystemDesc
        {
            get => ecsSystemDesc;
            set { ecsSystemDesc = value; OnPropertyChanged("EcsSystemDesc"); }
        }
        private bool ecsCommEnable;
        public bool EcsCommEnable
        {
            get => ecsCommEnable;
            set { ecsCommEnable = value; OnPropertyChanged("EcsCommEnable"); }
        }
        private string commInfo;
        public string CommInfo
        {
            get => commInfo;
            set { commInfo = value; OnPropertyChanged("CommInfo"); }
        }
        
        private ECSCommType ecsComm;
        public ECSCommType EcsComm
        {
            get => ecsComm;
            set { ecsComm = value; OnPropertyChanged("EcsComm"); }
        }
       
        #endregion

        public NewEcsSystemViewModel()
        {
            DvSystemLst = new ObservableCollection<DbModels.dv_system>();
            //获取参数点

            var dvSys  = DbHelper.db.Queryable<DbModels.dv_system>().ToList();

            foreach (var item in dvSys)
            {
                DvSystemLst.Add(new DbModels.dv_system()
                {
                    system_id=item.system_id,
                    system_name=item.system_name,
                    system_desc=item.system_desc,
                    comm_enable=item.comm_enable,
                    comm_info=item.comm_info,
                });
            }
            OpenPageCommand = new Command(OpenPage);
            AddCommand = new Command(AddInterface);
            SelectParamCommand = new Command(SelectParam);
            EditReturnCommand = new Command(EditReturnVal);
        }
        private void OpenPage(object o)
        {
        }
        private void SelectParam(object o)
        {
            Pages.Popups.ParamSelectedPopup paramSelectPopup = new Pages.Popups.ParamSelectedPopup(null);
            paramSelectPopup.ShowDialog();
          
        }
        private void EditReturnVal(object o)
        {
            Pages.Popups.RequestReturnPopup rrp = new Pages.Popups.RequestReturnPopup();
            rrp.ShowDialog();
            var returValLst = (rrp.DataContext as ViewModels.RequestReturnPopupViewModel).ReturnValLst;
           
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddInterface(object o)
        {
            var dvSysmem = new DbModels.dv_system();
            if (string.IsNullOrEmpty(EcsSystemName))
            {
                MessageBox.Show("对象名称不能为空！");
                return;
            }
            dvSysmem.system_name = this.EcsSystemName;
            dvSysmem.system_desc = this.ecsSystemDesc;
            dvSysmem.comm_info = this.CommInfo;
            dvSysmem.comm_enable = this.ecsCommEnable;
            dvSysmem.comm_type = this.EcsComm.ToString();
            int affectedRow=DbHelper.db.Insertable<DbModels.dv_system>(dvSysmem).ExecuteCommand();
            if(affectedRow != 0)
            {
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
}
