   M S B A M L   `   `   `  ����I  ETCPChat.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TCPChat.ClientTCPChat.Client  5   	       TCPChat.Client.MainWindow   5   	   Z VDevExpress.Mvvm.UI, Version=21.1.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:TCPChat.Client     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � DataContext 5	      "   TCPChat.Client.MainViewModel  5
   
    � 	Resources 5       �� 
TargetType �� Property �� Value1   +TCPChat.Client.BooleanToVisibilityConverter    BooleanToVisibilityConverter*   $TCPChat.Client.ColorToBrushConverter  ColorToBrushConverter/   )TCPChat.Client.BooleanToVisibilityForText  BooleanToVisibilityForText)   #TCPChat.Client.PathToImageConverter  PathToImageConverter��%�  '��       '�� }     '�� �     &	  A    &	 T    &	 g    &	 z    �� 5   
   " ��6   .�5      ��  � ��$ 3q�6'   �� 5       �@��TextAlignment$ Center=�6.   5      �� 5   
   " ��6   .�5      ��  � ��$ 3q�6'   �� 5       �@��TextAlignment$ Center=�6.   5      �� 5   
   " ��6   .�5      ��  � ��$ 3q�6'   5        5   
   6-     5   
   6&     5   
   6+     5   
   6%   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5       �� 5!      $
��Auto �6   .+�5#   
   �� .+�5$      A�  A� Columns$ 6��6   .+�5%      �� $��IP��6   �� 5&      $
��Port��6   �� 5'      $��Login��6   �� 5(      $��Password��6   5)      A� 5*      $ 6��6   .+�5+      �� ��6   �� *IP+�� 5,      ��6   �� *Port+�� 5-      ��6   �� *Nick+�� 5.      ��6   �� *
Password+�� 5/      $��Регистрация��6=   ��6   �� *RegisterCommand+6=   �� 50      $��Подключиться��6<   ��6   �� *ConnectCommand+6<   51      52      �� 54   
   -   5        $��chatListBox��54      $
��Auto=�6%   $��1��6U    �� SelectionMode$ Single=�6�   ��6b   �� *ChatMessagesListBox+��55      �� 56      .R�57      �� 	 �� Orientation$	 
Horizontal=�6!   $��0,0,0,-2q�6:   .+�58      �� ��6$   �� 
 �� Path$
 Username���6C   �� $
 UsernameColor� �� 	Converter# [  J� InputBindings 59      d� 5:   "    d� Gesture$ LeftDoubleClicka�6/    �� Command 5;   '   �� *DataContext.OnLoginClicked+ �� RelativeSource ��  �� AncestorType" �� �� CommandParameter 5<   '   �� 5>      �� 5?      $��10,0,0,0q�6B   $
��Wrap=�6T   $	��600��6h   ��6$   �� $
 Message�y�6t   �� $
 IsImage�# [ �� 5@      $	��200��6�   $��10,0,0,0q�6�   ��6    �� $
 
imageBytes�# [ y�6u   �� $
 IsImage�# [  6�   5B      5C      5E      � 5G   
   $��2��6    � ColumnDefinitions 5H      �� 5I      $��4* �6#   �� 5J      $��* �6#   �� 5K      $��* �6#   .+�5M      ��  �� MaxLines$ 4��6   $��0��6#    �� TextAlignment$
 Left=�5O       �� TextWrapping$
 Wrap=�5P      ��5N      �� *MessageField+5P      �� 5Q      $��1��5R      $#��Прикрепить фото��5T      ��5S      �� *AttachPhoto+5T      �� 5U      $��2��5V      $��Отправить��5X      ��5W      �� *SendCommand+5X      5Y      5Z      5[      