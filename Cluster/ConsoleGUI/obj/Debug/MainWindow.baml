   M S B A M L   `   `   `  ����E  AConsoleGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ConsoleGUI
ConsoleGUI  5   	       ConsoleGUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ConsoleGUI     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      � -   5         �Name$ MainGrid��5	      .+�5
   
   �� -   5         ��Name$ conversationWindow��5
      $	��251��6.    �� TextWrapping$
 Wrap=�6;   $	��Top=�6O   $��	100,0,0,0q�6g    �� 
IsReadOnly . 6z   �� 5   
   -   5        $ inputTextbox��5      $��29��6(   $
 Wrap=�64   $	��Top=�6H   $��100,256,0,0q�6`   .��6�    ^� ToolTip 5      � 5       � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� -   5         ��Name$ toolTipKeys��5       $
��Bold"�65   �� 5      -   5        $ toolTipDesc��5   0   $��2��6    60   5      .��5   &   5      �� 5   
   -   5         ��Name$ sendMsgButton��5      $
��Send��6(   $
��Left=�6L   $	��Top=�6g   $��75��6   $��432,290,0,0q�6�   �� 5   
   -   5        $ loginButton��5      $��Log In��6&   $
��Left=�6Q   $	��Top=�6l   $��75��6�   $��	10,10,0,0q�6�   5      5      