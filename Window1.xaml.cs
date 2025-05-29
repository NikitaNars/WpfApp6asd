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
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void SaveItem(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Owner;
            string title = titleToDo.Text;

            string description = descriptionToDo.Text;
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Введите название дела");
                return;
            }

            if (!dateToDo.SelectedDate.HasValue) 
            {
                dateToDo.SelectedDate = DateTime.Now;
            }


            mainWindow.TodoItems.Add(new ToDo(title, dateToDo.SelectedDate.Value, description));

            mainWindow.listToDo.Items.Refresh();

            titleToDo.Text = null;
            dateToDo.SelectedDate = DateTime.Now;
            descriptionToDo.Text = "Описания нет";

            mainWindow.listToDo.ItemsSource = null;
            mainWindow.listToDo.ItemsSource = mainWindow.TodoItems;
            this.Close();
            mainWindow.Update();
        }

        private void descriptionToDo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
