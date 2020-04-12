using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static StorageIndex.MainWindow;

namespace StorageIndex
{
	internal class SessionManager
	{
		private static readonly MainWindow Window = Application.Current.MainWindow as MainWindow;

		public static void RegisterUser(Type group, string login, string password, string dbLogin, string dbPassword)
		{
			string encryptedPass = EncryptString(password);
			Users.Add(new User(group, login, encryptedPass, dbLogin, dbPassword));
			SaveSystem.SaveUsersToFile();
			LogIn(login, password);
		}

		public static void LogIn(string login, string password)
		{
			var encryptedPass = EncryptString(password);
			foreach (var u in Users.Where(u => u.Login == login))
				if (encryptedPass == u.Password)
				{
					CurrentUser = u;
					SetCommonRights();
					if (CurrentUser.Group == Type.Administrator)
						SetAdminRights();
				}

			if (CurrentUser == null)
				new Alert("Неверный пароль",
					"Неверный логин или пароль.\nПроверьте правильность\nвведённых данных").Show();
			else
				foreach (Window w in Application.Current.Windows)
					if (w is LoginWindow)
						w.Close();
		}

		public static void LogOut()
		{
			CurrentUser = null;
			Window.loginButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 59, 48));
			Window.Title = "Storage Index";
			Window.loginButton.Content = "Войти / зарегистрироваться";
			Window.saveButton.Visibility = Visibility.Hidden;
			Window.reportType.Visibility = Visibility.Hidden;
			Window.reportButton.Visibility = Visibility.Hidden;
			Window.addDeviceButton.Visibility = Visibility.Hidden;
			Window.addFolderButton.Visibility = Visibility.Hidden;
			Window.addFileButton.Visibility = Visibility.Hidden;
			Window.setAdminRights.Visibility = Visibility.Hidden;
		}

		private static void SetCommonRights()
		{
			Window.loginButton.Content = CurrentUser.Login + " [Выйти]";
			Window.loginButton.Foreground = new SolidColorBrush(Color.FromRgb(0, 122, 255));
			Window.reportType.Visibility = Visibility.Visible;
			Window.reportButton.Visibility = Visibility.Visible;

			//context = new db_dataContext(
			//	@"metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=""data source=" +
			//	ServerName +
			//	@";initial catalog=db_data;user id=" + CurrentUser.DbLogin + ";password=" + CurrentUser.DbPassword + ";MultipleActiveResultSets=True;App=EntityFramework");
			//Window.Window_Loaded("", new RoutedEventArgs());
		}

		private static void SetAdminRights()
		{
			Window.Title = "Storage Index — Администратор";
			Window.saveButton.Visibility = Visibility.Visible;
			Window.addDeviceButton.Visibility = Visibility.Visible;
			Window.addFolderButton.Visibility = Visibility.Visible;
			Window.addFileButton.Visibility = Visibility.Visible;
			Window.setAdminRights.Visibility = Visibility.Visible;
		}


		public static string EncryptString(string password)
		{
			var hash = SHA1.Create().ComputeHash(Encoding.Default.GetBytes(password));
			var builder = new StringBuilder();
			foreach (var b in hash)
				builder.Append(b.ToString("x2"));
			return builder.ToString();
		}
	}
}
