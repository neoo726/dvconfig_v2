   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5	   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5
   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   4    .DataViewConfig.Pages.Popups.EcsSystemEditPopup   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5   
   =	converter'clr-namespace:DataViewConfig.Converters     5	   	   6vm'clr-namespace:DataViewConfig.ViewModels     5
   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  EcsSystemEditPopupWin��5   	    � Title$ 参数选择��5   	   $	��650��6   $	��850��6#   
������� � 
ResizeMode$ NoResize=�5   	      WIndowChromeKey'  !System.Windows.Shell.WindowChrome   WindowChrome# �   5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.     NormalBackGround#��[ 6B   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool 	 targetTypeEnum S� 
MethodName S� 
ObjectType	  System.Enum	 S� MethodParameters
 M� Type,
  &DataView_Configuration.ParamTargetType 
 paramSourceEnum/  )DataView_Configuration.RequestParamSource  WindowChromeKey   ResizeBorderThickness   CaptionHeight��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &		 �     &	
 �     &	 ;      5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5   
   $ 	GetValues��64   " 	 6K   	 5      M� 5      "
 
 6   5!      S� 5"   
   $ 	GetValues��65   " 	 6L   	 5#      M� 5$      "
  6   5&        5'   
    5(      u�5)      5q�6   $ 0Q�5+   G   5,      5-      .��5.      � -   5         �Name$
 grid��5.      ���#1f1f1f � RowDefinitions 5/   
   �� 50      $��30 �6   �� 51      �� 52      $��80 �6   .+�54   
   ]� $��0��6   
���   �.+�55      �� $��Stretch=�6   $
��Left=�62   $��交互对象配置��6H     NormalLabel#��[ 6Y   �� 56      $��1��6   $��Right=�6&   $��Right=�6=     
TextButton#��[ 6Y   $	����57      
�������$��0 0 10 0q�6<   ��58      �� *CloseCommand+��68   ��  �� ElementName$ EcsSystemEditPopupWin��6y   59      �� 5:   
   $��1��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�5;      �� $ 
Horizontal=�6   $��5q�62   .+�5<      �� $��对象名称��6   $	��100��6)   $
��Left=�65   #��[ 6W   �� 5=      $	��150��64     NormalTextBox#��[ 6@   ��6   �� *	EcsName+6@   �� 5>      -   5         ��Name$ isCommEnableChkbox��5>      $��启用��6X   $	��150��6e   $��20 0 0 0q�6q   $��Center=�6�   
������� d� 	IsChecked 67   �� *
CommEnable+6�   5?      �� 5@      $ 
Horizontal=�6   $��5q�62   .+�5A      �� $��对象描述��6   $	��100��6)   $
��Left=�65   #��[ 6W   �� 5B      $	��150��64   #��[ 6@   ��6   �� *	EcsDesc+6@   5C      �� 5D      {�6   �� $ isCommEnableChkbox�� �� Path$ 	IsChecked�.+�5F      �� $ Horizontal =�6   $��5q�67   .+�5G      �� $��通讯类型��6   $	��100��6,   $
��Left=�68   #��[ 6Z   	� 5H      -   5         	�Name$ MQCommTypeRadioBtn��5H   "   $��RabbitMQ��5J   "     NormalRadioButton#��[ 66    5I   "   �� *	ECSComm+ �� 	Converter# [ 5J   6   	� 5K      $��OPCUA��6"   #��[ 62    5L   #   �� *	ECSComm+# [ 5N      �� 5P      y�6   �� $ MQCommTypeRadioBtn��$ 	IsChecked�# [ .+�5Q      �� $ Horizontal =�6!   $��5q�6;   .+�5R      �� $��MQ配置��6    $	��100��6/   $
��Left=�6;   $
��Left=�6V   #��[ 6x   5S      �� 5T      $ Vertical=�6!   $��	20 0 0 0 q�68   .+�5U      �� $ 
Horizontal=�6%   $��5q�6>   .+�5V      �� $��IP��6$   $	��150��61   $
��Left=�6>   #��[ 6`   �� 5W      $	��150��6F   #��[ 6T   ��6&   �� *MqCommModel.ip+6T   �� 5X      $��端口��6$   $	��100��61   #��[ 6>   �� 5Y      $	��100��6H   #��[ 6U   ��6&   �� *MqCommModel.port+6U   5Z      �� 5[      $ 
Horizontal=�6%   $��5q�6>   .+�5\      �� $��	用户名��6$   $	��150��62   $
��Left=�6?   #��[ 6a   �� 5]      $	��150��6H   #��[ 6U   ��6&   �� *MqCommModel.user+6U   �� 5^      $��密码��6$   $	��100��61   #��[ 6>   �� 5_      $	��100��6L   #��[ 6Y   ��6&   �� *MqCommModel.password+6Y   5`      �� 5a      $ 
Horizontal=�6%   $��5q�6>   .+�5b      �� $��Fanout交换机名称��6$   $	��150��6:   $
��Left=�6G   #��[ 6i   �� 5c      $	��350��6S   #��[ 6`   ��6&   �� *MqCommModel.fanout_exchange+6`   5e      �� 5f      $ 
Horizontal=�6%   $��5q�6>   .+�5g      �� $��RPC队列类型��6$   $	��150��66   $
��Left=�6C   #��[ 6e   	� 5h      -   5        $ OnlyOneRpcRadioBtn��5h   *   $��单个RPC队列��6F   #��[ 5j   *    5i   *   �� *	MqRpcType+# [ 5j   *   	� 5k      -   5        $ MultiRpcByCIDRadioBtn��5k   *   $,��&多RPC队列（根据Crane ID区分）��6I   #��[ 5m   *    5l   *   �� *	MqRpcType+# [ 5m   *   	� 5n      -   5        $ MultiRpcByBlockIDRadioBtn��5n   *   $,��&多RPC队列（根据Block ID区分）��6M   #��[ 5p   *    5o   *   �� *	MqRpcType+# [ 5p   *   5r      �� 5s      $ 
Horizontal=�6%   $��5q�6>   .+�5u      �� $��RPC交换机名称��6$   $	��150��67   $
��Left=�6D   #��[ 6f   �� 5v      $	��350��6P   #��[ 6]   ��6&   �� *MqCommModel.rpc_exchange+6]   5x      �� 5z      $ 
Horizontal=�6%   $��5q�6>   y�5{   &   �� $ OnlyOneRpcRadioBtn��$ 	IsChecked�# [ .+�5|      �� $��RPC唯一队列名称��6$   $	��150��69   $
��Left=�6F   #��[ 6h   �� 5}      $	��250��6M   #��[ 6Z   ��6&   �� *MqCommModel.rpc_queue+6Z   5~      �� 5�      y�6%   �� $ OnlyOneRpcRadioBtn��$ 	IsChecked�# [ .+�5�      �� $ 
Horizontal=�6)   $��5q�6B   .+�5�   "   �� $��RPC队列相关宏��6(   $	��150��6;   $
��Left=�6H   #��[ 6j   �� 5�   "   $	��350��6Y   #��[ 6f   ��6*   �� *MqCommModel.related_cid_macro+6f   5�      �� 5�      $��5q�6)   $ Vertical=�64   .+�5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��RPC队列列表��6,   $	��150��6>   $
��Left=�6J   $
��Left=�6l   #��[ 6�   �� 5�   &   $��添加��6-   ���#4c4c4c$	��100��6O   $��25��6[   $��10q�6g     NormalButton#��[ 6s   $�� ��6�   ��6�   �� *AddNewRpcQUeueCommand+6�   �� 5�   &   $ ��根据Crane ID快速添加��6-   ���#4c4c4c$	��180��6[   $��25��6g   $��10q�6s   #��[ 6   $�� ��6�   ��6�   �� *QuickAddRpcByCraneIDCommand+6�   �� 5�   &   $ ��根据Block ID快速添加��6-   ���#4c4c4c$	��180��6[   $��25��6g   $��10q�6s   #��[ 6   $�� ��6�   ��6�   �� *QuickAddRpcByBlockIDCommand+6�   5�   #   �� 5�   "   .��5�   &   &   System.Windows.Controls.DataGrid  $	��100��6/      	RowHeight$ 25Q�5�   ,   $��Center=�6<      AutoGenerateColumns .     CanUserAddRows .  "   VerticalScrollBarVisibility$
 Auto=�5�   c   ��5�   <   �� *MqRpcQueueLst+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5�   *   4  .System.Windows.Controls.DataGridTemplateColumn  5�   .   ,  &System.Windows.Controls.DataGridColumn   Header$ 操作��6E      CellTemplate 5�   2   �� 5�   6   .R�5�   :   �� $ 
Horizontal=�6E   .+�5�   >   �� $��删除��6E   #��[ 6R   ��6v   �� *#!DataContext.DeleteRpcQUeueCommand+ �� RelativeSource ��   �� Mode$  FindAncestor=�! �� AncestorType$! DataGridL���5�   -   ��  �� $  FindAncestor=�$! DataGridL�$ SelectedItem�5�   ;   5�   7   5�   /   0  *System.Windows.Controls.DataGridTextColumn  5�   .   "   Width5  /System.Windows.Controls.DataGridLengthConverter$	" 100 6D   1  +System.Windows.Controls.DataGridBoundColumn#   Binding# 6P   �� *key+$   HeaderTemplate$ 5�   2   �� 5�   6   .R�5�   :   �� ��6D   �� *!DataContext.MqRpcTypeHeaderDesc+$ TwoWay=�$ PropertyChanged=� �� $! DataGridL�5�   7   %   ElementStyle% 5�   2   �� 5�   6   .�5�   :   �� & �� Property& � ��' �� Value$' Center=�6o   5�   7   5�   /     5�   .   $ RPC队列名称��6A   $	" 450 6R   # 6^   �� *
queue_name+% 5�   2   �� 5�   6   .�5�   :   �� & � ��$' Center=�6o   5�   7   5�   /   5�   '   5�   #   5�      5�      5�      5�      5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Right=�6/   $��2��6K   .+�5�      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O   #��[ 6[   ��5�      �� *ConfirmCommand+��69   �� $ EcsSystemEditPopupWin��6z   �� 5�      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5�      �� *CancelCommand+��69   �� $ EcsSystemEditPopupWin��6z   5�      5�      5�      