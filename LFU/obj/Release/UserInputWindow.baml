   M S B A M L   `   `   `  ����   LFU    LFU.UserInputWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$4  .UserInputWindowTitleShouldBeSetInConstructorYo��$	��250��$	��400�� � WindowStyle$ 
ToolWindow=�
�������.���  � RowDefinitions �� �� $
��Auto � � ColumnDefinitions �� .+��� $��0��$��0�� �� Orientation$ Vertical=�$��10q�.+�]� .+��� -    ��Name$ tbInput��$	��Top=�$��0,0,0,10q�$	��300�� � ToolTip$% Press enter to queue the input.�� �� AcceptsReturn . �� -    ��Name$ stackInputs��$ Vertical=�$	��200��]� $��1��$��0��
�����_�.+�� $
��Left=�$
��Auto��.+��� -   	 ��Name$	 	tblStatus��$��10q�$
��Wrap=�
�������$	��400���� $��Right=�$ 
Horizontal=�$��Right=�$��Bottom=�$��10q�.+��� -   
 ��Name$
 	btnCancel��$��10,0,0,0q�.��Cancel�� -   $
 btnOk��$��10,0,0,0q�.��OK