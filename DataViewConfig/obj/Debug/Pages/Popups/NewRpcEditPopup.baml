   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   /clr-namespace:DataViewConfigDataViewConfig  5
   	   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   1    +DataViewConfig.Pages.Popups.NewRpcEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   0currentclr-namespace:DataViewConfig     5
   	   =	converter'clr-namespace:DataViewConfig.Converters     5   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  6<   -   5          �Name$  NewRpcEditPopupWin��5      $	��750��5   	   $	��950��6   
������� � WindowStartupLocation$ CenterScreen=�5   	      NormalBackGround#��[  5   	     NormalFontSize#��[ 68    � 
ResizeMode$ NoResize=�5   	    � Title$ 接口编辑��6     � SizeToContent$ WidthAndHeight=�6/   $
��1280��6N   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      D  >DataViewConfig.Converters.ParamSelectOther2VisibilityConverter  ParamSelectOther2Visibility?  9DataViewConfig.Converters.ParamAddNew2VisibilityConverter  ParamAddNew2VisibilityB   <DataViewConfig.Converters.ParamEditExist2VisibilityConverter  ParamEditExist2VisibilityB  <DataViewConfig.Converters.ParamAddOrEidt2VisibilityConverter  ParamEditOrAdd2VisibilityA  ;DataViewConfig.Converters.ParamEidt2ReverseBooleanConverter  ParamEdit2ReverseBool8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2REverse?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter 	 Bool2ReverseVisibility>	  8DataViewConfig.Converters.EcsComm_MQ2VisibilityConverter 
 EcsCommTypeMQ2Visibility?
  9DataViewConfig.Converters.EcsComm_OPC2VisibilityConverter  EcsCommTypeOPC2Visibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool<  6DataViewConfig.Converters.Bool2DisplayContentConverter  Bool2DisplayYesOrNo:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2BoolA  ;DataViewConfig.Converters.ComboxControl2VisibilityConverter  ComboxCtrl2Visibility?  9DataViewConfig.Converters.ComboxSelectIndex2BoolConverter  ComboxIndex2Bool>  8DataViewConfig.Converters.ComboxSelectText2BoolConverter  ComboxText2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters	 M� Type,  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource'  !System.Windows.Shell.WindowChrome  WIndowChromeKey
   ResizeBorderThickness   CaptionHeight��%I  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 �     &	 �     &	 
    &	     &	 0    &	 C    &	 V    &	 i    &	 �    &	       5   
   69     5   
   64     5   
   67     5   
   67     5   
   66     5   
   6-     5   
   6*     5    
   64   	  5!   
   63   
  5"   
   64     5#   
   6-     5$   
   6.     5%   
   61     5&   
   6/     5'   
   68     5(   
   64     5)   
   66     5*   
   64     5+   
   63   S� 5,   
   $ 	GetValues��64   "  6K    5-      M� 5.      "	  6   50      S� 51   
   $ 	GetValues��65   "  6L    52      M� 53      "	  6   55        56   
   
 57      u�58      5q�6   $ 0Q�5:   G   5;      5<      .��5=      �� .��5>   
   � -   5         �Name$
 grid��5>      ���#1f1f1f � RowDefinitions 5?      �� 5@      �� 5A      $��80 �6!   .+�5I      �� $��0��6   $��0q�6'    �� Orientation$ Vertical=�62   .+�5J      �� $��接口基础信息��6     NormalGroupBox#��[ 6+   .��5K      �� $ Vertical=�6!   .+�5L      �� $ 
Horizontal=�6%   $��5q�6>   .+�5M      �� $ 
Horizontal=�6)   .+�5N   "   �� $��接口名称��6,   $��5,0,5,0q�68     
NormalText#��[ 6J   '   !DataViewConfig.Controls.TipsLabel  5O   "      TipName$! [rpc_edit]rpc_internal_name��67   5P      �� 5Q        NormalTextBox#��[ 6&   $	��300��6r   $��5,0,30,0q�6~   ��6M   �� *RequestInternalName+6~   �� 5R      $��接口描述��6(   $��5,0,5,0q�64   #��[ 6F   �� 5S      #��[ 6&   $	��300��6j   $��5,0,30,0q�6v   ��6M   �� *RequestDesc+6v   5U      �� 5V      -   5         ��Name$ msgTypePanel��5V   %   $ 
Horizontal=�6;   $��5q�6U   y�5W      �� *	EcsComm+ �� 	Converter# [
 .+�5X      �� $��msg_type / api url��6)   $��5,0,35,0q�6C   #��[ 6W   �� 5Y      #��[ 6)   $	��250��6i   $��	20,0,30,0q�6u   ��6P   �� *	MsgType+6u   5Z      �� 5[      -   5        $ msgTypePanel1��5[   %   $ 
Horizontal=�6<   $��5q�6V   y�5\      �� *	EcsComm+# [ .+�5]      �� $ 
Horizontal=�6)   y�6B   ��  �� ElementName$% tagCmdIdIndexValOpcTypeRadioBtn�� �� Path$ 	IsChecked�# [ .+�5^   "   �� $��Index��6-   $��5,0,35,0q�6:   #��[ 6O   �� 5_   "   #��[ 6-   $	��250��6m   $��	20,0,30,0q�6y   ��6T   �� *	MsgType+6y   5`      5a      �� 5b      $ 
Horizontal=�6%   $��5q�6>   .+�5c      �� $��弹窗提示设置��6)   $��5,0,5,0q�67   $	��100��6I   #��[ 6U   6{   �� 5d      $��成功后弹窗提示��6Q   
�������$��5,0,15,0q�6v    d� 	IsChecked 6(   �� *IsSuccessShowTips+6v   �� 5e      $��失败后弹窗提示��6Q   
�������$��5,0,50,0q�6v    6)   �� *IsFailedShowTips+6v   5f      5g      5h      �� 5k      $��接口参数配置��6   #��[ 6+   .��5l      �� $ Vertical=�6!   .+�5n      �� $ Vertical=�6%   y�5o      �� *	EcsComm+# [ .+�5p      #  HandyControl.Controls.Divider     Content$ OPC交互配置��6)   
�������6;   �� 5q      $ 
Horizontal=�6)   .+�5r   "   �� $ 
Horizontal=�6-   $	��130��6F   $��5,0,5,0q�6R   .+�5s   &   �� $��OPC交互类型��6,     NormalLabel#��[ 6B     5t   &   $ [rpc_edit]opc_comm_type��6;   5u   #   	� 5v   "   -   5         	�Name$  tagFeedbackOpctypeRadioBtn��5v   .   $!��写单个点，等待反馈��5w   .     NormalRadioButton#��[ 6C    5v   R   �� *IsWriteSingleTag+5w   C   	� 5x   "   -   5        $! tagArgsEventOpcTypeRadioBtn��5x   .   $ ��写args/event,等待反馈��5y   .   #��[ 6I    5x   S   �� *IsWriteArgsEventsTag+5y   I   	� 5z   "   -   5        $% tagCmdIdIndexValOpcTypeRadioBtn��5z   .   $'��!写CmdId/Index/Value,等待反馈��5{   /   #��[ 6Q    5z   X   �� *IsWriteCmdIdIndexValueTag+5{   Q   5|      �� 5~      $ Vertical=�6)   y�6@   �� $  tagFeedbackOpctypeRadioBtn��$ 	IsChecked�# [ .+�5   "   �� $ 
Horizontal=�6/   $��5q�6H   .+�5�   &   �� $��写点��62   $��5,0,5,0q�6<   $	��100��6M   #��[ 6Z   6�   �� 5�   &   -   5         ��Name$&  opcWriteSingleTagSelectionCombox��5�   /   $	��250��6Z   $��5,0,50,0q�6f     NormalCombox#��[ 6x    �� 
IsEditable .  �� IsTextSearchEnabled .   �� IsTextSearchCaseSensitive .  w� TextPath$ tag_internal_name��5�   <   
�������
���   ���5�      �� *TagLst+ �� SelectedItem 5�      �� *
RequestTag+��5�   *   �� 5�   .   .R�5�   2   �� .+�5�   6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5�   6     Global.Gray#��[ 6@   ��6j   �� *
tag_desc+5�   3   5�   /   5�   '   �� 5�   &   $��	反馈点��62   $��5,0,5,0q�6=   $	��100��6N   #��[ 6[   6�   �� 5�   &   -	   5        $) #opcFeedbackSingleTagSelectionCombox��5�   /   $	��250��6]   $��5,0,50,0q�6i   #��[ 6{    .  .  . $ tag_internal_name��5�   D   
�������
���   ���5�      �� *TagLst+ 5�      �� *RequestFeedbackTag+��5�   *   �� 5�   .   .R�5�   2   �� .+�5�   6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5�   6   #��[ 6@   ��6j   �� *
tag_desc+5�   3   5�   /   5�   '   5�   #   5�      �� 5�      $ Vertical=�6)   y�6@   �� $! tagArgsEventOpcTypeRadioBtn��$ 	IsChecked�# [ .+�5�   "   �� -
   5        $ paramSepartorPanel��5�   -   $ 
Horizontal=�6I   $��5q�6b   .+�5�   &   �� $��参数分隔符��61   $��5,0,5,0q�6>   $	��100��6P   #��[ 6\   �� 5�   &   #��[ 61   $	��100��6x   $��5,0,50,0q�6�   ��6X   �� *ParamSeperator+6�   �� 5�   &   -   5        $ eventTagPanel��5�   1   $ 
Horizontal=�6I   $��5q�6b   .+�5�   *   �� $��Event点��66   $��	150,0,5,0q�6D   $	��100��6W   #��[ 6d   6�   �� 5�   *   -   5        $% opcWriteEventTagSelectionCombox��5�   3   $	��250��6]   $��5,0,50,0q�6i   #��[ 6{    .  .  . $ tag_internal_name��5�   :   
�������
���   ���5�      �� *TagLst+ 5�      �� *
EventTag+��5�   .   �� 5�   2   .R�5�   6   �� .+�5�   :   �� $��0 3 0 5q�6D   $
��Bold"�6x   ��6U   �� *tag_internal_name+6x   �� 5�   :   #��[ 6D   ��6n   �� *
tag_desc+5�   7   5�   3   5�   +   5�   '   5�   #   �� 5�   "   -   5        $ argsTagPanel��5�   -   $ 
Horizontal=�6C   .+�5�   &   �� $��	参数点��62   $��10,0,5,0q�6=   $	��100��6P   #��[ 6\   6�   �� 5�   &   -   5        $$ opcWriteArgsTagSelectionCombox��5�   /   $	��250��6X   $��5,0,50,0q�6d   #��[ 6v    .  .  . $ tag_internal_name��5�   9   
�������
���   ���5�      �� *TagLst+ 5�      �� *	ArgsTag+��5�   *   �� 5�   .   .R�5�   2   �� .+�5�   6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5�   6   #��[ 6@   ��6j   �� *
tag_desc+5�   3   5�   /   5�   '   �� 5�   &   -   5        $ returnTagPanel��5�   1   $ 
Horizontal=�6J   .+�5�   *   �� $��	反馈点��66   $��5,0,5,0q�6A   $	��100��6S   #��[ 6_   6�   �� 5�   *   -   5        $' !opcReadFeedbackTagSelectionCombox��5�   3   $	��250��6_   $��5,0,50,0q�6k   #��[ 6}    .  .  . $ tag_internal_name��5�   ;   
�������
���   ���5�      �� *TagLst+ 5�      �� *	ReturnTag+��5�   .   �� 5�   2   .R�5�   6   �� .+�5�   :   �� $��0 3 0 5q�6D   $
��Bold"�6x   ��6U   �� *tag_internal_name+6x   �� 5�   :   #��[ 6D   ��6n   �� *
tag_desc+5�   7   5�   3   5�   +   5�   '   5�   #   �� 5�   "   -   5        $ returnTagTypePanel��5�   -   $ 
Horizontal=�6J   $��5q�6c   5  #   5
     �� 5     $ Vertical=�6)   y�6@   �� $% tagCmdIdIndexValOpcTypeRadioBtn��$ 	IsChecked�# [ .+�5  "   �� -   5        $ eventTagPanel1��5  -   $ 
Horizontal=�6F   $��5q�6_   .+�5  &   �� $��DataView->PLC��62   $��0,0,5,0q�6G   $	��150��6X   #��[ 6e   6�   �� 5  &   $��	CommandId��62   $��0,0,5,0q�6C   $	��100��6T   #��[ 6a   6�   �� 5  &   -   5        $&  opcWriteEventTagSelectionCombox1��5  /   $	��250��6Z   $��5,0,10,0q�6f   #��[ 6x    .  .  . $ tag_internal_name��5  +   
�������
���   ���5     �� *TagLst+ 5     �� *CmdIdTag_Dv2Plc+��5  *   �� 5  .   .R�5  2   �� .+�5  6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5  6   #��[ 6@   ��6j   �� *
tag_desc+5  3   5  /   5  '   �� 5   &   $��Index��62   $��10,0,5,0q�6?   $��50��6R   #��[ 6]   6�   �� 5"  &   -   5        $% opcWriteArgsTagSelectionCombox1��5"  /   $	��250��6Y   $��5,0,10,0q�6e   #��[ 6w    .  .  . $ tag_internal_name��5$  .   
�������
���   ���5#     �� *TagLst+ 5$     �� *CmdIndexTag_Dv2Plc+��5%  *   �� 5&  .   .R�5'  2   �� .+�5(  6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5)  6   #��[ 6@   ��6j   �� *
tag_desc+5*  3   5+  /   5-  '   �� 5/  &   -   5        $ returnTagPanel2��5/  1   $ 
Horizontal=�6K   .+�50  *   �� $��Value��66   $��5,0,5,0q�6C   $��50��6U   #��[ 6`   6�   �� 52  *   -   5        $( "opcReadFeedbackTagSelectionCombox2��52  3   $	��250��6`   $��5,0,10,0q�6l   #��[ 6~    .  .  . $ tag_internal_name��54  .   
�������
���   ���53     �� *TagLst+ 54     �� *CmdValueTag_Dv2Plc+��55  .   �� 56  2   .R�57  6   �� .+�58  :   �� $��0 3 0 5q�6D   $
��Bold"�6x   ��6U   �� *tag_internal_name+6x   �� 59  :   #��[ 6D   ��6n   �� *
tag_desc+5:  7   5;  3   5=  +   5>  '   5@  #     5A  "   $��1��6-   $��5q�68   ���#1f1f1f6\   �� 5C  "   -   5        $ eventTagPanel2��5C  -   $ 
Horizontal=�6F   $��5q�6_   .+�5D  &   �� $��PLC->DataView��62   $��0,0,5,0q�6G   $	��150��6X   #��[ 6e   6�   �� 5E  &   $��	CommandId��62   $��0,0,5,0q�6C   $	��100��6T   #��[ 6a   6�   �� 5G  &   -   5        $&  opcWriteEventTagSelectionCombox2��5G  /   $	��250��6Z   $��5,0,10,0q�6f   #��[ 6x    .  .  . $ tag_internal_name��5I  +   
�������
���   ���5H     �� *TagLst+ 5I     �� *CmdIdTag_Plc2Dv+��5J  *   �� 5K  .   .R�5L  2   �� .+�5M  6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5N  6   #��[ 6@   ��6j   �� *
tag_desc+5O  3   5P  /   5R  '   �� 5T  &   $��Index��62   $��10,0,5,0q�6?   $��50��6R   #��[ 6]   6�   �� 5V  &   -   5        $% opcWriteArgsTagSelectionCombox2��5V  /   $	��250��6Y   $��5,0,10,0q�6e   #��[ 6w    .  .  . $ tag_internal_name��5X  .   
�������
���   ���5W     �� *TagLst+ 5X     �� *CmdIndexTag_Plc2Dv+��5Y  *   �� 5Z  .   .R�5[  2   �� .+�5\  6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5]  6   #��[ 6@   ��6j   �� *
tag_desc+5^  3   5_  /   5a  '   �� 5c  &   -   5        $ returnTagPanel3��5c  1   $ 
Horizontal=�6K   .+�5d  *   �� $��Value��66   $��5,0,5,0q�6C   $��50��6U   #��[ 6`   6�   �� 5f  *   -   5        $( "opcReadFeedbackTagSelectionCombox3��5f  3   $	��250��6`   $��5,0,10,0q�6l   #��[ 6~    .  .  . $ tag_internal_name��5h  .   
�������
���   ���5g     �� *TagLst+ 5h     �� *CmdValueTag_Plc2Dv+��5i  .   �� 5j  2   .R�5k  6   �� .+�5l  :   �� $��0 3 0 5q�6D   $
��Bold"�6x   ��6U   �� *tag_internal_name+6x   �� 5m  :   #��[ 6D   ��6n   �� *
tag_desc+5n  7   5o  3   5q  +   5r  '   5s  #   �� 5u  "   -   5        $ eventTagPanel3��5u  -   $ 
Horizontal=�6F   $��5q�6_   .+�5v  &   �� $��Return��62   $��	155,0,5,0q�6@   $	��100��6S   #��[ 6`   6�   �� 5w  &   -   5        $&  opcWriteEventTagSelectionCombox3��5w  /   $	��250��6Z   $��5,0,10,0q�6f   #��[ 6x    .  .  . $ tag_internal_name��5y  /   
�������
���   ���5x     �� *TagLst+ 5y     �� *CmdReturnTag_Plc2Dv+��5z  *   �� 5{  .   .R�5|  2   �� .+�5}  6   �� $��0 3 0 5q�6@   $
��Bold"�6t   ��6Q   �� *tag_internal_name+6t   �� 5~  6   #��[ 6@   ��6j   �� *
tag_desc+5  3   5�  /   5�  '   5�  #   5�     5�     �� 5�     $ Vertical=�6%   .+�5�     � ���#1f1f1fy�6#   �� *ShowParamEditExpander+# [  5�  "   �� 5�  &   �� 5�  &   �� 5�  &   �� 5�  &   $��80 �65    � ColumnDefinitions 5�  "   �� 5�  &   �� 5�  &   .+�5�  "   �� $��0��6.   $��0��6;   $ Vertical=�5�  &   .+�5�  &   �� $ 
Horizontal=�61   $��5q�6J   .+�5�  *   �� $��参数名称��60   $	��100��6?   #��[ 6M   �� 5�  *   
�������
������ #��[ 5�  T   $	��200��5�  U   $��5,0,50,0q�6a   y�5�  2   �� *ParamChange+# [ ��5�  -   �� *ParameterInternalName+6a   �� 5�  *   #���  5�  %   $	��200��6L   $��5,0,50,0q�6X   $
��Left=�6j   y�5�  %   �� *ParamChange+# [ ��5�  %   �� *AllParamLst+ 6J   �� *SelectedParam+��5�  .   :   4DataViewConfig.Controls.ComboBoxItemTemplateSelector  5�  2      SelectedTemplate 5�  6   �� 5�  :   .R�5�  >   �� 
�������#��[ 5�  R   ��5�  H   �� *	ParamName+5�  R   5�  ;      DropDownTemplate 5�  6   �� 5�  :   .R�5�  >   ��   Content.BackGround#��[ 6I   .+�5�  B   �� 
�������$��0 0 0 1q�6\   $��1q�6v   ._�5�  F   �� 
������ $ Vertical=�6j   $	��300��6�   $
��Left=�6�   .+�5�  J   �� $��Center=�6o   $
��Left=�6�   
�������#��[ 6�   ��6T   �� *	ParamName+6�   �� 5�  J   $��Center=�6n   $
��Left=�6�   $��	-5 0 0 0 q�6�   #��[ 6�   #��[ 6�   ��6P   �� *	ParamDesc+6�   5�  G   5�  C   5�  ?   5�  ;   5�  3   5�  +   5�  '   5�  #   �� 5�  "   $��1��6.   $��0��6;   $��2��6K   $ Vertical=�6_   .+�5�  &   �� $ 
Horizontal=�61   $��5q�6J   
������ .+�5�  *   �� $��参数信息��60   $	��100��6?   #��[ 6M   �� 5�  *      TextBlockDefaultThiLight#��[  6T   $
��auto��6�     �� 
LineHeight$  25��6�   ��64   �� *ParamTotalInfo+6�   5�  '   5�  #   �� 5�  "   $��3��6-   $��0��6:   $��2��6J   $ 
Horizontal=�6^   $��Right=�6w   .+�5�  &   �� $��确认选择��5�  !   $	��150��61   $��35��6=   $��10q�6I    ! NormalButton#��[! 6U   y�5�  .   �� *ParamChange+# [ ��5�     �� *ConfirmSelectParameterCommand+��6H   �� $ ParamterEditPopupWin��6�   �� 5�  &   $��取消��6.   ���#4c4c4c$	��150��6P   $��35��6]   $��10q�6i   #��[! 6u   ��5�     �� *CancelAddParameterCommand+��6E   �� $ ParamterEditPopupWin��6�   5�  #   5�     �� 5�     $ 
Horizontal=�6)   .+�5�  "   �� $��参数列表��6(   #��[ 67   �� 5�  "   $��选择参数��6)   $	��120��69   #��[! 5�  )   ��5�  )   �� *SelectRpcParamCommand+5�  #   5�     &   System.Windows.Controls.DataGrid  5�     $��5q�6(   #��[ 63   !   AutoGenerateColumns! .  "   CanUserAddRows" .  ��6^   �� *DisplayParamLst+ 5�  "   *  $System.Windows.Controls.DataGridCell# �� 
TargetType$ �� Property% �� Value& �� 	Resources' �� Color( �� Triggers) P� Property* P� Value��%j  '        �� 5�  &   "#  6,   .�5�  *   �� $ � ��$% 15(�6E   & 5�  *   ���� 5�  .   $' White��6�   (��       6>   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6�   5�  +   ( 5�  *   P� 5�  .   ) �@ 
IsSelected* . . �5�  2   �� $ � ��% �#4340416O   �� 5�  2   $ � ��
% ���� 6P   5�  /   5�  '   5�  #   +   RowStyle+ 5�  "   �� 5�  &   )  #System.Windows.Controls.DataGridRow"#  6,   .�5�  *   �� $ � ��$% 35��6C   �� 5�  *   $ � ��#% [ 6E   ( 5�  *   �� 5�  .   , �� Binding, 6:   �� *
IsSelected+- �� Value- true6Y   .a�5�  2   �� $ � ��% �#0078d76a   5�  /   �� 5�  .   , 6:   �� *
IsSelected+	- false6Y   .a�5�  2   �� $ � ��% �#1f1f1f6a   5�  /   5�  '   .   Columns. 5�  "   4  .System.Windows.Controls.DataGridTemplateColumn  5�  &   ,  &System.Windows.Controls.DataGridColumn/   Header$/ 操作��6=   0   Width5   /System.Windows.Controls.DataGridLengthConverter$	0 120  6I   1   CellTemplate1 5�  *   �� 5�  .   .R�5�  2   �� $ 
Horizontal=�6=   .+�5�  6   �� $��编辑��6=   $��60��6J   #��[! 6U   ��6|   �� *!DataContext.EditRpcParamCommand+2 �� RelativeSource2 �� 3 �� Mode$3 FindAncestor=�4 �� AncestorType$4 DataGridL���5�  -   �� 2 �� $3 FindAncestor=�$4 DataGridL�$ SelectedItem��� 5�  6   $��移除��6=   $��60��6J   #��[! 6U   ��6|   �� *+)DataContext.RemoveSelectedRpcParamCommand+2 �� $3 FindAncestor=�$4 DataGridL���5�  -   �� 2 �� $3 FindAncestor=�$4 DataGridL�$ SelectedItem�5�  3   5�  /   5�  '   0!  *System.Windows.Controls.DataGridTextColumn!  5�  &   5   
IsReadOnly5 . $0 *  6K   $/ 参数名称��6U    "" DataGridTextColumnElementStyle1"  +System.Windows.Controls.DataGridBoundColumn6 "  ElementStyle#6 [" 6�   7 "  Binding7 6c   �� *	ParamName+6�   !  5�  &   5 . $0 *  6K   $/ 参数描述��6U   #6 [" 6�   7 6c   �� *	ParamDesc+6�   !  5�  &   5 . $0 *  6K   $/ 参数来源��6U   #6 [" 6�   7 6c   �� *ParamSource+6�   !  5�  &   5 . $0 *  6K   $/ 参数JSON字段名称��6U   #6 [" 6�   7 6i   �� *JsonVariableName+6�   5     5     5     5     �� 5     #��[ 6   y�6E   �� *IsCommReturnVal+# [	 .��5     �� -   5        $ requestReturnPanel��5  !   $ Vertical=�6=   .+�5     �� -   5        $ SpecialReturnValPanel��5  &   $��5q�6E   $ Vertical=�6P   y�5     �� *IsCommReturnVal+# [	 .+�5     �� $ 
Horizontal=�6)   .+�5  "   �� $��自定义返回值��6(   $	��150��69   $
��Left=�6E   $
��Left=�6g   #��[ 6�   �� 5  "   $��添加��6)   $	��100��68   $��25��6D   $��10q�6P   #��[! 6\   $�� ��5  A   ��6   �� *AddNewReturnValCommand+6W   5      �� 5!     $ 
Horizontal=�6)   $��5q�6B   y�5"  1   �� *ShowCommonValEditOrAdd+# [ .+�5#  "   �� $��	返回值��6(   $
��Left=�67   $
��Left=�6Y   #��[ 6t   �� 5$  "   $��50��6G   $
��Left=�6R   $
��Left=�6t   #��[ 6�   ��6*   �� *ReturnValue+6�   �� 5%  "   $��请求成功返回值��6(   $
��Left=�6;   $
��Left=�6]   #��[ 6x   �� 5&  "   $
��Left=�6Y   $
��Left=�6{    # NormalCheckBox#��[# 6�    6,   �� *ReturnValueSuccessFlag+6�   �� 5(  "   $��中文描述��6(   $
��Left=�68   $
��Left=�6Z   #��[ 6u   �� 5)  "   $	��150��6M   $
��Left=�6Y   $
��Left=�6{   #��[ 6�   ��6*   �� *ReturnValueDescZh+6�   �� 5*  "   $��英文描述��6(   $
��Left=�68   $
��Left=�6Z   #��[ 6u   �� 5+  "   $	��150��6M   $
��Left=�6Y   $
��Left=�6{   #��[ 6�   ��6*   �� *ReturnValueDescEn+6�   �� 5,  "   $��确认��6)   $	��100��67   $��25��6C   #��[! 6P   $�� ��5-  K   ��6   �� *" ConfirmAddOrEditReturnValCommand+6a   5.       5/     8   	RowHeight$8 25Q�50  ,   $��Stretch=�6;   $��Center=�6Y   ! .  " .  "9   VerticalScrollBarVisibility$
9 Auto=�51  c   $	��250��6�   ��5/  '   �� $ ReturnValueLst� 52  "   ��%j  '        �� 53  &   "#  6,   .�54  *   �� $ � ��$% 15(�6E   & 55  *   ���� 56  .   $' White��6�   (��       6>   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)6�   57  +   ( 58  *   P� 59  .   ) �@ 
IsSelected* . . �5;  2   �� $ � ��% �#4340416O   �� 5<  2   $ � ��
% ���� 6P   5=  /   5?  '   5@  #   . 5B  "     5C  &   $/ 操作��6=   $	0 120  6I   1 5D  *   �� 5E  .   .R�5F  2   �� $ 
Horizontal=�6=   .+�5G  6   �� $��编辑��6=   $��60��6J   #��[! 6U   ��6|   �� *" DataContext.EditReturnValCommand+2 �� $3 FindAncestor=�$4 DataGridL���5H  -   �� 2 �� $3 FindAncestor=�$4 DataGridL�$ SelectedItem��� 5J  6   $��删除��6=   $��60��6J   #��[! 6U   ��6|   �� *$"DataContext.DeleteReturnValCommand+2 �� $3 FindAncestor=�$4 DataGridL���5K  -   �� 2 �� $3 FindAncestor=�$4 DataGridL�$ SelectedItem�5M  3   5N  /   5P  '   !  5Q  &   $/ 成功返回值��69   $	0 100  6H   7 6T   �� *is_success_flag+# [ 5S  '   !  5T  &   $0 50  6<   $/ 	返回值��6m   7 6G   �� $ return_value�6 5U  *   �� 5V  .   .�5W  2   �� $ � ��$% Center=�6g   5X  /   5Z  '   !  5[  &   $/ 中文描述��69   $0 *  6G   #6 [" 6z   7 6Q   �� $ return_desc_zh�5]  '   !  5^  &   $/ 英文描述��69   $0 *  6G   #6 [" 6z   7 6Q   �� $ return_desc_en�5`  '   5b     5c     5d     5e     5f     �� 5g     $ 
Horizontal=�6   $��Right=�63   $��1��6O   .+�5h     �� $��确认��6   $	��150��6'   $��35��63   $��10q�6?   #��[! 6K   ��5i     �� *ConfirmCommand+��69   �� $ NewRpcEditPopupWin��6w   �� 5j     $��取消��6   ���#4c4c4c$	��150��6<   $��35��6I   $��10q�6U   #��[! 6a   ��5k     �� *CancelCommand+��69   �� $ NewRpcEditPopupWin��6w   5l     5m     5n     5o     