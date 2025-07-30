using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DataViewConfig.Controls
{
    /// <summary>
    /// MenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class MenuItem : UserControl
    {
        public Action AddEvent;
       
        MainWindow _context;
        public MenuItem(MenuModel menu,MainWindow context,bool IsShowAddBtn=false, Action addHandler =null)
        {
            InitializeComponent();
            this.DataContext= menu;
            _context = context;
            if (!IsShowAddBtn)
            {
                this.AddBtn.Visibility=Visibility.Collapsed;
            }
            else
            {
              
                this.AddEvent = addHandler;
                this.AddBtn.Click += AddBtn_Click; ;
            }
            this.ExpanderMenu.Visibility=menu.MenuItemLst==null?Visibility.Collapsed:Visibility.Visible;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AddEvent != null)
            {
                AddEvent.Invoke();
            }
        }

        //切换画面
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((MenuItemModel)((ListView)sender).SelectedItem);
            if (selectedItem != null)
            {
                if (!selectedItem.IsDynamicCreated)
                {
                    (_context.DataContext as MainViewModel).OpenPage(selectedItem.RelatedConfigPageName);
                }
                else
                {
                    (_context.DataContext as MainViewModel).ExSystemConfigPage(selectedItem.ExSystemModel);
                }
            }
        }

        private void ListViewMenu_Unselected(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewMenu_LostFocus(object sender, RoutedEventArgs e)
        {
            var curListView = (ListView)sender;
            var focusedElement = FocusManager.GetFocusedElement(Application.Current.MainWindow);
            if (focusedElement != null)
            {
                var curType = focusedElement.GetType();
                if (curType == typeof(ListViewItem))
                {
                    curListView.SelectedIndex = -1;
                }
            }
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicDeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_LostFocus(object sender, RoutedEventArgs e)
        {
            //var itema = e.Sourcce;
            ListViewItem item = e.OriginalSource as ListViewItem;
            if (item != null)
            {
                item.Background = new SolidColorBrush(Colors.Transparent);
                item.IsSelected = false;
            }
        }
    }
    public class MenuModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MenuModel(string header, List<MenuItemModel> menuItemLst)
        {
            this.Header = header;
            this.MenuItemLst = new ObservableCollection<MenuItemModel>(menuItemLst);
        }
        public string Header { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public ObservableCollection<MenuItemModel> MenuItemLst { get; set; }
        
    }
    public class MenuItemModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string RelatedConfigPageName { get; set; }
        //是否动态创建item
        public bool IsDynamicCreated { get; set; }
        public object ExSystemModel { get; set; }
        //是否enable
        private bool isEnable { get; set; }
        public bool IsEnable 
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                EventBus.Instance.PublishExSystemEnableChange(this.Name, value);
            }
        }
//是否显示该Item
public bool IsShow { get; set; }
       
        public bool IsDeleteEnable { get; set; }

public event PropertyChangedEventHandler PropertyChanged;
        public Command DeleteCommand { get; set; }
        /// <summary>
        /// 订阅交互对象的启用enable属性和是否显示属性
        /// </summary>
        public void SubEventHandler()
        {
            EventBus.Instance.EventChangeEventHandler += EventBus_EventChangeEventHandler;
        }
        public MenuItemModel()
        {
            IsShow = true;
            DeleteCommand = new Command(new Action<object>((o) =>
            {
                EventBus.Instance.PublishDeleteExSystem(o.ToString());
            }));
        }
        ~MenuItemModel()
        {
            EventBus.Instance.EventChangeEventHandler -= EventBus_EventChangeEventHandler;
        }
        private void EventBus_EventChangeEventHandler(EventBus.EventBusType eventType,string exSystemName=null, bool CommEnable = false)
        {
            if (!IsDynamicCreated) return;
            if(eventType== EventBus.EventBusType.ExSystemChanged&& exSystemName==Name)
            {
                this.IsEnable = CommEnable;
            }
        }
    }
}
