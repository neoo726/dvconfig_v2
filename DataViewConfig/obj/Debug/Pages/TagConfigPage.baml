   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null;"clr-namespace:DataViewConfig.PagesDataViewConfig.Pages  5      A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      /clr-namespace:DataViewConfigDataViewConfig  5	      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5
      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      (    "DataViewConfig.Pages.TagConfigPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:DataViewConfig.Pages     5      :controls%clr-namespace:DataViewConfig.Controls     5      =	converter'clr-namespace:DataViewConfig.Converters     5      0currentclr-namespace:DataViewConfig     5	      6vm'clr-namespace:DataViewConfig.ViewModels     5
      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5      6sys&clr-namespace:System;assembly=mscorlib     5         Content.BackGround#��[  5      
�������$��12(�6R   $��5q�5        � 	Resources  5      8   2DataViewConfig.Converters.EnumDescriptionConverter  EnumValue2Description8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2Reverse  tagDataEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type(  "DataView_Configuration.TagDataType  postfixtEnum+  %DataView_Configuration.TagPostfixType*  $System.Windows.Controls.DataGridCell �� 
TargetType �� Property �� Value �� 	Resources	 �� Color
 �� Triggers P� Property P� Value��%�  &	       &	      &	 &     &	 9     &	 �     '  �       5   
   6-     5   
   6-     5   
   6*   S� 5   
   $ 	GetValues��61   "  6H    5      M� 5      "  6   5      S� 5   
   $ 	GetValues��62   "  6I    5      M� 5      "  6   5      �� 5    
   "  6   .�5!      ��  � ��$ 15(�6)    5"      ���� 5#      $	 White��6i   (��       6"   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6i   5$      
 5%      P� 5&       �@ 
IsSelected . . �5(      ��  � �� �#00469663   �� 5)       � ��
 ���� 64   5*      5,      5-      .��5.      �  � RowDefinitions 5/   
   �� 50      $	��150 �6   �� 51      .+�54   
   �� $��0��6   .+�55      �� $��点名配置��6     NormalGroupBox#��[ 6%    � ToolTip$� �此处配置的点名用于二次代码调用、画面调用函数参数等，如果仅作为画面组态显示，则不需要配置!!!��6M   .��56      �  57      �� 58      �� 59      �� 5:       � ColumnDefinitions 5=      �� 5>      $	��170 �6+   �� 5?      $	��280 �6+   �� 5@      $	��150 �6,   �� 5A      $	��280 �6,   �� 5B      $	��240 �6,   .+�5E      �� $��0��6!   $��0��6.   $��5q�6>    �� Orientation$ 
Horizontal=�6I   $
��Left=�6c   .+�5F      �� $��点名(DataAccess)��6      NormalLabel#��[ 5G      '   !DataViewConfig.Controls.TipsLabel  5H         TipName$% [tag_config_page]tag_dataaccess��6/   5I      �� 5J      $��0��6   $��1��6+     NormalTextBox#��[ 6<   $	��250��5K   ;   ��6   �� *TagRealName+6;   �� 5L      $��0��6!   $��2��6.   $ 
Horizontal=�6>   $	��150��6Y   .+�5M      �� $��点名(内部）��6    $
��Left=�62   #��[ 6U     5N      $% [tag_config_page]tag_dataaccess��6/   5O      �� 5P      $��0��6   $��3��6+   #��[ 6;   $
��Left=�6c   $	��250��5R   "   ��5P      �� *TagInternalName+{�5Q   "   �� *TagNameKeepSame+ �� 	Converter# [ 5R   "   �� 5S      -   5         ��Name$ TagNameKeepSameChkbox��5S   <   $��0��6   $��4��6,   $+��%与点名（DataAccess）保持一致��6[   
������� d� 	IsChecked 6y   �� *TagNameKeepSame+6�   �� 5U      $��1��6!   $��0��6.   $ 
Horizontal=�6>   $��5q�6W   $	��150��6c   .+�5V      �� $��点名类型��6    $
��Left=�60   #��[ 6T     5W      $$ [tag_config_page]tag_data_type��6/   5X      �� 5Y      $��1��6   $��1��6,    	 NormalCombox#��� 	 6<   $	��250��6c    �� DisplayMemberPath$ data_type_desc��5[      ��5Z      �� *TagDataTypeLst+ �� Text 5[   :   �� *CurTagDataType+�� 5\      $��1��6"   $��2��6/   $ 
Horizontal=�6A   .+�5]      �� $��描述��6    $	��150��6-   $
��Left=�69   #��[ 6\   5^      �� 5_      $��1��6   $��3��6,   $��2��6<   #��[ 6R   $
��Left=�6y   $	��350��6�   ��6�   �� *	TagDesc+6�   �� 5a      $��2��6"   $��0��6/   $ 
Horizontal=�6?   $��5q�6X   $	��150��6c   .+�5b      �� $��点名后缀��6    $
��Left=�60   #��[ 6V     5c      $" [tag_config_page]tag_postfix��6/   5d      �� 5e      -   5         ��Name$ TagPostFixCombox��5e   =   $��2��6   $��1��6,   #��� 	 6W   $	��250��6~   $ postfix_type_desc��5g      ��5f      �� *TagPostfixLst+ 5g   >   �� *CurTagPostFixType+�� 5h      $��2��6!   $��2��6.   $ 
Horizontal=�6?   $	��150��6X   y�6d   ��  �� ElementName$ TagPostFixCombox�� �� Path$ SelectedIndex�# [ .+�5i      �� $��关联宏(Macro)��6    $
��Left=�66   #��[ 6Z     5j      $( "[tag_config_page]tag_related_macro��6/   5k      �� 5l      $��2��6   $��3��6+   $
��Left=�6;   #��[ 6�   $	��250��6  y�6V   �� $ TagPostFixCombox��$ SelectedIndex�# [ ��6�   �� *RelatedMacroName+6  �� 5n      $��2��6!   $��4��6.   $ 
Horizontal=�6?   .+�5o      �� $��添加��6!    
 NormalButton#��� 
 6.   $	��150��6U   ��5p      �� *AddNewCommand+5q      �� 5t      $��3��6!   $ 
Horizontal=�6.   $��Center=�6G   $��10q�6d   5v      5w      5x      5y      �� 5z   
   $��点名列表��6   $��1��6!   #��[ 6.   .��5{      �  5|      �� 5}      $��50 �6$   �� 5~      .+�5�      ]� $��0��6   $
��auto��6*     Content.Topbar.BackGround#��[ 67   $��35��6q   ��. .+�5�      �� $ 
Horizontal=�6!   $��Right=�6:   .+�5�      �� $��点名类型��6    #��[ 6/   $
��Left=�6U   �� 5�      $ data_type_desc��6J   #��[	 6�   $	��150��6�   ��6#   �� *TagDataTypeLst+ 6m   �� *SearchTagType+6�   �� 5�      $��后缀格式��6    #��[ 6/   $
��Left=�6T   �� 5�      $ postfix_type_desc��6J   #��[	 6�   $	��150��6�   ��6$   �� *TagPostfixLst+ 6p   �� *SearchTagPrefix+6�   �� 5�      $��	关联宏��6    #��[ 6.   $
��Left=�6S   �� 5�      #��[ 6A   $	��150��6h   ��6#   �� *SearchMacro+6h   �� 5�      $&�� 点名(内部/DataAccess/描述)��6    #��[ 6?   $
��Left=�6d   �� 5�      #��[ 6A   $	��150��6h   ��6"   �� *SearchTagName+6h   �� 5�      $	����6!   $��50��65   $��Center=�6@     ImageButton#��[ 5�      ��6A   �� *QueryCommand+�� 5�      $	����6!   #���  64   $��50��6Z   $��Center=�6e   ��5�      �� *ResetCommand+�� 5�      $��导入��6!   $��50��6U   #��� 
 6`   $��5q�6�   ��6.   �� *ImportDataCommand+6�   �� 5�      $��导出��6!   $��50��6V   #��� 
 6a   $��5q�6�   ��6/   �� *ExportDataCommand+6�   5�      �� 5�      5�      &	   System.Windows.Controls.DataGrid	  5�      $��1��6    	  ColumnWidth5
  /System.Windows.Controls.DataGridLengthConverter$ *
 6V     NormalDataGrid#��[ 5�      ��5�   )   �� *TagLst+ �� IsAsync .  	  Columns 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ 操作��61      Width$	 120
 6=      CellTemplate 5�      �� 5�   "   .R�5�   &   �� $ 
Horizontal=�61   .+�5�   *   �� $��编辑��61   $��60��6>     
TextButton#��[ 6I   ��6n   �� *DataContext.EditCommand+  �� RelativeSource  �� ! �� Mode$! FindAncestor=�" �� AncestorType$" DataGridL���5�   -   ��   �� $! FindAncestor=�$" DataGridL�$ SelectedItem��� 5�   *   $��删除��61   $��60��6?   #��[ 6J   ��6n   �� *DataContext.DeleteCommand+  �� $! FindAncestor=�$" DataGridL���5�   -   ��   �� $! FindAncestor=�$" DataGridL�$ SelectedItem�5�   '   5�   #   5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $ 序号��6-   $ 50
 6P    " DataGridTextColumnElementStyle1  +System.Windows.Controls.DataGridBoundColumn#   ElementStyle## [ 6[   $   Binding$ 69   �� *Id+5�        5�      $ 点名（内部）��6-   ## [ 6b   $ *
 6�   $ 6=   �� *TagInternalName+5�        5�      $ 描述��6-   ## [ 6R   $ *
 6�   $ 69   �� *Desc+5�        5�      $* $点名（DataAccess）(不带后缀)��6-   ## [ 6i   $ *
 6�   $ 6K   �� *	TagName+5�        5�      $ 类型��6-   ## [ 6X   $ *
 6�   $ 69   �� *
DataType+5�        5�      $ 后缀格式��6-   ## [ 6�   $ *
 6�   $ 6;   �� *	Postfix+# [ 5�        5�      $ 相关宏（macro)名称��6-   ## [ 6i   $ *
 6�   $ 6C   �� *RelatedMacroName+5�      5�      5�      5�      5�      5�      