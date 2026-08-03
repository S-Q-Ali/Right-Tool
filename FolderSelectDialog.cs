using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace YoutubeZenniTool;

public class FolderSelectDialog : CommonDialog
{
	private OpenFileDialog _dialog = new OpenFileDialog();

	public OpenFileDialog Dialog
	{
		get
		{
			return _dialog;
		}
		set
		{
			_dialog = value;
		}
	}

	public string SelectedPath
	{
		get
		{
			try
			{
				if (_dialog.FileName != null && (_dialog.FileName.EndsWith("Folder Selection.") || !File.Exists(_dialog.FileName)) && !Directory.Exists(_dialog.FileName))
				{
					return Path.GetDirectoryName(_dialog.FileName);
				}
				return Path.GetDirectoryName(_dialog.FileName);
			}
			catch (Exception)
			{
				return _dialog.FileName;
			}
		}
		set
		{
			if (value != null && value != "")
			{
				_dialog.FileName = value;
			}
		}
	}

	public string SelectedPaths
	{
		get
		{
			if (_dialog.FileNames != null && _dialog.FileNames.Length > 1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string[] fileNames = _dialog.FileNames;
				foreach (string text in fileNames)
				{
					try
					{
						if (File.Exists(text))
						{
							stringBuilder.Append(text + ";");
						}
					}
					catch (Exception)
					{
					}
				}
				return stringBuilder.ToString();
			}
			return null;
		}
	}

	public DialogResult ShowFolderDialog()
	{
		return ShowFolderDialog(null);
	}

	public DialogResult ShowFolderDialog(IWin32Window _003F80_003F)
	{
		_dialog.ValidateNames = false;
		_dialog.CheckFileExists = false;
		_dialog.CheckPathExists = true;
		try
		{
			if (_dialog.FileName != null && _dialog.FileName != "")
			{
				if (Directory.Exists(_dialog.FileName))
				{
					_dialog.InitialDirectory = _dialog.FileName;
				}
				else
				{
					_dialog.InitialDirectory = Path.GetDirectoryName(_dialog.FileName);
				}
			}
		}
		catch (Exception)
		{
		}
		_dialog.FileName = "Folder Selection.";
		if (_003F80_003F == null)
		{
			return _dialog.ShowDialog();
		}
		return _dialog.ShowDialog(_003F80_003F);
	}

	public override void Reset()
	{
		_dialog.Reset();
	}

	protected override bool RunDialog(IntPtr _003F81_003F)
	{
		return true;
	}
}
