   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=null;"clr-namespace:DataViewConfig.PagesDataViewConfig.Pages  5      A%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      E'clr-namespace:DataViewConfig.ViewModelsDataViewConfig.ViewModels  5	      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5
      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      )    #DataViewConfig.Pages.NaviConfigPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:DataViewConfig.Pages     5      :controls%clr-namespace:DataViewConfig.Controls     5      =	converter'clr-namespace:DataViewConfig.Converters     5      6vm'clr-namespace:DataViewConfig.ViewModels     5	      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5
      6sys&clr-namespace:System;assembly=mscorlib     5         Content.BackGround#��[  5      
�������$��15(�6R   $��5q�5        � 	Resources  5      9  3DataViewConfig.Converters.TemplateRadiobtnConverter  templateConverter8  2DataViewConfig.Converters.Bool2VisibilityConverter  bool2VisibilityConverter?  9DataViewConfig.Converters.Bool2ReverseVisibilityConverter # bool2ReverseVisibilityConverter5  /DataViewConfig.Converters.Bool2InverseConverter  bool2ReverseboolConverter��%t   &	       &	      &	 &     &	 9       5   
   6.     5   
   6-     5   
   64     5   
   6*   5      .��5      �� .+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      ��  $  config_view_current_project_type#��[ 6     NormalGroupBox#��[ 6R   y�6z   �� *IsDataViewConfig+ �� 	Converter# [ .��5      �� $ 
Horizontal=�6   $��	20 10 0 0q�66   .+�5      	�  	� 	GroupName$ ProjectType��6"    & "config_view_project_type_rxg_crane#��[ 6:   
�������$��0 0 20 0q�6�    d� 	IsChecked 6y   �� *IsRxgProject+6�   	� 5      $ ProjectType��6"    % !config_view_project_type_qc_crane#��[ 6:   
�������$��0 0 20 0q�6�    6w   �� *IsStsProject+6�   	� 5      $ ProjectType��6"    (	 $config_view_project_type_mixed_crane#��[	 6:   
������� 6z   �� *IsCombinedProject+6�   �� 5       
 config_view_operate_save#��[
 6   $��crane��6|     
TextButton#��[ 6�   ��6Q   �� *SaveProjectTypeCommand+6�   5!      5"      �� 5#        config_view_current_ros#��[ 6   #��[ 6I   y�6q   �� *IsDataViewConfig+# [ .��5$      �� $ 
Horizontal=�6   $��	20 10 0 0q�66   .+�5%      ��   config_view_current_ros_id#��[ 6    $	��100��6S     
NormalText#��[ 6_   �� 5&      $��50��67     NormalTextBox#��[ 6B   ��6   �� *RcsNO+6B   �� 5'      #��[
 6   $��crane��6v   #��[ 6�   ��6Q   �� *SaveRcsNoCommand+6�   5(      5)      �� 5*        config_view_base_info#��[ 6   #��[ 6G   .��5+      �� $ Vertical=�6   $��20q�64   .+�5,      �� $ 
Horizontal=�6!   $��0 10 0 0q�6:   .+�5-      ��   config_view_base_crane#��[ 6$   $��70��6S   #��[ 6^   �� 5.      $��50��6B   #��[ 6M   ��6$   �� *
CraneCount+6M   �� 5/        config_view_operate_edit#��[ 6!   $��crane��6u   #��[ 6�   ��6U   �� *EditCommand+6�   50      �� 51      $ 
Horizontal=�6!   $��0 10 0 0q�6:   .+�52      ��   config_view_base_ros#��[ 6$   $��70��6Q   #��[ 6\   �� 53      $��50��6@   #��[ 6K   ��6$   �� *
RcsCount+6K   �� 54      #��[ 6!   $	��rcs��6u   #��[ 6�   ��6U   �� *EditCommand+6�   55      56      57      �� 59         config_view_interactive_info#��[ 6   #��[ 6N   .��5:      �� $ Vertical=�6   $��20q�64   .+�5<      �� $ 
Horizontal=�6!   $��0 10 0 0q�6:   .+�5=      �� -   5         ��Name$ exSystemItemsControl��5=   '   ��6E   �� *ExSystemInfoLst+ �� UpdateSourceTrigger$ PropertyChanged=���5>      �� 5?   "   .R�5@   &   �� $ 
Horizontal=�61   .+�5A   *   ��  " config_view_interactive_object#��[ 64   $��70��6k   $��5q�6w   #��[ 6�   �� 5B   *   {�.   �� 
IsReadOnly . $��70��6o   $��5q�6z   #��[ 6�   ��62   �� *	EcsName+6�   �� 5C   *    . *config_view_interactive_communication_type#��[ 64   $��5q�6y   #��[ 6�   	� 5D   *    	�Name$ IsMqRadioBtn��66   $��RabbitMQ��6L   {�.  $��Center=�6q   $	��100��6�   
������� 6�   �� *
IsRabbitMQ+	� 5E   *   $ IsOpcRadioBtn��66   $	��OPC��6M   {�.  $��Center=�6m   $��70��6�   
������� 6�   �� *IsOPC+	� 5F   *   $ IsRestRadioBtn��66   $
��Rest��6N   {�.  $��Center=�6o   $��70��6�   
������� 6�   �� *IsRest+	� 5G   *   $ IsRedisRadioBtn��66   $��Redis��6O   {�.  $��Center=�6q   $��70��6�   
������� 6�   �� *	IsRedis+�� 5S   *   #��[ 61   #��[ 5U   R   ��5T   -   �� 	 �� RelativeSource	 �� 
 �� AncestorType"
 �� �� Path$ DataContext.EditCommand���5U   -   �� *	EcsName+6R   5V   '   5W   #   5Y      5Z      5[      5\      �� 5]        config_view_language_config#��[ 6   #��[ 6M   .��5^      �� $ 
Horizontal=�6   $��20q�66   .+�5_      	� $��中文��6"     NormalRadioButton#��[ 60    6[   �� *IsChineseLanguageChecked+6�   	� 5`      $��English��6"   #��[ 65    6a   �� *IsChineseLanguageChecked+# [ 6�   �� 5a      #��[
 6   $��crane��6   #��[ 6�   ��6Q   �� *SaveLanguageConfigCommand+6�   5m      5n      5o      5p      5q      