   M S B A M L   `   `   `  ����   LFU    LFU.Db.LawImportExport   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  LAW Import\Export��$	��450��$	��650��$	��450��$	��600��$	��600��$	��900��
������� � WindowStyle$ 
ToolWindow=�.��]� 
�������$��Stretch=�.+�]� -    ]�Name$ dockInterface��$��
10,10,10,0q�$	��Top=�$��Stretch=�.+��� $	��Top=� �� Orientation$ 
Horizontal=�$��0,0,0,20q�.+��� $	��100��$��Center=�$
��Bold"�
Connection�� -    ��Name$ cmboConnections��$	��200���� $	��Top=�$ Vertical=�$��0,0,0,20q�.+��� $ 
Horizontal=�$��0,0,0,10q� � ToolTip$ Match on these fields��.+��� $	��100��$
��Bold"�Identity field�� $ Vertical=�$��0,0,10,5q�$3 -Match records on this field from the loadfile��.+��� $
��Left=�
Loadfile�� -   $ cmboIdentityLoadfile��$	��200������ *FieldNamesAsDisplayed+�� $ Vertical=�$��0,0,10,5q�$6 0Match records on this field in the case database��.+��� $
��Left=�Case�� -   $ cmboIdentityCase��$	��200���� $ 
Horizontal=�$��0,0,0,10q�.+��� $	��100���� $ Vertical=�$��0,0,10,5q�$3 -Match records on this field from the loadfile��.+��� $
��Left=�WHERE clause�� -    ��Name$( "tbWhereClauseAdditionalExpressions��$	��410��$	��600�� �� TextWrapping$ WrapWithOverflow=� �� AcceptsReturn . $� �The platform will be queried for records where the selected 
identity field IS NOT NULL in addition to (i.e., AND) any 
expressions that you add here.

The WHERE clause is applied to tblDoc when importing or to 
LFU's backend when exporting.���� $ 
Horizontal=�$3 -Update the loadfile value with the case value��.+��� $	��100��$
��Bold"�Update fields�� $ Vertical=�$��0,0,10,5q�$3 -Match records on this field from the loadfile��.+��� $
��Left=�
Loadfile�� -   $ cmboUpdateLoadfile��$	��200������ *FieldNamesAsDisplayed+�� $ Vertical=�$��0,0,10,5q�.+��� $
��Left=�Case�� -   $ cmboUpdateCase��$	��200���� -	   	 ��Name$	 btnAdd��$��20��$��Bottom=�$��0,0,0,5q�$&  Add this pair to your import job��.��Add�� $	��200��$��50��$��0,0,0,20q�$9 3Loadfile fields to be updated with values from case��.���� -
   
 ��Name$
 stackUpdateFields��$ Vertical=�$��
90,5,10,10q�]� -   $ dockProgBar��$��Bottom=�$��Bottom=�.+�]� $	��Top=�$��Bottom=�
�����_�.+��� -    ��Name$ 	tblStatus��$��10,10,10,10q�$
��Left=�$��Center=�
��������� -   $	 	btnCancel��$��
0,10,10,10q�$��Right=�$��Right=�.��Cancel�� -   $
 stackAction��$ 
Horizontal=�$��Right=�$D >Import from LAW to LFU or export (aka overlay) from LFU to LAW��$��Right=�.+�	� -    	�Name$ 	rdoImport�� 	� 	GroupName$ Action��$��Import��$��12(�
�������$��0,0,10,0q�	� -   $ 	rdoExport��$ Action��$��Export��$��12(�
�������$��0,0,10,0q��� -   $	 btnGo��$��
0,10,10,10q�.��Go� -    �Name$ progBar��$��Bottom=�$��10�� � Value$ 0Q� � Maximum$	 100Q� � Minimum$ 0Q� � IsIndeterminate .  $y�	Collapsed=�