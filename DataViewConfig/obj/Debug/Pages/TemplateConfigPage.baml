   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null;"clr-namespace:DataViewConfig.PagesDataViewConfig.Pages  5   	   E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  6:   A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  6u   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  6�   -    'DataViewConfig.Pages.TemplateConfigPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:DataViewConfig.Pages     5   	   >
viewmodels'clr-namespace:DataViewConfig.ViewModels     6:   :controls%clr-namespace:DataViewConfig.Controls     6u   =	converter'clr-namespace:DataViewConfig.Converters     6�     � 	Resources  5	      5  /DataViewConfig.Converters.Bool2InverseConverter   Bool2InverseC  =DataViewConfig.Converters.TemplateTypeRXG2VisibilityConverter  TemplateRXG2VisibleC  =DataViewConfig.Converters.TemplateTypeSTS2VisibilityConverter  TemplateSTS2Visible8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visible��%t   &	        &	      &	 &     &	 9       5
   
   6*     5   
   68     5   
   68     5   
   6-   5      .��5      ��   Content.BackGround#��[ 6   .+�5   
   �� $��项目机型��6     NormalGroupBox#��[ 6#   .��5      �� $
��Left=�6    �� Orientation$ 
Horizontal=�64   $��10q�6M   .+�5      	� -   5         	�Name$ RXGRadioBtn��5      $	��RXG��63   
�������$��10q�6u    d� 	IsChecked 6A   �� *
RXGChecked+6u   	� 5      -   5        $ STSRadioBtn��5      $	��STS��63   
�������$��10q�6v    6A   �� *
STSChecked+6v   5      5      �� 5   
   $��模板方案��6   #��[ 6"   .��5      �� $ Vertical=�6   .+�5      �� ��6   �� *TemplateLst+y�6D   ��  �� ElementName$ RXGRadioBtn�� �� Path$ 	IsChecked� �� 	Converter# [ ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   y�6B   �� *TemplateType+# [ .+�5   "   	�  	� 	GroupName$ SchemeRadioBtn��6.   
�������$��10q�6}    6I   �� *
IsSelected+.��5   &   �� $ 
Horizontal=�61   .+�5    *   �� $��5q�6J     
NormalText#��[ 6U   ��64   �� *Name+6U   �� 5"   *     Global.Gray#��[ 6S   $��5q�6}   #��[ 6�   ��66   �� *Description+�� 5#   0   -   5          Global.Blue#��[ 5#   :   ��6d   �� *Url+.�6�   文档链接6�   5%   +   52   '   53   #   54      55      57      �� 58      ��6   �� *TemplateLst+y�6D   �� $ STSRadioBtn��$ 	IsChecked�# [ ��59      �� 5:      .R�5;      �� $ 
Horizontal=�6)   y�6B   �� *TemplateType+# [ .+�5<   "   	� $ SchemeRadioBtn1��6.   
�������$��10q�6~    6J   �� *
IsSelected+.��5=   &   �� $ 
Horizontal=�61   .+�5>   *   �� $��5q�6J   #��[ 6U   ��64   �� *Name+6U   �� 5?   *   #��[ 6Q   $��5q�6{   #��[ 6�   ��64   �� *Description+�� 5@   2   -   5        #��[ 5@   <   ��6f   �� *Url+.�6�   文档链接6�   5B   +   5P   '   5Q   #   5R      5S      5U      5V      5W      �� 5Y   
   $��其他��6   #��[ 6   .��5Z      �� $-��'确认后自动删除未选择的模板��6   
�������$��10q�6r    60   �� *AutoDeleteOtherTemplate+6r   5[      �� 5\   
   $��确定��6   $��5q�6}   $��35��6�    	 NormalButton#��[	 6�   ��6   �� *SaveCommand+��6?   �� $ TemplateConfigWinWin��6�   5]      5_      