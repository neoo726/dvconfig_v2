   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null;"clr-namespace:DataViewConfig.PagesDataViewConfig.Pages  5      A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5	      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5
      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      2    ,DataViewConfig.Pages.FanoutReceiveConfigPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:DataViewConfig.Pages     5      :controls%clr-namespace:DataViewConfig.Controls     5      =	converter'clr-namespace:DataViewConfig.Converters     5      6vm'clr-namespace:DataViewConfig.ViewModels     5	      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5
      6sys&clr-namespace:System;assembly=mscorlib     5      ���#111111
�������$��12(�5   6   $��5q�5        � 	Resources  5      8  2DataViewConfig.Converters.Bool2VisibilityConverter   Bool2Visibility  systemNameEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type.  (DataView_Configuration.RequestSystemEnum  preconditionEnum4  .DataView_Configuration.RequestPreConditionType  ecsCommEnum(  "DataView_Configuration.ECSCommType  receiveTypeEnum.  (DataView_Configuration.DvReceiveTypeEnum  receiveWriteTypeEnum3  -DataView_Configuration.DvReceiveWriteTypeEnum  receiveStoreTypeEnum3  -DataView_Configuration.DvReceiveStoreTypeEnum��%{  &	        &	      &	 n     &	 �     &	 $    &	     &	 �      5   
   6-   S� 5   
   $ 	GetValues��64   "  6K    5      M� 5      "  6   5      S� 5   
   $ 	GetValues��66   "  6M    5      M� 5      "  6   5      S� 5   
   $ 	GetValues��61   "  6H    5      M� 5      "  6   5!      S� 5"   
   $ 	GetValues��65   "  6L    5#      M� 5$      "  6   5&      S� 5'   
   $ 	GetValues��6:   "  6Q    5(      M� 5)      "  6   5+      S� 5,   
   $ 	GetValues��6:   "  6Q    5-      M� 5.      "  6   50      51      .��52      ��  �� Orientation$ Vertical=�6   .+�54   
   �� .+�55      ]� $
��auto��6   ���#262626��.  .+�56      �� $��新增Fanout接口��6   $��5q�6/   $
��Left=�6:   $��Center=�6P   �� 57      $��添加��6   $��50��6'     NormalButton#���  62   $��5q�6Y   ��58      �� *AddNewCommand+5=      5>      �� 5@   
   $ Vertical=�6   .+�5B      ]� $
��auto��6   ���#262626��. .+�5C      �� $"��广播（Fanout)接口列表��6     NormalLabel#��[ 61   $
��Left=�6V   +	   %DataViewConfig.Controls.SearchTextBox	  5D      $��Right=�6*   $	��300��6A    	  
PreviewTxt$ 输入msg_type/描述��6M   $��0 0 10 0q�6�    	  
BtnCommand 6h   �� *QueryCommand+6�   �� 5E      5F      &
   System.Windows.Controls.DataGrid
  5G      -   5         
 Name$ requestDgrid��5G      $��5q�6-   $	��650��68   $
��1280��6H   	 
  
IsReadOnly	 . $
 
  HorizontalScrollBarVisibility$
 Visible=�6�    
  AutoGenerateColumns .   
  CanUserAddRows .  ��.   
  AlternatingRowBackground �#000000 �� AlternationCount$ 2��6G  ��6Y   �� *ReceiveFanoutLst+ �� Mode$ TwoWay=�  5H      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� 	Resources �� Color �� Triggers P� Property P� Value �� Property �� Value��%C  '        �� 5I      "  6    5J      ���� 5K      $ White��6u   (��       6.   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6u   5M       5N      P� 5O       �@ 
IsSelected . . �5Q   "   ��  � �� �#4340416?   �� 5R   "    � ��
 ���� 6@   5S      5U      5V       
  Columns 5W      4  .System.Windows.Controls.DataGridTemplateColumn  5X      ,  &System.Windows.Controls.DataGridColumn   Header$ 操作��6-      CellTemplate 5Y      �� 5Z      .R�5[   "   �� $ 
Horizontal=�6-   .+�5\   &   �� $��编辑��6-    	 
TextButton#��[	 6:   ��6_   �� *DataContext.EditCommand+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ DataGridL���5]   -   ��  �� $ FindAncestor=�$ DataGridL� �� Path$ SelectedItem��� 5^   &   $��删除��6-   #��[	 6;   ��6_   �� *DataContext.DeleteCommand+ �� $ FindAncestor=�$ DataGridL���5_   -   ��  �� $ FindAncestor=�$ DataGridL�$ SelectedItem�5`   #   5a      5c      0  *System.Windows.Controls.DataGridTextColumn  5d      $ 序号��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+    ElementStyle  5e      �� 5f      .�5g   "   ��  � ��$ Center=�6W   5h      5j        5k      $ msg_type��6)    6;   �� *	MsgType+  5l      �� 5m      .�5n   "   ��  � ��$ Center=�6W   5o      5q        5r      $ 交互对象��6)    67   �� *
SystemId+  5s      �� 5t      .�5u   "   ��  � ��$ Center=�6W   5v      5x        5y      $ 描述��6)    65   �� *
FanoutDesc+  5z      �� 5{      .�5|   "   ��  � ��$ Center=�6W   5}      5        5�      $" 设备/操作台区分字段��6)    6=   �� *DeviceField+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 接收目标类型��6)    69   �� *ReceiveType+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 数据存储类型��6)    69   �� *ReceiveStoreType+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 写Memory点类型��6)    6=   �� *ReceiveWriteType+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 匹配字段名称��6)    69   �� * CacheWriteCondition.field_name+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 	匹配值��6)    66   �� *CacheWriteCondition.match_tag+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 匹配点名（内部）��6)    6;   �� *%#DestTagName.event_tag_internal_name+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 缓存更新点名��6)    69   �� *CacheTagInternalName+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�        5�      $ 全量存储更新点名��6)    6;   �� *FullStoreTagInternalName+  5�      �� 5�      .�5�   "   ��  � ��$ Center=�6W   5�      5�      5�      5�      5�      5�      