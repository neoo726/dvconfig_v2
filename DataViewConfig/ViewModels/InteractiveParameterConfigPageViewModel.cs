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
using DataViewConfig.Models;

namespace DataViewConfig.ViewModels
{
    internal class InteractiveParameterConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        public ObservableCollection<DbModels.dv_request_param> RequestOrignalParamLst { get; set; }
        public ObservableCollection<DbModels.dv_request_param_source> RequestParamSourceLst { get; set; }
        public ObservableCollection<DataView_Configuration.RequestParamContentModel> ParamModelLst { get; set; }
        public ObservableCollection<ParamModel> ParamInfoLst { get; set; }
        public string TipsName { get; set; }
        public bool TxtTipsChecked { get; set; }
        public string TipsContent { get; set; }
        public string TipsImageUrl { get; set; }
        public string SearchParamSourceTypeStr { get; set; }
        public string SearchParamNameStr { get; set; }
        #region Command
        public Command QueryCommand { get; set; }
        public Command ResetCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command SelectImagUrlCommand { get; set; }
        public Command PreviewImageCommand { get; set; }
        public Command ImportDataCommand { get; set; }
        public Command ExportDataCommand { get; set; }
        #endregion
        public InteractiveParameterConfigPageViewModel()
        {
            this.ParamInfoLst = new ObservableCollection<ParamModel>();
            RequestOrignalParamLst = new ObservableCollection<DbModels.dv_request_param>(DbHelper.db.Queryable<DbModels.dv_request_param>().ToList());
            RefreshParamLst(null,null);

            QueryCommand = new Command(Query);
            EditCommand =new Command(Edit);
            DeleteCommand = new Command(Delete);
            ResetCommand = new Command(ResetQuery);

            AddNewCommand = new Command(AddNewParamter);
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
                    //RefreshTipLst();
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
                //MiniExcel.SaveAs(exportDataPath, new List<DbModels.config_tool_tips>(TipsLst), excelType: ExcelType.XLSX, overwriteFile: true);
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
        private void AddNewParamter(object o)
        {
            Pages.Popups.ParamEditPopup paramEdit = new Pages.Popups.ParamEditPopup(null);
            MainWindow.AddMask();
            if (paramEdit.ShowDialog() == true)
            {
                RefreshParamLst(null,null);
            }
            MainWindow.RemoveMask();
        }
        private void ResetQuery(object o)
        {
            SearchParamNameStr = String.Empty;
            SearchParamSourceTypeStr = String.Empty;
            Query(o);
        }
        private void RefreshParamLst(string paramSourceTypeStr,string paramNameStr)
        {
            this.ParamInfoLst.Clear();
            RequestOrignalParamLst = new ObservableCollection<DbModels.dv_request_param>(DbHelper.db.Queryable<DbModels.dv_request_param>().ToList());
            RequestParamSourceLst = new ObservableCollection<DbModels.dv_request_param_source>(DbHelper.db.Queryable<DbModels.dv_request_param_source>().ToList());
            foreach (var orignalParam in RequestOrignalParamLst)
            {
                DataView_Configuration.RequestParamContentModel rpc = JsonConvert.DeserializeObject<RequestParamContentModel>(orignalParam.param_content);
                var paraminfo = new ParamModel
                {
                    ParamID=orignalParam.param_id,
                    ParamName=orignalParam.param_name,
                    ParamDesc=orignalParam.param_desc,
                    ParamSource=Utli.ConvertToEnum<RequestParamSource>(orignalParam.param_source.ToString()),
                    JsonVariableName = orignalParam.param_json_variable_name,
                    TargetValType= Utli.ConvertToEnum<ParamTargetType>(orignalParam.value_type),
                };
                if (rpc != null)
                {
                    paraminfo.ConstValue = rpc.constant_value;
                    paraminfo.MacroName = rpc.macro_name;
                    paraminfo.ControlInternalName = rpc.control_internal_name;
                    paraminfo.ControlComboxIndexOrTextType = rpc.control_combox_data_type;
                    paraminfo.TagInternalName = rpc.tag_internal_name;
                    paraminfo.TagValueJsonPath = rpc.tag_value_json_path;
                    paraminfo.ExpressionStr = rpc.value_expression;
                };
                if (!String.IsNullOrEmpty(paramNameStr)&& !paraminfo.ParamName.Contains(paramNameStr))
                {
                    continue;
                }
                if (!String.IsNullOrEmpty(paramSourceTypeStr) && !Utli.GetEnumDescription<RequestParamSource>(paraminfo.ParamSource).Contains(paramSourceTypeStr))
                {
                    continue;
                }
                this.ParamInfoLst.Add(paraminfo);
            }
        }
        //搜索
        private void Query(object o)
        {
            //模糊搜索
            RefreshParamLst(this.SearchParamSourceTypeStr,this.SearchParamNameStr);
        }
        private void Edit(object o)
        {
            var selectedParam = o as ParamModel;

            Pages.Popups.ParamEditPopup paramEdit = new Pages.Popups.ParamEditPopup(selectedParam);
            MainWindow.AddMask();
            if (paramEdit.ShowDialog() == true)
            {
                RefreshParamLst(this.SearchParamSourceTypeStr, this.SearchParamNameStr);
            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedParam = o as ParamModel;
            if (selectedParam == null)
            {
                System.Windows.MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (System.Windows.MessageBox.Show("确认要删除该参数吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            //判断是否有接口正在使用该参数
            var relatedInterfaceLst = DbHelper.db.Queryable<DbModels.dv_request_interface>()
                .Where(x => x.request_param_list.Contains(selectedParam.ParamID.ToString())).Select(x => x.request_internal_name).ToList();
            if (relatedInterfaceLst.Count > 0)
            {
                var relatedInterfaceArray = String.Join(",", relatedInterfaceLst.ToArray());
                System.Windows.MessageBox.Show($"无法删除该参数，因为正在被接口：{relatedInterfaceArray} 使用！");
                return;
            }

            var iRet = DbHelper.db.Deleteable<DbModels.dv_request_param>().Where(x=>x.param_id==selectedParam.ParamID).ExecuteCommand();
            if (iRet == 0)
            {
                System.Windows.MessageBox.Show("删除失败！");
            }
            else
            {
                System.Windows.MessageBox.Show("删除成功！");
                RefreshParamLst(this.SearchParamSourceTypeStr, this.SearchParamNameStr);
                //TipsLst.Remove(selectedCrane);
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
