   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullA%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      +    %DataViewConfig.Controls.SearchTextBox   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:DataViewConfig.Controls     5      -   5          >�Name$  	MyControl��5      $_��Ypack://application:,,,/DataViewConfig;component/Fonts/fontawesome-webfont.ttf#FontAwesome)�5	   7   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��15 �6   6,   �� 5      $��* �6   6+   �� 5      $��26 �6   6,   �� 5      $��36 �6   6,   .+�5   
   �� $��0��6   $��4��6$   
�������
������ $��30��6Z    �� RadiusX$ 5��6f    �� RadiusY$ 5��6r    J� Opacity$	 0.8Q�6~   6�   �� 5   
   -   5         ��Name$ TbxInput��5      $��1��6%   
������ $��35��6O   .��6[   ��5      �� 5       �� 
TargetType" ��6"   .R�5      � .+�5      ��  ��Name$ 
Uc_TblShow��6$   
�������$	 0.5Q�6�   $��Center=�6�   $y�	Collapsed=�6�   ��68   ��  �� ElementName$ 	MyControl��	 �� Path$	 
PreviewTxt�6�   �� 5      $ Uc_TbxContent��6"   -   5        $	��250��5   9      NormalTextBox#��[  6E   
�������
������ $��Center=�6�   $��Center=�6�   $��0q�6�   $��18(�5   �   ��6"   �� $ TbxInput��$
	 Text�
 �� Mode$
 TwoWay=�6�   5       �� Triggers 5      P� 5       P� 
SourceName$ Uc_TbxContent��6"    P� Property � �� P� Value$  ��6M   . �5      ��  �� 
TargetName$ 
Uc_TblShow��6%    �� Property � y� �� Value$ Visible=�6S   6e   5      P� 5      $ Uc_TbxContent��6"    �@J�	IsFocused . . �5       �� $ 
Uc_TblShow��6%    � y�$ 	Collapsed=�6S   6g   5!      5#      .��5$       5%      �� 5&   
   -   5         ��Name$ 	BtnDelete��5&      $��2��6$   $y�	Collapsed=�64    � Cursor$
 Hand��6c    �� 	IsDefault .   	BaseStyle#��[ 6�   
������ ��5'      �� 5(      " ��6"   .R�5)      � .+�5*      ��  ��Name$ Uc_Image��6    #��[ 62     
IconFamily#��[ 5+       
�������$��Center=�5,   2   $��Center=�6V   $	����6x   $��26��6�   $��26��6�   6�   �� 5-      6-   5.       5/      P� 50       �@J�IsMouseOver . . �51      �� $ Uc_Image��6%    � ��$ 26��6M   6Z   �� 52      $ Uc_Image��6%    � ��$ 26��6L   6Y   53      55      57      �� 58   
   -   5        $ 	BtnSearch��58      $��3��6$   $
 Hand��6N    . #��[ 6m   
������ ��59      �� 5:      " ��6"   .R�5;      � .+�5<      �� $ Uc_Image��6    #��[ 62   #��[ 5=       
�������$��Center=�5>   3   $��Center=�6W   $	����6y   $��35��6�   $��35��6�   6�   �� 5?      6-   5@       5A      P� 5B       �@J�IsMouseOver . . �5C      �� $ Uc_Image��6%    � ��$ 25��6M   6Z   �� 5D      $ Uc_Image��6%    � ��$ 25��6L   6Y   5E      5G      5I      5J      5K      