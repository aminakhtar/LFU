   M S B A M L   `   `   `  ����  LFU    LFU.Db.LawImportExport   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  LAW Import\Export��5   	   $	��450��5   	   $	��650��6   $	��450��5   	   $	��600��6   $	��600��5   	   $	��900��6   ���#94D500 � WindowStyle$ 
ToolWindow=�5	   	   .��5      ]� ���#94D500$��Stretch=�6%   .+�5   
   ]� -   5         ]�Name$ dockInterface��5      $��
10,10,10,0q�6+   $	��Top=�6?   $��Stretch=�6T   .+�5      �� $	��Top=�6    �� Orientation$ 
Horizontal=�6.   $��0,0,0,20q�6G   .+�5      �� $	��100��6   $��Center=�6(   $
��Bold"�6C   
Connection6a   �� 5      -   5         ��Name$ cmboConnections��5      $	��200��64   6y   5      �� 5      $	��Top=�6   $ Vertical=�6.   $��0,0,0,20q�6E   .+�5      �� $ 
Horizontal=�6   $��0,0,0,10q�66    � ToolTip$ Match on these fields��6H   .+�5      �� $	��100��6    $
��Bold"�6,   Identity field6N   �� 5!      $ Vertical=�6!   $��0,0,10,5q�68   $3 -Match records on this field from the loadfile��6J   .+�5"      �� $
��Left=�6$   
Loadfile6I   �� 5#      -   5        $ cmboIdentityLoadfile��5#   #   $	��200��6A   ��6P   �� *FieldNamesAsDisplayed+5$      �� 5'      $ Vertical=�6!   $��0,0,10,5q�68   $6 0Match records on this field in the case database��6J   .+�5(      �� $
��Left=�6$   Law6D   �� 5)      -   5        $ cmboIdentityCase��5)   #   $	��200��6=   5*      5,      �� 5/      $ 
Horizontal=�6   $��0,0,0,10q�66   .+�51      �� $	��100��6    �� 53      $ Vertical=�6!   $��0,0,10,5q�68   $3 -Match records on this field from the loadfile��6J   .+�54      �� $
��Left=�6$   $5 /This WHERE CLAUSE is applying on tbldoc fields.��6?   WHERE clause6�   �� 55      -   5         ��Name$( "tbWhereClauseAdditionalExpressions��55   "   $	��410��6N   $	��600��6]    �� TextWrapping$ WrapWithOverflow=�6l    �� AcceptsReturn . $� �The platform will be queried for records where the selected 
identity field IS NOT NULL in addition to (i.e., AND) any 
expressions that you add here.

The WHERE clause is applied to tblDoc when importing or to 
LFU's backend when exporting.��56   &   57      58      �� 5;      $ 
Horizontal=�6   $3 -Update the loadfile value with the case value��66   .+�5=      �� $	��100��6    $
��Bold"�6,   Update fields6M   �� 5@      $ Vertical=�6!   $��0,0,10,5q�68   $3 -Match records on this field from the loadfile��6J   .+�5A      �� $
��Left=�6$   
Loadfile6I   �� 5B      -   5        $ cmboUpdateLoadfile��5B   #   $	��200��6?   ��6N   �� *FieldNamesAsDisplayed+5C      �� 5F      $ Vertical=�6!   $��0,0,10,5q�68   .+�5G      �� $
��Left=�6$   Law6D   �� 5H      -   5        $ cmboUpdateCase��5H   #   $	��200��6;   5I      �� 5L      -	   5        	 ��Name$	 btnAdd��5L      $��20��6-   $��Bottom=�69   $��0,0,0,5q�6T   $&  Add this pair to your import job��6z   .��6�   Add6�   5N      �� 5Q      $	��200��6   $��50��6/   $��0,0,0,20q�6>   $9 3Loadfile fields to be updated with values from case��6P   .��5R      �� -
   5        
 ��Name$
 stackUpdateFields��5R   !   $ Vertical=�6<   $��
90,5,10,10q�6S   5S      5U      5W      ]� 5Z   
   -   5        $ dockProgBar��5Z      $��Bottom=�6)   $��Bottom=�6A   .+�5\      ]� $	��Top=�6   $��Bottom=�6-   ���#545759.+�5_      �� -   5         ��Name$ 	tblStatus��5_      $��10,10,10,10q�6/   $
��Left=�6D   $��Center=�6Z   
�������6u   �� 5b      -   5        $	 	btnCancel��5b      $��
0,10,10,10q�6,   $��Right=�6X   $��Right=�6o   .��6�   Cancel6�   �� 5e      -   5        $
 stackAction��5e      $ 
Horizontal=�62   $��Right=�6K   $D >Import from LAW to LFU or export (aka overlay) from LFU to LAW��5f      $��Right=�5g      .+�5h      	� -   5         	�Name$ 	rdoImport��5h   "    	� 	GroupName$ Action��65   $��Import��6H   $��12(�6Y   
�������$��0,0,10,0q�6�   	� 5i      -   5        $ 	rdoExport��5i   "   $ Action��65   $��Export��6H   $��12(�6Y   
�������$��0,0,10,0q�6�   �� 5j      -   5        $	 btnGo��5j      $��
0,10,10,10q�6,   .��6T   Go6X   5k      5m      � 5p      -   5         �Name$ progBar��5p      $��Bottom=�6+   $��10��6C    � Value$ 0Q�6O    � Maximum$	 100Q�6Y    � Minimum$ 0Q�6g    � IsIndeterminate .  $y�	Collapsed=�6�   6�   5q      5s      5v      