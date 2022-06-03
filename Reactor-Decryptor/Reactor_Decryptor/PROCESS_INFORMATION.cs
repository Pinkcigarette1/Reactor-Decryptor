using System;

namespace Reactor_Decryptor
{
	// Token: 0x0200000C RID: 12
	public struct PROCESS_INFORMATION
	{
		// Token: 0x0400007D RID: 125
		public IntPtr hProcess;

		// Token: 0x0400007E RID: 126
		public IntPtr hThread;

		// Token: 0x0400007F RID: 127
		public uint dwProcessId;

		// Token: 0x04000080 RID: 128
		public uint dwThreadId;
	}
}
