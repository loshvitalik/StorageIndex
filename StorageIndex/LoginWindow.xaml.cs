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
	/// Логика взаимодействия для LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void RegisterUser(object sender, RoutedEventArgs e)
		{
			if (loginBox.Text.Contains(",") || loginBox.Text.Contains(":") || loginBox.Text.Contains("\"") ||
			    loginBox.Text.Contains("{") || loginBox.Text.Contains("}") || loginBox.Text.Contains("[") ||
			    loginBox.Text.Contains("]"))
			{
				new Alert("Неверный логин",
					"Логин не может содержать символы\n\",\", \":\", \"{\", \"}\", \"[\", \"]\" и \"").Show();
			}
			else if (Users.Any(u => loginBox.Text == u.Login))
			{
				new Alert("Неверный логин",
					"Такой пользователь уже\nзарегистрирован").Show();
			}
			else
			{
				var group = Type.User;
				SessionManager.RegisterUser(group, loginBox.Text, passBox.Password, dbLoginBox.Text, dbPassBox.Password);
			}
		}

		private void LoginKeyPress(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
				SessionManager.LogIn(loginBox.Text, passBox.Password);
		}

		private void PassEnterKeyPress(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
				SessionManager.LogIn(loginBox.Text, passBox.Password);
		}

		private void LoginUserButtonClick(object sender, RoutedEventArgs e)
		{
			SessionManager.LogIn(loginBox.Text, passBox.Password);
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
