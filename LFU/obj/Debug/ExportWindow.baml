   M S B A M L   `   `   `  ����  LFU    LFU.ExportWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Export Loadfile��5   	   $	��400��5   	   $	��700��6    � Icon$* $Aha-Soft-Agriculture-Cow-history.ico��5   	    � ShowInTaskbar .  .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      6   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   ]� $��0��6   $��0��6!   ���#648B1A.+�5      �  � Orientation$ 
Horizontal=�6   $��10,10,10,20q�61   $	��Top=�6F   .+�5      �� $��0,0,10,0q�6   $��Center=�6.   
Filepath6S   �� 5      -   5         ��Name$ 
tbFilepath��5      $��0,0,10,0q�6/   $��Center=�6A   $	��400��6\   $	��900��6k    �� TextWrapping$
 Wrap=�6z    �� 
IsReadOnly . 6�   �� 5      -   5        	 ��Name$	 	btnBrowse��5      $��Center=�6,   $��25��6G   .��6j   ...6o   5      5      ]� 5   
   $��1��6   $��0��6!   ���#94D500.+�5"      � $ 
Horizontal=�6   $	��Top=�61   $��10,10,10,10q�6F   .+�5#      �� $	��110��6   $��Center=�6(   Loadfile Type6R   �� 5$      -   5        
 ��Name$
 cmboLoadfileType��5$      $	��200��65   $��Center=�6A   {�.  6p   5%      � 5(      -   5         �Name$ wpanelOutputEncoding��5(      $ 
Horizontal=�66   $	��Top=�6O   $��10,10,10,10q�6d   $y�Visible=�6y   .+�5)      �� $	��110��6   $��Center=�6)   Output Encoding6U   �� 5*      -   5        $
 cmboEncodings��5*      $	��200��63   $��0,0,20,0q�6?   $��Center=�6Q   6�   �� 5+       �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5,      �� (6#   �� 5-      -   5         ��Name$  tblLoadfileCurrentEncoding��5-       6F   �� 5.      )6#   5/      50      � 53      -   5        $ wpanelDelimiters��53      $ 
Horizontal=�62   $y�Visible=�6K   $	��Top=�6`   $��10,10,10,10q�6u   .+�56      �� $��0,5,10,5q�6   $	��110��6/   $��Center=�6;   
Delimiters6b   �� 59      $��0,5,10,5q�6   $��Center=�6.   Field6P   �� 5:      -   5        $
 cmboDelimField��5:      $��0,5,10,5q�63   $��Courier New)�6E   $��Center=�6^   6y   �� 5=      $��0,5,10,5q�6   $��Center=�6.   Text6O   �� 5>      -	   5        $
 cmboDelimText��5>      $��0,5,10,5q�62   $��Courier New)�6D   $��Center=�6]   6x   �� 5A      $��0,5,10,5q�6   $��Center=�6.   Line6O   �� 5B      -
   5        $
 cmboDelimLineBreakSub��5B      $��0,5,10,5q�6:   $��Courier New)�6L   $��Center=�6e   6�   5D      � 5G      -   5        $ wpanelIncludeHeaders��5G      $ 
Horizontal=�66   $y�Visible=�6O   $	��Top=�6d   $��10,10,10,10q�6y   .+�5H      �� $	��110��6   Include Headers?6;   �� 5I      -   5         ��Name$ chkIncludeHeaders��5I      $��Center=�66   6�   5J      5L      ]� 5O   
   $��2��6   $��0��6!   ���#545759.+�5Q      �� $ 
Horizontal=�6   $
��Left=�62   .+�5R      �� -   5        $ 	tblStatus��5R      $��10,10,10,10q�6/   
�������$��Center=�6W   6t   5S      �� 5U      $ 
Horizontal=�6   $��Right=�62   $��10,10,10,10q�6N   $��Right=�6c   .+�5V      �� -   5        $	 	btnCancel��5V      $��0,0,10,0q�6,   .��6V   Cancel6^   �� 5W      -   5        $	 btnSave��5W      $��Bottom=�6@   .��6[   Save6a   5X      5Z      5\      5^      