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
	/// Логика взаимодействия для ServerSelection.xaml
	/// </summary>
	public partial class ServerSelection : Window
	{
		public ServerSelection()
		{
			InitializeComponent();
			if (ServerName != null) textBoxName.Text = ServerName;
		}

		private void AddButton_OnClick(object sender, RoutedEventArgs e)
		{
			ServerName = textBoxName.Text;
			context = new db_dataContext(
				@"metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=""data source=" +
				textBoxName.Text +
				@";initial catalog=db_data;user id=sa;password=1;MultipleActiveResultSets=True;App=EntityFramework""");
			Close();
		}
	}
}