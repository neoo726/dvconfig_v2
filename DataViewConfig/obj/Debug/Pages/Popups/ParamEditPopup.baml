   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   /clr-namespace:DataViewConfigDataViewConfig  5
   	   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   0    *DataViewConfig.Pages.Popups.ParamEditPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   0currentclr-namespace:DataViewConfig     5
   	   =	converter'clr-namespace:DataViewConfig.Converters     5   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  6<   -   5          �Name$  ParamEditPopupWin��5      $	��750��5   	   $	��950��6   
������� � WindowStartupLocation$ CenterScreen=�5   	      NormalBackGround#��[  5   	     NormalFontSize#��[ 68    � 
ResizeMode$ NoResize=�5   	    � Title$ 参数编辑��6     � SizeToContent$ Height=�6/   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
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
 grid��5>      ���#1f1f1f � RowDefinitions 5?      �� 5@      $��60 �6    �� 5A      �� 5B      �� 5C      $��80 �6!   .+�5F      ��  �� Orientation$ 
Horizontal=�6   $��0��62   $��10q�6?   .+�5G      �� $ 
Horizontal=�6   .+�5H      �� $��参数名称��6   $	��100��6+     NormalLabel#��[ 69   �� 5I      
�������
������   NormalTextBox#��[ 5J   0   $	��200��5K      $��5,0,50,0q�6   ��5I      �� *ParameterInternalName+5K      5L      �� 5M      $ 
Horizontal=�6   .+�5N      �� $��描述��6   #��[ 6,   $	��120��6Q   �� 5O      
�������#��[ 62   
������ $	��250��5P      $��5,0,50,0q�6(   ��6   �� *	ParamDesc+6(   5Q      5R      �� 5T      $��1��6   $ Vertical=�6&   $��10q�6>   .+�5U      ]� ��. .+�5V      �� $��参数值输入(来源)��6   $��0q�60   ���#333333
�������6Y   #  HandyControl.Controls.Divider  5W      
�������66   5X      �� 5Y      $ 
Horizontal=�6   $��5q�66   .+�5Z      �� $ 
Horizontal=�6!   $	��100��6:   .+�5[      �� $��参数来源��6    #��[ 61   '   !DataViewConfig.Controls.TipsLabel  5\         TipName$# [rpc_edit_config]param_source��6/   5]      �� 5^      -   5         ��Name$ ParamSourceType��5^        NormalCombox#���  68   $	��200��6_   $��5,0,30,0q�6k    �� SelectedValuePath$ Value��5`   +   ��5_      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:RequestParamSource++ �� SelectedValue 5`      �� *CurParamrSource+��5a      :   4DataViewConfig.Controls.ComboBoxItemTemplateSelector  5b         DropDownTemplate 5c   "   �� 5d   &   .R�5e   *   �  5f   .   �� 5g   2   �� 5h   2   .+�5j   .   �� $��0��6O   ��68   �� *Value+6O   �� 5k   .     Global.Gray#��[ 6U   $��1��6   ��68   �� *Description+6   5l   +   5m   '      SelectedTemplate 5o   "   �� 5p   &   .R�5q   *   �� ��64   �� *Value+5r   '   5t      5v      5y      �� 5z      -   5         ��Name$ controlNamePanel��5z      $ 
Horizontal=�67   $��5q�6P   .+�5{      �� $ 
Horizontal=�6!   $	��100��6:   .+�5|      �� $��控件名称��6    #��[ 60     5}      $ [rpc_edit]control_name��6/   5~      �� 5�      #���  6   $	��200��6F   $��5,0,50,0q�6R   $
��Left=�6d   $ Value��5�   ,   ��5�      �� *DbControlLst+ �� SelectedItem 5�      �� *SelectedControl+��5�        5�       5�   "   �� 5�   &   .R�5�   *   �� ��64   �� *dv_control_internal_name+5�   '    5�   "   �� 5�   &   .R�5�   *   �� 
�������$��0 0 0 1q�6E   $��1q�6_   ._�5�   .   �� $ Vertical=�6:   
������ $	��300��6k   .+�5�   2   �� $��Center=�6f   
�������  
NormalText#��[ 6�   ��6<   �� *dv_control_internal_name+6�   �� 5�   2   $
��Left=�6]   $��-5 0 0 0q�6x   $��Center=�6�   #��[ 6�   #��[ 6�   ��68   �� *dv_control_desc+6�   5�   /   5�   +   5�   '   5�      5�      5�      �� 5�      -   5        $" controlComboxDataSelectPanel��5�      .+�5�      �� $ 
Horizontal=�6"   $��5q�6;   y�6F   �� *SelectedControl+ �� 	Converter# [ .+�5�      �� $ 
Horizontal=�6%   $	��100��6>   .+�5�      �� $��数值来源��6$   #��[ 64     5�      $ [rpc_edit]param_source��63   5�      	� 5�      -   5         	�Name$ SelectIndexRadioBtn��5�   &   $��Index��5�        NormalRadioButton#��[ 6   $��0 0 50 0q�6H    d� 	IsChecked 5�      �� *SelectedControlComboxDataType+# [ 5�   H   	� 5�      -   5        $ SelectTextRadioBtn��5�   &   $
��Text��5�   	   #��[ 6   $��0 0 50 0q�6E    5�      �� *SelectedControlComboxDataType+# [ 5�   E   5�      5�      �� 5�      -   5        $ constValPanel��5�      $ 
Horizontal=�64   $��5q�6N   .+�5�      �� $ 
Horizontal=�6!   $��Right=�6:   $	��100��6W   .+�5�      �� $��常量��6    #��[ 6.     5�      $% [rpc_edit_config]param_constant��6/   5�      �� 5�      #��[ 6   $	��200��6a   $��5,0,50,0q�6m   ��6E   �� *
ConstValue+6m   5�      �� 5�      -	   5        $ macroNamePanel��5�      $ 
Horizontal=�65   $��5q�6N   .+�5�      �� $��	宏名称��6   $	��100��6*   #��[ 66   �� 5�      #��[ 6   $	��200��6c   $��5,0,50,0q�6o   ��6E   �� *CurMacroName+6o   5�      �� 5�      -
   5        $ childParamPanel��5�      $ 
Horizontal=�66   $��5q�6O   .+�5�      �� $��数组内参数��6   $	��100��6,   #��[ 68   .   (DataViewConfig.Controls.MultiParamCombox  5�      -   5          Name$ multiParamCombox��5�   0      DefaultText$  ��5�      $	��280��5�      $��30��6   
������ 
�������$��5,0,50,0q�6J      ItemsSource 5�      �� *ParamMultiCheckLst+ �� Mode$ TwoWay=�   
ParamIdStr 5�   .   �� *ArrayItemsParamStr+    Command  5�   &   �� *!ParamMultiSelectedChangeCommand+!   CommandParameter! 6Z   �� " �� ElementName$" multiParamCombox��# �� Path$# 
ParamIdStr�5�   J   5�      �� 5�      -   5        $ dbSheetNamePanel��5�      $ 
Horizontal=�68   $��5q�6Q   .+�5�      �� $��数据库表��6   $	��100��6+   #��[ 67   �� 5�      #��[ 6   $	��200��6e   $��5,0,50,0q�6q   ��6E   �� *CurDbSheetName+6q   5�      �� 5�      -   5        $ tagNamePanel��5�      $ 
Horizontal=�64   $��5q�6M   .+�5�      �� $ 
Horizontal=�6!   $	��100��6;   .+�5�      �� $��点名��6    #��[ 6-     5�      $! [rpc_edit]tag_internal_name��6/   5�      �� 5�      -   5        $ tagSelectionCombox��5�      $	��250��6<   $��5,0,50,0q�6H   #��[ 6Z   $ �� 
IsEditable$ . % �� IsTextSearchEnabled% .  & �� IsTextSearchCaseSensitive& . ' w� TextPath$' tag_internal_name��5�   '   
�������
���   ���5�      �� *
DbTagLst+ 5�      �� *SelectedTag+��5�      �� 5�      .R�5�   "   �� .+�5�   &   �� $��0 3 0 5q�60   $
��Bold"�6d   ��6A   �� *tag_internal_name+6d   �� 5�   &   #��[ 60   ��6Z   �� *
tag_desc+5�   #   5�      5�      5�      �� 5�      -   5        $ tagJsonPathNamePanel��5�      $ 
Horizontal=�6<   $��5q�6U   .+�5�      �� $ 
Horizontal=�6!   $	��110��6:   .+�5�      �� $��	JSON Path��6    #��[ 66     5�      $&  [rpc_edit_config]param_json_path��6/   5�      �� 5�      #��[ 6   $	��250��6b   $��5,0,50,0q�6n   ��6E   �� *TagJsonPath+6n   5�      5�      �� 5�      $��2��6   $��10q�6'   .+�5�      ]� ��. .+�5�      �� $��参数值输出��6   $��0q�6.   ���#333333
�������6W     5�      
�������66   5�      �� 5�      $��1��6   $��1��6*   .+�5�      �� -   5        $ tagJsonVariableNamePanel��5�   !   $ 
Horizontal=�6D   $��5q�6]   .+�5�      �� $ 
Horizontal=�6%   $	��120��6?   .+�5�      �� $��JSON字段名��6$   #��[ 67     5�      $# [rpc_edit]json_filed_name(mq)��62   5�      �� 5�      #��[ 6"   $	��250��6p   $��5,0,50,0q�6|   ��6I   �� *ParamJsonVariableName+6|   5�      �� 5�      $ 
Horizontal=�6!   $��5q�6:   .+�5�      �� $��参数值类型��6    #��[ 61   $	��120��6V   �� 5�      #���  6#   $	��200��6J   $��5,0,50,0q�6V   ( �� DisplayMemberPath$( Value��5�      ��5�      ��    *M� *dv_config:ParamTargetType++) �� Text) 5�      �� *CurParamTargetType+5�      �� 5�      $ 
Horizontal=�6!   $��5q�6:   y�6E   �� *IsTranslateOrMap+# [ .+�5�      �� $ 
Horizontal=�6%   $	��120��6?   .+�5�      �� $��参数值转换��6$   #��[ 65     5�      $* $[rpc_edit]value_translate_expression��62   5�      �� 5�      #��[ 6"   $	��250��6m   $��5,0,50,0q�6y   ��6I   �� *ParamExpressionStr+6y   5�      5�      �� 5�      $��2��6   $��0��6*   $��2��6:   $ 
Horizontal=�6N   $��5q�6g   .+�5�      �� $ 
Horizontal=�6!   .+�5      �� -   5        * ��Name$* ParamCheckBox��5   #   $��参数校验��6h   
������� 6:   �� *IsCheckParamValidation+6w     5     $0 *[rpc_edit_config]param_validation_checkbox��6/   5     �� 5     $	����6�   + � ToolTip$	+ Add��6�   $��Center=�6�   $��0 0 0 0q�6�     ImageButton#��[ 5     y�5     �� $" ParamCheckBox��$# 	IsChecked�# [ ��5     �� *AddNewParamValidationCommand+5     �� 5     ��6#   �� * SelectedParamValidationRuleLst+y�5     �� $" ParamCheckBox��$# 	IsChecked�# [ ��5     �� 5	     .R�5
  "   �� $ 
Horizontal=�6-   $��Center=�6F   .+�5  &   �� $��移除��6-   $+ 保存后生效��5  G   $��50��5       NormalButton#��[ 6"   ��5  :   �� , �� RelativeSource, �� - �� AncestorType$- ItemsControlL�$.# (DataContext.RemoveParamValidationCommand���5     �� *validation_type_id+5  "   �� 5  &   $��校验规则��6,   #��[ 6<   �� 5  &   #���  6/   $	��200��6V   $��5,0,10,0q�6b   $( RuleDesc��5     $ RuleID��6K   ��5     �� , �� $- ItemsControlL�$(# "DataContext.ParamValidationRuleLst� 5     �� *validation_type_id+6K   �� 5  &   $ 
Horizontal=�61   $	��120��6K   .+�5  *   �� $��校验失败弹窗��60   #��[ 6B     5  *   $3 -[rpc_edit_config]param_invalid_tips_selection��6?   5  '   �� 5  &   $ paramInvalidTipsCombox��6/   $	��250��6P   $��5,0,50,0q�6\   #��[ 6n   $ . % . & . $ tips_internal_name��5  8   $' tips_internal_name��5  )   
�������
���   ���5     �� , �� $- ItemsControlL�$# DataContext.DvTipsLst� 5     �� *invalid_tips_internal_name+��5  *   �� 5  .   .R�5   2   �� .+�5!  6   �� $��0 3 0 5q�6@   $
��Bold"�6u   ��6Q   �� *tips_internal_name+6u   �� 5"  6   #��[ 6@   ��6j   �� *	tips_zh+5#  3   5$  /   5&  '   5'  #   5(     5*     5+     5,     �� 5.     $��3��6   .+�5/     �� $ 
Horizontal=�6   $��Right=�67   $��1��6S   .+�50     �� $��确认��6   $	��150��6+   $��35��67   $��10q�6C   #��[ 6O   ��51     �� *ConfirmCommand+��6.   �� $" ParamEditPopupWin��6k   �� 52     $��取消��6   ���#4c4c4c$	��150��6@   $��35��6M   $��10q�6Y   #��[ 6e   ��53     �� *CancelCommand+��6.   �� $" ParamEditPopupWin��6k   54     55     56     57     58     