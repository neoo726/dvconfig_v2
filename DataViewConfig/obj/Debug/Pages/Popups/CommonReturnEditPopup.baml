   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   /clr-namespace:DataViewConfigDataViewConfig  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   7    1DataViewConfig.Pages.Popups.CommonReturnEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   0currentclr-namespace:DataViewConfig     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  CommonReturnEditPopupWin��5      $	��450��5   	   $	��550��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.      NormalFontSize#��[  6C     NormalBackGround#��[ 5   	    � 
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
   �� 53      $��30 �6   �� 54      �� 55      .+�57   
   ]� $��0��6   
���   �.+�58      �� $��Stretch=�6   $
��Left=�62   $��通用返回值编辑��6H     NormalLabel#��[ 6Z   �� 59      $��1��6   $��Right=�6&   $��Right=�6=     
TextButton#��[ 6Y   
�������$	����5:   *   ��5;      �� *CloseCommand+��68   ��  �� ElementName$ CommonReturnEditPopupWin��6|   5<      �� 5=   
   $��1��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�5>      �� $ 
Horizontal=�6   $��5q�62   .+�5?      �� $��返回值ID��6   $	��150��6*   $
��Left=�66   #��[ 6X   �� 5@      {�.  $	��150��6K     NormalTextBox#��[ 6W   ��6   �� *ReturnCodeID+6W   5A      �� 5B      $ 
Horizontal=�6   $��5q�62   .+�5C      �� $��	返回值��6   $	��150��6(   $
��Left=�64   #��[ 6V   �� 5D      {�.  $	��150��6I   #��[ 6U   ��6   �� *	ReturnVal+6U   �� 5E      $	��200��6   $��5,0,50,0q�6)   
�������$��Center=�6O   $ ��是否为成功标记  ��5F       d� 	IsChecked 63   �� *IsSuccessFlag+5G      �� 5H      $ 
Horizontal=�6   $��5q�62   .+�5I      �� $��中文描述��6   $	��150��6)   $
��Left=�65   #��[ 6W   �� 5J      $	��200��6<   #��[ 6H   ��6   �� *ReturnDescZh+6H   5K      �� 5L      $ 
Horizontal=�6   $��5q�62   .+�5M      �� $��英文描述��6   $	��150��6)   $
��Left=�66   #��[ 6X   �� 5N      $	��200��69   #��[ 6E   ��6   �� *ReturnDescEn+6E   5O      �� 5P      $ 
Horizontal=�6   $��5q�62   .+�5Q      �� $��系统名称��6   $	��150��6)   $
��Left=�65   #��[ 6W   �� 5R        NormalCombox#���  6   $	��150��6B    �� DisplayMemberPath$ system_desc��5T       �� SelectedValuePath$ 	system_id��67   ��5S      �� *DvSystemLst+ �� SelectedValue 5T   U   �� *
SystemId+5U      5V      �� 5W   
   $��2��6   $��10q�6#   $ 
Horizontal=�6/   $��Right=�6H   .+�5X      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5Y      �� *ConfirmCommand+��69   �� $ CommonReturnEditPopupWin��6}   �� 5Z      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5[      �� *CancelCommand+��69   �� $ CommonReturnEditPopupWin��6}   5]      5^      5_      