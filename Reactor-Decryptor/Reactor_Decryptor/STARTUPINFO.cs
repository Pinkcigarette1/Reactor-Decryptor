using System;

namespace Reactor_Decryptor
{
	// Token: 0x0200000B RID: 11
	public struct STARTUPINFO
	{
		// Token: 0x0400006B RID: 107
		public int cb;

		// Token: 0x0400006C RID: 108
		public IntPtr lpReserved;

		// Token: 0x0400006D RID: 109
		public IntPtr lpDesktop;

		// Token: 0x0400006E RID: 110
		public IntPtr lpTitle;

		// Token: 0x0400006F RID: 111
		public int dwX;

		// Token: 0x04000070 RID: 112
		public int dwY;

		// Token: 0x04000071 RID: 113
		public int dwXSize;

		// Token: 0x04000072 RID: 114
		public int dwYSize;

		// Token: 0x04000073 RID: 115
		public int dwXCountChars;

		// Token: 0x04000074 RID: 116
		public int dwYCountChars;

		// Token: 0x04000075 RID: 117
		public int dwFillAttribute;

		// Token: 0x04000076 RID: 118
		public int dwFlags;

		// Token: 0x04000077 RID: 119
		public int wShowWindow;

		// Token: 0x04000078 RID: 120
		public int cbReserved2;

		// Token: 0x04000079 RID: 121
		public byte lpReserved2;

		// Token: 0x0400007A RID: 122
		public int hStdInput;

		// Token: 0x0400007B RID: 123
		public int hStdOutput;

		// Token: 0x0400007C RID: 124
		public int hStdError;
	}
}
