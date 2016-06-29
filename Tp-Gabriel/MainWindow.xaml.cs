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

namespace Tp_Gabriel
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Files files = new Files(System.Configuration.ConfigurationSettings.AppSettings["xmlFile"], System.Configuration.ConfigurationSettings.AppSettings["folder"]);
        List<Captor> listeCaptor;
        public MainWindow()
        {
            InitializeComponent();
            listeCaptor = generateData();
            // for generate the textBox values 
            generateTextBoxSalle();
            // for generate the date
            generateTextBoxDate();

            DataContext = new Graph();        
        }

        private void generateTextBoxSalle()
        {
            foreach (Captor capt in listeCaptor)
            {
                comboSalle.Items.Add(capt.id);
            }
        }

        private void generateTextBoxDate()
        {
            string[] list = files.getDateDirecotry();
            foreach (string file in list)
            {
                comboDate.Items.Add(file);
            }
        }

        private List<Captor> generateData()
        {
            
            return files.getXmlAndDt();
        }

        private void comboSalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder stringBuilder = new StringBuilder(comboDate.Text);
            DataContext = new Graph(listeCaptor, stringBuilder, comboSalle.Text);
        }  
    }
}
