   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null/clr-namespace:DataViewConfigDataViewConfig  5   	   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   
        DataViewConfig.StartWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DataViewConfig     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   7	converter'clr-namespace:DataViewConfig.Converters  5   
   -   5          � Title$  选择数据库��5
   	   $	��420��6   $	��550��6$    � WindowStartupLocation$ CenterScreen=�62    � Icon$ /Images/icon.png��5   -      NormalBackGround#��[  5   	     NormalForeGround#��[ 5   	    � SizeToContent$ WidthAndHeight=�5   	    � 	Resources 5      5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2Inverse8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visible��%:   &	       &	        5   
   6*     5   
   6-   5      .��5      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��4* �6   �� 5      $��3* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�6I   $��0��6f   $��10q�6s   .+�5       	� -   5         	�Name$ MysqlRadioBtn��5        d� 	IsChecked$
 TrueY�61   $��Mysql��6B   
�������$��0,0,20,0q�6e   	� 5!      -   5        $ SqlteRadioBtn��5!      $��Sqlite��61   
�������$��0,0,20,0q�6V   5"      �� 5$   
   $��1��6   .+�5%      �� $ 
Horizontal=�5&      $��Center=�6+   $��Center=�6F   y�5%      �� 	 �� ElementName$	 MysqlRadioBtn��
 �� Path$
 	IsChecked� �� 	Converter# [ .+�5)      �� $ Vertical=�6   .+�5*      �� $ 
Horizontal=�6!   $��5q�6:   .+�5+      �� $��IP��6$   $��80��6.   $��5q�69     
NormalText#��[ 6D   �� 5,      -   5         ��Name$ IpTxtBox��5,   "   $��	127.0.0.1��64     NormalTextBox#��[ 6E   �� 5-      $��端口��6$   $��60��6.   $��5q�69   #��[ 6D   �� 5.      -   5        $ 
PortTxtBox��5.   "   $
��5672��66   #��[ 6B   5/      �� 50      $ 
Horizontal=�6!   $��5q�6;   .+�51      �� $��	用户名��6$   $��80��6/   $��5q�6:   #��[ 6E   �� 52      -   5        $ 
UserTxtBox��52   "   $
��root��66   #��[ 6B   �� 53      $��密码��6$   $��60��6.   $��5q�6:   #��[ 6E   �� 54      -   5        $ 	PwdTxtBox��54   "   $��	Zpmc@3261��65   #��[ 6F   55      �� 56      $ 
Horizontal=�6!   $��5q�6;   .+�57      �� $��选择数据库��6$   $��80��61   $��5q�6<   #��[ 6G   �� 58      -   5        $ DatabaseTxtBox��58   "   $	��200��6:   $��dataview_template_dev��6F   #��[ 6c   5;      5<      5>      �� 5A      $ 
Horizontal=�5B      $��Center=�6+   $��Center=�6F   $��10q�6c   y�5A      �� $	 SqlteRadioBtn��$
 	IsChecked�# [ .+�5C      �� -	   5         ��Name$ 
SqlitePath��5C      
�������$
��Wrap=�6D   $	��400��6Y   6g   �� 5D      -
   5          NormalButton#��[ 5D      $��50��6@   $��选择��6K   $	��Top=�6X   6p   5E      5G      �� 5H   
   -   5        $��2��5H      #��[ 6   $��确定��6E   $��5q�6R   $��Center=�6]   $��35��6z   $	��400��6�   6�   5O      5P      