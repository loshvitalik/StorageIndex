using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace StorageIndex
{
	public static class Report
	{
		private static Excel.Application exApp;

		public static void Create(db_dataContext context, string deviceName, string folderName)
		{
			var deviceReport = folderName == null;
			exApp = new Excel.Application();
			exApp.SheetsInNewWorkbook = 1;
			exApp.Workbooks.Add();
			Excel.Worksheet sheet = exApp.Workbooks[1].Worksheets.Item[1];
			var device = context.storage.First(s => s.name == deviceName);
			var index = 1;
			sheet.Range["A1", "A1"].Value = "Устройство: " + device.name;
			sheet.Range["A2", "A2"].Value = "Тип: " + context.mediaTypes.First(t => t.id == device.type).name;
			sheet.Range["A3", "A3"].Value = "Ёмкость: " + device.capacityMb + " МБ";

			foreach (var folder in context.folders.ToList()
				.Where(f => deviceReport ? device.folders.Contains(f) : f.name == folderName))
			{
				sheet.Range["B" + index, "B" + index].Value = folder.name;
				if (folder.files.Count == 0) index++;
				foreach (var file in folder.files.Where(file => folder.files.Contains(file)))
				{
					sheet.Range["C" + index, "C" + index].Value = file.name + file.extension;
					sheet.Range["D" + index, "D" + index].Value = "(размер: " + file.sizeKb + " кБ)";
					index++;
				}
			}

			exApp.Visible = true;
		}
	}
}