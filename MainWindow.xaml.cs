using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
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
        public ObservableCollection<ToDo> TodoItems = new ObservableCollection<ToDo>();
        public static RoutedCommand AddNewTaskCommand { get; } = new RoutedCommand();
        public static RoutedCommand DeleteTasck { get; } = new RoutedCommand();
        public static RoutedCommand SaveTascks { get; } = new RoutedCommand();
        public MainWindow()
        {
            InitializeComponent();
            
            //TodoItems = new ObservableCollection<ToDo>
            //{
            //    new ToDo("Зdas", new DateTime(2024, 1, 15), "asd"),
            //    new ToDo("asd", new DateTime(2024, 1, 12), "Мdas"),
            //    new ToDo("sda", new DateTime(2024, 1, 20), "asd")
            //};
            listToDo.ItemsSource = TodoItems;

            
            Update();
            


            
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
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить задачу \"{selectedToDo.GetName}\"?",
            "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                TodoItems.Remove(listToDo.SelectedItem as ToDo);
                Update();

            }
            


            
            

        }
        private void CheckItem(object sender, RoutedEventArgs e)
        {
            var todo = (sender as CheckBox)?.DataContext as ToDo;
            todo.GetDoing = true;
            Update();
        }
        private void UncheckItem(object sender, RoutedEventArgs e)
        {
            var todo = (sender as CheckBox)?.DataContext as ToDo;
            
            todo.GetDoing = false;
            Update();
        }

        public void Update()
        {
            TextProgressBar.Text = $"{TodoItems.Count(t => t.GetDoing)}/ {TodoItems.Count}";
            ProgressBar.Maximum = TodoItems.Count;
            ProgressBar.Value = TodoItems.Count(t => t.GetDoing);
        }
        public void SaveItem(object sender, EventArgs e)
        {
            if (TodoItems.Count == 0)
            {
                MessageBox.Show(
                    "Список задач пуст! Нет данных для сохранения.",
                    "Ошибка сохранения",
                    MessageBoxButton.OK);
                return; 
            }

            // Создаем диалоговое окно для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Title = "Сохранить список дел";

            // Показываем диалоговое окно и проверяем, нажал ли пользователь "ОК"
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Формируем содержимое файла
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Список дел:");
                sb.AppendLine("--------------------------");

                foreach (var todo in TodoItems)
                {
                    if (todo.GetDoing == true)
                    {
                        sb.Append("✔");
                    }
                    
                    sb.AppendLine($"Название: {todo.GetName}");
                    sb.AppendLine($"Дата: {todo.GetDateImpl:dd.MM.yyyy}");
                    sb.AppendLine($"Описание: {todo.GetDescription}");
                    sb.AppendLine($"Выполнено: {(todo.GetDoing ? "Да" : "Нет")}");
                    sb.AppendLine("--------------------------");
                }

                // Записываем данные в файл
                try
                {
                    File.WriteAllText(filePath, sb.ToString());
                    MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
    
}