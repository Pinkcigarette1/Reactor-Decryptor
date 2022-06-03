namespace Reactor_Decryptor
{
	// Token: 0x02000006 RID: 6
	public partial class ErrorMessageShow : global::System.Windows.Forms.Form
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00003AE8 File Offset: 0x00002AE8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003B24 File Offset: 0x00002B24
		private void InitializeComponent()
		{
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.textBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBox1.Location = new global::System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(367, 266);
			this.textBox1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(367, 266);
			base.Controls.Add(this.textBox1);
			base.Name = "ErrorMessageShow";
			this.Text = "Error from thread:";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Shown += new global::System.EventHandler(this.ErrorMessageShowShown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000038 RID: 56
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.TextBox textBox1;
	}
}
