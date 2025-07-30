using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Shapes;
using DataView_Configuration;
using DataViewConfig.Pages.Popups;
using Newtonsoft.Json;
using Path = System.IO.Path;

namespace DataViewConfig.ViewModels
{
    internal class ScreenCswSelectPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
       
        public string BayID { get; set; }
        public int GantryPosition { get; set; }
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public bool IsAddNew { get; set; }
        public ObservableCollection<FileNode> CswLst { get; set; }
        public ObservableCollection<string> SelectedCswNameLst { get; set; }
        private FileNode selectedNode;
        public FileNode SelectedNode
        {
            get
            {
                return selectedNode;
            }
            set
            {
                if (value == null||value.Children.Count>0)
                {
                    IsSelectValidNode = false;
                }
                else
                {
                    IsSelectValidNode = true;
                }
                selectedNode = value;
            }
        }
        private string selectedRightCswName;
        public string SelectedRightCswName
        {
            get
            {
                return selectedRightCswName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    IsSelectRightCswName = false;
                }
                else
                {
                    IsSelectRightCswName = true;
                }
                selectedRightCswName = value;
            }
        }
        /// <summary>
        /// 是否选中右侧csw画面名称
        /// </summary>
        public bool IsSelectRightCswName { get; set; }
        public bool IsSelectValidNode { get; set; }

        #region Commnad
        public Command CloseCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command SelectParamCommand { get; set; }
        public Command AddNewReturnValCommand { get; set; }
        public Command DeleteReturnValCommand { get; set; }
        /// <summary>
        /// 从左侧列表中选中
        /// </summary>
        public Command SelectCswNodeCommand { get; set; }
        /// <summary>
        /// 从右侧已选中列表中移除
        /// </summary>
        public Command RemoveCswRightNodeCommand { get; set; }

        #endregion
        private string path = "";
        public ScreenCswSelectPopupViewModel(string selectedCswNames = "")
        {
            var curPath = Environment.CurrentDirectory;
            var path = Directory.GetParent(curPath).FullName + "\\Screen";
            if (!Directory.Exists(path))
                return;
            // 使用DirectoryInfo类来获取Screen目录的信息
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            // 使用GetFiles方法递归地获取所有.csw后缀的文件
            FileInfo[] files = dirInfo.GetFiles("*.csw", SearchOption.AllDirectories);

            List<FileNode> fileNodes = new List<FileNode>();
            SearchFiles(path, ".csw", fileNodes);
            CswLst = new ObservableCollection<FileNode>(fileNodes);
            //已有选中的csw画面名称
            if (!string.IsNullOrEmpty(selectedCswNames))
            {
                SelectedCswNameLst = new ObservableCollection<string>(selectedCswNames.Split(','));
            }
            else
            {
                SelectedCswNameLst = new ObservableCollection<string>();
            }
            CloseCommand = new Command(ClosePopup);
            CancelCommand = new Command(ClosePopup);
            ConfirmCommand = new Command(Confirm);
            SelectCswNodeCommand = new Command(SelectCswNode);
            RemoveCswRightNodeCommand = new Command(RemoveCswRightNode);
        }
        private void SelectCswNode(object o)
        {
            //选中的仅仅是子文件夹名称
            if (SelectedNode.Children.Count > 0) return;
            var selectCswName = "";
            if (!string.IsNullOrEmpty(SelectedNode.ParentName))
            {
                selectCswName = SelectedNode.ParentName + "/" + SelectedNode.Name;
            }
            else
            {
                selectCswName = SelectedNode.Name;
            }
            SelectedCswNameLst.Add(selectCswName);
        }
        private void RemoveCswRightNode(object o)
        {
            SelectedCswNameLst.Remove(SelectedRightCswName);
        }
        private void Confirm(object o)
        {
            System.Windows.MessageBox.Show("保存成功！");
            var win = o as Window;
            win.DialogResult = true;
        }
        private void ClosePopup(object o)
        {
            var win = o as Window;
            win.DialogResult = false;
        }
        private void SearchFiles(string directoryPath, string extension, List<FileNode> fileNodes)
        {
            foreach (var subDirectory in Directory.GetDirectories(directoryPath))
            {
                // 创建子目录的FileNode
                FileNode folderNode = new FileNode
                {
                    Name = new DirectoryInfo(subDirectory).Name,
                    Children = new List<FileNode>(),
                    IsChecked = false
                };

                // 检查子目录中是否有.csw文件
                bool hasCswFiles = Directory.GetFiles(subDirectory, "*" + extension).Length > 0;

                if (hasCswFiles)
                {
                    fileNodes.Add(folderNode);
                    foreach (var file in Directory.GetFiles(subDirectory, "*" + extension))
                    {
                        FileNode fileNode = new FileNode
                        {
                            ParentName=folderNode.Name,
                            Name = Path.GetFileName(file),
                            Children = new List<FileNode>(),
                            IsChecked = false
                        };
                        folderNode.Children.Add(fileNode);
                    }
                }
            }

            // 检查当前目录下是否有.csw文件
            foreach (var file in Directory.GetFiles(directoryPath, "*" + extension))
            {
                FileNode fileNode = new FileNode
                {
                    Name = Path.GetFileName(file),
                    Children = new List<FileNode>(),
                    IsChecked = false
                };
                fileNodes.Add(fileNode);
            }
        }
}
    public class FileNode
    {
        public string ParentName { get; set; }
        public string Name { get; set; }
        public List<FileNode> Children { get; set; }
        public bool IsChecked { get; set; }
    }
}
