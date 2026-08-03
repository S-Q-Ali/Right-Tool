using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace YoutubeZenniTool;

internal class FfmpegHelper
{
	public List<string> GetFfmpegCodeFiles()
	{
		List<string> list = new List<string>();
		DirectoryInfo directoryInfo = new DirectoryInfo(GetFfmpegCodeDir());
		FileInfo[] files = directoryInfo.GetFiles("*.txt");
		FileInfo[] array = files;
		foreach (FileInfo fileInfo in array)
		{
			list.Add(Path.GetFileName(fileInfo.FullName));
		}
		return list;
	}

	public string GetFfmpegCodeDir()
	{
		string exeDir = AppDomain.CurrentDomain.BaseDirectory;
		if (Directory.Exists(Path.Combine(exeDir, "ffmpeg code")))
		{
			return Path.Combine(exeDir, "ffmpeg code");
		}
		return Path.GetFullPath(".\\ffmpeg code");
	}

	public string GetFfmpegBinPath()
	{
		string exeDir = AppDomain.CurrentDomain.BaseDirectory;
		string candidate = Environment.Is64BitOperatingSystem ? "bin\\x64" : "bin\\x86";
		if (Directory.Exists(Path.Combine(exeDir, candidate)))
		{
			return Path.Combine(exeDir, candidate) + "\\";
		}
		return (Environment.Is64BitOperatingSystem ? ".\\bin\\x64\\" : ".\\bin\\x86\\");
	}

	public string StripComments(string _003F91_003F)
	{
		string pattern = "(@(?:\"[^\"]*\")+|\"(?:[^\"\\n\\\\]+|\\\\.)*\"|'(?:[^'\\n\\\\]+|\\\\.)*')|//.*|/\\*(?s:.*?)\\*/";
		return Regex.Replace(_003F91_003F, pattern, "$1");
	}

	public CommandResult RunCommand(bool _003F86_003F, string _003F91_003F, string _003F89_003F)
	{
		_003F91_003F = _003F91_003F.Replace("ffmpeg", GetFfmpegBinPath() + "ffmpeg");
		_003F91_003F = _003F91_003F.Replace("ffplay", GetFfmpegBinPath() + "ffplay");
		CommandResult result = new CommandResult();
		try
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.CreateNoWindow = _003F86_003F;
			processStartInfo.FileName = "cmd.exe";
			processStartInfo.Arguments = _003F89_003F + " " + _003F91_003F;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.UseShellExecute = false;
			process.StartInfo = processStartInfo;
			process.Start();

			StringBuilder errorBuilder = new StringBuilder();
			process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e)
			{
				if (e.Data != null)
				{
					errorBuilder.AppendLine(e.Data);
				}
			};
			process.BeginErrorReadLine();
			result.Output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			result.ExitCode = process.ExitCode;
			result.Error = errorBuilder.ToString();
		}
		catch (Exception ex)
		{
			result.ExitCode = 1;
			result.Error = ex.Message;
		}
		return result;
	}
}

public class CommandResult
{
	public int ExitCode { get; set; }
	public string Output { get; set; } = "";
	public string Error { get; set; } = "";
}
