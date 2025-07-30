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
    internal class NewControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #region 命令

        public Command OpenPageCommand { get; set; }
        public Command AddCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command EditReturnCommand { get; set; }
        #endregion
        #region 输入值 
        private ObservableCollection<DbModels.dv_screen_conf> screenNameLst;
        public ObservableCollection<DbModels.dv_screen_conf> ScreenNameLst
        {
            get => screenNameLst;
            set { screenNameLst = value; OnPropertyChanged("ScreenNameLst"); }
        }
        private DbModels.dv_screen_conf selectedScreenInternalName;
        public DbModels.dv_screen_conf SelectedScreenInternalName
        {
            get => selectedScreenInternalName;
            set { selectedScreenInternalName = value; OnPropertyChanged("SelectedScreenInternalName"); }
        }
        private string controlInternalName;
        public string ControlInternalName
        {
            get => controlInternalName;
            set { controlInternalName = value; OnPropertyChanged("ControlInternalName"); }
        }
        private ControlType curControlType;
        public ControlType CurControlType
        {
            get => curControlType;
            set { curControlType = value; OnPropertyChanged("CurControlType"); }
        }
        private int accessId;
        public int AccessId
        {
            get => accessId;
            set { accessId = value; OnPropertyChanged("AccessId"); }
        }
        
        
        #endregion

        public NewControlViewModel()
        {
            //获取参数点
            var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
              .Where(x=>!x.dv_screen_csw_name.Contains(","))
              .ToList();
            ScreenNameLst = new ObservableCollection<DbModels.dv_screen_conf>();
            foreach (var item in dvScreenLst)
            {
                ScreenNameLst.Add(item);
            }
            OpenPageCommand = new Command(OpenPage);
            AddCommand = new Command(AddControl);
        }
        private void OpenPage(object o)
        {
        }
        
        private void EditReturnVal(object o)
        {
            Pages.Popups.RequestReturnPopup rrp = new Pages.Popups.RequestReturnPopup();
            rrp.ShowDialog();
            var returValLst = (rrp.DataContext as ViewModels.RequestReturnPopupViewModel).ReturnValLst;
            //this.RequestReturnVal = returValLst.Count>0? JsonConvert.SerializeObject(returValLst):"";
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddControl(object o)
        {
            var dv_controls = new DbModels.dv_control_conf();

            dv_controls.dv_control_internal_name = this.controlInternalName;
            dv_controls.dv_control_access_id = this.AccessId;
            dv_controls.dv_control_type_id = (int)this.curControlType;
            dv_controls.dv_screen_id = this.selectedScreenInternalName.dv_screen_id;
            


            int affectedRow = DbHelper.db.Insertable<DbModels.dv_control_conf>(dv_controls).ExecuteCommand();
            if (affectedRow != 0)
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
