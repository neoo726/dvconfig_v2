   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   /clr-namespace:DataViewConfigDataViewConfig  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   .    (DataViewConfig.Pages.Popups.TagEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   0currentclr-namespace:DataViewConfig     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  TagEditPopupWin��5      $	��400��5   	   $	��550��6   
������� � SizeToContent$ Width=�5       � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6/    � Title$ 点名编辑��5         NormalBackGround#��[  5   	   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters	 M� Type,	  &DataView_Configuration.ParamTargetType 	 paramSourceEnum/
  )DataView_Configuration.RequestParamSource��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �       5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5   
   $ 	GetValues��64   "  6K    5       M� 5!      "	 	 6   5#      S� 5$   
   $ 	GetValues��65   "  6L    5%      M� 5&      "	 
 6   5(      5)      .��5*      � ���#1f1f1f
 � RowDefinitions
 5+   
   �� 5,      $��2* �6   �� 5-      $��3* �6   �� 5.      $��60 �6   .+�56   
   �� $��0��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�57      �� $ 
Horizontal=�6   $��5q�62   .+�58      �� $��点名（内部）��6   $	��150��6�   $
��Left=�6�    
 NormalLabel#��[
 6�   {�6)   ��  �� ElementName$ KeepSameTagNameChkbox�� �� Path$ 	IsChecked�  Boolean2BooleanReConverter �� 	Converter# [ 6�   �� 59      $	��250��6�     NormalTextBox#��[ 6�   ��6   �� *TagInternalName+{�6<   �� $ KeepSameTagNameChkbox��$ 	IsChecked�# [ 6�   �� 5:      -   5         ��Name$ KeepSameTagNameChkbox��5:       d� 	IsChecked$
 TrueY�6:   $��5 0 0 0q�6K   
�������$!��与点名(DataAccess)一致��6o   5;      �� 5<      $ 
Horizontal=�6   $��5q�62   .+�5=      �� $!��点名（DataAccess）  ��6   $	��150��6=   $
��Left=�6I   #��[
 6k   �� 5>      $	��250��64   #��[ 6@   ��6   �� *	TagName+6@   5?      �� 5@      $ 
Horizontal=�6   $��5q�62   .+�5A      �� $��描述��6   $	��150��6'   $
��Left=�64   #��[
 6V   �� 5B      $	��350��64   #��[ 6@   ��6   �� *	TagDesc+6@   5C      5D      �� 5E   
   $��1��6   $��5q�6#   $ Vertical=�6.   .+�5F      �� $ 
Horizontal=�6   $��5q�62   .+�5G      �� $ 
Horizontal=�6   $	��150��66   .+�5H      �� $��点名类型��6   $
��Left=�6.   #��[
 6P   '   !DataViewConfig.Controls.TipsLabel  5I         TipName$$ [tag_config_page]tag_data_type��6+   5J      �� 5K        NormalCombox#���  6   $	��200��6B   $��5,0,50,0q�6N    �� DisplayMemberPath$ Value��5M      ��5L      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:TagDataType++ �� Text 5M   1   �� *CurTagDataType+5N      �� 5O      $ 
Horizontal=�6   $��5q�62   .+�5P      �� $ 
Horizontal=�6   $	��150��67   .+�5Q      �� $��点名后缀��6   $
��Left=�6-   #��[
 6O     5R      $" [tag_config_page]tag_postfix��6+   5S      �� 5T      -   5         ��Name$ TagPostFixCombox��5T      #���  65   $	��200��6\   $��5,0,50,0q�6h   $ Value��5V      ��5U      ��    *M� *dv_config:TagPostfixType++ 5V   2   �� *CurTagPostfixType+5X      �� 5Y      $ 
Horizontal=�6   $��5q�62   y�6=   �� $ TagPostFixCombox��$ SelectedIndex�# [ .+�5Z      �� $ 
Horizontal=�6   $	��150��66   .+�5[      �� $��关联宏(Macro)��6   $
��Left=�62   #��[
 6T     5\      $( "[tag_config_page]tag_related_macro��6+   5]      �� 5^      #��[ 6   $	��100��6c   $��5,0,50,0q�6o   ��6A   �� *RelatedMacroName+6o   5_      5d      �� 5e   
   $ 
Horizontal=�6   $��Right=�6/   $��2��6K   .+�5f      �� $��确认��6   $	��150��6"   $��35��6.   $��10q�6:     NormalButton#��[ 6F   ��5g      �� *ConfirmCommand+��69   �� $ TagEditPopupWin��6t   �� 5h      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5i      �� *CancelCommand+��69   �� $ TagEditPopupWin��6t   5j      5k      5l      