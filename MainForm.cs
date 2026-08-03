using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using YoutubeZenniTool;

namespace YoutubeZenniTool;

public class MainForm : Form
{
	[CompilerGenerated]
	private sealed class _003F13_003F
	{
		public int _003F178_003F;

		public MainForm _003F179_003F;

		internal void _003F78_003F()
		{
			_003F179_003F.RenderSingleItem(_003F178_003F++);
		}
	}

	private FfmpegHelper _ffmpegHelper = new FfmpegHelper();

	private Thread _renderThread;

	private string _tempFile = "";

	private int _fileCounter = 0;

	private IContainer _components = null;

	private TabControl tabControl;

	private TabPage tabRender;

	private TabPage tabLiveStream;

	private GroupBox grpLiveInfo;

	private RadioButton rbLiveTextList;

	private RadioButton rbLiveFolder;

	private RadioButton rbLiveFile;

	private GroupBox grpLiveSetting;

	private Label lblPreset;

	private ComboBox cbbPreset;

	private TextBox txtLiveSize;

	private TextBox txtLiveBitrate;

	private Label lblSize;

	private Label lblBitrate;

	private CheckBox cbLiveLoop;

	private TextBox txtLiveOutput;

	private Label lblLiveOutput;

	private Button btLiveInput;

	private TextBox txtLiveInput;

	private Label lblLiveInput;

	private Label lblSeparator;

	private TextBox txtLogoLocation;

	private Label lblLogoLocation;

	private TextBox txtLogoSize;

	private Label lblLogoSize;

	private Button btLiveLogo;

	private TextBox txtLiveLogo;

	private CheckBox cbLogo;

	private ComboBox cbbOption;

	private Button btLiveStop;

	private Button btLiveRun;

	private Button btLiveImage;

	private TextBox txtLiveImage;

	private Label lblImage;

	private CheckBox cbNonFilter;

	private NumericUpDown numVolume;

	private Label lblVolume;

	private RichTextBox rtbInstructions;

	private LinkLabel lnkGetLink;

	private Button btnLivePreview;

	private Button btnLiveRen10s;

	private GroupBox grpRenderSettings;

	private Button btnBrowseOutput;

	private TextBox txtOutputRender;

	private Label lblOutput;

	private GroupBox grpCodeEditor;

	private Button btnReload;

	private Button btnDelete;

	private ComboBox cbbFfmpegCode;

	private Label lblFfmpegCode;

	private DataGridView dgvRender;

	private Button btnImportVideo;

	private Button btnEditCode;

	private CheckBox cbHideFfmpeg;

	private Button btnPreview;

	private Button btnStop;

	private Button btnStart;

	private ComboBox cbbAddVideoInput;

	private Label lblRenderInput;

	private DataGridViewTextBoxColumn colVideoInput;

	private DataGridViewTextBoxColumn colStatus;

	private Label lblThread;

	private NumericUpDown numThread;

	public MainForm()
	{
		InitializeComponent();
		_renderThread = new Thread((ThreadStart)delegate
		{
			RenderSingleItem(0);
		});
		txtOutputRender.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
		cbbAddVideoInput.SelectedIndex = 0;
		cbbFfmpegCode.DataSource = _ffmpegHelper.GetFfmpegCodeFiles();
		cbbOption.SelectedIndex = 0;
		cbbPreset.SelectedIndex = 4;
		cbNonFilter.Checked = true;
		Control.CheckForIllegalCrossThreadCalls = false;
	}

	private void TabRender_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void TabRender_Enter(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtLiveRun_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		_tempFile = "textlist"+ _fileCounter++ + ".txt";
		ThreadStart start = RunLiveStream;
		Thread thread = new Thread(start);
		thread.Start();
	}

	private void RunLiveStream()
	{
		string text = txtLiveBitrate.Text;
		string text2 = txtLiveSize.Text;
		string text3 = txtLiveInput.Text;
		string text4 = txtLiveOutput.Text;
		string text5 = numVolume.Value.ToString();
		string text6 = "";
		string text7 = "";
		string text8 = Directory.GetCurrentDirectory() + "\\icon\\logo.png";
		string text9 = "100x100";
		string text10 = "0:0";
		if (cbNonFilter.Checked)
		{
			txtLiveBitrate.Enabled = false;
			txtLiveSize.Enabled = false;
			text = "";
			text2 = "";
		}
		else
		{
			txtLiveBitrate.Enabled = true;
			txtLiveSize.Enabled = true;
			text = txtLiveBitrate.Text;
			text2 = txtLiveSize.Text;
		}
		if (cbbPreset.SelectedIndex == 0)
		{
			text7 = "veryslow";
		}
		else if (cbbPreset.SelectedIndex == 1)
		{
			text7 = "slower";
		}
		else if (cbbPreset.SelectedIndex == 2)
		{
			text7 = "slow";
		}
		else if (cbbPreset.SelectedIndex == 3)
		{
			text7 = "medium";
		}
		else if (cbbPreset.SelectedIndex == 4)
		{
			text7 = "fast";
		}
		else if (cbbPreset.SelectedIndex == 5)
		{
			text7 = "faster";
		}
		else if (cbbPreset.SelectedIndex == 6)
		{
			text7 = "veryfast";
		}
		else if (cbbPreset.SelectedIndex == 7)
		{
			text7 = "superfast";
		}
		else if (cbbPreset.SelectedIndex == 8)
		{
			text7 = "ultrafast";
		}
		if (cbLogo.Checked)
		{
			txtLiveLogo.Enabled = true;
			btLiveLogo.Enabled = true;
			txtLogoSize.Enabled = true;
			txtLogoLocation.Enabled = true;
			text8 = txtLiveLogo.Text;
			text9 = txtLogoSize.Text;
			text10 = txtLogoLocation.Text;
		}
		else
		{
			txtLiveLogo.Enabled = false;
			btLiveLogo.Enabled = false;
			txtLogoSize.Enabled = false;
			txtLogoLocation.Enabled = false;
			text8 = Directory.GetCurrentDirectory() + "\\icon\\logo.png";
			text9 = "100x100";
			text10 = "0:0";
		}
		if (rbLiveFile.Checked)
		{
			if ((txtLiveInput.Text.Contains("https://") && !txtLiveInput.Text.Contains("goo.gl") && !txtLiveInput.Text.Contains(".m3u8") && !txtLiveInput.Text.Contains("googlevideo.com")) || (txtLiveInput.Text.Contains("www.") && !txtLiveInput.Text.Contains("goo.gl") && !txtLiveInput.Text.Contains(".m3u8") && !txtLiveInput.Text.Contains("googlevideo.com")))
			{
				string text11 = ".\\bin\\youtube-dl -g -f best "+ txtLiveInput.Text;
				string text12 = string.Empty;
				string text13 = string.Empty;
				ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c "+ text11);
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.RedirectStandardError = true;
				processStartInfo.CreateNoWindow = false;
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				processStartInfo.UseShellExecute = false;
				Process process = Process.Start(processStartInfo);
				using (StreamReader streamReader = process.StandardOutput)
				{
					text12 = streamReader.ReadToEnd();
				}
				using (StreamReader streamReader2 = process.StandardError)
				{
					text13 = streamReader2.ReadToEnd();
				}
				Console.WriteLine("The following output was detected:");
				text3 = text12.Trim();
				if (string.IsNullOrEmpty(text12))
				{
					Console.WriteLine("The following error was detected:");
					MessageBox.Show(text13, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				text3 = txtLiveInput.Text.Trim();
			}
			_tempFile = "VienNV_"+ _fileCounter + ".txt";
			while (File.Exists(_tempFile))
			{
				_tempFile = "VienNV_"+ _fileCounter++ + ".txt";
			}
			File.WriteAllText(_tempFile, text3);
			text6 = (cbLiveLoop.Checked ? ((!cbNonFilter.Checked) ? (":loop\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -i \""+ text8 + "\" -filter_complex \"[1:v]scale = "+ text9 + "[logo]; [0:v][logo]overlay = "+ text10 + "; [0:a]volume = "+ text5 + "\" -c:a aac -s "+ text2 + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ text + " -maxrate "+ text + " -bufsize "+ text + " -framerate 30 -g 4 -threads 0 -preset "+ text7 + " -f flv \""+ text4 + "\"\ngoto loop") : (":loop\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -ar 44100 -vcodec copy -f flv \""+ text4 + "\"\ngoto loop")) : ((!cbNonFilter.Checked) ? ("for /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -i \""+ text8 + "\" -filter_complex \"[1:v]scale = "+ text9 + "[logo]; [0:v][logo]overlay = "+ text10 + "; [0:a]volume = "+ text5 + "\" -c:a aac -s "+ text2 + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ text + " -maxrate "+ text + " -bufsize "+ text + " -framerate 30 -g 4 -threads 0 -preset "+ text7 + " -f flv \""+ text4 + "\"") : ("for /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -ar 44100 -vcodec copy -f flv \""+ text4 + "\"")));
		}
		if (rbLiveFolder.Checked)
		{
			text6 = (cbLiveLoop.Checked ? ((!cbNonFilter.Checked) ? ("dir/b/s \""+ text3 + "\\*.*\" > "+ _tempFile + "\n:loop\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -i \""+ text8 + "\" -filter_complex \"[1:v]scale = "+ text9 + "[logo]; [0:v][logo]overlay = "+ text10 + "; [0:a]volume = "+ text5 + "\"  -c:a aac -s "+ text2 + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ text + " -maxrate "+ text + " -bufsize "+ text + " -framerate 30 -g 4 -threads 0 -preset "+ text7 + " -f flv \""+ text4 + "\"\ngoto loop") : ("dir/b/s \""+ text3 + "\\*.*\" > "+ _tempFile + "\n:loop\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -ar 44100 -vcodec copy -f flv \""+ text4 + "\"\ngoto loop")) : ((!cbNonFilter.Checked) ? ("dir/b/s \""+ text3 + "\\*.*\" > "+ _tempFile + "\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -i \""+ text8 + "\" -filter_complex \"[1:v]scale = "+ text9 + "[logo]; [0:v][logo]overlay = "+ text10 + "; [0:a]volume = "+ text5 + "\"  -c:a aac -s "+ text2 + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ text + " -maxrate "+ text + " -bufsize "+ text + " -framerate 30 -g 4 -threads 0 -preset "+ text7 + " -f flv \""+ text4 + "\"") : ("dir/b/s \""+ text3 + "\\*.*\" > "+ _tempFile + "\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -ar 44100 -vcodec copy -f flv \""+ text4 + "\"")));
		}
		if (rbLiveTextList.Checked)
		{
			if (cbLiveLoop.Checked)
			{
				if (cbNonFilter.Checked)
				{
					_tempFile = txtLiveInput.Text;
					text6 = ":loop\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -ar 44100 -vcodec copy -f flv \""+ text4 + "\"\ngoto loop";
				}
				else
				{
					_tempFile = txtLiveInput.Text;
					text6 = ":loop\nfor /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -i \""+ text8 + "\" -filter_complex \"[1:v]scale = "+ text9 + "[logo]; [0:v][logo]overlay = "+ text10 + "; [0:a]volume = "+ text5 + "\" -c:a aac -s "+ text2 + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ text + " -maxrate "+ text + " -bufsize "+ text + " -framerate 30 -g 4 -threads 0 -preset "+ text7 + " -f flv \""+ text4 + "\"\ngoto loop";
				}
			}
			else if (cbNonFilter.Checked)
			{
				_tempFile = txtLiveInput.Text;
				text6 = "for /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -ar 44100 -vcodec copy -f flv \""+ text4 + "\"";
			}
			else
			{
				_tempFile = txtLiveInput.Text;
				text6 = "for /F \"delims=;\" %%F in ("+ _tempFile + ") DO ffmpeg -re -i \"%%F\" -i \""+ text8 + "\" -filter_complex \"[1:v]scale = "+ text9 + "[logo]; [0:v][logo]overlay = "+ text10 + "; [0:a]volume = "+ text5 + "\" -c:a aac -s "+ text2 + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ text + " -maxrate "+ text + " -bufsize "+ text + " -framerate 30 -g 4 -threads 0 -preset "+ text7 + " -f flv \""+ text4 + "\"";
			}
		}
		text6 = text6.Replace("ffmpeg", _ffmpegHelper.GetFfmpegBinPath() + "ffmpeg");
		text6 = text6.Replace("ffplay", _ffmpegHelper.GetFfmpegBinPath() + "ffplay");
		string text14 = "VienNV_"+ _fileCounter + ".bat";
		while (File.Exists(text14))
		{
			text14 = "VienNV_"+ _fileCounter++ + ".bat";
		}
		File.WriteAllText(text14, text6);
		Process.Start("CMD.exe", "/c start "+ text14);
		Process.Start("CMD.exe", "/c attrib +h "+ text14);
		Process.Start("CMD.exe", "/c attrib +h "+ _tempFile);
	}

	private void TabRender_Leave(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void TabRender_Paint(object _003F82_003F, PaintEventArgs _003F83_003F)
	{
	}

	private void Browser_DocumentCompleted(object _003F82_003F, WebBrowserDocumentCompletedEventArgs _003F83_003F)
	{
	}

	private void TabLiveStream_Paint(object _003F82_003F, PaintEventArgs _003F83_003F)
	{
	}

	private void TabLiveStream_Leave(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtLiveInput_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (rbLiveFile.Checked)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				txtLiveInput.Text = openFileDialog.FileName;
			}
		}
		if (rbLiveFolder.Checked)
		{
			FolderSelectDialog folderDialog = new FolderSelectDialog();
			folderDialog.ShowFolderDialog();
			txtLiveInput.Text = folderDialog.SelectedPath;
		}
		if (rbLiveTextList.Checked)
		{
			OpenFileDialog openFileDialog2 = new OpenFileDialog();
			openFileDialog2.InitialDirectory = Directory.GetCurrentDirectory();
			openFileDialog2.Filter = "txt Files (*.txt)|*.txt";
			if (openFileDialog2.ShowDialog() == DialogResult.OK)
			{
				txtLiveInput.Text = Path.GetFileName(openFileDialog2.FileName);
			}
		}
	}

	private void CbNonFilter_CheckedChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (cbNonFilter.Checked)
		{
			txtLiveBitrate.Enabled = false;
			txtLiveSize.Enabled = false;
			cbLogo.Enabled = false;
			txtLiveLogo.Enabled = false;
			btLiveLogo.Enabled = false;
			txtLogoSize.Enabled = false;
			txtLogoLocation.Enabled = false;
			numVolume.Enabled = false;
			numVolume.Value = 1m;
		}
		else
		{
			txtLiveBitrate.Enabled = true;
			txtLiveSize.Enabled = true;
			cbLogo.Enabled = true;
			cbLogo.Checked = false;
			numVolume.Enabled = true;
		}
	}

	private void CbbPreset_SelectedIndexChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void CbLogo_CheckedChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (cbLogo.Checked)
		{
			txtLiveLogo.Enabled = true;
			btLiveLogo.Enabled = true;
			txtLogoSize.Enabled = true;
			txtLogoLocation.Enabled = true;
		}
		else
		{
			txtLiveLogo.Enabled = false;
			btLiveLogo.Enabled = false;
			txtLogoSize.Enabled = false;
			txtLogoLocation.Enabled = false;
		}
	}

	private void BtLiveLogo_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			txtLiveLogo.Text = openFileDialog.FileName;
		}
	}

	private void BtLiveStop_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		Process.Start("taskkill", "/F /IM ffmpeg.exe");
		Process.Start("taskkill", "/F /IM cmd.exe");
		Directory.EnumerateFiles(".", "*.bat").ToList().ForEach(delegate(string _003F92_003F)
		{
			File.Delete(_003F92_003F);
		});
		Directory.EnumerateFiles(".", "*.txt").ToList().ForEach(delegate(string _003F92_003F)
		{
			File.Delete(_003F92_003F);
		});
	}

	private void BtLiveImage_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void RbLive_CheckedChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void TabRender_DoubleClick(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void TabLiveStream_DoubleClick(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void LnkGetLink_LinkClicked(object _003F82_003F, LinkLabelLinkClickedEventArgs _003F83_003F)
	{
		GetLinkForm getLinkForm = new GetLinkForm();
		getLinkForm.ShowDialog();
	}

	private void RtbInstructions_TextChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void TabLiveStream_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	public void PreviewLive()
	{
		string text = "";
		string text2 = Directory.GetCurrentDirectory() + "\\icon\\logo.png";
		string text3;
		string text4;
		if (cbLogo.Checked)
		{
			text2 = txtLiveLogo.Text;
			text3 = txtLogoSize.Text;
			text4 = txtLogoLocation.Text;
		}
		else
		{
			text2 = Directory.GetCurrentDirectory() + "\\icon\\logo.png";
			text3 = "100x100";
			text4 = "0:0";
		}
		if (text2 == string.Empty)
		{
			MessageBox.Show("Logo can not empty!\nPlease insert a logo.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if ((txtLiveInput.Text.Contains("https://") && !txtLiveInput.Text.Contains("goo.gl") && !txtLiveInput.Text.Contains(".m3u8") && !txtLiveInput.Text.Contains("googlevideo.com")) || (txtLiveInput.Text.Contains("www.") && !txtLiveInput.Text.Contains("goo.gl") && !txtLiveInput.Text.Contains(".m3u8") && !txtLiveInput.Text.Contains("googlevideo.com")))
		{
			string text5 = ".\\bin\\youtube-dl -g -f best "+ txtLiveInput.Text;
			string text6 = string.Empty;
			string text7 = string.Empty;
			ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c "+ text5);
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.CreateNoWindow = false;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.UseShellExecute = false;
			Process process = Process.Start(processStartInfo);
			using (StreamReader streamReader = process.StandardOutput)
			{
				text6 = streamReader.ReadToEnd();
			}
			using (StreamReader streamReader2 = process.StandardError)
			{
				text7 = streamReader2.ReadToEnd();
			}
			Console.WriteLine("The following output was detected:");
			text = text6.Trim();
			if (string.IsNullOrEmpty(text6))
			{
				Console.WriteLine("The following error was detected:");
				MessageBox.Show(text7, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else
		{
			text = txtLiveInput.Text.Trim();
		}
		string text8 = "ffmpeg -re -i \""+ text + "\" -i \""+ text2 + "\" -filter_complex \"[1:v]scale = "+ text3 + "[logo]; [0:v][logo]overlay = "+ text4 + "; [0:a]volume = "+ numVolume.Value + "\" -c:a aac -s "+ txtLiveSize.Text + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ txtLiveBitrate.Text + " -maxrate "+ txtLiveBitrate.Text + " -bufsize "+ txtLiveBitrate.Text + " -framerate 30 -g 4 -threads 0 -preset fast -f matroska - | ffplay -";
		text8 = text8.Replace("ffmpeg", _ffmpegHelper.GetFfmpegBinPath() + "ffmpeg");
		text8 = text8.Replace("ffplay", _ffmpegHelper.GetFfmpegBinPath() + "ffplay");
		Process process2 = new Process();
		ProcessStartInfo processStartInfo2 = new ProcessStartInfo();
		processStartInfo2.WindowStyle = ProcessWindowStyle.Normal;
		processStartInfo2.FileName = "cmd.exe";
		processStartInfo2.Arguments = "/k "+ text8;
		process2.StartInfo = processStartInfo2;
		process2.Start();
	}

	public void RenderTenSeconds()
	{
		string text = "";
		string text2 = Directory.GetCurrentDirectory() + "\\icon\\logo.png";
		string text3;
		string text4;
		if (cbLogo.Checked)
		{
			text2 = txtLiveLogo.Text;
			text3 = txtLogoSize.Text;
			text4 = txtLogoLocation.Text;
		}
		else
		{
			text2 = Directory.GetCurrentDirectory() + "\\icon\\logo.png";
			text3 = "100x100";
			text4 = "0:0";
		}
		if (text2 == string.Empty)
		{
			MessageBox.Show("Logo can not empty!\nPlease insert a logo.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if ((txtLiveInput.Text.Contains("https://") && !txtLiveInput.Text.Contains("goo.gl") && !txtLiveInput.Text.Contains(".m3u8") && !txtLiveInput.Text.Contains("googlevideo.com")) || (txtLiveInput.Text.Contains("www.") && !txtLiveInput.Text.Contains("goo.gl") && !txtLiveInput.Text.Contains(".m3u8") && !txtLiveInput.Text.Contains("googlevideo.com")))
		{
			string text5 = ".\\bin\\youtube-dl -g -f best "+ txtLiveInput.Text;
			string text6 = string.Empty;
			string text7 = string.Empty;
			ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c "+ text5);
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.CreateNoWindow = false;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.UseShellExecute = false;
			Process process = Process.Start(processStartInfo);
			using (StreamReader streamReader = process.StandardOutput)
			{
				text6 = streamReader.ReadToEnd();
			}
			using (StreamReader streamReader2 = process.StandardError)
			{
				text7 = streamReader2.ReadToEnd();
			}
			Console.WriteLine("The following output was detected:");
			text = text6.Trim();
			if (string.IsNullOrEmpty(text6))
			{
				Console.WriteLine("The following error was detected:");
				MessageBox.Show(text7, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else
		{
			text = txtLiveInput.Text.Trim();
		}
		string text8 = "ffmpeg -y -i \""+ text + "\" -i \""+ text2 + "\" -filter_complex \"[1:v]scale = "+ text3 + "[logo]; [0:v][logo]overlay = "+ text4 + "; [0:a]volume = "+ numVolume.Value + "\" -c:a aac -s "+ txtLiveSize.Text + " -ab 128k -vcodec libx264 -pix_fmt yuv420p -minrate "+ txtLiveBitrate.Text + " -maxrate "+ txtLiveBitrate.Text + " -bufsize "+ txtLiveBitrate.Text + " -framerate 30 -g 4 -threads 0 -preset fast -t 10 preview.mp4";
		text8 = text8.Replace("ffmpeg", _ffmpegHelper.GetFfmpegBinPath() + "ffmpeg");
		text8 = text8.Replace("ffplay", _ffmpegHelper.GetFfmpegBinPath() + "ffplay");
		Process process2 = new Process();
		ProcessStartInfo processStartInfo2 = new ProcessStartInfo();
		processStartInfo2.WindowStyle = ProcessWindowStyle.Normal;
		processStartInfo2.FileName = "cmd.exe";
		processStartInfo2.Arguments = "/C "+ text8;
		process2.StartInfo = processStartInfo2;
		process2.Start();
		process2.WaitForExit();
		processStartInfo2.WindowStyle = ProcessWindowStyle.Hidden;
		processStartInfo2.Arguments = "/C preview.mp4";
		process2.StartInfo = processStartInfo2;
		process2.Start();
		process2.WaitForExit();
	}

	private void BtnLivePreview_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (cbNonFilter.Checked || rbLiveFolder.Checked || rbLiveTextList.Checked)
		{
			MessageBox.Show("Preview not available.\nPlease choose Option File or Link and uncheck Non Filter", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (txtLiveInput.Text == string.Empty)
		{
			MessageBox.Show("Input can not Empty!\nPlease insert input before preview", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			PreviewLive();
		}
	}

	private void BtnLiveRen10s_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (cbNonFilter.Checked || rbLiveFolder.Checked || rbLiveTextList.Checked)
		{
			MessageBox.Show("Preview not available.\nPlease choose Option File or Link and uncheck Non Filter", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (txtLiveInput.Text == string.Empty)
		{
			MessageBox.Show("Input can not Empty!\nPlease insert input before preview", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			RenderTenSeconds();
		}
	}

	private void CbbOption_SelectedIndexChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtnReload_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		try
		{
			cbbFfmpegCode.DataSource = _ffmpegHelper.GetFfmpegCodeFiles();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void BtnDelete_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		try
		{
			File.Delete(".\\ffmpeg code\\"+ cbbFfmpegCode.SelectedItem.ToString());
			cbbFfmpegCode.DataSource = _ffmpegHelper.GetFfmpegCodeFiles();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void TxtOutputRender_TextChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtnBrowseOutput_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		FolderSelectDialog folderDialog = new FolderSelectDialog();
		if (folderDialog.ShowFolderDialog() == DialogResult.OK)
		{
			txtOutputRender.Text = folderDialog.SelectedPath;
		}
	}

	private void LblOutput_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtnStop_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		_renderThread.Abort();
		Process.Start("cmd.exe", "/c taskkill /F /IM ffmpeg.exe /T");
		Process.Start("cmd.exe", "/c taskkill /F /IM cmd.exe /T");
	}

	private void BtnImportVideo_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Multiselect = true;
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			string[] fileNames = openFileDialog.FileNames;
			foreach (string text in fileNames)
			{
				dgvRender.Rows.Add(text, "Waiting");
			}
		}
	}

	private void BtnStart_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (!_renderThread.IsAlive)
		{
			_renderThread = new Thread((ThreadStart)delegate
			{
				_003F13_003F CS_0024_003C_003E8__locals5 = new _003F13_003F();
				CS_0024_003C_003E8__locals5._003F179_003F = this;
				CS_0024_003C_003E8__locals5._003F178_003F = 0;
				while (CS_0024_003C_003E8__locals5._003F178_003F < dgvRender.Rows.Count)
				{
					Process[] processesByName = Process.GetProcessesByName("ffmpeg");
					int num = processesByName.Length;
					Thread.Sleep(100);
					if ((decimal)num < numThread.Value)
					{
						Thread thread = new Thread((ThreadStart)delegate
						{
							CS_0024_003C_003E8__locals5._003F179_003F.RenderSingleItem(CS_0024_003C_003E8__locals5._003F178_003F++);
						});
						Thread.Sleep(100);
						thread.Start();
					}
					Thread.Sleep(500);
				}
			});
			_renderThread.Start();
		}
		else
		{
			MessageBox.Show("Program is running", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void RenderSingleItem(int _003F85_003F)
	{
		string text = _ffmpegHelper.StripComments(File.ReadAllText(".\\ffmpeg code\\"+ cbbFfmpegCode.SelectedItem.ToString()));
		string text2 = "";
		text2 = ((!text.Contains("{output}.*")) ? ("\""+ txtOutputRender.Text + "\\"+ Path.GetFileNameWithoutExtension(dgvRender.Rows[_003F85_003F].Cells[0].Value.ToString())) : ("\""+ txtOutputRender.Text + "\\"+ Path.GetFileName(dgvRender.Rows[_003F85_003F].Cells[0].Value.ToString())));
		dgvRender.Rows[_003F85_003F].DefaultCellStyle.BackColor = Color.Yellow;
		dgvRender.Rows[_003F85_003F].Cells[1].Value = "Processing...";
		RunFfmpegCommand(cbHideFfmpeg.Checked, _003F85_003F, text2, "/c");
		dgvRender.Rows[_003F85_003F].DefaultCellStyle.BackColor = Color.LimeGreen;
		dgvRender.Rows[_003F85_003F].Cells[1].Value = "Render Completed";
	}

	private void MainForm_Load(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtnEditCode_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		string text = cbbFfmpegCode.SelectedItem.ToString();
		EditFfmpegCodeForm editCodeForm = new EditFfmpegCodeForm(".\\ffmpeg code\\"+ text);
		editCodeForm.Show();
	}

	private void CbHideFfmpeg_CheckedChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtnPreview_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (dgvRender.RowCount > 0)
		{
			_renderThread = new Thread((ThreadStart)delegate
			{
				int rowIndex = dgvRender.CurrentCell.RowIndex;
				RunFfmpegCommand(_003F86_003F: false, rowIndex, "-f matroska - | ffplay -", "/k");
			});
			_renderThread.Start();
		}
	}

	private void RunFfmpegCommand(bool _003F86_003F, int _003F87_003F, string _003F88_003F, string _003F89_003F)
	{
		string text = _ffmpegHelper.StripComments(File.ReadAllText(".\\ffmpeg code\\"+ cbbFfmpegCode.SelectedItem.ToString()));
		if (text.Contains("{input}.*"))
		{
			text = text.Replace("{input}.*", dgvRender.Rows[_003F87_003F].Cells[0].Value.ToString());
		}
		else
		{
			string oldValue = text.Substring(text.IndexOf("{input}."), 11);
			text = text.Replace(oldValue, dgvRender.Rows[_003F87_003F].Cells[0].Value.ToString());
		}
		if (text.Contains("{output}.*"))
		{
			text = (_003F88_003F.Contains("-f matroska - | ffplay -") ? text.Replace("\"{output}.*\"", _003F88_003F) : text.Replace("\"{output}.*", _003F88_003F));
		}
		else if (_003F88_003F.Contains("-f matroska - | ffplay -"))
		{
			string oldValue2 = text.Substring(text.IndexOf("\"{output}."), 13);
			text = text.Replace(oldValue2, _003F88_003F);
		}
		else
		{
			text = text.Replace("\"{output}", _003F88_003F + "\"");
		}
		_ffmpegHelper.RunCommand(_003F86_003F, text, _003F89_003F);
	}

	private void CbbAddVideoInput_SelectedIndexChanged(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void GrpCodeEditor_Enter(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void LblThread_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	protected override void Dispose(bool _003F84_003F)
	{
		if (_003F84_003F && _components != null)
		{
			_components.Dispose();
		}
		base.Dispose(_003F84_003F);
	}

	private void InitializeComponent()
	{
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
		tabControl = new TabControl();
		tabRender = new TabPage();
		dgvRender = new DataGridView();
		colVideoInput = new DataGridViewTextBoxColumn();
		colStatus = new DataGridViewTextBoxColumn();
		grpRenderSettings = new GroupBox();
		cbbAddVideoInput = new ComboBox();
		lblRenderInput = new Label();
		btnStop = new Button();
		btnStart = new Button();
		btnImportVideo = new Button();
		btnBrowseOutput = new Button();
		txtOutputRender = new TextBox();
		lblOutput = new Label();
		grpCodeEditor = new GroupBox();
		lblThread = new Label();
		numThread = new NumericUpDown();
		btnPreview = new Button();
		cbHideFfmpeg = new CheckBox();
		btnEditCode = new Button();
		btnReload = new Button();
		btnDelete = new Button();
		cbbFfmpegCode = new ComboBox();
		lblFfmpegCode = new Label();
		tabLiveStream = new TabPage();
		rtbInstructions = new RichTextBox();
		btLiveStop = new Button();
		btLiveRun = new Button();
		grpLiveSetting = new GroupBox();
		btnLivePreview = new Button();
		btnLiveRen10s = new Button();
		cbNonFilter = new CheckBox();
		numVolume = new NumericUpDown();
		lblVolume = new Label();
		lblSeparator = new Label();
		txtLogoLocation = new TextBox();
		lblLogoLocation = new Label();
		txtLogoSize = new TextBox();
		lblLogoSize = new Label();
		btLiveLogo = new Button();
		txtLiveLogo = new TextBox();
		cbLogo = new CheckBox();
		lblPreset = new Label();
		cbbPreset = new ComboBox();
		txtLiveSize = new TextBox();
		txtLiveBitrate = new TextBox();
		lblSize = new Label();
		lblBitrate = new Label();
		cbLiveLoop = new CheckBox();
		grpLiveInfo = new GroupBox();
		lnkGetLink = new LinkLabel();
		btLiveImage = new Button();
		txtLiveImage = new TextBox();
		lblImage = new Label();
		cbbOption = new ComboBox();
		txtLiveOutput = new TextBox();
		lblLiveOutput = new Label();
		btLiveInput = new Button();
		txtLiveInput = new TextBox();
		lblLiveInput = new Label();
		rbLiveTextList = new RadioButton();
		rbLiveFolder = new RadioButton();
		rbLiveFile = new RadioButton();
		tabControl.SuspendLayout();
		tabRender.SuspendLayout();
		((ISupportInitialize)dgvRender).BeginInit();
		grpRenderSettings.SuspendLayout();
		grpCodeEditor.SuspendLayout();
		((ISupportInitialize)numThread).BeginInit();
		tabLiveStream.SuspendLayout();
		grpLiveSetting.SuspendLayout();
		((ISupportInitialize)numVolume).BeginInit();
		grpLiveInfo.SuspendLayout();
		SuspendLayout();
		tabControl.Appearance = TabAppearance.Buttons;
		tabControl.Controls.Add(tabRender);
		tabControl.Controls.Add(tabLiveStream);
		tabControl.Location = new Point(-4, -3);
		tabControl.Multiline = true;
		tabControl.Name = "tabControl1";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(805, 545);
		tabControl.SizeMode = TabSizeMode.Fixed;
		tabControl.TabIndex = 11;
		tabRender.BackColor = Color.DarkGray;
		tabRender.BackgroundImageLayout = ImageLayout.Stretch;
		tabRender.Controls.Add(dgvRender);
		tabRender.Controls.Add(grpRenderSettings);
		tabRender.Controls.Add(grpCodeEditor);
		tabRender.Location = new Point(4, 25);
		tabRender.Name = "tabPage1";
		tabRender.Padding = new Padding(3);
		tabRender.Size = new Size(797, 516);
		tabRender.TabIndex = 0;
		tabRender.Text = "Render";
		tabRender.Click += TabRender_Click;
		dgvRender.AllowUserToAddRows = false;
		dgvRender.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvRender.Columns.AddRange(colVideoInput, colStatus);
		dgvRender.Location = new Point(6, 109);
		dgvRender.Name = "dgvRender";
		dgvRender.Size = new Size(777, 400);
		dgvRender.TabIndex = 2;
		colVideoInput.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		colVideoInput.HeaderText = "Video Input";
		colVideoInput.Name = "Column1";
		colVideoInput.ReadOnly = true;
		colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		colStatus.HeaderText = "Status";
		colStatus.Name = "Column2";
		colStatus.ReadOnly = true;
		colStatus.Width = 150;
		grpRenderSettings.Controls.Add(cbbAddVideoInput);
		grpRenderSettings.Controls.Add(lblRenderInput);
		grpRenderSettings.Controls.Add(btnStop);
		grpRenderSettings.Controls.Add(btnStart);
		grpRenderSettings.Controls.Add(btnImportVideo);
		grpRenderSettings.Controls.Add(btnBrowseOutput);
		grpRenderSettings.Controls.Add(txtOutputRender);
		grpRenderSettings.Controls.Add(lblOutput);
		grpRenderSettings.Location = new Point(6, 6);
		grpRenderSettings.Name = "groupBox2";
		grpRenderSettings.Size = new Size(335, 97);
		grpRenderSettings.TabIndex = 1;
		grpRenderSettings.TabStop = false;
		grpRenderSettings.Text = "Settings";
		cbbAddVideoInput.DropDownStyle = ComboBoxStyle.DropDownList;
		cbbAddVideoInput.FormattingEnabled = true;
		cbbAddVideoInput.Items.AddRange(new object[2]
		{
"File in Computer",
"Import Link Video"		});
		cbbAddVideoInput.Location = new Point(51, 13);
		cbbAddVideoInput.Name = "cbbAddVideoInput";
		cbbAddVideoInput.Size = new Size(161, 21);
		cbbAddVideoInput.TabIndex = 8;
		cbbAddVideoInput.SelectedIndexChanged += CbbAddVideoInput_SelectedIndexChanged;
		lblRenderInput.AutoSize = true;
		lblRenderInput.Location = new Point(5, 16);
		lblRenderInput.Name = "label12";
		lblRenderInput.Size = new Size(31, 13);
		lblRenderInput.TabIndex = 7;
		lblRenderInput.Text = "Input";
		btnStop.Location = new Point(218, 68);
		btnStop.Name = "button6";
		btnStop.Size = new Size(75, 23);
		btnStop.TabIndex = 6;
		btnStop.Text = "Stop";
		btnStop.UseVisualStyleBackColor = true;
		btnStop.Click += BtnStop_Click;
		btnStart.Location = new Point(137, 68);
		btnStart.Name = "button5";
		btnStart.Size = new Size(75, 23);
		btnStart.TabIndex = 5;
		btnStart.Text = "Start";
		btnStart.UseVisualStyleBackColor = true;
		btnStart.Click += BtnStart_Click;
		btnImportVideo.Location = new Point(218, 11);
		btnImportVideo.Name = "button3";
		btnImportVideo.Size = new Size(75, 23);
		btnImportVideo.TabIndex = 4;
		btnImportVideo.Text = "Import Video";
		btnImportVideo.UseVisualStyleBackColor = true;
		btnImportVideo.Click += BtnImportVideo_Click;
		btnBrowseOutput.Location = new Point(299, 40);
		btnBrowseOutput.Name = "button4";
		btnBrowseOutput.Size = new Size(28, 23);
		btnBrowseOutput.TabIndex = 3;
		btnBrowseOutput.Text = "...";
		btnBrowseOutput.UseVisualStyleBackColor = true;
		btnBrowseOutput.Click += BtnBrowseOutput_Click;
		txtOutputRender.Location = new Point(51, 42);
		txtOutputRender.Name = "txtOutputRender";
		txtOutputRender.Size = new Size(242, 20);
		txtOutputRender.TabIndex = 2;
		txtOutputRender.TextChanged += TxtOutputRender_TextChanged;
		lblOutput.AutoSize = true;
		lblOutput.Location = new Point(5, 45);
		lblOutput.Name = "label2";
		lblOutput.Size = new Size(39, 13);
		lblOutput.TabIndex = 1;
		lblOutput.Text = "Output";
		lblOutput.Click += LblOutput_Click;
		grpCodeEditor.Controls.Add(lblThread);
		grpCodeEditor.Controls.Add(numThread);
		grpCodeEditor.Controls.Add(btnPreview);
		grpCodeEditor.Controls.Add(cbHideFfmpeg);
		grpCodeEditor.Controls.Add(btnEditCode);
		grpCodeEditor.Controls.Add(btnReload);
		grpCodeEditor.Controls.Add(btnDelete);
		grpCodeEditor.Controls.Add(cbbFfmpegCode);
		grpCodeEditor.Controls.Add(lblFfmpegCode);
		grpCodeEditor.Location = new Point(347, 6);
		grpCodeEditor.Name = "groupBox1";
		grpCodeEditor.Size = new Size(436, 97);
		grpCodeEditor.TabIndex = 0;
		grpCodeEditor.TabStop = false;
		grpCodeEditor.Text = "Code Editor";
		grpCodeEditor.Enter += GrpCodeEditor_Enter;
		lblThread.AutoSize = true;
		lblThread.Location = new Point(242, 73);
		lblThread.Name = "label14";
		lblThread.Size = new Size(41, 13);
		lblThread.TabIndex = 9;
		lblThread.Text = "Thread";
		lblThread.Click += LblThread_Click;
		numThread.Location = new Point(289, 71);
		numThread.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		numThread.Name = "numThread";
		numThread.Size = new Size(41, 20);
		numThread.TabIndex = 8;
		numThread.TextAlign = HorizontalAlignment.Right;
		numThread.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		btnPreview.Location = new Point(355, 40);
		btnPreview.Name = "button8";
		btnPreview.Size = new Size(75, 23);
		btnPreview.TabIndex = 6;
		btnPreview.Text = "Preview";
		btnPreview.UseVisualStyleBackColor = true;
		btnPreview.Click += BtnPreview_Click;
		cbHideFfmpeg.AutoSize = true;
		cbHideFfmpeg.Location = new Point(336, 72);
		cbHideFfmpeg.Name = "cbHideFfmpeg";
		cbHideFfmpeg.Size = new Size(94, 17);
		cbHideFfmpeg.TabIndex = 5;
		cbHideFfmpeg.Text = "Hide FFMPEG";
		cbHideFfmpeg.UseVisualStyleBackColor = true;
		cbHideFfmpeg.CheckedChanged += CbHideFfmpeg_CheckedChanged;
		btnEditCode.Location = new Point(112, 40);
		btnEditCode.Name = "button7";
		btnEditCode.Size = new Size(75, 23);
		btnEditCode.TabIndex = 4;
		btnEditCode.Text = "Edit Code";
		btnEditCode.UseVisualStyleBackColor = true;
		btnEditCode.Click += BtnEditCode_Click;
		btnReload.Location = new Point(274, 40);
		btnReload.Name = "button2";
		btnReload.Size = new Size(75, 23);
		btnReload.TabIndex = 3;
		btnReload.Text = "Reload";
		btnReload.UseVisualStyleBackColor = true;
		btnReload.Click += BtnReload_Click;
		btnDelete.Location = new Point(193, 40);
		btnDelete.Name = "button1";
		btnDelete.Size = new Size(75, 23);
		btnDelete.TabIndex = 2;
		btnDelete.Text = "Delete";
		btnDelete.UseVisualStyleBackColor = true;
		btnDelete.Click += BtnDelete_Click;
		cbbFfmpegCode.DropDownStyle = ComboBoxStyle.DropDownList;
		cbbFfmpegCode.FormattingEnabled = true;
		cbbFfmpegCode.Location = new Point(92, 13);
		cbbFfmpegCode.Name = "cbbFfmpegCode";
		cbbFfmpegCode.Size = new Size(338, 21);
		cbbFfmpegCode.TabIndex = 1;
		lblFfmpegCode.AutoSize = true;
		lblFfmpegCode.Location = new Point(8, 16);
		lblFfmpegCode.Name = "label1";
		lblFfmpegCode.Size = new Size(78, 13);
		lblFfmpegCode.TabIndex = 0;
		lblFfmpegCode.Text = "FFMPEG Code";
		tabLiveStream.BackColor = Color.DarkGray;
		tabLiveStream.BackgroundImageLayout = ImageLayout.Stretch;
		tabLiveStream.Controls.Add(rtbInstructions);
		tabLiveStream.Controls.Add(btLiveStop);
		tabLiveStream.Controls.Add(btLiveRun);
		tabLiveStream.Controls.Add(grpLiveSetting);
		tabLiveStream.Controls.Add(grpLiveInfo);
		tabLiveStream.Location = new Point(4, 25);
		tabLiveStream.Name = "tabPage2";
		tabLiveStream.Padding = new Padding(3);
		tabLiveStream.Size = new Size(797, 516);
		tabLiveStream.TabIndex = 1;
		tabLiveStream.Text = "Live Stream";
		tabLiveStream.Click += TabLiveStream_Click;
		rtbInstructions.Location = new Point(278, 6);
		rtbInstructions.Name = "richTextBox1";
		rtbInstructions.Size = new Size(505, 294);
		rtbInstructions.TabIndex = 7;
		rtbInstructions.Text = componentResourceManager.GetString("richTextBox1.Text");
		rtbInstructions.TextChanged += RtbInstructions_TextChanged;
		btLiveStop.Location = new Point(156, 475);
		btLiveStop.Name = "btLiveStop";
		btLiveStop.Size = new Size(72, 35);
		btLiveStop.TabIndex = 3;
		btLiveStop.Text = "Stop All";
		btLiveStop.UseVisualStyleBackColor = true;
		btLiveStop.Click += BtLiveStop_Click;
		btLiveRun.Location = new Point(39, 475);
		btLiveRun.Name = "btLiveRun";
		btLiveRun.Size = new Size(72, 35);
		btLiveRun.TabIndex = 2;
		btLiveRun.Text = "Run";
		btLiveRun.UseVisualStyleBackColor = true;
		btLiveRun.Click += BtLiveRun_Click;
		grpLiveSetting.Controls.Add(btnLivePreview);
		grpLiveSetting.Controls.Add(btnLiveRen10s);
		grpLiveSetting.Controls.Add(cbNonFilter);
		grpLiveSetting.Controls.Add(numVolume);
		grpLiveSetting.Controls.Add(lblVolume);
		grpLiveSetting.Controls.Add(lblSeparator);
		grpLiveSetting.Controls.Add(txtLogoLocation);
		grpLiveSetting.Controls.Add(lblLogoLocation);
		grpLiveSetting.Controls.Add(txtLogoSize);
		grpLiveSetting.Controls.Add(lblLogoSize);
		grpLiveSetting.Controls.Add(btLiveLogo);
		grpLiveSetting.Controls.Add(txtLiveLogo);
		grpLiveSetting.Controls.Add(cbLogo);
		grpLiveSetting.Controls.Add(lblPreset);
		grpLiveSetting.Controls.Add(cbbPreset);
		grpLiveSetting.Controls.Add(txtLiveSize);
		grpLiveSetting.Controls.Add(txtLiveBitrate);
		grpLiveSetting.Controls.Add(lblSize);
		grpLiveSetting.Controls.Add(lblBitrate);
		grpLiveSetting.Controls.Add(cbLiveLoop);
		grpLiveSetting.Location = new Point(12, 217);
		grpLiveSetting.Name = "groupBox4";
		grpLiveSetting.Size = new Size(256, 227);
		grpLiveSetting.TabIndex = 1;
		grpLiveSetting.TabStop = false;
		grpLiveSetting.Text = "Setting";
		btnLivePreview.Location = new Point(6, 190);
		btnLivePreview.Name = "btnLivePreview";
		btnLivePreview.Size = new Size(75, 23);
		btnLivePreview.TabIndex = 8;
		btnLivePreview.Text = "Preview";
		btnLivePreview.UseVisualStyleBackColor = true;
		btnLivePreview.Click += BtnLivePreview_Click;
		btnLiveRen10s.Location = new Point(6, 160);
		btnLiveRen10s.Name = "btnLiveRen10s";
		btnLiveRen10s.Size = new Size(75, 23);
		btnLiveRen10s.TabIndex = 9;
		btnLiveRen10s.Text = "Render 10s";
		btnLiveRen10s.UseVisualStyleBackColor = true;
		btnLiveRen10s.Click += BtnLiveRen10s_Click;
		cbNonFilter.AutoSize = true;
		cbNonFilter.Location = new Point(7, 137);
		cbNonFilter.Name = "cbNonFilter";
		cbNonFilter.Size = new Size(71, 17);
		cbNonFilter.TabIndex = 17;
		cbNonFilter.Text = "Non Filter";
		cbNonFilter.UseVisualStyleBackColor = true;
		cbNonFilter.CheckedChanged += CbNonFilter_CheckedChanged;
		numVolume.Location = new Point(169, 193);
		numVolume.Name = "numVolume";
		numVolume.Size = new Size(78, 20);
		numVolume.TabIndex = 16;
		numVolume.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		lblVolume.AutoSize = true;
		lblVolume.Location = new Point(125, 195);
		lblVolume.Name = "label13";
		lblVolume.Size = new Size(42, 13);
		lblVolume.TabIndex = 15;
		lblVolume.Text = "Volume";
		lblSeparator.AutoSize = true;
		lblSeparator.Location = new Point(7, 83);
		lblSeparator.Name = "label10";
		lblSeparator.Size = new Size(241, 13);
		lblSeparator.TabIndex = 14;
		lblSeparator.Text = "_______________________________________";
		txtLogoLocation.Enabled = false;
		txtLogoLocation.Location = new Point(168, 67);
		txtLogoLocation.Name = "txtLogoLocation";
		txtLogoLocation.Size = new Size(79, 20);
		txtLogoLocation.TabIndex = 13;
		txtLogoLocation.Text = "20:20";
		lblLogoLocation.AutoSize = true;
		lblLogoLocation.Location = new Point(114, 70);
		lblLogoLocation.Name = "label9";
		lblLogoLocation.Size = new Size(48, 13);
		lblLogoLocation.TabIndex = 12;
		lblLogoLocation.Text = "Location";
		txtLogoSize.Enabled = false;
		txtLogoSize.Location = new Point(36, 67);
		txtLogoSize.Name = "txtLogoSize";
		txtLogoSize.Size = new Size(59, 20);
		txtLogoSize.TabIndex = 11;
		txtLogoSize.Text = "70x70";
		lblLogoSize.AutoSize = true;
		lblLogoSize.Location = new Point(3, 70);
		lblLogoSize.Name = "label8";
		lblLogoSize.Size = new Size(27, 13);
		lblLogoSize.TabIndex = 10;
		lblLogoSize.Text = "Size";
		btLiveLogo.Enabled = false;
		btLiveLogo.Location = new Point(222, 41);
		btLiveLogo.Name = "btLiveLogo";
		btLiveLogo.Size = new Size(25, 23);
		btLiveLogo.TabIndex = 8;
		btLiveLogo.Text = "...";
		btLiveLogo.UseVisualStyleBackColor = true;
		btLiveLogo.Click += BtLiveLogo_Click;
		txtLiveLogo.Enabled = false;
		txtLiveLogo.Location = new Point(6, 43);
		txtLiveLogo.Name = "txtLiveLogo";
		txtLiveLogo.Size = new Size(210, 20);
		txtLiveLogo.TabIndex = 9;
		cbLogo.AutoSize = true;
		cbLogo.Location = new Point(6, 19);
		cbLogo.Name = "cbLogo";
		cbLogo.Size = new Size(72, 17);
		cbLogo.TabIndex = 8;
		cbLogo.Text = "Add Logo";
		cbLogo.UseVisualStyleBackColor = true;
		cbLogo.CheckedChanged += CbLogo_CheckedChanged;
		lblPreset.AutoSize = true;
		lblPreset.Location = new Point(125, 164);
		lblPreset.Name = "label7";
		lblPreset.Size = new Size(37, 13);
		lblPreset.TabIndex = 7;
		lblPreset.Text = "Preset";
		cbbPreset.DropDownStyle = ComboBoxStyle.DropDownList;
		cbbPreset.FormattingEnabled = true;
		cbbPreset.Items.AddRange(new object[9]
		{
"Very Slow",
"Slower",
"Slow",
"Medium",
"Fast",
"Faster",
"Very Fast",
"Super Fast",
"Ultra Fast"		});
		cbbPreset.Location = new Point(168, 161);
		cbbPreset.Name = "cbbPreset";
		cbbPreset.Size = new Size(79, 21);
		cbbPreset.TabIndex = 6;
		cbbPreset.SelectedIndexChanged += CbbPreset_SelectedIndexChanged;
		txtLiveSize.Location = new Point(168, 135);
		txtLiveSize.Name = "txtLiveSize";
		txtLiveSize.Size = new Size(79, 20);
		txtLiveSize.TabIndex = 5;
		txtLiveSize.Text = "1280x720";
		txtLiveBitrate.Location = new Point(168, 110);
		txtLiveBitrate.Name = "txtLiveBitrate";
		txtLiveBitrate.Size = new Size(79, 20);
		txtLiveBitrate.TabIndex = 4;
		txtLiveBitrate.Text = "2500k";
		lblSize.AutoSize = true;
		lblSize.Location = new Point(125, 137);
		lblSize.Name = "label6";
		lblSize.Size = new Size(27, 13);
		lblSize.TabIndex = 3;
		lblSize.Text = "Size";
		lblBitrate.AutoSize = true;
		lblBitrate.Location = new Point(125, 113);
		lblBitrate.Name = "label5";
		lblBitrate.Size = new Size(37, 13);
		lblBitrate.TabIndex = 2;
		lblBitrate.Text = "Bitrate";
		cbLiveLoop.AutoSize = true;
		cbLiveLoop.Location = new Point(7, 113);
		cbLiveLoop.Name = "cbLiveLoop";
		cbLiveLoop.Size = new Size(92, 17);
		cbLiveLoop.TabIndex = 0;
		cbLiveLoop.Text = "Loop Infinitive";
		cbLiveLoop.UseVisualStyleBackColor = true;
		grpLiveInfo.Controls.Add(lnkGetLink);
		grpLiveInfo.Controls.Add(btLiveImage);
		grpLiveInfo.Controls.Add(txtLiveImage);
		grpLiveInfo.Controls.Add(lblImage);
		grpLiveInfo.Controls.Add(cbbOption);
		grpLiveInfo.Controls.Add(txtLiveOutput);
		grpLiveInfo.Controls.Add(lblLiveOutput);
		grpLiveInfo.Controls.Add(btLiveInput);
		grpLiveInfo.Controls.Add(txtLiveInput);
		grpLiveInfo.Controls.Add(lblLiveInput);
		grpLiveInfo.Controls.Add(rbLiveTextList);
		grpLiveInfo.Controls.Add(rbLiveFolder);
		grpLiveInfo.Controls.Add(rbLiveFile);
		grpLiveInfo.Location = new Point(12, 6);
		grpLiveInfo.Name = "groupBox3";
		grpLiveInfo.Size = new Size(256, 205);
		grpLiveInfo.TabIndex = 0;
		grpLiveInfo.TabStop = false;
		grpLiveInfo.Text = "Information";
		lnkGetLink.AutoSize = true;
		lnkGetLink.LinkColor = Color.Red;
		lnkGetLink.Location = new Point(166, 115);
		lnkGetLink.Name = "linkLabel2";
		lnkGetLink.Size = new Size(50, 13);
		lnkGetLink.TabIndex = 12;
		lnkGetLink.TabStop = true;
		lnkGetLink.Text = "Get Link!";
		lnkGetLink.LinkClicked += LnkGetLink_LinkClicked;
		btLiveImage.Enabled = false;
		btLiveImage.Location = new Point(223, 88);
		btLiveImage.Name = "btLiveImage";
		btLiveImage.Size = new Size(25, 23);
		btLiveImage.TabIndex = 11;
		btLiveImage.Text = "...";
		btLiveImage.UseVisualStyleBackColor = true;
		btLiveImage.Click += BtLiveImage_Click;
		txtLiveImage.Enabled = false;
		txtLiveImage.Location = new Point(7, 90);
		txtLiveImage.Name = "txtLiveImage";
		txtLiveImage.Size = new Size(209, 20);
		txtLiveImage.TabIndex = 10;
		lblImage.AutoSize = true;
		lblImage.Location = new Point(4, 74);
		lblImage.Name = "label11";
		lblImage.Size = new Size(36, 13);
		lblImage.TabIndex = 9;
		lblImage.Text = "Image";
		cbbOption.DropDownStyle = ComboBoxStyle.DropDownList;
		cbbOption.FormattingEnabled = true;
		cbbOption.Items.AddRange(new object[2]
		{
"Live Stream From Video and Link",
"Live Stream With Image and MP3"		});
		cbbOption.Location = new Point(6, 19);
		cbbOption.Name = "cbbOption";
		cbbOption.Size = new Size(241, 21);
		cbbOption.TabIndex = 8;
		cbbOption.SelectedIndexChanged += CbbOption_SelectedIndexChanged;
		txtLiveOutput.Location = new Point(6, 175);
		txtLiveOutput.Name = "txtLiveOutput";
		txtLiveOutput.Size = new Size(240, 20);
		txtLiveOutput.TabIndex = 7;
		txtLiveOutput.Text = "rtmp://a.rtmp.youtube.com/live2/";
		lblLiveOutput.AutoSize = true;
		lblLiveOutput.Location = new Point(3, 159);
		lblLiveOutput.Name = "label4";
		lblLiveOutput.Size = new Size(39, 13);
		lblLiveOutput.TabIndex = 6;
		lblLiveOutput.Text = "Output";
		btLiveInput.Location = new Point(222, 129);
		btLiveInput.Name = "btLiveInput";
		btLiveInput.Size = new Size(25, 23);
		btLiveInput.TabIndex = 5;
		btLiveInput.Text = "...";
		btLiveInput.UseVisualStyleBackColor = true;
		btLiveInput.Click += BtLiveInput_Click;
		txtLiveInput.Location = new Point(6, 131);
		txtLiveInput.Name = "txtLiveInput";
		txtLiveInput.Size = new Size(209, 20);
		txtLiveInput.TabIndex = 4;
		lblLiveInput.AutoSize = true;
		lblLiveInput.Location = new Point(4, 115);
		lblLiveInput.Name = "label3";
		lblLiveInput.Size = new Size(31, 13);
		lblLiveInput.TabIndex = 3;
		lblLiveInput.Text = "Input";
		rbLiveTextList.AutoSize = true;
		rbLiveTextList.Location = new Point(181, 52);
		rbLiveTextList.Name = "rbLiveTextList";
		rbLiveTextList.Size = new Size(65, 17);
		rbLiveTextList.TabIndex = 2;
		rbLiveTextList.TabStop = true;
		rbLiveTextList.Text = "Text List";
		rbLiveTextList.UseVisualStyleBackColor = true;
		rbLiveTextList.CheckedChanged += RbLive_CheckedChanged;
		rbLiveFolder.AutoSize = true;
		rbLiveFolder.Location = new Point(101, 52);
		rbLiveFolder.Name = "rbLiveFolder";
		rbLiveFolder.Size = new Size(54, 17);
		rbLiveFolder.TabIndex = 1;
		rbLiveFolder.TabStop = true;
		rbLiveFolder.Text = "Folder";
		rbLiveFolder.UseVisualStyleBackColor = true;
		rbLiveFile.AutoSize = true;
		rbLiveFile.Checked = true;
		rbLiveFile.Location = new Point(6, 52);
		rbLiveFile.Name = "rbLiveFile";
		rbLiveFile.Size = new Size(76, 17);
		rbLiveFile.TabIndex = 0;
		rbLiveFile.TabStop = true;
		rbLiveFile.Text = "File or Link";
		rbLiveFile.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new SizeF(6f, 13f);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.ClientSize = new Size(795, 537);
		base.Controls.Add(tabControl);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "MainForm";
		Text = "Youtube Zenni Tool (Open Source)";
		base.Load += MainForm_Load;
		tabControl.ResumeLayout(performLayout: false);
		tabRender.ResumeLayout(performLayout: false);
		((ISupportInitialize)dgvRender).EndInit();
		grpRenderSettings.ResumeLayout(performLayout: false);
		grpRenderSettings.PerformLayout();
		grpCodeEditor.ResumeLayout(performLayout: false);
		grpCodeEditor.PerformLayout();
		((ISupportInitialize)numThread).EndInit();
		tabLiveStream.ResumeLayout(performLayout: false);
		tabLiveStream.PerformLayout();
		grpLiveSetting.ResumeLayout(performLayout: false);
		grpLiveSetting.PerformLayout();
		((ISupportInitialize)numVolume).EndInit();
		grpLiveInfo.ResumeLayout(performLayout: false);
		grpLiveInfo.PerformLayout();
		ResumeLayout(performLayout: false);
	}

	[CompilerGenerated]
	private void _003F67_003F_002E_003F68_003F()
	{
		RenderSingleItem(0);
	}

	[CompilerGenerated]
	private void _003F69_003F()
	{
		_003F13_003F CS_0024_003C_003E8__locals5 = new _003F13_003F();
		CS_0024_003C_003E8__locals5._003F179_003F = this;
		CS_0024_003C_003E8__locals5._003F178_003F = 0;
		while (CS_0024_003C_003E8__locals5._003F178_003F < dgvRender.Rows.Count)
		{
			Process[] processesByName = Process.GetProcessesByName("ffmpeg");
			int num = processesByName.Length;
			Thread.Sleep(100);
			if ((decimal)num < numThread.Value)
			{
				Thread thread = new Thread((ThreadStart)delegate
				{
					CS_0024_003C_003E8__locals5._003F179_003F.RenderSingleItem(CS_0024_003C_003E8__locals5._003F178_003F++);
				});
				Thread.Sleep(100);
				thread.Start();
			}
			Thread.Sleep(500);
		}
	}

	[CompilerGenerated]
	private void _003F70_003F()
	{
		int rowIndex = dgvRender.CurrentCell.RowIndex;
		RunFfmpegCommand(_003F86_003F: false, rowIndex, "-f matroska - | ffplay -", "/k");
	}
}
