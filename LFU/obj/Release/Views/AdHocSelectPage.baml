   M S B A M L   `   `   `  ����   LFU    LFU.Views.AdHocSelectPage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  AdHocSelectPage�� N� 
Background
 �����.��]� .+�� $	��Top=�$��	0,10,0,10q�$	��Top=�.+���  �� Orientation$ 
Horizontal=�$��
10,0,50,10q� � ToolTip$ Page controls��.+��� -    ��Name$ btnPrevPage��$
��Prev��$��0,0,0,0q��� -   $ btnNextPage��$
��Next��$��10,0,0,0q��� -    ��Name$ tbCurrentPage��$��10,0,3,0q� �� TextWrapping$
 Wrap=�$��1��$��60��$��90��$ Current Page���� $��Center=�of�� -   $ 
tbLastPage��$��3,0,0,0q�$
 Wrap=�
������ $��60��$��90�� �� 
IsReadOnly . $ 
Final Page���� $��10,0,0,0q�$��Center=�Size�� -   $ tbPageRowCount��$ NoWrap=�$��60��$��90��$��3,0,0,0q�$$ Page Size in Number of Records���� -    ��Name$ tblTotalRowCount��$��3,0,0,0q�$��Center=����� 	 �� Path$	 TotalRowCount�� $��0q�$��Bottom=�.+�&   System.Windows.Controls.DataGrid  -   
  Name$
 dgData��$
��Left=�
�������   AlternatingRowBackground
 �����   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader    CanUserSortColumns .    CanUserDeleteRows .     CanUserReorderColumns .    CanUserResizeRows .    CanUserResizeColumns . ���� 