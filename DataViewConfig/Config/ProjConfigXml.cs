using CMSCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataViewConfig.Config
{
   public class ProjConfig
    {
        public static ProjConfigXml curProjConfig;
        private string m_xmlPath = "";
        public ProjConfig(string xmlPath)
        {
            m_xmlPath = xmlPath;
            ReadProjConfigXml();
        }
        public void ReadProjConfigXml()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(ProjConfigXml),"Config");
                System.IO.StreamReader file = new System.IO.StreamReader(m_xmlPath);
                
                curProjConfig = (ProjConfigXml)reader.Deserialize(file);
                file.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("异常:" + e.ToString());
                LogHelper.Error($"[ProjConfig_Initial]{e.ToString()}");
            }
        }
        /// <summary>
        /// Saves the current configuration to the specified XML file.
        /// </summary>
        public void SaveProjConfigXml()
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(ProjConfigXml), "Config");
                using (StreamWriter file = new StreamWriter(m_xmlPath, false, Encoding.UTF8))
                {
                    writer.Serialize(file, curProjConfig);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("保存配置时发生异常: " + e.ToString());
                LogHelper.Error($"[SaveProjConfigXml]{e.ToString()}");
            }
        }
        [XmlRoot(Namespace = "", IsNullable = false, ElementName = "ProjConfigXml")]
        public class ProjConfigXml
        {
            public List<User> UserList { get; set; }
            //[XmlElement("ShowRcmsConfigSelector")]    
            //public ShowRcmsConfigSelector showRcmsConfigSelector { get; set; }
            //[XmlElement("ShowDbSelectionWindow")]
            //public ShowDbSelectionWindow showDbSelectionWindow { get; set; }
            [XmlElement("DbConnectionString")]
            public DbConnectionString dbConnectionString { get; set; }
            [XmlElement("ConfigFileXmlName")]
            public string configFileXmlName { get; set; }
            public class User
            {
                [XmlAttribute()]
                public int userLevel { get; set; }
                [XmlAttribute()]
                public string userName { get; set; }
                [XmlAttribute()]
                public string userPwd { get; set; }
            }
            public class DbConnectionString
            {
                [XmlAttribute("dbType")]
                public string DbType { get; set; }

                [XmlText]
                public string ConnectionString { get; set; }
            }
            public class ShowDbSelectionWindow
            {
                [XmlAttribute("enable")]
                public bool showDbSelectionWindowEnable { get; set; }
            }
            public class ShowRcmsConfigSelector
            {
                [XmlAttribute("enable")]
                public bool showRcmsConfigSelector { get; set; }
            }
            [XmlElement("CurLanguage")]
            public string CurLanguage { get; set; }
        }
        
    }
}
