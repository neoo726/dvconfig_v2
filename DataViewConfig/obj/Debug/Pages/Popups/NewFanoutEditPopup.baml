   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   /clr-namespace:DataViewConfigDataViewConfig  5      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   4    .DataViewConfig.Pages.Popups.NewFanoutEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   0currentclr-namespace:DataViewConfig     5      6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   -   5          �Name$  NewFanoutEditPopupWin��5      $	��550��5   	   $	��950��6   
������� � WindowStartupLocation$ CenterScreen=�5   	      NormalBackGround#��[  5   	     NormalFontSize#��[ 68    � 
ResizeMode$ NoResize=�5   	    � Title$2 ,状态接口编辑(Fanout广播/Redis轮询)��6    � SizeToContent$ WidthAndHeight=�6A   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibilityF  @DataViewConfig.Converters.EcsComm_Int2FanoutTitleStringConverter  EcsCommTypeInt2FanoutTitleA  ;DataViewConfig.Converters.ReceiveWrite_Normal2BoolConverter  WriteNormarlTag2BoolD  >DataViewConfig.Converters.ReceiveWrite_Condition2BoolConverter  WriteConditionTag2BoolG  ADataViewConfig.Converters.ReceiveWrite_Normal2VisibilityConverter  WriteNormarlTag2VisibilityJ  DDataViewConfig.Converters.ReceiveWrite_Condition2VisibilityConverter   WriteConditionTag2Visibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter 	 EcsCommTypeMQ2Bool9	  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter 
 EcsCommTypeOPC2Bool:
  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters	 M� Type,  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource'  !System.Windows.Shell.WindowChrome  WIndowChromeKey
   ResizeBorderThickness   CaptionHeight��%~  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 �     &	 ?    &	 �      5   
   6-     5   
   64     5   
   6;     5   
   66     5   
   69     5   
   6<     5   
   6?     5    
   6-   	  5!   
   6.   
  5"   
   6/     5#   
   68     5$   
   64   S� 5%   
   $ 	GetValues��64   "  6K    5&      M� 5'      "	  6   5)      S� 5*   
   $ 	GetValues��65   "  6L    5+      M� 5,      "	  6   5.        5/   
   
 50      u�51      5q�6   $ 0Q�53   G   54      55      .��56      � -   5         �Name$
 grid��56      ���#1f1f1f � RowDefinitions 57   
   �� 59      �� 5:      $��80 �6   .+�5B   
   �� $��0��6   $��5q�6#    �� Orientation$ Vertical=�6.   .+�5C      �� $ 
Horizontal=�6   $��5q�63   .+�5D      �� $��接口名称��6   $��5,0,5,0q�6(   $	��150��69     
NormalText#��[ 6F   �� 5E        NormalTextBox#��[ 6   $	��250��6]   $��5,0,30,0q�6i   ��6A   �� *
FanoutName+6i   5F      �� 5G      $ 
Horizontal=�6   $��5q�63   .+�5H      �� $��描述��6   $��5,0,5,0q�6&   $	��150��67   #��[ 6D   �� 5I      #��[ 6   $	��250��6]   $��5,0,30,0q�6i   ��6A   �� *
FanoutDesc+6i   5J      �� 5K      $ 
Horizontal=�6   $��5q�63   y�6>   �� *ShowRedisConfigs+ �� 	Converter# [ .+�5L      �� $��	Redis Key��6   $��5,0,5,0q�6-   $	��150��6>   #��[ 6K   �� 5M      #��[ 6   $	��250��6[   $��5,0,30,0q�6g   ��6A   �� *
RedisKey+6g   5N      �� 5S      $ 
Horizontal=�6   $��5q�62   $	��Top=�6=   y�6U   �� *ShowRedisConfigs+# [ .+�5T      �� $��JSON解析配置��6   $	��150��6-   $	��Top=�6:   $��5,10,5,0q�6R   #��[ 6d   �� 5U      $ Vertical=�6   .+�5V      �� $ 
Horizontal=�6!   $��5q�6;   .+�5W      �� $ 
Horizontal=�6%   $	��180��6?   $��5,0,5,0q�6L   .+�5X      �� $��msg_type��6(   #��[ 6:   '   !DataViewConfig.Controls.TipsLabel  5Y         TipName$ [fanout_edit]msg_type��63   5Z      �� 5[      #��[ 6"   $	��200��6b   $��5,0,5,0q�6o   ��6I   �� *	MsgType+6o   5\      �� 5]      $ 
Horizontal=�6!   $��5q�6;   .+�5^      �� $ 
Horizontal=�6%   $	��180��6?   $��5,0,5,0q�6L   .+�5_      �� $��对象类型��6(   #��[ 65     5`      $" [fanout_edit]msg_target_type��63   5a      �� 5b        NormalCombox#���  6#   $	��200��6J   $��5,0,50,0q�6V    �� DisplayMemberPath$ Value��5d      ��5c      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:DvReceiveTypeEnum++ �� Text 5d   2   �� *StrReceiveType+5e      �� 5f      $ 
Horizontal=�6!   $��5q�6;   .+�5g      �� $ 
Horizontal=�6%   $	��180��6>   $��5,0,5,0q�6K   .+�5h      �� $"��设备/操作台区分字段��6(   #��[ 6<     5i      $0 *[fanout_edit]crane_rcs_identify_filed_name��63   5j      �� 5k      #��[ 6"   $	��200��6f   $��5,0,5,0q�6s   ��6I   �� *DeviceField+6s   5l      5m      5n      �� 5p      $ 
Horizontal=�6   $��5q�62   y�6>   �� *ShowRedisConfigs+# [ .+�5q      �� $ 
Horizontal=�6   .+�5r      �� $��写入点名��6    $��5q�6-   $	��Top=�6:   #��[ 6T     5s      $# [fanout_edit]direct_write_tag��6+   5t      �� 5u      -   5         ��Name$! WriteMemTagSelectionCombox1��5u      $	��250��6A   $��5,0,50,0q�6M   #��[ 6_    �� 
IsEditable .  �� IsTextSearchEnabled .   �� IsTextSearchCaseSensitive .  w� TextPath$ tag_internal_name��5w   :   
�������
���   ���5v      �� *TagLst+ �� SelectedItem 5w      �� *
WriteTag+��5x      �� 5y      .R�5z      �� .+�5{   "   �� $��0 3 0 5q�6,   $
��Bold"�6`   ��6=   �� *tag_internal_name+6`   �� 5|   "     Global.Gray#��[ 6,   ��6V   �� *
tag_desc+5}      5~      5�      5�      �� 5�      $ 
Horizontal=�6   $��5q�62   y�6=   �� *ShowRedisConfigs+# [ .+�5�      �� $��数据存储配置��6   $��5,0,5,0q�6+   $	��150��6=   $	��Top=�6J   #��[ 6b   �� 5�      $ Vertical=�6   $	��Top=�64   .+�5�      �� $ 
Horizontal=�6!   $��5q�6:   .+�5�      	� -   5         	�Name$ directWriteTagRadioBtn��5�   &   $��直接写入Memory点��5�   "     NormalRadioButton#��[ 69    d� 	IsChecked 5�   "   �� *ReceiveWriteType+# [ 5�   9     5�      $* $[fanout_edit]direct_write_memory_tag��6/   	� 5�      -   5        $  dependsOnCondiitonRadioBtn��5�   &   $0��*根据不同条件写入不同的Memory点��5�      #��[ 6=    5�   #   �� *ReceiveWriteType+# [ 5�   =   5�      �� 5�      $ Horizontal =�6!   $��5q�6;   y�6G   �� *ReceiveWriteType+# [ .+�5�      �� $ 
Horizontal=�6%   .+�5�      �� $��写入点名��6(   $��5q�65   $	��Top=�6B   #��[ 6\     5�      $# [fanout_edit]direct_write_tag��63   5�      �� 5�      -   5        $  WriteMemTagSelectionCombox��5�   #   $	��250��6H   $��5,0,50,0q�6T   #��[ 6f    .  .  . $ tag_internal_name��5�   :   
�������
���   ���5�      �� *TagLst+ 5�      �� *
WriteTag+��5�      �� 5�   "   .R�5�   &   �� .+�5�   *   �� $��0 3 0 5q�64   $
��Bold"�6h   ��6E   �� *tag_internal_name+6h   �� 5�   *   #��[ 64   ��6^   �� *
tag_desc+5�   '   5�   #   5�      5�      �� 5�      $ Vertical=�6!   y�69   �� *ReceiveWriteType+# [ .+�5�      �� $ 
Horizontal=�6%   .+�5�      �� $��+��6%     NormalButton#��[ 6]   
�������$��50��6�   ��61   �� *AddNewConditionCommand+6�   �� 5�      $��添加条件��6$     NormalLabel#��[ 64   5�      �� 5�      $ Vertical=�6%   .+�5�      �� -   5         ��Name$ conditionItems��5�   +   ��6C   �� *ConditionMatchTagLst+ �� NotifyOnSourceUpdated . ��5�   "   �� 5�   &   .R�5�   *   �� $ 
Horizontal=�65   .+�5�   .   �� $��移除��65   $��50��5�   -   #��[ 68   ��5�   B   ��  �� RelativeSource ��  �� AncestorType$ ItemsControlL�  �� Path$(  "DataContext.DeleteConditionCommand���5�   -   �� 5�   8   #  HandyControl.Controls.TextBox  5�   .   
�������(  "HandyControl.Controls.TitleElement!   Title$! 	字段名��5�   9   "   TitlePlacement$
" Left=�6U   $	��200��6{   
�������
������ $��30��5�   e   ��5�   9   �� *	FieldName+5�   e     5�   .   
�������$��5 0 0 0q�6j   $! 	条件值��5�   9   $
" Left=�6U   $	��150��6{   
�������
������ $��30��5�   f   ��5�   9   �� *
FieldValue+5�   f   �� 5�   .   $ Horizontal =�69   $��5q�6S   .+�5�   2   �� $��写入点名��6<   $��5q�6I   $��Center=�6V   #��[ 6s     5�   2   $&  [fanout_edit]condition_write_tag��6G   �� 5�   2   -   5        $	��250��5�   =   $��5,0,50,0q�6I   #��[ 6[    .  .  . $ tag_internal_name��5�   ;   
�������
���   ���5�      ��  �� $ ItemsControlL�$  DataContext.TagLst� 5�      �� *	TargetTag+��5�   6   �� 5�   :   .R�5�   >   �� .+�5�   B   �� $��0 3 0 5q�6L   $
��Bold"�6�   ��6]   �� *tag_internal_name+6�   �� 5�   B   #��[ 6L   ��6v   �� *
tag_desc+5�   ?   5�   ;   5�   3   5�   /   5�   +   5�   '   5�      5�      5�      5�      5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Right=�6/   $��1��6K   .+�5�      �� $��确认��6   $	��150��6#   $��35��6/   $��10q�6;   #��[ 6G   ��5�      �� *ConfirmCommand+��69   �� # �� ElementName$# NewFanoutEditPopupWin��6z   �� 5�      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5�      �� *CancelCommand+��69   �� $# NewFanoutEditPopupWin��6z   5�      5�      5�      