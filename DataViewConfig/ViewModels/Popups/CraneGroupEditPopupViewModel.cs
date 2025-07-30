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
using MessageBox = System.Windows.Forms.MessageBox;

namespace DataViewConfig.ViewModels
{
    internal class CraneGroupEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        public int CraneGroupID { get; set; }
        public string CraneGroupName { get; set; }      
      
        public DbModels.dv_screen_conf SelectedScreen { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command AddNewCraneSpreaderScreenCommand { get; set; }
        public Command RemoveCraneTypeScreenCommand { get; set; }
        #endregion
        public CraneGroupEditPopupViewModel(CraneGroupModel craneGroup)
        {

            Task.Factory.StartNew(new Action(() =>
            {
                var dvConfig = DbHelper.db.Queryable<DbModels.dv_project_conf>().First();
                //获取参数点
                var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
                 
                  .ToList();
                //ScreenNameLst = new ObservableCollection<DbModels.dv_screen_conf>(dvScreenLst);
            }));
            this.CraneGroupID = craneGroup.CraneGroupID;
            this.CraneGroupName = craneGroup.CraneGroupName;
          
            //if (this.SpreaderMatchEnable)
            //{
            //    if(craneType.SpreaderMatchScreenModel!= null)
            //    {
            //        this.CraneSpreaderMatchScreenLst = new ObservableCollection<SpreaderMatchScreenModel>(craneType.SpreaderMatchScreenModel);
            //    }
            //}

            //AddNewCraneSpreaderScreenCommand = new Command(AddNewCraneSpreaderScreen);
            //RemoveCraneTypeScreenCommand = new Command(RemoveCraneTypeScreen);
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        //private void AddNewCraneSpreaderScreen(object o)
        //{
        //    if (CraneSpreaderMatchScreenLst == null) 
        //    {
        //        CraneSpreaderMatchScreenLst = new ObservableCollection<SpreaderMatchScreenModel>();
        //    }
        //    CraneSpreaderMatchScreenLst.Add(new SpreaderMatchScreenModel() 
        //    {
                 
        //    });
        //}
        //private void RemoveCraneTypeScreen(object o)
        //{
        //    if (!int.TryParse(o.ToString(),out  int selectedSpreaderId)) return;
        //    var selectedSpreaderMatchModel = CraneSpreaderMatchScreenLst.Where(x => x.spreader_id == selectedSpreaderId).FirstOrDefault();
        //    if (selectedSpreaderMatchModel == null) return;
        //    this.CraneSpreaderMatchScreenLst.Remove(selectedSpreaderMatchModel);
        //}
        private void Confirm(object o)
        {
            DbModels.crane_group c =new DbModels.crane_group()
            {
                group_id= this.CraneGroupID,
                group_name=this.CraneGroupName,
            };
            //if (SpreaderMatchEnable)
            //{
            //    if (CraneSpreaderMatchScreenLst.Count()==0)
            //    {
            //        MessageBox.Show("请选择吊具匹配的画面！");return;
            //    }
            //    var spreaderMatchScreenLst = new List<DataView_Configuration.SpreaderMatchScreenModel>();
            //    foreach (var item in CraneSpreaderMatchScreenLst)
            //    {
            //        var newSpreaderMatchItem = new SpreaderMatchScreenModel();
            //        newSpreaderMatchItem.screen_info = new DbModels.dv_screen_conf()
            //        {
            //            dv_screen_id = item.screen_info.dv_screen_id,
            //            dv_screen_internal_name = item.screen_info.dv_screen_internal_name,
            //            dv_screen_csw_name = item.screen_info.dv_screen_csw_name,
            //            dv_screen_desc = item.screen_info.dv_screen_desc,
            //        };
            //        spreaderMatchScreenLst.Add(item);
            //    }
            //    c.spreader_match_enable = true;
            //    c.spreader_match_screen= JsonConvert.SerializeObject(spreaderMatchScreenLst);
            //}
            
            int affectedRow = DbHelper.db.Updateable<DbModels.crane_group>(c)
               .Where(x => x.group_id == c.group_id).ExecuteCommand();
            if (affectedRow >0)
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
