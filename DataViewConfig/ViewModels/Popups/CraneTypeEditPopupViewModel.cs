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
    internal class CraneTypeEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        public int CraneTypeID { get; set; }
        public string CraneTypeName { get; set; }
        public string CraneTypeDesc { get; set; }
        public bool SpreaderMatchEnable { get; set; }
        public ObservableCollection<DataView_Configuration.SpreaderMatchScreenModel> CraneSpreaderMatchScreenLst { get; set; }
        public ObservableCollection<DbModels.dv_screen_conf> ScreenNameLst { get; set; }
      
        public DbModels.dv_screen_conf SelectedScreen { get; set; }
        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command AddNewCraneSpreaderScreenCommand { get; set; }
        public Command RemoveCraneTypeScreenCommand { get; set; }
        #endregion
        public CraneTypeEditPopupViewModel(CraneTypeModel craneType)
        {

            Task.Factory.StartNew(new Action(() =>
            {
                var dvConfig = DbHelper.db.Queryable<DbModels.dv_project_conf>().First();
                //获取参数点
                var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
                 
                  .ToList();
                ScreenNameLst = new ObservableCollection<DbModels.dv_screen_conf>(dvScreenLst);
            }));
            this.CraneTypeID = craneType.CraneTypeID;
            this.CraneTypeName = craneType.CraneTypeName;
            this.CraneTypeDesc = craneType.CraneTypeDesc;
            this.SpreaderMatchEnable = craneType.SpreaderMatchEnable;
            if (this.SpreaderMatchEnable)
            {
                if(craneType.SpreaderMatchScreenModel!= null)
                {
                    this.CraneSpreaderMatchScreenLst = new ObservableCollection<SpreaderMatchScreenModel>(craneType.SpreaderMatchScreenModel);
                }
            }

            AddNewCraneSpreaderScreenCommand = new Command(AddNewCraneSpreaderScreen);
            RemoveCraneTypeScreenCommand = new Command(RemoveCraneTypeScreen);
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
        }
        private void AddNewCraneSpreaderScreen(object o)
        {
            if (CraneSpreaderMatchScreenLst == null) 
            {
                CraneSpreaderMatchScreenLst = new ObservableCollection<SpreaderMatchScreenModel>();
            }
            CraneSpreaderMatchScreenLst.Add(new SpreaderMatchScreenModel() 
            {
                 
            });
        }
        private void RemoveCraneTypeScreen(object o)
        {
            if (!int.TryParse(o.ToString(),out  int selectedSpreaderId)) return;
            var selectedSpreaderMatchModel = CraneSpreaderMatchScreenLst.Where(x => x.spreader_id == selectedSpreaderId).FirstOrDefault();
            if (selectedSpreaderMatchModel == null) return;
            this.CraneSpreaderMatchScreenLst.Remove(selectedSpreaderMatchModel);
        }
        private void Confirm(object o)
        {
            DbModels.crane_type c =new DbModels.crane_type()
            {
                 crane_type_id= this.CraneTypeID,
                 crane_type_name=this.CraneTypeName,
                 crane_type_desc=this.CraneTypeDesc
            };
            if (SpreaderMatchEnable)
            {
                if (CraneSpreaderMatchScreenLst.Count()==0)
                {
                    MessageBox.Show("请选择吊具匹配的画面！");return;
                }
                var spreaderMatchScreenLst = new List<DataView_Configuration.SpreaderMatchScreenModel>();
                foreach (var item in CraneSpreaderMatchScreenLst)
                {
                    var newSpreaderMatchItem = new SpreaderMatchScreenModel();
                    newSpreaderMatchItem.screen_info = new DbModels.dv_screen_conf()
                    {
                        dv_screen_id = item.screen_info.dv_screen_id,
                        dv_screen_internal_name = item.screen_info.dv_screen_internal_name,
                        dv_screen_csw_name = item.screen_info.dv_screen_csw_name,
                        dv_screen_desc = item.screen_info.dv_screen_desc,
                    };
                    spreaderMatchScreenLst.Add(item);
                }
                c.spreader_match_enable = true;
                c.spreader_match_screen= JsonConvert.SerializeObject(spreaderMatchScreenLst);
            }
            int affectedRow = DbHelper.db.Updateable<DbModels.crane_type>(c)
               .Where(x => x.crane_type_id == c.crane_type_id).ExecuteCommand();
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
