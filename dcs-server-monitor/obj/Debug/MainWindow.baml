   M S B A M L   `   `   `  ����M  Idcs-server-monitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:dcs_server_monitordcs_server_monitor  5   	   #    dcs_server_monitor.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:dcs_server_monitor     5   	   -   5          � Title$+  %DCS Server Monitor (Private use only)��5   	   $	��450��67   $	��800��6D    � 
ResizeMode$ NoResize=�6b   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $��0* �6    � ColumnDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ Button_RestartServer��5      $��(Re)start Server��6/   $
��Left=�6J   $��	10,10,0,0q�6e   $	��Top=�6x   $	��101��6�   $��20��6�   6�   �� 5   
   -   5        $ Button_KillServer��5      $��Kill Server��6,   $
��Left=�6B   $��	10,35,0,0q�6]   $	��Top=�6p   $	��101��6�   $��20��6�   6�   �� 5   
   -   5         ��Name$ CheckBox_KillSafety��5      $��SAFETY��60   $
��Left=�6A   $��	10,60,0,0q�6\   $	��Top=�6o    J� RenderTransformOrigin$ 0.507,0.467&�6�   $	��101��6�   $��15��6�   �� 5   
   -   5        $ Button_ShutDown��5      $��Shutdown��6*   $
��Left=�6=   $��
10,362,0,0q�6X   $	��Top=�6l   $	��101��6�   $��33��6�   6�   �� 5   
   ���	#FFF4F4F5$
��Left=�6%   $	��385��6@   $��
116,10,0,0q�6M   
���   �$	��Top=�6p   $	��648��6�   �� 5   
   -   5         ��Name$ InfoBox��5      $
��Left=�6#   $	��329��6>   $��
136,30,0,0q�6K   $	��Top=�6_   $	��608��6w   6�   �� 5   
   $��Refresh Rate (ms)��6   $
��Left=�6,   $��	10,80,0,0q�6G   $	��Top=�6Z   $	��101��6r   �� 5   
   -   5         ��Name$ TextBox_RefreshRate��5      $
��Left=�6/   $��23��6J   $��
10,106,0,0q�6V   	 �� TextWrapping$
	 Wrap=�6j   $
��1000��6~   $	��Top=�6�   $	��101��6�   6�   � 5   
   -   5        
 �Name$
 ProgressBar_Restart��5      $
��Left=�63   $��19��6N   $��241,364,0,0q�6Z   $	��Top=�6o   $	��415��6�    � Maximum$ 1Q�6�    � LargeChange$
 0.01Q�6�   �� 5   
   $��Time Until Restart��6   $
��Left=�6-   $��131,360,0,0q�6H   $	��Top=�6]   $	��105��6u   $��27��6�   �� 5   
   -	   5        $ TextBox_TimeRemaining��5      $
��Left=�61   $��20��6L   $��666,364,0,0q�6X   $
	 Wrap=�6m   $��TextBox��6�   $	��Top=�6�   $��78��6�    �� TextAlignment$ Center=�6�    �� 
IsReadOnly . 6�   5      5      