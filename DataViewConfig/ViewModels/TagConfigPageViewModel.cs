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
using System.Windows.Forms;
using CMSCore;
using DataView_Configuration;
using MiniExcelLibs;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class TagConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        public ObservableCollection<TagModel> TagLst { get; set; }

        public string TagInternalName { get; set; }

        public string CurTagPostFixType { get; set; }

        public TagDataType CurTagDataType { get; set; }

        public string RelatedMacroName { get; set; }

        public string RelatedTagInternalName { get; set; }

        private string tagRealName { get; set; }
        public string TagRealName
        {
            get => tagRealName;
            set {
                tagRealName = value; 
                if (TagNameKeepSame)
                {
                    TagInternalName = value;
                }
            }
        }
        public string TagDesc { get; set; }
        public string SearchTagType { get; set; }
        public string SearchTagName { get; set; }
        public string SearchTagPrefix { get; set; }
        public string SearchMacro { get; set; }

        public ObservableCollection<DbModels.dv_tag_data_type> TagDataTypeLst { get; set; }
        public ObservableCollection<DbModels.dv_tag_postfix_type> TagPostfixLst { get; set; }
        public ObservableCollection<DbModels.dv_system> DvSystemLst { get; set; }
        private  List<DbModels.dv_tag> dbTagLst { get; set; }
        public bool TagNameKeepSame { get; set; }
        public bool IsTagNameEditable { get; set; }
        #region COmmand
        public Command QueryCommand { get; set; }
        public Command ResetCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command AddNewCommand { get; set; }
        public Command ImportDataCommand { get; set; }
        public Command ExportDataCommand { get; set; }
        #endregion
        public TagConfigPageViewModel()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                DvSystemLst = new ObservableCollection<DbModels.dv_system>(DbHelper.db.Queryable<DbModels.dv_system>().ToList());
                TagNameKeepSame = true;
                RefreshTagLst();
                RefreshSearchInfo();
            }));
           
            QueryCommand = new Command(Query);
            AddNewCommand = new Command(AddNew);
            EditCommand =new Command(Edit);
            DeleteCommand = new Command(Delete);
            ResetCommand = new Command(ResetQuery);
            ImportDataCommand = new Command(ImportExcelData);
            ExportDataCommand = new Command(ExportExcelData);
        }
        private void RefreshSearchInfo()
        {
            var tagDataTypeLst = DbHelper.db.Queryable<DbModels.dv_tag_data_type>().ToList();
            var tagPrefixLst = DbHelper.db.Queryable<DbModels.dv_tag_postfix_type>().ToList();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                TagDataTypeLst = new ObservableCollection<DbModels.dv_tag_data_type>(tagDataTypeLst);

                TagPostfixLst = new ObservableCollection<DbModels.dv_tag_postfix_type>(tagPrefixLst);
            }));
        }
        private void ResetQuery(object o)
        {
            this.SearchMacro = String.Empty;
            this.SearchTagName = String.Empty;
            this.SearchTagPrefix = String.Empty;
            this.SearchTagType = String.Empty;
            Query(o);
        }
        private void RefreshTagLst()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                dbTagLst = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();
                TagLst = new ObservableCollection<TagModel>();
                var curRequests = DbHelper.db.Queryable<DbModels.dv_tag>()
                    .LeftJoin<DbModels.dv_tag_data_type>((o, cus) => o.data_type_id == cus.data_type_id)
                    .LeftJoin<DbModels.dv_tag_postfix_type>((o, cus, p) => p.postfix_type_id == o.postfix_type_id)
                    .Where((o, cus, p) => string.IsNullOrEmpty(SearchTagName) || (o.tag_internal_name.ToLower().Contains(SearchTagName.ToLower()) || o.tag_name.ToLower().Contains(SearchTagName.ToLower()) || o.tag_desc.ToLower().Contains(SearchTagName.ToLower())))
                    .Where((o, cus, p) => string.IsNullOrEmpty(SearchMacro) || (o.related_macro_name.ToLower().Contains(SearchMacro.ToLower())))
                    .Where((o, cus, p) => string.IsNullOrEmpty(SearchTagType) || (cus.data_type_desc.ToLower().Equals(SearchTagType.ToLower())))
                    .Where((o, cus, p) => string.IsNullOrEmpty(SearchTagPrefix) || (p.postfix_type_desc.ToLower().Equals(SearchTagPrefix.ToLower())))

                    .ToList();
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (var item in curRequests)
                    {
                        var tag = new TagModel()
                        {
                            Id = item.id,
                            TagInternalName = item.tag_internal_name,
                            Desc = item.tag_desc,
                            TagName = item.tag_name,
                            DataType = (TagDataType)Enum.Parse(typeof(TagDataType), item.data_type_id.ToString(), true),
                            Postfix = (TagPostfixType)Enum.Parse(typeof(TagPostfixType), item.postfix_type_id.ToString(), true),
                            RelatedMacroName = item.related_macro_name,
                            RelatedTagInternalName = item.related_tag_internal_name,
                            TagNameEditable = item.is_internal_name_editable,
                            RelatedSystemIds = item.related_system_id,

                        };
                        TagLst.Add(tag);
                    }
                }));
            }));
        }
        //搜索
        private void Query(object o)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //var searchtxt = o.ToString();
                //模糊搜索
                RefreshTagLst();
            }));
           
        }
        private void Edit(object o)
        {
            try
            {
                var t = o as TagModel;

                Pages.Popups.TagEditPopup tagEdit = new Pages.Popups.TagEditPopup(t);
                MainWindow.AddMask();
                if (tagEdit.ShowDialog() == true)
                {
                    RefreshTagLst();
                }
                MainWindow.RemoveMask();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"弹出编辑页面失败！异常信息:{ex.ToString()}"); return;
            }
        }
        private void Delete(object o)
        {
            try
            {
                var selectedCrane = o as TagModel;
                if (selectedCrane == null)
                {
                    System.Windows.MessageBox.Show("程序内部错误!选中内容无法识别！");
                    return;
                }
                if (System.Windows.MessageBox.Show("确认要删除该点名吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
                var iRet = DbHelper.db.Deleteable<DbModels.dv_tag>().Where(x => x.id == selectedCrane.Id).ExecuteCommand();
                if (iRet == 0)
                {
                    System.Windows.MessageBox.Show("删除失败！");
                }
                else
                {
                    //MessageBox.Show("删除成功！");
                    TagLst.Remove(selectedCrane);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"弹出编辑页面失败！异常信息:{ex.ToString()}"); return;
            }
        }
        private DbModels.dv_tag_postfix_type GetPostfixType(string postfixDesc)
        {
            if (string.IsNullOrEmpty(postfixDesc)) return default;
            var tagPostfixType=TagPostfixLst.Where(x => x.postfix_type_desc == postfixDesc).FirstOrDefault();
            return tagPostfixType;
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void AddNew(object o)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TagInternalName))
                {
                    System.Windows.MessageBox.Show("输入信息不能为空！"); return;
                }
                if (this.CurTagPostFixType == null)
                {
                    System.Windows.MessageBox.Show("请选择点名后缀类型！"); return;
                }
                if (this.CurTagDataType == 0)
                {
                    System.Windows.MessageBox.Show("请选择点名类型！"); return;
                }
                if (TagLst.Where(x => x.TagInternalName == this.TagInternalName || x.TagName == this.tagRealName).Count() > 0)
                {
                    System.Windows.MessageBox.Show("已存在重复点名！"); return;
                }
                var dv_tags = new DbModels.dv_tag();
                dv_tags.tag_internal_name = this.TagInternalName;
                dv_tags.tag_name = this.TagRealName;
                dv_tags.tag_desc = this.TagDesc;
                dv_tags.postfix_type_id = GetPostfixType(this.CurTagPostFixType).postfix_type_id;
                dv_tags.data_type_id = (int)this.CurTagDataType;
                //dv_tags.data_type_id=(int)this.CurTagDataType;
                dv_tags.related_macro_name = this.RelatedMacroName;
                int affectedRow = DbHelper.db.Insertable<DbModels.dv_tag>(dv_tags).ExecuteCommand();
                if (affectedRow != 0)
                {
                    RefreshTagLst();
                    System.Windows.MessageBox.Show("添加成功！");
                }
                else
                {
                    System.Windows.MessageBox.Show("添加失败！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show($"添加失败！异常信息:{ex.ToString()}"); return;
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
                    if (!allColumns.Contains("TagInternalName") || !allColumns.Contains("TagName") || !allColumns.Contains("DataType") || !allColumns.Contains("Postfix"))
                    {
                        System.Windows.Forms.MessageBox.Show("导入数据字段不匹配，请重新确认！"); return;
                    }
                    var dataLst = MiniExcel.Query<TagModel>(fileDialog.FileName, excelType: ExcelType.XLSX);
                    if (dataLst == null || dataLst.Count() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("载入Excel文件失败，请检查Excel文件格式！"); return;
                    }
                    dataLst = dataLst.Where(x => !string.IsNullOrEmpty(x.TagInternalName) && !string.IsNullOrEmpty(x.TagName) && (int)x.Postfix > 0 && (int)x.DataType > 0).ToList();
                    var duplicatedLst = dataLst.GroupBy(x => new { x.TagInternalName }).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                    if (duplicatedLst.Count() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Excel文件中有TagInternalName重复！" + string.Join(",", duplicatedLst)); return;
                    }
                    try
                    {
                        DbHelper.db.BeginTran();
                        //DbHelper.db.DbMaintenance.TruncateTable<DbModels.bay>();
                        //var tmpLst = dataLst.ToList();
                        //for (int i = 0; i < dataLst.ToList().Count; i++)
                        //{
                        //    dataLst[i].id=default;
                        //}
                        var deletedTagLst = this.dbTagLst.Where(x => !dataLst.Any(n => n.TagInternalName == x.tag_internal_name)).ToList();
                        var insertedTagLst = dataLst.Where(x => !this.dbTagLst.Any(n => n.tag_internal_name == x.TagInternalName)).ToList();
                        var updatedTagLst = dataLst.Where(x => this.dbTagLst.Any(n => n.tag_internal_name == x.TagInternalName)).ToList();
                        var deletedDbTagLst = new List<DbModels.dv_tag>();
                        var updatedDbTagLst = new List<DbModels.dv_tag>();
                        foreach (var item in this.dbTagLst)
                        {
                            if (deletedTagLst.Any(x => x.tag_internal_name == item.tag_internal_name))
                            {
                                deletedDbTagLst.Add(item);
                            }
                        }
                        foreach (var item in updatedTagLst)
                        {
                            updatedDbTagLst.Add(new DbModels.dv_tag()
                            {
                                tag_internal_name = item.TagInternalName,
                                tag_name = item.TagName,
                                tag_desc = item.Desc,
                                postfix_type_id = (int)item.Postfix,
                                data_type_id = (int)item.DataType,
                                related_macro_name = item.RelatedMacroName
                            });
                        }
                        var insertedDbTagLst = new List<DbModels.dv_tag>();
                        foreach (var item in insertedTagLst)
                        {
                            insertedDbTagLst.Add(new DbModels.dv_tag()
                            {
                                tag_internal_name = item.TagInternalName,
                                tag_name = item.TagName,
                                tag_desc = item.Desc,
                                postfix_type_id = (int)item.Postfix,
                                data_type_id = (int)item.DataType,
                                related_macro_name = item.RelatedMacroName
                            });
                        }
                        DbHelper.db.Deleteable<DbModels.dv_tag>(deletedDbTagLst).ExecuteCommand();
                        DbHelper.db.Updateable<DbModels.dv_tag>(updatedDbTagLst).ExecuteCommand();
                        DbHelper.db.Insertable<DbModels.dv_tag>(insertedDbTagLst).ExecuteCommand();
                        DbHelper.db.CommitTran();
                        System.Windows.Forms.MessageBox.Show("导入成功！");
                        Task.Factory.StartNew(new Action(() =>
                        {
                            RefreshTagLst();
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
                var exportDataPath = dataPath + "\\Tag_" + DateTime.Now.ToString("yyyy-mm-dd-H-mm-ss") + ".xlsx";
                MiniExcel.SaveAs(exportDataPath, new List<TagModel>(TagLst), excelType: ExcelType.XLSX, overwriteFile: true);
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
    }
    public class TagModel
    {
        public int Id { get; set; }
        public string TagInternalName { get; set; }
        public string TagName { get; set; }
        public string Desc { get; set; }
        public TagDataType DataType{ get; set; }
        public TagPostfixType Postfix { get; set; }
        public string RelatedMacroName { get; set; }
        public string RelatedTagInternalName { get; set; }
        public bool TagNameEditable { get; set; }
        public string RelatedSystemIds { get; set; }
        public bool IsSelected { get; set; }
        //public class ReturnValueModel
        //{
        //    public int  return_value { get; set; }
        //    public string  return_desc { get; set; }
        //}

    }
}
