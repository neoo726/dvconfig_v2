   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   4    .DataViewConfig.Pages.Popups.CraneTypeEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  CraneTypeEditPopupWin��5      $	��350��5   	   $	��650��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � Title$ 设备机型��5   	    � 
ResizeMode$ NoResize=�5         NormalBackGround#��[  5   	    � Icon$ /Images/icon.png��68    � SizeToContent$ WidthAndHeight=�6Q   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum	 S� MethodParameters
 M� Type,	  &DataView_Configuration.ParamTargetType 	 paramSourceEnum/
  )DataView_Configuration.RequestParamSource��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �       5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5   
   $ 	GetValues��64   "  6K   	 5       M� 5!      "
 	 6   5#      S� 5$   
   $ 	GetValues��65   "  6L   	 5%      M� 5&      "
 
 6   5(      5*      .��5+      � ���#1f1f1f � RowDefinitions 5,   
   �� 5.      �� 5/      $��80 �6   .+�57   
   �� $��0��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�58      �� $ 
Horizontal=�6   $��5q�62   .+�59      �� $��ID��6   {�.  $	��100��68   $
��Left=�6D    
 NormalLabel#��[
 6f   �� 5:      {�.  $	��150��6K     NormalTextBox#��[ 6W   ��6   �� *CraneTypeID+6W   5;      �� 5<      $ 
Horizontal=�6   $��5q�62   .+�5=      �� $��机型名称��6   $	��100��6)   $
��Left=�65   #��[
 6W   �� 5>      $	��150��6:   #��[ 6F   ��6   �� *CraneTypeName+6F   5?      �� 5@      $ 
Horizontal=�6   $��5q�62   .+�5A      �� $��机型描述��6   $	��100��6)   $
��Left=�65   #��[
 6W   �� 5B      $	��150��6:   #��[ 6F   ��6   �� *CraneTypeDesc+6F   5C      5a      �� 5b   
   $ 
Horizontal=�6   $��Right=�6/   $��1��6K   .+�5c      �� $��确认��6   $	��150��6#   $��35��6/   $��10q�6;     NormalButton#��[ 6G   ��5d      �� *ConfirmCommand+��69   ��  �� ElementName$ CraneTypeEditPopupWin��6z   �� 5e      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5f      �� *CancelCommand+��69   �� $ CraneTypeEditPopupWin��6z   5g      5h      5i      