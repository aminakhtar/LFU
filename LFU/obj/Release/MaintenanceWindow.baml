   M S B A M L   `   `   `  ����   LFU    LFU.MaintWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  MaintWindow��$	��300��$	��700�� � WindowStyle$ 
ToolWindow=� � Topmost . 
�����_� � 	Resources &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter��%  '        �� "  .���  �@ ColumnWidth$ SizeToHeader ��  �@ CanUserAddRows .  ��  �@ CanUserDeleteRows .  ��  �@ 
IsReadOnly . ��  � ��$ 0,10,0,0q���  �@ GridLinesVisibility$
 None=���  � ��$ Consolas, Courier New)�.���  � RowDefinitions �� �� $
��Auto � � ColumnDefinitions �� .+��� -   	 ��Name$	 tabcontrolMain��$��0��$��0��$��0q�.@��� $��0q�$��Backend Databases��.��]� 
�������.+�� $	��Top=�
 � Orientation$
 
Horizontal=�$��
10,10,10,0q�$��Right=�.+��� -    ��Name$ btnScan��$��0,0,10,0q�.��Scan�� -   $ 	btnDelete��.��Delete  -     Name$ dgDatabaseFiles��$��Bottom=�   AlternatingRowBackground
 ������� $��0q�$��Platform Connections��.��]� 
�������.+�  -   $# dgPlatformDatabaseConnections��$��Bottom=�
 �����]� $��2��$��0��
�����_�$��40��.+��� -    ��Name$ 	tblStatus��$��Bottom=�
�������$��10,10,10,10q�