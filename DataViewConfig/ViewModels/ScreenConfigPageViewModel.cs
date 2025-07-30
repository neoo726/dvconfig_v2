using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using CMSCore;
using System.Windows.Forms;
using System.Windows.Input;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using MiniExcelLibs;
using Newtonsoft.Json;
using System.Linq;
using MessageBox = System.Windows.MessageBox;

namespace DataViewConfig.ViewModels
{
    internal class ScreenConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
      
        public ObservableCollection<ScreenModel> ScreenLst { get; set; }
        public ObservableCollection<ScreenModel> SelectedScreenLst { get; set; }
        public string ScreenInternalName { get; set; }
        public string ScreenCswName { get; set; }
        public string ScreenDesc { get; set; }
        public string SearchScreenType { get; set; }
        public string SearchScreenTxt { get; set; }
        #region COmmand
        public Command QueryCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command ResetCommand { get; set; }
        public Command ImportDataCommand { get; set; }
        public Command ExportDataCommand { get; set; }
        public Command SelectCswCommand { get; set; }
        #endregion
        public ScreenConfigPageViewModel()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                RefreshScreenLst();
            }));
           
           
            AddNewCommand = new Command(AddNew);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            QueryCommand = new Command(Query);
            ResetCommand = new Command(ResetQuery);
            ImportDataCommand = new Command(ImportExcelData);
            ExportDataCommand = new Command(ExportExcelData);
            SelectCswCommand = new Command(OpenSelectCswWin);
        }
        private void OpenSelectCswWin(object o)
        {
            ScreenCswSelectPopup scs = new ScreenCswSelectPopup();
           
            if(scs.ShowDialog()== true)
            {
                var vm = scs.DataContext as ScreenCswSelectPopupViewModel;
                ScreenCswName =string.Join(",", vm.SelectedCswNameLst) ;
            }
            else
            {

            }   
        }
        /// <summary>
        /// 导入当前堆场
        /// </summary>
        /// <param name="p"></param>
        private void ImportExcelData(object p)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                var appDirectory = System.Environment.CurrentDirectory.ToString();
                var dataPath = System.Environment.CurrentDirectory.ToString() + "\\Data";
                fileDialog.InitialDirectory = dataPath;
                //fileDialog.FileName = "Image|*.png;";
                fileDialog.Filter = "Excel|*.xlsx;";
                DialogResult result = fileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (System.Windows.Forms.MessageBox.Show("确认要导入数据吗？导入后会覆盖原有数据！", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                    var allColumns = MiniExcel.GetColumns(fileDialog.FileName, excelType: ExcelType.XLSX, useHeaderRow: true);
                    if (!allColumns.Contains("dv_screen_id") || !allColumns.Contains("dv_screen_internal_name") || !allColumns.Contains("dv_screen_csw_name") || !allColumns.Contains("dv_screen_desc") )
                    {
                        System.Windows.Forms.MessageBox.Show("导入数据字段不匹配，请重新确认！"); return;
                    }
                    var dataLst = MiniExcel.Query<ScreenModel>(fileDialog.FileName, excelType: ExcelType.XLSX);
                    if (dataLst == null || dataLst.Count() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("载入Excel文件失败，请检查Excel文件格式！"); return;
                    }
                    //剔除无效数据
                    dataLst = dataLst.Where(x => !string.IsNullOrEmpty(x.dv_screen_csw_name) && !string.IsNullOrEmpty(x.dv_screen_internal_name) && (int)x.dv_screen_id > 0).ToList();
                    var duplicatedLst = dataLst.GroupBy(x => new { x.dv_screen_internal_name }).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                    if (duplicatedLst.Count() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Excel文件中有ScreenInternalName重复！" + string.Join(",", duplicatedLst)); return;
                    }
                    try
                    {
                        DbHelper.db.BeginTran();
                       
                        var deletedTagLst = this.ScreenLst.Where(x => !dataLst.Any(n => n.dv_screen_internal_name == x.dv_screen_internal_name)).ToList();
                        var insertedTagLst = dataLst.Where(x => !this.ScreenLst.Any(n => n.dv_screen_internal_name == x.dv_screen_internal_name)).ToList();
                        var updatedTagLst = dataLst.Where(x => this.ScreenLst.Any(n => n.dv_screen_internal_name == x.dv_screen_internal_name)).ToList();
                        var deletedDbTagLst = new List<DbModels.dv_screen_conf>();
                        var updatedDbTagLst = new List<DbModels.dv_screen_conf>();
                        foreach (var item in this.ScreenLst)
                        {
                            if (deletedTagLst.Any(x => x.dv_screen_internal_name == item.dv_screen_internal_name))
                            {
                                deletedDbTagLst.Add(new DbModels.dv_screen_conf()
                                {
                                     dv_screen_id=(short)item.dv_screen_id,
                                     dv_screen_internal_name=item.dv_screen_internal_name,
                                     dv_screen_csw_name=item.dv_screen_csw_name,
                                     dv_screen_desc=item.dv_screen_desc,
                                });
                            }
                        }
                        foreach (var item in updatedTagLst)
                        {
                            updatedDbTagLst.Add(new DbModels.dv_screen_conf()
                            {
                                dv_screen_id = (short)item.dv_screen_id,
                                dv_screen_internal_name = item.dv_screen_internal_name,
                                dv_screen_csw_name = item.dv_screen_csw_name,
                                dv_screen_desc = item.dv_screen_desc,
                            });
                        }
                        var insertedDbTagLst = new List<DbModels.dv_screen_conf>();
                        foreach (var item in insertedTagLst)
                        {
                            insertedDbTagLst.Add(new DbModels.dv_screen_conf()
                            {
                                dv_screen_id = (short)item.dv_screen_id,
                                dv_screen_internal_name = item.dv_screen_internal_name,
                                dv_screen_csw_name = item.dv_screen_csw_name,
                                dv_screen_desc = item.dv_screen_desc,
                            });
                        }
                        DbHelper.db.Deleteable<DbModels.dv_screen_conf>(deletedDbTagLst).ExecuteCommand();
                        DbHelper.db.Updateable<DbModels.dv_screen_conf>(updatedDbTagLst).ExecuteCommand();
                        DbHelper.db.Insertable<DbModels.dv_screen_conf>(insertedDbTagLst).ExecuteCommand();
                        DbHelper.db.CommitTran();
                        System.Windows.Forms.MessageBox.Show("导入成功！");
                        Task.Factory.StartNew(new Action(() =>
                        {
                            RefreshScreenLst();
                        }));
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("导入失败！" + ex.ToString());
                        DbHelper.db.RollbackTran();
                    }
                    //this.TipsImageUrl = fileDialog.FileName.Replace(dataPath + "\\", "");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"导入失败！异常信息:{ex.ToString()}"); return;
            }

        }
        private void ExportExcelData(object o)
        {
            try
            {
                //保存文件
                var appDirectory = System.Environment.CurrentDirectory.ToString();
                var dataPath = System.Environment.CurrentDirectory.ToString() + "\\Data";
                //目录不存在时，自动创建目录
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }
                var exportDataPath = dataPath + "\\Screen_" + DateTime.Now.ToString("yyyy-mm-dd-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<ScreenModel>(ScreenLst), excelType: ExcelType.XLSX, overwriteFile: true);
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, " + exportDataPath;
                p.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"添加失败！异常信息:{ex.ToString()}"); return;
            }
        }
        //搜索
        private void Query(object o)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //模糊搜索
                RefreshScreenLst();
            }));

        }
        private void ResetQuery(object o)
        {
            this.SearchScreenType = "";
            this.SearchScreenTxt = "";
            Query(null);
        }
        private bool CheckStringContains(string originStr,string searchTxt)
        {
            if (string.IsNullOrEmpty(searchTxt)) return true;
            if (originStr.Contains(searchTxt)) return true;
            return false;
        }
        private void RefreshScreenLst()
        {
            ScreenLst = new ObservableCollection<ScreenModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
                .Where(x=> string.IsNullOrEmpty(this.SearchScreenType) ||(this.SearchScreenType == "单个画面"&& !x.dv_screen_csw_name.Contains(",")) || (this.SearchScreenType == "组合画面" && x.dv_screen_csw_name.Contains(",")))
                .Where(x=>string.IsNullOrEmpty(this.SearchScreenTxt)||(x.dv_screen_internal_name.Contains(this.SearchScreenTxt)|| x.dv_screen_csw_name.Contains(this.SearchScreenTxt) || x.dv_screen_desc.Contains(this.SearchScreenTxt)))
                .ToList();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var item in curRequests)
                {
                    var newScreen = new ScreenModel();
                    newScreen.dv_screen_id = item.dv_screen_id;
                    newScreen.dv_screen_internal_name = item.dv_screen_internal_name;
                    newScreen.dv_screen_csw_name = item.dv_screen_csw_name;
                    newScreen.dv_screen_desc = item.dv_screen_desc;
                    newScreen.dv_screen_type = item.dv_screen_csw_name.Contains(",") ? "组合画面" : "单个画面";

                    ScreenLst.Add(newScreen);
                }
            }));
        }
      
        private void AddNew(object o)
        {
            if(ScreenLst.Where(x=>x.dv_screen_internal_name==this.ScreenInternalName).Count() > 0)
            {
                MessageBox.Show("已存在重复的画面配置项(Screen Internal Name重复)！"); return;
            }
            var dv_screens = new DbModels.dv_screen_conf();

            dv_screens.dv_screen_internal_name = this.ScreenInternalName;
            dv_screens.dv_screen_csw_name = this.ScreenCswName;
            dv_screens.dv_screen_desc =this.ScreenDesc;
            if (string.IsNullOrEmpty(this.ScreenInternalName) || string.IsNullOrEmpty(this.ScreenCswName))
            {
                MessageBox.Show("请填写画面名称！");return;
            }
            int affectedRow = DbHelper.db.Insertable<DbModels.dv_screen_conf>(dv_screens).ExecuteCommand();
            if (affectedRow != 0)
            {
                MessageBox.Show("添加成功！");
                RefreshScreenLst();
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
        private void Edit(object o)
        {
            var selectedScreen = o as ScreenModel;
            var scr = new DbModels.dv_screen_conf();
            scr.dv_screen_internal_name = selectedScreen.dv_screen_internal_name;
            scr.dv_screen_desc = selectedScreen.dv_screen_desc;
            scr.dv_screen_csw_name = selectedScreen.dv_screen_csw_name;
            scr.dv_screen_id = (short)selectedScreen.dv_screen_id;
           
            ScreenEditPopup ce = new ScreenEditPopup(scr);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    RefreshScreenLst();
                }));
             

            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedScreen = o as ScreenModel;
            if (selectedScreen == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该画面吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_screen_conf>().Where(x=>x.dv_screen_id==selectedScreen.dv_screen_id).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                ScreenLst.Remove(selectedScreen);
            }
        }
    }
    public class ScreenModel
    {
        public int dv_screen_id { get; set; }
        public string dv_screen_internal_name { get; set; }
        public string dv_screen_csw_name { get; set; }
        public string dv_screen_desc { get; set; }
        public string dv_screen_type{ get; set; }
       
    }
}
