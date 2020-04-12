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
	/// Логика взаимодействия для AddTypeWindow.xaml
	/// </summary>
	public partial class AddTypeWindow : Window
	{
		public AddTypeWindow()
		{
			InitializeComponent();
		}

		private void AddButton_OnClick(object sender, RoutedEventArgs e)
		{
			context.mediaTypes.Add(new mediaTypes {name = textBoxName.Text});
			context.SaveChanges();
			Close();
		}
	}
}