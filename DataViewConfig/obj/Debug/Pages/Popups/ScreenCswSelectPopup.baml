   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DataViewConfig.Pages.PopupsDataViewConfig.Pages.Popups  5   	   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5	   
   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5
   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5   	   /clr-namespace:DataViewConfigDataViewConfig  5   	   R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   6    0DataViewConfig.Pages.Popups.ScreenCswSelectPopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:DataViewConfig.Pages.Popups     5   	   :controls%clr-namespace:DataViewConfig.Controls     5	   
   =	converter'clr-namespace:DataViewConfig.Converters     5
   	   6vm'clr-namespace:DataViewConfig.ViewModels     5   	   0currentclr-namespace:DataViewConfig     5   	   Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  ScreenCswSelectPopupWin��5      $	��450��5   	   $	��650��6   
������� � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.    � Title$ 选择.csw画面��6E      NormalBackGround#��[  5   	   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   
    � 	Resources 5      8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visibility?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter  Bool2ReverseVisibility8   2DataViewConfig.Converters.EcsComm_MQ2BoolConverter  EcsCommTypeMQ2Bool9  3DataViewConfig.Converters.EcsComm_OPC2BoolConverter  EcsCommTypeOPC2Bool:  4DataViewConfig.Converters.MqRpcOnlyOne2BoolConverter  MqRpcOneType2BoolC  =DataViewConfig.Converters.MqRpcTypeMultiBlockId2BoolConverter  MqRpcMultiBlockIdType2Bool?  9DataViewConfig.Converters.MqRpcTypeMultiCid2BoolConverter  MqRpcMultiCidType2Bool  targetTypeEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type,	  &DataView_Configuration.ParamTargetType 	 paramSourceEnum/
  )DataView_Configuration.RequestParamSource��%�  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &		 �       5   
   6-     5   
   64     5   
   6-     5   
   6.     5   
   6/     5   
   68     5   
   64   S� 5   
   $ 	GetValues��64   "  6K    5      M� 5       " 	 6   5"      S� 5#   
   $ 	GetValues��65   "  6L    5$      M� 5%      " 
 6   5'      5)      .��5*      � ���#1f1f1f	 � RowDefinitions	 5+   
   �� 5,      �� 5-      $��80 �6   
 � ColumnDefinitions
 5/   
   �� 50      �� 51      $��45 �6   �� 52      .+�54   
   �� $��0��6   $��0��6$   .��55      R� -   5         R�Name$ FileTreeView��55      ���#1f1f12
�������$��5q�56   ?   ��55   -   �� *CswLst+��57      �� 58       �� 
TargetType" Q�6   .�59      ��  �� Property �@Q�
IsExpanded �� Value . 67   �� 5:       �@Q�
IsSelected 67   �� *
IsSelected+ �� Mode$ TwoWay=��� 5;       � �� 6=   �� *Name+�� 5<       � ��$ 0 5 0 0q�63   �� 5=       � ��$ 0 0 0 1q�6<   �� 5>       � ��
 �����68   �� 5?       � ��$ Stretch=�6G   5@      ��5B      �� 5C       �� ItemsSource 6/   �� *
Children+.R�5D      �� 
���������6$   �� *Name+6<   5E      5G      5H      �� 5I   
   $��1��6    �� Orientation$ Vertical=�6%   $��Center=�6<   .+�5J      �� $��1��6   $��2��6"   $	����63    
 ImageButton#��� 
 5K      $��45��6;   $��Center=�6F   $��1q�5M      $��0 0 0 20q�6)   ��5L      �� *SelectCswNodeCommand+{�6+   �� *IsSelectValidNode+5M   )   �� 5N      $��1��6   $��2��6"   $	����63   #��� 
 5O      $��45��6;   $��Center=�6F   $��1q�5Q      $��0 0 0 20q�6)   ��5P      �� *RemoveCswRightNodeCommand+{�60   �� *IsSelectRightCswName+5Q   )   5R      R� 5S   
   -   5        $ FileRightTreeView��5S   0   $��0��6   $��2��6    ���#1f1f12
�������$��5q�5T   3   ��5S   K   �� *SelectedCswNameLst+��5U      �� 5V      " Q�6   .�5W      ��  �@Q�
IsExpanded . 63   �� 5X       �@Q�
IsSelected 63   �� *
IsSelected+$ TwoWay=��� 5Y       � ��$ 0 5 0 0q�6/   �� 5Z       � ��$ 0 0 0 1q�68   �� 5[       � ��
 �����63   5\      5^      �� 5_   
   $ 
Horizontal=�6   $��Right=�6/   $��1��6L   $��0��6Y   $��3��6i   .+�5a      �� $��确认��6   ���#4c4c4c$	��150��67   $��35��6C   $��10q�6O     NormalButton#��[ 6[   ��5b      �� *ConfirmCommand+��69   ��  �� ElementName$ ScreenCswSelectPopupWin��6|   �� 5c      $��取消��6   ���#4c4c4c$	��150��68   $��35��6E   $��10q�6Q   #��[ 6]   ��5d      �� *CancelCommand+��69   �� $ ScreenCswSelectPopupWin��6|   5e      5f      5g      