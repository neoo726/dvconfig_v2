   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5	   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5
   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   4    .DataViewConfig.Pages.Popups.ParamSelectedPopup   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5   
   =	converter'clr-namespace:DataViewConfig.Converters     5	   	   6vm'clr-namespace:DataViewConfig.ViewModels     5
   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  ParamSelectedWin��5   	    � 
ResizeMode$ NoResize=�5         WIndowChromeKey'  !System.Windows.Shell.WindowChrome   WindowChrome# �   62     NormalFontSize#��[ 5   	    � Title$ 参数选择��5   	   $	��650��6   $	��800��6$    � WindowStartupLocation$ CenterScreen=�60     Popup.PanelBackGround#��[ 6V    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters	 M� Type,  &DataView_Configuration.ParamTargetType  paramSourceEnum/  )DataView_Configuration.RequestParamSource
   ResizeBorderThickness   CaptionHeight��%?  &	       &	      &	 n     &	  �       5   
   6-   S� 5   
   $ 	GetValues��64   "  6K    5      M� 5      "	  6   5      S� 5   
   $ 	GetValues��65   "  6L    5      M� 5      "	  6   5        5   
   
 5      u�5      5q�6   $ 0Q�5!   G   5"      5#      .��5$      �� -   5         ��Name$
 grid��5$       �� Orientation$ Vertical=�6   #��[ 66   .+�5&   
   �� $ Vertical=�6   ���#1f1f1f.+�5'      �� $ 
Horizontal=�6   $
��auto��63     Popup.TopbarBackGround#��[ 6@   .+�5(      �� $��接口列表��6     
NormalText#��[ 6(   $��Center=�6L   $��15(�6h   $��
10 10 0 0 q�6v   5)      &   System.Windows.Controls.DataGrid  5*      $��5q�6   $	��500��6#   #��[ 60   $   HorizontalScrollBarVisibility$ Visible=�6|      AutoGenerateColumns .     CanUserAddRows .  ��.  ��6[   �� *
ParamLst+   RowStyle 5+      �� 5,       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5-      ��  �� Property � �� �� Value$ 35��63   �� 5.       � ��# [ 65    �� Triggers 5/      �� 50       �� Binding 6*   �� *
IsSelected+ �� Value true6I   .a�51   "   ��  � �� �#0078d76Q   52      �� 53       6*   �� *
IsSelected+	 false6I   .a�54   "   ��  � �� �#1f1f1f6Q   55      57         Columns 59      4  .System.Windows.Controls.DataGridTemplateColumn  5<      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 选择��6-      CellTemplate 5=      �� 5>      .R�5?   "   �� $��35��6+   $��35��67    d� 	IsChecked 6B   ��  �� Path$ 
IsSelected� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5@      5B      0
  *System.Windows.Controls.DataGridTextColumn
  5C       	  
IsReadOnly . $ 序号��6;   1  +System.Windows.Controls.DataGridBoundColumn    Binding  6G   �� *Id+
  5D       . $ 参数名称��6;     6I   �� *ParamInternalName+
  5E       . $ 参数描述��6;     6I   �� *	ParamDesc+
  5F       . $ 参数来源��6;     6I   �� *ParamSource+
  5O       . $ 参数JSON字段名称��6;     6O   �� *ParamJsonVariableName+5d      5e      �� 5f   
   $ 
Horizontal=�6   $��Center=�6/   .+�5g      ��   NormalButton#��[ 6   $	��150��6;   $��50��6G   $��确定��6S   $��20q�6`   ���#4c4c4c��5h      �� *ConfirmCommand+��68   �� ! �� ElementName$! ParamSelectedWin���� 5i      #��[ 6   $	��150��6;   $��50��6G   $��取消��6S   $��20q�6`   ���#4c4c4c��5j      �� *CancelCommand+��67   �� $! ParamSelectedWin��5k      5m      5n      