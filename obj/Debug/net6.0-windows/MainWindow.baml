   M S B A M L   `   `   `  ����D  @PekaraWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:PekaraWPF	PekaraWPF  5   	       PekaraWPF.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:PekaraWPF     5   	     � Title$  Pekara��5   	   $	��600��6   $	��800��6%   .��5	      � 
�������.+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGridCentralni��5
      $��Center=�6,   $��Center=�6I   $
��auto��6d   $
��auto��6r      
IsReadOnly$
 True��6      AlternatingRowBackground
 �����   CanUserAddRows$ False��5   ,      SelectionUnit$ FullRow=�6C      SelectionMode$ Single=�6[   �� 5   
   -   5         ��Name$ stackPanelTop��5      $��Center=�6,   $	��Top=�6I   $
��auto��6a    �� Orientation$ 
Horizontal=�6n   $��40��6�   .+�5      �� -   5         ��Name$ btnKupac��5      $��Kupci��6'   $	��100��67   6C   �� 5      -   5        $ 	btnPecivo��5      $��Peciva��6(   $	��100��69   6E   �� 5      -   5        $ btnPice��5      $
��Pica��6&   $	��100��65   6A   �� 5      -   5        $ btnZaposleni��5      $��	Zaposleni��6+   $	��100��6?   6K   �� 5      -   5        $ btnNarudzbina��5      $��
Narudzbine��6,   $	��100��6A   6M   5      �� 5   
   -   5        $ stackPanelBottom��5      $��Center=�6/   $��Bottom=�6L   $
��auto��6g   $ 
Horizontal=�6t   $��40��6�   .+�5      �� -	   5        $ btnDodaj��5      $��Dodaj��6'   $	��180��67   $��0,0,30,0q�6Z   �� 5      -
   5        $ 	btnIzmeni��5      $��Izmeni��6(   $	��180��69   $��0,0,30,0q�6]   �� 5      -   5        $ 	btnObrisi��5      $��Obriši��6(   $	��180��69   6E   5      5      5      