using System;

namespace Reactor_Decryptor
{
	// Token: 0x0200000D RID: 13
	public struct STARTUPINFO2
	{
		// Token: 0x04000081 RID: 129
		public uint cb;

		// Token: 0x04000082 RID: 130
		public string lpReserved;

		// Token: 0x04000083 RID: 131
		public string lpDesktop;

		// Token: 0x04000084 RID: 132
		public string lpTitle;

		// Token: 0x04000085 RID: 133
		public uint dwX;

		// Token: 0x04000086 RID: 134
		public uint dwY;

		// Token: 0x04000087 RID: 135
		public uint dwXSize;

		// Token: 0x04000088 RID: 136
		public uint dwYSize;

		// Token: 0x04000089 RID: 137
		public uint dwXCountChars;

		// Token: 0x0400008A RID: 138
		public uint dwYCountChars;

		// Token: 0x0400008B RID: 139
		public uint dwFillAttribute;

		// Token: 0x0400008C RID: 140
		public uint dwFlags;

		// Token: 0x0400008D RID: 141
		public short wShowWindow;

		// Token: 0x0400008E RID: 142
		public short cbReserved2;

		// Token: 0x0400008F RID: 143
		public IntPtr lpReserved2;

		// Token: 0x04000090 RID: 144
		public IntPtr hStdInput;

		// Token: 0x04000091 RID: 145
		public IntPtr hStdOutput;

		// Token: 0x04000092 RID: 146
		public IntPtr hStdError;
	}
}
