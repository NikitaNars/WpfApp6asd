using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ToDo> TodoItems;

        public MainWindow()
        {
            InitializeComponent();
            TodoItems = new ObservableCollection<ToDo>
            {
                new ToDo("Зdas", new DateTime(2024, 1, 15), "asd", false),
                new ToDo("asd", new DateTime(2024, 1, 12), "Мdas", false),
                new ToDo("sda", new DateTime(2024, 1, 20), "asd", false)
            };



            listToDo.ItemsSource = TodoItems;


            
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Owner = this;
            window1.Show();

        }
        
        private void KillItem(object sender, RoutedEventArgs e)
        {
            ToDo selectedToDo = listToDo.SelectedItem as ToDo;
            if (selectedToDo == null)
            {
                MessageBox.Show("Выберите дело для удаления");
                return;
            }

            TodoItems.Remove(selectedToDo);

            
        }
        private void Update()
        {
            TextProgressBar.Text = $"{TodoItems.Count}";
        }
    }
    
}