   M S B A M L   `   `   `  ����   LFU    LFU.SelectHeader   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  SelectWindow�� � Title$ Select Fields for Loading��$	��500��$	��300�� � WindowStyle$ 
ToolWindow=�
������� � ShowInTaskbar .  .���  � RowDefinitions �� �� $
��Auto � � ColumnDefinitions �� .+��� $��0��$��0��$��Stretch=�.���� -    ��Name$ ListBoxHeaders��
������$��Stretch=�$��10,10,10,10q�$	��Top=����� *	TheList+���� .R��� -   $��	10,0,10,0q� d� 	IsChecked �� *
IsSelected+���� *	TheText+]� $��0��$��1��
�����_�.+��� -    ��Name$ 	tblStatus��
�������$	��Top=�� $��Bottom=�	 � Orientation$	 
Horizontal=�$��Right=�.+��� -   
 ��Name$
 	btnCancel��$��10,10,10,10q�.��Cancel�� -   $
 	btnSelect��$��Select/Unselect All��$��
0,10,10,10q��� -   $
 btnUseSelected��$��
0,10,10,10q�.��Use Selected