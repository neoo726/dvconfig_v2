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
    internal class NewScreenViewModel : INotifyPropertyChanged
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
        
        private string screenInternalName;
        public string ScreenInternalName
        {
            get => screenInternalName;
            set { screenInternalName = value; OnPropertyChanged("ScreenInternalName"); }
        }
     
        private string screenCswName;
        public string ScreenCswName
        {
            get => screenCswName;
            set { screenCswName = value; OnPropertyChanged("ScreenCswName"); }
        }
        
        
        #endregion

        public NewScreenViewModel()
        {
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
            var dv_screens = new DbModels.dv_screen_conf();

            dv_screens.dv_screen_internal_name =this.ScreenInternalName;
            dv_screens.dv_screen_csw_name = this.ScreenCswName;

            int affectedRow = DbHelper.db.Insertable<DbModels.dv_screen_conf>(dv_screens).ExecuteCommand();
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
