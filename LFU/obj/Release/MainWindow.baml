   M S B A M L   `   `   `  ����   LFU    LFU.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Loadfile Utility��$	��500��$
��1000��
�����_� � Icon$* $Aha-Soft-Agriculture-Cow-history.ico��.���  � RowDefinitions �� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� .+�]� -   $��0��$��0��$	��Top=�$��	0,10,0,10q� � ToolTip$6 0You may drag and drop a loadfile into this panel��
������  J� 	AllowDrop . .+��� -    ��Name$ btnOpen��$��10,0,5,0q�$
��Left=�$ Open a loadfile��.��Open�� -   $ btnSqlConsole��$��5,0,5,0q�{�.  $
��Left=�$ Open the SQLite console��.��SQL Console�� -   $ btnPlatformConnect��$��5,0,5,0q�{�. $
��Left=�$; 5Create a connection to a processing platform database��.��Platform Connect�� -   $ btnAbout��$��5,0,10,0q�{�. $��Right=�$5 /App version, documentation, configuration, etc.��.��About�� -   $ btnMaint��$��5,0,5,0q�{�. $��Right=�$J DClean up orphaned databases, and 
examine open database connections.��.��Maint]� $��1��$��0��$��10,0,0,0q�.+��� -    ��Name$ tabcontrolMain��$	��Top=�$��0q�
������$��0q��� -	    ��Name$ MyStatusBar��$��2��$��0��
������ $��30��$��0,0,0,0q�.@��� -
   $��	tblStatus��$��5,5,10,0q�$	��Top=�
�������