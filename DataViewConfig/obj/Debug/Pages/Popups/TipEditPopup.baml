   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   /clr-namespace:DataViewConfigDataViewConfig  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   .    (DataViewConfig.Pages.Popups.TipEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   0currentclr-namespace:DataViewConfig     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  TipEditPopupWin��5      $	��300��5   	   $	��550��6   
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
   �� 5-      $��2* �6   �� 5.      $��3* �6   �� 5/      $��60 �6   .+�57   
   �� $��0��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�58      �� $ 
Horizontal=�6   $��5q�62   .+�59      �� $��提示名称��6   {�.  $	��150��6:   $
��Left=�6F     NormalLabel#��[ 6h   �� 5:      {�.  $	��350��6H     NormalTextBox#��[ 6T   ��6   �� *
TipsName+6T   5<      �� 5=      $ 
Horizontal=�6   $��5q�62   .+�5>      �� $��提示类型��6   $	��150��6)   $
��Left=�65   #��[ 6W   	� 5?      -   5         	�Name$ TxtTipsRadioBtn��5?      $��文字提示��6\     NormalRadioButton#��[ 6l    d� 	IsChecked 67   �� *TxtTipsChecked+6l   	� 5C      $��图片提示��6   #��[ 6�    6-   �� *TxtTipsChecked+  Boolean2BooleanReConverter �� 	Converter# [ 6�   5D      �� 5E      $ 
Horizontal=�6   $��5q�62   y�6=   ��  �� ElementName$ TxtTipsRadioBtn�� �� Path$ 	IsChecked�# [ .+�5F      �� $��提示内容��6   $	��150��6)   $
��Left=�66   #��[ 6X   �� 5G      $	��350��68   #��[ 6D   ��6   �� *TipsContent+6D   5H      �� 5I      $ 
Horizontal=�6   $��5q�62   y�6=   �� $ TxtTipsRadioBtn��$ 	IsChecked�# [ .+�5J      �� $��提示图片��6   $	��150��6)   $
��Left=�66   #��[ 6X   �� 5K      $
��Left=�6d   $	��100��6   $	��100��6�   ��6   �� *TipsImageUrl+# [ 6�   �� 5L      $��预览��6     
TextButton#��[ 6N   ��6&   �� *PreviewImageCommand+6N   �� 5M      $��选择��6   #��[ 6M   ��6&   �� *SelectImageCommand+6M   5N      5O      �� 5P   
   $ 
Horizontal=�6   $��Center=�6/   $��2��6L   .+�5Q      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5R      �� *ConfirmCommand+��69   �� $ TipEditPopupWin��6t   �� 5S      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5T      �� *CancelCommand+��69   �� $ TipEditPopupWin��6t   5U      5V      5W      