   M S B A M L   `   `   `  ����I  EDataViewConfig, Version=2.0.0.8, Culture=neutral, PublicKeyToken=nullA%clr-namespace:DataViewConfig.ControlsDataViewConfig.Controls  5      /clr-namespace:DataViewConfigDataViewConfig  5      E'clr-namespace:DataViewConfig.ConvertersDataViewConfig.Converters  5      R NDataView_Configuration, Version=1.0.0.79, Culture=neutral, PublicKeyToken=null_Dclr-namespace:DataView_Configuration;assembly=DataView_ConfigurationDataView_Configuration 5	      .    (DataViewConfig.Controls.NewReceiveFanout   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:DataViewConfig.Controls     5      0currentclr-namespace:DataViewConfig     5      =	converter'clr-namespace:DataViewConfig.Converters     5      Z	dv_configDclr-namespace:DataView_Configuration;assembly=DataView_Configuration     5	      
�������  � 	Resources  5      5  /DataViewConfig.Converters.Bool2InverseConverter   Bool2Inverse8  2DataViewConfig.Converters.Bool2VisibilityConverter  Bool2VisibleE  ?DataViewConfig.Converters.ReceiveWrite_None2VisibilityConverter  ReceiveWrite_None2VisibleG  ADataViewConfig.Converters.ReceiveWrite_Normal2VisibilityConverter  ReceiveWrite_Normal2VisibleJ  DDataViewConfig.Converters.ReceiveWrite_Condition2VisibilityConverter " ReceiveWrite_Condition2VisibleD  >DataViewConfig.Converters.ReceiveStoreType2VisibilityConverter  ReceiveStore2VisibleI  CDataViewConfig.Converters.ReceiveStoreCacheType2VisibilityConverter  ReceiveStoreCache2Visible��%�   &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r       5   
   6*     5   
   6-     5   
   6:     5   
   6<     5   
   6?     5   
   69     5   
   6>   5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5       �� $��接口名称��6   $��5,0,5,0q�6$     
NormalText#��[ 65   �� 5!        NormalTextBox#��[ 6   $	��150��6]   $��5,0,50,0q�6i   ��6=   �� *ReceiveMsgType+6i   �� 5#      $��交互对象��6   $��5,0,5,0q�6%   #��[ 66   �� 5$       	 NormalCombox#��� 	 6   $	��100��6>   $��5,0,50,0q�6J    �� DisplayMemberPath$ Description��5&       �� SelectedValuePath$ Value��6c   ��5%      ��  �� Source )  #DataViewConfig.EnumerationExtension  *M� *dv_config:RequestSystemEnum++ �� SelectedValue 5&   7   �� *ReceiveSystemName+6c   �� 5(      $��接收类型��6   $��5,0,5,0q�6%   #��[ 66   �� 5)      #��� 	 6   $	��200��6>   $��5,0,50,0q�6J   $ Description��5+      $ Value��6b   ��5*      ��    *M� *dv_config:DvReceiveTypeEnum++ 5+   8   �� *ReceiveTypeName+6b   �� 5-      $(��"设备/操作台区分字段名称��6   $��5,0,5,0q�6-   #��[ 6>   �� 5.      #��[ 6   $	��100��6^   $��5,0,5,0q�6k   ��6=   �� *DeviceFiledName+6k   5/      �� 51   
   $��1��6   $ 
Horizontal=�6"   $��5q�6<   .+�52      �� $��数据存储��6   $��5,0,5,0q�6%   #��[ 66   �� 53      -   5         ��Name$ StoreTypeCombox��53      #��� 	 60   $	��250��6W   $��5,0,50,0q�6c   $ Description��55      $ Value��6a   ��54      ��    *M� *" dv_config:DvReceiveStoreTypeEnum++ 55   7   �� *ReceiveStorType+6a   �� 57      $ 
Horizontal=�6   y�62   ��  �� ElementName$ StoreTypeCombox��	 �� Path$	 SelectedIndex�
 �� 	Converter#
 [ .+�58      �� $-��'立即写入的数组点名（内部）��6   $��5,0,5,0q�62   #��[ 6C   6i   �� 59      #��� 	 6   $	��170��6B   $��5,0,50,0q�6N   $ Value��5:   }   ��6   �� *FullStoreTagLst+ �� SelectedItem 6?   �� *FullStoreTagInternalName+ �� Mode$ TwoWay=�6}   5<      �� 5=      $ 
Horizontal=�6   y�63   �� $ StoreTypeCombox��$	 SelectedIndex�#
 [ .+�5>      �� $��写入Memory点方式��6   $��5,0,5,0q�61   #��[ 6B   �� 5?      -   5        $ WriteTypeCombox��5@      #��� 	 63   $	��200��6Z   $��5,0,50,0q�6f   $ Description��5B      $ Value��6b   ��5A      ��    *M� *" dv_config:DvReceiveWriteTypeEnum++ 5B   7   �� *ReceiveWriteType+6b   �� 5E      $ 
Horizontal=�6   y�66   �� $ WriteTypeCombox��$	 SelectedIndex�#
 [ .+�5F      �� $��写入条件��6�   $��5,0,5,0q�6�   #��[ 6�   y�6    �� $ WriteTypeCombox��$	 SelectedIndex�#
 [ 6�   �� 5H      #��[ 6�   $	��100��6�   $��5,0,10,0q�6�   y�6   �� $ WriteTypeCombox��$	 SelectedIndex�#
 [ ��6�   �� *CacheWriteCondition+6�   �� 5I      $��编辑��6�    
 NormalButton#��� 
 6�   $��5,0,50,0q�6�   $��50��6�   $�� ��6  y�6   �� $ WriteTypeCombox��$	 SelectedIndex�#
 [ ��6�   �� *EditReturnCommand+6  �� 5K      $��缓存点名��6�   $��5,0,5,0q�6�   #��[ 6�   y�6    �� $ WriteTypeCombox��$	 SelectedIndex�#
 [ 6�   �� 5L      #��� 	 5M       $	��170��6G   $��5,0,50,0q�6S   $ Value��5N   �   y�5L       �� $ WriteTypeCombox��$	 SelectedIndex�#
 [ ��5N      �� *CacheTagLst+$ OneWay=� 6G   �� *CacheTagInternalName+$ TwoWay=�6�   5P      5Q      5T      �� 5V   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   $��30q�6X   .+�5W      �� $��添加��6   #��� 
 6"   $	��150��6I   $�� ��6t   ��6U   �� *
AddCommand+6t   5X      5Y      5Z      