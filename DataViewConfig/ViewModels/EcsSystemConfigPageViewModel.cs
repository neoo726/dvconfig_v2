using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class EcsSystemConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<EcsSystemModel> ecsSystemLst;
        public ObservableCollection<EcsSystemModel> EcsSystemLst 
        {
            get => ecsSystemLst;
            set {
                ecsSystemLst = value; OnPropertyChanged("EcsSystemLst");
            }
        }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command AddNewEcsCommand { get; set; }
        public EcsSystemConfigPageViewModel()
        {

            RefreshEcsSystemLst();
             EditCommand = new Command(EditRow);
            DeleteCommand = new Command(DeleteRow);
            AddNewEcsCommand = new Command(AddNewEcsSystem);
        }
        private void RefreshEcsSystemLst()
        {
            EcsSystemLst = new ObservableCollection<EcsSystemModel>();
            var curSystem = DbHelper.db.Queryable<DbModels.dv_system>().ToList();
            foreach (var item in curSystem)
            {
                EcsSystemModel es = new EcsSystemModel();
                es.Id = item.system_id;
                es.SystemName = item.system_name;
                es.SystemDesc = item.system_desc;
                es.CommEnable = item.comm_enable ;
                es.CommInfo = item.comm_info;
                es.EcsComm = Utli.ConvertToEnum<ECSCommType>(item.comm_type);
                EcsSystemLst.Add(es);
            }
        }
        private void AddNewEcsSystem(object o)
        {
            EcsSystemEditPopup e = new EcsSystemEditPopup(null);
            if (e.ShowDialog() == true)
            {
                RefreshEcsSystemLst();
            }
        }
        private void EditRow(object o)
        {
            EcsSystemEditPopup e = new EcsSystemEditPopup(o as EcsSystemModel);
            MainWindow.AddMask();
            if (e.ShowDialog() ==true)
            {
              
                RefreshEcsSystemLst();
            }
            MainWindow.RemoveMask();
        }
        private void DeleteRow(object o)
        {
            if(MessageBox.Show("是否确认删除？","提示",MessageBoxButtons.YesNo)== DialogResult.No)
            {
                return;
            }
            var ecs= o as EcsSystemModel;
            int affectedRow=DbHelper.db.Deleteable<DbModels.dv_system>()
                .Where(x=>x.system_id== ecs.Id).ExecuteCommand();
            if (affectedRow > 0)
            {
                MessageBox.Show("删除成功！");
                RefreshEcsSystemLst();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }
    }
    public class EcsSystemModel
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string SystemDesc { get; set; }
        public bool IsPermanent { get; set; }
        public ECSCommType EcsComm { get; set; }
       
        public bool CommEnable { get; set; }
        public string MsgType { get; set; }
         public string CommInfo { get; set; }
        //public string ParamSeparator { get; set; }
        //public RequestDestTagModel DestTagName { get; set; }
        //public List<RequestSpecialReturnValueModel> ReturnValueLst { get; set; }

        
        //public class ReturnValueModel
        //{
        //    public int  return_value { get; set; }
        //    public string  return_desc { get; set; }
        //}

    }
}
