   M S B A M L   `   `   `  ����  FilmWebAdmin!    FilmWebAdmin.AddMovieWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Add New Movie��5   	   $	��310��6   $	��400��6,    � WindowStartupLocation$ CenterScreen=�68   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Title:��6   $��0��6!   �� 5   
   -   5         ��Name$ MovieTitleTextBox��5      $��1��6-   $��0,0,0,10q�6:   �� 5   
   $��Genre:��6   $��2��6!   �� 5   
   -   5         ��Name$ MovieGenreComboBox��5       �� DisplayMemberPath$ 
genre_name��6/   $��3��6N   $��0,0,0,10q�6[   �� 5   
   $��Release Year:��6   $��4��6(   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ MovieReleaseYearPicker��5      $��5��65   $��0,0,0,10q�6B   �� 5   
   $��
Image URL:��6   $��6��6%   �� 5   
   -   5        $ MovieImgUrlTextBox��5      $��7��6.   $��0,0,0,10q�6;   �� 5   
   $��8��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5      �� -   5        $
��Save��5      $��75��6=   $��0,0,10,0q�6H   �� 5      -   5        $��Cancel��5      $��75��6A   5      5       5!      