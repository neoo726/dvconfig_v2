   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null;"clr-namespace:DataViewConfig.PagesDataViewConfig.Pages  5      A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5	      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5
      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      /clr-namespace:DataViewConfigDataViewConfig  5      ,    &DataViewConfig.Pages.ControlConfigPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:DataViewConfig.Pages     5      :controls%clr-namespace:DataViewConfig.Controls     5      =	converter'clr-namespace:DataViewConfig.Converters     5      6vm'clr-namespace:DataViewConfig.ViewModels     5	      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5
      6sys&clr-namespace:System;assembly=mscorlib     5      0currentclr-namespace:DataViewConfig     5         Content.BackGround#��[  5      
�������$��12(�6R   $��5q�5        � 	Resources  5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility  controlTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type(  "DataView_Configuration.ControlType*  $System.Windows.Controls.DataGridCell �� 
TargetType �� Property �� Value �� 	Resources	 �� Color
 �� Triggers P� Property P� Value��%�  &	       &	      '  n       5   
   6-   S� 5   
   $ 	GetValues��65   "  6L    5      M� 5      "  6   5      �� 5   
   "  6   .�5      ��  � ��$ 15(�6)    5      ���� 5      $	 White��6i   (��       6"   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6i   5      
 5      P� 5        �@ 
IsSelected . . �5"      ��  � �� �#00469663   �� 5#       � ��
 ���� 64   5$      5&      5'      .��5(      �  � RowDefinitions 5)   
   �� 5*      $	��150 �6   �� 5+      .+�5.   
   �� $��控件配置��6   $��0��6!     NormalGroupBox#��[ 6.   .��5/      � $	��180��6    50      �� 51      $��1* �6%   �� 52      $��1* �6$   .+�55      �� $��0��6    �� Orientation$ Vertical=�6*   $��5q�6A   .+�56      �� $ 
Horizontal=�6!   $��5 0 0 0q�6<   .+�57      �� $ 
Horizontal=�6%   $	��150��6?   .+�58      �� $��控件名称(内部）��6$     NormalLabel#��[ 67   '   !DataViewConfig.Controls.TipsLabel  59         TipName$0 *[control_config_page]control_internal_name��63   5:      �� 5;        NormalTextBox#��[ 5<      $	��200��6k   ��6E   �� *ControlInternalName+6k   �� 5?      $ 
Horizontal=�6%   $	��200��6>   $��5q�6J   .+�5@      �� $��控件类型��6$   $
��Left=�65   #��[ 6R     5A      $' ![control_config_page]control_type��63   5B      �� 5C        NormalCombox#���  6#   $	��200��6J    �� SelectedValuePath$ Value��5E   L    �� DisplayMemberPath$ Description��6f   ��5D      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:ControlType++ �� SelectedValue 5E      �� *CurControlType+ �� Mode$ TwoWay=�5F      5G      �� 5I      $ 
Horizontal=�6!   $��5q�6:   .+�5J      �� $ 
Horizontal=�6%   $	��150��6?   .+�5K      �� $��控件AccessID��6$   #��[ 6<     5L      $$ [control_config_page]access_id��63   5M      �� 5N      #��[ 6"   $	��200��6c   ��6I   �� *
AccessId+6c   �� 5P      $ 
Horizontal=�6%   $	��200��6>   $��5q�6J   $��35��6U   .+�5Q      �� $��控件所在画面��6$   #��[ 69     5R      $) #[control_config_page]control_screen��63   5S      �� 5T      #���  6#   $	��200��6J   ��5U      �� *ScreenNameLst+ �� SelectedItem 5V      �� *SelectedScreen+$ TwoWay=���5W      �� 5X   "   .R�5Y   &   �� .+�5Z   *   �� $��0 3 0 5q�64   $
��Bold"�6i   ��6E   �� *dv_screen_csw_name+6i   �� 5[   *     Global.Gray#��[ 64   ��6^   �� *dv_screen_desc+5\   '   5]   #   5_      �� 5`      $��添加��6!     NormalButton#���  6.   $��30 0 0 0q�6U   $	��150��6g   $�� ��5a   7   ��6   �� *AddNewCommand+67   5b      5c      5d      5e      �� 5f   
   $��1��6   $��控件列表��6    #��[ 6.   .��5g      �  5h      �� 5i      $��40 �6$   �� 5j      .+�5m      ]� $��0��6   $
��auto��6*   $��35��67   $��Bottom=�6C    	 Content.Topbar.BackGround#��[	 6^   ��. .+�5o      �� $ 
Horizontal=�6!   $��Right=�6:   .+�5q      �� $��控件所在画面��6    $��5,0,5,0q�62   #��[ 6D   �� 5r      #���  6#   $	��200��6J   ��5s      �� *ScreenNameLst+ 5t      �� *SearchScreen+$ TwoWay=���5v      �� 5w   "   .R�5x   &   �� .+�5y   *   �� $��0 3 0 5q�64   $
��Bold"�6i   ��6E   �� *dv_screen_csw_name+6i   �� 5z   *   #��[ 64   ��6^   �� *dv_screen_desc+5{   '   5|   #   5~      �� 5      $��控件类型��6    #��[ 6/   $
��Left=�6U   �� 5�      #���  6#   $	��200��6J   $��5,0,5,0q�6V   $ Description��5�      ��5�      ��    *M� *dv_config:ControlType++ �� Text 5�   9   �� *SearchControlType+�� 5�      $��名称/描述/AccessID��6    #��[ 69   $
��Left=�6^   �� 5�      #��[ 6=   $	��150��6d   ��6"   �� *	SearchTxt+6d   �� 5�      $	����6!    
 ImageButton#��� 
 64   $��50��6Z   $��Center=�6e    � ToolTip$ Query��5�   >   ��6   �� *QueryCommand+6>   �� 5�      $	����6!   #��� 
 64   $��50��6Z   $��Center=�6e   $ Reset��5�   >   ��6   �� *ResetCommand+6>   �� 5�      $��导入��6!   $��50��6U   #���  6`   $��5q�6�   ��6.   �� *ImportDataCommand+6�   �� 5�      $��导出��6!   $��50��6V   #���  6a   $��5q�6�   ��6/   �� *ExportDataCommand+6�   5�      �� 5�      5�      &   System.Windows.Controls.DataGrid  5�      $��1��6   $��5q�6(     NormalDataGrid#��[ 6e   ��64   �� *
ControlLst+ �� IsAsync .    Columns 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 操作��61    	  Width5
  /System.Windows.Controls.DataGridLengthConverter$	 120
 6?      CellTemplate 5�      �� 5�   "   .R�5�   &   �� $ 
Horizontal=�61   .+�5�   *   �� $��编辑��61   $��60��6?     
TextButton#��[ 6K   ��6p   �� *DataContext.EditCommand+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ DataGridL���5�   -   ��  �� $ FindAncestor=�$ DataGridL�  �� Path$  SelectedItem��� 5�   *   $��删除��61   $��60��6?   #��[ 6K   ��6o   �� *DataContext.DeleteCommand+ �� $ FindAncestor=�$ DataGridL���5�   -   ��  �� $ FindAncestor=�$ DataGridL�$  SelectedItem�5�   '   5�   #   5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $ 序号��6-   $ 80
 6W   1  +System.Windows.Controls.DataGridBoundColumn!   Binding! 69   �� *	ControlID+"   ElementStyle" 5�      �� 5�   "   .�5�   &   ��  � ��$ Center=�6[   5�   #   5�        5�      $ 控件名称（内部）��6-    " DataGridTextColumnElementStyle#" [ 6g   $ *
 6�   ! 6?   �� *ControlInternalName+5�        5�      $ 控件描述��6-   #" [ 6[   $ *
 6�   ! 6;   �� *ControlDesc+5�        5�      $ 控件AccessID��6-   #" [ 6^   $ *
 6�   ! 6A   �� *
AccessID+5�        5�      $ 控件类型��6-   #" [ 6[   $ *
 6�   ! 6;   �� *ControlType+5�        5�      $ 所在画面��6-   #" [ 6]   $ *
 6�   ! 6;   �� *ScreenCswName+5�      5�      5�      5�      5�      5�      