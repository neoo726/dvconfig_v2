   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   /clr-namespace:DataViewConfigDataViewConfig  5      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   1    +DataViewConfig.Pages.Popups.FanoutEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   0currentclr-namespace:DataViewConfig     5      6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  FanoutEditPopupWin��5      $	��550��5   	   $	��450��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.      NormalBackGround#��[  5   	     NormalFontSize#��[ 68    � 
ResizeMode$ NoResize=�5   	     WIndowChromeKey'  !System.Windows.Shell.WindowChrome   WindowChrome# �  5   	   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter 	 MqRpcMultiCidType2Bool 
 targetTypeEnum S� 
MethodName S� 
ObjectType	  System.Enum S� MethodParameters	 M� Type,
  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource
   ResizeBorderThickness   CaptionHeight��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &		 r     &	
 �     &	 �     &	 ;      5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5    
   $ 	GetValues��64   " 	 6K    5!      M� 5"      "	 
 6   5$      S� 5%   
   $ 	GetValues��65   " 	 6L    5&      M� 5'      "	  6   5)        5*   
   
 5+      u�5,      5q�6   $ 0Q�5.   G   5/      50      .��51      � -   5         �Name$
 grid��51      ���#1f1f1f � RowDefinitions 52   
   �� 53      $��30 �6   �� 54      �� 55      $��80 �6   .+�57   
   ]� $��0��6   
���   �.+�58      �� $��Stretch=�6   $
��Left=�62   $��Fanout编辑��6H     NormalLabel#��[ 6[   �� 59      $��1��6   $��Right=�6&   $��Right=�6=     
TextButton#��[ 6Y   
�������$	����5:   *   ��5;      �� *CloseCommand+��68   ��  �� ElementName$ FanoutEditPopupWin��6v   5<      �� 5=   
   $��1��6   $��5q�6#    �� Orientation$ Vertical=�6.   .+�5>      �� $ 
Horizontal=�6k   $��5q�6�   {�.  y�6   �� *
IsAddNew+ �� 	Converter# [ .+�5?      �� $��接口ID��6   $��5,0,5,0q�6(   $	��150��69     
NormalText#��[ 6F   �� 5@        NormalTextBox#��[ 6   $	��150��6[   $��5,0,50,0q�6g   ��6A   �� *
FanoutID+6g   5A      �� 5B      $ 
Horizontal=�6   $��5q�62   .+�5C      �� $��MsgType��6   $	��150��6,   $��5,0,5,0q�69   #��[ 6J   �� 5D      #��[ 6   $	��100��6Z   $��5,0,5,0q�6g   ��6A   �� *	MsgType+6g   5E      �� 5F      $ 
Horizontal=�6   $��5q�62   .+�5G      �� $��交互对象��6   $��5,0,5,0q�6)   $	��150��6;   #��[ 6H   �� 5H        NormalCombox#���  6   $	��100��6B   $��5,0,50,0q�6N   
������� �� DisplayMemberPath$ system_desc��5J       �� SelectedValuePath$ 	system_id��6c   ��5I      �� *RequestSystemLst+ �� SelectedValue 5J   9   �� *RequestSystemId+6c   5K      �� 5L      $ 
Horizontal=�6   $��5q�62   .+�5M      �� $��描述��6   $	��150��6'   $��5,0,5,0q�64   #��[ 6E   �� 5N      #��[ 6   $	��100��6]   $��5,0,5,0q�6j   ��6A   �� *
FanoutDesc+6j   5O      �� 5P      $ 
Horizontal=�6   $��5q�62   .+�5Q      �� $��接收类型��6   $��5,0,5,0q�6)   $	��150��6;   #��[ 6H   �� 5R      #���  6   $	��200��6B   $��5,0,50,0q�6N   $ Value��5T      ��5S      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:DvReceiveTypeEnum++ �� Text 5T   2   �� *ReceiveType+5U      �� 5V      $ 
Horizontal=�6   $��5q�62   .+�5W      �� $(��"设备/操作台区分字段名称��6   $	��150��61   $��5,0,5,0q�6>   #��[ 6O   �� 5X      #��[ 6   $	��100��6^   $��5,0,5,0q�6k   ��6A   �� *DeviceField+6k   5Y      �� 5Z      $ 
Horizontal=�6   $��5q�62   .+�5[      �� $��数据存储��6   $��5,0,5,0q�6)   $	��150��6;   #��[ 6I   �� 5\      -   5         ��Name$ StoreTypeCombox��5\      #���  64   $	��250��6[   $��5,0,50,0q�6g   $ Value��5^      ��5]      ��    *M� *" dv_config:DvReceiveStoreTypeEnum++ 5^   1   �� *ReceiveStoreType+6T   5_      �� 5`      -   5         ��Name$ fullStoreNamePanel��5`      $ 
Horizontal=�66   $��5q�6O   .+�5a      �� $ 
Horizontal=�6   .+�5b      �� $-��'立即写入的数组点名（内部）��6!   $	��150��67   $��5,0,5,0q�6D   #��[ 6U   6{   �� 5c      #���  6   $	��170��6F   $��5,0,50,0q�6R   ��5d      �� *FullStoreTagLst+ 6?   �� *FullStoreTagInternalName+ �� Mode$ TwoWay=�5f      5g      �� 5h      $ 
Horizontal=�6   $��5q�62   .+�5i      �� $��写入Memory点方式��6   $��5,0,5,0q�61   $	��150��6C   #��[ 6P   �� 5j      -   5        $ WriteTypeCombox��5k      #���  63   $	��200��6Z   $��5,0,50,0q�6f   $ Value��5m      ��5l      ��    *M� *" dv_config:DvReceiveWriteTypeEnum++ 5m   1   �� *ReceiveWriteType+6T   5o      �� 5p      -   5        $ cacheTagNamePanel��5p      $ 
Horizontal=�64   $��5q�6M   .+�5q      �� $��写入点名（内部）��6   $��5,0,5,0q�6.   $	��150��6@   #��[ 6M   �� 5r      #���  5s      $	��200��6B   $��5,0,50,0q�6N   ��5t      �� *
TagNameLst+ 5u      �� *CacheTagInternalName+5w      �� 5x      -   5        $ cacheConditionPanel��5x      $ 
Horizontal=�66   $��5q�6O   .+�5y      �� $��5q�6   $ Vertical=�6)   .+�5z      �� $ 
Horizontal=�6!   .+�5{      �� $��写入条件设定��6    $	��150��61   $
��Left=�6=   $
��Left=�6_   #��[ 6z   �� 5|      $��添加��6!   ���#4c4c4c$	��100��6C   $��25��6O   $��10q�6[     NormalButton#��[ 6g   $�� ��5}   A   ��6   �� *AddNewConditionCommand+6W   5~      �� 5�      .��5�      &   System.Windows.Controls.DataGrid  $	��100��6#      	RowHeight$ 25Q�5�   ,   $��Center=�6;   $��Center=�6X      AutoGenerateColumns .     CanUserAddRows .  "   VerticalScrollBarVisibility$
 Auto=�5�   c   ��5�   0   ��  �� Path$ ReturnValueLst�   Columns 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�   "   ,  &System.Windows.Controls.DataGridColumn   Header$ 操作��69       CellTemplate  5�   &   �� 5�   *   .R�5�   .   �� $ 
Horizontal=�69   .+�5�   2   �� $��删除��69   #��[ 6F   ��6j   �� *$"DataContext.DeleteReturnValCommand+! �� RelativeSource! �� " �� Mode$" FindAncestor=�# �� AncestorType$# DataGridL���5�   -   �� ! �� $" FindAncestor=�$# DataGridL�$ SelectedItem�5�   /   5�   +   5�   #   0  *System.Windows.Controls.DataGridTextColumn  5�   "   $   Width5  /System.Windows.Controls.DataGridLengthConverter$$ 50 68   $ 	条件值��6i   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6C   �� $ return_value�&   ElementStyle& 5�   &   �� 5�   *   .�5�   .   �� ' �� Property' � ��( �� Value$( Center=�6c   5�   +   5�   #     5�   "   $6 0满足该条件时要写入的点名（内部）��65   $	$ 350 6O   % 6[   �� $ return_desc�& 5�   &   �� 5�   *   .�5�   .   �� ' � ��$( Center=�6c   5�   +   5�   #   5�      5�      5�      5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Right=�6/   $��2��6K   .+�5�      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O   #��[ 6[   ��5�      �� *ConfirmCommand+��69   �� $ FanoutEditPopupWin��6w   �� 5�      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5�      �� *CancelCommand+��69   �� $ FanoutEditPopupWin��6w   5�      5�      5�      