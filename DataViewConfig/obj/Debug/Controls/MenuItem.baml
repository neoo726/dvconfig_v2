   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullA%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      9!clr-namespace:DataViewConfig.BaseDataViewConfig.Base  6I   &     DataViewConfig.Controls.MenuItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:DataViewConfig.Controls     5      =	converter'clr-namespace:DataViewConfig.Converters     5      2base!clr-namespace:DataViewConfig.Base     6I   
������   � 	Resources  5
      8  2DataViewConfig.Converters.Bool2VisibilityConverter   Bool2Visible&    DataViewConfig.Base.BindingProxy  BindingProxy   Data��%C   &	        &	        5   
   6-     5   
    61   �� 5      .��5      � .+�5   
   9� -   5         9�Name$ ExpanderMenu��5      $��22(�6)    9� 
IsExpanded . $��0q�6I   ���#201f2d���#252526
���������5      �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ AddBtn��5      $	����6-   $��Center=�6@     ImageButton#��[ 6\   5      �� 5      $��Center=�68   
���������6    �� *Header+6S   5      .��5      �� -   5         ��Name$ ListViewMenu��5      
������ 
������ ��5      �� *MenuItemLst+ �� UpdateSourceTrigger$ PropertyChanged=���5      �� 5       �� 
TargetType" ��6   .�5&      �� 	 �� Property	 � ��
 �� Value

 ���� 67   �� 5'      	 � ��

 ���� 68   -   5         �� Triggers 5)      P� 5*       P� Property �@J�	IsFocused P� Value . . �5+   "   �� 	 � ��

 � ���6?   5,      P� 5-       �@��
IsSelected . . �5.   "   �� 	 � ��

 �  ��6?   5/      58      ��5:      �� 5;      .R�5<      �� $ 
Horizontal=�6%   $��Stretch=�6>   .+�5=      ��  �� Padding$ 10q�6(   
�������$��NoWrap=�6^   ��6H   �� *Name+6^   �� 5>       � ToolTip$ Communication Enable ��6w     CheckBoxBaseStyle#��[ 6�   y�6'   �� *IsDynamicCreated+ �� 	Converter# [   d� 	IsChecked 6�   �� *
IsEnable+ �� Mode$ TwoWay=��� 5?      $ 	DeleteBtn��6%   $��10 0 0 0q�6�   $ Delete��6�   $	����6�   $��Center=�6   #��[ 6  y�68   �� *IsDeleteEnable+# [  ��6�   �� *DeleteCommand+��6�   �� *Name+6  5@      5A      5C      5D      5E      5F      