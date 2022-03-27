
namespace TeamTrackMatchHistory
{
	partial class FrmMain
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnPlay = new System.Windows.Forms.ToolStripButton();
			this.btnPause = new System.Windows.Forms.ToolStripButton();
			this.btnParticipants = new System.Windows.Forms.ToolStripButton();
			this.btnSettings = new System.Windows.Forms.ToolStripButton();
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.dgShows = new System.Windows.Forms.DataGridView();
			this.splitDetails = new System.Windows.Forms.SplitContainer();
			this.bsShows = new System.Windows.Forms.BindingSource(this.components);
			this.groupDetail = new System.Windows.Forms.GroupBox();
			this.dgDetails = new System.Windows.Forms.DataGridView();
			this.grpErrors = new System.Windows.Forms.GroupBox();
			this.dgErrors = new System.Windows.Forms.DataGridView();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgShows)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitDetails)).BeginInit();
			this.splitDetails.Panel1.SuspendLayout();
			this.splitDetails.Panel2.SuspendLayout();
			this.splitDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bsShows)).BeginInit();
			this.groupDetail.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
			this.grpErrors.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgErrors)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
			this.statusStrip1.Location = new System.Drawing.Point(0, 590);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(921, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(163, 17);
			this.statusLabel.Text = "Press Start to begin tracking...";
			// 
			// progressBar
			// 
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(100, 16);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.Visible = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlay,
            this.btnPause,
            this.btnParticipants,
            this.btnSettings});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(921, 86);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnPlay
			// 
			this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
			this.btnPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(68, 83);
			this.btnPlay.Text = "Start";
			this.btnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// btnPause
			// 
			this.btnPause.Enabled = false;
			this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
			this.btnPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(68, 83);
			this.btnPause.Text = "Pause";
			this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnPause.ToolTipText = "Pause";
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnParticipants
			// 
			this.btnParticipants.Image = ((System.Drawing.Image)(resources.GetObject("btnParticipants.Image")));
			this.btnParticipants.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnParticipants.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnParticipants.Name = "btnParticipants";
			this.btnParticipants.Size = new System.Drawing.Size(73, 83);
			this.btnParticipants.Text = "Participants";
			this.btnParticipants.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnParticipants.Visible = false;
			this.btnParticipants.Click += new System.EventHandler(this.btnParticipants_Click);
			// 
			// btnSettings
			// 
			this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
			this.btnSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(68, 83);
			this.btnSettings.Text = "Settings";
			this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSettings.ToolTipText = "Settings";
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// splitMain
			// 
			this.splitMain.Cursor = System.Windows.Forms.Cursors.VSplit;
			this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitMain.Location = new System.Drawing.Point(0, 86);
			this.splitMain.Name = "splitMain";
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(this.dgShows);
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.splitDetails);
			this.splitMain.Size = new System.Drawing.Size(921, 504);
			this.splitMain.SplitterDistance = 347;
			this.splitMain.TabIndex = 2;
			// 
			// dgShows
			// 
			this.dgShows.AllowUserToAddRows = false;
			this.dgShows.AllowUserToDeleteRows = false;
			this.dgShows.AllowUserToResizeRows = false;
			this.dgShows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgShows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgShows.Location = new System.Drawing.Point(0, 0);
			this.dgShows.MultiSelect = false;
			this.dgShows.Name = "dgShows";
			this.dgShows.RowTemplate.Height = 25;
			this.dgShows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgShows.Size = new System.Drawing.Size(347, 504);
			this.dgShows.TabIndex = 0;
			// 
			// splitDetails
			// 
			this.splitDetails.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitDetails.Location = new System.Drawing.Point(0, 0);
			this.splitDetails.Name = "splitDetails";
			this.splitDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitDetails.Panel1
			// 
			this.splitDetails.Panel1.Controls.Add(this.groupDetail);
			// 
			// splitDetails.Panel2
			// 
			this.splitDetails.Panel2.Controls.Add(this.grpErrors);
			this.splitDetails.Size = new System.Drawing.Size(570, 504);
			this.splitDetails.SplitterDistance = 332;
			this.splitDetails.TabIndex = 0;
			// 
			// groupDetail
			// 
			this.groupDetail.Controls.Add(this.dgDetails);
			this.groupDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupDetail.Location = new System.Drawing.Point(0, 0);
			this.groupDetail.Name = "groupDetail";
			this.groupDetail.Size = new System.Drawing.Size(570, 332);
			this.groupDetail.TabIndex = 0;
			this.groupDetail.TabStop = false;
			this.groupDetail.Text = "Details";
			// 
			// dgDetails
			// 
			this.dgDetails.AllowUserToAddRows = false;
			this.dgDetails.AllowUserToDeleteRows = false;
			this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgDetails.Location = new System.Drawing.Point(3, 19);
			this.dgDetails.Name = "dgDetails";
			this.dgDetails.ReadOnly = true;
			this.dgDetails.RowTemplate.Height = 25;
			this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgDetails.Size = new System.Drawing.Size(564, 310);
			this.dgDetails.TabIndex = 0;
			// 
			// grpErrors
			// 
			this.grpErrors.Controls.Add(this.dgErrors);
			this.grpErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpErrors.Location = new System.Drawing.Point(0, 0);
			this.grpErrors.Name = "grpErrors";
			this.grpErrors.Size = new System.Drawing.Size(570, 168);
			this.grpErrors.TabIndex = 0;
			this.grpErrors.TabStop = false;
			this.grpErrors.Text = "Detail Errors";
			// 
			// dgErrors
			// 
			this.dgErrors.AllowUserToAddRows = false;
			this.dgErrors.AllowUserToDeleteRows = false;
			this.dgErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgErrors.Location = new System.Drawing.Point(3, 19);
			this.dgErrors.Name = "dgErrors";
			this.dgErrors.ReadOnly = true;
			this.dgErrors.RowTemplate.Height = 25;
			this.dgErrors.Size = new System.Drawing.Size(564, 146);
			this.dgErrors.TabIndex = 0;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(921, 612);
			this.Controls.Add(this.splitMain);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "FrmMain";
			this.Text = "FallGuy Team Track Match History";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
			this.splitMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgShows)).EndInit();
			this.splitDetails.Panel1.ResumeLayout(false);
			this.splitDetails.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitDetails)).EndInit();
			this.splitDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bsShows)).EndInit();
			this.groupDetail.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
			this.grpErrors.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgErrors)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnPlay;
		private System.Windows.Forms.ToolStripButton btnPause;
		private System.Windows.Forms.ToolStripButton btnParticipants;
		private System.Windows.Forms.ToolStripButton btnSettings;
		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.SplitContainer splitDetails;
		private System.Windows.Forms.DataGridView dgShows;
		private System.Windows.Forms.BindingSource bsShows;
		private System.Windows.Forms.GroupBox groupDetail;
		private System.Windows.Forms.DataGridView dgDetails;
		private System.Windows.Forms.GroupBox grpErrors;
		private System.Windows.Forms.DataGridView dgErrors;
	}
}

