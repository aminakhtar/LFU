   M S B A M L   `   `   `  ����  LFU    LFU.SelectHeader   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  SelectWindow��5   	    � Title$ Select Fields��5   	   $	��500��5   	   $	��300��6    � WindowStyle$ 
ToolWindow=�5   	   ���#648B1A � ShowInTaskbar .  .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   �� $��0��6   $��0��6$   $��Stretch=�64   .��5      �� -   5         ��Name$ ListBoxHeaders��5      ���#94D500$��Stretch=�6c   $��10,10,10,10q�6�   $	��Top=�6�   ��6.   �� *	TheList+��5      �� 5      .R�5      �� -   5        $��	10,0,10,0q�5   a    d� 	IsChecked 6#   �� *
IsSelected+��6E   �� *	TheText+6t   5      5      5      ]� 5    
   $��0��6   $��1��6$   ���#545759.+�5"      �� -   5         ��Name$ 	tblStatus��5"      
�������$	��Top=�6>   6V   � 5$      $��Bottom=�6   	 � Orientation$	 
Horizontal=�60   $��Right=�6I   .+�5%      �� -   5        
 ��Name$
 	btnCancel��5%      $��10,10,10,10q�6D   .��6Y   Cancel6a   �� 5&      -   5        $
 	btnSelect��5&      $��Select/Unselect All��6A   $��
0,10,10,10q�6_   �� 5'      -   5        $
 btnUseSelected��5'      $��
0,10,10,10q�6N   .��6b   Use Selected6p   5(      5*      5+      5-      