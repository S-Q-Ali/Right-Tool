using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace YoutubeZenniTool;

internal class FfmpegHelper
{
	public List<string> GetFfmpegCodeFiles()
	{
		List<string> list = new List<string>();
		DirectoryInfo directoryInfo = new DirectoryInfo(".\\ffmpeg code");
		FileInfo[] files = directoryInfo.GetFiles("*.txt");
		FileInfo[] array = files;
		foreach (FileInfo fileInfo in array)
		{
			list.Add(Path.GetFileName(fileInfo.FullName));
		}
		return list;
	}

	public string StripComments(string _003F91_003F)
	{
		string pattern = "(@(?:\"[^\"]*\")+|\"(?:[^\"\\n\\\\]+|\\\\.)*\"|'(?:[^'\\n\\\\]+|\\\\.)*')|//.*|/\\*(?s:.*?)\\*/";
		return Regex.Replace(_003F91_003F, pattern, "$1");
	}

	public void RunCommand(bool _003F86_003F, string _003F91_003F, string _003F89_003F)
	{
		_003F91_003F = _003F91_003F.Replace("ffmpeg", GetFfmpegBinPath() + "ffmpeg");
		_003F91_003F = _003F91_003F.Replace("ffplay", GetFfmpegBinPath() + "ffplay");
		try
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			if (_003F86_003F)
			{
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			}
			else
			{
				processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
			}
			processStartInfo.FileName = "cmd.exe";
			processStartInfo.Arguments = _003F89_003F + " "+ _003F91_003F;
			process.StartInfo = processStartInfo;
			process.Start();
			process.WaitForExit();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public string GetFfmpegBinPath()
	{
		if (Environment.Is64BitOperatingSystem)
		{
			return ".\\bin\\x64\\";
		}
		return ".\\bin\\x86\\";
	}
}
