using CALCULATION.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CALCULATION.View
{
    //public class DecimalTextBox : TextBox
    //{
    //    public DecimalTextBox()
    //    {
    //        this.PreviewTextInput += IntTextBlock_PreviewTextInput;

    //        this.TextChanged += IntTextBox_TextChanged;
    //    }

    //    private void IntTextBox_TextChanged(object sender, TextChangedEventArgs e)
    //    {
    //        if (sender is TextBox tb) tb.Select(tb.Text.Length, 0);
    //    }

    //    private void IntTextBlock_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    //    {
    //        if (Char.IsDigit(e.Text, 0)
    //            || (this.Text.Length == 0 && e.Text == "-")
    //            || (e.Text == "." && this.Text.IndexOf(".") == -1))
    //        {
    //            e.Handled = false;
    //            return;
    //        }

    //        e.Handled = true;
    //    }
    //}

    public class DecimalTextBox : TextBox
    {
        public DecimalTextBox()
        {
            PreviewTextInput += OnPreviewTextInput;
            PreviewKeyDown += OnPreviewKeyDown;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем допустимость символа
            if (!IsValidDecimalSymbol(e.Text))
            {
                e.Handled = true;
                return;
            }

            // Передаём ввод во ViewModel
            if (DataContext is MainWindowVM vm)
            {
                vm.KeyBoardInput(e.Text);
                e.Handled = true; // ОЧЕНЬ ВАЖНО
            }
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back && DataContext is MainWindowVM vm)
            {
                vm.RemoveLast();
                e.Handled = true;
            }
        }

        private bool IsValidDecimalSymbol(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            char c = text[0];

            if (char.IsDigit(c))
                return true;

            if (c == '-' && Text.Length == 0)
                return true;

            if (c == '.' && !Text.Contains('.'))
                return true;

            return false;
        }
    }

}