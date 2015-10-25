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

namespace SignProgrammer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sign sign = new Sign();
        bool firstChange = true;

        public MainWindow()
        {
            InitializeComponent();
            //Sign.SendMessage("<ID01><PA>a\r\n<ID01><RPA>\r\n\r");
            LoadEffects();
            Graphic g = Graphic.LoadFromFile("..\\..\\..\\graphics\\r.txt");
        }

        private void LoadEffects()
        {
            foreach (var e in Sign.Effects)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Header = e.MenuText;
                menuItem.Tag = e;
                menuItem.Click += effectButton_Clicked;
                switch (e.Type)
                {
                    case SignEffect.SignEffectType.Color:
                        colorMenu.Items.Add(menuItem);
                        break;
                    case SignEffect.SignEffectType.Graphic:
                        graphicMenu.Items.Add(menuItem);
                        break;
                    case SignEffect.SignEffectType.Transition:
                        transitionMenu.Items.Add(menuItem);
                        break;
                    case SignEffect.SignEffectType.Special:
                        specialMenu.Items.Add(menuItem);
                        break;
                    case SignEffect.SignEffectType.Font:
                        fontMenu.Items.Add(menuItem);
                        break;
                    default:
                        break;
                }
            }
        }

        private void effectButton_Clicked(object sender, EventArgs e)
        {
            SignEffect eff = (sender as MenuItem).Tag as SignEffect;
            int pos = textBox.CaretIndex;
            string text = textBox.Text;
            textBox.Text = text.Insert(pos, eff.Text);
            textBox.CaretIndex = pos + eff.Text.Length;
        }

        private void speedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!firstChange)
            {
                sign.SetSpeed((int)speedSlider.Value);
            }
            firstChange = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                sign.SendMessage(textBox.Text);
            }
        }
        
    }
}
