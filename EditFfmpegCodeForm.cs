using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YoutubeZenniTool;

public class EditFfmpegCodeForm : Form
{
	private string _filePath = "";

	private IContainer _components = null;

	private RichTextBox mainCode;

	private Button btnSave;

	private Button btnSaveAs;

	public EditFfmpegCodeForm()
	{
		InitializeComponent();
	}

	public EditFfmpegCodeForm(string _003F90_003F)
		: this()
	{
		try
		{
			_filePath = _003F90_003F;
			mainCode.Text = File.ReadAllText(_003F90_003F);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void EditFfmpegCodeForm_Load(object _003F82_003F, EventArgs _003F83_003F)
	{
	}

	private void BtnSaveAs_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		if (saveFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		try
		{
			File.WriteAllText(saveFileDialog.FileName, mainCode.Text);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void BtnSave_Click(object _003F82_003F, EventArgs _003F83_003F)
	{
		try
		{
			File.WriteAllText(_filePath, mainCode.Text);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
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
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EditFfmpegCodeForm));
		mainCode = new RichTextBox();
		btnSave = new Button();
		btnSaveAs = new Button();
		SuspendLayout();
		mainCode.BackColor = Color.Black;
		mainCode.Dock = DockStyle.Top;
		mainCode.Font = new Font("Comic Sans MS", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
		mainCode.ForeColor = Color.White;
		mainCode.Location = new Point(0, 0);
		mainCode.Name = "mainCode";
		mainCode.Size = new Size(733, 329);
		mainCode.TabIndex = 0;
		mainCode.Text = "";
		btnSave.Location = new Point(565, 335);
		btnSave.Name = "button2";
		btnSave.Size = new Size(75, 23);
		btnSave.TabIndex = 2;
		btnSave.Text = "Save";
		btnSave.UseVisualStyleBackColor = true;
		btnSave.Click += BtnSave_Click;
		btnSaveAs.Location = new Point(646, 335);
		btnSaveAs.Name = "button1";
		btnSaveAs.Size = new Size(75, 23);
		btnSaveAs.TabIndex = 1;
		btnSaveAs.Text = "Save As";
		btnSaveAs.UseVisualStyleBackColor = true;
		btnSaveAs.Click += BtnSaveAs_Click;
		base.AutoScaleDimensions = new SizeF(6f, 13f);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.ClientSize = new Size(733, 366);
		base.Controls.Add(btnSave);
		base.Controls.Add(btnSaveAs);
		base.Controls.Add(mainCode);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		base.Name = "EditFfmpegCode";
		Text = "Edit Ffmpeg Code";
		base.Load += EditFfmpegCodeForm_Load;
		ResumeLayout(performLayout: false);
	}
}
