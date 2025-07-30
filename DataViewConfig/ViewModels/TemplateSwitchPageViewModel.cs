using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using DataView_Configuration;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class TemplateSwitchPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 模板枚举
        /// </summary>
        public enum TemplateEnum
        {
            AUTO_RMG_MQ=1,
            AUTO_RMG_RCCS=2,
            AUTO_RMG_ROSCPU=3,
            SEMIAUTO_STS_BR=4,
            SEMIAUTO_STS_RCCS = 5,
            AUTO_STS_BR = 6,
            AUTO_STS_RCCS = 7,
        }
        //模板文件夹名称
        public const string Template_auto_rmg_mq= "/Screen/auto_rmg_ecsmq/";
        public const string Template_auto_rmg_rccs = "/Screen/auto_rmg_rccs/";
        public const string Template_auto_rmg_roscpu = "/Screen/auto_rmg_roscpu/";
        public const string Template_semiauto_sts_rccs = "/Screen/semiauto_sts_rccs/";
        public const string Template_semiauto_sts_br = "/Screen/semiauto_sts_br/";
        public const string Template_auto_sts_rccs = "/Screen/auto_sts_rccs/";
        public const string Template_auto_sts_br = "/Screen/auto_sts_br/";
        public const string Template_auto_rxg_gui = "/Screen/auto_rxg_gui/";

        public event PropertyChangedEventHandler  PropertyChanged;        
        public ObservableCollection<RequestInterfaceModel> RequestInterfaceLst { get; set; }       
        public int TemplateName { get; set; }       
        public TemplateEnum CurTemplate { get; set; }       
        public bool IsAutoDeleteOtherFile { get; set; }        
        public bool ProjectTypeSTS { get; set; }        
        public bool ProjectTypeRXG { get; set; }        
        public bool ProjectZhLanguage { get; set; }        
        public bool ProjectEnLanguage { get; set; }        
        public bool TemplateRxgMq { get; set; }        
        public bool TemplateRxgRccs { get; set; }  
        public bool TemplateRxgRoscpu { get; set; }      
        public bool TemplateSemiAutoStsRcsBr { get; set; }       
        public bool TemplateSemiAutoStsRccs { get; set; }
        public bool TemplateAutoStsRcsBr { get; set; }
        public bool TemplateAutoStsRccs { get; set; }
        public Command SaveCommand { get; set; }        

        public TemplateSwitchPageViewModel()
        {
            IsAutoDeleteOtherFile = false;
            var curProjectConf = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
            var CurProjectLanguage = curProjectConf.language.ToLower() ;
            if(curProjectConf.language.ToLower() == "zh-cn")
            {
                this.ProjectZhLanguage = true;
                this.ProjectEnLanguage = false;
            }
            else
            {
                this.ProjectZhLanguage = false;
                this.ProjectEnLanguage = true;
            }
            var CurProjectType = curProjectConf.project_type ;
            if (CurProjectType.ToLower() == "sts")
            {
                this.ProjectTypeSTS = true;
                this.ProjectTypeRXG = false;
            }
            else
            {
                this.ProjectTypeSTS = false;
                this.ProjectTypeRXG = true;
            }
            CurTemplate = Utli.ConvertToEnum<TemplateEnum>(curProjectConf.template_id.ToString());
            switch (CurTemplate)
            {
                case TemplateEnum.AUTO_RMG_MQ:
                    this.TemplateRxgMq = true;
                    break;
                case TemplateEnum.AUTO_RMG_RCCS:
                    this.TemplateRxgRccs = true;
                    break;
                case TemplateEnum.AUTO_RMG_ROSCPU:
                    this.TemplateRxgRoscpu = true;
                    break;
                case TemplateEnum.SEMIAUTO_STS_BR:
                    this.TemplateSemiAutoStsRcsBr = true;
                    break;
                case TemplateEnum.SEMIAUTO_STS_RCCS:
                    this.TemplateSemiAutoStsRccs = true;
                    break;
                case TemplateEnum.AUTO_STS_BR:
                    this.TemplateAutoStsRcsBr = true;
                    break;
                case TemplateEnum.AUTO_STS_RCCS:
                    this.TemplateAutoStsRccs = true;
                    break;
            }
            SaveCommand = new Command(SaveTemplateSelection);
        }
       
        private void SaveTemplateSelection(object o)
        {
            try
            {
                bool bRet = false;
                if (MessageBox.Show("是否确认选择该模板?", "确认", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
                if (TemplateRxgMq)
                {
                    CurTemplate = TemplateEnum.AUTO_RMG_MQ;
                }
                else if (TemplateRxgRccs)
                {
                    CurTemplate = TemplateEnum.AUTO_RMG_RCCS;
                }
                else if (TemplateRxgRoscpu)
                {
                    CurTemplate = TemplateEnum.AUTO_RMG_ROSCPU;
                }
                else if (TemplateSemiAutoStsRccs)
                {
                    CurTemplate = TemplateEnum.SEMIAUTO_STS_RCCS;
                }
                else if (TemplateSemiAutoStsRcsBr)
                {
                    CurTemplate = TemplateEnum.SEMIAUTO_STS_BR;
                }
                else if (TemplateAutoStsRccs)
                {
                    CurTemplate = TemplateEnum.AUTO_STS_RCCS;
                }
                else if (TemplateAutoStsRcsBr)
                {
                    CurTemplate = TemplateEnum.AUTO_STS_BR;
                }
                string path = "";
                //先移除Screen目录下除Login/Logout画面外的其他csw画面
                if (!RemoveCswFilesFromScreen())
                {
                    MessageBox.Show("删除Screen下已有的画面文件失败，请关闭SCADA Studio后再重试！"); return;
                }
                switch (CurTemplate)
                {
                    case TemplateEnum.AUTO_RMG_MQ:
                        path = "/Screen/auto_rmg_ecsmq/";
                        break;
                    case TemplateEnum.AUTO_RMG_RCCS:
                        path = "/Screen/auto_rmg_rccs/";
                        break;
                    case TemplateEnum.AUTO_RMG_ROSCPU:
                        path = "/Screen/auto_rmg_roscpu/";

                        break;
                    case TemplateEnum.SEMIAUTO_STS_RCCS:
                        path = "/Screen/semiauto_sts_rccs/";
                        break;
                    case TemplateEnum.SEMIAUTO_STS_BR:
                        path = "/Screen/semiauto_sts_br/";
                        break;
                    case TemplateEnum.AUTO_STS_RCCS:
                        path = "/Screen/auto_sts_rccs/";
                        break;
                    case TemplateEnum.AUTO_STS_BR:
                        path = "/Screen/auto_sts_br/";
                        break;
                }
                bRet = MoveCswFiles2Screen(path);
                if (IsAutoDeleteOtherFile)
                {
                    bRet = RemoveDirectoryInScreenDir();
                }
                //重启
                // System.Windows.Forms.Application.Restart();
                SwitchTemplate();
                if (bRet)
                {
                    MessageBox.Show("切换成功,请重新启动配置工具！");
                    // DataView_Configuration.ProjectConf.InitialConfig();
                    //ScreenHelper.OpenScreenByCswName(template_type + @"\Login.csw");
                    var curProjectConfg = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
                    curProjectConfg.template_id = (int)CurTemplate;
                    curProjectConfg.project_type = this.ProjectTypeSTS ? "sts" : "rxg";
                    curProjectConfg.language = this.ProjectZhLanguage ? "zh-cn" : "en-us";
                    DbHelper.db.Updateable<DbModels.dv_project_conf>(curProjectConfg)
                        .UpdateColumns(it => new { it.template_id, it.language, it.project_type })
                        .Where(it => it.id == 1).ExecuteCommand();
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 移除其他模板目录
        /// </summary>
        /// <returns></returns>
        private bool RemoveDirectoryInScreenDir()
        {
            try
            {
                var curPath = Environment.CurrentDirectory;
                //Directory.SetCurrentDirectory(Directory.GetParent(curPath).FullName);
                var path = Directory.GetParent(curPath).FullName;
                if (!Directory.Exists(path + "\\Screen"))
                {
                    MessageBox.Show("未找到Screen目录，请确认配置工具所在路径是否正确！"); return false;
                }
                if(Directory.Exists(path + Template_auto_rmg_mq))
                {
                    Directory.Delete(path + Template_auto_rmg_mq, true);
                }
                if (Directory.Exists(path + Template_auto_rmg_rccs))
                {
                    Directory.Delete(path + Template_auto_rmg_rccs, true);
                }
                if (Directory.Exists(path + Template_auto_rmg_roscpu))
                {
                    Directory.Delete(path + Template_auto_rmg_roscpu, true);
                }
                   
                if (Directory.Exists(path + Template_auto_sts_br))
                {
                    Directory.Delete(path + Template_auto_sts_br, true);
                }
                    
                if (Directory.Exists(path + Template_auto_sts_rccs))
                {
                    Directory.Delete(path + Template_auto_sts_rccs, true);
                }
                  
                if (Directory.Exists(path + Template_semiauto_sts_br))
                {
                    Directory.Delete(path + Template_semiauto_sts_br, true);
                }
                  
                if (Directory.Exists(path + Template_semiauto_sts_rccs))
                {
                    Directory.Delete(path + Template_semiauto_sts_rccs, true);
                }
                   
                //Directory.Delete(path + Template_semiauto_rxg_gui);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// remove file in screen directory
        /// </summary>
        /// <returns></returns>
        private bool RemoveCswFilesFromScreen()
        {
            try
            {
                var curPath = Environment.CurrentDirectory;
                //Directory.SetCurrentDirectory(Directory.GetParent(curPath).FullName);
                var path = Directory.GetParent(curPath).FullName;
                if (!Directory.Exists(path + "\\Screen"))
                {
                    MessageBox.Show("未找到Screen目录，请确认配置工具所在路径是否正确！"); return false;
                }
                var destFiltPath = path + "/Screen/";

                string[] files = Directory.GetFiles(destFiltPath, "*.csw"); // 获取源目录下所有以.csw为后缀的文件

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    if (fileName.Equals("Login.csw") || fileName.Equals("Logout.csw"))
                    {
                        continue;
                    }
                    File.Delete(file);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 移动csw文件到screen画面中去,如果有重复的，提示是否覆盖
        /// </summary>
        /// <param name="originFilePath"></param>
        private bool MoveCswFiles2Screen(string originFilePath)
        {
            var curPath = Environment.CurrentDirectory;
            //Directory.SetCurrentDirectory(Directory.GetParent(curPath).FullName);
            var path = Directory.GetParent(curPath).FullName;
            if (!Directory.Exists(path + "\\Screen"))
            {
                MessageBox.Show("未找到Screen目录，请确认配置工具所在路径是否正确！"); return false;
            }
            
            string sourceDirectory = path+originFilePath; // 指定源目录
            string targetDirectory = path + "\\Screen"; // 指定目标目录
            string[] files = Directory.GetFiles(sourceDirectory, "*.csw"); // 获取源目录下所有以.csw为后缀的文件

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string targetPath = Path.Combine(targetDirectory, fileName);
                File.Copy(file, targetPath, true);
                
            }
            return true;
        }
        private void SwitchTemplate()
        {

            //修改operate画面
            //var strOperateScr = @"OP_ContainerConfirm.csw,OP_ManualIntervention.csw,OP_CraneSetting.csw,OP_RCSSetting.csw,OP_TruckConfirm.csw,RT_RealAlarm.csw";
            //string[] arrayOperateStr = strOperateScr.Split(',');
            //string newOperateStr = "";
            ////修改等待画面internal name
            //var strWaitingScr = @"RT_Waiting_Crane.csw,Navi_BottomBar.csw,Navi_SideBar.csw,Navi_TopBar.csw,Navi_TaskInfoBar.csw";
            //string[] arrayWaitingStr = strWaitingScr.Split(',');
            //string newWaitingStr = "";

            //foreach (var item in arrayWaitingStr)
            //{
            //    newWaitingStr += template_type + @"\" + item + ",";
            //}
            //foreach (var item in arrayOperateStr)
            //{
            //    newOperateStr += template_type + @"\" + item + ",";
            //}
            //newOperateStr.Remove(newOperateStr.Length - 1, 1);
            //newWaitingStr.Remove(newWaitingStr.Length - 1, 1);

            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("login", template_type + @"\" + @"Login.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("over_view", template_type + @"\" + @"RT_Overview.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("wait_view", template_type + @"\" + @"RT_Waiting_Crane.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("alarm", template_type + @"\" + @"RT_RealAlarm.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("truck_confirm", template_type + @"\" + @"OP_TruckConfirm.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("manual_intervention", template_type + @"\" + @"OP_ManualIntervention.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("container_confirm", template_type + @"\" + @"OP_ContainerConfirm.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("ocr_truck", template_type + @"\" + @"OCR_Truck.csw");
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("waiting", newWaitingStr);
            //DataView_Configuration.ScreenConf.UpdateScreenCswNameByInternalName("operate", newOperateStr);
            //修改接口参数,控件id，必要的点名（后缀）
            switch (CurTemplate)
            {
                case  TemplateEnum.AUTO_RMG_MQ:
                    DvSysmtemConfig.UpdateSystemConfg("rocs", "mq", true);
                    DvSysmtemConfig.UpdateSystemConfg("rccs", "opc", false);
                    DvSysmtemConfig.UpdateSystemConfg("bms", "mq", true);
                    //DvLoginConfig.UpdateLoginConf(true, false, "rocs_login", "rocs_logout", "", "");
                    RequestConfig.UpdateRequestInterface("rocs_connect_crane", "1,4", "json");
                    ScreenConf.UpdateControlIdByInternalName("overview_block_control", 1006);
                    TagConfig.UpdateTagTypeByInternalName("block_id", 3);

                    break;
                case TemplateEnum.AUTO_RMG_RCCS:
                    DvSysmtemConfig.UpdateSystemConfg("rocs", "mq", false);
                    DvSysmtemConfig.UpdateSystemConfg("rccs", "opc", true);
                    DvSysmtemConfig.UpdateSystemConfg("bms", "mq", false);
                    //DvLoginConfig.UpdateLoginConf(true, false, "roscpu_login", "roscpu_logout", "", "");
                    //RequestConfig.UpdateRequestInterface("rocs_connect_crane", "4", "string");
                    ScreenConf.UpdateControlIdByInternalName("overview_block_control", 1002);
                    TagConfig.UpdateTagTypeByInternalName("block_id", 1);
                    break;
                case TemplateEnum.AUTO_RMG_ROSCPU:
                    DvSysmtemConfig.UpdateSystemConfg("rocs", "mq", false);
                    DvSysmtemConfig.UpdateSystemConfg("rccs", "opc", true);
                    DvSysmtemConfig.UpdateSystemConfg("bms", "opc", true);
                    //DvLoginConfig.UpdateLoginConf(true, false, "rccs_login", "rccs_logout", "", "");
                    ScreenConf.UpdateControlIdByInternalName("overview_block_control", 1073);
                    TagConfig.UpdateTagTypeByInternalName("block_id", 3);
                    break;
            }
        }

    }
}
