   M S B A M L   `   `   `  ����D  @FGOscript, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:FGOscript	FGOscript  5   	       FGOscript.InitWindows   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:FGOscript     5   	   Jlocal19http://schemas.microsoft.com/netfx/2009/xaml/presentation    5   	   -   5          � Title$  InitWindows��5	   	   $	��450��6   $	��800��6*    � WindowStyle$
 None=�5
   	    � Topmost .  � AllowsTransparency . ���	#10ffffff � WindowState$ 	Maximized=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $	��37* �6   �� 5      $
��330* �6   �� 5      $	��52* �6   .+�5   
   �� .+�5      �� 选择监视的窗体6!   �� 5      -   5        $��
OutputText��5   +   
�������6?   �� 5      $��1��6   ���	#FFB08F8F���	#FFC75E5E6D   5      �� 5   
   -   5        $��PreviewImage��5   -   $��1��6A   6P   �� 5   
    �� Orientation$ 
Horizontal=�6   $��2��6.   .+�5      �� -   5        $��SelectButton��5      .��6D   重置6H   �� 5      -   5        $��CloseWindowsButton��5      .��6P   关闭6T   5      5       5!      