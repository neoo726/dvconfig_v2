using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using CMSCore;

namespace DataViewConfig
{
    internal class Utli
    {
        /// <summary>
        /// 枚举转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T ConvertToEnum<T>(string val)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), val, true);
            }
            catch (Exception e)
            {
                throw new Exception($"枚举转换异常：" + e.ToString());
            }
        }
        /// <summary>
        /// 获取当前激活的语言资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetResourceString(string key)
        {
            try
            {
                var resource = System.Windows.Application.Current.Resources[key];
                if (resource != null)
                {
                    return resource.ToString();
                }
                return key;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"获取资源字符串失败: {ex.Message}");
                return key;
            }
        }
        public static string GetEnumDescription<T>(object enumValue)
        {
            try
            {
                var curType = typeof(T);
                var fieldInfo = curType.GetField(enumValue.ToString());
                if (fieldInfo == null)
                {
                    return enumValue.ToString();
                    //throw new Exception($"获取枚举信息异常。枚举类型：{typeof(T)},枚举值：{enumValue}");
                }
                var descriptionAttribute = fieldInfo
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;
                return descriptionAttribute != null ? descriptionAttribute.Description : enumValue.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception($"获取枚举异常.枚举类型：{typeof(T)},枚举值：{enumValue}." + ex.ToString());
                
            }
        }
        /// <summary>
        /// 判断字符串是否包含元素，字符串为null则返回fals
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="itemStr"></param>
        /// <returns></returns>
        public static bool StringContains(string sourceStr,string itemStr)
        {
            if (string.IsNullOrEmpty(sourceStr)|| string.IsNullOrEmpty(itemStr))
            {
                return false;
            }
            if (sourceStr.ToLower().Contains(itemStr.ToLower()))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断是否为合法IP
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool IsValidIPAddress(string ipAddress)
        {
            string pattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(ipAddress);
        }
        /// <summary>
        /// 利用visualtreehelper寻找对象的子级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        TList.Add((T)child);
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                    else
                    {
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }
                return TList;
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(ee.Message);
                return null;
            }
        }
        // 读取xml
        public static T Load<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File '{filePath}' not found.");
            }
            T Result = default(T);
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filePath))
            {
                Result = serializer.Deserialize(reader) as T;
                reader.Close();
            }
            return Result;
        }

        // 更新xml
        public static void Save<T>(string filePath, T data) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                IndentChars = "  ", // 设置缩进字符为两个空格
            };
            using(var writer = XmlWriter.Create(filePath, settings))
            {
                serializer.Serialize(writer, data);
                writer.Close();
            }
            
        }
        //public  static void MySort<TSource,TKey>(this ObservableCollection<TSource> observableCollection,Func<TSource,TKey> keySelector)
        //{
        //    var a = observableCollection.OrderBy(keySelector).ToList();
        //    observableCollection.Clear();
        //    foreach (var b in a)
        //    {
        //        observableCollection.Add(b);
        //    }
        //}
        #region 设置简单的随机字符串
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomChars);
        }

        public static string AddTimestamp(string inputString)
        {
            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            return inputString + timestamp;
        }
        #endregion
        /// <summary>
        /// 根据数据库中配置的语言选项 ，切换UI语言风格
        /// </summary>
        public static void ChangeLanguage()
        {
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in System.Windows.Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedEnCulture = @"./Language/en_us.xaml";
            string requestedZhCulture = @"./Language/zh_cn.xaml";
            ResourceDictionary resourceZhDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedZhCulture));
            ResourceDictionary resourceEnDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedEnCulture));

            System.Windows.Application.Current.Resources.MergedDictionaries.Remove(resourceZhDictionary);
            System.Windows.Application.Current.Resources.MergedDictionaries.Remove(resourceEnDictionary);
            if (GlobalConfig.CurLanguage == GlobalLanguage.enUS)
            {
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceEnDictionary);
            }
            else
            {
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceZhDictionary);
            }
        }
    }
}
