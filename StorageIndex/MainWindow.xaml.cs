using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StorageIndex
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static ObservableCollection<User> Users;
		public static User CurrentUser;
		private db_dataContext context = new db_dataContext();

		public MainWindow()
		{
			InitializeComponent();
			Users = new ObservableCollection<User>();
			SaveSystem.CreateFilesIfNotPresent();
			SaveSystem.LoadAll();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			context.files.Load();
			context.mediaTypes.Load();
			context.storage.Load();
			context.folders.Load();
			System.Windows.Data.CollectionViewSource storageViewSource =
				((System.Windows.Data.CollectionViewSource) (this.FindResource("storageViewSource")));
			// Загрузите данные, установив свойство CollectionViewSource.Source:
			// storageViewSource.Source = [универсальный источник данных]
			storageDataGrid.ItemsSource = context.storage.Local;
			System.Windows.Data.CollectionViewSource foldersViewSource =
				((System.Windows.Data.CollectionViewSource) (this.FindResource("foldersViewSource")));
			// Загрузите данные, установив свойство CollectionViewSource.Source:
			// foldersViewSource.Source = [универсальный источник данных]
			System.Windows.Data.CollectionViewSource filesViewSource =
				((System.Windows.Data.CollectionViewSource) (this.FindResource("filesViewSource")));
			// Загрузите данные, установив свойство CollectionViewSource.Source:
			// filesViewSource.Source = [универсальный источник данных]
		}

		private void storageDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foldersDataGrid.ItemsSource =
				context.folders.Local.Where(f =>
					context.storage.First(s => ((storage) storageDataGrid.SelectedItem).id == s.id).folders
						.Contains(f));
			filesDataGrid.ItemsSource = new List<files>();
		}

		private void foldersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			filesDataGrid.ItemsSource = foldersDataGrid.SelectedItem == null
				? new List<files>()
				: context.files.Local.Where(f =>
					context.folders.First(s => ((folders) foldersDataGrid.SelectedItem).id == s.id).files.Contains(f));
		}

		private void SaveButton_OnClick(object sender, RoutedEventArgs e)
		{
			context.SaveChanges();
		}

		private void searchButton_Click(object sender, RoutedEventArgs e)
		{
			switch (searchType.SelectedIndex)
			{
				case 0:
					storageDataGrid.ItemsSource = searchBox.Text == ""
						? context.storage.Local
						: context.storage.Local.Where(f =>
							searchBox.Text.Contains(f.name, StringComparison.OrdinalIgnoreCase));

					foldersDataGrid.ItemsSource = new List<folders>();
					filesDataGrid.ItemsSource = new List<files>();
					break;
				case 1:
					if (storageDataGrid.SelectedItem == null) return;
					foldersDataGrid.ItemsSource = searchBox.Text == ""
						? context.folders.Local.Where(f =>
							context.storage.First(s => ((storage) storageDataGrid.SelectedItem).id == s.id).folders
								.Contains(f))
						: context.folders.Local.Where(f =>
							context.storage.First(s => ((storage) storageDataGrid.SelectedItem).id == s.id).folders
								.Contains(f) && searchBox.Text.Contains(f.name, StringComparison.OrdinalIgnoreCase));
					filesDataGrid.ItemsSource = new List<files>();
					break;
				case 2:
					if (foldersDataGrid.SelectedItem == null) return;
					filesDataGrid.ItemsSource = searchBox.Text == ""
							? context.files.Local.Where(f =>
								context.folders.First(s => ((folders) foldersDataGrid.SelectedItem).id == s.id).files
									.Contains(f))
							: context.files.Local.Where(f =>
								context.folders.First(s => ((folders) foldersDataGrid.SelectedItem).id == s.id)
									.files
									.Contains(f) &&
								searchBox.Text.Contains(f.name, StringComparison.OrdinalIgnoreCase));
					break;
			}
		}

		private void reportButton_Click(object sender, RoutedEventArgs e)
		{
			if (reportType.SelectedIndex == 0 && storageDataGrid.SelectedIndex == -1) return;
			if (storageDataGrid.SelectedIndex == -1 && foldersDataGrid.SelectedIndex == -1) return;
			Report.Create(context, ((storage)storageDataGrid.SelectedItem).name, reportType.SelectedIndex == 0 ? null : ((folders)foldersDataGrid.SelectedItem).name);
		}

		private void LoginButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (CurrentUser == null)
				new LoginWindow().Show();
			else
				SessionManager.LogOut();
		}
	}
}