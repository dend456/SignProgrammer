   M S B A M L   `   `   `  ����I  ESignProgrammer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SignProgrammerSignProgrammer  5   	   C&clr-namespace:SignProgrammer.ViewModelSignProgrammer.ViewModel  5   	   $    SignProgrammer.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:SignProgrammer     5   	   5vm&clr-namespace:SignProgrammer.ViewModel     5   	     � Title$  Signer��5	   	   $	��350��6   $	��525��6%    � DataContext 5
      +   %SignProgrammer.ViewModel.MainWindowVM  5   
    � 	Resources 5         EffectItemStyleKey �� 
TargetType �� Property �� Value �� Path �� RelativeSource �� Mode	 �� AncestorType
 �� AncestorLevel��%  &	        �� 5   
   " m�6+   .�5      ��  � �� 6'   �� $ MenuText��� 5       �@m�Command 5      �� *DataContext.EffectCommand+ �� $ FindAncestor=�"	 m�$
 1���� 5       �@m�CommandParameter 61   �� 5      5      .��5      � .+�5   
   o� $	��Top=�6    o� 
IsMainMenu . .@�5      m� -   5        $��fontMenu��5   �   #��[  6   $��F_ont��6�   ��6P   �� $ CurrentSign.Effects[Font]�6�   m� 5      -   5        $��transitionMenu��5   �   #��[  6   $��_Transition��6�   ��6P   �� $% CurrentSign.Effects[Transition]�6�   m� 5      -   5        $��	colorMenu��5   �   #��[  6   $��_Color��6�   ��6P   �� $  CurrentSign.Effects[Color]�6�   m� 5      -   5        $��graphicMenu��5   c   $��_Graphic��6Q   ��6   �� $" CurrentSign.Effects[Graphic]� 5        iconKey��%g   &	      �� 5      $��	-10,0,0,0q�6=   $��36��6P   $��14��6[   ��6g   �� $ 	Thumbnail�5      ��5      �� 5       " m�6   .�5!      ��  �@m�Icon# [ 61   �� 5"       � �� 63   �� $ MenuText��� 5#       �@m�Command 5$      �� *DataContext.EffectCommand+ �� $ FindAncestor=�"	 m�$
 1���� 5&       �@m�CommandParameter 6=   �� 5'      5)      m� 5*      -   5        $��specialMenu��5*   �   #��[  6   $��_Special��6�   ��6P   �� $" CurrentSign.Effects[Special]�6�   5+      �� 5,   
   -   5         ��Name$ label��5,      $��Speed��6   $��
0,71,110,0q�6/   $	��Top=�6C   $��Right=�6[   $��43��6w   �� 5-   
   -   5         ��Name$ speedSlider��5-      $��	0,74,10,0q�6J   $	��Top=�6]    � Maximum$ 25Q�6u    � SmallChange$ 5Q�6�   $��Right=�6�   $��95��6�    �� TickPlacement$ BottomRight=�6�    �� TickFrequency$ 5Q�6�    J� IsHitTestVisible .  �� IsSnapToTickEnabled .  � LargeChange$ 5Q�6   � Value 6&   �� $ CurrentSpeed�6  �� 5.   
   -   5         ��Name$ 
sendButton��5.      $
��Send��6L   $��	0,0,10,10q�6[   $��22��6n   $��Bottom=�6z   $��Right=�6�   $��75��6�   ��6%   �� *SendMessageCommand+6�   �� 5/   
   -	   5         ��Name$ 
messageBox��5/      $��10,100,10,37q�6m    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .  �� 
AcceptsTab .  �� 	IsEnabled . ��6&   �� $ MessageText� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 50   
   -
   5        $ label1��50      $��Page:��6    $
��Left=�60   $��	10,70,0,0q�6K   $	��Top=�6^   $��43��6v   �� 51   
    �� SelectedValuePath$ Content��6?   $
��Left=�6[   $��	53,73,0,0q�6v   $	��Top=�6�   $��36��6�    �� SelectedIndex$ 0��6�    �� SelectedValue 6   �� $ SelectedPage�.@�52      �� .��6   A6   �� 53      .��6   B6   �� 54      .��6   C6   �� 55      .��6   D6   �� 56      .��6   E6   �� 57      .��6   F6   �� 58      .��6   G6   �� 59      .��6   H6   �� 5:      .��6   I6   �� 5;      .��6   J6   �� 5<      .��6   K6   �� 5=      .��6   L6   �� 5>      .��6   M6   �� 5?      .��6   N6   �� 5@      .��6   O6   �� 5A      .��6   P6   �� 5B      .��6   Q6   �� 5C      .��6   R6   �� 5D      .��6   S6   �� 5E      .��6   T6   �� 5F      .��6   U6   �� 5G      .��6   V6   �� 5H      .��6   W6   �� 5I      .��6   X6   �� 5J      .��6   Y6   �� 5K      .��6   Z6   5L      `� 5M   
   $��32��6   $��0,21,0,0q�6"   $	��Top=�64   .�5N      c� $��0,0,-517,-32q�6   .@�5O      �� $��G+��6   ��6&   �� *NewGraphicCommand+5P      5Q      �� 5R   
   $
��Left=�6g   $��
180,73,0,0q�6�   $	��Top=�6�   $��72��6�   ��6   �� *OpenComPorts+ 68   �� $ SelectedComPort�6�   �� 5S   
   -   5        $ label2��5S      $��	COM Port:��6    $
��Left=�64   $��
110,70,0,0q�6O   $	��Top=�6c   �� 5T   
   -   5        $ button��5T      $	��⟳��6!   $
��Left=�6-   $��
257,73,0,0q�6H   $	��Top=�6\   $��25��6t     J� RenderTransformOrigin$  0.33,-0.022&�6   $��16(�6�   $��22��6�   $��Arial)�6�   5U      5V      