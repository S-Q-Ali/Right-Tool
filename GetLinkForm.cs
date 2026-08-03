using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace YoutubeZenniTool;

public class GetLinkForm : Form
{
	private string _linkResult = string.Empty;

	private IContainer _components = null;

	private RichTextBox txtOutput;

	private Button btnCopy;

	private Label lblUrl;

	private TextBox txtInput;

	private Button btnGetLink;

	private Button btnSaveTxt;

	public GetLinkForm()
	{
		InitializeComponent();
		base.MaximizeBox = false;
	}

	private void BtnCopy_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		if (txtOutput.Text != string.Empty)
		{
			Clipboard.SetText(txtOutput.Text);
		}
	}

	private void BtnGetLink_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		string text = ".\\bin\\youtube-dl -g -f best "+ txtInput.Text;
		_linkResult = string.Empty;
		string text2 = string.Empty;
		ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c "+ text);
		processStartInfo.RedirectStandardOutput = true;
		processStartInfo.RedirectStandardError = true;
		processStartInfo.CreateNoWindow = true;
		processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		processStartInfo.UseShellExecute = false;
		Process process = Process.Start(processStartInfo);
		using (StreamReader streamReader = process.StandardOutput)
		{
			_linkResult = streamReader.ReadToEnd();
		}
		using (StreamReader streamReader2 = process.StandardError)
		{
			text2 = streamReader2.ReadToEnd();
		}
		Console.WriteLine("The following output was detected:");
		txtOutput.Text = _linkResult;
		if (string.IsNullOrEmpty(_linkResult))
		{
			Console.WriteLine("The following error was detected:");
			MessageBox.Show(text2, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		MessageBox.Show("Get link completed!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void GetLinkForm_Load(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtnSaveTxt_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		string text = "";
		do
		{
			text = Interaction.InputBox("Enter name of file (*.txt)??? \n ex: filename.txt", "", "");
			while (File.Exists(text) || text.Equals("") || !text.Contains(".txt"))
			{
				text = ((!File.Exists(text)) ? ((!text.Equals("")) ? Interaction.InputBox("Please type a file name follow format \"filename.txt\"", "", "") : Interaction.InputBox("Please type a file name.", "", "")) : Interaction.InputBox("File is exist!\nPlease enter another name.", "", ""));
			}
		}
		while (File.Exists(text));
		File.WriteAllText(text, txtOutput.Text);
		MessageBox.Show("Save File Completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(GetLinkForm));
		txtOutput = new RichTextBox();
		btnCopy = new Button();
		lblUrl = new Label();
		txtInput = new TextBox();
		btnGetLink = new Button();
		btnSaveTxt = new Button();
		SuspendLayout();
		txtOutput.Location = new Point(49, 60);
		txtOutput.Name = "txtOutput";
		txtOutput.Size = new Size(324, 68);
		txtOutput.TabIndex = 0;
		txtOutput.Text = "";
		btnCopy.Location = new Point(379, 60);
		btnCopy.Name = "button1";
		btnCopy.Size = new Size(75, 27);
		btnCopy.TabIndex = 1;
		btnCopy.Text = "Copy";
		btnCopy.UseVisualStyleBackColor = true;
		btnCopy.Click += BtnCopy_Click;
		lblUrl.AutoSize = true;
		lblUrl.Location = new Point(13, 13);
		lblUrl.Name = "label1";
		lblUrl.Size = new Size(29, 13);
		lblUrl.TabIndex = 2;
		lblUrl.Text = "URL";
		txtInput.Location = new Point(49, 13);
		txtInput.Name = "txtInput";
		txtInput.Size = new Size(324, 20);
		txtInput.TabIndex = 3;
		btnGetLink.Location = new Point(379, 11);
		btnGetLink.Name = "button2";
		btnGetLink.Size = new Size(75, 23);
		btnGetLink.TabIndex = 4;
		btnGetLink.Text = "GetLink";
		btnGetLink.UseVisualStyleBackColor = true;
		btnGetLink.Click += BtnGetLink_Click;
		btnSaveTxt.Location = new Point(379, 102);
		btnSaveTxt.Name = "btnCreateFile";
		btnSaveTxt.Size = new Size(75, 26);
		btnSaveTxt.TabIndex = 5;
		btnSaveTxt.Text = "Save *.txt";
		btnSaveTxt.UseVisualStyleBackColor = true;
		btnSaveTxt.Click += BtnSaveTxt_Click;
		base.AutoScaleDimensions = new SizeF(6f, 13f);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.ClientSize = new Size(467, 152);
		base.Controls.Add(btnSaveTxt);
		base.Controls.Add(btnGetLink);
		base.Controls.Add(txtInput);
		base.Controls.Add(lblUrl);
		base.Controls.Add(btnCopy);
		base.Controls.Add(txtOutput);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		base.Name = "GetLink";
		Text = "Get Link for Input";
		base.Load += GetLinkForm_Load;
		ResumeLayout(performLayout: false);
		PerformLayout();
	}
}
