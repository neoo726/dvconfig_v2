   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   /clr-namespace:DataViewConfigDataViewConfig  5
   	   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   4    .DataViewConfig.Pages.Popups.InterfaceEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   0currentclr-namespace:DataViewConfig     5
   	   =	converter'clr-namespace:DataViewConfig.Converters     5   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  InterfaceEditPopupWin��5      $	��750��5   	   $	��550��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.      NormalBackGround#��[  5   	     NormalFontSize#��[ 68    � 
ResizeMode$ NoResize=�5   	     WIndowChromeKey'  !System.Windows.Shell.WindowChrome   WindowChrome# �  6    $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2REverse?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility>  8DataViewConfig.Converters.EcsComm_MQ2VisibilityConverter  EcsCommTypeMQ2Visibility?  9DataViewConfig.Converters.EcsComm_OPC2VisibilityConverter  EcsCommTypeOPC2Visibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter 	 EcsCommTypeOPC2Bool:	  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter 
 MqRpcOneType2BoolC
  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters	 M� Type,  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource
   ResizeBorderThickness   CaptionHeight��%D  &	       &	      &	 &     &	 9     &	 L     &	 _     &		 r     &	
 �     &	 �     &	 �     &	 �     &	     &	 t      5   
   6-     5   
   6*     5   
   64     5   
   63     5   
   64     5   
   6-     5   
   6.   	  5    
   6/   
  5!   
   68     5"   
   64   S� 5#   
   $ 	GetValues��64   "  6K    5$      M� 5%      "	  6   5'      S� 5(   
   $ 	GetValues��65   "  6L    5)      M� 5*      "	  6   5,        5-   
   
 5.      u�5/      5q�6   $ 0Q�51   G   52      53      .��54      � -   5         �Name$
 grid��54      ���#1f1f1f � RowDefinitions 55   
   �� 56      $��30 �6   �� 57      �� 58      $��80 �6   .+�5:   
   ]� $��0��6   
���   �.+�5;      �� $��Stretch=�6   $
��Left=�62   $��接口编辑��6H     NormalLabel#��[ 6W   �� 5<      $��1��6   $��Right=�6&   $��Right=�6=     
TextButton#��[ 6Y   
�������$	����5=   *   ��5>      �� *CloseCommand+��68   ��  �� ElementName$ InterfaceEditPopupWin��6y   5?      �� 5@   
   $��1��6   $��10q�6#    �� Orientation$ Vertical=�6/   .+�5A      �� $ 
Horizontal=�6   $��5q�62   .+�5B      �� $��接口名称��6   $��5,0,5,0q�6(   $	��100��6:     
NormalText#��[ 6F   �� 5C        NormalTextBox#��[ 6   $	��150��6f   $��5,0,50,0q�6r   ��6A   �� *RequestInternalName+6r   5D      �� 5E      $ 
Horizontal=�6   $��5q�62   .+�5F      �� $��接口描述��6   $��5,0,5,0q�6(   $	��100��6:   #��[ 6F   �� 5G      #��[ 6   $	��150��6^   $��5,0,50,0q�6j   ��6A   �� *RequestDesc+6j   5H      �� 5I      $ 
Horizontal=�6   $��5q�62   .+�5J      �� $��交互对象��6   $��5,0,5,0q�6)   $	��100��6:   #��[ 6G   �� 5K        NormalCombox#���  6   $	��100��6B   $��5,0,50,0q�6N   
������� �� DisplayMemberPath$ system_desc��5M       �� SelectedValuePath$ 	system_id��6a   ��5L      �� *RequestSystemLst+ �� SelectedValue 5M   7   �� *RequestSystemId+6a   5O      �� 5P      $ 
Horizontal=�6   $��5q�62   .+�5Q      �� $��前置条件��6   $��5,0,5,0q�6)   $	��100��6;   #��[ 6G   �� 5R      #���  6   $	��200��6B   $��5,0,50,0q�6N   $ Value��5T      ��5S      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *#!dv_config:RequestPreConditionType++ �� Text 5T   2   �� *RequestPrecondition+5V      �� 5W      $ 
Horizontal=�6   $��5q�62   .+�5X      �� $��参数列表��6   $��5,0,5,0q�6)   $	��100��6;   #��[ 6G   �� 5Y      -   5         ��Name$ 
paramIdLst��5Y      #��[ 6.   $	��200��6q   $��5,0,5,0q�6~   ��6U   �� *
ParamIdLst+6~   �� 5Z        NormalButton#���  6   $��选择��6@   $��5,0,10,0q�5[   ~   $��50��6�   ��6   �� *SelectParamCommand+��6@   �� $ 
paramIdLst�� �� Path$
 Text�6�   5]      �� 5^      $ 
Horizontal=�6   $��5q�62   .+�5_      �� $��交互方式��6   $��5,0,5,0q�6)   $	��100��6;   #��[ 6G   �� 5`      -   5         ��Name$ CommTypeCombox��5`      #���  63   $	��100��6Z   $��5,0,50,0q�6f   $ Value��5b      ��5a      ��    *M� *dv_config:ECSCommType++ 5b   2   �� *	EcsComm+6L   5d      �� 5e      $ 
Horizontal=�6   $��5q�62   .+�5f      �� $��接口请求失败��6   $��5,0,5,0q�6+   $	��100��6=   #��[ 6I   6o   �� 5g      $��成功后弹窗提示��6E   
�������$��5,0,15,0q�6j    d� 	IsChecked 6   �� *IsSuccessShowTips+6j   �� 5h      $��失败后弹窗提示��6E   
�������$��5,0,50,0q�6j    6   �� *IsFailedShowTips+6j   5i      �� 5j      -   5         ��Name$ msgTypePanel��5j      $ 
Horizontal=�60   $��5q�6I   y�5k      �� $ CommTypeCombox��$ SelectedIndex� �� 	Converter# [ .+�5l      �� $��msg_type��6   $��5,0,5,0q�6-   $	��100��6?   #��[ 6L   �� 5m      #��[ 6   $	��200��6]   $��5,0,50,0q�6i   ��6D   �� *	MsgType+6i   5o      �� 5q      $ Vertical=�6   y�5r      �� $ CommTypeCombox��$ SelectedIndex�# [ .+�5s      �� $ 
Horizontal=�6   .+�5t      �� $��OPC交互类型��6   $	��100��6/   $��5,0,5,0q�6;   #��[ 6O   	� 5u      -   5         	�Name$  tagFeedbackOpctypeRadioBtn��5u   "   $!��写单个点，等待反馈��5w   "     NormalRadioButton#��[ 67    5v   "   �� *DestTagType+5w   7   	� 5x      -   5        $! tagArgsEventOpcTypeRadioBtn��5x   "   $
 TrueY�5y   "   $ ��写args/event,等待反馈��63   #��[ 6|   	� 5z      -   5        $) #tagCommandIdIndexValOpcTypeRadioBtn��5z   "   $��写CommandId/Index/Value��5|      #��[ 6f    5{      �� *DestTagType+5|   f   5}      �� 5      $ Vertical=�6   y�64   �� $  tagFeedbackOpctypeRadioBtn��$ 	IsChecked�# [ .+�5�      �� $ 
Horizontal=�6#   $��5q�6<   .+�5�      �� $��写点��6&   $��5,0,5,0q�60   $	��100��6A   #��[ 6N   6t   �� 5�      #���  6$   $	��200��6K   $��5,0,50,0q�6W   $ Value��5�   m   ��6   �� *PlcRequestTagNameLst+ 6E   �� *RequestTagInternalName+6m   5�      �� 5�      $ 
Horizontal=�6"   $��5q�6;   .+�5�      �� $��	反馈点��6&   $��5,0,5,0q�61   $	��100��6B   #��[ 6O   6u   �� 5�      #���  6$   $	��200��6K   $��5,0,50,0q�6W   $ Value��5�   v   ��6   �� *PlcFeedbackTagNameLst+ 6F   �� * RequestFeedbackTagInternalName+6v   5�      5�      �� 5�      $ Vertical=�6   y�64   �� $  tagFeedbackOpctypeRadioBtn��$ 	IsChecked�# [ .+�5�      �� -	   5        $ paramSepartorPanel��5�   !   $ 
Horizontal=�6=   $��5q�6V   .+�5�      �� $��参数分隔符��6%   $��5,0,5,0q�62   $	��100��6D   #��[ 6P   �� 5�      #��[ 6%   $	��100��6l   $��5,0,50,0q�6x   ��6L   �� *ParamSeperator+6x   5�      �� 5�      -
   5        $ argsTagPanel��5�   !   $ 
Horizontal=�68   $��5q�6Q   .+�5�      �� $��	参数点��6&   $��5,0,5,0q�61   $	��100��6C   #��[ 6O   6u   �� 5�      #���  6&   $	��200��6M   $��5,0,50,0q�6Y   $ Value��5�   [   ��6   �� *ArgsTagNameLst+ 6>   �� *ArgsTagName+6[   5�      �� 5�      -   5        $ eventTagPanel��5�   !   $ 
Horizontal=�69   $��5q�6R   .+�5�      �� $��Event点��6&   $��5,0,5,0q�64   $	��100��6E   #��[ 6R   6x   �� 5�      #���  6$   $	��200��6K   $��5,0,50,0q�6W   $ Value��5�   ^   ��6   �� *EventTagNameLst+ 6@   �� *EventTagName+6^   5�      �� 5�      -   5        $ returnTagPanel��5�   !   $ 
Horizontal=�6:   $��5q�6S   .+�5�      �� $��	反馈点��6&   $��5,0,5,0q�61   $	��100��6C   #��[ 6O   6u   �� 5�      #���  6%   $	��200��6L   $��5,0,50,0q�6X   $ Value��5�   _   ��6   �� *ReturnTagNameLst+ 6@   �� *ReturnTagName+6_   5�      �� 5�      -   5        $ returnTagTypePanel��5�   !   $ 
Horizontal=�6>   $��5q�6W   .+�5�      �� $��反馈点类型��6&   $��5,0,5,0q�63   $	��100��6E   #��[ 6Q   6w   �� 5�      $��0q�6#   .��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "   	� #���  6V   $��
INT__ARRAY��6�   
�������$��5,0,50,0q�6�    6/   �� *IsReturnIntArray+6�   	� 5�   "   #���  6R   $
��LONG��6~   
�������$��5,0,15,0q�6�    6/   �� *IsReturnLong+6�   5�      5�      5�      5�      5�      �� 5�      -   5        $ requestReturnPanel��5�      $ Vertical=�65   .+�5�      �� $ 
Horizontal=�6!   $��5q�6:   .+�5�      �� $��接口返回值��6%   $��5,0,5,0q�62   $	��100��6D   #��[ 6P   6v   �� 5�      $��0q�6#   .��5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "   	� -   5        $ globalReturnRadioBtn��5�   .   #���  6r   $��通用返回值��6�   
�������$��5,0,50,0q�6�    6L   �� *IsCommReturnVal+6�   	� 5�   "   -   5        $ specialReturnRadioBtn��5�   .   #���  6v   $��单独定义返回值��6�   
�������$��5,0,15,0q�6�    6M   �� *IsSpecialReturnVal+6�   5�      5�      5�      �� 5�      -   5        $ SpecialReturnValPanel��5�   "   $��5q�6A   $ Vertical=�6L   y�5�      �� $ globalReturnRadioBtn��$ 	IsChecked�# [ .+�5�      �� $ 
Horizontal=�6%   .+�5�      �� $��返回值定义列表��6$   $	��150��66   $
��Left=�6B   $
��Left=�6d   #��[ 6   �� 5�      $��添加��6%   ���#4c4c4c$	��100��6G   $��25��6S   $��10q�6_   #��[ 6k   $�� ��5�   A   ��6   �� *AddNewReturnValCommand+6W   5�      �� 5�      .��5�      &   System.Windows.Controls.DataGrid  $	��150��6'      	RowHeight$ 25Q�5�   ,   $��Center=�6;   $��Center=�6X      AutoGenerateColumns .     CanUserAddRows .  "   VerticalScrollBarVisibility$
 Auto=�5�   c   ��5�   4   �� $ ReturnValueLst�    Columns  5�   "   4  .System.Windows.Controls.DataGridTemplateColumn  5�   &   ,  &System.Windows.Controls.DataGridColumn!   Header$! 操作��6=   "   CellTemplate" 5�   *   �� 5�   .   .R�5�   2   �� $ 
Horizontal=�6=   .+�5�   6   �� $��删除��6=   #��[ 6J   ��6n   �� *$"DataContext.DeleteReturnValCommand+# �� RelativeSource# �� $ �� Mode$$ FindAncestor=�% �� AncestorType$% DataGridL���5�   -   �� # �� $$ FindAncestor=�$% DataGridL�$ SelectedItem�5�   3   5�   /   5�   '   4  .System.Windows.Controls.DataGridCheckBoxColumn  5�   &   $! 成功返回值��6=   &   Width5  /System.Windows.Controls.DataGridLengthConverter$	& 100 6L   1  +System.Windows.Controls.DataGridBoundColumn'   Binding' 6X   �� $ is_success_flag�5�   '   0  *System.Windows.Controls.DataGridTextColumn  5�   &   $& 50 6<   $! 	返回值��6m   ' 6G   �� $ return_value�(   ElementStyle( 5�   *   �� 5�   .   .�5�   2   �� ) �� Property) � ��* �� Value$* Center=�6g   5�   /   5�   '     5�   &   $! 描述��69   $	& 350 6E   ' 6Q   �� $ return_desc�( 5�   *   �� 5�   .   .�5�   2   �� ) � ��$* Center=�6g   5�   /   5�   '   5�      5�      5�      5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Right=�6/   $��2��6K   .+�5�      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O   #��[ 6[   ��5�      �� *ConfirmCommand+��69   �� $ InterfaceEditPopupWin��6z   �� 5�      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5�      �� *CancelCommand+��69   �� $ InterfaceEditPopupWin��6z   5�      5�      5�      