using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.ShowDialog();
            //this.Close();

            b_personCounter.Content = App.personCounter;

        }
    }

    public partial class Window1 : Window
    {
        class TextBoxEx : TextBox
        {
            public string ActualText
            {
                get { return (string)GetValue(ActualTextProperty); }
                set { SetValue(ActualTextProperty, value); }
            }

            // Using a DependencyProperty as the backing store for ActualText. This enables animation, styling, binding, etc...
            public static readonly DependencyProperty ActualTextProperty =
            DependencyProperty.Register("ActualText", typeof(string), typeof(TextBoxEx), new PropertyMetadata(string.Empty, OnActualTextChanged));

            private static void OnActualTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                TextBox tx = d as TextBox;
                tx.Text = (string)e.NewValue;
                string str = tx.Text;
                double dbl = Convert.ToDouble(str);
                str = string.Format("{0:0.###}", dbl);
                tx.Text = str;
            }

            public TextBoxEx()
            {
                this.GotFocus += TextBoxEx_GotFocus;
                this.LostFocus += TextBoxEx_LostFocus;
                this.PreviewTextInput += TextBoxEx_PreviewTextInput;
            }

            void TextBoxEx_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
            {
#pragma warning disable IDE0018 // Inlinevariablendeklaration
                decimal d;
#pragma warning restore IDE0018 // Inlinevariablendeklaration
                if (!decimal.TryParse(e.Text, out d))
                {
                    e.Handled = true;
                }
            }

            void TextBoxEx_LostFocus(object sender, System.Windows.RoutedEventArgs e)
            {
                ConvertText();
            }

            void TextBoxEx_GotFocus(object sender, System.Windows.RoutedEventArgs e)
            {
                this.Text = ActualText;
            }

            private void ConvertText()
            {
                string str = this.Text;
                ActualText = str;
                double dbl = Convert.ToDouble(str);
                str = string.Format("{0:0.###}", dbl);
                this.Text = str;
            }
        }
    }
    }

