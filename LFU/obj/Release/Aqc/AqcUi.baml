   M S B A M L   `   `   `  ����   LFU    LFU.Aqc.AqcUi   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Assay and QC��$	��400��$	��800��
������� � ShowInTaskbar .   � WindowStyle$ 
ToolWindow=�.���  � RowDefinitions �� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� $��4* ��� $��5* �.+�]� $��0��$��0��$��2��
������.+���  �� Orientation$ 
Horizontal=�$��10,10,10,10q�$
��Left=�$��Center=��� $ 
Horizontal=�$��10,10,10,10q�$��Right=�$��Right=�.+��� -    ��Name$ btnGo��$
��Auto�� � ToolTip$ Engage the process��.��Go]� $��0��$��1��$��
10,10,5,10q�.+��� -    ��Name$ lstPluginsAvailable��$	��Top=�$ Available plugins��$��0q����� .R��� $
��Auto��$��0,0,0,5q�$ Vertical=�.+��� $ 
Horizontal=�$��3,3,3,3q�$	��Top=�.+��� $
��Bold"�$
��Wrap=�$��10(����� 	 �� Path$	 
PluginName��� $�� (��$��10(��� $
��Wrap=�$��10(����� $	 PluginAuthor��� $��) ��$��10(��� $ Vertical=�$��3,0,3,10q�$	��Top=�.+��� $��Italic$�$
��Wrap=�$��10(����� $	 PluginDescription�]� $��1��$��1��$��
5,10,10,10q�.+��� -   $ lstPluginsSelected��$ Queued processes��$��0q�]� $��0��$��2��$��2��
�����_�$ Status messages��.+��� -   
 ��Name$
 	tblStatus��$��10,10,10,10q�$��WrapWithOverflow=�
�������