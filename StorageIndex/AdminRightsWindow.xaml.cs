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
	/// Логика взаимодействия для AdminRightsWindow.xaml
	/// </summary>
	public partial class AdminRightsWindow : Window
	{
		public AdminRightsWindow()
		{
			InitializeComponent();
		}

		private void AddButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (Users.FirstOrDefault(u => u.Login == textBoxName.Text) != null)
			{
				Users.First(u => u.Login == textBoxName.Text).Group = Type.Administrator;
				SaveSystem.SaveUsersToFile();
				Close();
			}
			else
			{
				new Alert("Пользователя не существует", "Пользователь с таким логином не найден").Show();
			}
		}
	}
}
