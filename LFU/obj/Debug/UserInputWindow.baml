   M S B A M L   `   `   `  ����  LFU    LFU.UserInputWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$4  .UserInputWindowTitleShouldBeSetInConstructorYo��5   	   $	��250��5   	   $	��400��6    � WindowStyle$ 
ToolWindow=�5   	   ���#94D500.��5
      �  � RowDefinitions 5   
   �� 5      6   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   �� $��0��6   $��0��6"    �� Orientation$ Vertical=�62   $��10q�6I   .+�5      ]� .+�5      �� -   5         ��Name$ tbInput��5      $	��Top=�6+   $��0,0,0,10q�6@   $	��300��6R    � ToolTip$% Press enter to queue the input.��6w    �� AcceptsReturn . 6�   5      �� 5      -   5         ��Name$ stackInputs��5      $ Vertical=�6.   $	��200��6E   5      ]� 5   
   $��1��6   $��0��6!   ���#545759.+�5      � $
��Left=�6   $
��Auto��6.   .+�5      �� -   5        	 ��Name$	 	tblStatus��5      $��10q�6/   $
��Wrap=�6;   
�������$	��400��6b   6s   5       �� 5!      $��Right=�6   $ 
Horizontal=�60   $��Right=�6I   $��Bottom=�6e   $��10q�6�   .+�5"      �� -   5        
 ��Name$
 	btnCancel��5"      $��10,0,0,0q�6,   .��6V   Cancel6^   �� 5#      -   5        $
 btnOk��5#      $��10,0,0,0q�6(   .��6N   OK6R   5$      5%      5(      5*      