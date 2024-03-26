
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lesson13;

public class Program
{
	static string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Images Folder";

	static void Main(string[] args)
	{
		try
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}
		catch { Console.WriteLine("\n\n\t\t\tError in Create Directory !\n\n"); }
		ConsoleKeyInfo key = default;

		try
		{
			while (true)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine("\n\n\n\t\t\tPress ' Enter ' Key for Take Screenshot.\n\n");
				Console.WriteLine("\t\t\tPress ' S ' Key for Show All Screenshots.\n\n");
				Console.WriteLine("\t\t\tPress ' E ' Key for Exit.\n\n");
				Console.ForegroundColor = ConsoleColor.White;

				key = Console.ReadKey(true);

				if (key.Key == ConsoleKey.Enter)
				{
					string fileName = "";
					try
					{
						var files = Directory.GetFiles(path).ToList();
						fileName = files.LastOrDefault();
					}
					catch { Console.WriteLine("\n\n\t\t\tError in Get Files !\n\n"); }

					int width = 1920;
					int height = 1080;

					Bitmap ssh = new Bitmap(width, height);

					using (Graphics graphics = Graphics.FromImage(ssh))
					{
						graphics.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
					}

					string endNum = "0";

					if (fileName != null)
					{
						int start = fileName.IndexOf('(');
						int end = fileName.IndexOf(')');

						endNum = fileName.Substring(start+1, end-start-1);

						endNum = $"{long.Parse(endNum)+1}";
					}

					string fName = $"{path}\\ScreenShot ({endNum}).jpeg";

					ssh.Save(fName, ImageFormat.Jpeg);

					Console.WriteLine("\n\t\tImage Saved.\n\n\n");
					Thread.Sleep(2400);
				}
				else if (key.Key == ConsoleKey.E) { return; }
				else if (key.Key == ConsoleKey.S)
				{
					int iok = 0;
					var olp = Directory.GetFiles(path).ToList();

					while (true)
					{
						Console.Clear();

						Console.ForegroundColor = ConsoleColor.DarkYellow;
						Console.WriteLine("\n\n\t\t\tUse KB Arrows or WASD for Select Screenshot.");
						Console.WriteLine("\n\t\t\tPress ' O ' Key for Open Screenshot.");
						Console.WriteLine("\n\t\t\tPress ' B ' Key for Go Back.\n");
						Console.ForegroundColor = ConsoleColor.White;

						ShowMenu(olp, iok);

						var k = KeyCheck(ref iok, 0, olp.Count-1).Key;

						if (k == ConsoleKey.B) break;
						else if (k != ConsoleKey.O) continue;

						ProcessStartInfo info = new ProcessStartInfo();
						Console.WriteLine($"\n\n\t\t\t{olp[iok]}\n\n");
						info.FileName = olp[iok];
						info.UseShellExecute = true;
						info.CreateNoWindow = true;
						info.Verb = string.Empty;
						Process.Start(info);
					}
				}
			}
		}
		catch { Console.WriteLine("\n\n\t\t\tError in While !\n\n"); }
	}

	private static void OpenSamplePhoto()
	{
		string samplePicturesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Sample_Pictures");

		string picturePath = Path.Combine(samplePicturesPath, "sample1.JPG");

		ProcessStartInfo info = new ProcessStartInfo();

		info.FileName = picturePath;
		info.UseShellExecute = true;
		info.CreateNoWindow = true;
		info.Verb = string.Empty; Process.Start(info);
	}

	public static void ShowMenu(in List<string> lst, in int opt)
	{
		int index = 0;

		Console.WriteLine("\n\n");

		foreach (var txt in lst)
		{
			if (index == opt)
			{
				Console.ForegroundColor=ConsoleColor.DarkRed;
				Console.Write($"\t\t\t{txt}\n\n");
				Console.ForegroundColor=ConsoleColor.White;
			}
			else Console.Write($"\t\t\t{txt}\n\n");
			
			index++;
		}
	}
	public static ConsoleKeyInfo KeyCheck(ref int option, int min, int max)
	{
		ConsoleKeyInfo key = Console.ReadKey(true);

		if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W ||
			key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
		{
			option--;
			if (option < min) option = max;
		}
		else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S ||
			key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
		{
			option++;
			if (option > max) option = min;
		}

		return key;
	}
}
