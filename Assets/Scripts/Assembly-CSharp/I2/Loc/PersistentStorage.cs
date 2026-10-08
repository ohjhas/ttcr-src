using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	public static class PersistentStorage
	{
		public static bool Save(string fileName, string data)
		{
			try
			{
				string path = Application.persistentDataPath + "/" + fileName + ".loc";
				File.WriteAllText(path, data, Encoding.UTF8);
				return true;
			}
			catch (Exception)
			{
				Debug.LogError("Error saving file " + fileName);
				return false;
			}
		}

		public static string Load(string fileName)
		{
			try
			{
				string path = Application.persistentDataPath + "/" + fileName + ".loc";
				return File.ReadAllText(path, Encoding.UTF8);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static void Delete(string fileName)
		{
			try
			{
				string path = Application.persistentDataPath + "/" + fileName + ".loc";
				File.Delete(path);
			}
			catch (Exception)
			{
			}
		}
	}
}
