using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ProcessUtils
{
	// Token: 0x0200002B RID: 43
	public class ProcModule
	{
		// Token: 0x0600005A RID: 90
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(ProcModule.ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

		// Token: 0x0600005B RID: 91
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x0600005C RID: 92
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool CloseHandle(HandleRef handle);

		// Token: 0x0600005D RID: 93
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot([In] uint dwFlags, [In] uint th32ProcessID);

		// Token: 0x0600005E RID: 94
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool Process32First([In] IntPtr hSnapshot, ref ProcModule.PROCESSENTRY32 lppe);

		// Token: 0x0600005F RID: 95
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool Process32Next([In] IntPtr hSnapshot, ref ProcModule.PROCESSENTRY32 lppe);

		// Token: 0x06000060 RID: 96
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool Module32First(HandleRef handle, IntPtr entry);

		// Token: 0x06000061 RID: 97
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool Module32Next(HandleRef handle, IntPtr entry);

		// Token: 0x06000062 RID: 98 RVA: 0x0000B508 File Offset: 0x0000A508
		public static bool get_IsNt()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000B528 File Offset: 0x0000A528
		public static ProcModule.ModuleInfo[] WinGetModuleInfos(uint processId)
		{
			IntPtr intPtr = (IntPtr)(-1);
			GCHandle gCHandle = default(GCHandle);
			ArrayList arrayList = new ArrayList();
			checked
			{
				ProcModule.ModuleInfo[] result;
				try
				{
					intPtr = ProcModule.CreateToolhelp32Snapshot(8u, processId);
					if (intPtr == (IntPtr)(-1))
					{
						result = null;
						return result;
					}
					int num = Marshal.SizeOf(typeof(ProcModule.WinModuleEntry));
					int num2 = num + 260 + 256;
					int[] value = new int[num2 / 4];
					gCHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
					IntPtr intPtr2 = gCHandle.AddrOfPinnedObject();
					Marshal.WriteInt32(intPtr2, num2);
					HandleRef handle = new HandleRef(null, intPtr);
					if (ProcModule.Module32First(handle, intPtr2))
					{
						do
						{
							ProcModule.WinModuleEntry winModuleEntry = new ProcModule.WinModuleEntry();
							Marshal.PtrToStructure(intPtr2, winModuleEntry);
							arrayList.Add(new ProcModule.ModuleInfo
							{
								baseName = Marshal.PtrToStringAnsi((IntPtr)((long)intPtr2 + unchecked((long)num))),
								fileName = Marshal.PtrToStringAnsi((IntPtr)((long)intPtr2 + unchecked((long)num) + 256L)),
								baseOfDll = winModuleEntry.modBaseAddr,
								sizeOfImage = winModuleEntry.modBaseSize,
								Id = winModuleEntry.th32ModuleID
							});
							Marshal.WriteInt32(intPtr2, num2);
						}
						while (ProcModule.Module32Next(handle, intPtr2));
					}
				}
				finally
				{
					if (gCHandle.IsAllocated)
					{
						gCHandle.Free();
					}
					if (intPtr != (IntPtr)(-1))
					{
						ProcModule.CloseHandle(new HandleRef(null, intPtr));
					}
				}
				ProcModule.ModuleInfo[] array = new ProcModule.ModuleInfo[arrayList.Count];
				arrayList.CopyTo(array, 0);
				result = array;
				return result;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000B6FC File Offset: 0x0000A6FC
		public static ProcModule.ModuleInfo[] NtGetModuleInfos(int processId)
		{
			return ProcModule.NtGetModuleInfos(processId, false);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000B718 File Offset: 0x0000A718
		public static bool IsOSOlderThanXP()
		{
			return Environment.OSVersion.Version.Major < 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0);
		}

		// Token: 0x06000066 RID: 102
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr OpenProcess(ProcModule.ProcessAccess access, bool inherit, int processId);

		// Token: 0x06000067 RID: 103
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetCurrentProcessId();

		// Token: 0x06000068 RID: 104
		[DllImport("psapi.dll", CharSet = CharSet.Auto)]
		public static extern bool EnumProcessModules(IntPtr handle, IntPtr modules, int size, ref int needed);

		// Token: 0x06000069 RID: 105
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWow64Process([In] IntPtr hSourceProcessHandle, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);

		// Token: 0x0600006A RID: 106
		[DllImport("psapi.dll", CharSet = CharSet.Auto)]
		public static extern bool GetModuleInformation(IntPtr processHandle, HandleRef moduleHandle, ProcModule.NtModuleInfo ntModuleInfo, int size);

		// Token: 0x0600006B RID: 107
		[DllImport("psapi.dll", CharSet = CharSet.Auto)]
		public static extern int GetModuleBaseName(IntPtr processHandle, HandleRef moduleHandle, StringBuilder baseName, int size);

		// Token: 0x0600006C RID: 108
		[DllImport("psapi.dll", CharSet = CharSet.Auto)]
		public static extern int GetModuleFileNameEx(IntPtr processHandle, HandleRef moduleHandle, StringBuilder baseName, int size);

		// Token: 0x0600006D RID: 109 RVA: 0x0000B768 File Offset: 0x0000A768
		private static ProcModule.ModuleInfo[] NtGetModuleInfos(int processId, bool firstModuleOnly)
		{
			IntPtr intPtr = IntPtr.Zero;
			checked
			{
				ProcModule.ModuleInfo[] result;
				try
				{
					intPtr = ProcModule.OpenProcess((ProcModule.ProcessAccess)1040, false, processId);
					IntPtr[] array = new IntPtr[64];
					GCHandle gCHandle = default(GCHandle);
					int num = 0;
					while (true)
					{
						bool flag = false;
						try
						{
							gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
							flag = ProcModule.EnumProcessModules(intPtr, gCHandle.AddrOfPinnedObject(), array.Length * IntPtr.Size, ref num);
							if (!flag)
							{
								bool flag2 = false;
								bool flag3 = false;
								if (!ProcModule.IsOSOlderThanXP())
								{
									IntPtr intPtr2 = IntPtr.Zero;
									try
									{
										intPtr2 = ProcModule.OpenProcess(ProcModule.ProcessAccess.QueryInformation, true, ProcModule.GetCurrentProcessId());
										if (!ProcModule.IsWow64Process(intPtr2, out flag2))
										{
											result = null;
											return result;
										}
										if (!ProcModule.IsWow64Process(intPtr, out flag3))
										{
											result = null;
											return result;
										}
										if (flag2 && !flag3)
										{
											result = null;
											return result;
										}
									}
									finally
									{
										if (intPtr2 != IntPtr.Zero)
										{
											ProcModule.CloseHandle(intPtr2);
										}
									}
								}
								for (int i = 0; i < 50; i++)
								{
									flag = ProcModule.EnumProcessModules(intPtr, gCHandle.AddrOfPinnedObject(), array.Length * IntPtr.Size, ref num);
									if (flag)
									{
										break;
									}
									Thread.Sleep(1);
								}
							}
						}
						finally
						{
							gCHandle.Free();
						}
						if (!flag)
						{
							break;
						}
						num /= IntPtr.Size;
						if (num <= array.Length)
						{
							goto Block_5;
						}
						array = new IntPtr[array.Length * 2];
					}
					result = null;
					return result;
					Block_5:
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < num; i++)
					{
						ProcModule.ModuleInfo moduleInfo = new ProcModule.ModuleInfo();
						IntPtr handle = array[i];
						ProcModule.NtModuleInfo ntModuleInfo = new ProcModule.NtModuleInfo();
						if (!ProcModule.GetModuleInformation(intPtr, new HandleRef(null, handle), ntModuleInfo, Marshal.SizeOf(ntModuleInfo)))
						{
							result = null;
							return result;
						}
						moduleInfo.sizeOfImage = ntModuleInfo.SizeOfImage;
						moduleInfo.entryPoint = ntModuleInfo.EntryPoint;
						moduleInfo.baseOfDll = ntModuleInfo.BaseOfDll;
						StringBuilder stringBuilder = new StringBuilder(1024);
						int num2 = ProcModule.GetModuleBaseName(intPtr, new HandleRef(null, handle), stringBuilder, stringBuilder.Capacity * 2);
						if (num2 == 0)
						{
							result = null;
							return result;
						}
						moduleInfo.baseName = stringBuilder.ToString();
						StringBuilder stringBuilder2 = new StringBuilder(1024);
						num2 = ProcModule.GetModuleFileNameEx(intPtr, new HandleRef(null, handle), stringBuilder2, stringBuilder2.Capacity * 2);
						if (num2 == 0)
						{
							result = null;
							return result;
						}
						moduleInfo.fileName = stringBuilder2.ToString();
						if (string.Compare(moduleInfo.fileName, "\\SystemRoot\\System32\\smss.exe", StringComparison.OrdinalIgnoreCase) == 0)
						{
							moduleInfo.fileName = Path.Combine(Environment.SystemDirectory, "smss.exe");
						}
						if (moduleInfo.fileName != null && moduleInfo.fileName.Length >= 4 && moduleInfo.fileName.StartsWith("\\\\?\\", StringComparison.Ordinal))
						{
							moduleInfo.fileName = moduleInfo.fileName.Substring(4);
						}
						arrayList.Add(moduleInfo);
						if (firstModuleOnly)
						{
							break;
						}
					}
					ProcModule.ModuleInfo[] array2 = new ProcModule.ModuleInfo[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					result = array2;
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						ProcModule.CloseHandle(intPtr);
					}
				}
				return result;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000BB5C File Offset: 0x0000AB5C
		public static ProcModule.ModuleInfo[] GetModuleInfos(int processId)
		{
			ProcModule.ModuleInfo[] result;
			if (ProcModule.get_IsNt())
			{
				result = ProcModule.NtGetModuleInfos(processId);
			}
			else
			{
				result = ProcModule.WinGetModuleInfos(checked((uint)processId));
			}
			return result;
		}

		// Token: 0x0200002C RID: 44
		public enum ProcessAccess
		{
			// Token: 0x040001D9 RID: 473
			AllAccess = 1050235,
			// Token: 0x040001DA RID: 474
			CreateThread = 2,
			// Token: 0x040001DB RID: 475
			DuplicateHandle = 64,
			// Token: 0x040001DC RID: 476
			QueryInformation = 1024,
			// Token: 0x040001DD RID: 477
			SetInformation = 512,
			// Token: 0x040001DE RID: 478
			Terminate = 1,
			// Token: 0x040001DF RID: 479
			VMOperation = 8,
			// Token: 0x040001E0 RID: 480
			VMRead = 16,
			// Token: 0x040001E1 RID: 481
			VMWrite = 32,
			// Token: 0x040001E2 RID: 482
			Synchronize = 1048576
		}

		// Token: 0x0200002D RID: 45
		[Flags]
		private enum SnapshotFlags : uint
		{
			// Token: 0x040001E4 RID: 484
			HeapList = 1u,
			// Token: 0x040001E5 RID: 485
			Process = 2u,
			// Token: 0x040001E6 RID: 486
			Thread = 4u,
			// Token: 0x040001E7 RID: 487
			Module = 8u,
			// Token: 0x040001E8 RID: 488
			Module32 = 16u,
			// Token: 0x040001E9 RID: 489
			Inherit = 2147483648u,
			// Token: 0x040001EA RID: 490
			All = 31u
		}

		// Token: 0x0200002E RID: 46
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct PROCESSENTRY32
		{
			// Token: 0x040001EB RID: 491
			private const int MAX_PATH = 260;

			// Token: 0x040001EC RID: 492
			internal uint dwSize;

			// Token: 0x040001ED RID: 493
			internal uint cntUsage;

			// Token: 0x040001EE RID: 494
			internal uint th32ProcessID;

			// Token: 0x040001EF RID: 495
			internal IntPtr th32DefaultHeapID;

			// Token: 0x040001F0 RID: 496
			internal uint th32ModuleID;

			// Token: 0x040001F1 RID: 497
			internal uint cntThreads;

			// Token: 0x040001F2 RID: 498
			internal uint th32ParentProcessID;

			// Token: 0x040001F3 RID: 499
			internal int pcPriClassBase;

			// Token: 0x040001F4 RID: 500
			internal uint dwFlags;

			// Token: 0x040001F5 RID: 501
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string szExeFile;
		}

		// Token: 0x0200002F RID: 47
		public class ModuleInfo
		{
			// Token: 0x040001F6 RID: 502
			public string baseName;

			// Token: 0x040001F7 RID: 503
			public string fileName;

			// Token: 0x040001F8 RID: 504
			public IntPtr baseOfDll;

			// Token: 0x040001F9 RID: 505
			public IntPtr entryPoint;

			// Token: 0x040001FA RID: 506
			public int sizeOfImage;

			// Token: 0x040001FB RID: 507
			public int Id;
		}

		// Token: 0x02000030 RID: 48
		[StructLayout(LayoutKind.Sequential)]
		public class WinModuleEntry
		{
			// Token: 0x040001FC RID: 508
			public const int sizeofModuleName = 256;

			// Token: 0x040001FD RID: 509
			public const int sizeofFileName = 260;

			// Token: 0x040001FE RID: 510
			public int dwSize;

			// Token: 0x040001FF RID: 511
			public int th32ModuleID;

			// Token: 0x04000200 RID: 512
			public int th32ProcessID;

			// Token: 0x04000201 RID: 513
			public int GlblcntUsage;

			// Token: 0x04000202 RID: 514
			public int ProccntUsage;

			// Token: 0x04000203 RID: 515
			public IntPtr modBaseAddr = IntPtr.Zero;

			// Token: 0x04000204 RID: 516
			public int modBaseSize;

			// Token: 0x04000205 RID: 517
			public IntPtr hModule = IntPtr.Zero;
		}

		// Token: 0x02000031 RID: 49
		[StructLayout(LayoutKind.Sequential)]
		public class NtModuleInfo
		{
			// Token: 0x04000206 RID: 518
			public IntPtr BaseOfDll = IntPtr.Zero;

			// Token: 0x04000207 RID: 519
			public int SizeOfImage;

			// Token: 0x04000208 RID: 520
			public IntPtr EntryPoint = IntPtr.Zero;
		}
	}
}
