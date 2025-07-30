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
using DbModels;
using CMSCore;
using System.IO;

namespace DataViewConfig.ViewModels
{
    internal class DvTipsConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        public ObservableCollection<DbModels.dv_tips> DvTipsLst { get; set; }

        public string TipsInternalName { get; set; }
        public bool TxtTipsChecked { get; set; }
        public string TipsZh { get; set; }
        public string TipsEn { get; set; }
      
        public string SearchTipsStr { get; set; }
       
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
        public DvTipsConfigPageViewModel()
        {
            DvTipsLst = new ObservableCollection<DbModels.dv_tips>();
           
            RefreshTipLst();
            
            QueryCommand = new Command(Query);
            EditCommand =new Command(Edit);
            DeleteCommand = new Command(Delete);
            ResetCommand = new Command(ResetQuery);

            AddNewTipsCommand = new Command(AddNewTips);
          
         
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
                    if (!allColumns.Contains("id") || !allColumns.Contains("tips_internal_name") || !allColumns.Contains("tips_zh") || !allColumns.Contains("tips_en"))
                    {
                        System.Windows.Forms.MessageBox.Show("导入数据字段不匹配，请重新确认！"); return;
                    }
                    var dataLst = MiniExcel.Query<DbModels.dv_tips>(fileDialog.FileName, excelType: ExcelType.XLSX);
                    if (dataLst == null || dataLst.Count() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("载入csv文件失败，请检查csv文件格式！"); return;
                    }
                    dataLst = dataLst.Where(x => !string.IsNullOrEmpty(x.tips_internal_name) && x.id > 0).ToList();
                    var duplicatedLst = dataLst.GroupBy(x => x.tips_internal_name).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                    if (duplicatedLst.Count() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("csv文件中有重复项！！！"); return;
                    }
                    try
                    {
                        DbHelper.db.BeginTran();
                        DbHelper.db.DbMaintenance.TruncateTable<DbModels.dv_tips>();
                        //var tmpLst = dataLst.ToList();
                        //for (int i = 0; i < dataLst.ToList().Count; i++)
                        //{
                        //    dataLst[i].id=default;
                        //}
                        DbHelper.db.Insertable<DbModels.dv_tips>(dataLst.ToList()).ExecuteCommand();
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
                var exportDataPath = dataPath + "\\DvTips_" + DateTime.Now.ToString("yyyy-mm-dd-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<DbModels.dv_tips>(DvTipsLst), excelType: ExcelType.XLSX, overwriteFile: true);
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
        
        private void AddNewTips(object o)
        {
            if (string.IsNullOrEmpty(TipsInternalName))
            {
                System.Windows.MessageBox.Show("提示名称不能空！"); return;
            }
            else if (TxtTipsChecked && (string.IsNullOrEmpty(TipsZh)|| string.IsNullOrEmpty(TipsEn)))
            {
                System.Windows.MessageBox.Show("提示内容不能空！"); return;
            }
           
            if (DvTipsLst.Where(x => x.tips_internal_name == this.TipsInternalName).Count() > 0)
            {
                System.Windows.MessageBox.Show("已经存在重复的提示！请修改名称！");return;
            }
            var newTip = new DbModels.dv_tips();
            newTip.tips_internal_name = this.TipsInternalName;
           
            newTip.tips_zh = this.TipsZh;
            newTip.tips_en = this.TipsEn;
            var affectedRow=DbHelper.db.Insertable<DbModels.dv_tips>(newTip).ExecuteCommand();
            if (affectedRow == 0)
            {
                System.Windows.MessageBox.Show("添加失败！");return;
            }
            RefreshTipLst();
        }
        private void ResetQuery(object o)
        {

            SearchTipsStr = String.Empty;
            Query(o);
        }
        private void RefreshTipLst()
        {
            var allTips = DbHelper.db.Queryable<DbModels.dv_tips>()           
                .Where(x => string.IsNullOrEmpty(SearchTipsStr) 
                || x.tips_internal_name.Contains(SearchTipsStr) 
                || x.tips_zh.Contains(SearchTipsStr)
                ||x.tips_en.Contains(SearchTipsStr))
                .ToList();
            DvTipsLst = new ObservableCollection<DbModels.dv_tips>(allTips);
            
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
            var t = o as DbModels.dv_tips;

            Pages.Popups.DvTipEditPopup tipEdit = new Pages.Popups.DvTipEditPopup(t);
            MainWindow.AddMask();
            if (tipEdit.ShowDialog() == true)
            {
                RefreshTipLst();
            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedCrane = o as DbModels.dv_tips;
            if (selectedCrane == null)
            {
                System.Windows.MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (System.Windows.MessageBox.Show("确认要删除该提示吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var paramLst = DbHelper.db.Queryable<DbModels.dv_request_param>().ToList();
            string existedParam=string.Empty;
            //判断是否有参数已经配置了该提示
            foreach (var item in paramLst)
            {
                var paramContent = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestParamContentModel>(item.param_content);
                if (paramContent.param_validation_enable)
                {
                    if (paramContent.validation_rules.Exists(x => x.invalid_tips_internal_name == selectedCrane.tips_internal_name))
                    {
                        existedParam += item.param_name + ",";
                    }
                }
            }
            if (!string.IsNullOrEmpty(existedParam))
            {
                System.Windows.MessageBox.Show($"删除失败！已有参数校验规则中配置了该提示。相关参数：{existedParam}");
                return;
            }
            var iRet = DbHelper.db.Deleteable<DbModels.dv_tips>().Where(x=>x.id==selectedCrane.id).ExecuteCommand();
            if (iRet == 0)
            {
                System.Windows.MessageBox.Show("删除失败！");
            }
            else
            {
                System.Windows.MessageBox.Show("删除成功！");
                DvTipsLst.Remove(selectedCrane);
            }
        }
    }
    
}
