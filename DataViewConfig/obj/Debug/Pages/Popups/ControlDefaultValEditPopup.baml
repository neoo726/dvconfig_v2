   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   /clr-namespace:DataViewConfigDataViewConfig  5
   	   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   <    6DataViewConfig.Pages.Popups.ControlDefaultValEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   0currentclr-namespace:DataViewConfig     5
   	   =	converter'clr-namespace:DataViewConfig.Converters     5   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$#  ControlDefaultValEditPopupWin��5      $	��550��5   	   $	��650��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.      NormalBackGround#��[  5   	     NormalFontSize#��[ 68    � 
ResizeMode$ NoResize=�5   	     WIndowChromeKey'  !System.Windows.Shell.WindowChrome   WindowChrome# �  6    $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2REverse?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter 	 MqRpcMultiBlockIdType2Bool?	  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter 
 MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType
  System.Enum S� MethodParameters	 M� Type,  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource
   ResizeBorderThickness   CaptionHeight��%
  &	       &	      &	 &     &	 9     &	 L     &	 _     &		 r     &	
 �     &	 �     &	 �     &	 N      5   
   6-     5   
   6*     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68   	  5    
   64   S� 5!   
   $ 	GetValues��64   " 
 6K    5"      M� 5#      "	  6   5%      S� 5&   
   $ 	GetValues��65   " 
 6L    5'      M� 5(      "	  6   5*        5+   
   
 5,      u�5-      5q�6   $ 0Q�5/   G   50      51      .��52      � -   5         �Name$
 grid��52      ���#1f1f1f � RowDefinitions 53   
   �� 54      $��30 �6   �� 55      �� 56      $��80 �6   .+�58   
   ]� $��0��6   
���   �.+�59      �� $��Stretch=�6   $
��Left=�62   $��控件默认值编辑��6H     NormalLabel#��[ 6Z   �� 5:      $��1��6   $��Right=�6&   $��Right=�6=     
TextButton#��[ 6Y   
�������$	����5;   *   ��5<      �� *CloseCommand+��68   ��  �� ElementName$# ControlDefaultValEditPopupWin��6�   5=      �� 5>   
   $��1��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�5?      �� $ 
Horizontal=�6   $��5q�62   y�6=   �� *
IsAddNew+ �� 	Converter# [ .+�5@      �� $��ID��6   $��5,0,5,0q�6'   $	��150��68   {�.    
NormalText#��[ 6V   �� 5B        NormalTextBox#��[ 6    �� 
IsReadOnly . $	��200��6q   $��5,0,50,0q�6}   ��6V   �� *	DefaultID+6}   5C      �� 5D      $ 
Horizontal=�6   $��5q�62   .+�5E      �� $��控件名称（内部）��6   $��5,0,5,0q�6-   $	��150��6>   #��[ 6K   �� 5G        NormalCombox#���  6   $	��200��6B   $��5,0,50,0q�6N   $
��Left=�6`   ��5H      �� *
ControlLst+ �� Text 5I      �� *ControlInternalName+��5J      :   4DataViewConfig.Controls.ComboBoxItemTemplateSelector  5K         SelectedTemplate 5L      �� 5M   "   .R�5N   &   �� ��60   �� *dv_control_internal_name+5O   #      DropDownTemplate 5Q      �� 5R   "   .R�5S   &   �� 
�������$��0 0 0 1q�6A   $��1q�6[   ._�5T   *   �� $ Vertical=�66   
������ $	��300��6g   .+�5U   .   �� $��Center=�6b   
�������#��[ 6�   ��68   �� *dv_control_internal_name+6�   �� 5V   .   $
��Left=�6Y   $��-5 0 0 0q�6t   $��Center=�6�     Global.Gray#��[ 6�   #��[ 6�   ��64   �� *dv_control_desc+6�   5X   +   5Y   '   5Z   #   5\      5^      5b      �� 5c      $ 
Horizontal=�6   $��5q�62   .+�5d      �� $��填充类型��6   $��5,0,5,0q�6)   $	��150��6;   #��[ 6G   �� 5e      -   5         ��Name$ IsFillComboxListCheckbox��5e      $	��200��6h   $��5,0,50,0q�6t   $��填充下拉框列表��5f      
������� d� 	IsChecked 5e   =   �� *IsFillDrawComboxList+5f   *   5g      �� 5h      $ 
Horizontal=�6   $��5q�62   y�5i      �� $ IsFillComboxListCheckbox�� �� Path$ 	IsChecked�# [ .+�5k      �� $"��数据来源/数据库表名��6   $	��Top=�6.   $��5,15,5,0q�6G   $	��150��6Y   #��[ 6f   �� 5l      $ Vertical=�6   .+�5m      	� -   5         	�Name$ AllCraneNameRadioBtn��5m   "    	� 	GroupName$ dbSheetSource��6@   $��所有设备名称��6Z   $��15q�6l     NormalRadioButton#��[ 5o   *    5n   *   �� *FillAllCraneByDb+5o   *   	� 5p      -   5        $ AllRcsNameRadioBtn��5p   "   $ dbSheetSource��6>   $��所有操作台名称��6Y   $��15q�6k   #��[ 5r   *    5q   *   �� *FillAllRcsByDb+5r   *   	� 5s      -   5        $ AllBlockNameRadioBtn��5s   "   $ dbSheetSource��6@   $��所有堆场名称��6[   $��15q�6l   #��[ 5u   *    5t   *   �� *FillAllBlockByDb+5u   *   	� 5v      -   5        $ AllBayNameRadioBtn��5v   "   $ dbSheetSource��6>   $��所有贝位名称  ��6Y   $��15q�6t   #��[ 5x   *    5w   *   �� *FillAllBayByDb+5x   *   5y      5z      �� 5{      $ Vertical=�6   y�5|      �� $ IsFillComboxListCheckbox��$ 	IsChecked�# [ .+�5}      �� $ 
Horizontal=�6   $��5q�66   .+�5~      �� $��填充默认值条件��6    $��5,0,5,0q�60   $	��150��6B   #��[ 6N   �� 5      -   5        $ IsConditionEnableChkBox��5      $!��直接填充，不设条件��5�   "   $��5,0,5,0q�66   
������� 5�   8   �� *HasCondition+5�      �� 5�      $ Vertical=�6   y�5�      �� $ IsConditionEnableChkBox��$ 	IsChecked�# [ .+�5�      �� $ 
Horizontal=�6!   $��5q�6:   .+�5�      �� $��点名（内部）��6$   $��25,0,5,0q�63   $	��150��6F   #��[ 6R   �� 5�      -	   5         ��Name$ tagSelectionCombox2��5�   #   $	��250��6A   $��5,0,50,0q�6M   #��[ 6_    �� 
IsEditable .  �� IsTextSearchEnabled .   �� IsTextSearchCaseSensitive .  w� TextPath$ tag_internal_name��5�   Q   
�������
���   ���5�      �� *TagLst+ �� SelectedItem 5�      �� *!DefaultConditionTagInternalName+��5�      �� 5�   "   .R�5�   &   �� .+�5�   *   �� $��0 3 0 5q�64   $
��Bold"�6h   ��6E   �� *tag_internal_name+6h   �� 5�   *   #��[ 64   ��6^   �� *
tag_desc+5�   '   5�   #   5�      �� 5�      -
   5        $  IsConditionTagJsonCheckbox��5�   $   $��JSON数据点��6I   
�������$	��100��6n   $��5,0,10,0q�6z   5�      �� 5�      $ 
Horizontal=�6#   $��5q�6<   y�5�      �� $  IsConditionTagJsonCheckbox��$ 	IsChecked�# [ .+�5�      �� $��	Json_Path��6%   $��25,0,5,0q�66   $	��150��6I   #��[ 6V   �� 5�      #��[ 6%   $	��200��6v   $��5,0,50,0q�6�   ��6L   �� *DefaultConditionJsonPath+6�   5�      �� 5�      $ 
Horizontal=�6#   $��5q�6<   .+�5�      �� $��点值��6&   $��25,0,5,0q�60   $	��150��6C   #��[ 6O   6u   �� 5�      #��[ 6%   $	��200��6q   $��5,0,50,0q�6}   ��6L   �� *DefaultConditionVal+6}   5�      5�      �� 5�      $ 
Horizontal=�6   $��5q�68   .+�5�      �� $��默认填充值��6"   $��5,0,5,0q�6/   $	��150��6@   #��[ 6M   6s   5�      �� 5�      $ 
Horizontal=�6   $��5q�68   .+�5�      �� $��点名（内部）��6"   $��25,0,5,0q�60   $	��150��6C   #��[ 6O   6u   �� 5�      -   5        $ tagSelectionCombox1��5�      $	��250��6=   $��5,0,50,0q�6I   #��[ 6[    .  .  . $ tag_internal_name��5�   O   
�������
���   ���5�      �� *TagLst+ 5�      �� *DefaultContentTagInternalName+��5�      �� 5�      .R�5�   "   �� .+�5�   &   �� $��0 3 0 5q�60   $
��Bold"�6d   ��6A   �� *tag_internal_name+6d   �� 5�   &   #��[ 60   ��6Z   �� *
tag_desc+5�   #   5�      5�      �� 5�      -   5        $ IsContentTagJsonCheckbox��5�      $��JSON数据点��6C   
�������$	��100��6h   $��5,0,10,0q�6t   5�      �� 5�      $ 
Horizontal=�6   $��5q�67   y�5�      �� $ IsContentTagJsonCheckbox��$ 	IsChecked�# [ .+�5�      �� $��	Json_Path��6!   $��25,0,5,0q�62   $	��150��6E   #��[ 6R   �� 5�      #��[ 6!   $	��200��6p   $��5,0,50,0q�6|   ��6H   �� *DefaultContentJsonPath+6|   5�      5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Right=�6/   $��2��6K   .+�5�      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5�      �� *ConfirmCommand+��69   �� $# ControlDefaultValEditPopupWin��6�   �� 5�      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5�      �� *CancelCommand+��69   �� $# ControlDefaultValEditPopupWin��6�   5�      5�      5�      