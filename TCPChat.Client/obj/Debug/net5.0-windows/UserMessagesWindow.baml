   M S B A M L   `   `   `  ����I  ETCPChat.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TCPChat.ClientTCPChat.Client  5   	   '    !TCPChat.Client.UserMessagesWindow   5   	   Z VDevExpress.Mvvm.UI, Version=21.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:TCPChat.Client     5   	     � Title$  UserMessagesWindow��5   	   $	��400��6$   $	��250��61    � 
ResizeMode$ NoResize=�5	   	    � 	Resources 5      1   +TCPChat.Client.BooleanToVisibilityConverter    BooleanToVisibilityConverter*   $TCPChat.Client.ColorToBrushConverter  ColorToBrushConverter/   )TCPChat.Client.BooleanToVisibilityForText  BooleanToVisibilityForText)   #TCPChat.Client.PathToImageConverter  PathToImageConverter��%t   &	        &	      &	 &     &	 9       5   
   6-     5   
   6&     5   
   6+     5   
   6%   5      .��5   
   �  � RowDefinitions 5   
   �� 5      $��1* �6   6*   �� 5      $	��10* �6   6+   .+�5   
   �� -   5        $��UserLoginLabel��5   R   $��0��6   $
��TEST��6!   $��20(�6-    �� TextAlignment$ Center=�6;   6j   �� 5   
   $��1��6   ��6   �� *MessageList+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   $��0,0,0,10q�6:   .+�5      �� $��10,0,0,0q�6?   $
��Wrap=�6Q   $	��180��6e   ��6$   ��  �� Path$
 Text�y�6q   �� $ IsImage� �� 	Converter# [ �� 5      $	��180��6�   $��10,0,0,0q�6�   ��6    �� $ 	ByteArray�# [ y�6t   �� $ IsImage�# [  6�   5      5      5       5!      5"      