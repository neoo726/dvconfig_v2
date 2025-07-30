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

namespace DataViewConfig.ViewModels
{
    internal class BlockConfigViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
       
        #region Command
        public Command OpenPageCommand { get; set; }
        public Command EditBlockCommand { get; set; }
        public Command EditBayCommand { get; set; }
        public Command DeleteBlockCommand { get; set; }
        public Command DeleteBayCommand { get; set; }
        public Command AddBlockCommand { get; set; }
        public Command SelectBlockCommand { get; set; }
        public Command QueryBlockCommand { get; set; }
        public Command ResetBlockCommand { get; set; }
        public Command QueryBayCommand { get; set; }
        public Command ResetBayCommand { get; set; }
        public Command ImportBlockDataCommand { get; set; }
        public Command ExportBlockDataCommand { get; set; }
        public Command ImportBayDataCommand { get; set; }
        public Command ExportBayDataCommand { get; set; }
        #endregion
        public ObservableCollection<BayModel> BayLst { get; set; }
        public ObservableCollection<BlockModel> BlockLst { get; set; }
        public string BlockName { get; set; }
        public string SelectedBlockName { get; set; }
        public DbModels.block SelectedCurBlock { get; set; }
        public string SearchBlockNameTxt { get; set; }
        public string SearchBlockIDTxt { get; set; }
        public string SearchBayIDTxt { get; set; }
        public string BlockDesc { get; set; }
        public int BlockPlcId { get; set; }
        public int BlockMaxPos { get; set; }
        public int BlockMinPos { get; set; }

        public List<DbModels.block> dbBlockLst;
        public List<DbModels.bay> dbBayLst;
        public BlockConfigViewModel()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                RefreshBlockLst();
            }));
            //OpenPageCommand = new Command(OpenPage);

            AddBlockCommand = new Command(AddNewBlock);
            EditBlockCommand = new Command(EditBlock);
            EditBayCommand = new Command(EditBay);
            DeleteBlockCommand = new Command(DeleteBlock);
            DeleteBayCommand = new Command(DeleteBay);
            SelectBlockCommand = new Command(SelectBlockChanged);
            QueryBlockCommand = new Command(QueryBlock);
            ResetBlockCommand = new Command(ResetBlockQuery);
            ImportBlockDataCommand = new Command(ImportBlockExcelData);
            ExportBlockDataCommand = new Command(ExportBlockExcelData);

            QueryBayCommand = new Command(QueryBay);
            ResetBayCommand = new Command(ResetBayQuery);
            ImportBayDataCommand = new Command(ImportBayExcelData);
            ExportBayDataCommand = new Command(ExportBayExcelData);
        }
        //搜索
        private void QueryBlock(object o)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //模糊搜索
                RefreshBlockLst();
            }));
        }
        private void ResetBlockQuery(object o)
        {
            this.SearchBlockNameTxt = "";
            this.SearchBlockIDTxt = "";
            QueryBlock(null);
        }
        //搜索
        private void QueryBay(object o)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //模糊搜索
                if (string.IsNullOrEmpty(SelectedBlockName)) return;
                var curBlock = dbBlockLst.Where(x => x.block_name == SelectedBlockName).FirstOrDefault();
                if (curBlock == null) return;
                RefreshBayLst((int)curBlock.block_plc_id);
            }));

        }
        private void ResetBayQuery(object o)
        {
            this.SearchBayIDTxt = "";
            QueryBay(null);
        }
        private void SelectBlockChanged(object o)
        {
            if (string.IsNullOrEmpty(SelectedBlockName))
            {
                SelectedCurBlock = null;
                return;
            };
            var curBlock = dbBlockLst.Where(x => x.block_name == SelectedBlockName).FirstOrDefault();
            if (curBlock == null)
            {
                SelectedCurBlock = null;
                return;
            };
            SelectedCurBlock = curBlock;
            Task.Factory.StartNew(new Action(() =>
            {
                RefreshBayLst((int)curBlock.block_plc_id);
            }));
           
        }
        private void RefreshBlockLst()
        {
            dbBlockLst = DbHelper.db.Queryable<DbModels.block>()
                .Where(x=>string.IsNullOrEmpty(this.SearchBlockNameTxt)||x.block_name.Contains(this.SearchBlockNameTxt))
                .Where(x=>string.IsNullOrEmpty(this.SearchBlockIDTxt)||x.block_plc_id.ToString().Equals(this.SearchBlockIDTxt)).ToList();
            BlockLst = new ObservableCollection<BlockModel>();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var item in dbBlockLst)
                {
                    BlockLst.Add(new BlockModel()
                    {
                        BlockID = (int)item.block_plc_id,
                        BlockName = item.block_name,
                        BlockDesc=item.block_desc,
                        BlockMinPos = item.block_pos_min,
                        BlockMaxPos = item.block_pos_max,
                        //BlockBayCount = dbBayLst != null ? dbBayLst.Where(x => x.block_id == (int)item.block_plc_id).Count() : 0,
                    });
                }
            }));
           
        }
        private void RefreshBayLst(int blockId)
        {
            dbBayLst = DbHelper.db.Queryable<DbModels.bay>().Where(x=>x.block_id==blockId&&(string.IsNullOrEmpty(SearchBayIDTxt)||x.bay_no.Contains(SearchBayIDTxt))).ToList();
            BayLst = new ObservableCollection<BayModel>();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var item in dbBayLst)
                {
                    var block = dbBlockLst.Where(x => x.block_id == item.block_id).FirstOrDefault();
                    if (block == null) continue;
                    BayLst.Add(new BayModel()
                    {
                        //ID=item.id,
                        BayID = item.bay_no,
                        GantryPosition = item.gantry_position,
                        BlockID = item.block_id,
                        BlockName = block.block_name,
                    });
                }
                if (BayLst == null || BayLst.Count == 0)
                {
                    System.Windows.MessageBox.Show("未找到该堆场的贝位列表！");
                }
            }));
            
        }
        private void EditBlock(object o)
        {
            try
            {
                var block = o as BlockModel;
                BlockEditPopup ce = new BlockEditPopup(block);
                if (ce.ShowDialog() == true)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        //模糊搜索
                        RefreshBlockLst();
                    }));
                }

            }
            catch (Exception ex)
            {

                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"弹出编辑页面失败！异常信息:{ex.ToString()}"); return;
            }
        }
        private void EditBay(object o)
        {
            try
            {
                var bay = o as BayModel;
                BayEditPopup ce = new BayEditPopup(bay);
                if (ce.ShowDialog() == true)
                {
                    if (string.IsNullOrEmpty(SelectedBlockName)) return;
                    var curBlock = dbBlockLst.Where(x => x.block_name == SelectedBlockName).FirstOrDefault();
                    if (curBlock == null) return;
                    Task.Factory.StartNew(new Action(() =>
                    {
                        RefreshBayLst((int)curBlock.block_plc_id);
                    }));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"弹出编辑页面失败！异常信息:{ex.ToString()}"); return;
            }
        }
        private void DeleteBlock(object o)
        {
            var selectedBlock = o as BlockModel;
            if (selectedBlock == null)
            {
                System.Windows.MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (System.Windows.MessageBox.Show("确认要删除该堆场吗？注意：该堆场所属的贝位也将一起删除！", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            try
            {
                DbHelper.db.BeginTran();
                DbHelper.db.Deleteable<DbModels.block>().Where(x => x.block_id == selectedBlock.BlockID).ExecuteCommand();
                DbHelper.db.Deleteable<DbModels.bay>().Where(x => x.block_id == selectedBlock.BlockID).ExecuteCommand();
                DbHelper.db.CommitTran();
                System.Windows.MessageBox.Show("删除堆场成功！");
                Task.Factory.StartNew(new Action(() =>
                {
                    //模糊搜索
                    RefreshBlockLst();
                }));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("删除堆场失败！");
                DbHelper.db.RollbackTran();
            }
        }
        private void DeleteBay(object o)
        {
            try
            {
                var selectedBay = o as BayModel;
                if (selectedBay == null)
                {
                    System.Windows.MessageBox.Show("程序内部错误!选中内容无法识别！");
                    return;
                }
                if (System.Windows.MessageBox.Show("确认要删除该贝位吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
                var iRet = DbHelper.db.Deleteable<DbModels.bay>().Where(x => x.block_id == selectedBay.BlockID && x.bay_no == selectedBay.BayID).ExecuteCommand();
                if (iRet == 0)
                {
                    System.Windows.MessageBox.Show("删除贝位失败！");
                }
                else
                {
                    System.Windows.MessageBox.Show("删除贝位成功！");
                    if (string.IsNullOrEmpty(SelectedBlockName)) return;
                    var curBlock = dbBlockLst.Where(x => x.block_name == SelectedBlockName).FirstOrDefault();
                    if (curBlock == null) return;
                    Task.Factory.StartNew(new Action(() =>
                    {
                        RefreshBayLst((int)curBlock.block_plc_id);
                    }));
                    //RcsLst.Remove(selectedRcs);
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"删除失败！异常信息:{ex.ToString()}"); return;
            }
        }
        private void AddNewBlock(object o )
        {
            try
            {
                if (string.IsNullOrEmpty(BlockName) || BlockPlcId == 0)
                {
                    System.Windows.MessageBox.Show("堆场名称或堆场ID不能为空或0！"); return;
                }
                if (dbBlockLst.Exists(x => x.block_name == BlockName || x.block_plc_id == BlockPlcId))
                {
                    System.Windows.MessageBox.Show("已存在有重复名称或ID的堆场！"); return;
                }
                var newBlock = new DbModels.block()
                {
                    block_plc_id = BlockPlcId,
                    block_id = BlockPlcId,
                    block_name = BlockName,
                    block_desc = BlockDesc,
                    block_pos_min = BlockMinPos,
                    block_pos_max = BlockMaxPos,
                };

                var affectedRow = DbHelper.db.Insertable<DbModels.block>(newBlock).ExecuteCommand();
                if (affectedRow > 0)
                {
                    System.Windows.MessageBox.Show("堆场添加成功！");
                    Task.Factory.StartNew(new Action(() =>
                    {
                        //模糊搜索
                        RefreshBlockLst();
                    }));
                }
                else
                {
                    System.Windows.MessageBox.Show($"堆场添加失败！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"添加失败！异常信息:{ex.ToString()}"); return;
            }
        }
        private void ImportBlockExcelData(object p)
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
                    if (!allColumns.Contains("block_name") || !allColumns.Contains("block_plc_id") || !allColumns.Contains("block_id"))
                    {
                        System.Windows.Forms.MessageBox.Show("导入数据字段不匹配，请重新确认！"); return;
                    }
                    var dataLst = MiniExcel.Query<DbModels.block>(fileDialog.FileName, excelType: ExcelType.XLSX);
                    if (dataLst == null || dataLst.Count() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("载入csv文件失败，请检查csv文件格式！"); return;
                    }
                    
                    var duplicatedLst = dataLst.GroupBy(x => x.block_name).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                    if (duplicatedLst.Count() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("csv文件中有重复项！"); return;
                    }
                    try
                    {
                        DbHelper.db.BeginTran();
                        DbHelper.db.DbMaintenance.TruncateTable<DbModels.block>();
                        //var tmpLst = dataLst.ToList();
                        //for (int i = 0; i < dataLst.ToList().Count; i++)
                        //{
                        //    dataLst[i].id=default;
                        //}
                        DbHelper.db.Insertable<DbModels.block>(dataLst.ToList()).ExecuteCommand();
                        DbHelper.db.CommitTran();
                        System.Windows.Forms.MessageBox.Show("导入成功！");
                        Task.Factory.StartNew(new Action(() =>
                        {
                            //模糊搜索
                            RefreshBlockLst();
                        }));
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("导入失败！" + ex.ToString());
                        DbHelper.db.RollbackTran();
                    }
                }
            }
            catch (Exception ex) 
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"导入失败！异常信息:{ex.ToString()}"); return;
            }
            
        }
        private void ExportBlockExcelData(object o)
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
                var exportDataPath = dataPath + "\\Block_" + DateTime.Now.ToString("yyyy-MM-d-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<DbModels.block>(this.dbBlockLst), excelType: ExcelType.XLSX, overwriteFile: true);
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, " + exportDataPath;
                p.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"导出失败！异常信息:{ex.ToString()}"); return;
            }
        }
        /// <summary>
        /// 导入当前堆场
        /// </summary>
        /// <param name="p"></param>
        private void ImportBayExcelData(object p)
        {
            try
            {
                if (SelectedCurBlock == null)
                {
                    System.Windows.Forms.MessageBox.Show("请先选择堆场！"); return;
                }
                OpenFileDialog fileDialog = new OpenFileDialog();
                var appDirectory = System.Environment.CurrentDirectory.ToString();
                var dataPath = System.Environment.CurrentDirectory.ToString() + "\\Data";
                fileDialog.InitialDirectory = dataPath;
                //fileDialog.FileName = "Image|*.png;";
                fileDialog.Filter = "Excel|*.xlsx;";
                DialogResult result = fileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (System.Windows.Forms.MessageBox.Show("确认要导入数据吗？导入后会覆盖原有数据！注意：ID列必须", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                    var allColumns = MiniExcel.GetColumns(fileDialog.FileName, excelType: ExcelType.XLSX, useHeaderRow: true);
                    if (!allColumns.Contains("BayID") || !allColumns.Contains("GantryPosition") || !allColumns.Contains("BlockID") || !allColumns.Contains("BlockName"))
                    {
                        System.Windows.Forms.MessageBox.Show("导入数据字段不匹配，请重新确认！"); return;
                    }
                    var dataLst = MiniExcel.Query<BayModel>(fileDialog.FileName, excelType: ExcelType.XLSX);
                    if (dataLst == null || dataLst.Count() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("载入Excel文件失败，请检查Excel文件格式！"); return;
                    }
                    dataLst = dataLst.Where(x => x.BlockName == SelectedCurBlock.block_name);
                    var newBayLst = dataLst.ToList();
                    var duplicatedLst = newBayLst.Where(x => x.BlockName == SelectedCurBlock.block_name).GroupBy(x => new { x.BayID, x.BlockName }).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                    if (duplicatedLst.Count() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Excel文件中有重复项(bay_id重复，同时block_id也重复！"); return;
                    }
                    try
                    {
                       
                        //DbHelper.db.DbMaintenance.TruncateTable<DbModels.bay>();
                        //var tmpLst = dataLst.ToList();
                        //for (int i = 0; i < dataLst.ToList().Count; i++)
                        //{
                        //    dataLst[i].id=default;
                        //}
                        var deletedBayLst = BayLst.Where(x => x.BlockID == SelectedCurBlock.block_id && !newBayLst.Any(n => n.BayID == x.BayID)).ToList();
                        var insertedBayLst = newBayLst.Where(x => x.BlockID == SelectedCurBlock.block_id && !BayLst.Any(n => n.BayID == x.BayID)).ToList();
                        var updatedBayLst = newBayLst.Where(x => x.BlockID == SelectedCurBlock.block_id && BayLst.Any(n => n.BayID == x.BayID)).ToList();
                        var deletedDbBayLst = new List<DbModels.bay>();
                        var updatedDbBayLst = new List<DbModels.bay>();
                        foreach (var item in this.dbBayLst)
                        {
                            if (deletedBayLst.Any(x => x.BayID == item.bay_no))
                            {
                                deletedDbBayLst.Add(item);
                            }
                           
                        }
                        foreach (var item in updatedBayLst)
                        {
                            var dbId = dbBayLst.FirstOrDefault(x => x.block_id == item.BlockID && x.bay_no == item.BayID).id;
                            updatedDbBayLst.Add(new DbModels.bay()
                            {
                                 id= dbId,
                                 bay_no=item.BayID,
                                 block_id=item.BlockID,
                                 gantry_position=item.GantryPosition,
                            });
                        }
                        var insertedDbBayLst = new List<DbModels.bay>();
                        foreach (var item in insertedBayLst)
                        {
                            insertedDbBayLst.Add(new DbModels.bay()
                            {
                                bay_no = item.BayID,
                                block_id = item.BlockID,
                                gantry_position = item.GantryPosition,
                            });
                        }
                        DbHelper.db.BeginTran();
                        if (deletedBayLst != null)
                        {
                            DbHelper.db.Deleteable<DbModels.bay>(deletedDbBayLst).ExecuteCommand();

                        }
                        if (updatedDbBayLst != null)
                        {
                            DbHelper.db.Updateable<DbModels.bay>(updatedDbBayLst).ExecuteCommand();

                        }
                        if (insertedBayLst != null)
                        {
                            DbHelper.db.Insertable<DbModels.bay>(insertedDbBayLst).ExecuteCommand();

                        }
                        DbHelper.db.CommitTran();
                        System.Windows.Forms.MessageBox.Show("导入成功！");
                        Task.Factory.StartNew(new Action(() =>
                        {
                            //模糊搜索
                            if (string.IsNullOrEmpty(SelectedBlockName)) return;
                            var curBlock = dbBlockLst.Where(x => x.block_name == SelectedBlockName).FirstOrDefault();
                            if (curBlock == null) return;
                            RefreshBayLst((int)curBlock.block_plc_id);
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
        private void ExportBayExcelData(object o)
        {
            try
            {
                if (SelectedCurBlock == null)
                {
                    System.Windows.Forms.MessageBox.Show("请先选择堆场！"); return;
                }
                if (this.dbBayLst.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("当前堆场没有贝位数据！"); return;
                }
                //保存文件
                var appDirectory = System.Environment.CurrentDirectory.ToString();
                var dataPath = System.Environment.CurrentDirectory.ToString() + "\\Data";
                //目录不存在时，自动创建目录
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }
                var exportDataPath = dataPath + "\\Bay_" + DateTime.Now.ToString("yyyy-mm-dd-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<BayModel>(BayLst), excelType: ExcelType.XLSX, overwriteFile: true);
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, " + exportDataPath;
                p.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"导出失败！异常信息:{ex.ToString()}"); return;
            }
        }
    }
    public class BlockModel
    {
        public string BlockName { get; set; }
        public int BlockID { get; set; }
        public string BlockDesc { get; set; }
        public int BlockMaxPos { get; set; }
        public int BlockMinPos { get; set; }
        public int BlockBayCount { get; set; }
    }
    public class BayModel
    {
        //public int ID { get; set; }
        public string BayID { get; set; }
        public int GantryPosition { get; set; }
        public int BlockID { get; set; }
        public string BlockName { get; set; }
    }
}

