   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null;"clr-namespace:DataViewConfig.PagesDataViewConfig.Pages  5      A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      /clr-namespace:DataViewConfigDataViewConfig  5	      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5
      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      1    +DataViewConfig.Pages.CommonReturnConfigPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:DataViewConfig.Pages     5      :controls%clr-namespace:DataViewConfig.Controls     5      =	converter'clr-namespace:DataViewConfig.Converters     5      0currentclr-namespace:DataViewConfig     5	      6vm'clr-namespace:DataViewConfig.ViewModels     5
      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5      6sys&clr-namespace:System;assembly=mscorlib     5      ���#111111
�������$��15(�5   6   $��5q�5        � 	Resources  5      8  2DataViewConfig.Converters.Bool2VisibilityConverter   Bool2Visibility  tagDataEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type(  "DataView_Configuration.TagDataType  postfixtEnum+  %DataView_Configuration.TagPostfixType��%�   &	        &	      &	 n       5   
   6-   S� 5   
   $ 	GetValues��61   "  6H    5      M� 5      "  6   5      S� 5   
   $ 	GetValues��62   "  6I    5      M� 5      "  6   5      5      .��5       ��  �� Orientation$ Vertical=�6   .+�5"   
   �� .+�5#      ]� $
��auto��6   ���#262626��.  .+�5$      �� $��新增返回值��6   $��5q�6*   $
��Left=�65   $��Center=�6K   5*      � 5+       � RowDefinitions 5,      �� 5-      �� 5.      �� 5/      .+�52      �� $��0��6   $ 
Horizontal=�6*   $��5q�6C   .+�53      �� $��	返回值��6   $��5,0,5,0q�6*   $	��100��6;   $
��Left=�6J     NormalLabel#��[ 6l   �� 54        NormalTextBox#��[ 6   $	��200��6`   ��6E   �� *	ReturnVal+6`   �� 56      $��中文描述��6   $��5,0,5,0q�6+   $	��100��6=   #��[ 6L   �� 57      #��[ 6   $	��200��6c   $��5,0,50,0q�6o   ��6E   �� *ReturnDescZh+6o   �� 59      $��英文描述��6   $��5,0,5,0q�6+   #��[ 6<   �� 5:      #��[ 6   $	��250��6c   $��5,0,50,0q�6o   ��6E   �� *ReturnDescEn+6o   5;      �� 5=      $��1��6   $ 
Horizontal=�6*   $��5q�6D   .+�5>      �� $��系统名称��6   $��5,0,5,0q�6,   $
��Left=�6>   $	��100��6a   #��[ 6p   �� 5?        NormalCombox#���  6   $	��150��6F    �� DisplayMemberPath$ system_desc��5A       �� SelectedValue$ 	system_id��67   ��5@      �� *DvSystemLst+	 �� Text	 5A   Q   �� *system_name+�� 5B      $��交互成功的返回值��6    
�������$��Center=�6F   $	��200��6a   $��5,0,50,0q�6m   
 d� 	IsChecked
 6�   �� *IsSuccessFlag+5C      �� 5E      $��2��6   $ 
Horizontal=�6*   $��Center=�6C   $��10q�6`   .+�5F      �� $��添加��6     NormalButton#���  6*   $	��150��6Q   ��5G      �� *AddNewCommand+5H      5I      5J      �� 5L   
   $ Vertical=�6   .+�5M      ]� $
��auto��6   ���#262626��. .+�5N      �� $��通用返回值列表��6   #��[ 6*   $
��Left=�6O   +   %DataViewConfig.Controls.SearchTextBox  5O      $��Right=�6*   $	��300��6A      
PreviewTxt$" 输入返回值数值/描述��6M   $��0 0 10 0q�6�      
BtnCommand 6e   �� *QueryCommand+6�   �� 5P      5Q      &   System.Windows.Controls.DataGrid  5R      $��5q�6   $��15(�6"   $	��600��6U   $
��1280��6e      
IsReadOnly . $   HorizontalScrollBarVisibility$ Visible=�6�      AutoGenerateColumns .     CanUserAddRows .  ��.     AlternatingRowBackground �#000000 �� AlternationCount$ 2��6-  ��60   �� *ReturnValLst+  5S      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Property �� Value �� 	Resources �� Color �� Triggers P� Property P� Value��%j  '        �� 5T      "  6   .�5U      ��  � ��$ 15(�65    5V      ���� 5W      $ White��6u   (��       6.   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6u   5Y       5Z      P� 5[       �@ 
IsSelected . . �5]   "   ��  � �� �#4340416?   �� 5^   "    � ��
 ���� 6@   5_      5a      5b         Columns 5c      4  .System.Windows.Controls.DataGridTemplateColumn  5d      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 操作��6-      CellTemplate 5e      �� 5f      .R�5g   "   �� $ 
Horizontal=�6-   .+�5h   &   �� $��编辑��6-     
TextButton#��[ 6:   ��6_   �� *DataContext.EditCommand+ �� RelativeSource ��  �� Mode$ FindAncestor=�  �� AncestorType$  DataGridL���5i   -   ��  �� $ FindAncestor=�$  DataGridL�! �� Path$! SelectedItem��� 5j   &   $��删除��6-   #��[ 6:   ��6^   �� *DataContext.DeleteCommand+ �� $ FindAncestor=�$  DataGridL���5k   -   ��  �� $ FindAncestor=�$  DataGridL�$! SelectedItem�5m   #   5n      5p      0
  *System.Windows.Controls.DataGridTextColumn
  5q      $ id��6)   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 65   �� *id+#   ElementStyle# 5r      �� 5s      .�5t   "   ��  � ��$ Center=�6W   5u      5w      
  5x      $ 系统名称��6)   " 67   �� *	system_id+# 5y      �� 5z      .�5{   "   ��  � ��$ Center=�6W   5|      5~      
  5      $ 	返回值��6)   " 66   �� *return_value+# 5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�      
  5�      $ 中文描述��6)   " 67   �� *return_desc_zh+# 5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�      
  5�      $ 英文描述��6)   " 67   �� *return_desc_en+# 5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�      
  5�      $ 是否为成功标记��6)   " 6:   �� *
is_success+# 5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�      5�      5�      5�      5�      