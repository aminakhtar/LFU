   M S B A M L   `   `   `  ����   LFU    LFU.Views.TablePage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� 
Background
  ����� N� Title$ 	TablePage��.��]� .+�� $��	0,10,0,10q�$	��Top=�.+���  �� Orientation$ 
Horizontal=�$��
10,0,50,10q� � ToolTip$ Page controls��.+��� -    ��Name$ btnPrevPage��$
��Prev��$��0,0,0,0q��� -   $ btnNextPage��$
��Next��$��10,0,0,0q��� -    ��Name$ tbCurrentPage��$��10,0,3,0q� �� TextWrapping$
 Wrap=�$��1��$��60��$��90��$ Current Page���� $��Center=�of�� -   $ 
tbLastPage��$��3,0,0,0q�$
 Wrap=�
������ $��60��$��90�� �� 
IsReadOnly . $ 
Final Page���� $��10,0,0,0q�$��Center=�Size�� -   $ tbPageRowCount��$ NoWrap=�$��60��$��90��$��3,0,0,0q�$$ Page Size in Number of Records���� -    ��Name$ tblTotalRowCount��$��3,0,0,0q�$��Center=����� 	 �� Path$	 TotalRowCount��� $ 
Horizontal=�$��	10,0,0,10q�$W QDrop, Sort, Index, and Filter operations will be performed on the selected column��.+��� $��Column��$��Center=�$��0,0,0,0q��� -   
 ��Name$
 cmboFunctionField�� �� SelectedIndex$ 0��$	��150��$	��300��$��3,0,0,0q����� $	 FieldNamesAsDisplayed��� -   $ btnDropColumn��$
��Drop��$��10,0,0,0q�$) #Drop selected column from the table���� -	   $ btnAddColumn��$	��Add��$��10,0,0,0q�$, &Add a column (or columns) to the table���� -
   $ btnSort��$
��Sort��$��10,0,0,0q�$5 /Sort the entire loadfile on the selected column���� -   $ btnIndex��$��Index��$��10,0,0,0q�$, &Create an index on the selected column���� -   $ 	btnFilter��$��Filter��$��10,0,0,0q�$= 7Select only loadfile records with a given search string���� -   $ tbSearchTerm��$��10,0,0,0q�$
 Wrap=�$��Search\Filter Term��$	��200��$	��400��$1 +A string to search for in the loadfile data���� -   $ HitsResultLabel��$�� ��$��Center=�$��10,0,0,0q��� -   $ 	btnImport��$��10,0,0,0q�$��Import\Export��$W QImport data from a LAW case to LFU, 
or export (aka overlay) data to LAW from LFU���� -   $ btnAqc��$	��AQC��$��10,0,0,0q�{�.  $7 1Assay and Quality Control (is under construction)��� $��Bottom=�.+�&   System.Windows.Controls.DataGrid  -     Name$ dgData��
�������   AlternatingRowBackground
 �����   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader    CanUserSortColumns .    CanUserDeleteRows .     CanUserReorderColumns .    CanUserResizeRows .    CanUserResizeColumns . ���� 