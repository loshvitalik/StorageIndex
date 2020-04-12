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
	/// Логика взаимодействия для AddStorageWindow.xaml
	/// </summary>
	public partial class AddStorageWindow : Window
	{
		public AddStorageWindow()
		{
			InitializeComponent();
			foreach (var type in context.mediaTypes.ToList().Select(t => t.name))
			{
				typeList.Items.Add(type);
			}

			typeList.Items.Add("(новый...)");

		}

		private void AddButtonClick(object sender, RoutedEventArgs e)
		{

			if (textBoxName.Text == "" || textBoxSize.Text == "" ||
			    textBoxName.Text.Length > 100 || 
			    !int.TryParse(textBoxSize.Text, out _))
			{
				new Alert("Неверно введены данные", "Не полностью введены данные. Устройство добавлено не будет").Show();
				return;
			}

			var device = new storage { name = textBoxName.Text, capacityMb = int.Parse(textBoxSize.Text), type = context.mediaTypes.ToList().First(t => t.name == typeList.SelectedItem.ToString()).id};
			context.storage.Add(device);
			context.SaveChanges();
			Close();
		}

		private void TypeList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (typeList.SelectedIndex == -1) return;
			if (typeList.SelectedItem.ToString() == "(новый...)")
			{
				new AddTypeWindow().ShowDialog();
				typeList.Items.Clear();
				foreach (var type in context.mediaTypes.ToList().Select(t => t.name))
				{
					typeList.Items.Add(type);
				}

				typeList.Items.Add("(новый...)");
			}
		}
	}
}
