   M S B A M L   `   `   `  ����  LFU    LFU.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Loadfile Utility��5   	   $	��500��5   	   $
��1000��6   ���#545759 � Icon$* $Aha-Soft-Agriculture-Cow-history.ico��5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6-   �� 5      6   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   ]� -   5        $��0��5      $��0��6!   $	��Top=�61   $��	0,10,0,10q�6I    � ToolTip$6 0You may drag and drop a loadfile into this panel��6\   ���#648B1A J� 	AllowDrop . .+�5       �� -   5         ��Name$ btnOpen��5       $��10,0,5,0q�6<   $
��Left=�6N   $ Open a loadfile��6d   .��6~   Open6�   �� 5!      -   5        $ btnSqlConsole��5!      $��5,0,5,0q�6,   {�.  $
��Left=�6k   $ Open the SQLite console��6�   .��6�   SQL Console6�   �� 5"      -   5        $ btnPlatformConnect��5"      $��5,0,5,0q�61   {�. $
��Left=�6t   $; 5Create a connection to a processing platform database��6�   .��6�   Platform Connect6�   �� 5$      -   5        $ btnAbout��5$      $��5,0,10,0q�6'   {�. $��Right=�6J   $5 /App version, documentation, configuration, etc.��6x   .��6�   About6�   �� 5%      -   5        $ btnMaint��5%      $��5,0,5,0q�6'   {�. $��Right=�6I   $J DClean up orphaned databases, and 
examine open database connections.��6|   .��6�   Maint6�   5'      ]� 5*   
   $��1��6   $��0��6!   $��	10,0,10,0q�61   .+�5+      �� -   5         ��Name$ tabcontrolMain��5+      $	��Top=�61   $��0q�6F   ���#E0E0E0$��0q�6o   5,      �� 5/   
   -	   5         ��Name$ MyStatusBar��5/   1   $��2��6   $��0��6!   
������ $��30��6_   $��0,0,0,0q�6k   .@�50      �� -
   5        $��	tblStatus��50      $��5,5,10,0q�6)   $	��Top=�6;   
�������6S   51      53      54      