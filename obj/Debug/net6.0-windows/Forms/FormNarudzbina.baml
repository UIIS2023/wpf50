   M S B A M L   `   `   `  ����D  @PekaraWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PekaraWPF.FormsPekaraWPF.Forms  5   	   $    PekaraWPF.Forms.FormNarudzbina   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:PekaraWPF.Forms     5   	     � Title$%  Dodavanje i Izmena (NARUDZBINA)��5   	   $	��550��61   $	��300��6>   .��5	      � .+�5
   
   �� $��Ukupna Cena:��6   $
��Left=�6'   $��	20,30,0,0q�6B   $	��Top=�6U   �� 5   
   $��Vreme Narudzbine:��6   $
��Left=�6,   $��	20,90,0,0q�6G   $	��Top=�6Z   �� 5   
   $��Ime zaposlenog:��6   $
��Left=�6*   $��
20,150,0,0q�6E   $	��Top=�6Y   �� 5   
   $��
Ime kupca:��6   $
��Left=�6%   $��
20,210,0,0q�6@   $	��Top=�6T   �� 5   
   $��Naziv pica:��6   $
��Left=�6&   $��
20,270,0,0q�6A   $	��Top=�6U   �� 5   
   $��Naziv peciva:��6   $
��Left=�6(   $��
20,330,0,0q�6C   $	��Top=�6W   �� 5   
   $��Tip narudzbine:��6   $
��Left=�6*   $��
20,390,0,0q�6E   $	��Top=�6Y   �� 5   
   -   5         ��Name$ txtUkupnaCena��5      $
��Left=�6)   $��
130,35,0,0q�6D   $��23��6X    �� 
IsReadOnly$
 True��6d    �� TextWrapping$
 Wrap=�6v   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ txtVremeNarudzbine��5      $
��Left=�6.   $��
130,95,0,0q�6I   $��23��6]   $
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$ cbZaposleni��5      $
��Left=�6(   $��130,155,0,0q�6C   $	��Top=�6X   $	��120��6p    �� DisplayMemberPath$	 ime��6|    �� SelectedValuePath$ zaposleniID��6�   �� 5   
   -   5        $ cbKupac��5      $
��Left=�6$   $��130,215,0,0q�6?   $	��Top=�6T   $	��120��6l   $	 ime��6x   $ kupacID��6�   �� 5   
   -   5        $ cbPice��5      $
��Left=�6#   $��130,275,0,0q�6>   $	��Top=�6S   $	��120��6k   $	 ime��6w   $ piceID��6�   �� 5   
   -   5        $ cbPecivo��5      $
��Left=�6%   $��130,335,0,0q�6@   $	��Top=�6U   $	��120��6m   $	 ime��6y   $ pecivoID��6�   �� 5   
   -   5        $ cbTipNarudzbine��5      $
��Left=�6,   $��130,395,0,0q�6G   $	��Top=�6\   $	��120��6t   $ vrstaNarudzbine��6�   $ tipNarudzbineID��6�   �� 5   
   -   5         ��Name$ btnOK��5      $��OK��6    $
��Left=�6-   $��
32,450,0,0q�6H   $	��Top=�6\   $��75��6t   6   �� 5   
   -	   5        $ 	btnCancel��5      $��Cancel��6$   $
��Left=�65   $��177,450,0,0q�6P   $	��Top=�6e   $��75��6}   6�   5      5      