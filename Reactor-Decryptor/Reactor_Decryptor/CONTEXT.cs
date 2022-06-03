using System;
using System.Runtime.InteropServices;

namespace Reactor_Decryptor
{
	// Token: 0x02000007 RID: 7
	public struct CONTEXT
	{
		// Token: 0x0400003A RID: 58
		public uint ContextFlags;

		// Token: 0x0400003B RID: 59
		public uint Dr0;

		// Token: 0x0400003C RID: 60
		public uint Dr1;

		// Token: 0x0400003D RID: 61
		public uint Dr2;

		// Token: 0x0400003E RID: 62
		public uint Dr3;

		// Token: 0x0400003F RID: 63
		public uint Dr6;

		// Token: 0x04000040 RID: 64
		public uint Dr7;

		// Token: 0x04000041 RID: 65
		[MarshalAs(UnmanagedType.Struct)]
		public FLOATING_SAVE_AREA FloatSave;

		// Token: 0x04000042 RID: 66
		public uint SegGs;

		// Token: 0x04000043 RID: 67
		public uint SegFs;

		// Token: 0x04000044 RID: 68
		public uint SegEs;

		// Token: 0x04000045 RID: 69
		public uint SegDs;

		// Token: 0x04000046 RID: 70
		public uint Edi;

		// Token: 0x04000047 RID: 71
		public uint Esi;

		// Token: 0x04000048 RID: 72
		public uint Ebx;

		// Token: 0x04000049 RID: 73
		public uint Edx;

		// Token: 0x0400004A RID: 74
		public uint Ecx;

		// Token: 0x0400004B RID: 75
		public uint Eax;

		// Token: 0x0400004C RID: 76
		public uint Ebp;

		// Token: 0x0400004D RID: 77
		public uint Eip;

		// Token: 0x0400004E RID: 78
		public uint SegCs;

		// Token: 0x0400004F RID: 79
		public uint EFlags;

		// Token: 0x04000050 RID: 80
		public uint Esp;

		// Token: 0x04000051 RID: 81
		public uint SegSs;

		// Token: 0x04000052 RID: 82
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public byte[] ExtendedRegisters;
	}
}
