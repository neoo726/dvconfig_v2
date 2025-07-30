using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;
using CMSCore;
using DataView_Configuration;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class TemplateConfigWinViewModel : INotifyPropertyChanged
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
        public event PropertyChangedEventHandler  PropertyChanged;  
        public string RecentTemplate { get; set; }
        public bool AutoDeleteOtherTemplate { get; set; }
        public ObservableCollection<DataViewConfig.Template> TemplateLst { get; set; }   
        public bool RXGChecked { get; set; }
        public bool STSChecked { get; set; }

        public Command SaveCommand { get; set; }        

        public TemplateConfigWinViewModel()
        {
            RXGChecked = true; STSChecked = false;
            RecentTemplate = TemplateConfigHandler.templateConfig.RecentTemplate;
            AutoDeleteOtherTemplate = TemplateConfigHandler.templateConfig.TemplateSwitch.AutoDeleteOther;
            TemplateLst =new ObservableCollection<Template>(TemplateConfigHandler.templateConfig.TemplateList);

            SaveCommand = new Command(SaveTemplateConfig);
        }

        //保存模板选择
        private void SaveTemplateConfig(object o)
        {
            try
            {
                var selectedTemplate = TemplateLst.Where(x => x.IsSelected).FirstOrDefault();
                if (selectedTemplate == null) 
                {
                    MessageBox.Show("请选择模板方案！");return;
                }
                //先移除已有画面文件
                if (!RemoveCswFilesFromScreen()) return;
                //拷贝画面文件到Screen下
                MoveFiles(selectedTemplate.TemplateFilePath + @"screen\", @"\Screen\", "*.csw");
                MoveFiles(selectedTemplate.TemplateFilePath + @"screen\", @"\Screen\", "*.csc");
                //拷贝.da文件
                MoveFiles(selectedTemplate.TemplateFilePath + @"dataaccess\", @"\", "*.da");
                //拷贝opcua文件
                MoveFiles(selectedTemplate.TemplateFilePath + @"opcua\", @"\OPD\", "*.uap");
                //拷贝dataview.db文件
                MoveFiles(selectedTemplate.TemplateFilePath + @"configfile\", @"\Screen\Program\ConfigFiles\", "*.db");
                //拷贝data下的report控件相关的xml配置文件
                MoveFiles(selectedTemplate.TemplateFilePath + @"data\", @"\Data\", "*.xml");
                //判断是否需要删除TemplateFile
                if (AutoDeleteOtherTemplate)
                {
                    if (!RemoveDirectoryInScreenDir())
                    {
                        MessageBox.Show("删除TemplateFile文件夹失败，请手动删除！");
                    }
                }
                MessageBox.Show("保存模板选择成功！");
                TemplateConfigHandler.templateConfig.RecentTemplate = selectedTemplate.Id.ToString();
                TemplateConfigHandler.templateConfig.TemplateSwitch.AutoDeleteOther = this.AutoDeleteOtherTemplate;
                TemplateConfigHandler.UpdateTemplateConfig();

                var win = o as Window;
                StartWindow s = new StartWindow(false);
                win.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！错误信息："+ex.Message);
                LogHelper.Error("[SaveTemplateConfig]" + ex.ToString());
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
                if (!Directory.Exists(path + "\\TemplateFile")) return false;
                Directory.Delete(path + "\\TemplateFile",true);
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
                string[] cswFiles = Directory.GetFiles(destFiltPath, "*.csw"); // 获取源目录下所有以.csw为后缀的文件
                string[] cscFiles = Directory.GetFiles(destFiltPath, "*.csc"); // 获取源目录下所有以.csc为后缀的文件
                foreach (string file in cswFiles)
                {
                    string fileName = Path.GetFileName(file);
                    File.Delete(file);
                }
                foreach (string file in cscFiles)
                {
                    string fileName = Path.GetFileName(file);
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
        /// 移动文件到指定文件夹中,如果有重复的，提示是否覆盖
        /// </summary>
        /// <param name="originFilePath">工程根目录下的相对路径</param>
        private bool MoveFiles(string originFilePath,string targetFilePath,string fileFormatStr)
        {
            //获取绝对路径
            var absolutePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            if (!Directory.Exists(absolutePath + "\\Screen"))
            {
                MessageBox.Show("未找到Screen目录，请确认配置工具所在路径是否正确！"); return false;
            }
            
            string sourceDirectory = absolutePath + originFilePath; // 指定源目录
            string targetDirectory = absolutePath + targetFilePath; // 指定目标目录
            string[] files = Directory.GetFiles(sourceDirectory, fileFormatStr); // 获取源目录下所有指定后缀的文件
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string targetPath = Path.Combine(targetDirectory, fileName);
                //目录不存在时，自动创建目录
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }
                File.Copy(file, targetPath, true);
            }
            return true;
        }
    }
}
