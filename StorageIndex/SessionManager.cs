using System;
using System.Collections.Generic;
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
		}

		public static void ChangePassword(string oldPassword, string newPassword)
		{
			if (EncryptString(oldPassword) == CurrentUser.Password)
			{
				CurrentUser.Password = Users.First(u => u.Id == CurrentUser.Id).Password = EncryptString(newPassword);
				SaveSystem.SaveUsersToFile();
			}
			else
				new Alert("Пароли не совпадают", "Текущий пароль введён неверно").Show();
		}

		public static void DeleteAccount(User userToDelete)
		{
			if (userToDelete.Id == CurrentUser.Id)
				LogOut();
			Users.Remove(Users.First(u => u.Id == userToDelete.Id));
			SaveSystem.SaveAll();
		}

		private static void SetCommonRights()
		{
			Window.loginButton.Content = CurrentUser.Login + " [Выйти]";
			Window.loginButton.Foreground = new SolidColorBrush(Color.FromRgb(0, 122, 255));
			Window.reportType.Visibility = Visibility.Visible;
			Window.reportButton.Visibility = Visibility.Visible;
		}

		private static void SetAdminRights()
		{
			Window.Title = "Storage Index — Администратор";
			Window.saveButton.Visibility = Visibility.Visible;
			Window.addDeviceButton.Visibility = Visibility.Visible;
			Window.addFolderButton.Visibility = Visibility.Visible;
			Window.addFileButton.Visibility = Visibility.Visible;
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
