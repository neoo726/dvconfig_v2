using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DataView_Configuration;
using Newtonsoft.Json;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DataViewConfig.ViewModels
{
    internal class EcsSystemEditPopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        #region  command
        public Command AddNewRpcQUeueCommand { get; set; }
        public Command QuickAddRpcByCraneIDCommand { get; set; }
        public Command QuickAddRpcByBlockIDCommand { get; set; }
        public Command DeleteRpcQUeueCommand { get; set; }
        public Command CloseCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        public Command CancelCommand { get; set; }
        #endregion
        #region 属性
        private string ecsName;
        public string EcsName
        {
            get => ecsName;
            set { ecsName = value; OnPropertyChanged("EcsName"); }
        }
        private string ecsDesc;
        public string EcsDesc
        {
            get => ecsDesc;
            set { ecsDesc = value; OnPropertyChanged("EcsDesc"); }
        }
        private ECSCommType ecsCommType;
        public ECSCommType ECSComm
        {
            get => ecsCommType;
            set { ecsCommType = value; OnPropertyChanged("ECSComm"); }
        }
        private bool commEnable;
        public bool CommEnable
        {
            get => commEnable;
            set { commEnable = value; OnPropertyChanged("CommEnable"); }
        }
        private MQRPCQueueTypeEnum mqRpcType;
        public MQRPCQueueTypeEnum MqRpcType
        {
            get => mqRpcType;
            set { mqRpcType = value; OnPropertyChanged("MqRpcType");
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
        private string mqRpcTypeHeaderDesc;
        public string MqRpcTypeHeaderDesc
        {
            get => mqRpcTypeHeaderDesc;
            set { mqRpcTypeHeaderDesc = value; OnPropertyChanged("MqRpcTypeHeaderDesc"); }
        }
        private MQCommunicationModel mqCommModel;
        public MQCommunicationModel MqCommModel
        {
            get => mqCommModel;
            set { mqCommModel = value; OnPropertyChanged("MqCommModel"); }
        }
        private ObservableCollection<MQRpcQueueModel> mqRpcQueueLst;
        public ObservableCollection<MQRpcQueueModel> MqRpcQueueLst
        {
            get => mqRpcQueueLst;
            set { mqRpcQueueLst = value; OnPropertyChanged("MqRpcQueueLst"); }
        }
        private int systemId;
        private bool IsAddNew = false;
        #endregion
        public EcsSystemEditPopupViewModel(EcsSystemModel dvSystem)
        {
            CloseCommand = new Command(CloseWin);
            ConfirmCommand = new Command(ConfirmSelection);
            CancelCommand = new Command(CancelSelection);
            AddNewRpcQUeueCommand = new Command(AddNewRpcQueue);
            QuickAddRpcByCraneIDCommand = new Command(AddNewRpcQueuesByCraneId);
            QuickAddRpcByBlockIDCommand = new Command(AddNewRpcQueuesByBlockId);
            DeleteRpcQUeueCommand = new Command(DeleteRpcQueue);
            if (dvSystem != null)
            {
                systemId = dvSystem.Id;
                this.ecsName = dvSystem.SystemName;
                this.ECSComm = dvSystem.EcsComm;
                this.EcsDesc = dvSystem.SystemDesc;
                this.CommEnable = Convert.ToBoolean(dvSystem.CommEnable);
                if (dvSystem.CommInfo != null)
                {
                    this.MqCommModel = JsonConvert.DeserializeObject<MQCommunicationModel>(dvSystem.CommInfo);
                    if (this.MqCommModel != null)
                    {
                        MqRpcType = Utli.ConvertToEnum<MQRPCQueueTypeEnum>(this.MqCommModel.rpc_queue_type);
                        if(MqRpcType!= MQRPCQueueTypeEnum.ONLY_ONE_RPC)
                        {
                            this.MqRpcQueueLst = new ObservableCollection<MQRpcQueueModel>(this.MqCommModel.rpc_queue_list);
                        }
                    }
                }
            }
            else
            {
                IsAddNew = true;
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
            if (MqRpcQueueLst == null || MqRpcQueueLst.Count == 0)
            {
                this.MqRpcQueueLst = new ObservableCollection<MQRpcQueueModel>();
            }
            this.MqRpcQueueLst.Add(new MQRpcQueueModel()
            {
                key = "1",
                queue_name = "new_queue_name",
            });
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
                    Where(x=>x.system_id==this.systemId).ExecuteCommand();
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
            //paramIdLst = new List<string>(); 

            //  foreach (var item in ParamLst) 
            //  {
            //      if (item.IsSelected)
            //      {
            //          paramIdLst.Add(item.Id.ToString());
            //      }
            //  }
            //  if (paramIdLst.Count == 0)
            //  {
            //      System.Windows.Forms.MessageBox.Show("请选择至少一个参数！");
            //      return;
            //  }

        }
        private void CancelSelection(object o)
        {
            var win = o as Window;
            win.DialogResult = true;
        }
    }
    
}
