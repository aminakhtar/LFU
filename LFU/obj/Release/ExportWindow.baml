   M S B A M L   `   `   `  ����   LFU    LFU.ExportWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Export Loadfile��$	��400��$	��700�� � Icon$* $Aha-Soft-Agriculture-Cow-history.ico�� � ShowInTaskbar .  .���  � RowDefinitions �� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� .+�]� $��0��$��0��
�������.+��  � Orientation$ 
Horizontal=�$��10,10,10,20q�$	��Top=�.+��� $��0,0,10,0q�$��Center=�
Filepath�� -    ��Name$ 
tbFilepath��$��0,0,10,0q�$��Center=�$	��400��$	��900�� �� TextWrapping$
 Wrap=� �� 
IsReadOnly . �� -   	 ��Name$	 	btnBrowse��$��Center=�$��25��.��...]� $��1��$��0��
�������.+�� $ 
Horizontal=�$	��Top=�$��10,10,10,10q�.+��� $	��110��$��Center=�Loadfile Type�� -   
 ��Name$
 cmboLoadfileType��$	��200��$��Center=�{�.  � -    �Name$ wpanelOutputEncoding��$ 
Horizontal=�$	��Top=�$��10,10,10,10q�$y�Visible=�.+��� $	��110��$��Center=�Output Encoding�� -   $
 cmboEncodings��$	��200��$��0,0,20,0q�$��Center=���  �� Orientation$ 
Horizontal=�$��Center=�.+��� (�� -    ��Name$  tblLoadfileCurrentEncoding���� )� -   $ wpanelDelimiters��$ 
Horizontal=�$y�Visible=�$	��Top=�$��10,10,10,10q�.+��� $��0,5,10,5q�$	��110��$��Center=�
Delimiters�� $��0,5,10,5q�$��Center=�Field�� -   $
 cmboDelimField��$��0,5,10,5q�$��Courier New)�$��Center=��� $��0,5,10,5q�$��Center=�Text�� -	   $
 cmboDelimText��$��0,5,10,5q�$��Courier New)�$��Center=��� $��0,5,10,5q�$��Center=�Line�� -
   $
 cmboDelimLineBreakSub��$��0,5,10,5q�$��Courier New)�$��Center=�� -   $ wpanelIncludeHeaders��$ 
Horizontal=�$y�Visible=�$	��Top=�$��10,10,10,10q�.+��� $	��110��Include Headers?�� -    ��Name$ chkIncludeHeaders��$��Center=�]� $��2��$��0��
�����_�.+��� $ 
Horizontal=�$
��Left=�.+��� -   $ 	tblStatus��$��10,10,10,10q�
�������$��Center=��� $ 
Horizontal=�$��Right=�$��10,10,10,10q�$��Right=�.+��� -   $	 	btnCancel��$��0,0,10,0q�.��Cancel�� -   $	 btnSave��$��Bottom=�.��Save