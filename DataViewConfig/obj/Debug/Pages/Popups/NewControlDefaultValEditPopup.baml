   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   /clr-namespace:DataViewConfigDataViewConfig  5
   	   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   ?    9DataViewConfig.Pages.Popups.NewControlDefaultValEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   0currentclr-namespace:DataViewConfig     5
   	   =	converter'clr-namespace:DataViewConfig.Converters     5   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$&   NewControlDefaultValEditPopupWin��5      $	��350��5   	   $	��850��6   
������� � WindowStartupLocation$ CenterScreen=�5   	      NormalBackGround#��[  5   	     NormalFontSize#��[ 68    � 
ResizeMode$ NoResize=�5   	    � Title$ 控件默认值编辑 ��5   	    � SizeToContent$ Height=�6   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2REverse?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter 	 MqRpcMultiCidType2Bool 
 targetTypeEnum S� 
MethodName S� 
ObjectType	  System.Enum S� MethodParameters	 M� Type,
  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource'  !System.Windows.Shell.WindowChrome  WIndowChromeKey
   ResizeBorderThickness   CaptionHeight��%
  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &		 �     &	
 �     &	 �     &	 N      5   
   6-     5   
   6*     5   
   64     5   
   6-     5   
   6.     5   
   6/     5    
   68     5!   
   64   S� 5"   
   $ 	GetValues��64   " 	 6K    5#      M� 5$      "	 
 6   5&      S� 5'   
   $ 	GetValues��65   " 	 6L    5(      M� 5)      "	  6   5+        5,   
   
 5-      u�5.      5q�6   $ 0Q�50   G   51      52      .��53      � -   5         �Name$
 grid��53      ���#1f1f1f � RowDefinitions 54   
   �� 56      �� 57      $��80 �6   .+�5:   
   �� $��0��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�5<      �� $ 
Horizontal=�6   $��5q�62   .+�5=      �� $��控件名称（内部）��6   $��5,0,5,0q�6-   $	��150��6>     
NormalText#��[ 6K   �� 5@        NormalTextBox#��[ 5A   B   $	��200��6i   {�.  y�5@      �� *
IsAddNew+ �� 	Converter# [ ��5A      �� *SelectedControlInternalName+6u   �� 5B        NormalCombox#���  5C      $	��200��6<   $��5,0,50,0q�6H   $
��Left=�6Z   y�5B      �� *
IsAddNew+# [ ��5D      �� *ControlInternalNameLst+ �� SelectedItem 5E      �� *SelectedControl+��5F      :   4DataViewConfig.Controls.ComboBoxItemTemplateSelector  5G         SelectedTemplate 5H      �� 5I   "   .R�5J   &   �� ��60   �� *dv_control_internal_name+5K   #      DropDownTemplate 5M      �� 5N   "   .R�5O   &   �� 
������ $ Vertical=�6J   $	��200��6a   .+�5P   *   �� 
�������#��[ 6r   ��64   �� *dv_control_internal_name+6r   �� 5Q   *   $
��Left=�6T     Global.Gray#��[ 6p     UnderlineLabel#��[ 6�   ��60   �� *dv_control_desc+6�   5R   '   5S   #   5U      5W      5X      �� 5Y      $ 
Horizontal=�6   $��5q�62   .+�5Z      �� $ 
Horizontal=�6   $��5,0,5,0q�6:   $	��150��6L   .+�5[      �� $��填充类型��6    #��[ 6,   '   !DataViewConfig.Controls.TipsLabel  5\         TipName$+ %[control_default_value_edit]fill_type��6+   5]      �� 5^      -   5         ��Name$ IsFillComboxListCheckbox��5^      $	��200��6h   $��5,0,50,0q�6t   $��填充下拉框列表��5_      
������� d� 	IsChecked 5^   =   �� *IsFillDrawComboxList+5_   *   5`      �� 5a      $ 
Horizontal=�6   $��5q�62   y�5b      ��  �� ElementName$ IsFillComboxListCheckbox�� �� Path$ 	IsChecked�# [ .+�5d      �� $"��数据来源/数据库表名��6   $	��Top=�6.   $��5,15,5,0q�6G   $	��150��6Y   #��[ 6f   �� 5e      $ Vertical=�6   .+�5f      	� -   5         	�Name$ AllCraneNameRadioBtn��5f   "    	� 	GroupName$ dbSheetSource��6@   $��所有设备名称��6Z   $��15q�6l     NormalRadioButton#��[ 5h   *    5g   *   �� *FillAllCraneByDb+5h   *   	� 5i      -   5        $ AllRcsNameRadioBtn��5i   "   $ dbSheetSource��6>   $��所有操作台名称��6Y   $��15q�6k   #��[ 5k   *    5j   *   �� *FillAllRcsByDb+5k   *   	� 5l      -   5        $ AllBlockNameRadioBtn��5l   "   $ dbSheetSource��6@   $��所有堆场名称��6[   $��15q�6l   #��[ 5n   *    5m   *   �� *FillAllBlockByDb+5n   *   �� 5o      $ 
Horizontal=�6!   .+�5p      	� -   5        $ AllBayNameRadioBtn��5p   &   $ dbSheetSource��6B   $��所有贝位名称��6]   $��15q�6n   #��[ 5r   *    5q   *   �� *FillAllBayByDb+5r   *     5s      $. ([control_default_value_edit]fill_all_bay��6/   5t      5u      5v      �� 5w      $ Vertical=�6   y�5x      �� $ IsFillComboxListCheckbox��$ 	IsChecked�# [ .+�5y      �� $ 
Horizontal=�6   $��5q�66   .+�5z      �� $��填充条件��6    $��5,0,5,0q�6-   $	��150��6?   #��[ 6K   �� 5{      -   5        $ IsConditionEnableChkBox��5{      {�.  $!��直接填充，不设条件��5|   "   $��5,0,5,0q�66   
������� 5}   8   �� *HasCondition+# [ 5~      �� 5      $ Vertical=�6   y�5�      �� $ IsConditionEnableChkBox��$ 	IsChecked�# [ .+�5�      �� $ 
Horizontal=�6!   $��5q�6:   .+�5�      �� $��点名（内部）��6$   $��25,0,5,0q�63   $	��150��6F   #��[ 6R   �� 5�      -	   5         ��Name$ tagSelectionCombox2��5�   #   $	��250��6A   $��5,0,50,0q�6M   #��[ 6_    �� 
IsEditable .  �� IsTextSearchEnabled .   �� IsTextSearchCaseSensitive .  w� TextPath$ tag_internal_name��5�   C   
�������
���   ���5�      �� *TagInternalNameLst+ 5�      �� *DefaultContentTag+��5�      �� 5�   "   .R�5�   &   �� .+�5�   *   �� $��0 3 0 5q�64   $
��Bold"�6h   ��6E   �� *tag_internal_name+6h   �� 5�   *   #��[ 64   ��6^   �� *
tag_desc+5�   '   5�   #   5�      �� 5�      -
   5        $! IsConditionTagArrayCheckbox��5�   #   $��数组类型��6y   
�������$��5,0,10,0q�6�    6H   �� *IsDefaultConditionTagArray+6�   �� 5�      -   5        $  IsConditionTagJsonCheckbox��5�   $   $��内容为JSON��6z   
�������$��5,0,10,0q�6�    6I   �� *IsDefaultConditionTagJson+6�   5�      �� 5�      $ 
Horizontal=�6!   $��5q�6:   y�5�      �� $! IsConditionTagArrayCheckbox��$ 	IsChecked�# [ .+�5�      �� $��数组索引相关宏��6%   $��25,0,5,0q�64   $	��150��6G   #��[ 6T   �� 5�      #��[ 6%   $	��200��6{   $��5,0,50,0q�6�   ��6L   �� *DefaultConditionTagArrayMacro+6�   5�      �� 5�      $ 
Horizontal=�6#   $��5q�6<   y�5�      �� $  IsConditionTagJsonCheckbox��$ 	IsChecked�# [ .+�5�      �� $��	Json_Path��6%   $��25,0,5,0q�66   $	��150��6I   #��[ 6V   �� 5�      #��[ 6%   $	��200��6v   $��5,0,50,0q�6�   ��6L   �� *DefaultConditionJsonPath+6�   5�      �� 5�      $ 
Horizontal=�6#   $��5q�6<   .+�5�      �� $��点值��6&   $��25,0,5,0q�60   $	��150��6C   #��[ 6O   6u   �� 5�      #��[ 6%   $	��200��6q   $��5,0,50,0q�6}   ��6L   �� *DefaultConditionVal+6}   5�      5�      �� 5�      $ 
Horizontal=�6   $��5q�68   .+�5�      �� $��	填充值��6"   $��5,0,5,0q�6-   #��[ 6?   6e     5�      $0 *[control_default_value_edit]fill_tag_value��6+   5�      �� 5�      $ 
Horizontal=�6   $��5q�68   .+�5�      �� $��点名（内部）��6"   $��25,0,5,0q�60   $	��150��6C   #��[ 6O   6u   �� 5�      -   5        $ tagSelectionCombox1��5�      $	��250��6=   $��5,0,50,0q�6I   #��[ 6[    .  .  . $ tag_internal_name��5�   C   
�������
���   ���5�      �� *TagInternalNameLst+ 5�      �� *DefaultContentTag+��5�      �� 5�      .R�5�   "   �� .+�5�   &   �� $��0 3 0 5q�60   $
��Bold"�6d   ��6A   �� *tag_internal_name+6d   �� 5�   &   #��[ 60   ��6Z   �� *
tag_desc+5�   #   5�      5�      �� 5�      -   5        $ IsContentTagArrayCheckbox��5�      $��数组类型��6r   
�������$��5,0,10,0q�6�    6C   �� *IsDefaultContentTagArray+6�   �� 5�      -   5        $ IsContentTagJsonCheckbox��5�      $��内容为JSON��6p   
�������$��5,0,10,0q�6�    6A   �� *IsDefaultContentTagJson+6�   5�      �� 5�      $ 
Horizontal=�6   $��5q�66   y�5�      �� $ IsContentTagArrayCheckbox��$ 	IsChecked�# [ .+�5�      �� $��数组索引相关宏��6!   $��25,0,5,0q�60   $	��150��6C   #��[ 6P   �� 5�      #��[ 6!   $	��200��6u   $��5,0,50,0q�6�   ��6H   �� *DefaultContentTagArrayMacro+6�   5�      �� 5�      $ 
Horizontal=�6   $��5q�67   y�5�      �� $ IsContentTagJsonCheckbox��$ 	IsChecked�# [ .+�5�      �� $��	Json_Path��6!   $��25,0,5,0q�62   $	��150��6E   #��[ 6R   �� 5�      #��[ 6!   $	��200��6p   $��5,0,50,0q�6|   ��6H   �� *DefaultContentJsonPath+6|   5�      5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Right=�6/   $��1��6K   .+�5�      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5�      �� *ConfirmCommand+��69   �� $&  NewControlDefaultValEditPopupWin��6�   �� 5�      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5�      �� *CancelCommand+��69   �� $&  NewControlDefaultValEditPopupWin��6�   5�      5�      5�      