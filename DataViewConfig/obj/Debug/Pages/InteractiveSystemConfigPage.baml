   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null;"clr-namespace:DataViewConfig.PagesDataViewConfig.Pages  5      A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5	      /clr-namespace:DataViewConfigDataViewConfig  5
      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      6    0DataViewConfig.Pages.InteractiveSystemConfigPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:DataViewConfig.Pages     5      :controls%clr-namespace:DataViewConfig.Controls     5      =	converter'clr-namespace:DataViewConfig.Converters     5      6vm'clr-namespace:DataViewConfig.ViewModels     5	      0currentclr-namespace:DataViewConfig     5
      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5      6sys&clr-namespace:System;assembly=mscorlib     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  6A   
������ $��1q�5   S      Content.BackGround#��[  5      
�������$��15(�6R   $��5q�5        � 	Resources  5      5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2Reverse8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.EcsComm_Rest2BoolConverter  EcsCommTypeRest2Bool;  5DataViewConfig.Converters.EcsComm_Redis2BoolConverter  EcsCommTypeRedis2BoolF  @DataViewConfig.Converters.EcsComm_Int2FanoutTitleStringConverter  EcsCommTypeInt2FanoutTitle:	  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter 	 MqRpcOneType2Bool<
  6DataViewConfig.Converters.Bool2DisplayContentConverter 
 Bool2DisplayYesOrNoC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2BoolI  CDataViewConfig.Converters.MqRpcTypeMultiBlockId2VisibilityConverter ! MqRpcMultiBlockIdType2VisibleE  ?DataViewConfig.Converters.MqRpcTypeMultiCid2VisibilityConverter   MqRpcMultiCidType2Visibility  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type,  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource  ShowOpcColumns R� Value �� ElementName �� Path �� 	Converter  ShowMqColumns  ShowReturnValueColumns  HideUnRedisColumns  ShowRedisColumns*  $System.Windows.Controls.DataGridCell	 �� 
TargetType
 �� Property �� Value �� 	Resources �� Color �� Triggers P� Property P� Value��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 �     &	 
    &	 e    &	 �    7  &	     7  &	 U    7  &	 �    7  &	 �    7  '  9      5   
   6*     5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   60     5   
   6;   	  5   
   6/   
  5   
   61     5   
   68     5   
   64     5   
   6>     5   
   6:   S� 5    
   $ 	GetValues��64   "  6K    5!      M� 5"      "  6   5$      S� 5%   
   $ 	GetValues��65   "  6L    5&      M� 5'      "  6   5)      i� 5*   
    69   �� $ OPCCommTypeRadioBtn��$ 	IsChecked�8   i� 5+   
    68   �� $ MQCommTypeRadioBtn��$ 	IsChecked�8   i� 5,   
    6A   �� $ globalReturnRadioBtn��$ 	IsChecked�8   i� 5-   
    6=   �� $ RestCommTypeRadioBtn��$ 	IsChecked�8   i� 5.   
    6;   �� $ RestCommTypeRadioBtn��$ 	IsChecked�8   �� 52   
   "	  6   .�53      �� 
 � ��$ 15(�6)    54      ���� 55      $ White��6i   (��       6"   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6i   56       57      P� 58       �@ 
IsSelected . . �5:      �� 
 � �� �#00469663   �� 5;      
 � ��
 ���� 64   5<      5>      5@      .��5A      ��  �� Orientation$ Vertical=�6   .+�5B   
   �� $ 
Horizontal=�6     Content.Topbar.BackGround#��[ 6/   $��5q�6g   .+�5C      ��   
NormalText#��[ 61   $��18(�6U   $
��Bold"�6c   $��10q�6u   ��6   �� *	EcsName+6u   5F      �� 5H   
   $��Visible=�6   .��5I      �� $ Vertical=�6   .+�5K      #  HandyControl.Controls.Divider     Content$ 基础信息��6      
LineStroke �#393939
�������6B   9� 5L      -   5         9�Name$ BaseInfoExpander��5L      $"��基础信息(点击展开）��5M      #��[ 6.   
�������$��0 5 0 0q�6z     Content.Expander.Border#��[ 5N       $��1q�6X    9� 
IsExpanded 5L   V   �� *AutoExpandedBaseInfo+.��5O      �� $��Visible=�6#   $	��500��6J   .��5P      �� #��[ 6!   $��1q�6X    �� Padding$ 5q�6l   ._�5Q      �� $ Vertical=�6)   .+�5R   "   �� $ 
Horizontal=�6-   #��[ 6G   $��5q�6   .+�5S   &   �� $��对象名称��60   #��[ 6<   $��18(�6`   $
��Bold"�6n   $��10q�6�   '   !DataViewConfig.Controls.TipsLabel  5T   &      TipName$' ![interactive_config]exsystem_name��6;   �� 5U   &   $	��250��6.     NormalTextBox#��[ 6�    �� 
CaretBrush
 �����
������ 
�������{�6:   �� *IsPermanent+# [ ��6�   �� *	EcsName+6�   �� 5V   &   $��对象描述��60   #��[ 6<   $��10q�6`   �� 5W   &   $	��250��6.   #��[ 6S   
 �����
������ 
���������6:   �� *	EcsDesc+6�   �� 5X   &   -   5         ��Name$ isCommEnableChkbox��5X   /   $��启用��6l   $��20 0 0 0q�6y   $��Center=�6�   
������� d� 	IsChecked 6K   �� *
CommEnable+6�     5Y   &   $) #[interactive_config]exsystem_enable��6;   5Z   #   �� 5[   "   $��通讯类型��6+   
������   NormalGroupBox#��[ 6T   .��5\   &   �� $ 
Horizontal=�61   $��50 5 0 5q�6J   .+�5^   *   	� -   5         	�Name$ MQCommTypeRadioBtn��5^   6   $��RabbitMQ��5`   "     NormalRadioButton#��[ 66   $��0 0 50 0q�6a   {�5^   S   �� *
IsShowItem+ 5_   "   �� *	ECSComm+# [ 5`   a   	� 5a   *   -   5        $ OPCCommTypeRadioBtn��5a   6   $	��OPC��5c      #��[ 6.   $��0 0 50 0q�6Z   {�5a   S   �� *
IsShowItem+ 5b   #   �� *	ECSComm+# [ 5c   Z   	� 5d   *   -   5        $ RestCommTypeRadioBtn��5d   6   $
��Rest��5f      #��[ 6/   $��0q�6Z   {�5d   T   �� *
IsShowItem+ 5e   #   �� *	ECSComm+# [ 5f   Z     5g   *   $' ![interactive_config]exsystem_name��6?   5k   '   5l   #   �� 5m   "   y�6-   �� $ MQCommTypeRadioBtn��$ 	IsChecked�# [ .+�5n   &   �� $ Vertical=�62   .+�5o   *   �� $��连接信息��63   
������ #��[ 6[   .��5p   .   �� $ 
Horizontal=�69   $��50 5 0 5q�6R   .+�5r   2   �� $ 
Horizontal=�6=   $��0 0 20 0q�6V   .+�5s   6   �� $��RabbitMQ  IP��6<   $	��150��6T   $
��Left=�6a     NormalLabel#��[ 6�   �� 5t   6   $	��150��6^   #��[ 6l   ��6>   �� *MqCommModel.ip+6l   5u   3   �� 5v   2   $ 
Horizontal=�6=   $��0 0 20 0q�6V   .+�5w   6   �� $��端口��6<   $��50��6I   #��[ 6V   �� 5x   6   $��80��6`   #��[ 6l   ��6>   �� *MqCommModel.port+6l   5y   3   �� 5z   2   $ 
Horizontal=�6=   $��0 0 20 0q�6W   .+�5{   6   �� $��	用户名��6<   $��60��6K   $
��Left=�6Y   #��[ 6{   �� 5|   6   $	��150��6`   #��[ 6m   ��6>   �� *MqCommModel.user+6m   5}   3   �� 5~   2   $ 
Horizontal=�6=   .+�5   6   �� $��密码��6<   $��50��6I   #��[ 6V   �� 5�   6   $	��100��6d   #��[ 6q   ��6>   �� *MqCommModel.password+6q   5�   3   5�   /   5�   +   �� 5�   *   $��Fanout广播��63   
������ #��[ 6_   .��5�   .   �� $ 
Horizontal=�69   $��50 5 0 5q�6R   .+�5�   2   �� $��交换机名称��68   $	��150��6I   $
��Left=�6U   #��[ 6w   �� 5�   2   $	��350��6g   #��[ 6t   ��6:   �� *MqCommModel.fanout_exchange+6t   5�   /   5�   +   �� 5�   *   $��RPC交互请求��63   
������ #��[ 6^   .��5�   .   �� $ Vertical=�69   $��50 5 0 5q�6P   .+�5�   2   �� $ 
Horizontal=�6=   $��0 10 0 0q�6V   .+�5�   6   �� $��RPC队列类型��6<   $	��150��6N   $
��Left=�6[   #��[ 6}   	� 5�   6   -   5        $ OnlyOneRpcRadioBtn��5�   B   $��单个RPC队列��6�   #��[ 5�   *   $��0 0 10 0q�6U   {�5�   _   �� *
IsShowItem+ 5�   *   �� *	MqRpcType+# [	 5�   U   	� 5�   6   -   5        $ MultiRpcByCIDRadioBtn��5�   B   $,��&多RPC队列（根据Crane ID区分）��6�   #��[ 5�   *   {�5�   b   �� *
IsShowItem+ 5�   *   �� *	MqRpcType+# [ 5�   *   	� 5�   6   -   5        $ MultiRpcByBlockIDRadioBtn��5�   B   $,��&多RPC队列（根据Block ID区分）��6�   #��[ 5�   *   {�5�   f   �� *
IsShowItem+ 5�   *   �� *	MqRpcType+# [ 5�   *   5�   3   �� 5�   2   $ 
Horizontal=�6=   $��	0 10 0 10q�6V   .+�5�   6   �� $��RPC交换机名称��6<   $	��150��6O   $
��Left=�6\   #��[ 6~   �� 5�   6   $	��350��6h   #��[ 6u   ��6>   �� *MqCommModel.rpc_exchange+6u   5�   3   �� 5�   2   $ 
Horizontal=�6=   $��0 5 0 10q�6V   y�5�   &   �� $ OnlyOneRpcRadioBtn��$ 	IsChecked�# [ .+�5�   6   �� $��RPC队列名称��6<   $	��150��6O   $
��Left=�6\   #��[ 6~   �� 5�   6   $	��250��6e   #��[ 6r   ��6>   �� *MqCommModel.rpc_queue+6r   5�   3   �� 5�   2   y�6=   �� $ OnlyOneRpcRadioBtn��$ 	IsChecked�# [ .+�5�   6   �� $ 
Horizontal=�6A   .+�5�   :   �� $��RPC队列相关宏��6@   $	��150��6S   $
��Left=�6`   #��[ 6�   �� 5�   :   $	��350��6q   #��[ 6~   ��6B   �� *MqCommModel.related_cid_macro+6~   5�   7   �� 5�   6   $ Vertical=�6C   .+�5�   :   �� $ 
Horizontal=�6E   .+�5�   >   �� $��RPC队列列表��6D   $	��150��6V   $
��Left=�6b   $
��Left=�6�   #��[ 6�   �� 5�   >   $��添加��6E   $	��100��5�   :   $��25��6F     NormalButton#��[ 6S   $�� ��5�   c   ��69   �� *AddNewRpcQUeueCommand+6y   �� 5�   >   $ ��根据Crane ID快速添加��6E   $	��180��5�   9   $��25��6E   $��10q�6Q   #��[ 6]   $�� ��5�   i   y�5�   9   �� *	MqRpcType+# [ ��5�   9   �� *QuickAddRpcByCraneIDCommand+6   �� 5�   >   $ ��根据Block ID快速添加��6E   $	��180��5�   9   $��25��6E   $��10q�6R   #��[ 6_   $�� ��5�   i   y�5�   9   �� *	MqRpcType+# [ ��5�   9   �� *QuickAddRpcByBlockIDCommand+6   5�   ;   �� 5�   :   $ 
Horizontal=�6E   $��5q�6^   y�5�   1   �� *ShowRpcQueuesEditOrAdd+# [ .+�5�   >   �� $
��Left=�6m   $
��Left=�6�   #��[ 6�   ��6D   �� *MqRpcTypeHeaderDesc+6�   �� 5�   >   $	��100��6e   $
��Left=�6q   $
��Left=�6�   #��[ 6�   ��6F   �� *MqRpcQueueKey+6�   �� 5�   >   $��队列名称��6D   $
��Left=�6T   $
��Left=�6v   #��[ 6�   �� 5�   >   $	��300��6f   $
��Left=�6r   $
��Left=�6�   #��[ 6�   ��6F   �� *MqRpcQueueName+6�   �� 5�   >   $��确认添加��6E   $	��100��6U   $��25��6a   #��[ 6n   $�� ��5�   J   ��6   �� *!ConfirmAddOrEditRpcQueueCommand+6`   5�   ;   �� 5�   :   .��5�   >   &   System.Windows.Controls.DataGrid     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5�   +     NormalDataGrid#��[ 5�   +   ��5�   H   �� *MqRpcQueueLst+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5�   B   4  .System.Windows.Controls.DataGridTemplateColumn  5�   F   ,  &System.Windows.Controls.DataGridColumn    Header$  操作��6]   !   Width$	! 120 6i   "   CellTemplate" 5�   J   �� 5�   N   .R�5�   R   �� $ 
Horizontal=�6]   .+�5�   V   �� $��编辑��6]   $��60��6l     
TextButton#��[ 6w   ��6�   �� *!DataContext.EditRpcQUeueCommand+# �� RelativeSource# �� $ �� Mode$$ FindAncestor=�% �� AncestorType$% DataGridL���5�   -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem��� 5�   V   $��删除��6]   $��60��6l   #��[ 6w   ��6�   �� *#!DataContext.DeleteRpcQUeueCommand+# �� $$ FindAncestor=�$% DataGridL���5�   -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem�5�   S   5�   O   5�   G   0  *System.Windows.Controls.DataGridTextColumn  5�   F   $! * 6]   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6g   �� *key+'   HeaderTemplate' 5�   J   �� 5�   N   .R�5�   R   �� ��6\   �� *!DataContext.MqRpcTypeHeaderDesc+$ TwoWay=�$ PropertyChanged=�# �� $% DataGridL�5�   O   (   ElementStyle( 5�   J   �� 5�   N   .�5�   R   �� 
 � ��$ Center=�6�   5�   O   5�   G     5�   F   $  RPC队列名称��6Y   $! * 6j   & 6t   �� *
queue_name+( 5�   J   �� 5�   N   .�5�   R   �� 
 � ��$ Center=�6�   5�   O   5�   G   5�   ?   5�   ;   5�   7   5�   3   5�   /   5�   +   5�   '   5�   #   �� 5�   "   y�6-   �� $ RestCommTypeRadioBtn��$ 	IsChecked�# [ .+�5�   &   �� $ Vertical=�62   .+�5�   ,   �� $��连接信息��65   
������ #��[ 6]   .��5�   .   �� $ Vertical=�6:   $��50 5 0 5q�6Q   .+�5�   2   �� $ 
Horizontal=�6=   $��0 10 0 0q�6V   .+�5�   6   �� $��Rest Server 部署��6<   $	��150��6U   $
��Left=�6b   #��[ 6�   	� 5�   6   -	   5        $ OnlyOneRestServerRadioBtn��5�   B   $��唯一一个Rest Server��6�   #��[ 5�   *   $��0 0 10 0q�6U   {�5�   f   �� *
IsShowItem+ 5�   *   �� *	MqRpcType+# [	 5�   U   	� 5�   6   -
   5        $" MultiRestServerByCIDRadioBtn��5�   B   $1��+多个Rest Server（根据Crane ID区分）��6�   #��[ 5  *   {�5�   i   �� *
IsShowItem+ 5   *   �� *	MqRpcType+# [ 5  *   	� 5  6   -   5        $&  MultiRestServerByBlockIDRadioBtn��5  B   $1��+多个Rest Server（根据Block ID区分）��6�   #��[ 5  *   {�5  m   �� *
IsShowItem+ 5  *   �� *	MqRpcType+# [ 5  *   5  3   �� 5  2   $ 
Horizontal=�6=   $��	0 10 0 10q�6V   y�6j   �� $ OnlyOneRestServerRadioBtn��$ 	IsChecked�# [ .+�5  6   �� $	��150��6A   $ 
Horizontal=�6M   .+�5	  :   �� $��
Rest地址��6@   $
��Left=�6S   #��[ 6u     5
  :   $+ %[interactive_config]restserver_format��6O   5  7   �� 5  6   $	��350��6e   #��[ 6r   ��6>   �� *RestCommModel.baseUrl+6r   5  3   �� 5  2   $ 
Horizontal=�6=   $��	0 10 0 10q�6V   y�6j   �� $ OnlyOneRestServerRadioBtn��$ 	IsChecked�# [ .+�5  6   �� $	��250��6A   $ 
Horizontal=�6M   .+�5  :   �� $&�� 消息头（header) 字段名称��6@   $
��Left=�6]   #��[ 6     5  :   $+ %[interactive_config]restserver_header��6O   5  7   �� 5  6   $	��150��6j   #��[ 6w   ��6>   �� *RestCommModel.header_field+6w   �� 5  6   $ ��对Value进行Base64加密��6?      NormalCheckBox#��[  6�   
������� 6Z   �� *RestCommModel.header_encrypt+6�   5  3   �� 5  2   $ 
Horizontal=�6=   $��	0 10 0 10q�6V   y�6j   �� $ OnlyOneRestServerRadioBtn��$ 	IsChecked�# [ .+�5  6   �� $	��250��6A   $ 
Horizontal=�6M   .+�5  :   �� $��消息体格式配置��6@   $
��Left=�6T   #��[ 6v   5  7   �� 5  6   $0��*用户ID参数放置于JSON根目录层级��6?   #��[  6�   
������� 6\   �� *+)RestCommModel.operator_id_outside_of_args+6�     5   6   $: 4[interactive_config]restserver_operator_id_root_path��6K   5!  3   �� 5)  2   y�6=   �� $ OnlyOneRestServerRadioBtn��$ 	IsChecked�# [ .+�5*  6   �� $ 
Horizontal=�6A   .+�5+  :   �� $��Rest地址区分宏��6@   $	��150��6T   $
��Left=�6a   #��[ 6�   �� 5,  :   $	��350��6s   #��[ 6�   ��6B   �� *!RestCommModel.related_cid_macro+6�   5-  7   �� 5.  6   $ Vertical=�6C   .+�5/  :   �� $ 
Horizontal=�6E   .+�50  >   �� $��Rest地址列表��6D   $	��150��6W   $
��Left=�6c   $
��Left=�6�   #��[ 6�   �� 51  >   $��添加��6E   $	��100��52  :   $��25��6F   #��[ 6S   $�� ��53  b   ��69   �� *AddNewRestUrlCommand+6x   �� 54  >   $ ��根据Crane ID快速添加��6E   $	��180��56  9   $��25��6E   $��10q�6Q   #��[ 6]   $�� ��57  m   y�55  9   �� *	MqRpcType+# [ ��57  9   �� *!QuickAddRestUrlByCraneIDCommand+6�   �� 58  >   $ ��根据Block ID快速添加��6E   $	��180��5:  9   $��25��6E   $��10q�6R   #��[ 6_   $�� ��5;  m   y�59  9   �� *	MqRpcType+# [ ��5;  9   �� *!QuickAddRestUrlByBlockIDCommand+6�   5<  ;   �� 5=  :   $ 
Horizontal=�6E   $��5q�6^   y�5>  1   �� *ShowRpcQueuesEditOrAdd+# [ .+�5?  >   �� $��ID��6D   $
��Left=�6R   $
��Left=�6t   #��[ 6�   �� 5@  >   $
��Left=�6m   $
��Left=�6�   #��[ 6�   ��6D   �� *MqRpcTypeHeaderDesc+6�   �� 5A  >   $	��100��6e   $
��Left=�6q   $
��Left=�6�   #��[ 6�   ��6F   �� *MqRpcQueueKey+6�   �� 5B  >   $��Rest请求地址��6D   $
��Left=�6X   $
��Left=�6z   #��[ 6�   �� 5D  >   $	��300��6f   $
��Left=�6r   $
��Left=�6�   #��[ 6�   ��6F   �� *MqRpcQueueName+6�   �� 5E  >   $��确认添加��6E   $	��100��6U   $��25��6a   #��[ 6n   $�� ��5F  I   ��6   �� * ConfirmAddOrEditRestUrlCommand+6_   5G  ;     5H  :   $ * 5I     #��[ 5J     $)   HorizontalScrollBarVisibility$
) Auto=�6*   $	��250��6O   ��5H  D   �� *
RestUrlLst+$ TwoWay=�$ PropertyChanged=� 5K  >     5L  B   $  操作��6Y   $	! 120 6e   " 5M  F   �� 5N  J   .R�5O  N   �� $ 
Horizontal=�6Y   .+�5P  R   �� $��编辑��6Y   $��60��6h   #��[ 6s   ��6�   �� *!DataContext.EditRpcQUeueCommand+# �� $$ FindAncestor=�$% DataGridL���5Q     �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem��� 5S  R   $��删除��6Y   $��60��6h   #��[ 6s   ��6�   �� *#!DataContext.DeleteRpcQUeueCommand+# �� $$ FindAncestor=�$% DataGridL���5T     �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem�5U  O   5V  K   5X  C     5Y  B   $! * 6Y   & 6c   �� *key+' 5Z  F   �� 5[  J   .R�5\  N   �� ��6X   �� *!DataContext.MqRpcTypeHeaderDesc+$ TwoWay=�$ PropertyChanged=�# �� $% DataGridL�5^  K   ( 5`  F   �� 5a  J   .�5b  N   �� 
 � ��$ Center=�6�   5c  K   5e  C     5f  B   $  
Rest地址��6U   $! * 6e   & 6o   �� *RestServerUrl+( 5g  F   �� 5h  J   .�5i  N   �� 
 � ��$ Center=�6�   5j  K   5l  C   5n  ;   5o  7   5p  3   5r  /   5s  +   5t  '   5u  #   �� 5�  "   $��交互请求返回值��6+   
������ #��[ 6V   y�5�  +   �� $ RedisCommTypeRadioBtn��$ 	IsChecked�# [ .��5�  &   �� -   5        * ��Name$* requestReturnPanel��5�  1   $ Vertical=�6M   $��	50 5 50 5q�6d   .+�5�  *   �� $ 
Horizontal=�65   $��	0 10 0 10q�6N   .+�5�  .   �� $ 
Horizontal=�69   $��5 0 0 0q�6R   $	��150��6c   .+�5�  2   �� $��请求返回值类型��6=   #��[ 6O   6u     5�  2   $- '[interactive_config]request_return_type��6G   5�  /   �� 5�  .   $ 
Horizontal=�69   $��Center=�6R   .+�5�  2   	� -   5        $ globalReturnRadioBtn��5�  >   #���  6�   $4��.所有接口共用同一套返回值(通用）��6   
�������$��5,0,20,0q�6.  {�6]   �� *
IsShowItem+ 6~   �� *IsCommReturnVal+$ TwoWay=�$ PropertyChanged=�6.  	� 5�  2   -   5        $ specialReturnRadioBtn��5�  >   #���  6�   $'��!每个接口单独定义返回值��6  
�������$��5,0,15,0q�6-  {�6^   �� *
IsShowItem+ 6   �� *IsSpecialReturnVal+$ TwoWay=�$ PropertyChanged=�6-  5�  /   5�  +   �� 5�  *   -   5        $* SpecialReturnValPanel��5�  6   $ Vertical=�6W   y�5�     �� $ globalReturnRadioBtn��$ 	IsChecked�# [ .+�5�  .   �� $ 
Horizontal=�69   .+�5�  2   �� $��通用返回值定义��68   $
��Left=�6K   $
��Left=�6m   #��[ 6�   �� 5�  2   $	����69   $��25��6M    ! ImageButton#��[! 6Z   $�� ��5�  G   ��6   �� *AddNewCommonReturnValCommand+6]   5�  /   �� 5�  .   $ 
Horizontal=�69   y�5�  1   �� *ShowCommonValEditOrAdd+# [ .+�5�  2   �� $��	返回值��68   $
��Left=�6G   $
��Left=�6i   #��[ 6�   �� 5�  2   $	��150��6W   $
��Left=�6c   $
��Left=�6�   #��[ 6�   ��6:   �� *ReturnValue+6�   �� 5�  2   $��请求成功返回值��68   $
��Left=�6K   $
��Left=�6m   #��[ 6�   �� 5�  2   $
��Left=�6i   $
��Left=�6�   #��[  6�    6<   �� *ReturnValueSuccessFlag+6�   �� 5�  2   $��中文描述��68   $
��Left=�6H   $
��Left=�6j   #��[ 6�   �� 5�  2   $	��150��6]   $
��Left=�6i   $
��Left=�6�   #��[ 6�   ��6:   �� *ReturnValueDescZh+6�   �� 5�  2   $��英文描述��68   $
��Left=�6H   $
��Left=�6j   #��[ 6�   �� 5�  2   $	��150��6]   $
��Left=�6i   $
��Left=�6�   #��[ 6�   ��6:   �� *ReturnValueDescEn+6�   �� 5�  2   $��确认��69   $	��100��6G   $��25��6S   #��[ 6`   $�� ��5�  K   ��6   �� *" ConfirmAddOrEditReturnValCommand+6a   5�  /   5�  +     5�  *   $ * 5�  _   $
��auto��6p   #��[ 5�  +   $��Stretch=�6S   $) Visible=�6q   y�5�  4   �� $ globalReturnRadioBtn��$ 	IsChecked�# [ ��5�  -   �� $ CommonReturnValueLst� 5�  .     5�  2   $  操作��6I   $	! 120 6U   " 5�  6   �� 5�  :   .R�5�  >   �� $ 
Horizontal=�6I   .+�5�  B   �� $��编辑��6I   $��60��6V   #��[ 6a   ��6�   �� *(&DataContext.EditCommonReturnValCommand+# �� $$ FindAncestor=�$% DataGridL���5�  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem��� 5�  B   $��删除��6I   $��60��6V   #��[ 6a   ��6�   �� **(DataContext.DeleteCommonReturnValCommand+# �� $$ FindAncestor=�$% DataGridL���5�  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem�5�  ?   5�  ;   5�  3     5�  2   $  	返回值��6F   $! 50 6|   & 6U   �� $ return_value�( 5�  6   �� 5�  :   .�5�  >   �� 
 � ��$ Center=�6u   �� 5�  >   
 � ��$ Center=�6s   5�  ;   5�  3     5�  2   $  表示请求成功��6E   $	! 100 6�   & 6V   �� *
is_success+# [
 ( 5�  6   �� 5�  :   .�5�  >   �� 
 � ��$ Center=�6u   �� 5�  >   
 � ��$ Center=�6s   5�  ;   5   3     5  2   $  中文描述��6E    "" DataGridTextColumnElementStyle#( [" 6|   $! * 6�   & 6T   �� $ return_desc_zh�5  3     5  2   $  英文描述��6E   #( [" 6{   $! * 6�   & 6S   �� $ return_desc_en�5  3   5  +   5	  '   5  #   5     5     5     5     �� 5     $ 
Horizontal=�6   $��Center=�66   $��10q�6S   .+�5     �� $��保存基础信息��6   #��[ 6V   $	��150��6|   $��40��6�   ��6.   �� *SaveBaseInfoCommand+6�   5     �� 5     y�6   �� *ShowRpcAndFanoutInfo+# [ .+�5        �#393939$- '交互接口（修改后自动保存）��6�   
�������y�6!   �� *
IsShowItem+# [ 6�   �� 5     y�6!   �� $ RedisCommTypeRadioBtn��$ 	IsChecked�# [ .+�5     9� -   5        $ RpcRequestExpander��5  $   $(��"发送请求接口(点击展开）��6@   
�������$��5q�5  P   $��Stretch=�6[   #��[ 5  !   $��1q�6Y    "# Content.Expander.Header.Border#��[# 5      y�5  V   �� *
IsShowItem+# [ .��5     �� .��5  "   �� #��[ 6)   $��1q�6`   $ 5q�6t   ._�5  &   �� $ Vertical=�61   .+�5  *   ]� .+�5   .   �� $��接口列表��64   #��[ 6C   �� 5!  .   $	����65   + � ToolTip$	+ Add��6H   #��[! 5#  )   ��5"  )   �� *AddNewRpcCommand+5#  )   +   %DataViewConfig.Controls.SearchTextBox  5'  .   $��Right=�6E   $��Right=�6]   $	��300��6y   ,   
PreviewTxt$, 输入接口名称/描述��6�   $��0 0 10 0q�6�   -   
BtnCommand- 6�   �� *QueryRpcCommand+6�   �� 5(  .   5)  +     5*  *   -   5        .  Name$. requestDgrid��5*  3   $��5q�6I   $	��300��6T   #��[ 6a   $��Center=�5+  S   ��6+   �� *RpcInterfaceLst+/ � ContextMenu/ 5,  .   �� 5-  2   -   5        $��
RpcLstMenu��5-  >   0 �� 	StaysOpen0 . 1 �� PlacementTarget1 6c   �� $ requestDgrid��$ DataContext�.@�5.  6   m� $��复制��6?   2 m� Command2 6K   �� *CopyRpcInterfaceCommand+3 m� CommandParameter3 5/  7   �� # �� $$ FindAncestor=�$% ContextMenuL�$ PlacementTarget�6�   50  3    52  .     53  2   $  操作��6I   $	! 120 6U   " 54  6   �� 55  :   .R�56  >   �� $ 
Horizontal=�6I   .+�57  B   �� $��编辑��6I   $��60��6V   #��[ 6a   ��6�   �� *DataContext.EditRpcCommand+# �� $$ FindAncestor=�$% DataGridL���58  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem��� 59  B   $��删除��6I   $��60��6V   #��[ 6a   ��6�   �� *DataContext.DeleteRpcCommand+# �� $$ FindAncestor=�$% DataGridL���5:  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem�5;  ?   5<  ;   5>  3     5?  2   $  序号��6E   $! 50 6h   & 6Q   �� *Id+( 5@  6   �� 5A  :   .�5B  >   �� 
 � ��$ Center=�6s   5C  ;   5E  3     5F  2   $  接口名称��6E   #( [" 6{   $! * 6�   & 6S   �� *RequestInternalName+5H  3     5I  2   $  接口描述��6E   #( [" 6s   $! * 6�   & 6S   �� *RequestDesc+5K  3     5M  2   $  	msg__type��5N  )   $! * 6Y   4   
Visibility4 5M  F   �� *Value+5 �� Source#5 [ & 5N  <   �� *	MsgType+( 5O  6   �� 5P  :   .�5Q  >   �� 
 � ��$ Center=�6s   5R  ;   5T  3     5U  2   $  事件ID点(event)��5V  )   $! * 6w   4 5U  E   �� *Value+#5 [ & 5V  ?   �� *$"DestTagName.args_tag_internal_name+( 5W  6   �� 5X  :   .�5Y  >   �� 
 � ��$ Center=�6s   5Z  ;   5\  3     5]  2   $  参数点(args)��5^  9   $! * 6�   4 5]  E   �� *Value+#5 [ & 5^  L   �� *%#DestTagName.event_tag_internal_name+( 5_  6   �� 5`  :   .�5a  >   �� 
 � ��$ Center=�6s   5b  ;   5d  3     5e  2   $  反馈点(return)��5f  9   $! * 6�   4 5e  E   �� *Value+#5 [ & 5f  N   �� *&$DestTagName.return_tag_internal_name+( 5g  6   �� 5h  :   .�5i  >   �� 
 � ��$ Center=�6s   5j  ;   5l  3     5m  2   $  	请求点��5n  9   $! * 6�   4 5m  E   �� *Value+#5 [ & 5n  F   �� *'%DestTagName.request_tag_internal_name+( 5o  6   �� 5p  :   .�5q  >   �� 
 � ��$ Center=�6s   5r  ;   5t  3     5u  2   $  	反馈点��5v  9   $! * 6�   4 5u  E   �� *Value+#5 [ & 5v  F   �� *(&DestTagName.feedback_tag_internal_name+( 5w  6   �� 5x  :   .�5y  >   �� 
 � ��$ Center=�6s   5z  ;   5|  3     5}  2   $  反馈点类型��5~  9   $! * 6u   4 5}  E   �� *Value+#5 [ & 5~  H   �� *DestTagName.return_type+( 5  6   �� 5�  :   .�5�  >   �� 
 � ��$ Center=�6s   5�  ;   5�  3   5�  +   5�  '   5�  #   5�     5�     5�     �� 5�     y�6"   �� $ MQCommTypeRadioBtn��$ 	IsChecked�# [ .+�5�     9� -   5        $ FanoutExpander��5�  #   
�������$��5q�5�  /   #��[ 5�     $��1q�6U   #��[# 5�      ��5�  ]   �� *	ECSComm+# [ y�5�     �� *
IsShowItem+# [ .��5�     �� .��5�  "   �� #��[ 6)   $��1q�6`   $ 5q�6t   ._�5�  &   �� $ Vertical=�61   .+�5�  *   ]� .+�5�  .   �� $��接口列表��64   #��[ 6C   �� 5�  .   $	����65   #��[! 5�  -   ��5�  H   �� *AddNewFanoutCommand+5�  -     5�  .   $��Right=�6E   $��Right=�6\   $	��300��6x   $, 输入接口名称/描述��6�   $��0 0 10 0q�6�   - 6�   �� *QueryFanoutCommand+6�   �� 5�  .   5�  +     5�  *   -   5        $. requestDgrid1��5�  3   $	��300��6K   $��5q�6X   #��[ 6�   ��6d   �� *ReceiveFanoutLst+$ TwoWay=�  5�  .    ($ $Content.DataGrid.Selected.BackGround��%=  '        7 $ �� 5�  2   "	  68    5�  6   ���� 5�  :   $ White��6�   (��       6J   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6�   5�  7    5�  6   P� 5�  :    �@ 
IsSelected . . �5�  >   �� 
 � ��8   6[   �� 5�  >   
 � ��
 ���� 6\   5�  ;   5�  3   5�  /    5�  .     5�  2   $  操作��6I   $	! 120 6U   " 5�  6   �� 5�  :   .R�5�  >   �� $ 
Horizontal=�6I   .+�5�  B   �� $��编辑��6I   $��60��6W   #��[ 6b   ��6�   �� *DataContext.EditFanoutCommand+# �� $$ FindAncestor=�$% DataGridL���5�  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem��� 5�  B   $��删除��6I   $��60��6W   #��[ 6b   ��6�   �� *!DataContext.DeleteFanoutCommand+# �� $$ FindAncestor=�$% DataGridL���5�  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem�5�  ?   5�  ;   5�  3     5�  2   $  序号��6E   $! 50 6h   & 6Q   �� *Id+( 5�  6   �� 5�  :   .�5�  >   �� 
 � ��$
 Left=�6u   �� 5�  >   
 � ��$ Center=�6s   5�  ;   5�  3     5�  2   $  接口名称��6E   $! * 6s   & 6S   �� *
FanoutName+( 5�  6   �� 5�  :   .�5�  >   �� 
 � ��$
 Left=�6u   �� 5�  >   
 � ��$ Center=�6s   5�  ;   5�  3     5�  2   $  描述��6E   #( [" 6q   $! * 6�   & 6Q   �� *
FanoutDesc+5�  3     5�  2   $  msg_type��6E   $! * 6t   & 6W   �� *	MsgType+4 6~   �� *Value+#5 [ ( 5�  6   �� 5�  :   .�5�  >   �� 
 � ��$
 Left=�6u   �� 5�  >   
 � ��$ Center=�6s   5�  ;   5�  3     5�  2   $"  设备/操作台区分字段��6E   $! * 5�  -   & 5�  Y   �� *DeviceField+4 6y   �� *Value+#5 [ ( 5�  6   �� 5�  :   .�5�  >   �� 
 � ��$
 Left=�6u   �� 5�  >   
 � ��$ Center=�6s   5�  ;   5�  3     5�  2   $  接收目标类型��6E   $! * 6v   & 6U   �� *ReceiveType+4 6�   �� *Value+#5 [ ( 5�  6   �� 5�  :   .�5�  >   �� 
 � ��$
 Left=�6u   �� 5�  >   
 � ��$ Center=�6s   5�  ;   5�  3   5.  +   5/  '   50  #   51     53     55     �� 56     y�6!   �� $ RedisCommTypeRadioBtn��$ 	IsChecked�# [ .+�57     9� -   5        $ DefaultValueExpander��57  #   $+��%控件默认值配置(点击展开）��6c   
�������$��5q�58  }   #��[ 59     $��1q�6U   #��[# 5:      y�58     �� *
IsShowItem+# [ .��5;     �� .��5<  "   �� #��[ 6)   $��1q�6`   $ 5q�6t   ._�5=  &   �� $ Vertical=�61   .+�5>  *   ]� .+�5?  .   �� $��控件默认值配置��64   #��[ 6F   �� 5@  .   $	����65   #��[! 5A  -   ��5@  H   �� *AddNewControlDefaultCommand+5A  -     5F  .   $��Right=�6E   $��Right=�6\   $	��300��6x   $, 输入控件名称/描述��6�   $��0 0 10 0q�6�   - 6�   �� *QueryDefaultValueCommand+6�   �� 5G  .   5H  +     5I  *   -   5        $. requestDgrid12��5I  3   $��5q�6K   $	��300��6W   #��[ 5J  %   ��5I  d   �� *ControlDefaultValLst+ 5]  .     5^  2   $  操作��6I   $	! 120 6V   " 5_  6   �� 5`  :   .R�5a  >   �� $ 
Horizontal=�6I   .+�5b  B   �� $��编辑��6I   $��60��6W   #��[ 6b   ��6�   �� *'%DataContext.EditControlDefaultCommand+# �� $$ FindAncestor=�$% DataGridL���5c  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem��� 5d  B   $��删除��6I   $��60��6V   #��[ 6a   ��6�   �� *)'DataContext.DeleteControlDefaultCommand+# �� $$ FindAncestor=�$% DataGridL���5e  -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem�5f  ?   5g  ;   5i  3     5q  2   $  控件名称（内部）��6E   $! * 6W   & 6a   �� *control_internal_name+( 5r  6   �� 5s  :   .�5t  >   �� 
 � ��$ Center=�6s   5u  ;   5w  3     5x  2   $  填充类型��6E   $! * 6U   & 6`   �� *	fill_type+( 5y  6   �� 5z  :   .�5{  >   �� 
 � ��$ Center=�6s   5|  ;   5~  3   5�  +   5�  '   5�  #   5�     5�     5�     5�     5�     5�     5�     5�     