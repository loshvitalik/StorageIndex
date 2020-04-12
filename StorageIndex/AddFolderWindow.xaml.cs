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
using static StorageIndex.MainWindow;

namespace StorageIndex
{
	/// <summary>
	/// Логика взаимодействия для AddFolderWindow.xaml
	/// </summary>
	public partial class AddFolderWindow : Window
	{
		private readonly MainWindow window = Application.Current.MainWindow as MainWindow;
		public AddFolderWindow()
		{
			InitializeComponent();
			if (window.storageDataGrid.SelectedIndex != -1)
			{
				deviceList.Items.Add("(выбранное)");
				deviceList.SelectedIndex = 0;
			}

			foreach (var device in context.storage.ToList().Select(s => s.name))
			{
				deviceList.Items.Add(device);
			}
		}

		private void AddButtonClick(object sender, RoutedEventArgs e)
		{
			string deviceName = deviceList.SelectedItem.ToString() == "(выбранное)"
				? ((storage)window.storageDataGrid.SelectedItem).name
				: deviceList.SelectedItem.ToString();
			if (textBoxName.Text == "" || textBoxName.Text.Length > 100)
			{
				new Alert("Неверно введены данные", "Не полностью введены данные. Папка добавлена не будет").Show();
				return;
			}

			var folder = new folders { name = textBoxName.Text };
			context.folders.Add(folder);
			var device = context.storage.ToList().First(f => f.name == deviceName);
			device.folders.Add(folder);
			context.storage.Add(device);
			context.SaveChanges();
			Close();
		}
	}
}
