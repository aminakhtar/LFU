   M S B A M L   `   `   `  ����>  :LFU, Version=2.5.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:LFU.DateTimeFormattingLFU.DateTimeFormatting  5   	   ,    &LFU.DateTimeFormatting.ConvertDateTime   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:LFU.DateTimeFormatting     5   	   -   5          � Title$  Convert DateTime��5   	   $	��150��5	   	   $	��600��5
   	   ���#94D500.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� $��0��6   $��0��6   $��Input Column:��6-   $��5q�6E   
������6j   �� 5   
   -   5         ��Name$ cmbInputColumn��5       �� SelectedIndex$ 0��6+   $��0��6=   $��1��6J   $��5q�6Z   
������6   �� 5   
   $��Output Column:��6   $��0��6)   $��2��66   $��5q�6F   
�������6j   �� 5   
   -   5        $ cmbOutputColumn��5      $ 0��6,   $��0��6>   $��3��6K   $��5q�6[   
�������6   �� 5    
   $��1��6   $��0��6   $��Input Format:��6-   $��5q�6E   
������6j   �� 5!   
   -   5        $ cmbInputFormat��5!      $ 0��6+   $��1��6=   $��1��6J   $��5q�6Z   
������6   �� 5#   
   $��Output Format:��6   $��1��6)   $��2��66   $��5q�6F   
�������6j   �� 5$   
   -   5        $ cmbOutputFormat��5$      $ 0��6,   $��1��6>   $��3��6K   $��5q�6[   
�������6   ]� 5&   
   $��2��6   $��0��6!   .+�5'      �� -   5         ��Name$ 	btnUpdate��5'      $��Update��6(   $��5q�69   6^   5(      ]� 5*   
   $��2��6   $��1��6!   .+�5+      �� -   5        $ 	btnCancel��5+      $��Cancel��6(   $��5q�69   6^   5,      5-      5.      