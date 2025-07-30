using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;
using System.Xml;
using CMSCore;
using DataView_Configuration;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class NaviConfigPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<RequestInterfaceModel> RequestInterfaceLst { get; set; }


        public int CraneCount { get; set; }
        public int RcsCount { get; set; }
        public int RcsNO { get; set; }
        public bool IsStsProject { get; set; } //sts项目
        public bool IsRxgProject { get; set; } //场桥项目
        public bool IsCombinedProject { get; set; } //混合项目
        public bool IsDataViewConfig { get; set; }
        public bool IsChineseLanguageChecked { get; set; } //语言选择-默认中文
        public ObservableCollection<ExSystemInfo> ExSystemInfoLst { get; set; }
        public Command EditCommand { get; set; }
        public Command SaveRcsNoCommand { get; set; }
        public Command SaveLanguageConfigCommand { get; set; }
        public Command SaveProjectTypeCommand { get; set; }
        public NaviConfigPageViewModel()
        {
            try
            {
                RefreshConfigBaseInfo();
                EditCommand = new Command(OpenConfigPage);
                SaveRcsNoCommand = new Command(WriteRcsNOToProjInixml);
                SaveLanguageConfigCommand = new Command(SaveLanguageConfig);
                SaveProjectTypeCommand = new Command(SaveProjectTypeConfig);
                EventBus.Instance.ConfigChangeEventHandler += Instance_ConfigChangeEventHandler;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载页面出错：" + ex.ToString());
                LogHelper.Error($"{ex.ToString()}");
            }
        }
        private void RefreshConfigBaseInfo()
        {
            try
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    IsDataViewConfig = GlobalConfig.IsDataViewConfig;
                    CraneCount = DbHelper.db.Queryable<DbModels.crane>().Count();
                    RcsCount = DbHelper.db.Queryable<DbModels.rcs>().Count();
                    var curProjectType = DbHelper.db.Queryable<DbModels.dv_project_conf>().First();
                    if (curProjectType.project_type.ToLower() == "sts")
                    {
                        this.IsStsProject = true;
                        this.IsRxgProject = false;
                        this.IsCombinedProject = false;
                    }
                    else if (curProjectType.project_type.ToLower() == "rxg") 
                    {
                        this.IsStsProject = false;
                        this.IsRxgProject = true;
                        this.IsCombinedProject = false;
                    }
                    //混合
                    else
                    {
                        this.IsStsProject = false;
                        this.IsRxgProject = false;
                        this.IsCombinedProject = true;
                    }
                    var exSystemInfo = DbHelper.db.Queryable<DbModels.dv_system>().ToList();
                    ExSystemInfoLst = new ObservableCollection<ExSystemInfo>();
                    var curLoginType = DvLoginConfig.LoginType;
                    foreach (var item in exSystemInfo)
                    {
                        switch (curLoginType)
                        {
                            case UmsTypeEnum.UMS_LOCAL:
                                if (item.system_name.ToLower() == "ums_dv" || item.system_name.ToLower() == "ums_ecs")
                                {
                                    continue;
                                }
                                break;
                            case UmsTypeEnum.UMS_DV:
                                if (item.system_name.ToLower() == "ums_ecs")
                                {
                                    continue;
                                }
                                break;
                            case UmsTypeEnum.UMS_ECS:
                                if (item.system_name.ToLower() == "ums_dv")
                                {
                                    continue;
                                }
                                break;
                        }
                        if (item.comm_enable)
                        {

                            MQCommunicationModel mqcommInfo = null;
                            if (item.comm_info != null)
                            {
                                mqcommInfo = JsonConvert.DeserializeObject<MQCommunicationModel>(item.comm_info);
                            }
                            var mqCommModel = item.comm_type.ToLower() == "opc" ? null : mqcommInfo;

                            ExSystemInfoLst.Add(new ExSystemInfo()
                            {
                                EcsName = item.system_name,
                                MQCommInfo = mqCommModel,
                                IsRabbitMQ = item.comm_type.ToLower() == "mq",
                                IsOPC = item.comm_type.ToLower() == "opc",
                                IsRest = item.comm_type.ToLower() == "rest",
                                IsRedis = item.comm_type.ToLower() == "redis",
                            });
                        }
                    }
                    if (GlobalConfig.IsDataViewConfig)
                    {
                        RcsNO = ReadRcsNOFromProjIniXml();
                    }
                    //语言
                    var curProjectConf = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
                    IsChineseLanguageChecked = curProjectConf.language.ToLower() != "en-us";
                }));
            }
            catch(Exception e)
            {
                MessageBox.Show($"加载配置信息失败！错误信息：{e.Message}");
            }
        }
        private void Instance_ConfigChangeEventHandler()
        {
            RefreshConfigBaseInfo();
        }

        /// <summary>
        /// 从projIni.xml中获取rcsno
        /// </summary>
        /// <returns></returns>
        private int ReadRcsNOFromProjIniXml()
        {
            try
            {
                if (!GlobalConfig.IsDataViewConfig) return 0;
                // Load the XML document
                XmlDocument doc = new XmlDocument();
                var curPath = Environment.CurrentDirectory;
                var path = Directory.GetParent(curPath).FullName + "\\Screen\\Program\\ConfigFiles\\";
                if (!Directory.Exists(path))
                    return 0;

                doc.Load(path + "ProjIni.xml");
                // Find the node with the attribute to modify
                XmlNode node = doc.SelectSingleNode("Config/RCSNO");

                // Modify the attribute value
                return Convert.ToInt16(node.InnerText);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                return 0;
            }
            
        }
        private void SaveProjectTypeConfig(object o)
        {
            try
            {
                var updateProjectConf = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
                var targetProjectType = "";
                if (this.IsStsProject)
                {
                    targetProjectType = "sts";
                }
                else if (this.IsRxgProject)
                {
                    targetProjectType = "rxg";
                }
                else
                {
                    targetProjectType = "combined";
                }
                updateProjectConf.project_type = targetProjectType;
                var updateResult = DbHelper.db.Updateable(updateProjectConf)
                    .Where(it => it.id == 1)
                    .ExecuteCommand();
                
                MessageBox.Show(updateResult > 0 ? 
                    (GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? "保存成功！" : "Save Success!") :
                    GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? "保存失败！更新数据库失败！！" : "Save Failed! Update database failed!");
                if (updateResult > 0)
                {
                    EventBus.Instance.Publish(EventBus.EventBusType.EditConfigPageOpen, "",false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? $"保存失败！异常信息：{ex.Message}" : $"Save failed！Error info：{ex.Message}");
            }
        }
        private void SaveLanguageConfig(object o)
        {
            try
            {
                var updateProjectConf = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
                updateProjectConf.language = IsChineseLanguageChecked ? "zh-cn" : "en-us";
                var updateResult = DbHelper.db.Updateable(updateProjectConf)
                    .Where(it => it.id == 1)
                    .ExecuteCommand();
                if (updateResult > 0)
                {
                    MessageBox.Show(GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? "保存成功！" : "Save Success!");
                    // 触发语言变更事件
                    //EventBus.Instance.LanguageChangePublish();
                }
                else
                {
                    MessageBox.Show(GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? "保存失败！更新数据库失败！！" : "Save Failed! Update database failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? $"保存失败！异常信息：{ex.Message}" : $"Save failed！Error info：{ex.Message}");
            }
        }
        /// <summary>
        /// 将rcsno写入PorojIni.xml
        /// </summary>
        /// <param name="rcsNo"></param>
        private void WriteRcsNOToProjInixml(object o)
        {
            try
            {
                if (!GlobalConfig.IsDataViewConfig) return;
                // Load the XML document
                XmlDocument doc = new XmlDocument();
                var curPath = Environment.CurrentDirectory;
                var path = Directory.GetParent(curPath).FullName + "\\Screen\\Program\\ConfigFiles\\";
                if (!Directory.Exists(path))
                    return;
                doc.Load(path + "ProjIni.xml");
                // Find the node with the attribute to modify
                XmlNode node = doc.SelectSingleNode("Config/RCSNO");

                node.InnerText = RcsNO.ToString();

                // Save the modified document
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = true;
                settings.OmitXmlDeclaration = true;

                using (XmlWriter writer = XmlWriter.Create(path + "ProjIni.xml", settings))
                {
                    doc.WriteTo(writer);
                }
                MessageBox.Show(GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? "保存成功！" : "Save Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(GlobalConfig.CurLanguage == GlobalLanguage.zhCN ? "保存失败！" : "Save Failed!" + ex.Message);
            }
        }
        private void OpenConfigPage(object o)
        {
            EventBus.Instance.Publish(EventBus.EventBusType.EditConfigPageOpen,o.ToString());
        }
    }
    public class ExSystemInfo
    {
        public string EcsName { get; set; }        
        public bool IsRabbitMQ { get; set; }
        public bool IsOPC { get; set; }
        public bool IsRest { get; set; }
        public bool IsRedis { get; set; }
        public MQCommunicationModel MQCommInfo { get; set; }
    }
}
