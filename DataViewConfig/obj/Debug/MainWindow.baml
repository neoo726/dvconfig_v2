   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null/clr-namespace:DataViewConfigDataViewConfig  5   	   9!clr-namespace:DataViewConfig.BaseDataViewConfig.Base  5
   	   E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5   
       DataViewConfig.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DataViewConfig     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5	   	   ,base!clr-namespace:DataViewConfig.Base  5
   	   7	converter'clr-namespace:DataViewConfig.Converters  5   
   -   5          �Name$  MainWIn��5   	    � Title$ DataView Config Tool��5   	   $	��955��6&   $
��1450��63      NormalBackGround#��[  6@    � WindowStyle$ ThreeDBorderWindow=�5   	    � Icon$" /Images/配置工具logo.png��6R    � WindowStartupLocation$ CenterScreen=�5   	   $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5   	    � 	Resources 5      4  .DataViewConfig.Converters.Bool2HiddenConverter  Bool2Hidden5  /DataViewConfig.Converters.Bool2InverseConverter  Bool2Reverse8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2Visible&    DataViewConfig.Base.BindingProxy  BindingProxy   Data'  !System.Windows.Shell.WindowChrome  WIndowChromeKey   ResizeBorderThickness   CaptionHeight  ToggleButtonStyle	 �� 
TargetType
 �� Property �� Value �� RelativeSource �� AncestorType �� Path �� 
TargetType � ColumnDefinitions ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  ExpanderStyle �� CornerRadius J� SnapsToDevicePixels � RowDefinitions  MenuButtonStyle 	 NormalButton �� BasedOn 
 SideBarRadioBtn ��Name �Name ( $Content.DataGrid.Selected.BackGround��%/  &	       &	      &	 &     &	 9     &	 U     &	 �     &	 �    &	 �    7 	 &	
 �    7    5   
   6)     5   
   6*     5   
   6-     5   
    61   ��   5   
    5      u�5      5q�6   $ 0Q�5   G   5      �� 5   
   "	 d�6*   .�5       �� 
 � �� 6&   ��  �� $ ExpanderL�$ Width��� 5!      
 �@d�	IsChecked 6*   ��  �� $ ExpanderL�$ 
IsExpanded��� 5"      
 � �� 5#      �� 5$      " d�6&   .R�5%      � $��50��6   $��Center=�6+    5&      �� 5'   "   �� 5(   "   $��30 �63   .+�5*      �� $	��150��6$   $��35��61   #��z��6=   #��z��6g   #��z��6�   $��Center=�6�   $��Stretch=�6�   �� 5+      $ contentPresenter��6F   $y�	Collapsed=�6/   6F   �� 5,      $ foldImg��6$   #��z��65   $��35��6_   $��1��6k   $	����6{   #��z��6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   5-       5.      P� 5/       �@d�	IsChecked$
 TrueY�6;   . �50   "   �� $ foldImg��6<   
 � ��$	 ��6Q   51      53      55      56      �� 57   
   "	 9�6&   .�58      �� 
 � �� 59      �� 5:      " 9�6&   .R�5;      �� #��z��6!   #��z��6K   #��z��6w   $ 3��6�    . ._�5<      �  5=   "   �� 5>   &   $
��auto �64   �� 5?   &   .+�5A   "   d� #��z��5B   /   #��z��5C   /   #��z��5D   /   #���  5E   0   $��50��5F   /   
������ $��Center=�6T   ��5A   /   ��  �� $ ExpanderL�$ Header�5F   T   �� 5G   "   $��1��6-   
������ .+�5H   &   �� $ 
ExpandSite��67   $��Bottom=�6K   |�.  #��z��6u   #��z��6�   #��z��6�   $y�	Collapsed=�6  5I   #   5J      5K       5L      P� 5M       �@9�
IsExpanded . . �5N   "   �� $ 
ExpandSite��6?   
 � y�$ Visible=�6W   5O      5Q      5S      5T      �� 5U   
   "	 ��6(   8   .�5V      �� 
 � ��$ 50��6'   �� 5W      
 � ��$ 10��6&   �� 5X      
 � ��
 ���� 6,   �� 5Y      
 � �� �#17181f6+   5Z      �� 5[   
   "	 	�6(   .�5\      �� 
 � ��
 �����6+   �� 5]      
 � ��
 ���� 6+   �� 5^      
 �@�FocusVisualStyle 61   Z� �� 5_      
 � ��$ Center=�69   �� 5`      
 � ��$ Center=�6;   �� 5a      
 � ��$ Center=�62   �� 5b      
 � ��$ Center=�64   �� 5c      
 � ��$	 150��6)   �� 5d      
 � �� 5e      �� 5f      " 	�6&   .R�5g      �� $ _border��6!   $��0 0 0 1q�62   ._�5h      � $ _grid��6#   .+�5i   "   �� #��z��6(   #��z��5j   (   #��z��5k   (   #��z��5l   (   #��z��5m   (   #��z��5n   (   $��0��5o   (   5p      5q       5r      P� 5s       �@d�	IsChecked$
 TrueY�6;   . �5t   "   �� 8   $ _grid��6}   
 � ��5u      5w      5y      5z      5{      .��5|      � -   5        $
 grid��5|       5}   
   �� 5~      �� 5      $��40 �6    5�   
   �� 5�      $	��150 �6   �� 5�      $��9* �6   .+�5�   
   �� -   5         ��Name$
 Menu��5�      $	��800��6#   $
��Auto=�63   
���   �$��0��6v   $��0��6�   $��Stretch=�6�   �� 5�   
   $��1��6   $��0��6)   ��66   �� *MainContent+]� 5�   
   ��. $��0��6*   $��2��6:   $��1��6N   $��2q�6[     SideBar.BackGround#��[ 6f   .+�5�      #  HandyControl.Controls.Divider  
�������6   �� 5�      #��[ 6   $��Bottom=�6I   .@�5�      �� .��5�      ��  �� Orientation$ 
Horizontal=�6!   .+�5�      �� $��Right=�6$     
NormalText#��[ 6;   $��Bottom=�6_   $��12(�6{   $��0 0 10 0q�6�   
�������  footer_version#��[ 6�   �� 5�      $��Right=�6$   #��[ 6;   $��Bottom=�6_   $��12(�6{   $��0 0 20 0q�6�   
���������6�   �� *CurrentVersion+5�      5�      �� 5�      �� 5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $��Right=�6$   #��[ 6;   $��Bottom=�6_   $��12(�6{   $��0 0 10 0q�6�   
�������  footer_config_database#��[ 6�   �� 5�      $��Right=�6$   #��[ 6;   $��Bottom=�6_   $��12(�6{   $��0 0 20 0q�6�   
���������6�   �� *ConfigDbVersion+5�      5�      �� 5�      �� 5�      .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $
��Left=�6$   #��[ 6:   $��Bottom=�6^   $��12(�6y   $��20 0 0 0q�6�   
�������  footer_current_project_path#��[ 6�   �� 5�      $
��Left=�6$   #��[ 6:   $��Bottom=�6^   $��12(�6y   $��20 0 0 0q�6�   
���������6�   �� *CurrentProjectPath+5�      5�      5�      5�      5�      5�      