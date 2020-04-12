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
	/// Логика взаимодействия для AddFileWindow.xaml
	/// </summary>
	public partial class AddFileWindow : Window
	{
		private readonly MainWindow window = Application.Current.MainWindow as MainWindow;

		public AddFileWindow()
		{
			InitializeComponent();
			if (window.foldersDataGrid.SelectedIndex != -1)
			{
				folderList.Items.Add("(выбранная)");
				folderList.SelectedIndex = 0;
			}

			foreach (var folder in context.folders.ToList().Select(f => f.name))
			{
				folderList.Items.Add(folder);
			}

		}

		private void AddButtonClick(object sender, RoutedEventArgs e)
		{
			string folderName = folderList.SelectedItem.ToString() == "(выбранная)"
				? ((folders) window.foldersDataGrid.SelectedItem).name
				: folderList.SelectedItem.ToString();
			if (textBoxName.Text == "" || textBoxExt.Text == "" || textBoxSize.Text == "" ||
			    textBoxName.Text.Length > 100 || textBoxExt.Text.Length > 10 ||
			    !int.TryParse(textBoxSize.Text, out var size))
			{
				new Alert("Неверно введены данные", "Не полностью введены данные. Файл добавлен не будет").Show();
				return;
			}

			var file = new files
				{name = textBoxName.Text, extension = textBoxExt.Text, sizeKb = int.Parse(textBoxSize.Text)};
			context.files.Add(file);
			var folder = context.folders.ToList().First(f => f.name == folderName);
			folder.files.Add(file);
			context.folders.Add(folder);
			context.SaveChanges();
			Close();
		}
	}
}