using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using CMSCore;
using System.Windows.Forms;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using MiniExcelLibs;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;

namespace DataViewConfig.ViewModels
{
    internal class ControlConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;      
       
        public ObservableCollection<ControlModel> ControlLst { get; set; }   
        public ObservableCollection<DbModels.dv_screen_conf> ScreenNameLst { get; set; }
        public DbModels.dv_screen_conf SelectedScreen { get; set; }
        public string ControlInternalName { get; set; }
        public string SearchScreenCswName { get; set; }
        public DbModels.dv_screen_conf SearchScreen { get; set; }
        public string SearchControlType { get; set; }
        public string SearchTxt { get; set; }
        public ControlType CurControlType { get; set; }
        public int AccessId { get; set; }
        #region Command
        public Command QueryCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command ResetCommand { get; set; }
        public Command ImportDataCommand { get; set; }
        public Command ExportDataCommand { get; set; }
        #endregion
        public ControlConfigPageViewModel()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //获取参数点
                var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
                  .Where(x => !x.dv_screen_csw_name.Contains(","))
                  .ToList();
                ScreenNameLst = new ObservableCollection<DbModels.dv_screen_conf>(dvScreenLst);
                RefreshControlLst();
            }));
            
            QueryCommand = new Command(Query);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            AddNewCommand = new Command(AddNewControl);
            ResetCommand = new Command(Reset);
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
                fileDialog.Filter = "Excel|*.xlsx;";
                DialogResult result = fileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (System.Windows.Forms.MessageBox.Show("确认要导入数据吗？导入后会覆盖原有数据！", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                    var allColumns = MiniExcel.GetColumns(fileDialog.FileName, excelType: ExcelType.XLSX, useHeaderRow: true);
                    //if (!allColumns.Contains("dv_screen_id") || !allColumns.Contains("dv_screen_internal_name") || !allColumns.Contains("dv_screen_csw_name") || !allColumns.Contains("dv_screen_desc"))
                    //{
                    //    System.Windows.Forms.MessageBox.Show("导入数据字段不匹配，请重新确认！"); return;
                    //}
                    var dataLst = MiniExcel.Query<ControlModel>(fileDialog.FileName, excelType: ExcelType.XLSX);
                    if (dataLst == null || dataLst.Count() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("载入Excel文件失败，请检查Excel文件格式！"); return;
                    }
                    //剔除无效数据
                    dataLst = dataLst.Where(x => !string.IsNullOrEmpty(x.ControlInternalName) && !string.IsNullOrEmpty(x.ScreenInternalName) && (int)x.AccessID > 0).ToList();
                    var duplicatedLst = dataLst.GroupBy(x => new { x.ControlInternalName }).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                    if (duplicatedLst.Count() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Excel文件中有Control Internal Name重复！" + string.Join(",", duplicatedLst)); return;
                    }
                    try
                    {
                        var deletedTagLst = this.ControlLst.Where(x => !dataLst.Any(n => n.ControlInternalName == x.ControlInternalName)).ToList();
                        var insertedTagLst = dataLst.Where(x => !this.ControlLst.Any(n => n.ControlInternalName == x.ControlInternalName)).ToList();
                        var updatedTagLst = dataLst.Where(x => this.ControlLst.Any(n => n.ControlInternalName == x.ControlInternalName)).ToList();
                        var deletedDbTagLst = new List<DbModels.dv_control_conf>();
                        var updatedDbTagLst = new List<DbModels.dv_control_conf>();
                        foreach (var item in this.ControlLst)
                        {
                            if (deletedTagLst.Any(x => x.ControlInternalName == item.ControlInternalName))
                            {
                                deletedDbTagLst.Add(new DbModels.dv_control_conf()
                                {
                                    dv_control_id=item.ControlID,
                                    dv_control_access_id=item.AccessID,
                                    dv_control_internal_name=item.ControlInternalName,
                                    dv_control_desc=item.ControlDesc,
                                });
                            }
                        }
                        foreach (var item in updatedTagLst)
                        {
                            updatedDbTagLst.Add(new DbModels.dv_control_conf()
                            {
                                dv_control_id = item.ControlID,
                                dv_control_access_id = item.AccessID,
                                dv_control_internal_name = item.ControlInternalName,
                                dv_control_desc = item.ControlDesc,
                                dv_control_type_id=(int)item.ControlType,
                                dv_screen_id=ScreenNameLst.Where(x=>x.dv_screen_internal_name==item.ScreenInternalName).First().dv_screen_id,
                            });
                        }
                        var insertedDbTagLst = new List<DbModels.dv_control_conf>();
                        foreach (var item in insertedTagLst)
                        {
                            insertedDbTagLst.Add(new DbModels.dv_control_conf()
                            {
                                dv_control_id = item.ControlID,
                                dv_control_access_id = item.AccessID,
                                dv_control_internal_name = item.ControlInternalName,
                                dv_control_desc = item.ControlDesc,
                                dv_control_type_id = (int)item.ControlType,
                                dv_screen_id = ScreenNameLst.Where(x => x.dv_screen_internal_name == item.ScreenInternalName).First().dv_screen_id,
                            });
                        }
                        DbHelper.db.BeginTran();
                        DbHelper.db.Deleteable<DbModels.dv_control_conf>(deletedDbTagLst).ExecuteCommand();
                        DbHelper.db.Updateable<DbModels.dv_control_conf>(updatedDbTagLst).ExecuteCommand();
                        DbHelper.db.Insertable<DbModels.dv_control_conf>(insertedDbTagLst).ExecuteCommand();
                        DbHelper.db.CommitTran();
                        System.Windows.Forms.MessageBox.Show("导入成功！");
                        Task.Factory.StartNew(new Action(() =>
                        {
                            RefreshControlLst();
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
                var exportDataPath = dataPath + "\\Control_" + DateTime.Now.ToString("yyyy-mm-dd-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<ControlModel>(ControlLst), excelType: ExcelType.XLSX, overwriteFile: true);
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
        private void Reset(object o)
        {
            SearchScreen = null;
            //SearchScreenCswName = string.Empty;
            SearchControlType = string.Empty;
            SearchTxt = string.Empty;
            RefreshControlLst();
        }
        private void RefreshControlLst()
        {
            ControlLst = new ObservableCollection<ControlModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_control_conf>()
                .LeftJoin<DbModels.dv_control_type>((o, cus) => o.dv_control_type_id == cus.dv_control_type_id)
                .LeftJoin<DbModels.dv_screen_conf>((o, cus, p) => p.dv_screen_id == o.dv_screen_id)
                .Select((o, cus, p) => new
                {
                    ControlId = o.dv_control_id,
                    AccessId = o.dv_control_access_id,
                    ScreenInternalName = p.dv_screen_internal_name,
                    ScreenCswName=p.dv_screen_csw_name,
                    ControlTypeName = cus.dv_control_type_name,
                    ControlInternalName = o.dv_control_internal_name,
                    ControlDesc=o.dv_control_desc,
                }).ToList();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var item in curRequests)
                {
                    if (item.ControlTypeName == null) continue;
                    var curControl = new ControlModel()
                    {
                        ControlID = item.ControlId,
                        AccessID = item.AccessId,
                        ScreenInternalName = item.ScreenInternalName,
                        ScreenCswName = item.ScreenCswName,
                        ControlType = (ControlType)Enum.Parse(typeof(ControlType), item.ControlTypeName, true),
                        ControlInternalName = item.ControlInternalName,
                        ControlDesc = item.ControlDesc,
                    };
                    //模糊搜索，输入返回值数值/描述
                    //if (!string.IsNullOrEmpty(SearchScreenCswName) && curControl.ScreenCswName != SearchScreenCswName)
                    //{
                    //    continue;
                    //}
                    if (SearchScreen != null && curControl.ScreenCswName != SearchScreen.dv_screen_csw_name)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(SearchControlType) && !SearchControlType.Contains(curControl.ControlType.ToString()))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(SearchTxt) && !curControl.ControlInternalName.ToLower().Contains(SearchTxt.ToLower()) &&
                       (curControl.ControlDesc == null || curControl.ControlDesc != null && !curControl.ControlDesc.ToLower().Contains(SearchTxt.ToLower())) &&
                        !curControl.AccessID.ToString().Contains(SearchTxt.ToLower())
                        )
                    {
                        continue;
                    }
                    ControlLst.Add(curControl);
                }

            }));
        }
        //搜索
        private void Query(object o)
        {
            RefreshControlLst();
        }
        private void Edit(object o)
        {
            var crane = o as ControlModel;
            ControlEditPopup ce = new ControlEditPopup(crane);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                RefreshControlLst();
            }
            MainWindow.RemoveMask();
        }
        private void Delete(object o)
        {
            var selectedCrane = o as ControlModel;
            if (selectedCrane == null)
            {
                System.Windows.Forms.MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (System.Windows.MessageBox.Show("确认要删除该控件吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_control_conf>().Where(x=>x.dv_control_id==selectedCrane.ControlID).ExecuteCommand();
            if (iRet == 0)
            {
                System.Windows.MessageBox.Show("删除失败！");
            }
            else
            {
                //MessageBox.Show("删除成功！");
                ControlLst.Remove(selectedCrane);
            }
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddNewControl(object o)
        {
            if (string.IsNullOrEmpty(this.ControlInternalName))
            {
                MessageBox.Show("请输入控件名称！"); return;
            }

            if (SelectedScreen==null||SelectedScreen.dv_screen_id <= 0)
            {
                MessageBox.Show("请选择控件所属画面！"); return;
            }
            if (this.AccessId.ToString().Length != 4|| this.AccessId<0)
            {
                MessageBox.Show("输入的AccessID错误！"); return;
            }
            if ((int)this.CurControlType == 0)
            {
                MessageBox.Show("请选择控件类型！"); return;
            }
            if (ControlLst.Where(x => x.ControlInternalName == this.ControlInternalName).Count() > 0)
            {
                MessageBox.Show("已存在重复的控件配置项(Control Internal Name重复)！"); return;
            }
            var dv_controls = new DbModels.dv_control_conf();

            dv_controls.dv_control_internal_name = this.ControlInternalName;
            dv_controls.dv_control_access_id = this.AccessId;
            dv_controls.dv_control_type_id = (int)this.CurControlType;
            dv_controls.dv_screen_id = this.SelectedScreen.dv_screen_id;



            int affectedRow = DbHelper.db.Insertable<DbModels.dv_control_conf>(dv_controls).ExecuteCommand();
            if (affectedRow != 0)
            {
                RefreshControlLst();
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
    public class ControlModel
    {
        public int ControlID { get; set; }
        public int AccessID { get; set; }
        public string ControlInternalName { get; set; }
        public string ScreenInternalName { get; set; }
        public string ScreenCswName { get; set; }
        public ControlType ControlType { get; set; }
       public string ControlDesc { get; set; }

    }
}
