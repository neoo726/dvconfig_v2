using DataView_Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewConfig
{
    public class EventBus
    {
        #region 单例
        private static readonly EventBus _instance = new EventBus();
        static EventBus() { }
        private EventBus() { }
        public static EventBus Instance
        {
            get { return _instance; }
        }
        #endregion
        public enum EventBusType
        {
            ExSystemChanged,
            EditConfigPageOpen,
        }

        //交互对象通讯Enable修改  通知
        public delegate void ExSystemEnableChangeDel(string exSystemName , bool CommEnable );
        public event ExSystemEnableChangeDel ExSystemEnableChangeEventHandler;

        public delegate void EventChangeDel(EventBusType eventType,string exSystemName=null, bool CommEnable = false);
        public event EventChangeDel EventChangeEventHandler;
        public delegate void ConfigChangeChangeDel();
        public event ConfigChangeChangeDel ConfigChangeEventHandler;

        public delegate void DeleteExSystemDel(string exSystemName);
        public event DeleteExSystemDel DeleteExSystemEventHandler;

        public delegate void ConfigPageEventChangeDel(EventBusType eventType,string pageName);
        public event ConfigPageEventChangeDel EditConfigPageOpenEventHandler;

        public delegate void LoginTypeChangeDel(UmsTypeEnum loginType);
        public event LoginTypeChangeDel LoginTypeEventHandler;
        public void LoginTypePublish(UmsTypeEnum loginType)
        {
            if (LoginTypeEventHandler != null)
            {
                LoginTypeEventHandler.Invoke(loginType);
            }
        }
        /// <summary>
        /// 交互对象Commnunication  enable属性变更通知
        /// </summary>
        /// <param name="exSystemName"></param>
        /// <param name="CommEnable"></param>
        public void PublishExSystemEnableChange(string exSystemName, bool CommEnable)
        {
            if (ExSystemEnableChangeEventHandler != null)
            {
                ExSystemEnableChangeEventHandler.Invoke(exSystemName, CommEnable);
            }
        }
        /// <summary>
        ///
        /// </summary>
        public void PublishConfigChange()
        {
            if (ConfigChangeEventHandler != null)
            {
                ConfigChangeEventHandler.Invoke();
            }
        }
        public void PublishDeleteExSystem(string exSystemName)
        {
            if (DeleteExSystemEventHandler != null)
            {
                DeleteExSystemEventHandler.Invoke(exSystemName);
            }
        }
        public void Publish(EventBusType eventType, string exSystemName = null, bool CommEnable=false)
        {
            if (EventChangeEventHandler != null)
            {
                EventChangeEventHandler.Invoke(eventType, exSystemName,CommEnable);
            }
        }

        public void Publish(EventBusType eventType,string pageName)
        {
            if (EditConfigPageOpenEventHandler != null)
            {
                EditConfigPageOpenEventHandler.Invoke(eventType,pageName);
            }
        }
    }
}
