   M S B A M L   `   `   `  ����F  BDesktopGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DesktopGameDesktopGame  5   	   G(clr-namespace:DesktopGame.MVVM.ViewModelDesktopGame.MVVM.ViewModel  5   	       DesktopGame.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DesktopGame     5   	   >	viewModel(clr-namespace:DesktopGame.MVVM.ViewModel     5   	     � Title$  
MainWindow��5	   	   $	��900��5
   	   $
��1200��5   	    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ #44F5E9��6   � 5      $ White��6    � Offset$ 1Q�6)   5       � DataContext 5      .  (DesktopGame.MVVM.ViewModel.MainViewModel  5   
   .��5      � 	 � ColumnDefinitions	 5   
   �� 5      $	��15* �6   �� 5      $	��80* �6   
 � RowDefinitions
 5    
   �� 5!      $	��10* �6   �� 5"      $	��90* �6   .+�5%   
   >� $��1��5&      $��1��5'      ��5%      �� *	CurrentVM+5'      �� 5)   
   $��1��6   $��0��5*      5,      � 5.   
   $��0��6   $��1��5/      	 50      �� 51      $��5* �6#   53      �� 54      $��5* �6#   .+�57      �� $��Right=�6   $��1��58       �� Orientation$ 
Horizontal=�59      .+�5;      �� $	��|_|��6      
AppControl#��[  5<      ��5=      �� *MaxApp.Command+�� 5?      $��_��6   #��[  5@      ��5A      �� *HideApp.Command+�� 5C      $��X��6   #��[  5E      ��5D      �� *CloseApp.Command+5E      5F      5G      5H      5I      