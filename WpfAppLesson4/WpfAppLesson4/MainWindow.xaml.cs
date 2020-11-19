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
using System.Windows.Threading;
namespace WpfAppLesson4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    static class Utilite
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> num)
        {
            int n = num.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = num[k];
                num[k] = num[n];
                num[n] = value;
            }
        }
    }  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int countCorect = 0;
        int countInCorect = 0; 
        List<int> num = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        DispatcherTimer timer = new DispatcherTimer(); 
        int counter = 1;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            if(int.Parse((sender as Button).Content.ToString()) == counter)
            {
                counter++;
                (sender as Button).Background = Brushes.Green;
                progres.Value++;
                countCorect++;
            }
            else
            {
                (sender as Button).Background = Brushes.Red;
                countInCorect++;
            }
        }
        int sliderValue = 0;
        int countSec = 0;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            lbTime.Content = Convert.ToString(countSec++);
            CommandManager.InvalidateRequerySuggested();
            int c = Convert.ToInt32(lbTime.Content);
            if (c >= sliderValue)
            {
                timer.Stop();
                MessageBox.Show("Time end");
                
            }
            else if (countCorect==myUnifomGrid.Children.Count)
            {
                MessageBox.Show($"You win\nYour time:{countSec}\tErrors:{countInCorect}");
                timer.Stop();
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             sliderValue=(int)sliderTime.Value;
            int i = 0;
            num.Shuffle();
            foreach (Button item in myUnifomGrid.Children.OfType<Button>())
            {
                item.Content = num[i++];
            }
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            

        }
    }
}
