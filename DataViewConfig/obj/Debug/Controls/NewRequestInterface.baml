   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullA%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      /clr-namespace:DataViewConfigDataViewConfig  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5	      1    +DataViewConfig.Controls.NewRequestInterface   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:DataViewConfig.Controls     5      0currentclr-namespace:DataViewConfig     5      =	converter'clr-namespace:DataViewConfig.Converters     5      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5	      
�������  � 	Resources  5      5  /DataViewConfig.Converters.Bool2InverseConverter   Bool2Inverse8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visible>  8DataViewConfig.Converters.EcsComm_MQ2VisibilityConverter  EcsComm_MQ2Visible?  9DataViewConfig.Converters.EcsComm_OPC2VisibilityConverter  EcsComm_OPC2Visible��%t   &	        &	      &	 &     &	 9       5   
   6*     5   
   6-     5   
   63     5   
   64   5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      �� $��接口名称��6   $��5,0,5,0q�6$     
NormalText#��[ 65   �� 5        NormalTextBox#��[ 6   $	��150��6b   $��5,0,50,0q�6n   ��6=   �� *RequestInternalName+6n   �� 5!      $��交互对象��6   $��5,0,5,0q�6%   #��[ 66   �� 5"        NormalCombox#���  6   $	��100��6>   $��5,0,50,0q�6J    �� DisplayMemberPath$ Description��5$       �� SelectedValuePath$ Value��6c   ��5#      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:RequestSystemEnum++ �� SelectedValue 5$   7   �� *RequestSystemName+6c   �� 5&      $��前置条件��6   $��5,0,5,0q�6%   #��[ 66   �� 5'      #���  6   $	��200��6>   $��5,0,50,0q�6J   $ Description��5)      $ Value��6c   ��5(      ��    *M� *#!dv_config:RequestPreConditionType++ 5)   8   �� *PreconditionTyep+6c   �� 5+      $��参数列表��6   $��5,0,5,0q�6%   #��[ 66   �� 5,      #��[ 6   $	��100��6Y   $��5,0,5,0q�6f   ��6=   �� *
ParamIdLst+6f   �� 5-        NormalButton#���  6   $��选择��6<   $�� ��6p   $��5,0,10,0q�6�   $��50��6�   ��6I   �� *SelectParamCommand+6�   5.      �� 50   
   $��1��6   $ 
Horizontal=�6"   $��5q�6<   .+�51      �� $��交互方式��6   $��5,0,5,0q�6%   #��[ 66   �� 52      -   5         ��Name$ CommTypeCombox��52      #���  6/   $	��150��6V   $��5,0,50,0q�6b   $ Description��54      $ Value��6Y   ��53      ��    *M� *dv_config:ECSCommType++ 54   7   �� *	EcsComm+6Y   �� 57      $��参数分隔符��6�   #��[ 6�   y�6   ��  �� ElementName$ CommTypeCombox��	 �� Path$	 SelectedIndex�
 �� 	Converter#
 [ 6�   �� 58      #��[ 6�   $��30��6�   $��5,0,50,0q�6�   y�6   �� $ CommTypeCombox��$	 SelectedIndex�#
 [ ��6�   �� *ParamSeparator+6�   �� 5:      $��msg_type��6�   #��[ 6�   y�6   �� $ CommTypeCombox��$	 SelectedIndex�#
 [ 6�   �� 5;      #��[ 6�   $	��100��6�   $��5,0,50,0q�6�   y�6   �� $ CommTypeCombox��$	 SelectedIndex�#
 [ ��6�   �� *	MsgType+6�   �� 5=      $��	参数点��6�   $��5,0,5,0q�6�   #��[ 6�   y�6   �� $ CommTypeCombox��$	 SelectedIndex�#
 [ 6�   �� 5>      #���  6�   $	��170��6�   $��5,0,50,0q�6�   $ Value��5?   g   y�5>      �� $ CommTypeCombox��$	 SelectedIndex�#
 [ ��5?      �� *	ArgTagLst+ 69   �� *ArgsTagInternalName+6g   �� 5B      $��Event点��6�   $��5,0,5,0q�6�   #��[ 6�   y�6   �� $ CommTypeCombox��$	 SelectedIndex�#
 [ 6�   �� 5C      #���  6�   $	��170��6�   $��5,0,50,0q�6�   $ Value��5D   k   y�5C      �� $ CommTypeCombox��$	 SelectedIndex�#
 [ ��5D      �� *EventTagLst+ 6<   �� *EventTagInternalName+6k   �� 5F      $��	反馈点��6�   $��5,0,5,0q�6�   #��[ 6�   y�6   �� $ CommTypeCombox��$	 SelectedIndex�#
 [ 6�   �� 5G      #���  6�   $	��170��6�   $��5,0,50,0q�6�   $ Value��5H   l   y�5G      �� $ CommTypeCombox��$	 SelectedIndex�#
 [ ��5H      �� *ReturnTagLst+ 6<   �� *ReturnTagInternalName+6l   5J      �� 5K   
   $��2��6   $ 
Horizontal=�6"   $��5q�6<   .+�5L      �� $��接口返回值��6   $��5,0,5,0q�6&   #��[ 67   6]   �� 5M      $��0q�6   .��5N      �� $ 
Horizontal=�6   $��Center=�66   .+�5O      	� -   5         	�Name$ globalReturnRadioBtn��5O   "     NormalRadioButton#���  6e   $��通用返回值��6�   
�������$��5,0,50,0q�6�    d� 	IsChecked 6@   �� *IsGlobalReturn+6�   	� 5P      -   5        $ specialReturnRadioBtn��5P   "   #���  6�   $��单独定义返回值��6�   
�������$��5,0,15,0q�6�    6A   �� *IsGlobalReturn+#
 [  6�   �� 5Q      $��编辑��6�   #���  6�   $��5,0,5,0q�6�   $��50��6�   $�� ��6  y�6   �� $ specialReturnRadioBtn��$	 	IsChecked�#
 [ ��6�   �� *EditReturnCommand+6  5R      5S      �� 5T      #��[ 6�   $	��500��6�   $��5,0,50,0q�6�   y�6   �� $ specialReturnRadioBtn��$	 	IsChecked�#
 [ ��6�   �� *RequestReturnVal+6�   5U      �� 5W   
   $��3��6   $ 
Horizontal=�6"   $��Center=�6;   $��30q�6X   .+�5X      �� $��添加��6   #���  6"   $	��150��6I   $�� ��6t   ��6U   �� *
AddCommand+6t   5Y      5Z      5[      