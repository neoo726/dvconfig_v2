   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   /clr-namespace:DataViewConfigDataViewConfig  5	   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5
   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   1    +DataViewConfig.Pages.Popups.ScreenEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   0currentclr-namespace:DataViewConfig     5	   	   :controls%clr-namespace:DataViewConfig.Controls     5
   
   =	converter'clr-namespace:DataViewConfig.Converters     5   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  ScreenEditPopupWin��5      $	��250��5   
   $	��680��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6/    � Title$ 画面编辑��6E      NormalBackGround#��[  5   	    � Icon$ /Images/icon.png��69    � SizeToContent$ WidthAndHeight=�6R   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� Type,	  &DataView_Configuration.ParamTargetType 	 paramSourceEnum/
  )DataView_Configuration.RequestParamSource��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �       5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5   
   $ 	GetValues��64   "  6K   	 5      M� 5       "
 	 6   5"      S� 5#   
   $ 	GetValues��65   "  6L   	 5$      M� 5%      "
 
 6   5'      5)      .��5*      � ���#1f1f1f � RowDefinitions 5+   
   �� 5-      �� 5.      $��80 �6   .+�56   
   �� $��0��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�57      �� $ 
Horizontal=�6   $��5q�62   .+�58      �� $��画面ID��6   {�.  $	��150��6:   $
��Left=�6F    
 NormalLabel#��[
 6h   �� 59      {�.  $	��200��6H     NormalTextBox#��[ 6T   ��6   �� *
ScreenID+6T   5:      �� 5;      $ 
Horizontal=�6   $��5q�62   .+�5<      �� $ 
Horizontal=�6   $	��150��66   .+�5=      �� $��画面名称（内部)��6   $
��Left=�60   #��[
 6R   '   !DataViewConfig.Controls.TipsLabel  5>         TipName$. ([screen_config_page]screen_internal_name��6+   5?      �� 5@      $	��200��6?   #��[ 6K   ��6   �� *SreenInternalName+6K   5B      �� 5C      $ 
Horizontal=�6   $��5q�62   .+�5D      �� $ 
Horizontal=�6   $	��150��67   .+�5E      �� $��画面文件名(.csw)��6   $
��Left=�63   #��[
 6U     5F      $) #[screen_config_page]screen_csw_name��6+   5G      �� 5H      $	��450��6:   #��[ 6I   ��6   �� *ScreenCswName+6I   �� 5I      $��1��6   $��2��6&   $	����67     ImageButton#���  6J   $��35��6p   $��Center=�6{   $��Right=�6�   ��5J      �� *SelectCswCommand+5K      �� 5L      $ 
Horizontal=�6   $��5q�62   .+�5M      �� $��画面描述��6   $	��150��6)   $
��Left=�65   #��[
 6W   �� 5N      $	��400��67   #��[ 6C   ��6   �� *
ScreenDesc+6C   5O      5P      �� 5Q   
   $ 
Horizontal=�6   $��Right=�6/   $��1��6K   .+�5R      �� $��确认��6   $	��150��6#   $��35��6/   $��10q�6;     NormalButton#��[ 6G   ��5S      �� *ConfirmCommand+��69   ��  �� ElementName$ ScreenEditPopupWin��6w   �� 5T      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5U      �� *CancelCommand+��69   �� $ ScreenEditPopupWin��6w   5V      5W      5X      