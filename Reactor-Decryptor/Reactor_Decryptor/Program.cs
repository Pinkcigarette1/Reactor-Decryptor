using System;
using System.Windows.Forms;

namespace Reactor_Decryptor
{
	// Token: 0x02000032 RID: 50
	internal sealed class Program
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000BBD8 File Offset: 0x0000ABD8
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
