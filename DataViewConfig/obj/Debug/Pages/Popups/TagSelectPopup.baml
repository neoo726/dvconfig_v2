   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   /clr-namespace:DataViewConfigDataViewConfig  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   0    *DataViewConfig.Pages.Popups.TagSelectPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   0currentclr-namespace:DataViewConfig     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  TagSelectPopupWin��5      $	��400��5   	   $	��350��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6/    � Title$ 点位列表��5         NormalBackGround#��[  5   	   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type,	  &DataView_Configuration.ParamTargetType 	 paramSourceEnum/
  )DataView_Configuration.RequestParamSource��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �       5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5   
   $ 	GetValues��64   "  6K    5       M� 5!      " 	 6   5#      S� 5$   
   $ 	GetValues��65   "  6L    5%      M� 5&      " 
 6   5(      5*      .��5+      � ���#1f1f1f	 � RowDefinitions	 5,   
   �� 5-      $��1* �6   �� 5.      $��4* �6   �� 5/      $��60 �6   .+�57   
   �� $��0��6   $��10q�6#   
 �� Orientation$
 Vertical=�6/   .+�58      �� $
 
Horizontal=�6   $��5q�62   .+�59      +   %DataViewConfig.Controls.SearchTextBox  $	��300��6+      
PreviewTxt$ 	关键词��68   $��0 0 10 0q�6o      
BtnCommand 6I   �� *QueryCommand+6�   5<      5=      �� 5>   
   
������ $��1��6K   $��15q�6X   ��6   �� *TagLst+��5?      �� 5@       �� 
TargetType" ��6   .�5A      ��  �� Property � �� �� Value
 ���� 63   5B      ��5D      �� 5E      .R�5F      �� $
 
Horizontal=�6!   ���#1f1f1f$��Stretch=�6O   $	��350��6m   .+�5G      	�  	�Name$ radioBtn��6&    	� 	GroupName$ ListBoxRadioBtns��68   $	��Top=�6U    
 NormalRadioButton#��[
 6t    d� 	IsChecked 5H   &   �� *
IsSelected+.��5I      �� $
 Vertical=�6)   ���#1f1f1f.+�5J   "   ��   
NormalText#��[ 6E   ��6,   �� *	TagName+6E   �� 5K   "   ���#c1c1c1#��[ 6W   ��6,   �� *Desc+6W   5L      5M      5O      5P      5R      �� 5S   
   $
 
Horizontal=�6   $��Right=�6/   $��2��6K   .+�5T      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5U      �� *ConfirmCommand+��69   ��  �� ElementName$ TagSelectPopupWin��6v   �� 5V      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5W      �� *CancelCommand+��69   �� $ TagSelectPopupWin��6v   5X      5Y      5Z      