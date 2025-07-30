using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using CMSCore;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class CraneConfigViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;

        #region Command 
        public Command ImportCsvCommand { get; set; }
        public Command ExportCsvCommand { get; set; }
        public Command OpenPageCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCraneCommand { get; set; }
        public Command QuickCreateCommand { get; set; }
        public Command AddNewCraneSpreaderScreenCommand { get; set; }
        public Command RemoveCraneTypeScreenCommand { get; set; }
        public Command AddMultiCraneTypeCommand { get; set; }
        public Command AddMultiCraneGroupCommand { get; set; }
        public Command DeleteCraneTypeCommand { get; set; }
        public Command EditCraneTypeCommand { get; set; }
        public Command EditCraneGroupCommand { get; set; }
        public Command DeleteCraneGroupCommand { get; set; }
        public Command AddNewCraneCommand { get; set; }
        public Command QueryCommand { get; set; }
        public Command ResetCommand { get; set; }
        #endregion
        public ObservableCollection<CraneModel> CraneModelLst { get; set; }
        public ObservableCollection<CraneGroupModel> CraneGroupLst { get; set; }
        public ObservableCollection<DbModels.crane> CraneLst { get; set; }
        public string CranePrefixFormat { get; set; }
        public int CraneCount { get; set; }
        public string StartLocalPLCIP { get; set; }
        private bool craneTypeEnable;
        public bool CraneTypeEnable
        {
            get { return craneTypeEnable; }
            set
            {
                if(value!= craneTypeEnable)
                {
                    //判断是否重复
                    var dvProjConf = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
                    dvProjConf.crane_type_enable = value;
                    var affectedRow=DbHelper.db.Updateable<DbModels.dv_project_conf>(dvProjConf).Where(x=>x.id==dvProjConf.id).ExecuteCommand();
                    if (affectedRow <=0)
                    {
                        MessageBox.Show("切换多机型配置失败，请重启配置工后再重试！"); return;
                    }
                }
                craneTypeEnable = value;
                
            }
        }
        private bool craneGroupEnable;
        public bool CraneGroupEnable
        {
            get { return craneGroupEnable; }
            set
            {
                if (value != craneGroupEnable)
                {
                    //判断是否重复
                    var dvProjConf = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
                    dvProjConf.crane_group_enable = value;
                    var affectedRow = DbHelper.db.Updateable<DbModels.dv_project_conf>(dvProjConf).Where(x => x.id == dvProjConf.id).ExecuteCommand();
                    if (affectedRow <= 0)
                    {
                        MessageBox.Show("切换多机型配置失败，请重启配置工后再重试！"); return;
                    }
                }
                craneGroupEnable = value;
            }
        }
        public bool CraneConfigIPEnable { get; set; }
        public int CraneGroupID { get; set; }
        public string CraneGroupName { get; set; }
        public int CraneTypeID { get; set; }
        public string CraneTypeName { get; set; }
        public string CraneTypeDesc { get; set; }
        public int CraneTypeRelatedScreenId { get; set; }
        public bool CraneTypeSpreaderMatchEnable { get; set; }
        public ObservableCollection<DataView_Configuration.SpreaderMatchScreenModel> CraneSpreaderMatchScreenModelLst { get; set;}

        public ObservableCollection<DbModels.dv_screen_conf> ScreenNameLst { get; set; }
        public ObservableCollection<CraneTypeModel> CraneTypeLst { get; set; }
        public DbModels.dv_screen_conf SelectedScreen { get; set; }

        
        public CraneConfigViewModel()
        {
            ImportCsvCommand = new Command(ImportCsv);
            ExportCsvCommand = new Command(ExportCsv);
            StartLocalPLCIP = "127.0.0.1";
            Task.Factory.StartNew(new Action(() =>
            {
                var dvConfig=DbHelper.db.Queryable<DbModels.dv_project_conf>().First();
                CraneTypeEnable = dvConfig.crane_type_enable;
                CraneGroupEnable = dvConfig.crane_group_enable;
                //
                var dvScreenLst = DbHelper.db.Queryable<DbModels.dv_screen_conf>()
                  .ToList();
                ScreenNameLst = new ObservableCollection<DbModels.dv_screen_conf>(dvScreenLst);
                RefreshCraneTypeLst();
                RefreshCraneGroupLst();
                RefreshCraneLst();

            }));
            CranePrefixFormat = "RXG{00}";

            OpenPageCommand = new Command(OpenPage);
            EditCommand = new Command(EditCrane);
            QuickCreateCommand = new Command(QuickCreateCrane);
            DeleteCraneCommand = new Command(DeleteCrane);
            AddNewCraneSpreaderScreenCommand = new Command(AddNewCraneSpreaderScreen);
            RemoveCraneTypeScreenCommand = new Command(RemoveCraneTypeScreen);
            AddMultiCraneTypeCommand = new Command(AddMultiCraneType);
            AddMultiCraneGroupCommand = new Command(AddMultiCraneGroup);
            DeleteCraneTypeCommand = new Command(DeleteCraneType);
            EditCraneTypeCommand = new Command(EditCraneType);
            QueryCommand = new Command(QueryCrane);
            ResetCommand = new Command(QueryCrane);
            AddNewCraneCommand = new Command(AddNewCrane);

            EditCraneGroupCommand = new Command(EditCraneGroup);
            DeleteCraneGroupCommand = new Command(DeleteCraneGroup);
        }
        private void ImportCsv(object o)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".csv",
                Filter = "CSV Files (*.csv)|*.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var lines = System.IO.File.ReadAllLines(dialog.FileName);
                    var newCranes = new List<DbModels.crane>();

                    // Skip header row
                    foreach (var line in lines.Skip(1))
                    {
                        var values = line.Split(',');
                        if (values.Length >= 3) // ID,名称,IP,类型ID(可选),类别ID(可选)
                        {
                            var crane = new DbModels.crane
                            {
                                crane_id = int.Parse(values[0]),
                                crane_name = values[1],
                                crane_ip = values[2],
                                crane_type_id = values.Length > 3 ? int.Parse(values[3]) : 0,
                                group_id = values.Length > 4 ? int.Parse(values[4]) : 0
                            };

                            // 检查新导入的数据中是否有重复
                            if (newCranes.Any(x => x.crane_id == crane.crane_id || x.crane_name == crane.crane_name))
                            {
                                MessageBox.Show($"CSV文件中存在重复的设备ID或名称: ID={crane.crane_id}, 名称={crane.crane_name}");
                                return;
                            }
                            newCranes.Add(crane);
                        }
                    }

                    // 获取现有设备列表
                    var existingCranes = DbHelper.db.Queryable<DbModels.crane>().ToList();

                    try
                    {
                        // 开始事务
                        DbHelper.db.BeginTran();

                        // 删除所有现有设备
                        if (existingCranes.Any())
                        {
                            var deleteResult = DbHelper.db.Deleteable<DbModels.crane>().ExecuteCommand();
                            if (deleteResult <= 0)
                            {
                                throw new Exception("删除现有设备失败");
                            }
                        }

                        // 插入新设备
                        var affectedRows = DbHelper.db.Insertable(newCranes).ExecuteCommand();
                        if (affectedRows != newCranes.Count)
                        {
                            throw new Exception("插入新设备数量不匹配");
                        }

                        // 提交事务
                        DbHelper.db.CommitTran();

                        // 刷新列表显示
                        RefreshCraneLst();
                        EventBus.Instance.PublishConfigChange();

                        MessageBox.Show($"CSV导入成功！共导入{affectedRows}条记录。");
                    }
                    catch (Exception ex)
                    {
                        // 回滚事务
                        DbHelper.db.RollbackTran();
                        MessageBox.Show($"数据库操作失败：{ex.Message}");
                        LogHelper.Error($"[ImportCsv]{ex}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"CSV导入失败：{ex.Message}");
                    LogHelper.Error($"[ImportCsv]{ex}");
                }
            }
        }
        private void ExportCsv(object o)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".csv",
                Filter = "CSV Files (*.csv)|*.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var lines = new List<string>();

                    // 添加表头
                    if (CraneTypeEnable && CraneGroupEnable)
                    {
                        lines.Add("设备ID,设备名称,设备IP,机型ID,类别ID");
                    }
                    else if (CraneTypeEnable)
                    {
                        lines.Add("设备ID,设备名称,设备IP,机型ID");
                    }
                    else if (CraneGroupEnable)
                    {
                        lines.Add("设备ID,设备名称,设备IP,类别ID");
                    }
                    else
                    {
                        lines.Add("设备ID,设备名称,设备IP");
                    }

                    // 添加数据行
                    foreach (var crane in CraneModelLst)
                    {
                        string line = $"{crane.CraneID},{crane.CraneName},{crane.CraneIP}";
                        if (CraneTypeEnable)
                        {
                            line += $",{crane.CraneTypeID}";
                        }
                        if (CraneGroupEnable)
                        {
                            line += $",{crane.CraneGroupID}";
                        }
                        lines.Add(line);
                    }

                    System.IO.File.WriteAllLines(dialog.FileName, lines);
                    MessageBox.Show("CSV导出成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"CSV导出失败：{ex.Message}");
                    LogHelper.Error($"[ExportCsv]{ex}");
                }
            }
        }
        private void AddNewCrane(object o)
        {
            var selectedCrane = o as CraneModel;

            CraneEditPopup ce = new CraneEditPopup(selectedCrane, CraneTypeEnable, CraneGroupEnable, CraneConfigIPEnable);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                MainWindow.RemoveMask();
                RefreshCraneLst();
            }
            else
            {
                MainWindow.RemoveMask();
            }
        }
        private void QueryCrane(object o)
        {
            RefreshCraneLst();
        }
        private void DeleteCraneType(object o)
        {
            var selectedCraneType = o as CraneTypeModel;
            if (selectedCraneType == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该设备类型吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iCount = CraneModelLst.Count(x => x.CraneTypeID == selectedCraneType.CraneTypeID);
            if (iCount > 0)
            {
                MessageBox.Show("请先删除该类型下的所有设备，或将这些设备修改为其他类型，再进行删除！");
                return;
            }
            var iRet = DbHelper.db.Deleteable<DbModels.crane_type>().Where(x => x.crane_type_id == selectedCraneType.CraneTypeID).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                CraneTypeLst.Remove(selectedCraneType);
            }
        }
        private void EditCraneType(object o)
        {
            var selectedCraneType = o as CraneTypeModel;

            CraneTypeEditPopup ce = new CraneTypeEditPopup(selectedCraneType);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                MainWindow.RemoveMask();
                RefreshCraneTypeLst();
                RefreshCraneLst();

            }
            else
            {
                MainWindow.RemoveMask();
            }
        }
        private void DeleteCraneGroup(object o)
        {
            var selectedCraneGroup = o as CraneGroupModel;
            if (selectedCraneGroup == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该设备类别吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iCount = CraneModelLst.Count(x => x.CraneGroupID == selectedCraneGroup.CraneGroupID);
            if (iCount > 0)
            {
                MessageBox.Show("请先删除该类别下的所有设备，或将这些设备修改为其他类别，再进行删除！");
                return;
            }
            var iRet = DbHelper.db.Deleteable<DbModels.crane_group>().Where(x => x.group_id == selectedCraneGroup.CraneGroupID).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                CraneGroupLst.Remove(selectedCraneGroup);
            }
        }
        private void EditCraneGroup(object o)
        {
            var selectedCraneGroup = o as CraneGroupModel;

            CraneGroupEditPopup ce = new CraneGroupEditPopup(selectedCraneGroup);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                MainWindow.RemoveMask();
                RefreshCraneGroupLst();
                RefreshCraneLst();

            }
            else
            {
                MainWindow.RemoveMask();
            }
        }
        private void AddMultiCraneType(object o)
        {
            if (CraneTypeID==0 )
            {
                MessageBox.Show("类型ID无效！");
                return;
            }
            else if (string.IsNullOrEmpty(CraneTypeName))
            {
                MessageBox.Show("类型名称不能为空！");
                return;
            }
            if (CraneTypeSpreaderMatchEnable)
            {
                if(CraneSpreaderMatchScreenModelLst == null|| CraneSpreaderMatchScreenModelLst.Count == 0)
                {
                    MessageBox.Show("请添加不同吊具ID匹配的画面！");return;
                }
            }
            //判断是否重复
            var existedCount=DbHelper.db.Queryable<DbModels.crane_type>().Count(x=>x.crane_type_id==CraneTypeID||x.crane_type_name==CraneTypeName);
            if (existedCount > 0)
            {
                MessageBox.Show("已存在重复的设备类型！添加失败！"); return;
            }
            var newCraneType = new DbModels.crane_type();
            newCraneType.crane_type_id = CraneTypeID;
            newCraneType.crane_type_name = CraneTypeName;
            newCraneType.crane_type_desc = CraneTypeDesc;
            newCraneType.spreader_match_enable = CraneTypeSpreaderMatchEnable;
            if (CraneTypeSpreaderMatchEnable)
            {
                newCraneType.spreader_match_screen = JsonConvert.SerializeObject(CraneSpreaderMatchScreenModelLst);
            }
            var affectedRow = DbHelper.db.Insertable<DbModels.crane_type>(newCraneType).ExecuteCommand();
            if (affectedRow > 0)
            {
                MessageBox.Show("添加成功！"); 
                RefreshCraneTypeLst(); return;
            }
            else
            {
                MessageBox.Show("添加失败(数据库处理失败)！"); return;
            }
        }
        private void AddMultiCraneGroup(object o)
        {
            if (CraneGroupID == 0)
            {
                MessageBox.Show("类别ID无效！");
                return;
            }
            else if (string.IsNullOrEmpty(CraneGroupName))
            {
                MessageBox.Show("类别名称不能为空！");
                return;
            }
           
            //判断是否重复
            var existedCount = DbHelper.db.Queryable<DbModels.crane_group>().Count(x => x.group_id == CraneGroupID || x.group_name == CraneGroupName);
            if (existedCount > 0)
            {
                MessageBox.Show("已存在重复的设备类别！添加失败！"); return;
            }
            var newCraneGroup = new DbModels.crane_group();
            newCraneGroup.group_id = CraneGroupID;
            newCraneGroup.group_name = CraneGroupName;
          
            var affectedRow = DbHelper.db.Insertable<DbModels.crane_group>(newCraneGroup).ExecuteCommand();
            if (affectedRow > 0)
            {
                MessageBox.Show("添加成功！");
                RefreshCraneGroupLst(); return;
            }
            else
            {
                MessageBox.Show("添加失败(数据库处理失败)！"); return;
            }
        }
        private void RemoveCraneTypeScreen(object o)
        {
            if (CraneSpreaderMatchScreenModelLst == null) return;
            var matchScreenItem = CraneSpreaderMatchScreenModelLst.Where(x => x.spreader_id.ToString() == o.ToString()).FirstOrDefault();
            if (matchScreenItem == null) return;
            CraneSpreaderMatchScreenModelLst.Remove(matchScreenItem);
        }
        private void AddNewCraneSpreaderScreen(object o)
        {
            if (CraneSpreaderMatchScreenModelLst == null)
            {
                CraneSpreaderMatchScreenModelLst = new ObservableCollection<SpreaderMatchScreenModel>();
            }
            int maxSpreaderId = 0;
            if (CraneSpreaderMatchScreenModelLst.Count() > 0)
            {
                maxSpreaderId = CraneSpreaderMatchScreenModelLst.Max(x => x.spreader_id);
            }
            CraneSpreaderMatchScreenModelLst.Add(new SpreaderMatchScreenModel()
            {
                spreader_id = maxSpreaderId + 1,
                screen_info = new DbModels.dv_screen_conf(),

            }) ;
        }
        private void RefreshCraneTypeLst()
        {
            var curCraneTypeLst= DbHelper.db.Queryable<DbModels.crane_type>().ToList();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                CraneTypeLst = new ObservableCollection<CraneTypeModel>();
                foreach (var item in curCraneTypeLst)
                {
                    var craneType = new CraneTypeModel();
                    craneType.CraneTypeID = item.crane_type_id;
                    craneType.CraneTypeName = item.crane_type_name;
                    craneType.CraneTypeDesc = item.crane_type_desc;
                    craneType.SpreaderMatchEnable = item.spreader_match_enable;
                    craneType.SpreaderMatchScreenModel = new List<SpreaderMatchScreenModel>();
                    if (item.spreader_match_enable && !string.IsNullOrEmpty(item.spreader_match_screen))
                    {
                        var dbSpreaderMatchInfoLst = JsonConvert.DeserializeObject<List<DataView_Configuration.SpreaderMatchScreenModel>>(item.spreader_match_screen);
                        foreach (var newItem in dbSpreaderMatchInfoLst)
                        {
                            if (newItem.screen_info == null) continue;
                            var screenInfo = new SpreaderMatchScreenModel()
                            {
                                spreader_id = newItem.spreader_id,
                                screen_info = newItem.screen_info,
                            };
                            craneType.SpreaderMatchScreenModel.Add(screenInfo);
                        }
                    }
                    CraneTypeLst.Add(craneType);
                }
            }));
        }
        private void RefreshCraneGroupLst()
        {
            var curCraneGroupLst = DbHelper.db.Queryable<DbModels.crane_group>().ToList();
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                CraneGroupLst = new ObservableCollection<CraneGroupModel>();
                foreach (var item in curCraneGroupLst)
                {
                    var craneGroup = new CraneGroupModel();
                    craneGroup.CraneGroupID = item.group_id;
                    craneGroup.CraneGroupName = item.group_name;
                    CraneGroupLst.Add(craneGroup);
                }
            }));
        }
        private void RefreshCraneLst()
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (CraneTypeEnable&&CraneGroupEnable)
                {
                    var craneInfoLst = DbHelper.db.Queryable<DbModels.crane>()
                        .LeftJoin<DbModels.crane_type>((c,t) => c.crane_type_id == t.crane_type_id)
                        .LeftJoin<DbModels.crane_group>((c,t,g)=>c.group_id==g.group_id)
                        .Select((c,t,g) => new CraneModel
                        {
                            CraneID = c.crane_id,
                            CraneIP = c.crane_ip,
                            CraneName = c.crane_name,
                            CraneTypeID = t.crane_type_id,
                            CraneTypeName = t.crane_type_name,
                            CraneTypeDesc = t.crane_type_desc,
                            CraneGroupID=g.group_id,
                            CraneGroupName=g.group_name,
                        }).ToList();
                    CraneModelLst = new ObservableCollection<CraneModel>(craneInfoLst);
                }
                else if (CraneTypeEnable && !CraneGroupEnable)
                {
                    var craneInfoLst = DbHelper.db.Queryable<DbModels.crane>()
                       .LeftJoin<DbModels.crane_type>((c, t) => c.crane_type_id == t.crane_type_id)
                       .Select((c, t) => new CraneModel
                       {
                           CraneID = c.crane_id,
                           CraneIP = c.crane_ip,
                           CraneName = c.crane_name,
                           CraneTypeID = t.crane_type_id,
                           CraneTypeName = t.crane_type_name,
                           CraneTypeDesc = t.crane_type_desc
                       }).ToList();
                    CraneModelLst = new ObservableCollection<CraneModel>(craneInfoLst);
                }
                else if (!CraneTypeEnable && CraneGroupEnable)
                {
                    var craneInfoLst = DbHelper.db.Queryable<DbModels.crane>()
                       .LeftJoin<DbModels.crane_group>((c, g) => c.group_id == g.group_id)
                       .Select((c, g) => new CraneModel
                       {
                           CraneID = c.crane_id,
                           CraneIP = c.crane_ip,
                           CraneName = c.crane_name,
                           CraneGroupID = g.group_id,
                           CraneGroupName = g.group_name,
                       }).ToList();
                    CraneModelLst = new ObservableCollection<CraneModel>(craneInfoLst);
                }
                else
                {
                    var craneInfoLst = DbHelper.db.Queryable<DbModels.crane>()
                       .Select(c => new CraneModel
                       {
                           CraneID = c.crane_id,
                           CraneIP = c.crane_ip,
                           CraneName = c.crane_name,

                       }).ToList();
                    CraneModelLst = new ObservableCollection<CraneModel>(craneInfoLst);
                }
                CraneCount = CraneModelLst.Count;
            }));
        }
        private void OpenPage(object o)
        {
            //反射创建
            //Type type = Assembly.GetExecutingAssembly().GetType("DataViewConfig.Pages." + o.ToString());
            ////避免重复创建UIElement实例
            //if (!PageDict.ContainsKey(o.ToString()))
            //{
            //    PageDict.Add(o.ToString(), (UIElement)Activator.CreateInstance(type));
            //}
            //MainContent = PageDict[o.ToString()];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MainContent"));
        }
        private void EditCrane(object o)
        {
            var selectedCrane = o as CraneModel;
          
            CraneEditPopup ce = new CraneEditPopup(selectedCrane, CraneTypeEnable,CraneGroupEnable, CraneConfigIPEnable);
            MainWindow.AddMask();
            if (ce.ShowDialog() == true)
            {
                MainWindow.RemoveMask();
                RefreshCraneLst();
            }
            else
            {
                MainWindow.RemoveMask();
            }
        }
        private void DeleteCrane(object o)
        {
            var selectedCrane = o as CraneModel;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该设备吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.crane>().Where(x=>x.crane_id==selectedCrane.CraneID).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                // MessageBox.Show("删除成功！");
                CraneModelLst.Remove(selectedCrane);
                EventBus.Instance.PublishConfigChange();
            }
        }
        private void QuickCreateCrane(object o )
        {
            try
            {
                if (CraneCount == 0)
                {
                    MessageBox.Show("待生成的设备数量不能为空！"); return;
                }

                Regex rg = new Regex(@"^[a-zA-z0-9]+\{[0-9]+\}$");
                if (!rg.IsMatch(this.CranePrefixFormat))
                {
                    MessageBox.Show("设备名称前缀格式错误！");
                    return;
                }
                CraneLst = new ObservableCollection<DbModels.crane>();
                var regexResult = Regex.Match(this.CranePrefixFormat, @"\{(.*)\}", RegexOptions.IgnorePatternWhitespace);
                var formatStr = regexResult.Value;//结果中包含大括号
                var prefixStr = this.CranePrefixFormat.Remove(this.CranePrefixFormat.IndexOf("{"));
                if (string.IsNullOrEmpty(formatStr) || string.IsNullOrEmpty(prefixStr))
                {
                    MessageBox.Show("设备名称前缀格式错误！");
                    return;
                }
                if (!Utli.IsValidIPAddress(StartLocalPLCIP))
                {
                    MessageBox.Show("IP地址格式错误！");
                    return;
                }
                for (int i = 1; i < CraneCount + 1; i++)
                {
                    //rxg{00}
                    string currentIP = StartLocalPLCIP;
                    string[] ipParts = currentIP.Split('.');
                    int lastPart = int.Parse(ipParts[3]) + i - 1;
                    if (lastPart > 255)
                    {
                        lastPart = 255;
                    }
                    string newIP = $"{ipParts[0]}.{ipParts[1]}.{ipParts[2]}.{lastPart}";

                    CraneLst.Add(new DbModels.crane()
                    {
                        crane_id = i,
                        crane_name = prefixStr + (i).ToString().PadLeft(formatStr.Length - 2, '0'),
                        crane_ip = newIP,
                        group_id = 1,
                    });
                }
                var curExitsCraneLst = DbHelper.db.Queryable<DbModels.crane>().ToList();
                try
                {
                    DbHelper.db.BeginTran();//涉及到删除和添加，使用事务
                    int affectedRow;
                    if (curExitsCraneLst.Count > 0)
                    {
                        //先删再添加
                        var iRet = DbHelper.db.Deleteable<DbModels.crane>(curExitsCraneLst).ExecuteCommand();
                    }
                    affectedRow = DbHelper.db.Insertable<DbModels.crane>(new List<DbModels.crane>(CraneLst)).ExecuteCommand();
                    DbHelper.db.CommitTran();
                    //MessageBox.Show("设备生成成功！");
                    RefreshCraneLst();
                    EventBus.Instance.PublishConfigChange();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"设备生成失败！{ex.ToString()}");
                    DbHelper.db.Ado.RollbackTran();
                    this.CraneLst = new ObservableCollection<DbModels.crane>(curExitsCraneLst);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"设备生成失败！{ex.ToString()}");
                LogHelper.Error($"[QuickCreateCrane]{ex.ToString()}");
            }
        }
    }
    public class CraneModel
    {
        public int CraneID { get; set; }
        public string CraneName { get; set; }
        public string CraneIP { get; set; }
        public int CraneTypeID { get; set; }
        public string CraneTypeName { get; set; }
        public string CraneTypeDesc { get; set; }
        public int CraneGroupID { get; set; }
        public string CraneGroupName { get; set; }
      
    }
    public class CraneGroupModel
    {
        public int CraneGroupID { get; set; }
        public string CraneGroupName { get; set; }
    }
    public class CraneTypeModel
    {
        public int CraneTypeID { get; set; }
        public string CraneTypeName { get; set; }
        public string CraneTypeDesc { get; set; }
        public string ScreenId { get; set; }
        public bool SpreaderMatchEnable { get; set; }
        public List<DataView_Configuration.SpreaderMatchScreenModel> SpreaderMatchScreenModel { get; set;}
        //public List<CraneSpreaderMatchModel> SpreaderMatchScreenModelLst { get; set; }
    }
   
}
