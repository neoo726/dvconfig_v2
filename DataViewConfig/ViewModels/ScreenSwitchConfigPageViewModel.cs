using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using CMSCore;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using MiniExcelLibs;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;
namespace DataViewConfig.ViewModels
{
    internal class ScreenSwitchConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
       
        #region Command
        public Command OpenPageCommand { get; set; }
        public Command EditExceptionCodeCommand { get; set; }
        public Command DeleteCommand { get; set; }
        
        public Command AddExceptionCodeCommand { get; set; }
       
        public Command ExportDataCommand { get; set; }
        public Command ImportDataCommand { get; set; }
        public Command QueryCommand { get; set; }
        public Command ResetCommand { get; set; }
        #endregion
        public ObservableCollection<ExceptionCodeModel> ExceptionLlst { get; set; }
        public ObservableCollection<DbModels.dv_screen_conf> DvScreenLst { get; set; }
        public DbModels.dv_screen_conf SelectedDvScreen { get; set; }
        public string ExceptionCode { get; set; }
        public int DvScreenID { get; set; }
        public string DvScreenName { get; set; }
        public string ExceptionDesc { get; set; }
        public string SearchExceptionCodeTxt { get; set; }
        private  List<DbModels.dv_exception_screen_map> curExceptionCodeLst { get; set; }
       
        public ScreenSwitchConfigPageViewModel()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>().ToList();
                DvScreenLst = new ObservableCollection<DbModels.dv_screen_conf>(dvScreenLst);
                RefreshExceptionCodeLst();
            }));

            AddExceptionCodeCommand = new Command(AddNewExceptionCode);
            EditExceptionCodeCommand = new Command(EditExceptionCode);
            
            DeleteCommand = new Command(DeleteExceptionCode);
          
           
            //SelectBlockCommand = new Command(SelectBlockChanged);
            ExportDataCommand = new Command(ExportExcelData);
            ImportDataCommand = new Command(ImportExcelData);

            QueryCommand = new Command(Query);
            ResetCommand = new Command(Reset);
        }
        //private void SelectBlockChanged(object o)
        //{
        //    if (string.IsNullOrEmpty(SelectedBlockName)) return;
        //    var curBlock = dbBlockLst.Where(x => x.block_name == SelectedBlockName).FirstOrDefault();
        //    if (curBlock == null) return;
        //    Task.Factory.StartNew(new Action(() =>
        //    {
        //        RefreshBayLst((int)curBlock.block_plc_id);
        //    }));
           
        //}
        private void Reset(object o)
        {
            this.SearchExceptionCodeTxt = string.Empty;
            Query(o);
        }
        private void Query(object o)
        {
            RefreshExceptionCodeLst();
        }
        private void RefreshExceptionCodeLst()
        {
            curExceptionCodeLst = DbHelper.db.Queryable<DbModels.dv_exception_screen_map>()
                .Where(x=>string.IsNullOrEmpty(SearchExceptionCodeTxt) ||x.exception_code.Contains(SearchExceptionCodeTxt))
                .OrderBy(x=>x.exception_code).ToList();          
            var screenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>().ToList();
            if (screenLst == null || curExceptionCodeLst == null) return;
            ExceptionLlst = new ObservableCollection<ExceptionCodeModel>();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var item in curExceptionCodeLst)
                {
                    var screenName = screenLst.FirstOrDefault(x => x.dv_screen_id == item.dv_screen_id);
                    if (screenName == null)
                    {
                        continue;
                    }
                    ExceptionLlst.Add(new ExceptionCodeModel()
                    {
                        ExceptionCode=item.exception_code,
                        ExceptionDesc=item.exception_desc,
                        DvScreenID=item.dv_screen_id,
                        DvScreenName= screenName.dv_screen_csw_name,
                    });
                }
               
            }));
        }
        private void EditExceptionCode(object o)
        {
            var exceptionModel = o as ExceptionCodeModel;
            ExceptionCodeEditPopup ce = new ExceptionCodeEditPopup(exceptionModel);
            if (ce.ShowDialog() == true)
            {
                RefreshExceptionCodeLst();
            }
        }
       
        private void DeleteExceptionCode(object o)
        {
            var selectedException = o as ExceptionCodeModel;
            if (selectedException == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该异常代码配置项吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_exception_screen_map>().Where(x=>x.exception_code== selectedException.ExceptionCode).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除异常代码失败！");
            }
            else
            {
                MessageBox.Show("删除异常代码成功！");
                //RcsLst.Remove(selectedRcs);
                RefreshExceptionCodeLst();
            }
        }
        private void DeleteBay(object o)
        {
            var selectedBay = o as BayModel;
            if (selectedBay == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该贝位吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.bay>().Where(x => x.block_id == selectedBay.BlockID&&x.bay_no==selectedBay.BayID).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除贝位失败！");
            }
            else
            {
                MessageBox.Show("删除贝位成功！");
                //RcsLst.Remove(selectedRcs);
            }
        }
        private void AddNewExceptionCode(object o )
        {
            if (string.IsNullOrEmpty(ExceptionCode)||string.IsNullOrEmpty(ExceptionDesc))
            {
                MessageBox.Show("异常码和异常描述不能为空！");return;
            }
            if (curExceptionCodeLst.Exists(x => x.exception_code == ExceptionCode))
            {
                MessageBox.Show("异常码有重复项！"); return;
            }
            var newExceptionCode = new DbModels.dv_exception_screen_map()
            {
                exception_code = ExceptionCode,
                exception_desc = ExceptionDesc,
                dv_screen_id=SelectedDvScreen.dv_screen_id,

            };
            
            var affectedRow = DbHelper.db.Insertable<DbModels.dv_exception_screen_map>(newExceptionCode).ExecuteCommand();
            if (affectedRow > 0)
            {
                MessageBox.Show("异常代码配置添加成功！");
                RefreshExceptionCodeLst();
            }
            else
            {
                MessageBox.Show($"异常代码配置添加失败！");
            }
        }
        private void ImportExcelData(object p)
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
                if (System.Windows.Forms.MessageBox.Show("确认要导入数据吗？导入会覆盖原有数据。", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                var dataLst = MiniExcel.Query<DbModels.dv_exception_screen_map>(fileDialog.FileName, excelType: ExcelType.XLSX);
                if (dataLst == null || dataLst.Count() == 0)
                {
                    System.Windows.Forms.MessageBox.Show("载入Excel文件失败，请检查Excel文件格式！"); return;
                }
                var duplicatedLst = dataLst.GroupBy(x => x.exception_code).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                if (duplicatedLst.Count() > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Excel文件中有重复项！"); return;
                }
                try
                {
                    DbHelper.db.BeginTran();
                    DbHelper.db.DbMaintenance.TruncateTable<DbModels.dv_exception_screen_map>();
                    //var tmpLst = dataLst.ToList();
                    //for (int i = 0; i < dataLst.ToList().Count; i++)
                    //{
                    //    dataLst[i].id=default;
                    //}
                    DbHelper.db.Insertable<DbModels.dv_exception_screen_map>(dataLst.ToList()).ExecuteCommand();
                    DbHelper.db.CommitTran();
                    System.Windows.Forms.MessageBox.Show("导入成功！");
                    Task.Factory.StartNew(new Action(() =>
                    {
                        //模糊搜索
                        RefreshExceptionCodeLst();
                    }));
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("导入失败！" + ex.ToString());
                    DbHelper.db.RollbackTran();
                }
            }
        }
        private void ExportExcelData(object o)
        {
            try
            {
                //保存文件
                var appDirectory = System.Environment.CurrentDirectory.ToString();
                var dataPath = System.Environment.CurrentDirectory.ToString() + "\\Data";
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }
                var exportDataPath = dataPath + "\\Exception_Screen_Map_" + DateTime.Now.ToString("yyyy-MM-d-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<DbModels.dv_exception_screen_map>(this.curExceptionCodeLst), excelType: ExcelType.XLSX, overwriteFile: true);
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, " + exportDataPath;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogHelper.Error(ex.ToString());
            }
        }
    }
    public class ExceptionCodeModel
    {
        public string ExceptionCode { get; set; }
        public int DvScreenID { get; set; }
        public string DvScreenName { get; set; }
        public string ExceptionDesc { get; set; }
    }
   
}

