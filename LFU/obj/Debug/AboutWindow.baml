   M S B A M L   `   `   `  ����  LFU    LFU.AboutWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  About��5   	    � WindowStyle$ 
ToolWindow=�5   	   $	��300��5   	   $	��500��6   ���#94D500 � Icon$* $Aha-Soft-Agriculture-Cow-history.ico��5   	    � 
ResizeMode$ NoResize=�5	   	    � 	Resources 5       �� 
TargetType   Tooltip �� Property �� Value  TooltipHeader  TooltipBody��%(  &	        &	 �     &	 3    �� 5   
   " ��6   .�5      ��  � ��$	 400��6)   67   �� 5       �@��Orientation$ Vertical=�6,   6?   5      �� 5   
   " ��6   .�5      ��  � ��$
 Bold"�6+   6:   �� 5       � ��$ Normal$�6*   6;   �� 5       � ��$ 0,0,0,5q�6'   69   5      �� 5   
   " ��6   .�5      ��  � ��$ Normal"�6+   6<   �� 5        � ��$ Normal$�6*   6;   �� 5!       � ��$ 0,0,0,5q�6'   69   �� 5"       � ��$
 Wrap=�6-   6<   5#      5%      .��5'      �  � RowDefinitions 5)   
   �� 5*      $
��Auto �6   6,   �� 5+      5-      	 � ColumnDefinitions	 5/   
   �� 50      6!   .+�54   
   ]� $��0��6   $��0��6!   ���#545759.+�57      �� $
��Left=�6   .+�59      �� -   5        
 ��Name$
 tblTitle��59      $��20(�6.   
�������$
��Bold"�6O   $��
10,10,10,0q�6a   Assembly title6�   �� 5;      -   5        $
 tblDescription��5;      
�������$��
10,0,10,10q�6G   Assembly description6q   5<      �� 5?      $��Right=�6   .+�5@      �� -   5         ��Name$ imgFruit��5@      $*��$Aha-Soft-Agriculture-Cow-history.ico��6*   $��32��6X   $��Center=�6c   $��Right=�6~   $��	0,10,10,0q�6�    � ToolTip$ 	Cow Books��5A       ^� InitialShowDelay$
 5000��6,   5B      5D      ]� 5G   
   $��1��6   $��0��6!   $��0,0,0,20q�63   .+�5J      ��  �� Orientation$ Vertical=�6   $
��Left=�60   $��Bottom=�6F   $��10,0,0,0q�6a   .+�5M      �� -   5        $
 
tblVersion��5M      $��0,10,0,0q�60   assembly version6T   �� 5O      -   5        $
 tblCopyright��5O      $��0,10,0,0q�62   assembly copyright6X   �� 5Q      -   5        $
 tblPdcLoadfilesVersion��5Q      $��0,10,0,0q�6<   $ Loadfiles library version��6N   Pdc.Loadfiles version6�   �� 5S      -   5        $
 tblQuestionsEmailAddress��5S      $��0,10,0,0q�5T      $��Italic$�6.   $y�Visible=�6A   $ Support and suggestions��6V   $	��300��5U      $
��Wrap=�6+   support email address5W      5Y      �� 5\      $��Right=�6   $ 
Horizontal=�60   $��Bottom=�6I   $��Right=�6d   $��0,0,10,0q�6�   .+�5]      �� -   5         ��Name$ 	btnConfig��5]      $ Open the config file��6D   $��0,0,10,0q�6c   .��6u   Config6}   �� 5^      -	   5        $ btnHelp��5^      $ Open the User Manual��6@   {�.  .��6r   Help6x   5_      5a      5c      5e      