using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;
using static StorageIndex.MainWindow;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace StorageIndex
{
	class SaveSystem
	{
		private static string UsersDB = Path.Combine(Environment.CurrentDirectory, "data\\users.txt");

		public static void CreateFilesIfNotPresent()
		{
			if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "data")))
				Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "data"));
			if (!File.Exists(UsersDB))
			{
				File.Create(UsersDB).Close();
				File.AppendAllText(UsersDB, @"[{""Id"":0,""Group"":0,""Login"":""admin"",""Password"":""d033e22ae348aeb5660fc2140aec35850c4da997""}]");
			}
		}

		public static void LoadAll()
		{
			LoadUsersFromFile();
		}

		public static void SaveAll()
		{
			SaveUsersToFile();
		}

		private static void LoadUsersFromFile()
		{
			Users.Clear();
			try
			{
				Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(File.ReadAllText(UsersDB, Encoding.Default))
						?? new ObservableCollection<User>();
			}
			catch (JsonReaderException)
			{
				new Alert("Неверный формат файла", "Файл должен быть в формате JSON").Show();
				UsersDB = Path.Combine(Environment.CurrentDirectory, "data\\users.txt");
			}
		}

		public static void SaveUsersToFile()
		{
			File.WriteAllBytes(UsersDB, new byte[0]);
			File.AppendAllText(UsersDB, JsonConvert.SerializeObject(Users), Encoding.Default);
		}

		public static void SetUsersFileName()
		{
			var dialog = new OpenFileDialog
			{
				InitialDirectory = Environment.SpecialFolder.Desktop.ToString(),
				Filter = "Текстовые файлы (*.txt)|*.txt",
				Title = "Загрузить из JSON-файла"
			};
			if (dialog.ShowDialog() == true)
				UsersDB = dialog.FileName;
			LoadUsersFromFile();
		}
	}
}
