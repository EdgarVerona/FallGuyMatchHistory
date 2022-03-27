
namespace TeamTrackMatchHistory
{
	partial class FrmSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtParticipantsPath = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtPlayerLogPath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtThreshold = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(137, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Path to Participants CSV:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(149, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(440, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "(Must be a file with a CSV that has 2 columns: \"Epic Gamertag\" and \"Team Name\")";
			// 
			// txtParticipantsPath
			// 
			this.txtParticipantsPath.Location = new System.Drawing.Point(149, 39);
			this.txtParticipantsPath.Name = "txtParticipantsPath";
			this.txtParticipantsPath.Size = new System.Drawing.Size(440, 23);
			this.txtParticipantsPath.TabIndex = 2;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(595, 39);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 3;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtPlayerLogPath
			// 
			this.txtPlayerLogPath.Location = new System.Drawing.Point(149, 104);
			this.txtPlayerLogPath.Name = "txtPlayerLogPath";
			this.txtPlayerLogPath.Size = new System.Drawing.Size(440, 23);
			this.txtPlayerLogPath.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(40, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(103, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "Path to Player.log:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(149, 130);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(339, 15);
			this.label4.TabIndex = 6;
			this.label4.Text = "(You almost certainly should leave this alone, but just in case...)";
			// 
			// txtThreshold
			// 
			this.txtThreshold.Location = new System.Drawing.Point(149, 172);
			this.txtThreshold.Name = "txtThreshold";
			this.txtThreshold.Size = new System.Drawing.Size(109, 23);
			this.txtThreshold.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(40, 175);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 15);
			this.label5.TabIndex = 7;
			this.label5.Text = "Threshold for Ties:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(264, 180);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(23, 15);
			this.label6.TabIndex = 9;
			this.label6.Text = "ms";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(149, 198);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(454, 38);
			this.label7.TabIndex = 10;
			this.label7.Text = "To account for players losing all at once, or client authoritative inconsistencie" +
    "s, players that lose within this time range of each other are considered to have" +
    " \"tied\")";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(276, 271);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(129, 43);
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// FrmSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(688, 339);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtThreshold);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPlayerLogPath);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtParticipantsPath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtParticipantsPath;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtPlayerLogPath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtThreshold;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnSave;
	}
}