   M S B A M L   `   `   `  ����I  ESignProgrammer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SignProgrammerSignProgrammer  5   	   9!clr-namespace:SignProgrammer.ViewSignProgrammer.View  5   	   C&clr-namespace:SignProgrammer.ViewModelSignProgrammer.ViewModel  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35ZAclr-namespace:System.Windows.Media;assembly=PresentationFrameworkSystem.Windows.Media 5	   	   '    !SignProgrammer.View.GraphicEditor   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:SignProgrammer     5   	   6controls!clr-namespace:SignProgrammer.View     5   	   5vm&clr-namespace:SignProgrammer.ViewModel     5   	   SmediaAclr-namespace:System.Windows.Media;assembly=PresentationFramework     5	   	     � Title$  Graphics Editor��5   	   $	��300��6!   $��850         ��6.    � DataContext 5      .   (SignProgrammer.ViewModel.GraphicEditorVM  5   
    � 	Resources 5         pixelTemplate �� RelativeSource �� Mode �� AncestorType �� Color��%&  &	        �� 5   
   .R�5      �� ��6   �� *DataContext.PixelCommand+ �� $ FindAncestor=�" ���5      �� ��5      �� 5      .R�5      �� $��.5,.5,.5,.5q�6$   ��5      �� 5   "    62   �� *
PixelColor+5      5      5      5      5       .��5!      � .+�5"   
   ��  �� SelectionMode$ Single=�6z   $	��125��6�   $
��Left=�6�   $��Stretch=�6�   ��6   ��  �� Path$" CurrentSign.Effects[Graphic]�	 �� SelectedValue	 6L   �� $ CurrentGraphic� 5#        panelKey
 � ColumnDefinitions��%   &	      � 5$      
 5%      �� 5&      $
��Auto �6+   �� 5'      $��* �6+   .+�5)      �� $��0��6   $��0,0,0,0q�6,   $��54��6=   $��21��6H   ��6T   �� $ 	Thumbnail��� 5*      $��1��6   $��Right=�6N   ��6,   �� $ FileName�6N   5+      5,      ��5-      �� 5.       �� 
TargetType" ��6   .�5/      ��  �� Property � �� �� Value# [ 60   50      52      �� 53   
    �� SelectedValuePath$ Value��6m   $��	125,0,0,0q�6�   $��40��6�   $��Bottom=�6�   ��6   �� *CurrentGraphic.ValidColors+	 6E   �� *SelectedColor+ 54        rectKey��%r   &	      �� 55      $��32��6=   $��32��6L   ��56      �� 57       6*   �� $ Value�59      5:      ��5;      �� 5<      .R�5=      ��  �� Orientation$ 
Horizontal=�6!    K� IsItemsHost . 6:   5>      ��5@      �� 5A      " ��6   .�5B      ��  � ��# [ 60   �� 5C       � ��
 �   �64   �� 5D       � ��$ 0,0,0,0q�60   �� 5E       � ��$ 2,2,2,2q�6/    �� Triggers 5F      P� 5G       P� Property �@��
IsSelected P� Value . . �5H      ��  � ��$ 1q�6@   5I      5K      5M      �� 5N   
   $��125,0,117,40q�6M   ��6   �� $ SelectedGraphicPixelData���5O      �� 5P      .R�5Q      A�  A� Columns$ 1��6"   5R      ��5T      �� 5U      .R�5V      �� #��[  6;   ��6#   �� ��5W      �� 5X      .R�5Y   "   A�  A� Rows$ 1��6.   5Z      5\      5]      5_      �� 5`   
   $	��New��6   $��	0,10,10,0q�6>   $	��Top=�6Q   $��Right=�6i   $��75��6�   ��6   �� *
NewCommand+6�   �� 5a   
   $
��Save��6   $��	0,37,10,0q�6@   $	��Top=�6S   $��Right=�6k   $��75��6�   ��6    �� *SaveCommand+6�   �� 5b   
   $��Delete��6   $��Right=�6D   $��	0,0,10,45q�6`   $��75��6s   $��22��6~   $��Bottom=�6�   ��6"   �� *DeleteCommand+6�   �� 5c   
   -   5         ��Name$ textBox��5c      $��Right=�6#   $��23��6?   $��	0,90,10,0q�6K    �� TextWrapping$
 Wrap=�6^   $	��Top=�6�   $��75��6�   ��6r   �� $ CurrentGraphic.FileName�6�   �� 5d   
   -   5         ��Name$ label��5d      $��Name:��6   $
��Left=�6/   $��
752,64,0,0q�6J   $	��Top=�6^   5e      5f      