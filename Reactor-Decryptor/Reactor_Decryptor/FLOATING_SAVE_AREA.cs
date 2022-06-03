using System;
using System.Runtime.InteropServices;

namespace Reactor_Decryptor
{
	// Token: 0x02000009 RID: 9
	public struct FLOATING_SAVE_AREA
	{
		// Token: 0x0400005E RID: 94
		public uint ControlWord;

		// Token: 0x0400005F RID: 95
		public uint StatusWord;

		// Token: 0x04000060 RID: 96
		public uint TagWord;

		// Token: 0x04000061 RID: 97
		public uint ErrorOffset;

		// Token: 0x04000062 RID: 98
		public uint ErrorSelector;

		// Token: 0x04000063 RID: 99
		public uint DataOffset;

		// Token: 0x04000064 RID: 100
		public uint DataSelector;

		// Token: 0x04000065 RID: 101
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
		public byte[] RegisterArea;

		// Token: 0x04000066 RID: 102
		public uint Cr0NpxState;
	}
}
