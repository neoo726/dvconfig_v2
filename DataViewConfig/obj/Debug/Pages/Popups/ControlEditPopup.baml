   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   /clr-namespace:DataViewConfigDataViewConfig  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   2    ,DataViewConfig.Pages.Popups.ControlEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   0currentclr-namespace:DataViewConfig     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  ControlEditPopupWin��5      $	��350��5   	   $	��350��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.    � Title$ 控件编辑��6E      NormalBackGround#��[  5   	   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type,	  &DataView_Configuration.ParamTargetType 	 paramSourceEnum/
  )DataView_Configuration.RequestParamSource��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �       5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5   
   $ 	GetValues��64   "  6K    5      M� 5       " 	 6   5"      S� 5#   
   $ 	GetValues��65   "  6L    5$      M� 5%      " 
 6   5'      5)      .��5*      � ���#1f1f1f	 � RowDefinitions	 5+   
   �� 5-      �� 5.      $��80 �6   .+�56   
   �� $��0��6   $��10q�6#   
 �� Orientation$
 Vertical=�6/   .+�57      �� $
 
Horizontal=�6   $��5q�62   .+�58      �� $��控件名称（内部）��6   $	��150��6,   $
��Left=�68    
 NormalLabel#��[
 6Z   �� 59      $	��150��6B     NormalTextBox#��[ 6N   ��6   �� *ControlInternalName+6N   5:      �� 5;      $
 
Horizontal=�6   $��5q�62   .+�5<      �� $��AccessID��6   $	��150��6-   $
��Left=�69   #��[
 6[   �� 5=      $	��150��6>   #��[ 6J   ��6   �� *ControlAccessID+6J   5>      �� 5?      $
 
Horizontal=�6   $��5q�62   .+�5@      �� $��控件描述��6   $	��150��6)   $
��Left=�65   #��[
 6W   �� 5A      $	��150��6:   #��[ 6F   ��6   �� *ControlDesc+6F   5B      �� 5C      $
 
Horizontal=�6   $��5q�62   .+�5D      �� $��控件类型��6   $	��150��6)   $
��Left=�65   #��[
 6W   �� 5E        NormalCombox#���  6   $	��150��6B    �� DisplayMemberPath$ Value��5G      ��5F      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:ControlType++ �� SelectedValuePath 5G   1   �� *CurControlType+ �� Text 5H      �� *CurControlType+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5I      �� 5J      $
 
Horizontal=�6   $��5q�62   .+�5K      �� $��所属画面(内部）��6   $
��Left=�6,   $	��150��6O   #��[
 6[   �� 5L      #���  6   $	��150��6B    �� 
IsEditable .  $ dv_screen_internal_name��5N      ��5M      �� *ScreenNameLst+ 5O      �� *CurScreenInternalName+$ TwoWay=�5P      5R      �� 5S   
   $
 
Horizontal=�6   $��Right=�6/   $��1��6K   .+�5T      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5U      �� *ConfirmCommand+��69   ��  �� ElementName$ ControlEditPopupWin��6x   �� 5V      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5W      �� *CancelCommand+��69   �� $ ControlEditPopupWin��6x   5X      5Y      5Z      