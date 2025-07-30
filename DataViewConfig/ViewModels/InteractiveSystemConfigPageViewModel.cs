using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using CMSCore;
using DataView_Configuration;
using DbModels;
using Newtonsoft.Json;
using static DataView_Configuration.RestCommunicationModel;
using MessageBox = System.Windows.Forms.MessageBox;
using static DataView_Configuration.RedisCommunicationModel;

namespace DataViewConfig.ViewModels
{
    internal class InteractiveSystemConfigPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        
        #region  command
        public Command AddNewRpcQUeueCommand { get; set; }
        public Command AddNewRpcCommand { get; set; }
        public Command EditRpcCommand { get; set; }
        public Command DeleteRpcCommand { get; set; }
        public Command AddNewFanoutCommand { get; set; }
        public Command EditFanoutCommand { get; set; }
        public Command DeleteFanoutCommand { get; set; }
        public Command AddNewControlDefaultCommand { get; set; }
        public Command EditControlDefaultCommand { get; set; }
        public Command DeleteControlDefaultCommand { get; set; }
        public Command AddNewRestUrlCommand { get; set; }
        public Command QuickAddRpcByCraneIDCommand { get; set; }
        public Command QuickAddRestUrlByCraneIDCommand { get; set; }
        public Command QuickAddRpcByBlockIDCommand { get; set; }
        public Command DeleteRpcQUeueCommand { get; set; }
        public Command CloseCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command SaveBaseInfoCommand { get; set; }

        public Command AddNewCommonReturnValCommand { get; set; }
        public Command EditCommonReturnValCommand { get; set; }
        public Command DeleteCommonReturnValCommand { get; set; }
        public Command ConfirmAddOrEditReturnValCommand { get; set; }
        
        //rpc queue 
        public Command ConfirmAddOrEditRpcQueueCommand { get; set; }
        public Command EditRpcQUeueCommand { get; set; }
        public Command QueryRpcCommand { get; set; }
        public Command QueryFanoutCommand { get; set; }
        public Command QueryDefaultValueCommand { get; set; } //填充默认值 列表查询
        
        public Command CopyRpcInterfaceCommand { get; set; }
        #endregion
        #region 属性
        //根据当前用户角色判断是否显示该元素
        public bool IsShowItem { get; set; }
        public string EcsName { get; set; }  
        public string EcsDesc { get; set; }
        public ECSCommType ECSComm { get; set; }
        public bool CommEnable { get; set; }
        public bool IsCommReturnVal { get; set; }
        public bool IsSpecialReturnVal { get; set; }
        public bool IsPermanent { get; set; }
        private MQRPCQueueTypeEnum mqRpcType;
        public MQRPCQueueTypeEnum MqRpcType
        {
            get => mqRpcType;
            set
            {
                mqRpcType = value;
                switch (value)
                {
                    case MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_CRANE_ID:
                        this.MqRpcTypeHeaderDesc = "设备ID";
                        break;
                    case MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_BLOCK_ID:
                        this.MqRpcTypeHeaderDesc = "堆场ID";
                        break;
                }
            }
        }
        public ObservableCollection<RequestInterfaceModel> RpcInterfaceLst { get; set; }
        public ObservableCollection<FanoutReceiveModel> ReceiveFanoutLst { get; set; }
        public ObservableCollection<RequestInterfaceModel> RequestInterfaceLst { get; set; }
        public ObservableCollection<DbModels.dv_request_return_code> CommonReturnValueLst { get; set; }
        public ObservableCollection<DbModels.dv_control_default_value> ControlDefaultValLst { get; set; }

        //public MQRPCQueueTypeEnum MqRpcType { get; set; }
        public string MqRpcTypeHeaderDesc { get; set; }
        public MQCommunicationModel MqCommModel { get; set; }
        public RestCommunicationModel RestCommModel { get; set; }
        public DataView_Configuration.RedisCommunicationModel RedisCommModel { get; set; }
        public ObservableCollection<MQRpcQueueModel> MqRpcQueueLst { get; set; }
        public ObservableCollection<RestUrlModel> RestUrlLst { get; set; }
        //返回值
        public int ReturnValueId { get; set; }
        public string ReturnValue { get; set; }
        public string ReturnValueDescZh { get; set; }
        public string ReturnValueDescEn { get; set; }
        public bool ReturnValueSuccessFlag { get; set; }
        public bool ShowCommonValEditOrAdd { get; set; }
        //mq rpc queue 
        public bool ShowRpcQueuesEditOrAdd { get; set; }
        public string MqRpcQueueKey { get; set; }
        public string MqRpcQueueName { get; set; }

        public bool AutoExpandedBaseInfo { get; set; }
        private int m_SystemId;
        private bool IsAddNew = false;
        public bool ShowRpcAndFanoutInfo { get; set; }
        #endregion
        public InteractiveSystemConfigPageViewModel(int systemId)
        {
           
            try
            {
                IsShowItem = GlobalConfig.CurUserLevel == UserLevelType.Administrator;
                if (systemId == 0)
                {
                    AutoExpandedBaseInfo = true;
                    IsAddNew = true;
                    this.IsCommReturnVal = true;
                    this.IsSpecialReturnVal = false;
                    this.CommEnable = true;
                    this.ECSComm = ECSCommType.MQ;
                    this.MqRpcType = MQRPCQueueTypeEnum.ONLY_ONE_RPC;
                    this.MqCommModel = new MQCommunicationModel();
                    this.MqCommModel.ip = "127.0.0.1";
                    this.MqCommModel.port = 5672;
                    this.MqCommModel.user = "admin";
                    this.MqCommModel.password = "123456";
                    this.MqCommModel.fanout_exchange = "ZPMC.EXCHANGE.XXXX2UI.STATUS.FANOUT";
                    this.MqCommModel.rpc_exchange = "ZPMC.EXCHANGE.UI2XXXX.DIRECT.RPC";
                    this.MqCommModel.rpc_queue = "ZPMC.QUEUE.UI2XXXX.RPC";
                    this.MqRpcQueueLst = new ObservableCollection<MQRpcQueueModel>();
                    this.RestCommModel = new RestCommunicationModel()
                    {
                        baseUrl="127.0.0.1:5001",
                        //redis_config = new RestCommunicationModel.RedisCommunicationModel()
                        //{
                        //    prefix_key = "UI:{CraneName}",
                        //    related_cid_macro = "CID",
                        //    polling_interval = "1000",
                        //    ip = "127.0.0.1",
                        //    port = "6379",
                        //    database_index = 0,
                        //}
                        header_field="",
                        header_encrypt=false,
                    };
                    this.RedisCommModel = new DataView_Configuration.RedisCommunicationModel()
                    {
                        prefix_key = "UI:{CraneName}",
                        related_cid_macro = "CID",
                        polling_interval = "1000",
                        ip = "127.0.0.1",
                        port = "6379",
                        database_index = 0,
                    };
                }
                else
                {
                    m_SystemId = systemId;
                    var tmpSystem = DataView_Configuration.DvSysmtemConfig.dvSystemModelLst
                        .Where(x => x.SystemId == systemId).First();
                    EcsSystemModel dvSystem = new EcsSystemModel();
                    dvSystem.SystemName = tmpSystem.SystemName;
                    dvSystem.SystemDesc = tmpSystem.SystemDesc;
                    dvSystem.CommEnable = tmpSystem.CommEnable;
                    dvSystem.EcsComm = tmpSystem.CommType;
                    dvSystem.IsPermanent = tmpSystem.IsPermanent;
                    
                    //判断是否为特定的登录信息
                    if(tmpSystem.SystemName.ToLower()=="ums_ecs"|| tmpSystem.SystemName.ToLower() == "ums_dv")
                    {
                        ShowRpcAndFanoutInfo = false;
                    }
                    else
                    {
                        ShowRpcAndFanoutInfo = true;
                    }
                    RefreshRpcLst(systemId);
                    RefreshFanoutLst(systemId);
                    RefreshCommonReturnVal(systemId);
                    RefreshControlDefaultValueLst(systemId);
                    this.IsCommReturnVal = !tmpSystem.IsSpecialReturnValue;
                    this.IsSpecialReturnVal = tmpSystem.IsSpecialReturnValue;
                    this.EcsName = dvSystem.SystemName;
                    this.ECSComm = dvSystem.EcsComm;
                    this.EcsDesc = dvSystem.SystemDesc;
                    this.IsPermanent = dvSystem.IsPermanent;
                    this.CommEnable = Convert.ToBoolean(dvSystem.CommEnable);
                    if (this.ECSComm== ECSCommType.MQ)
                    {
                        dvSystem.CommInfo = JsonConvert.SerializeObject(tmpSystem.MQInfo);
                        if (dvSystem.CommInfo != null)
                        {
                            this.MqCommModel = JsonConvert.DeserializeObject<MQCommunicationModel>(dvSystem.CommInfo);
                            if (this.MqCommModel != null)
                            {
                                MqRpcType = Utli.ConvertToEnum<MQRPCQueueTypeEnum>(this.MqCommModel.rpc_queue_type);
                                if (MqRpcType != MQRPCQueueTypeEnum.ONLY_ONE_RPC)
                                {
                                    this.MqRpcQueueLst = new ObservableCollection<MQRpcQueueModel>(this.MqCommModel.rpc_queue_list);
                                }
                            }
                            else
                            {
                                this.MqCommModel = new MQCommunicationModel();
                            }
                        }
                        else
                        {
                            this.MqCommModel = new MQCommunicationModel();
                        }
                    }
                    else if(this.ECSComm==ECSCommType.Rest)
                    {
                        dvSystem.CommInfo = JsonConvert.SerializeObject(tmpSystem.RestConfigInfo);
                        if (dvSystem.CommInfo != null)
                        {
                            this.RestCommModel = JsonConvert.DeserializeObject<RestCommunicationModel>(dvSystem.CommInfo);
                            if (this.RestCommModel != null)
                            {
                                //if (this.RestCommModel.redis_config == null)
                                //{
                                //    this.RestCommModel.redis_config = new RestCommunicationModel.RedisCommunicationModel();
                                //}
                                MqRpcType = Utli.ConvertToEnum<MQRPCQueueTypeEnum>(this.RestCommModel.rest_server_deployment_type);
                                if (MqRpcType != MQRPCQueueTypeEnum.ONLY_ONE_RPC)
                                {
                                    this.RestUrlLst = new ObservableCollection<RestUrlModel>(this.RestCommModel.rest_url_list);
                                }
                                
                            }
                            else
                            {
                                this.RestCommModel = new RestCommunicationModel()
                                {
                                    baseUrl = "127.0.0.1:5001",
                                };
                            }
                        } 
                        else 
                        {
                            this.RestCommModel = new RestCommunicationModel();
                        }
                    }
                    else if (this.ECSComm == ECSCommType.Redis)
                    {
                        dvSystem.CommInfo = JsonConvert.SerializeObject(tmpSystem.RedisConfigInfo);
                        if (tmpSystem.RedisConfigInfo != null)
                        {
                            this.RedisCommModel = JsonConvert.DeserializeObject<DataView_Configuration.RedisCommunicationModel>(dvSystem.CommInfo);
                        }
                        else
                        {
                            this.RedisCommModel = new DataView_Configuration.RedisCommunicationModel()
                            {
                                prefix_key = "UI:{CraneName}",
                                related_cid_macro = "CID",
                                polling_interval = "1000",
                                ip = "127.0.0.1",
                                port = "6379",
                                database_index = 0,
                            };
                        }
                    }
                }
               
                CloseCommand = new Command(CloseWin);
                ConfirmCommand = new Command(ConfirmSelection);
                CancelCommand = new Command(CancelSelection);
                AddNewRpcQUeueCommand = new Command(AddNewRpcQueue);
               
                QuickAddRpcByCraneIDCommand = new Command(AddNewRpcQueuesByCraneId);
                QuickAddRpcByBlockIDCommand = new Command(AddNewRpcQueuesByBlockId);
                //rest
                AddNewRestUrlCommand = new Command(AddNewRestUrl);
                QuickAddRestUrlByCraneIDCommand = new Command(AddNewRestUrlsByCraneId);
                DeleteRpcQUeueCommand = new Command(DeleteRpcQueue);
                SaveBaseInfoCommand = new Command(SaveBaseInfo);
                AddNewRpcCommand = new Command(AddNewRpcRequest);
                EditRpcCommand = new Command(EditRpcRequest);
                DeleteRpcCommand = new Command(DeleteRpcRequest);

                AddNewFanoutCommand = new Command(AddNewFanoutRequest);
                EditFanoutCommand = new Command(EditFanoutRequest);
                DeleteFanoutCommand = new Command(DeleteFanoutRequest);

                AddNewControlDefaultCommand = new Command(AddNewControlDefaultValue);
                EditControlDefaultCommand = new Command(EditControlDefaultValue);
                DeleteControlDefaultCommand = new Command(DeleteControlDefaultValue);

                AddNewCommonReturnValCommand = new Command(AddNewCommonReturnVal);
                EditCommonReturnValCommand = new Command(EditCommonReturnVal);
                DeleteCommonReturnValCommand = new Command(DeleteCommonReturnVal);
                ConfirmAddOrEditReturnValCommand = new Command(ConfirmAddOrEditReturnVal);

                ConfirmAddOrEditRpcQueueCommand = new Command(ConfirmAddOrEditRpcQueue);
                EditRpcQUeueCommand = new Command(EditRpcQueue);
                //查询
                QueryRpcCommand = new Command(new Action<object>((object o) =>
                {
                    RefreshRpcLst(systemId,o.ToString());
                }));
                QueryFanoutCommand = new Command(new Action<object>((object o) =>
                {
                    RefreshFanoutLst(systemId, o.ToString());
                }));
                QueryDefaultValueCommand = new Command(new Action<object>((object o) =>
                {
                    RefreshControlDefaultValueLst(systemId, o.ToString());
                }));


                CopyRpcInterfaceCommand = new Command(CopyRpcInterface);
                EventBus.Instance.ExSystemEnableChangeEventHandler += Instance_ExSystemEnableChangeEventHandler;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载页面出错：" + ex.ToString());
                LogHelper.Error($"{ex.ToString()}");
            }
        }
        /// <summary>
        /// 复制选中的rpc接口
        /// </summary>
        /// <param name="o"></param>
        private void CopyRpcInterface(object o)
        {
            var grid = o as System.Windows.Controls.DataGrid;
            if (grid == null) return;
            RequestInterfaceModel selectedRpc = grid.CurrentItem as RequestInterfaceModel;
            if (selectedRpc != null)
            {
                var dbRpcDef = DbHelper.db.Queryable<DbModels.dv_request_interface>().Where(x => x.request_internal_name == selectedRpc.RequestInternalName).First();
                if (dbRpcDef != null)
                {
                    dbRpcDef.id = 0;
                    dbRpcDef.request_internal_name += "_copy";
                    int i = 1;
                    while (DbHelper.db.Queryable<DbModels.dv_request_interface>().Count(x => x.request_internal_name == dbRpcDef.request_internal_name) > 0)
                    {
                        dbRpcDef.request_internal_name += i++;
                    }
                    var affectedRow = DbHelper.db.Insertable<DbModels.dv_request_interface>(dbRpcDef).ExecuteCommand();
                    if (affectedRow > 0)
                    {
                        RefreshRpcLst();
                    }
                }
            }
        }
        /// <summary>
        /// 监听交互对象enable变化
        /// </summary>
        /// <param name="exSystemName"></param>
        /// <param name="CommEnable"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Instance_ExSystemEnableChangeEventHandler(string exSystemName, bool CommEnable)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (exSystemName == this.EcsName)
                {
                    this.CommEnable = CommEnable;
                }
            }));
        }

        private bool isAddCommonReturnVal = false;
        private void AddNewCommonReturnVal(object o)
        {
            if (ShowCommonValEditOrAdd)
            {
                ShowCommonValEditOrAdd = false;return;
            }
            var t = o as DbModels.dv_request_return_code;
            this.ReturnValue = string.Empty;
            this.ReturnValueDescZh = string.Empty;
            this.ReturnValueDescEn = string.Empty;
            this.ReturnValueSuccessFlag =false;
            ReturnValueId = 0;
            ShowCommonValEditOrAdd = true;
            isAddCommonReturnVal = true;
        }
        private void EditCommonReturnVal(object o)
        {
            var t = o as DbModels.dv_request_return_code;

            this.ReturnValue = t.return_value;
            this.ReturnValueDescZh = t.return_desc_zh;
            this.ReturnValueDescEn = t.return_desc_en;
            this.ReturnValueSuccessFlag = t.is_success;
            this.ReturnValueId = t.id;
            ShowCommonValEditOrAdd = true;
            isAddCommonReturnVal = false;
        }
        private void DeleteCommonReturnVal(object o)
        {
            var selectedCrane = o as DbModels.dv_request_return_code;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该返回值吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_request_return_code>()
                .Where(x => x.id == selectedCrane.id&&x.system_id==m_SystemId).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                CommonReturnValueLst.Remove(selectedCrane);
            }
        }
        /// <summary>
        /// 添加新接口
        /// </summary>
        /// <param name="o"></param>
        private void ConfirmAddOrEditReturnVal(object o)
        {
            if (string.IsNullOrEmpty(this.ReturnValue))
            {
                MessageBox.Show("返回值不能为空！"); return;
            }
            if (isAddCommonReturnVal)
            {
                if (this.CommonReturnValueLst == null) this.CommonReturnValueLst = new ObservableCollection<DbModels.dv_request_return_code>();
                if (ReturnValueId == 0 && new List<DbModels.dv_request_return_code>(this.CommonReturnValueLst).Exists(x => x.return_value == this.ReturnValue))
                {
                    MessageBox.Show("返回值不能重复！"); return;
                }
            }
            DbModels.dv_request_return_code c = new DbModels.dv_request_return_code()
            {
                return_value = this.ReturnValue,
                return_desc_en = this.ReturnValueDescEn,
                return_desc_zh = this.ReturnValueDescZh,

                system_id = this.m_SystemId,
                is_success = this.ReturnValueSuccessFlag,
            };
            int affectedRow = 0;
            //new
            if (ReturnValueId == 0)
            {
                affectedRow = DbHelper.db.Insertable<DbModels.dv_request_return_code>(c).ExecuteCommand();
            }
            else
            {
                c.id = ReturnValueId;
                affectedRow = DbHelper.db.Updateable<DbModels.dv_request_return_code>(c).ExecuteCommand();
            }
            if (affectedRow != 0)
            {
                RefreshCommonReturnValLst();
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
        private void RefreshCommonReturnValLst(string searchTxt = null)
        {
            var curReturnValLst = DbHelper.db.Queryable<DbModels.dv_request_return_code>()
                .Where(x=>x.system_id==m_SystemId).ToList();
            CommonReturnValueLst = new ObservableCollection<DbModels.dv_request_return_code>();
            foreach (var item in curReturnValLst)
            {
                //模糊搜索，输入返回值数值/描述
                if (string.IsNullOrEmpty(searchTxt))
                {
                    CommonReturnValueLst.Add(item);
                }
                else
                {
                    if (Utli.StringContains(item.return_value, searchTxt)
                         || Utli.StringContains(item.return_desc_zh, searchTxt)
                         || Utli.StringContains(item.return_desc_en, searchTxt))
                    {
                        CommonReturnValueLst.Add(item);
                    }
                }
            }
        }
        private void RefreshControlDefaultValueLst(int systemId = 0,string searchTxt = null)
        {
            var curControlDefaultVals = DbHelper.db.Queryable<DbModels.dv_control_default_value>()
                .Where(x => x.system_id == systemId)
                .ToList();

            ControlDefaultValLst = new ObservableCollection<DbModels.dv_control_default_value>();
            foreach (var item in curControlDefaultVals)
            {

                //模糊搜索，控件名称
                if (string.IsNullOrEmpty(searchTxt))
                {
                    ControlDefaultValLst.Add(item);
                }
                else
                {
                    if (Utli.StringContains(item.control_internal_name, searchTxt))
                    {
                        ControlDefaultValLst.Add(item);
                    }
                }
            }
        }
        private void RefreshCommonReturnVal(int systemId = 0)
        {
            if (systemId == 0) return;
            var curDbReturnValLst = DbHelper.db.Queryable<DbModels.dv_request_return_code>()
               .Where(x => x.system_id == systemId)
               .ToList();
            CommonReturnValueLst = new ObservableCollection<DbModels.dv_request_return_code>(curDbReturnValLst);
        }
        private void RefreshRpcLst(int systemId=0,string searchTxt = null)
        {
            if (systemId == 0) return;
            RpcInterfaceLst = new ObservableCollection<RequestInterfaceModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_request_interface>()
                .Where(x=>x.system_id==systemId)
                .ToList();
            int i = 1;
            if (curRequests.Count > 0)
            {
                var firstRequest = curRequests[0];
                //通用返回值
                IsCommReturnVal = !firstRequest.special_return_value;
                IsSpecialReturnVal = firstRequest.special_return_value;
                
                foreach (var item in curRequests)
                {
                    RequestInterfaceModel rf = new RequestInterfaceModel();
                    rf.Id += i;
                    rf.RequestId = item.id;
                    rf.ParamIdLst = item.request_param_list;
                    rf.RequestDesc = item.request_desc;
                    rf.RequestInternalName = item.request_internal_name;
                    //rf.DestSystemName = (RequestSystemEnum)Enum.Parse(typeof(RequestSystemEnum), item.dest_system_name, true);
                    rf.SystemId = item.system_id;
                    if (item.param_format.ToLower() == "json")
                    {
                        rf.EcsComm = ECSCommType.MQ;
                    }
                    else
                    {
                        rf.ParamSeparator = item.param_separtor;
                        rf.EcsComm = ECSCommType.OPC;
                        rf.DestTagName = JsonConvert.DeserializeObject<RequestDestTagModel>(item.dest_tag_name);
                    }
                    if (!string.IsNullOrEmpty(item.return_value))
                    {
                        rf.ReturnValueLst = JsonConvert.DeserializeObject<List<RequestSpecialReturnValueModel>>(item.return_value);
                    }
                    rf.MsgType = item.msg_type;
                    rf.PreCondition = (RequestPreConditionType)Enum.Parse(typeof(RequestPreConditionType), item.precondition_id.ToString(), true);
                    rf.IsSuccessShowTips = item.success_tips_show;
                    rf.IsFailedShowTips = item.failed_tips_show;
                    rf.IsSpecialReturnValue = item.special_return_value;
                    //模糊搜索，接口名称，描述，参数列表，msg_type
                    if (string.IsNullOrEmpty(searchTxt))
                    {
                        RpcInterfaceLst.Add(rf);
                    }
                    else
                    {
                        if (Utli.StringContains(rf.RequestInternalName, searchTxt)
                             || Utli.StringContains(rf.RequestDesc, searchTxt)
                             || Utli.StringContains(rf.MsgType, searchTxt)
                             || Utli.StringContains(rf.ParamIdLst, searchTxt)
                             )
                        {
                            RpcInterfaceLst.Add(rf);
                        }
                    }
                    i++;
                }
            }
            else
            {
                IsCommReturnVal = true;
                IsSpecialReturnVal = false;
            }
        }
        private void RefreshFanoutLst(int systemId=0,string searchTxt = null)
        {
            if (systemId == 0) return;
            ReceiveFanoutLst = new ObservableCollection<FanoutReceiveModel>();
            var curRequests = DbHelper.db.Queryable<DbModels.dv_receive_fanout>()
                 .Where(x => x.system_id == systemId)
                .ToList();
            int i = 1;
            foreach (var item in curRequests)
            {
                FanoutReceiveModel rf = new FanoutReceiveModel();
                rf.Id = item.id;
                rf.MsgType = item.msg_type;
                rf.FanoutName = item.fanout_name;
                //rf.SystemName = (RequestSystemEnum)Enum.Parse(typeof(RequestSystemEnum), item.system_name, true);
                rf.SystemId = item.system_id;
                rf.DeviceField = item.device_field;
                rf.ReceiveType = (DvReceiveTypeEnum)Enum.Parse(typeof(DvReceiveTypeEnum), item.receive_type_id.ToString(), true);
                rf.ReceiveWriteType = (DvReceiveWriteTypeEnum)Enum.Parse(typeof(DvReceiveWriteTypeEnum), item.write_type_id.ToString(), true); ;
                //rf.ReceiveStoreType = (DvReceiveStoreTypeEnum)Enum.Parse(typeof(DvReceiveStoreTypeEnum), item.store_type_id.ToString(), true);
                rf.CacheWriteCondition = JsonConvert.DeserializeObject<ReceiveCacheWriteConditionModel>(item.cache_write_condition);
                rf.CacheTagInternalName = item.cache_tag_internal_name;
                //rf.FullStoreTagInternalName = item.full_store_tag_internal_name;
                rf.FanoutDesc = item.dv_fanout_desc;
                rf.RedisKey =!string.IsNullOrEmpty(item.metadata) ? JsonConvert.DeserializeObject<ReceiveFanoutMetadataModel>(item.metadata).redis_postfix_key :null;
                //模糊搜索，接口msg_type/描述
                if (string.IsNullOrEmpty(searchTxt))
                {
                    ReceiveFanoutLst.Add(rf);
                }
                else
                {
                    if (Utli.StringContains(rf.MsgType, searchTxt)
                         || Utli.StringContains(rf.FanoutDesc, searchTxt)
                        )
                    {
                        ReceiveFanoutLst.Add(rf);
                    }
                }
                i++;
            }
        }
        private void DeleteRpcQueue(object o)
        {
            var rpcQueue = o as MQRpcQueueModel;
            if (MqRpcQueueLst == null || MqRpcQueueLst.Count == 0)
            {
                return;
            }
            this.MqRpcQueueLst.Remove(rpcQueue);
        }
        private void AddNewRpcQueue(object o)
        {
            if (this.ShowRpcQueuesEditOrAdd)
            {
                this.ShowRpcQueuesEditOrAdd = false;return;
            }
            ShowRpcQueuesEditOrAdd = true;
          
            this.MqRpcQueueKey = "1";
            this.MqRpcQueueName = "new_queue_name";
        }
        private void EditRpcQueue(object o)
        {
            ShowRpcQueuesEditOrAdd = true;
            var rpcQueue = o as MQRpcQueueModel;
            this.MqRpcQueueKey = rpcQueue.key;
            this.MqRpcQueueName = rpcQueue.queue_name;
        }
        private void ConfirmAddOrEditRpcQueue(object o)
        {
            if (MqRpcQueueLst == null || MqRpcQueueLst.Count == 0)
            {
                this.MqRpcQueueLst = new ObservableCollection<MQRpcQueueModel>();
            }
            this.MqRpcQueueLst.Add(new MQRpcQueueModel()
            {
                key = MqRpcQueueKey,
                queue_name = MqRpcQueueName,
            }); 
            this.ShowRpcQueuesEditOrAdd = false;
        }
        private void AddNewRpcQueuesByCraneId(object o)
        {
            var craneLst = DbHelper.db.Queryable<DbModels.crane>().ToList();
            if (MqRpcQueueLst == null || MqRpcQueueLst.Count == 0)
            {
                this.MqRpcQueueLst = new ObservableCollection<MQRpcQueueModel>();
            }
            else
            {
                this.MqRpcQueueLst.Clear();
            }
            foreach (var item in craneLst)
            {
                this.MqRpcQueueLst.Add(new MQRpcQueueModel()
                {
                    key = item.crane_id.ToString(),
                    queue_name = $"ZPMC.QUEUE.UI2{EcsName.ToUpper()}.RPC.{item.crane_name}",
                });
            }            
        }
        private void AddNewRpcQueuesByBlockId(object o)
        {
            var blockLst = DbHelper.db.Queryable<DbModels.block>().ToList();
            if (MqRpcQueueLst == null || MqRpcQueueLst.Count == 0)
            {
                this.MqRpcQueueLst = new ObservableCollection<MQRpcQueueModel>();
            }
            else
            {
                this.MqRpcQueueLst.Clear();
            }
            foreach (var item in blockLst)
            {
                this.MqRpcQueueLst.Add(new MQRpcQueueModel()
                {
                    key = item.block_plc_id.ToString(),
                    queue_name = $"ZPMC.QUEUE.UI2{EcsName.ToUpper()}.RPC.{item.block_name}",
                });
            }
        }
        private void AddNewRestUrl(object o)
        {
            try
            {
                if (RestUrlLst == null || RestUrlLst.Count == 0)
                {
                    this.RestUrlLst = new ObservableCollection<RestUrlModel>();
                }
                this.RestUrlLst.Add(new RestUrlModel()
                {
                    key = (RestUrlLst.Max(x => x.key) + 1).ToString(),
                    RestServerUrl = $"127.0.0.1:9000",
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
            }
        }
        private void AddNewRestUrlsByCraneId(object o)
        {
            var craneLst = DbHelper.db.Queryable<DbModels.crane>().ToList();
            if (RestUrlLst == null || RestUrlLst.Count == 0)
            {
                this.RestUrlLst = new ObservableCollection<RestUrlModel>();
            }
            else
            {
                this.RestUrlLst.Clear();
            }
            foreach (var item in craneLst)
            {
                this.RestUrlLst.Add(new  RestUrlModel()
                {
                    key = item.crane_id.ToString(),
                    RestServerUrl = $"http://127.0.0.1:9000/",
                });
            }
        }
        private void CloseWin(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
        private void ConfirmSelection(object o)
        {
            if (string.IsNullOrEmpty(this.EcsName))
            {
                System.Windows.MessageBox.Show("请输入对象名称！");
                return;
            }
            if (string.IsNullOrEmpty(this.EcsDesc))
            {
                System.Windows.MessageBox.Show("请输入对象描述！");
                return;
            }
            var newDvSystem = new DbModels.dv_system();
            newDvSystem.system_name = this.EcsName;
            newDvSystem.system_desc = this.EcsDesc;
            newDvSystem.comm_enable = this.CommEnable;
            newDvSystem.comm_type = this.ECSComm.ToString();
            if(this.ECSComm== ECSCommType.MQ)
            {
                this.MqCommModel.rpc_queue_type = this.MqRpcType.ToString();
                if (this.MqRpcType!= MQRPCQueueTypeEnum.ONLY_ONE_RPC)
                {
                    if(this.MqRpcQueueLst==null|| this.MqRpcQueueLst.Count == 0)
                    {
                        MessageBox.Show("根据已选择的RPC队列类型，需要配置RPC队列列表！");
                        return;
                    }
                    else
                    {
                        this.MqCommModel.rpc_queue_list = new List<MQRpcQueueModel>(this.MqRpcQueueLst);
                    }
                }
                newDvSystem.comm_info = JsonConvert.SerializeObject(this.MqCommModel);
            }
            int iRet;
            if (IsAddNew)
            {
                var tmpResult = DbHelper.db.Queryable<DbModels.dv_system>().Where(x => x.system_name == newDvSystem.system_name).ToList();
                if (tmpResult != null && tmpResult.Count > 0)
                {
                    MessageBox.Show("该对象已经存在，请勿重复配置！");
                    return;
                }
                iRet = DbHelper.db.Insertable<DbModels.dv_system>(newDvSystem).ExecuteCommand();
            }
            else
            {
                iRet = DbHelper.db.Updateable<DbModels.dv_system>(newDvSystem).
                    Where(x=>x.system_id==this.m_SystemId).ExecuteCommand();
            }
            
            if (iRet == 1)
            {
                System.Windows.MessageBox.Show("保存成功！");
                var win = o as Window;
                win.DialogResult = true;
            }
            else
            {
                System.Windows.MessageBox.Show("保存失败！");
            }
        }
        private void CancelSelection(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
        private void SaveBaseInfo(object o)
        {
            try
            {
                if (string.IsNullOrEmpty(this.EcsName))
                {
                    System.Windows.MessageBox.Show("请输入对象名称！");
                    return;
                }
                if (string.IsNullOrEmpty(this.EcsDesc))
                {
                    System.Windows.MessageBox.Show("请输入对象描述！");
                    return;
                }
                var newDvSystem = new DbModels.dv_system();
                newDvSystem.system_name = this.EcsName;
                newDvSystem.system_desc = this.EcsDesc;
                newDvSystem.comm_enable = this.CommEnable;
                newDvSystem.comm_type = this.ECSComm.ToString();
                newDvSystem.is_special_return_value = IsSpecialReturnVal;
                if (this.ECSComm == ECSCommType.MQ)
                {
                    this.MqCommModel.rpc_queue_type = this.MqRpcType.ToString();
                    if (this.MqRpcType != MQRPCQueueTypeEnum.ONLY_ONE_RPC)
                    {
                        if (this.MqRpcQueueLst == null || this.MqRpcQueueLst.Count == 0)
                        {
                            MessageBox.Show("根据已选择的RPC队列类型，需要配置RPC队列列表！");
                            return;
                        }
                        else
                        {
                            this.MqCommModel.rpc_queue_list = new List<MQRpcQueueModel>(this.MqRpcQueueLst);
                        }
                    }
                    newDvSystem.comm_info = JsonConvert.SerializeObject(this.MqCommModel);
                }
                else if (ECSComm == ECSCommType.Rest)
                {
                    if (this.RestCommModel == null) this.RestCommModel = new RestCommunicationModel();
                    this.RestCommModel.rest_server_deployment_type = this.MqRpcType.ToString();
                    ////添加redis
                    //if (this.RestCommModel.redis_config == null)
                    //{
                    //    this.RestCommModel.redis_config = new RestCommunicationModel.RedisCommunicationModel();
                    //}
                    if (this.MqRpcType != MQRPCQueueTypeEnum.ONLY_ONE_RPC)
                    {
                        if (this.RestUrlLst == null || this.RestUrlLst.Count == 0)
                        {
                            MessageBox.Show("根据已选择的Rest server部署类型，需要配置Rest Server地址列表！");
                            return;
                        }
                        else
                        {
                            this.RestCommModel.rest_url_list = new List<RestUrlModel>(this.RestUrlLst);
                        }
                    }
                    else
                    {
                        //this.RestCommModel.baseUrl=
                    }
                    newDvSystem.comm_info = JsonConvert.SerializeObject(this.RestCommModel);
                }
                else if (ECSComm == ECSCommType.Redis)
                {
                    if (this.RedisCommModel == null) this.RedisCommModel = new DataView_Configuration.RedisCommunicationModel();
                    newDvSystem.comm_info = JsonConvert.SerializeObject(this.RedisCommModel);
                }
                int iRet;
                if (IsAddNew)
                {
                    var tmpResult = DbHelper.db.Queryable<DbModels.dv_system>().Where(x => x.system_name == newDvSystem.system_name).ToList();
                    if (tmpResult != null && tmpResult.Count > 0)
                    {
                        MessageBox.Show("该对象已经存在，请勿重复配置！");
                        return;
                    }
                    iRet = DbHelper.db.Insertable<DbModels.dv_system>(newDvSystem).ExecuteReturnIdentity();
                    if (iRet > 0)
                    {
                        m_SystemId = iRet;
                    }
                }
                else
                {
                    iRet = DbHelper.db.Updateable<DbModels.dv_system>(newDvSystem).
                        Where(x => x.system_id == this.m_SystemId).ExecuteCommand();
                    var rpcLst = DbHelper.db.Queryable<DbModels.dv_request_interface>().Where(x => x.system_id == this.m_SystemId).ToList();
                    if (rpcLst != null && rpcLst.Count > 0)
                    {
                        foreach (var rpc in rpcLst)
                        {
                            rpc.special_return_value = IsSpecialReturnVal;
                        }
                        iRet = DbHelper.db.Updateable<DbModels.dv_request_interface>(rpcLst).ExecuteCommand();
                    }
                }
                if (iRet > 0)
                {
                    //iRet = DbHelper.db.Updateable<DbModels.dv_request_interface>().UpdateColumns(it=>new {it.special_return_value})
                    //    .Where(x=>x.system_id==this.m_SystemId).ExecuteCommand();
                    System.Windows.MessageBox.Show("保存成功！");
                    DataView_Configuration.DvSysmtemConfig.ReloadSystemConfig();
                    EventBus.Instance.Publish(EventBus.EventBusType.ExSystemChanged, this.EcsName, this.CommEnable);
                    EventBus.Instance.PublishConfigChange();
                }
                else
                {
                    System.Windows.MessageBox.Show("保存失败！数据库更新失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败，异常信息：{ex.Message}");
            }
        }
        private void AddNewRpcRequest(object o)
        {
            if (m_SystemId == 0)
            {
                MessageBox.Show("请先保存交互对象的基础信息，再配置接口！"); return;
            }
            Pages.Popups.NewRpcEditPopup e = new Pages.Popups.NewRpcEditPopup(m_SystemId,null, this.ECSComm);
            MainWindow.AddMask();
            if (e.ShowDialog() == true)
            {
                RefreshRpcLst(this.m_SystemId);
            }
            MainWindow.RemoveMask();
        }
        private void EditRpcRequest(object o)
        {
            var curRpcRequest = o as RequestInterfaceModel;
            Pages.Popups.NewRpcEditPopup e = new Pages.Popups.NewRpcEditPopup(m_SystemId,curRpcRequest, this.ECSComm);
            MainWindow.AddMask();
            if (e.ShowDialog() == true)
            {
                RefreshRpcLst(this.m_SystemId);
            }
            MainWindow.RemoveMask();
        }
        private void DeleteRpcRequest(object o)
        {
            try
            {
                MainWindow.AddMask();
                var curRpcRequest = o as RequestInterfaceModel;

                var selectedCrane = o as RequestInterfaceModel;
                if (selectedCrane == null)
                {
                    MessageBox.Show("程序内部错误!选中内容无法识别！");
                    return;
                }
                if (MessageBox.Show("确认要删除该RPC接口吗？", "提示",  MessageBoxButtons.YesNo) ==  DialogResult.No) return;
                var iRet = DbHelper.db.Deleteable<DbModels.dv_request_interface>()
                    .Where(x => x.request_internal_name == selectedCrane.RequestInternalName&&x.system_id==this.m_SystemId).ExecuteCommand();
                if (iRet == 0)
                {
                    MessageBox.Show("删除失败！");
                }
                else
                {
                    MessageBox.Show("删除成功！");
                    RefreshRpcLst(this.m_SystemId);
                    //RequestInterfaceLst.Remove(selectedCrane);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                MainWindow.RemoveMask();
            }
        }
        private void AddNewFanoutRequest(object o)
        {
            if (m_SystemId == 0)
            {
                MessageBox.Show("请先保存交互对象的基础信息，再配置接口！");return;
            }
            Pages.Popups.NewFanoutEditPopup e = new Pages.Popups.NewFanoutEditPopup(null,ECSComm, m_SystemId);
            MainWindow.AddMask();
            if (e.ShowDialog() == true)
            {
                RefreshFanoutLst(m_SystemId);
            }
            MainWindow.RemoveMask();
        }
        private void EditFanoutRequest(object o)
        {
            var curFanout = o as FanoutReceiveModel;
            Pages.Popups.NewFanoutEditPopup e = new Pages.Popups.NewFanoutEditPopup(curFanout,ECSComm,m_SystemId);
            MainWindow.AddMask();
            if (e.ShowDialog() == true)
            {
                RefreshFanoutLst(m_SystemId);
            }
            MainWindow.RemoveMask();
        }

        private void DeleteFanoutRequest(object o)
        {
            var selectedCrane = o as FanoutReceiveModel;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该Fanout接口吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_receive_fanout>()
                .Where(x => x.id == selectedCrane.Id && x.system_id == this.m_SystemId).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                ReceiveFanoutLst.Remove(selectedCrane);
                RefreshFanoutLst(this.m_SystemId);
            }
        }

        private void AddNewControlDefaultValue(object o)
        {
            if (m_SystemId == 0)
            {
                MessageBox.Show("请先保存交互对象的基础信息，再配置相关控件默认值！"); return;
            }
            Pages.Popups.NewControlDefaultValEditPopup e = new Pages.Popups.NewControlDefaultValEditPopup(null, m_SystemId);
            MainWindow.AddMask();
            if (e.ShowDialog() == true)
            {
                RefreshControlDefaultValueLst(m_SystemId);
            }
            MainWindow.RemoveMask();
        }
        private void EditControlDefaultValue(object o)
        {
            var curFanout = o as DbModels.dv_control_default_value;
            Pages.Popups.NewControlDefaultValEditPopup e = new Pages.Popups.NewControlDefaultValEditPopup(curFanout, m_SystemId);
            MainWindow.AddMask();
            if (e.ShowDialog() == true)
            {
                RefreshControlDefaultValueLst(m_SystemId);
            }
            MainWindow.RemoveMask();
        }
        private void DeleteControlDefaultValue(object o)
        {
            var selectedCrane = o as DbModels.dv_control_default_value;
            if (selectedCrane == null)
            {
                MessageBox.Show("程序内部错误!选中内容无法识别！");
                return;
            }
            if (MessageBox.Show("确认要删除该配置吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            var iRet = DbHelper.db.Deleteable<DbModels.dv_control_default_value>()
                .Where(x => x.id == selectedCrane.id && x.system_id == this.m_SystemId).ExecuteCommand();
            if (iRet == 0)
            {
                MessageBox.Show("删除失败！");
            }
            else
            {
                MessageBox.Show("删除成功！");
                ControlDefaultValLst.Remove(selectedCrane);
            }
        }
    }
    
}
