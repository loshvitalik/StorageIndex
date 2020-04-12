using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static StorageIndex.MainWindow;

namespace StorageIndex
{
	public enum Type
	{
		Administrator,
		User
	}

	public class User
	{
		public User(Type group, string login, string password, string dbLogin, string dbPassword)
		{
			Id = Users.Any() ? Users.Last().Id + 1 : 0;
			Group = group;
			Login = login;
			Password = password;
			DbLogin = dbLogin;
			DbPassword = dbPassword;
		}

		[JsonConstructor]
		public User(int id, Type group, string login, string password, string dbLogin, string dbPassword)
		{
			Id = id;
			Group = group;
			Login = login;
			Password = password;
			DbLogin = dbLogin;
			DbPassword = dbPassword;
		}

		public int Id { get; }
		public Type Group { get; set; }
		public string Login { get; }
		public string Password { get; set; }
		public string DbLogin { get; set; }
		public string DbPassword { get; set; }
	}
}
