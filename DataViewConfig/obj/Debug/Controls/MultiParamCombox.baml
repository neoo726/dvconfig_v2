   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullA%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      .    (DataViewConfig.Controls.MultiParamCombox   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:DataViewConfig.Controls     5        � 	Resources  5	         NormalBrush �� 
StartPoint �� EndPoint � Color � Offset  NormalBorderBrush  
GlyphBrush �� Color  	DarkBrush  PressedBrush  DisabledForegroundBrush  DisabledBackgroundBrush  WindowBackgroundBrush  SelectedBackgroundBrush 	 ComboBox.Static.Border '
 #ComboBox.Static.Editable.Background # ComboBox.Static.Editable.Border . *ComboBox.Static.Editable.Button.Background * &ComboBox.Static.Editable.Button.Border  ComboBox.MouseOver.Glyph ! ComboBox.MouseOver.Background  ComboBox.MouseOver.Border * &ComboBox.MouseOver.Editable.Background & "ComboBox.MouseOver.Editable.Border 1 -ComboBox.MouseOver.Editable.Button.Background - )ComboBox.MouseOver.Editable.Button.Border  ComboBox.Pressed.Glyph  ComboBox.Pressed.Background  ComboBox.Pressed.Border ( $ComboBox.Pressed.Editable.Background $  ComboBox.Pressed.Editable.Border / +ComboBox.Pressed.Editable.Button.Background + 'ComboBox.Pressed.Editable.Button.Border  ComboBox.Disabled.Glyph   ComboBox.Disabled.Background  ComboBox.Disabled.Border ) %ComboBox.Disabled.Editable.Background %  !ComboBox.Disabled.Editable.Border 0! ,ComboBox.Disabled.Editable.Button.Background ," (ComboBox.Disabled.Editable.Button.Border # ComboBox.Static.Glyph $ ComboBoxTextBox �� 
TargetType ��Name % ComboBoxToggleButton �� 
TargetType	 �� Property
 �� Value �� CornerRadius J� SnapsToDevicePixels F�Name �� Triggers ^� 
Conditions �� Binding �� RelativeSource �� AncestorType �� Value �� 
TargetName P� Property P� Value �� Padding��%�  &	        &	 �     &	 T    &	 r    &	     &	      &	 >    &	 \    &	 z    &		 �    &	
 �    &	 �    &	     &	 (    &	 M    &	 p    &	     &	 '    &	 F    &	 i    &	     &	 (    &	 K    &	 �    &	 
    &	 -    &	 P    &	 �    &	     &	 2    &	 U    &	 x    &	  �    &	! �    &	" �    &	# 	    &	$ +	    &	% �	    7 
 7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7   7 ! 7 " '�� �    �� 5
   
   $	 0,0&�62   $	 0,1&�6C   ��5      � � 5      $
 #FFF��6#   $	 0.0Q�60   � 5      $
 #CCC��6#   $	 1.0Q�60   5      5      �� 5   
   $	 0,0&�68   $	 0,1&�6I   ��5      � � 5      $
 #CCC��6#   $	 0.0Q�60   � 5      $
 #444��6#   $	 1.0Q�60   5      5      �� 5   
   $
 #444��6-   �� 5   
   $	 0,0&�60   $	 0,1&�6A   ��5       � � 5!      $
 #FFF��6#   $	 0.0Q�60   � 5"      $
 #AAA��6#   $	 1.0Q�60   5#      5%      �� 5'   
   $	 0,0&�63   $	 0,1&�6D   ��5)      � � 5*      $
 #BBB��6#   $	 0.0Q�60   � 5+      $
 #EEE��6#   $	 0.1Q�60   � 5,      $
 #EEE��6#   $	 0.9Q�60   � 5-      $
 #FFF��6#   $	 1.0Q�60   5.      50      �� 52   
   $
 #888��6:   �� 54   
   $
 #EEE��6:   �� 56   
   $
 #FFF��68   �� 58   
   $
 #DDD��6:   �� 5:   
   $ 	#FFACACAC��69   �� 5<   
   $ Transparent��6F   �� 5>   
   $ 	#FFABADB3��6B   �� 5@   
   $ Transparent��6M   �� 5B   
   $ Transparent��6I   �� 5D   
   $ 	#FF000000��6;   �� 5F   
   $	 0,1&�6D   $	 0,0&�6S   .��5G      � $ Black��6   $	 0.0Q�6)   � 5H      $ Black��6   $	 1.0Q�6)   5I      �� 5K   
   $ 	#FF7EB4EA��6<   �� 5M   
   $ Black��6I   �� 5O   
   $ 	#FF7EB4EA��6E   �� 5Q   
   $	 0,1&�6T   $	 0,0&�6c   .��5R      � $ 	#FFEBF4FC��6   $	 0.0Q�6-   � 5S      $ 	#FFDCECFC��6   $	 1.0Q�6-   5T      �� 5V   
   $ 	#FF7EB4EA��6L   �� 5X   
   $ 	#FF000000��69   �� 5Z   
   $	 0,1&�6B   $	 0,0&�6Q   .��5[      � $ 	#FFDAECFC��6   $	 0.0Q�6-   � 5\      $ 	#FFC4E0FC��6   $	 1.0Q�6-   5]      �� 5_   
   $ 	#FF569DE5��6:   �� 5a   
   $ 	#FFFFFFFF��6G   �� 5c   
   $ 	#FF569DE5��6C   �� 5e   
   $	 0,1&�6R   $	 0,0&�6a   .��5f      � $ 	#FFDAEBFC��6   $	 0.0Q�6-   � 5g      $ 	#FFC4E0FC��6   $	 1.0Q�6-   5h      �� 5j   
   $ 	#FF569DE5��6J   �� 5l   
   $ 	#FFBFBFBF��6:   �� 5n   
   $ 	#FFF0F0F0��6?   �� 5p   
   $ 	#FFD9D9D9��6;   �� 5r   
   $ 	#FFFFFFFF��6H   �� 5t   
   $ 	#FFBFBFBF��6D   �� 5v   
   $ Transparent��6O   �� 5x   
   $ Transparent��6K   �� 5z   
   $ 	#FF3C77DD��68   �� 5|   
   " ��62   .R�5}      �� $ PART_ContentHost��6   |�.  #��z��6A   5~      �� 5   
   " d�6-   .�5�      �� 	 �@�OverridesDefaultStyle
 . 66   �� 5�      	 � ��
 .  6*   �� 5�      	 � |�
 .  6*   �� 5�      	 �@��	ClickMode$
 Press=�6*   �� 5�      	 � ��
 5�      �� 5�      " d�6&   .R�5�      �� $ templateRoot��6!   $ 2��67   
�������$��1q�6[    . 
���   �._�5�      �� $ splitBorder��6%   
������ $��1q�6U   $��Right=�6i   $��0,0,2,0q�6�    . #��� r�6�   ._�5�   "   F� $ arrow��6'   F����        �8�7 ��( ��o     ��o P�����7 � �    P����        
�������$��Center=�6�   $��Center=�6�   5�      5�       5�      ^� 5�       5�   "   �� 5�   &    60   �� *
IsEditable+ �� " �� true6�   �� 5�   &    60   �� *IsMouseOver+ �� *Self+	 false6v   �� 5�   &    60   �� *	IsPressed+ �� *Self+	 false6t   �� 5�   &    60   �� *	IsEnabled+ �� *Self+ true6t   .0�5�   "   �� $ templateRoot��6?   8
   	 � ��6Y   �� 5�   "   $ templateRoot��6@   8
  	 � ��6Z   �� 5�   "   $ splitBorder��6?   8
  	 � ��6X   �� 5�   "   $ splitBorder��6@   8
  	 � ��6Y   5�      P� 5�       �@J�IsMouseOver . . �5�   "   �� $ arrow��69   8
  	 � ��6L   5�      ^� 5�       5�   "   �� 5�   &    60   �� *IsMouseOver+ �� *Self+ true6v   �� 5�   &    60   �� *
IsEditable+ �� " ��	 false6�   .0�5�   "   �� $ templateRoot��6?   8
  	 � ��6Y   �� 5�   "   $ templateRoot��6@   8
  	 � ��6Z   5�      ^� 5�       5�   "   �� 5�   &    60   �� *IsMouseOver+ �� *Self+ true6v   �� 5�   &    60   �� *
IsEditable+ �� " �� true6�   .0�5�   "   �� $ templateRoot��6?   8
  	 � ��6Y   �� 5�   "   $ templateRoot��6@   8
  	 � ��6Z   �� 5�   "   $ splitBorder��6?   8
 	 	 � ��6X   �� 5�   "   $ splitBorder��6@   8
 
 	 � ��6Y   5�      P� 5�       � �� . . �5�   "   �� $ arrow��69   8
  	 � ��6L   5�      ^� 5�       5�   "   �� 5�   &    60   �� *	IsPressed+ �� *Self+ true6t   �� 5�   &    60   �� *
IsEditable+ �� " ��	 false6�   .0�5�   "   �� $ templateRoot��6?   8
  	 � ��6Y   �� 5�   "   $ templateRoot��6@   8
  	 � ��6Z   5�      ^� 5�       5�   "   �� 5�   &    60   �� *	IsPressed+ �� *Self+ true6t   �� 5�   &    60   �� *
IsEditable+ �� " �� true6�   .0�5�   "   �� $ templateRoot��6?   8
  	 � ��6Y   �� 5�   "   $ templateRoot��6@   8
  	 � ��6Z   �� 5�   "   $ splitBorder��6?   8
  	 � ��6X   �� 5�   "   $ splitBorder��6@   8
  	 � ��6Y   5�      P� 5�       � {� .  . �5�   "   �� $ arrow��69   8
  	 � ��6L   5�      ^� 5�       5�   "   �� 5�   &    60   �� *	IsEnabled+ �� *Self+	 false6t   �� 5�   &    60   �� *
IsEditable+ �� " ��	 false6�   .0�5�   "   �� $ templateRoot��6?   8
  	 � ��6Y   �� 5�   "   $ templateRoot��6@   8
  	 � ��6Z   5�      ^� 5�       5�   "   �� 5�   &    60   �� *	IsEnabled+ �� *Self+	 false6t   �� 5�   &    60   �� *
IsEditable+ �� " �� true6�   .0�5�   "   �� $ templateRoot��6?   8
  	 � ��6Y   �� 5�   "   $ templateRoot��6@   8
  	 � ��6Z   �� 5�   "   $ splitBorder��6?   8
  	 � ��6X   �� 5�   "   $ splitBorder��6@   8
  	 � ��6Y   5�      5�      5�      5�      �� 5�   
   " ��6.   .�5�      �� 	 �@J�SnapsToDevicePixels
 . 64   �� 5�      	 �@�OverridesDefaultStyle
 . 66   �� 5�      	 � ��
 5�      �� 5�      " ��6&   .R�5�      �� $��Border��5�      $ 2q�5�       . ._�5�      �� 5�       5�      P� 5�       �@��IsHighlighted . . �5�   "   �� $ Border��6)   	 � ��

 ���� 6S   5�      P� 5�       � {� .  . �5�   "   �� 	 � ��

 �  ��6?   5�      5�      5�      5�      5�      .��5�      � .+�5�   
   �� -   5         ��Name$ CheckableCombo��5�      ��5�      �� 5�      .R�5�      �� -   5        
�������$	��Top=�5�   8    d� 	IsChecked 5�      �� *
IsSelected+ �� Mode$ TwoWay=�
 � Tag 5�      �� *FindAncestor+" ��.��5�      ��  �� Orientation$ Vertical=�6%   .+�5�      �� $��Bottom=�6C   ��6(   �� *	ParamName+6C   �� 5       & Global.Gray#��[& 6C   ��6(   �� *	ParamDesc+6C   5     5     5     ��5     �� 5     " ��6"   .R�5     � .+�5     d� $��ToggleButton��6'   #��[% 5	  '   |�.   5  '   ��  �� Path$ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+5     �� 5      ��Name$ 	Presenter��6+    J� IsHitTestVisible .  $��3, 3, 23, 3q�5  +   $��Center=�5  +   $
��Left=�5  +   ��5     �� 5  "   $��CharacterEllipsis=�6,   
�������
������ ��5  ,   �� $
 Text�$ TwoWay=� ��   �� Mode$  FindAncestor=�$ UserControlL�5  #   5     �� 5     ! ��Name$! EditableTextBox��6"   #��[$ 5     $
��Left=�5     $��Center=�5     $��3,3,23,3q�5     |�. 
������ $y�Hidden=�5      " �� 
IsReadOnly# �� 
IsReadOnly## z" 5!     ��5     Z� 5!     � 5"     $��Popup��5#     $��Bottom=�5$     $ �� IsDropDownOpen#��z$ 5%     % � AllowsTransparency% . |�.  $��Slide=�5(     .��5)     � $��DropDown��5*  #    . & � ActualWidth#��z& 5,  #   ' �� MaxDropDownHeight#��z' 5-  #   .+�5.  "   �� $ DropDownBorder��5/  %   
���   �$��1q�51  %   �� 52  "   $��4,6,4,6q�6/    . ( � DataContext( 6[   �� .��53  &   �� ) K� IsItemsHost) . -  'System.Windows.Input.KeyboardNavigation*   DirectionalNavigation$* 	Contained=�6D   54  #   55     56     57      58     P� 59      �@��HasItems .  . �5:     �� $ DropDownBorder��6%   	 � ��$
 95��6V   5;     P� 5<      � {� .  . �5=     �� #
 [ 6;   	 � ��5>     P� 5?      �@��
IsGrouping . . �5@     �� 	 � ��
 .  6N   5A     P� 5B     + P� 
SourceName$+ Popup��6"    �@�AllowsTransparency . . �5C     �� $ DropDownBorder��6%   	 �@��CornerRadius$
 4��6Y   �� 5D     $ DropDownBorder��6%   	 � ��$
 0,2,0,0q�6S   5E     P� 5F      �@��
IsEditable . . �5H     �� 	 � ��
 .  6:   �� 5I     $ EditableTextBox��6%   	 � y�$
 Visible=�6[   �� 5J     $ 	Presenter��6%   	 � y�$
 Hidden=�6R   5K     5M     5O     5P     5Q     