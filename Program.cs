using System;
using System.IO;
using System.Windows.Forms;

namespace YoutubeZenniTool;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		string exeDir = AppDomain.CurrentDomain.BaseDirectory;
		if (Directory.Exists(Path.Combine(exeDir, "ffmpeg code")) || Directory.Exists(Path.Combine(exeDir, "bin", "x64")))
		{
			Directory.SetCurrentDirectory(exeDir);
		}
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new MainForm());
	}
}
