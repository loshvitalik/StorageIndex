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

namespace StorageIndex
{
	/// <summary>
	/// Логика взаимодействия для Alert.xaml
	/// </summary>
	public partial class Alert : Window
	{
		public Alert(string title, string content)
		{
			InitializeComponent();
			Title = title;
			label.Content = content;
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
