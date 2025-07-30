using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using CMSCore;
using DataView_Configuration;
using Newtonsoft.Json;

namespace DataViewConfig.ViewModels
{
    internal class LoginConfigViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
       
        #region command
        public Command ApplyCommand { get; set; }

        public Command ImportCsvCommand { get; set; }  // Add this
        public Command ExportCsvCommand { get; set; }  // Add this
        public Command DeleteLocalUserCommand { get; set; }
        public Command AddNewLocalUserCommand { get; set; }
        public Command ConfirmLocalUserCommand { get; set; }
        public Command EditLocalUserCommand { get; set; }
        public Command AddExSystemInfoCommand { get; set; }
        public Command RemoveCommand { get; set; }
        public Command UserSystemSelectedChangeCommand { get; set; }
        #endregion

        public  UmsTypeEnum UmsLoginType { get; set; }
        public ObservableCollection<string> RequestInternalNameLst { get; set; }
        public ObservableCollection<DbModels.dv_system> ExSystemLst { get; set; }
        public ObservableCollection<DbModels.dv_request_interface> ExSystemRequestLst { get; set; }
        public ObservableCollection<DbModels.dv_local_user> LocalUserLst { get; set; }
        public ObservableCollection<LoginExSystemInfo> LoginExSystemInfoLst { get; set; }
        public string EcsLoginInternalName { get; set; }
        public string EcsLogoutInternalName { get; set; }
        public string PlcLoginInternalName { get; set; }
        public string PlcLogoutInternalName { get; set; }
        public bool EcsLoginEnable { get; set; }
        public bool PlcLoginEnable { get; set; }
        public bool IsNeedUserAuthentication { get; set; }
        public int EditId { get; set; }
        public string EditUserID { get; set; }
        public string EditUserName { get; set; }
        public string EditUserPwd { get; set; }
        public bool ShowLocalUserEdit { get; set; }
        public bool LoginInformRocsEnable { get; set; }
        public bool LoginInformRemotePLCEnable { get; set; }
        public bool LocalLoginEnable { get; set; }
        public bool UmsDvLoginEnable { get; set; }
        public bool UmsEcsLoginEnable { get; set; }
        public bool UmsOtherLoginEnable { get; set; }
        private bool IsAddNewLocalUser;
        public LoginConfigViewModel()
        {

            ImportCsvCommand = new Command(ImportCsv);
            ExportCsvCommand = new Command(ExportCsv);
            try
            {
                var curLoginConf = DbHelper.db.Queryable<DbModels.dv_login_conf>().ToList()[0];
                this.UmsLoginType = (UmsTypeEnum)Enum.Parse(typeof(UmsTypeEnum), curLoginConf.ums_type, true);
                if (this.UmsLoginType == UmsTypeEnum.UMS_LOCAL)
                {
                    this.LocalLoginEnable = true;
                    this.UmsDvLoginEnable = false;
                    this.UmsEcsLoginEnable = false;
                    this.UmsOtherLoginEnable = false;
                }
                else if (this.UmsLoginType == UmsTypeEnum.UMS_DV)
                {
                    this.LocalLoginEnable = false;
                    this.UmsDvLoginEnable = true;
                    this.UmsEcsLoginEnable = false;
                    this.UmsOtherLoginEnable = false;
                }
                else if (this.UmsLoginType == UmsTypeEnum.UMS_ECS)
                {
                    this.LocalLoginEnable = false;
                    this.UmsDvLoginEnable = false;
                    this.UmsEcsLoginEnable = true;
                    this.UmsOtherLoginEnable = false;
                }
                else if(this.UmsLoginType== UmsTypeEnum.UMS_OTHERR)
                {
                    this.LocalLoginEnable = false;
                    this.UmsDvLoginEnable = false;
                    this.UmsEcsLoginEnable = false;
                    this.UmsOtherLoginEnable = true;
                }
                LoginInformRocsEnable = curLoginConf.ecs_login_enable == 1;
                LoginInformRemotePLCEnable = curLoginConf.plc_login_enable == 1;
                //this.EcsLoginEnable = curLoginConf.ecs_login_enable==1;
                //this.EcsLoginInternalName = curLoginConf.ecs_login_interface_name;
                //this.EcsLogoutInternalName = curLoginConf.ecs_logout_internal_name;

                //this.PlcLoginEnable = curLoginConf.plc_login_enable == 1;
                //this.PlcLoginInternalName = curLoginConf.plc_login_interface_name;
                //this.PlcLogoutInternalName = curLoginConf.plc_logout_interface_name;
                this.IsNeedUserAuthentication = curLoginConf.is_need_user_authentication;
                //LoginExSystemInfoLst = new ObservableCollection<LoginExSystemInfo>();
                //if (curLoginConf.inform_ex_system_info!= null)
                //{
                //    var curExSystemInfoLst = JsonConvert.DeserializeObject<List<DataView_Configuration.LoginExSystemInfoModel>>(curLoginConf.inform_ex_system_info);
                //    foreach (var item in curExSystemInfoLst)
                //    {
                //        LoginExSystemInfoLst.Add(new LoginExSystemInfo()
                //        {
                //            Enable = item.enable,
                //            ExSystemId = item.system_id,
                //            ExSystemName = item.system_name,
                //            LoginRequestInternalName = item.login_request_internal_name,
                //            LogoutRequestInternalName=item.logout_request_internal_name,
                //        });
                //    }
                //}

                //var curSystemLst = DbHelper.db.Queryable<DbModels.dv_system>().ToList();
                //ExSystemLst = new ObservableCollection<DbModels.dv_system>(curSystemLst);
                //var curRequestLst = DbHelper.db.Queryable<DbModels.dv_request_interface>()
                //     .Where(x => x.request_internal_name.Contains("_login") || x.request_internal_name.Contains("_logout")).ToList();
                //ExSystemRequestLst = new ObservableCollection<DbModels.dv_request_interface>(curRequestLst);

                ////request internal name 

                //RequestInternalNameLst = new ObservableCollection<string>(curRequestLst.Select(x => x.request_internal_name).ToList());


                var curLocaluserLst = DbHelper.db.Queryable<DbModels.dv_local_user>()
                    .ToList();
                LocalUserLst = new ObservableCollection<DbModels.dv_local_user>(curLocaluserLst);

                ApplyCommand = new Command(SaveInfo);
                DeleteLocalUserCommand = new Command(DeleteLocalUser);
                AddNewLocalUserCommand = new Command(AddNewLocalUser);
                AddExSystemInfoCommand = new Command(AddExSystemInfo);
                RemoveCommand = new Command(RemoveExSystemInfo);
                UserSystemSelectedChangeCommand = new Command((o) =>
                {
                    if (LocalLoginEnable)
                    {
                        UmsLoginType = UmsTypeEnum.UMS_LOCAL;
                    }
                    else if (UmsDvLoginEnable)
                    {
                        UmsLoginType = UmsTypeEnum.UMS_DV;
                    }
                    else if (UmsEcsLoginEnable)
                    {
                        UmsLoginType = UmsTypeEnum.UMS_ECS;
                    }
                    else if (UmsOtherLoginEnable)
                    {
                        UmsLoginType = UmsTypeEnum.UMS_OTHERR;
                    }
                    //选项变化时，通知主页左侧菜单-拓展功能-常驻交互对象变化
                    EventBus.Instance.LoginTypePublish(UmsLoginType);
                });
                ConfirmLocalUserCommand = new Command(ConfirmLocalUserEditOrAdd);
                EditLocalUserCommand = new Command(EditLocalUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载页面出错：" + ex.ToString());
                LogHelper.Error($"{ex.ToString()}");
            }
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
                    var newUsers = new List<DbModels.dv_local_user>();

                    // Skip header row
                    foreach (var line in lines.Skip(1))
                    {
                        var values = line.Split(',');
                        if (values.Length >= 2)  // 只需要用户ID和密码
                        {
                            var user = new DbModels.dv_local_user
                            {
                                user_id = values[0],
                                user_password = values[1],
                                user_name = values[0]  // 用户名默认与ID相同
                            };

                            // 检查新导入的数据中是否有重复
                            if (newUsers.Any(x => x.user_id == user.user_id))
                            {
                                MessageBox.Show($"CSV文件中存在重复的用户ID: {user.user_id}");
                                return;
                            }
                            newUsers.Add(user);
                        }
                    }

                    // 清空数据库中的现有用户并插入新用户
                    try
                    {
                        DbHelper.db.BeginTran();
                        DbHelper.db.DbMaintenance.TruncateTable<DbModels.dv_local_user>();
                        var affectedRows = DbHelper.db.Insertable(newUsers).ExecuteCommand();
                        DbHelper.db.CommitTran();
                        // 更新界面显示
                        LocalUserLst.Clear();
                        foreach (var user in newUsers)
                        {
                            LocalUserLst.Add(user);
                        }

                        MessageBox.Show($"CSV导入成功！共导入{affectedRows}条记录。");
                    }
                    catch (Exception ex)
                    {
                        DbHelper.db.RollbackTran();
                        MessageBox.Show($"数据库操作失败：{ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"CSV导入失败：{ex.Message}");
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
                    var lines = new List<string>
            {
                "用户ID,密码" // 只导出用户ID和密码
            };

                    foreach (var user in LocalUserLst)
                    {
                        lines.Add($"{user.user_id},{user.user_password}");
                    }

                    System.IO.File.WriteAllLines(dialog.FileName, lines);
                    MessageBox.Show("CSV导出成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"CSV导出失败：{ex.Message}");
                }
            }
        }
        private void AddNewLocalUser(object o)
        {
            
            IsAddNewLocalUser = true;
            this.EditUserID = string.Empty;
            //this.EditUserName = string.Empty;
            this.EditUserPwd = string.Empty;
            ShowLocalUserEdit = !ShowLocalUserEdit;
            //var selectedUser = o as DbModels.dv_local_user;
            //this.LocalUserLst.Add(new DbModels.dv_local_user()
            //{
            //    user_id = "1001",
            //    user_name = "1001",
            //    user_password = "1001",
            //});
        }
        private void EditLocalUser(object o)
        {
            IsAddNewLocalUser = false;
            ShowLocalUserEdit = !ShowLocalUserEdit;
            var selectedUser = o as DbModels.dv_local_user;
            this.EditUserID = selectedUser.user_id;
            this.EditUserName = selectedUser.user_name;
            this.EditUserPwd = selectedUser.user_password;
            this.EditId = selectedUser.id;
        }
        private void DeleteLocalUser(object o)
        {
            var selectedUser = o as DbModels.dv_local_user;
            this.LocalUserLst.Remove(selectedUser);
        }
        private void ConfirmLocalUserEditOrAdd(object o)
        {
            if (string.IsNullOrEmpty(EditUserID) || string.IsNullOrEmpty(EditUserPwd))
            {
                MessageBox.Show("用户信息不能为空！");return;
            }
            //新增
            if (IsAddNewLocalUser)
            {
                if (new List<DbModels.dv_local_user>(this.LocalUserLst).Exists(x => x.user_id == EditUserID ))
                {
                    MessageBox.Show("用户ID或用户名重复！"); return;
                }
                this.LocalUserLst.Add(new DbModels.dv_local_user()
                {
                    user_id = EditUserID,
                    //user_name = EditUserName,
                    user_password = EditUserPwd,
                });
            }
            //编辑
            else
            {
                if (new List<DbModels.dv_local_user>(this.LocalUserLst).Exists(x =>x.id!=EditId &&(x.user_id == EditUserID )))
                {
                    MessageBox.Show("用户ID或用户名重复！"); return;
                }
                var editItem = this.LocalUserLst.Where(x => x.id == EditId).First();
                var iIdnex = this.LocalUserLst.IndexOf(editItem);
                this.LocalUserLst[iIdnex].user_id = EditUserID;
                this.LocalUserLst[iIdnex].user_password = EditUserPwd;
                var tmpLst = this.LocalUserLst;
                this.LocalUserLst = null;
                this.LocalUserLst = tmpLst;
            }
            ShowLocalUserEdit = false;
            IsAddNewLocalUser = false;
        }
        private void RemoveExSystemInfo(object o)
        {
            var exSystemInfo = o as LoginExSystemInfo;
            this.LoginExSystemInfoLst.Remove(exSystemInfo);
        }
        private void AddExSystemInfo(object o)
        {
            LoginExSystemInfoLst.Add(new LoginExSystemInfo()
            {
                 Enable=true,
            });
        }
       
        private void SaveInfo(object o)
        {
            //check local user
            if (this.UmsLoginType == UmsTypeEnum.UMS_LOCAL)
            {
                if (this.LocalUserLst.GroupBy(x => x.user_id).Where(g => g.Count() > 1).Count() > 0)
                {
                    MessageBox.Show("本地用户列表中有UserID重复，请删除重复项后重新提交！");
                    return;
                }
            }
            var newLoginConf = new DbModels.dv_login_conf();
            newLoginConf.id = 1;
            newLoginConf.ums_type = this.UmsLoginType.ToString();
            newLoginConf.is_need_user_authentication = this.IsNeedUserAuthentication;
            
            newLoginConf.ecs_login_enable = this.LoginInformRocsEnable ? 1 : 0;
           
            newLoginConf.plc_login_enable = this.LoginInformRemotePLCEnable ? 1 : 0;
           
            var affectedRow =DbHelper.db.Updateable<DbModels.dv_login_conf>(newLoginConf).
                Where(x=>x.id==1).ExecuteCommand();
            if (affectedRow != 0)
            {
                //本地用户，覆盖用户列表
                if(this.UmsLoginType== UmsTypeEnum.UMS_LOCAL)
                {
                    var curLst = new List<DbModels.dv_local_user>(LocalUserLst);
                    if (curLst.Count > 0)
                    {
                        DbHelper.db.DbMaintenance.TruncateTable<DbModels.dv_local_user>();
                        var affectedUserRow = DbHelper.db.Insertable<DbModels.dv_local_user>(curLst).ExecuteCommand();
                        if (affectedUserRow > 0)
                        {
                            MessageBox.Show("保存配置成功！");
                        }
                        else
                        {
                            MessageBox.Show("保存本地用户失败！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请添加本地用户！");
                    }
                }
                else
                {
                    MessageBox.Show("保存配置成功！");
                }
                //选项变化时，通知主页左侧菜单-拓展功能-常驻交互对象变化
                EventBus.Instance.LoginTypePublish(UmsLoginType);
            }
            else
            {
                MessageBox.Show("保存配置失败！");
            }
        }
        
    }
    public class LoginExSystemInfo
    {
        public bool Enable { get; set; }
        public int  ExSystemId { get; set; }
        public int ExSystemName { get; set; }
        public string LoginRequestInternalName { get; set; }
        public string LogoutRequestInternalName { get; set; }
    }
}
