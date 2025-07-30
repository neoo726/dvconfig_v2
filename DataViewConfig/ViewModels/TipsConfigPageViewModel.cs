using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using DataView_Configuration;
using MiniExcelLibs;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using CMSCore;

namespace DataViewConfig.ViewModels
{
    internal class TipsConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        public ObservableCollection<DbModels.config_tool_tips> TipsLst { get; set; }

        public string TipsName { get; set; }
        public bool TxtTipsChecked { get; set; }
        public string TipsContent { get; set; }
        public string TipsImageUrl { get; set; }
        public string SearchTipTypeStr { get; set; }
        public string SearchTipStr { get; set; }
        #region Command
        public Command QueryCommand { get; set; }
        public Command ResetCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command AddNewTipsCommand { get; set; }
        public Command SelectImagUrlCommand { get; set; }
        public Command PreviewImageCommand { get; set; }
        public Command ImportDataCommand { get; set; }
        public Command ExportDataCommand { get; set; }
        #endregion
        public TipsConfigPageViewModel()
        {
            TipsLst = new ObservableCollection<DbModels.config_tool_tips>(DbHelper.db.Queryable<DbModels.config_tool_tips>().ToList());
           
            RefreshTipLst();
            
            QueryCommand = new Command(Query);
            EditCommand =new Command(Edit);
            DeleteCommand = new Command(Delete);
            ResetCommand = new Command(ResetQuery);

            AddNewTipsCommand = new Command(AddNewTips);
            SelectImagUrlCommand = new Command(OpenImageSelection);
            PreviewImageCommand = new Command(PreviewImage);
            ImportDataCommand = new Command(ImportExcelData);
            ExportDataCommand = new Command(ExportExcelData);

        }
        private void ImportExcelData(object p)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                var appDirectory = System.Environment.CurrentDirectory.ToString();
                var dataPath = System.Environment.CurrentDirectory.ToString() + "\\Data";
                fileDialog.InitialDirectory = dataPath;
                //fileDialog.FileName = "Image|*.png;";
                fileDialog.Filter = "Csv|*.xlsx;";
                DialogResult result = fileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (System.Windows.Forms.MessageBox.Show("确认要导入数据吗？导入会覆盖原有数据。", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                    var allColumns = MiniExcel.GetColumns(fileDialog.FileName, excelType: ExcelType.XLSX, useHeaderRow: true);
                    //if (!allColumns.Contains("id") || !allColumns.Contains("tips_internal_name") || !allColumns.Contains("tips_zh") || !allColumns.Contains("tips_en"))
                    //{
                    //    System.Windows.Forms.MessageBox.Show("导入数据字段不匹配，请重新确认！"); return;
                    //}
                    var dataLst = MiniExcel.Query<DbModels.config_tool_tips>(fileDialog.FileName, excelType: ExcelType.XLSX);
                    if (dataLst == null || dataLst.Count() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("载入csv文件失败，请检查csv文件格式！"); return;
                    }
                    //剔除无效数据

                    var duplicatedLst = dataLst.GroupBy(x => x.tips_name).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                    if (duplicatedLst.Count() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("csv文件中有重复项！！！"); return;
                    }
                    try
                    {
                        DbHelper.db.BeginTran();
                        DbHelper.db.DbMaintenance.TruncateTable<DbModels.config_tool_tips>();
                        //var tmpLst = dataLst.ToList();
                        //for (int i = 0; i < dataLst.ToList().Count; i++)
                        //{
                        //    dataLst[i].id=default;
                        //}
                        DbHelper.db.Insertable<DbModels.config_tool_tips>(dataLst.ToList()).ExecuteCommand();
                        DbHelper.db.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("导入失败！" + ex.ToString());
                        DbHelper.db.RollbackTran();
                    }
                    RefreshTipLst();
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
                var exportDataPath = dataPath + "\\Tips_" + DateTime.Now.ToString("yyyy-mm-dd-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<DbModels.config_tool_tips>(TipsLst), excelType: ExcelType.XLSX, overwriteFile: true);
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, " + exportDataPath;
                p.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"导入失败！异常信息:{ex.ToString()}"); return;
            }
            
        }
        private void PreviewImage(object p)
        {
            if (string.IsNullOrEmpty(TipsImageUrl))
            {
                return;
            }
            Controls.ImagePopup imgP = new Controls.ImagePopup(TipsImageUrl);
            imgP.Show();
        }
        private void AddNewTips(object o)
        {
            if (string.IsNullOrEmpty(TipsName))
            {
                System.Windows.MessageBox.Show("提示名称不能空！"); return;
            }
            else if (TxtTipsChecked && string.IsNullOrEmpty(TipsContent))
            {
                System.Windows.MessageBox.Show("提示内容不能空！"); return;
            }
            else if (!TxtTipsChecked && string.IsNullOrEmpty(TipsImageUrl))
            {
                System.Windows.MessageBox.Show("请选择提示图片！"); return;
            }
            if (TipsLst.Where(x => x.tips_name == this.TipsName).Count() > 0)
            {
                System.Windows.MessageBox.Show("已经存在重复的提示！请修改名称！");return;
            }
            var newTip = new DbModels.config_tool_tips();
            newTip.tips_name = this.TipsName;
            newTip.tips_type = TxtTipsChecked ? 1 : 2;
            newTip.tips_zh = this.TipsContent;
            newTip.tips_zh_img_url = this.TipsImageUrl;
            var affectedRow=DbHelper.db.Insertable<DbModels.config_tool_tips>(newTip).ExecuteCommand();
            if (affectedRow == 0)
            {
                System.Windows.MessageBox.Show("添加失败！");return;
            }
            RefreshTipLst();
        }
        private void ResetQuery(object o)
        {
            SearchTipTypeStr = String.Empty;
            SearchTipStr = String.Empty;
            Query(o);
        }
        private void RefreshTipLst()
        {
            var allTips = DbHelper.db.Queryable<DbModels.config_tool_tips>()
                .Where(x => string.IsNullOrEmpty(SearchTipTypeStr) || (SearchTipTypeStr == "文字" && x.tips_type == 1)
                || (SearchTipTypeStr == "图片" && x.tips_type == 2))
                .Where(x => string.IsNullOrEmpty(SearchTipStr) || x.tips_name.Contains(SearchTipStr) || x.tips_zh.Contains(SearchTipStr))
                .ToList();
            TipsLst = new ObservableCollection<DbModels.config_tool_tips>(allTips);
            
        }
        //搜索
        private void Query(object o)
        {
            //var searchtxt = o.ToString();
            
            //模糊搜索
            RefreshTipLst();
        }
        private void Edit(object o)
        {
            var t = o as DbModels.config_tool_tips;

            Pages.Popups.TipEditPopup tipEdit = new Pages.Popups.TipEditPopup(t);
            MainWindow.AddMask();
            if (tipEdit.ShowDialog() == true)
            {
                RefreshTipLst();
            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedCrane = o as DbModels.config_tool_tips;
            if (selectedCrane == null)
            {
                System.Windows.MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (System.Windows.MessageBox.Show("确认要删除该提示吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.config_tool_tips>().Where(x=>x.id==selectedCrane.id).ExecuteCommand();
            if (iRet == 0)
            {
                System.Windows.MessageBox.Show("删除失败！");
            }
            else
            {
                System.Windows.MessageBox.Show("删除成功！");
                TipsLst.Remove(selectedCrane);
            }
        }
        //打开选择图片
        private void OpenImageSelection(object o)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            var appDirectory = System.Environment.CurrentDirectory.ToString();
            var imagePath = System.Environment.CurrentDirectory.ToString() + "\\Images";
            fileDialog.InitialDirectory = imagePath;
            //fileDialog.FileName = "Image|*.png;";
            fileDialog.Filter = "Image|*.png;*.jpg;*.svg;";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.TipsImageUrl = fileDialog.FileName.Replace(imagePath+"\\", "");
            }
        }
    }
    
}
