   M S B A M L   `   `   `  ����>  :LFU, Version=2.5.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LFULFU  5   	       LFU.MergeWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:LFU     5   	   -   5          � Title$  Merge Window��5   	   $	��230��5	   	   $	��300��5
   	    � WindowStartupLocation$ CenterOwner=�5   	   ���#94D500.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� $��First Column:��6   $��0��6(   $��0��65   $��5q�6E   6R   �� 5   
   -   5         ��Name$ cmbFirst��5      $��0��6&   $��1��63   $��5q�6C    �� SelectedIndex$ 0��6N   6b   �� 5   
   $��Between two columns:��6   $��1��60   $��0��6=   $��5q�6M   6Z   �� 5   
   -   5         ��Name$ 
txtBetween��5      $��1��6'   $��1��64   $��5q�6D   6Q   �� 5!   
   $��Second Column:��6   $��2��6*   $��0��67   $��5q�6G   6T   �� 5"   
   -   5        $ 	cmbSecond��5"      $��2��6'   $��1��64   $��5q�6D   $ 0��6O   6c   �� 5$   
   $��Result Column:��6   $��3��6*   $��0��67   $��5q�6G   6T   �� 5%   
   -   5        $ 	cmbResult��5%      $��3��6'   $��1��64   $��5q�6D   $ 0��6O   6c   �� 5'   
   $��4��6   $��5q�6"   $��2��6-   .+�5(   
   �� -   5         ��Name$ btnMerge��5(      $��Merge��6#   
�������6d   5)      5*      5+      