   M S B A M L   `   `   `  ����   LFU    LFU.OpenLoadfileDialog   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Open Loadfile��$	��500��$	��900�� J� 	AllowDrop .  � Icon$* $Aha-Soft-Agriculture-Cow-history.ico�� � ShowInTaskbar .  .���  � RowDefinitions �� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� .+�]� $��0��$��0��
�������.+��  � Orientation$ 
Horizontal=�$��
10,0,10,20q�$	��Top=�.+�]� .+��� $��	0,10,10,0q�$��80��$��Center=�
Filepath�� -    ��Name$ 
tbFilepath��$��	0,10,10,0q�$	��400��$��Stretch=�$
��Auto��$
��Left=��� -    ��Name$ 	btnBrowse��$��Center=�$��	0,10,10,0q�$��Right=�.��Browse]� $��1��$��0��
�������.+�]� -   	 ]�Name$	 dockPreviewButtons��$	��Top=�.+�]� .+��� -   
 ��Name$
 tblLoadfileType��$��10,10,10,10q�$
��Bold"�$��16(�
�����_�$
��Left=�]� $��Right=�$��Right=�.+��� -   $ 
btnPreview��$��10,10,10,10q�.��	Preview�� -   $ btnSelectFields��$��
0,10,10,10q�.��Select Fields�� $��Bottom=�.���� $��10,10,10,20q� �� Orientation$ Vertical=�.+��� -    ��Name$ stackEncoding��$ 
Horizontal=�$y�	Collapsed=�.+��� $��0,5,10,5q�$��80��
Encoding�� -    ��Name$ cmboEncodings��$��	0,5,10,10q�{�.  �� -	    ��Name$ chkAutoDetectEncoding��$��10,10,10,10q� d� 	IsChecked$
 TrueY�{�. $��Center=�$y�Visible=� � ToolTip$5 /Uncheck if you need to manually select encoding��.��Autodetect Encoding�� -
   $ stackFieldPreview��$ 
Horizontal=�$y�	Collapsed=�$) #Comma-separated list of field names��.+��� $��0,5,10,5q�$��80��Field Names�� -   $
 tblFieldNames��$��	0,5,10,10q�$
��Wrap=�$
��Auto��$	��800��� -    �Name$ wrapDelimiters��$ 
Horizontal=�$y�	Collapsed=�.+��� $��0,5,10,5q�$��80��$
��Left=�
Delimiters�� $ Vertical=�.+��� $��0,5,10,0q�Field�� -   $ cmboDelimField��$��0,5,10,5q�$��Courier New)��� $ Vertical=�.+��� $��0,5,10,0q�Text�� -   $ cmboDelimText��$��0,5,10,5q�$��Courier New)��� $ Vertical=�.+��� $��0,5,10,0q�Line�� -   $ cmboDelimLineBreakSub��$��	0,5,10,10q�$��Courier New)��� -   $ stackHasHeaders��$ 
Horizontal=�$y�	Collapsed=�$��0,10,0,0q�.+��� $��0,0,10,0q�$��80��Has Headers?�� -   $ chkHasHeaders��]� $��2��$��0��
�����_�.+��� $ 
Horizontal=�$
��Left=�.+��� -   $
 	tblStatus��$��10,10,10,10q�
�������$	��Top=��� $ 
Horizontal=�$��Right=�$��10,10,10,10q�$��Right=�.+��� -   $ 	btnCancel��$��0,0,10,0q�.��Cancel�� -   $ btnLoad��.��Load