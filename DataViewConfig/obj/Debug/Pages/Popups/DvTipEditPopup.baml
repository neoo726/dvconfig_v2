   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   /clr-namespace:DataViewConfigDataViewConfig  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   0    *DataViewConfig.Pages.Popups.DvTipEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   0currentclr-namespace:DataViewConfig     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  DvTipEditPopupWin��5      $	��200��5   	   $	��550��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6/    � SizeToContent$ WidthAndHeight=�6E    � Title$ 提示编辑��5         NormalBackGround#��[  5   	   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      .   (DataViewConfig.Converters.ImageConverter  ImageConverter8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool 	 targetTypeEnum S� 
MethodName S� 
ObjectType	  System.Enum S� MethodParameters	 M� Type,
  &DataView_Configuration.ParamTargetType 
 paramSourceEnum/  )DataView_Configuration.RequestParamSource��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �     &	
 �       5   
   6#     5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5    
   $ 	GetValues��64   " 	 6K    5!      M� 5"      "	 
 6   5$      S� 5%   
   $ 	GetValues��65   " 	 6L    5&      M� 5'      "	  6   5)      5*      .��5+      � ���#1f1f1f
 � RowDefinitions
 5,   
   �� 5-      �� 5.      $��60 �6   .+�56   
   �� $��0��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�57      �� $ 
Horizontal=�6   $��5q�62   .+�58      �� $��提示名称��6   $	��150��6(   $
��Left=�64     NormalLabel#��[ 6V   �� 59      $	��350��6?     NormalTextBox#��[ 6K   ��6   �� *TipsInternalName+6K   5:      �� 5;      $ 
Horizontal=�6   $��5q�62   .+�5<      �� $��中文提示��6   $	��150��6)   $
��Left=�66   #��[ 6X   �� 5=      $	��350��63   #��[ 6?   ��6   �� *TipsZh+6?   5>      �� 5?      $ 
Horizontal=�6   $��5q�62   .+�5@      �� $��英文提示��6   $	��150��6)   $
��Left=�66   #��[ 6X   �� 5A      $	��350��63   #��[ 6?   ��6   �� *TipsEn+6?   5B      5C      �� 5D   
   $ 
Horizontal=�6   $��Center=�6/   $��1��6L   .+�5E      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5F      �� *ConfirmCommand+��69   ��  �� ElementName$ DvTipEditPopupWin��6v   �� 5G      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5H      �� *CancelCommand+��69   �� $ DvTipEditPopupWin��6v   5I      5J      5K      