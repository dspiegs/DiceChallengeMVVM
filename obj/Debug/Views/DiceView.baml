   M S B A M L   `   `   `  ����L  HDiceChallengeMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DiceChallengeMVVM.ViewModelsDiceChallengeMVVM.ViewModels  5   	   "    DiceChallengeMVVM.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   A
viewModels*clr-namespace:DiceChallengeMVVM.ViewModels     5   	     � Title$  Dice Challenge��5   	   $	��400��6    $	��475��6-    � DataContext 5      ,   &DiceChallengeMVVM.ViewModels.ViewModel  5   
   .��5	      �� .+�5
   
   �� {�.  $��Disabled=�5      ��5
      �� *
RolledDice+��5      �� 5      .R�5      �  K� IsItemsHost . 6    5      ��5      �� 5      .R�5      �� 
���   �$��1q�61   ._�5      �� $��25��6?   $��25��6J   ��6    �� *BitmapImage+6J   5      5      5      �� 5   
   $��10��6   �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5      �� $��Bet Amount: ��6   �� 5      $��50��61   ��6   �� *	BetAmount+61   5      �� 5   
   $��10��6   �� 5   
   $ 
Horizontal=�6   .+�5       ��  �� ContentStringFormat$ Current Bank: {0:$0.00}��6   
���  ����6B   �� *Bank+6[   5"      �� 5#   
   $��10��6   �� 5$   
   $��0q�60   {�.  ��6   �� *Rules+��5%      �� 5&      .R�5'      �� ��6   �� 5(      5*      �� 5+   
   $��10��6   �� 5,   
   $ 
Horizontal=�6   .+�5-      �� $��	Roll Dice��6   ��6)   �� *RollDiceCommand+�� 5.      $��20��6   �� 5/      $��New Game��6   ��6(   �� *NewGameCommand+50      �� 51   
   $��10��6   �� 52   
   
���  ����6   �� *ErrorMessage+61   53      54      