   M S B A M L   `   `   `  ����  LFU    LFU.MaintWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  MaintWindow��5   	   $	��300��6   $	��700��6*    � WindowStyle$ 
ToolWindow=�5   	    � Topmost . ���#545759 � 	Resources 5
      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter��%�  '        �� 5   
   "  6   .�5      ��  �@ ColumnWidth$ SizeToHeader 6,   �� 5       �@ CanUserAddRows .  6/   �� 5       �@ CanUserDeleteRows .  62   �� 5       �@ 
IsReadOnly . 6+   �� 5       � ��$ 0,10,0,0q�6'   �� 5       �@ GridLinesVisibility$
 None=�64   �� 5       � ��$ Consolas, Courier New)�6+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      6!   .+�5!   
   �� -   5        	 ��Name$	 tabcontrolMain��5!      $��0��6-   $��0��6:   $��0q�6J   .@�5$      �� $��0q�6   $��Backend Databases��6!   .��5%      ]� ���#648B1A.+�5&      � $	��Top=�6    
 � Orientation$
 
Horizontal=�65   $��
10,10,10,0q�6N   $��Right=�6b   .+�5'      �� -   5         ��Name$ btnScan��5'   !   $��0,0,10,0q�6H   .��6Z   Scan6`   �� 5(      -   5        $ 	btnDelete��5(   !   .��6M   Delete6U   5)        5+      -   5          Name$ dgDatabaseFiles��5,      $��Bottom=�5-         AlternatingRowBackground �#94D5005.      5/      50      �� 53      $��0q�6   $��Platform Connections��6!   .��54      ]� 
�������.+�55        -   5        $# dgPlatformDatabaseConnections��56      $��Bottom=�57      
 �����58      59      5:      5<      ]� 5?   
   $��2��6   $��0��6!   ���#545759$��40��6F   .+�5@      �� -   5         ��Name$ 	tblStatus��5@      $��Bottom=�6+   
�������$��10,10,10,10q�6V   6m   5A      5C      5D      