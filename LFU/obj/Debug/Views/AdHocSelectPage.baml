   M S B A M L   `   `   `  ����  LFU    LFU.Views.AdHocSelectPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  AdHocSelectPage��5       N� 
Background �#648B1A.��5      ]� .+�5   
   � $	��Top=�6   $��	0,10,0,10q�6-   $	��Top=�6@   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��
10,0,50,10q�62    � ToolTip$ Page controls��6F   .+�5      �� -   5         ��Name$ btnPrevPage��5      $
��Prev��6H   $��0,0,0,0q�6W   �� 5      -   5        $ btnNextPage��5      $
��Next��6H   $��10,0,0,0q�6W   �� 5      -   5         ��Name$ tbCurrentPage��5      $��10,0,3,0q�63    �� TextWrapping$
 Wrap=�6E   $��1��6Y   $��60��6b   $��90��6p   $ Current Page��6�   �� 5      $��Center=�6   
�������of6N   �� 5      -   5        $ 
tbLastPage��5      $��3,0,0,0q�6.   $
 Wrap=�6?   
������ $��60��6l   $��90��6z    �� 
IsReadOnly . $ 
Final Page��6�   �� 5      $��10,0,0,0q�6   $��Center=�6.   
�������Size6b   �� 5      -   5        $ tbPageRowCount��5      $ NoWrap=�62   $��60��6I   $��90��6W   $��3,0,0,0q�6e   $$ Page Size in Number of Records��6�   �� 5      -   5         ��Name$ tblTotalRowCount��5      $��3,0,0,0q�6Z   $��Center=�6k   ��66   �� 	 �� Path$	 TotalRowCount�6k   5      5      � 5!   
   $��0q�6   $��Bottom=�6   .+�5"      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 dgData��5"      $
��Left=�6'   ���#E0E0E0   AlternatingRowBackground �#E0E0E0   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader 5#   O      CanUserSortColumns .    CanUserDeleteRows .     CanUserReorderColumns .    CanUserResizeRows .    CanUserResizeColumns . ��5%      �� 5&      5'      5)      5+      