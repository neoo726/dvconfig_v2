using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace DataViewConfig
{
    public static class TemplateConfigHandler
    {
        public static TemplateConfig templateConfig { get; set; }
       
        static TemplateConfigHandler()
        {
           
            ReadTemplateConfig();
        }
        public static void  ReadTemplateConfig()
        {
            // 读取xml
            templateConfig = Utli.Load<TemplateConfig>(System.AppDomain.CurrentDomain.BaseDirectory + "templateConfig.xml");
        }
        public static void UpdateTemplateConfig()
        {
            Utli.Save<TemplateConfig>(System.AppDomain.CurrentDomain.BaseDirectory + "templateConfig.xml",templateConfig);
        }
    }
    [XmlRoot("TemplateConfig")]
    public class TemplateConfig
    {
        [XmlElement("recentTemplate")]
        public string RecentTemplate { get; set; }

        [XmlElement("templateSwitch")]
        public TemplateSwitch TemplateSwitch { get; set; }

        [XmlArray("templateList")]
        [XmlArrayItem("template")]
        public List<Template> TemplateList { get; set; }
    }

    public class TemplateSwitch
    {
        [XmlAttribute("autoDeleteOther")]
        public bool AutoDeleteOther { get; set; }
    }

    public class Template
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("templateType")]
        public string TemplateType { get; set; }

        [XmlAttribute("desc")]
        public string Description { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("imagePath")]
        public string ImagePath { get; set; }

        [XmlAttribute("templateFilePath")]
        public string TemplateFilePath { get; set; }

        [XmlAttribute("isSelected")]
        public bool IsSelected { get; set; }

        [XmlAttribute("tipsName")]
        public string TipsName { get; set; }
    }
}
