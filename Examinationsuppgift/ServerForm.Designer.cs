namespace Examinationsuppgift
{
	partial class ServerForm
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
			this.btnStart = new System.Windows.Forms.Button();
			this.labelUsers = new System.Windows.Forms.Label();
			this.labelCards = new System.Windows.Forms.Label();
			this.boxUsers = new System.Windows.Forms.ListBox();
			this.boxCards = new System.Windows.Forms.ListBox();
			this.boxMsg = new System.Windows.Forms.TextBox();
			this.picGolden = new System.Windows.Forms.PictureBox();
			this.picCard = new System.Windows.Forms.PictureBox();
			this.picLogo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picGolden)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picCard)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(12, 432);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(79, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Starta Server";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// labelUsers
			// 
			this.labelUsers.AutoSize = true;
			this.labelUsers.Location = new System.Drawing.Point(9, 172);
			this.labelUsers.Name = "labelUsers";
			this.labelUsers.Size = new System.Drawing.Size(124, 13);
			this.labelUsers.TabIndex = 4;
			this.labelUsers.Text = "Registrerade användare:";
			// 
			// labelCards
			// 
			this.labelCards.AutoSize = true;
			this.labelCards.Location = new System.Drawing.Point(262, 172);
			this.labelCards.Name = "labelCards";
			this.labelCards.Size = new System.Drawing.Size(119, 13);
			this.labelCards.TabIndex = 5;
			this.labelCards.Text = "Giltiga kortserienummer:";
			// 
			// boxUsers
			// 
			this.boxUsers.FormattingEnabled = true;
			this.boxUsers.Location = new System.Drawing.Point(12, 188);
			this.boxUsers.Name = "boxUsers";
			this.boxUsers.Size = new System.Drawing.Size(246, 212);
			this.boxUsers.TabIndex = 6;
			// 
			// boxCards
			// 
			this.boxCards.FormattingEnabled = true;
			this.boxCards.Location = new System.Drawing.Point(265, 188);
			this.boxCards.Name = "boxCards";
			this.boxCards.Size = new System.Drawing.Size(193, 212);
			this.boxCards.TabIndex = 7;
			// 
			// boxMsg
			// 
			this.boxMsg.Location = new System.Drawing.Point(12, 406);
			this.boxMsg.Name = "boxMsg";
			this.boxMsg.ReadOnly = true;
			this.boxMsg.Size = new System.Drawing.Size(446, 20);
			this.boxMsg.TabIndex = 8;
			// 
			// picGolden
			// 
			this.picGolden.Location = new System.Drawing.Point(227, 57);
			this.picGolden.Name = "picGolden";
			this.picGolden.Size = new System.Drawing.Size(116, 108);
			this.picGolden.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picGolden.TabIndex = 9;
			this.picGolden.TabStop = false;
			// 
			// picCard
			// 
			this.picCard.Location = new System.Drawing.Point(349, 57);
			this.picCard.Name = "picCard";
			this.picCard.Size = new System.Drawing.Size(109, 108);
			this.picCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picCard.TabIndex = 10;
			this.picCard.TabStop = false;
			// 
			// picLogo
			// 
			this.picLogo.Location = new System.Drawing.Point(19, 16);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(202, 149);
			this.picLogo.TabIndex = 11;
			this.picLogo.TabStop = false;
			// 
			// ServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(478, 463);
			this.Controls.Add(this.picLogo);
			this.Controls.Add(this.picCard);
			this.Controls.Add(this.picGolden);
			this.Controls.Add(this.boxMsg);
			this.Controls.Add(this.boxCards);
			this.Controls.Add(this.boxUsers);
			this.Controls.Add(this.labelCards);
			this.Controls.Add(this.labelUsers);
			this.Controls.Add(this.btnStart);
			this.Name = "ServerForm";
			this.Text = "Databas";
			this.Load += new System.EventHandler(this.ServerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.picGolden)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picCard)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label labelUsers;
		private System.Windows.Forms.Label labelCards;
		private System.Windows.Forms.ListBox boxUsers;
		private System.Windows.Forms.ListBox boxCards;
		private System.Windows.Forms.TextBox boxMsg;
		private System.Windows.Forms.PictureBox picGolden;
		private System.Windows.Forms.PictureBox picCard;
		private System.Windows.Forms.PictureBox picLogo;
	}
}

