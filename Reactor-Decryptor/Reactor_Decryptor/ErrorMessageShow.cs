using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Reactor_Decryptor
{
	// Token: 0x02000006 RID: 6
	public partial class ErrorMessageShow : Form
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00003AB1 File Offset: 0x00002AB1
		public ErrorMessageShow(string inputmessage)
		{
			this.errorstring = inputmessage;
			this.InitializeComponent();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003AD1 File Offset: 0x00002AD1
		private void ErrorMessageShowShown(object sender, EventArgs e)
		{
			this.textBox1.Text = this.errorstring;
		}

		// Token: 0x04000037 RID: 55
		public string errorstring;
	}
}
