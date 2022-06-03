using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ProcessUtils;

namespace Reactor_Decryptor
{
	// Token: 0x0200000F RID: 15
	public partial class MainForm : Form
	{
		// Token: 0x06000012 RID: 18
		[DllImport("kernel32.dll")]
		public static extern int CloseHandle(IntPtr hObject);

		// Token: 0x06000013 RID: 19
		[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "CreateProcessW", ExactSpelling = true, SetLastError = true)]
		public static extern int CreateProcess2(IntPtr lpApplicationName, IntPtr lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, int bInheritHandles, int dwCreationFlags, IntPtr lpEnvironment, IntPtr lpCurrentDriectory, IntPtr lpStartupInfo, IntPtr lpProcessInformation);

		// Token: 0x06000014 RID: 20
		[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "ReadProcessMemory", ExactSpelling = true, SetLastError = true)]
		public static extern int ReadProcessMemory2(int hProcess, int lpBaseAddress, IntPtr lpBuffer, int nSize, ref int lpNumberOfBytesRead);

		// Token: 0x06000015 RID: 21
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int GetThreadContext(int hThread, IntPtr lpContext);

		// Token: 0x06000016 RID: 22
		[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "TerminateProcess", ExactSpelling = true, SetLastError = true)]
		private static extern int TerminateProcess2(int hProcess, int uExitCode);

		// Token: 0x06000017 RID: 23
		[DllImport("kernel32.dll")]
		private static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, int lpOverlapped);

		// Token: 0x06000018 RID: 24
		[DllImport("kernel32.dll")]
		private static extern bool WriteFile(IntPtr hFile, IntPtr lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, int lpOverlapped);

		// Token: 0x06000019 RID: 25
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Ansi)]
		private static extern bool WriteFile(IntPtr hFile, StringBuilder lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, [In] ref NativeOverlapped lpOverlapped);

		// Token: 0x0600001A RID: 26
		[DllImport("Kernel32.dll", EntryPoint = "RtlZeroMemory")]
		private static extern void ZeroMemory(IntPtr dest, int size);

		// Token: 0x0600001B RID: 27
		[DllImport("kernel32")]
		private static extern uint VirtualAlloc(uint lpStartAddr, uint size, uint flAllocationType, uint flProtect);

		// Token: 0x0600001C RID: 28
		[DllImport("kernel32")]
		private static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, uint dwFreeType);

		// Token: 0x0600001D RID: 29
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref MainForm.PROCESS_BASIC_INFORMATION processInformation, uint processInformationLength, out int returnLength);

		// Token: 0x0600001E RID: 30
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern uint SetFilePointer([In] IntPtr hFile, [In] int lDistanceToMove, int lpDistanceToMoveHigh, [In] MainForm.EMoveMethod dwMoveMethod);

		// Token: 0x0600001F RID: 31
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern uint SetFilePointer([In] IntPtr hFile, [In] int lDistanceToMove, out int lpDistanceToMoveHigh, [In] MainForm.EMoveMethod dwMoveMethod);

		// Token: 0x06000020 RID: 32
		[DllImport("kernel32.dll")]
		public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In] [Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesRead);

		// Token: 0x06000021 RID: 33
		[DllImport("kernel32.dll")]
		public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In] [Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesWritten);

		// Token: 0x06000022 RID: 34
		[DllImport("Kernel32.dll")]
		public static extern bool ResumeThread(IntPtr hProcess);

		// Token: 0x06000023 RID: 35
		[DllImport("kernel32.dll", EntryPoint = "CreateFileA", SetLastError = true)]
		internal static extern IntPtr CreateFile(string filename, uint desiredAccess, uint shareMode, IntPtr attributes, uint creationDisposition, uint flagsAndAttributes, IntPtr templateFile);

		// Token: 0x06000024 RID: 36
		[DllImport("kernel32.dll")]
		private static extern uint GetFileSize(IntPtr hFile, IntPtr lpFileSizeHigh);

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenFileMapping(int wDesiredAccess, bool bInheritHandle, string lpName);

		// Token: 0x06000026 RID: 38
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

		// Token: 0x06000027 RID: 39
		[DllImport("Kernel32.dll")]
		private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

		// Token: 0x06000028 RID: 40
		[DllImport("kernel32.dll")]
		internal static extern bool FlushViewOfFile(IntPtr lpBaseAddress, int dwNumberOfBytesToFlush);

		// Token: 0x06000029 RID: 41
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x0600002A RID: 42
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CloseHandle(uint hHandle);

		// Token: 0x0600002B RID: 43
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern uint GetLastError();

		// Token: 0x0600002C RID: 44
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenThread(MainForm.ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

		// Token: 0x0600002D RID: 45
		[DllImport("kernel32.dll")]
		private static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);

		// Token: 0x0600002E RID: 46 RVA: 0x00003C33 File Offset: 0x00002C33
		public MainForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003C58 File Offset: 0x00002C58
		private void Button1Click(object sender, EventArgs e)
		{
			this.label2.Text = "";
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Browse for target assembly";
			openFileDialog.InitialDirectory = "c:\\";
			if (this.DirectoryName != "")
			{
				openFileDialog.InitialDirectory = this.DirectoryName;
			}
			openFileDialog.Filter = "All files (*.exe,*.dll)|*.exe;*.dll";
			openFileDialog.FilterIndex = 2;
			openFileDialog.RestoreDirectory = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openFileDialog.FileName;
				this.textBox1.Text = fileName;
				int num = fileName.LastIndexOf("\\");
				if (num != -1)
				{
					this.DirectoryName = fileName.Remove(num, checked(fileName.Length - num));
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003D24 File Offset: 0x00002D24
		private void Button2Click(object sender, EventArgs e)
		{
			this.label2.Text = "";
			string text = this.textBox1.Text;
			if (text != "")
			{
				this.KernelOfDeprotection();
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			else
			{
				this.label2.ForeColor = Color.Red;
				this.label2.Text = "Please select a .NET Reactor protected file!";
			}
		}

		// Token: 0x06000031 RID: 49
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x06000032 RID: 50
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x06000033 RID: 51
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int WriteProcessMemory(int hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesWritten);

		// Token: 0x06000034 RID: 52
		[DllImport("kernel32.dll")]
		private static extern uint SuspendThread(IntPtr hThread);

		// Token: 0x06000035 RID: 53
		[DllImport("kernel32.dll")]
		private static extern bool Sleep(long Milliseconds);

		// Token: 0x06000036 RID: 54
		[DllImport("psapi.dll", CharSet = CharSet.Auto)]
		public static extern bool EnumProcessModules(IntPtr handle, IntPtr modules, int size, ref int needed);

		// Token: 0x06000037 RID: 55
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, MainForm.AllocationType flAllocationType, MainForm.MemoryProtection flProtect);

		// Token: 0x06000038 RID: 56 RVA: 0x00003D9C File Offset: 0x00002D9C
		private static void LoopWhileModulesAreLoadedNt(IntPtr processHandle, IntPtr htread)
		{
			int num = 0;
			while (true)
			{
				MainForm.ResumeThread(htread);
				MainForm.Sleep(1L);
				MainForm.SuspendThread(htread);
				int num2 = 0;
				MainForm.EnumProcessModules(processHandle, IntPtr.Zero, 0, ref num2);
				if (num2 > num)
				{
					num = num2;
					if (num2 >= 12)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003E00 File Offset: 0x00002E00
		public void PatchStrongNameCheck(int hprocess, IntPtr CryptVerifySignatureA)
		{
			int num = 0;
			byte[] array = new byte[]
			{
				51,
				192,
				64,
				194,
				24,
				0
			};
			if (CryptVerifySignatureA != IntPtr.Zero)
			{
				MainForm.WriteProcessMemory(hprocess, CryptVerifySignatureA, array, array.Length, ref num);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003E50 File Offset: 0x00002E50
		public void PatchMessageBox(int hprocess, IntPtr MessageBoxTimeoutW)
		{
			int num = 0;
			byte[] array = new byte[]
			{
				51,
				192,
				64,
				194,
				24,
				0
			};
			if (MessageBoxTimeoutW != IntPtr.Zero)
			{
				MainForm.WriteProcessMemory(hprocess, MessageBoxTimeoutW, array, array.Length, ref num);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003EA4 File Offset: 0x00002EA4
		public void PatchFindResource(IntPtr hprocess, IntPtr FindresourceA)
		{
			byte[] array = new byte[4096];
			IntPtr intPtr;
			MainForm.ReadProcessMemory(hprocess, FindresourceA, array, 4096u, out intPtr);
			int num = 0;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == 194)
					{
						num = i;
						break;
					}
					if (array[i] == 195)
					{
						return;
					}
				}
				if (num == 0)
				{
					return;
				}
				byte[] array2 = new byte[]
				{
					133,
					192,
					117,
					5,
					184,
					0,
					0,
					64,
					0,
					194,
					12,
					0
				};
				IntPtr intPtr2 = MainForm.VirtualAllocEx(hprocess, IntPtr.Zero, 512u, MainForm.AllocationType.Commit, MainForm.MemoryProtection.ReadWrite);
				MainForm.WriteProcessMemory(hprocess, intPtr2, array2, (uint)array2.Length, out intPtr);
				byte[] array3 = new byte[]
				{
					104,
					0,
					0,
					0,
					0,
					195
				};
				byte[] bytes = BitConverter.GetBytes((int)intPtr2);
				for (int i = 0; i < bytes.Length; i++)
				{
					array3[i + 1] = bytes[i];
				}
				MainForm.WriteProcessMemory(hprocess, (IntPtr)((long)FindresourceA + unchecked((long)num)), array3, (uint)array3.Length, out intPtr);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003FCC File Offset: 0x00002FCC
		public static void LoadAssemblyonDomain()
		{
			Assembly data = null;
			string assemblyFile = (string)AppDomain.CurrentDomain.GetData("asmname");
			try
			{
				AssemblyName assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
				data = Assembly.Load(assemblyName);
			}
			catch
			{
			}
			AppDomain.CurrentDomain.SetData("assembly", data);
			AppDomain.CurrentDomain.SetData("assembly", data);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000403C File Offset: 0x0000303C
		public void KernelOfDeprotection()
		{
			string text = this.textBox1.Text;
			checked
			{
				if (text == "" || !File.Exists(text))
				{
					this.label2.ForeColor = Color.Red;
					this.label2.Text = "Can't find patern error - maybe is not a .NET Reactor file";
				}
				else
				{
					bool @checked = this.radioButton1.Checked;
					byte[] array = File.ReadAllBytes(text);
					if (this.checkBox3.Checked)
					{
						IntPtr moduleHandle = MainForm.GetModuleHandle("advapi32.dll");
						IntPtr procAddress = MainForm.GetProcAddress(moduleHandle, "CryptVerifySignatureA");
						this.PatchStrongNameCheck(-1, procAddress);
					}
					if (this.checkBox8.Checked)
					{
						IntPtr moduleHandle2 = MainForm.GetModuleHandle("kernel32.dll");
						IntPtr procAddress2 = MainForm.GetProcAddress(moduleHandle2, "FindResourceA");
						this.PatchFindResource(new IntPtr(-1), procAddress2);
					}
					if (this.checkBox7.Checked)
					{
						IntPtr moduleHandle3 = MainForm.GetModuleHandle("user32.dll");
						IntPtr procAddress3 = MainForm.GetProcAddress(moduleHandle3, "MessageBoxTimeoutW");
						this.PatchMessageBox(-1, procAddress3);
					}
					MetadataReader metadataReader = new MetadataReader();
					FileStream fileStream = new FileStream(text, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
					BinaryReader binaryReader = new BinaryReader(fileStream);
					if (metadataReader.Intialize(binaryReader))
					{
						int num = metadataReader.inh.ioh.SizeOfCode + metadataReader.sections[0].pointer_to_raw_data;
						uint num2 = (uint)metadataReader.inh.ioh.SectionAlignment;
						long num3 = metadataReader.TablesOffset;
						for (int i = 0; i < 6; i++)
						{
							num3 += unchecked((long)(checked(metadataReader.tablesize[i].TotalSize * metadataReader.TableLengths[i])));
						}
						int num4 = 0;
						for (int i = 0; i < 6; i++)
						{
							num4 += metadataReader.tablesize[6].Sizes[i];
						}
						int metadataToken = 0;
						uint num5 = 0u;
						int num6 = 0;
						bool flag = false;
						int num7 = 0;
						int num8 = 0;
						int num9 = 0;
						for (int j = 0; j < metadataReader.TableLengths[1]; j++)
						{
							string text2 = "";
							string text3 = "";
							char c = 'a';
							int num10 = 0;
							try
							{
								while (c != '\0')
								{
									c = (char)metadataReader.Strings[(int)((IntPtr)(metadataReader.tables[1].members[j][1] + unchecked((long)num10)))];
									if (c != '\0')
									{
										text2 += c;
									}
									num10++;
								}
							}
							catch
							{
							}
							c = 'a';
							num10 = 0;
							try
							{
								while (c != '\0')
								{
									c = (char)metadataReader.Strings[(int)((IntPtr)(metadataReader.tables[1].members[j][2] + unchecked((long)num10)))];
									if (c != '\0')
									{
										text3 += c;
									}
									num10++;
								}
							}
							catch
							{
							}
							if (text2 == "ICryptoTransform" && text3 == "System.Security.Cryptography")
							{
								num8 = j + 1;
							}
						}
						for (int j = 0; j < metadataReader.TableLengths[6]; j++)
						{
							if (metadataReader.tables[6].members[j][0] != 0L)
							{
								uint num11 = (uint)metadataReader.Rva2Offset((int)metadataReader.tables[6].members[j][0]);
								char c = 'a';
								string text4 = "";
								int num10 = 0;
								while (c != '\0')
								{
									c = (char)metadataReader.Strings[(int)((IntPtr)(metadataReader.tables[6].members[j][3] + unchecked((long)num10)))];
									if (c != '\0')
									{
										text4 += c;
									}
									num10++;
								}
								if (text4 == ".cctor")
								{
									binaryReader.BaseStream.Position = (long)(unchecked((ulong)num11));
									byte b = binaryReader.ReadByte() & 255;
									int count = 0;
									int num12 = 0;
									if ((b & 3) == 2)
									{
										count = b >> 2;
										num12 = 1;
									}
									if ((b & 3) == 3)
									{
										binaryReader.BaseStream.Position = binaryReader.BaseStream.Position + 3L;
										count = binaryReader.ReadInt32();
										binaryReader.BaseStream.Position = binaryReader.BaseStream.Position + 4L;
										num12 = 12;
									}
									byte[] array2 = binaryReader.ReadBytes(count);
									for (int k = 0; k < array2.Length; k++)
									{
										if (k + 4 < array2.Length && array2[k] == 40 && array2[k + 4] == 6)
										{
											try
											{
												binaryReader.BaseStream.Position = (long)(unchecked((ulong)num11) + (ulong)(unchecked((long)k)) + 1uL + (ulong)(unchecked((long)num12)));
												int num13 = (binaryReader.ReadInt32() & 16777215) - 1;
												int num14 = (int)(metadataReader.tables[6].members[num13 + 1][5] - metadataReader.tables[6].members[num13][5]);
												if (num14 == 0)
												{
													uint num15 = (uint)metadataReader.tables[6].members[num13][0];
													binaryReader.BaseStream.Position = unchecked((long)metadataReader.Rva2Offset(checked((int)num15)));
													b = binaryReader.ReadByte();
													if ((b & 3) == 3)
													{
														binaryReader.BaseStream.Position = unchecked((long)metadataReader.Rva2Offset(checked((int)num15 + 4)));
														num6 = binaryReader.ReadInt32();
														if (num6 > 10000)
														{
															metadataToken = j + 100663297;
															binaryReader.BaseStream.Position = unchecked((long)(checked(metadataReader.Rva2Offset((int)num15) + 8)));
															int num16 = (binaryReader.ReadInt32() & 16777215) - 1;
															num16 = (int)metadataReader.tables[17].members[num16][0];
															binaryReader.BaseStream.Position = unchecked((long)(checked(num16 + (int)metadataReader.BlobOffset)));
															int num17 = (int)binaryReader.ReadByte();
															if ((num17 & 128) != 0)
															{
																num17 = (int)binaryReader.ReadByte();
															}
															binaryReader.BaseStream.Position = binaryReader.BaseStream.Position + 2L;
															byte[] array3 = new byte[4];
															byte[] array4 = array3;
															int num18 = 0;
															while (true)
															{
																CorElementType corElementType;
																do
																{
																	corElementType = (CorElementType)binaryReader.ReadByte();
																}
																while (corElementType == CorElementType.ELEMENT_TYPE_PINNED || corElementType == CorElementType.ELEMENT_TYPE_PTR || corElementType == CorElementType.ELEMENT_TYPE_BYREF || corElementType == CorElementType.ELEMENT_TYPE_SZARRAY);
																if (corElementType == CorElementType.ELEMENT_TYPE_VALUETYPE || corElementType == CorElementType.ELEMENT_TYPE_CMOD_REQD || corElementType == CorElementType.ELEMENT_TYPE_CMOD_OPT)
																{
																	byte b2 = binaryReader.ReadByte();
																	if ((b2 & 128) != 0)
																	{
																		binaryReader.ReadByte();
																	}
																}
																else if (corElementType == CorElementType.ELEMENT_TYPE_CLASS)
																{
																	array4[1] = binaryReader.ReadByte();
																	if ((array4[1] & 128) != 0)
																	{
																		array4[0] = binaryReader.ReadByte();
																		num16 = BitConverter.ToInt32(array4, 0);
																		num16 &= 32767;
																		num16 >>= 2;
																		if (num16 == num8)
																		{
																			break;
																		}
																	}
																}
																num18++;
																if (num18 > num17)
																{
																	goto IL_79F;
																}
															}
															num9 = num18;
															num5 = num15;
															IL_79F:;
														}
													}
												}
											}
											catch
											{
											}
										}
										if (num9 != 0)
										{
											break;
										}
									}
								}
							}
							if (num9 != 0)
							{
								break;
							}
						}
						if (num5 != 0u)
						{
							if (num8 != 0)
							{
								if (num9 != 0)
								{
									int offsetAProfiling = 0;
									binaryReader.BaseStream.Position = 0L;
									byte[] array5 = binaryReader.ReadBytes(num);
									for (int j = 0; j < num - 20; j++)
									{
										if (array5[j] == 40 && array5[j + 4] == 10 && array5[j + 5] == 57 && array5[j + 6] == 1 && array5[j + 7] == 0 && array5[j + 8] == 0 && array5[j + 9] == 0 && array5[j + 10] == 42 && array5[j + 11] == 115 && array5[j + 15] == 6 && array5[j + 16] == 38 && array5[j + 17] == 42)
										{
											offsetAProfiling = j;
										}
									}
									int num19 = 0;
									IntPtr intPtr = IntPtr.Zero;
									IntPtr intPtr2 = IntPtr.Zero;
									int processId = 0;
									Thread thread = null;
									Assembly assembly = null;
									if (@checked)
									{
										PROCESS_INFORMATION2 pROCESS_INFORMATION = default(PROCESS_INFORMATION2);
										int num20 = 0;
										IntPtr lpApplicationName = Marshal.StringToHGlobalUni(text);
										STARTUPINFO2 sTARTUPINFO = default(STARTUPINFO2);
										IntPtr intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf(sTARTUPINFO));
										Marshal.StructureToPtr(sTARTUPINFO, intPtr3, false);
										IntPtr intPtr4 = Marshal.AllocHGlobal(Marshal.SizeOf(pROCESS_INFORMATION));
										Marshal.StructureToPtr(pROCESS_INFORMATION, intPtr4, false);
										MainForm.CreateProcess2(lpApplicationName, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0, 4, IntPtr.Zero, IntPtr.Zero, intPtr3, intPtr4);
										sTARTUPINFO = (STARTUPINFO2)Marshal.PtrToStructure(intPtr3, typeof(STARTUPINFO2));
										pROCESS_INFORMATION = (PROCESS_INFORMATION2)Marshal.PtrToStructure(intPtr4, typeof(PROCESS_INFORMATION2));
										IntPtr intPtr5 = Marshal.AllocHGlobal(4);
										if (Environment.OSVersion.Platform == PlatformID.Win32NT)
										{
											MainForm.PROCESS_BASIC_INFORMATION pROCESS_BASIC_INFORMATION = default(MainForm.PROCESS_BASIC_INFORMATION);
											int num22;
											int num21 = MainForm.NtQueryInformationProcess((IntPtr)pROCESS_INFORMATION.hProcess, 0, ref pROCESS_BASIC_INFORMATION, (uint)Marshal.SizeOf(pROCESS_BASIC_INFORMATION), out num22);
											if (num21 >= 0)
											{
												MainForm.ReadProcessMemory2(pROCESS_INFORMATION.hProcess, pROCESS_BASIC_INFORMATION.PebBaseAddress + 8, intPtr5, Marshal.SizeOf(num19), ref num20);
												num19 = Marshal.ReadInt32(intPtr5, 0);
											}
										}
										else
										{
											CONTEXT cONTEXT = default(CONTEXT);
											IntPtr intPtr6 = Marshal.AllocHGlobal(1024);
											cONTEXT.ContextFlags = 65538u;
											Marshal.StructureToPtr(cONTEXT, intPtr6, false);
											if (MainForm.GetThreadContext(pROCESS_INFORMATION.hThread, intPtr6) != 0)
											{
												IntPtr value = new IntPtr((long)(unchecked((ulong)((CONTEXT)Marshal.PtrToStructure(intPtr6, typeof(CONTEXT))).Ebx) + 8uL));
												MainForm.ReadProcessMemory2(pROCESS_INFORMATION.hProcess, (int)value, intPtr5, Marshal.SizeOf(num19), ref num20);
												num19 = Marshal.ReadInt32(intPtr5, 0);
											}
											Marshal.FreeHGlobal(intPtr6);
											Marshal.FreeHGlobal(intPtr4);
											Marshal.FreeHGlobal(intPtr3);
										}
										Marshal.FreeHGlobal(intPtr5);
										intPtr = (IntPtr)pROCESS_INFORMATION.hProcess;
										intPtr2 = (IntPtr)pROCESS_INFORMATION.hThread;
										processId = pROCESS_INFORMATION.dwProcessId;
									}
									else
									{
										try
										{
											AssemblyName assemblyName = AssemblyName.GetAssemblyName(text);
											assembly = AppDomain.CurrentDomain.Load(assemblyName);
										}
										catch (Exception ex)
										{
											this.label2.ForeColor = Color.Red;
											this.label2.Text = ex.Message;
											return;
										}
										num19 = (int)Marshal.GetHINSTANCE(assembly.ManifestModule);
										intPtr = (IntPtr)(-1);
									}
									if (num19 == 0)
									{
										num19 = metadataReader.inh.ioh.ImageBase;
									}
									num5 += 12u;
									int num23 = 0;
									int resPatchRva = 0;
									byte[] array6 = new byte[]
									{
										43,
										254
									};
									byte[] array3 = new byte[2];
									array3[0] = 19;
									byte[] array7 = array3;
									byte[] array8 = new byte[]
									{
										106,
										97
									};
									byte[] array9 = new byte[num6];
									IntPtr intPtr7;
									int num24 = MainForm.ReadProcessMemory(intPtr, (IntPtr)(unchecked((long)num19) + (long)(unchecked((ulong)num5))), array9, (uint)num6, out intPtr7);
									byte[] array10 = new byte[5];
									array7[1] = (byte)num9;
									for (int l = 0; l < num6 - 8; l++)
									{
										if (num23 == 0 && array9[l] == array7[0] && array9[l + 1] == array7[1] && (array9[l - 5] == 40 || array9[l - 5] == 56))
										{
											num23 = l + (int)num5;
										}
										if (array9[l] == 32 && array9[l + 5] == 64)
										{
											Array.Copy(array9, l, array10, 0, 5);
											resPatchRva = l + (int)num5 + 1;
										}
									}
									if (num23 != 0)
									{
										bool flag2 = false;
										for (int l = 0; l < num6 - 6; l++)
										{
											if (array9[l] == array10[0] && array9[l + 1] == array10[1] && array9[l + 2] == array10[2] && array9[l + 3] == array10[3] && array9[l + 4] == array10[4])
											{
												if (array9[l + 5] == 19)
												{
													flag2 = true;
													break;
												}
											}
										}
										if (!flag2)
										{
											resPatchRva = 0;
										}
										int m;
										for (m = 0; m < num6 - 2; m++)
										{
											if (array9[m] == array8[0] && array9[m + 1] == array8[1])
											{
												flag = true;
												if (array9[m - 5] == 32)
												{
													num7 = BitConverter.ToInt32(array9, m - 4);
												}
												break;
											}
										}
										if (flag && num7 == 0)
										{
											for (int l = 0; l < num6 - 2; l++)
											{
												if (array9[l] == 56)
												{
													int num25 = BitConverter.ToInt32(array9, l + 1);
													num25 = num25 + l + 5;
													if (num25 == m)
													{
														num7 = BitConverter.ToInt32(array9, l - 4);
														break;
													}
												}
											}
										}
										IntPtr intPtr8;
										MainForm.WriteProcessMemory(intPtr, (IntPtr)(unchecked((long)num19) + unchecked((long)num23)), array6, (uint)array6.Length, out intPtr8);
										if (@checked)
										{
											if (this.checkBox3.Checked || this.checkBox8.Checked)
											{
												int num26 = 0;
												if (this.checkBox3.Checked)
												{
													num26++;
												}
												if (this.checkBox8.Checked)
												{
													num26++;
												}
												if (this.checkBox7.Checked)
												{
													num26++;
												}
												for (int n = 0; n < 1500; n++)
												{
													MainForm.LoopWhileModulesAreLoadedNt(intPtr, intPtr2);
													ProcModule.ModuleInfo[] moduleInfos = ProcModule.GetModuleInfos(processId);
													int num27 = 0;
													if (moduleInfos != null && moduleInfos.Length > 0)
													{
														for (int l = 0; l < moduleInfos.Length; l++)
														{
															if (moduleInfos[l].baseName.ToLower().Contains("kernel32"))
															{
																if (this.checkBox8.Checked)
																{
																	IntPtr moduleHandle2 = MainForm.GetModuleHandle("kernel32.dll");
																	IntPtr procAddress2 = MainForm.GetProcAddress(moduleHandle2, "FindResourceA");
																	long num28 = (long)procAddress2 - (long)moduleHandle2;
																	IntPtr intPtr9 = (IntPtr)((long)moduleInfos[l].baseOfDll + num28);
																	this.PatchFindResource(intPtr, intPtr9);
																	num27++;
																}
															}
															if (moduleInfos[l].baseName.ToLower().Contains("advapi32"))
															{
																if (this.checkBox3.Checked)
																{
																	IntPtr moduleHandle = MainForm.GetModuleHandle("advapi32.dll");
																	IntPtr procAddress = MainForm.GetProcAddress(moduleHandle, "CryptVerifySignatureA");
																	long num29 = (long)procAddress - (long)moduleHandle;
																	IntPtr intPtr9 = (IntPtr)((long)moduleInfos[l].baseOfDll + num29);
																	this.PatchStrongNameCheck((int)intPtr, intPtr9);
																	num27++;
																}
															}
															if (moduleInfos[l].baseName.ToLower().Contains("user32"))
															{
																if (this.checkBox7.Checked)
																{
																	IntPtr moduleHandle3 = MainForm.GetModuleHandle("user32.dll");
																	IntPtr procAddress3 = MainForm.GetProcAddress(moduleHandle3, "MessageBoxTimeoutW");
																	long num30 = (long)procAddress3 - (long)moduleHandle3;
																	IntPtr intPtr9 = (IntPtr)((long)moduleInfos[l].baseOfDll + num30);
																	this.PatchMessageBox((int)intPtr, intPtr9);
																	num27++;
																}
															}
														}
													}
													if (num27 == num26)
													{
														break;
													}
												}
											}
											MainForm.ResumeThread(intPtr2);
										}
										else
										{
											ConstructorInfo constructorInfo = null;
											try
											{
												constructorInfo = (ConstructorInfo)assembly.ManifestModule.ResolveMethod(metadataToken);
											}
											catch
											{
											}
											if (constructorInfo == null)
											{
												for (int num31 = 2; num31 < metadataReader.TableLengths[2]; num31++)
												{
													try
													{
														Type type = assembly.ManifestModule.ResolveType(33554433 + num31);
														if (type.TypeInitializer != null)
														{
															constructorInfo = type.TypeInitializer;
															break;
														}
													}
													catch
													{
													}
												}
											}
											MainForm.InitializerInvoker.errormessage = "";
											MainForm.InitializerInvoker @object = new MainForm.InitializerInvoker(assembly, metadataReader.TableLengths[6]);
											ThreadStart start = new ThreadStart(@object.Initialize);
											thread = new Thread(start);
											try
											{
												thread.Start();
											}
											catch
											{
											}
										}
										MainForm.InitData();
										uint num32 = 0u;
										int value2 = 0;
										int num33 = metadataReader.TableLengths[40];
										Thread.Sleep(1000);
										num32 = MainForm.SecondSearch(intPtr, (IntPtr)value2);
										try
										{
											if (!@checked)
											{
												thread.Suspend();
											}
										}
										catch
										{
										}
										if (num32 != 0u)
										{
											byte[] array11 = new byte[32];
											byte[] array12 = new byte[16];
											array3 = new byte[1];
											int num34 = 0;
											int num35 = 0;
											while (true)
											{
												num32 += 12u;
												int num36 = MainForm.ReadProcessMemory(intPtr, (IntPtr)((long)(unchecked((ulong)num32))), array11, (uint)array11.Length, out intPtr8);
												num32 += 44u;
												num36 = MainForm.ReadProcessMemory(intPtr, (IntPtr)((long)(unchecked((ulong)num32))), array12, (uint)array12.Length, out intPtr8);
												num34 = 0;
												while (true)
												{
													try
													{
														int resindex = num34;
														long num37 = (long)(unchecked((ulong)(checked((uint)metadataReader.tables[40].members[num34][0] + (uint)metadataReader.netdir.ResourceRVA))));
														uint num38 = (uint)metadataReader.Rva2Offset((int)num37);
														byte[] array14;
														unchecked
														{
															long num39 = (long)BitConverter.ToInt32(array, checked((int)num38));
															byte[] array13 = new byte[num39];
															Array.Copy(array, (long)((ulong)(checked(num38 + 4u))), array13, 0L, num39);
															ICryptoTransform transform = new RijndaelManaged
															{
																Mode = CipherMode.CBC
															}.CreateDecryptor(array11, array12);
															MemoryStream memoryStream = new MemoryStream();
															CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
															cryptoStream.Write(array13, 0, array13.Length);
															cryptoStream.FlushFinalBlock();
															array14 = memoryStream.ToArray();
															memoryStream.Close();
															cryptoStream.Close();
														}
														if (array14[5] != 0 || array14[6] != 0 || array14[7] != 0)
														{
															if (num34 < num33)
															{
																num34++;
																continue;
															}
															num32 += num2 - num32 % num2;
															num32 = MainForm.SecondSearch(intPtr, (IntPtr)((long)(unchecked((ulong)num32))));
															if (num32 != 0u)
															{
																break;
															}
														}
														else
														{
															num35 = 1;
															this.DecryptSave(text, metadataReader, flag, array11, array12, array14, num7, resindex, num38, num5, offsetAProfiling, binaryReader, resPatchRva);
															num35 = 2;
														}
													}
													catch
													{
														if (num34 < num33)
														{
															num34++;
															continue;
														}
														num32 += num2 - num32 % num2;
														num32 = MainForm.SecondSearch(intPtr, (IntPtr)((long)(unchecked((ulong)num32))));
														if (num32 != 0u)
														{
															break;
														}
													}
													goto IL_14D3;
												}
											}
											IL_14D3:
											if (@checked)
											{
												MainForm.TerminateProcess2((int)intPtr, -1);
											}
											else
											{
												IntPtr hThread = MainForm.OpenThread(MainForm.ThreadAccess.TERMINATE, false, MainForm.InitializerInvoker.threadid);
												MainForm.TerminateThread(hThread, 0u);
											}
											if (num35 == 0)
											{
												this.label2.ForeColor = Color.Red;
												this.label2.Text = "Something goes wrong whit decryption!";
											}
											if (num35 == 1)
											{
												this.label2.ForeColor = Color.Red;
												this.label2.Text = "Error while decrypting methods!";
											}
										}
										else
										{
											if (@checked)
											{
												MainForm.TerminateProcess2((int)intPtr, -1);
											}
											else
											{
												IntPtr hThread = MainForm.OpenThread(MainForm.ThreadAccess.TERMINATE, false, MainForm.InitializerInvoker.threadid);
												MainForm.TerminateThread(hThread, 0u);
											}
											this.label2.ForeColor = Color.Red;
											this.label2.Text = "Can't find patern error - maybe is not a .NET Reactor file";
										}
									}
									else
									{
										this.label2.ForeColor = Color.Red;
										this.label2.Text = "Can't find local variable! Is not protected whit .NET Reactor ?";
									}
								}
								else
								{
									this.label2.ForeColor = Color.Red;
									this.label2.Text = "Can't find ICryptoTransform variable!";
								}
							}
							else
							{
								this.label2.ForeColor = Color.Red;
								this.label2.Text = "Can't find ICryptoTransform! Is not protected whit .NET Reactor ?";
							}
						}
						else
						{
							this.label2.ForeColor = Color.Red;
							this.label2.Text = "Can't find decryption method! Is not protected whit .NET Reactor ?";
						}
					}
					else
					{
						this.label2.ForeColor = Color.Red;
						this.label2.Text = "Not even a .NET assembly!";
					}
					fileStream.Close();
					binaryReader.Close();
					if (this.radioButton2.Checked && MainForm.InitializerInvoker.errormessage != "")
					{
						ErrorMessageShow errorMessageShow = new ErrorMessageShow(MainForm.InitializerInvoker.errormessage);
						errorMessageShow.Show();
					}
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00005820 File Offset: 0x00004820
		public unsafe void DecryptSave(string fp, MetadataReader mr, bool IsR4310, byte[] buffer2, byte[] buffer3, byte[] buffer5, int XorValue, int resindex, uint offsetmanifest, uint DecryptionMethodRVA, int OffsetAProfiling, BinaryReader reader2, int ResPatchRva)
		{
			int num = 0;
			checked
			{
				for (int i = 0; i < 6; i++)
				{
					num += mr.tablesize[6].Sizes[i];
				}
				long num2 = mr.TablesOffset;
				for (int i = 0; i < 6; i++)
				{
					num2 += unchecked((long)(checked(mr.tablesize[i].TotalSize * mr.TableLengths[i])));
				}
				bool flag = false;
				int sectionAlignment = mr.inh.ioh.SectionAlignment;
				string text = "";
				int num3 = fp.LastIndexOf(".");
				string text2 = fp.Remove(num3, fp.Length - num3) + "_decrypted" + Path.GetExtension(fp);
				File.Copy(fp, text2, true);
				IntPtr intPtr = IntPtr.Zero;
				uint num4 = 0u;
				IntPtr intPtr2 = IntPtr.Zero;
				IntPtr intPtr3 = IntPtr.Zero;
				byte* ptr = (byte*)intPtr3.ToPointer();
				byte* ptr2 = (byte*)intPtr3.ToPointer();
				intPtr = MainForm.CreateFile(text2, 3221225472u, 0u, IntPtr.Zero, 4u, 0u, IntPtr.Zero);
				num4 = MainForm.GetFileSize(intPtr, IntPtr.Zero);
				intPtr2 = MainForm.CreateFileMapping((int)intPtr, IntPtr.Zero, 4u, 0u, num4, "MyOwnFileMapping");
				intPtr3 = MainForm.MapViewOfFile(intPtr2, 6u, 0u, 0u, 0u);
				ptr = (byte*)intPtr3.ToPointer();
				ptr2 = ptr + offsetmanifest;
				if (this.checkBox2.Checked && OffsetAProfiling != 0)
				{
					Marshal.WriteByte((IntPtr)((void*)ptr), OffsetAProfiling + 10, 0);
				}
				if (IsR4310)
				{
					int num5 = buffer5.Length / 8;
					fixed (byte* ptr3 = buffer5)
					{
						int j = 0;
						do
						{
							int* ptr4 = unchecked((int*)ptr3) + unchecked((IntPtr)j) * 8 / 4;
							*ptr4 ^= XorValue;
							j++;
						}
						while (j < num5);
					}
				}
				BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer5));
				binaryReader.BaseStream.Position = 0L;
				int num6 = 0;
				int num7 = binaryReader.ReadInt32();
				int num8 = binaryReader.ReadInt32();
				if (num7 != 0)
				{
					for (int k = 0; k < num7; k++)
					{
						try
						{
							int num9 = binaryReader.ReadInt32() - num6;
							num9 = mr.Rva2Offset(num9);
							Marshal.WriteInt32((IntPtr)((void*)(ptr + num9)), binaryReader.ReadInt32());
						}
						catch
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						binaryReader.BaseStream.Position = 0L;
						int num10 = binaryReader.ReadInt32();
						binaryReader.ReadInt32();
						uint num11 = (uint)mr.Offset2Rva((int)offsetmanifest);
						uint num12 = ptr2;
						int j = 0;
						do
						{
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							j++;
						}
						while (j < num10);
						long position = binaryReader.BaseStream.Position;
						int num13 = binaryReader.ReadInt32();
						binaryReader.ReadInt32();
						j = 0;
						do
						{
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							j++;
						}
						while (j < num13);
						long position2 = binaryReader.BaseStream.Position;
						while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length - 1L)
						{
							int num14 = binaryReader.ReadInt32();
							int num15 = binaryReader.ReadInt32();
							j = 0;
							do
							{
								int rva = binaryReader.ReadInt32();
								int val = binaryReader.ReadInt32();
								uint num16 = (uint)mr.Rva2Offset(rva);
								Marshal.WriteInt32((IntPtr)((void*)(ptr + num16)), 0, val);
								j += 2;
							}
							while (j < num15);
						}
						byte[] array = new byte[position2 - position + 8L];
						array[0] = 0;
						array[1] = 0;
						array[2] = 0;
						array[3] = 0;
						array[4] = 0;
						array[5] = 0;
						array[6] = 0;
						array[7] = 0;
						Array.Copy(buffer5, position, array, 8L, position2 - position);
						MemoryStream memoryStream = new MemoryStream();
						Rijndael rijndael = Rijndael.Create();
						rijndael.Key = buffer2;
						rijndael.IV = buffer3;
						CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
						cryptoStream.Write(array, 0, array.Length);
						cryptoStream.Close();
						byte[] array2 = memoryStream.ToArray();
						memoryStream.Close();
						uint num17 = (uint)mr.tables[40].members[resindex][0] + (uint)mr.Rva2Offset(mr.netdir.ResourceRVA);
						uint num18 = (uint)((int)intPtr3) + num17;
						unchecked
						{
							Marshal.Copy(array2, 0, (IntPtr)((long)((ulong)(checked(num18 + 4u)))), array2.Length);
							Marshal.WriteInt32((IntPtr)((long)((ulong)num18)), 0, array2.Length);
						}
						if (this.checkBox6.Checked)
						{
							uint num19 = (uint)mr.Rva2Offset((int)DecryptionMethodRVA);
							byte[] array3 = new byte[4];
							array3[0] = 1;
							byte[] array4 = array3;
							Marshal.Copy(array4, 0, (IntPtr)((void*)(ptr + num19 - 8)), array4.Length);
							array4 = new byte[]
							{
								42
							};
							Marshal.Copy(array4, 0, (IntPtr)((void*)(ptr + num19)), array4.Length);
						}
						binaryReader.Close();
						reader2.Close();
						MainForm.FlushViewOfFile(intPtr3, (int)num4);
						MainForm.UnmapViewOfFile(intPtr3);
						MainForm.CloseHandle((uint)((int)intPtr2));
						MainForm.CloseHandle((uint)((int)intPtr));
						text = " Reactor 3.x!";
						goto IL_1CD0;
					}
				}
				if (num7 == 0)
				{
					uint num16 = 0u;
					while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length - 1L)
					{
						int num20 = binaryReader.ReadInt32();
						int num21 = binaryReader.ReadInt32();
						int num22 = binaryReader.ReadInt32();
						byte[] array5 = binaryReader.ReadBytes(num22);
						if (array5.Length >= 8 && array5[1] == 56 && array5[2] == 2 && array5[3] == 0 && array5[4] == 0 && array5[5] == 0)
						{
							array5[1] = 0;
							array5[2] = 0;
							array5[3] = 0;
							array5[4] = 0;
							array5[5] = 0;
							array5[6] = 0;
							array5[7] = 0;
						}
						if (array5.Length >= 19 && array5[12] == 56 && array5[13] == 2 && array5[14] == 0 && array5[15] == 0 && array5[16] == 0)
						{
							array5[12] = 0;
							array5[13] = 0;
							array5[14] = 0;
							array5[15] = 0;
							array5[16] = 0;
							array5[17] = 0;
							array5[18] = 0;
						}
						if (num20 != 0)
						{
							num16 = (uint)mr.Rva2Offset(num20);
						}
						if (num20 != 0 && num22 != 0)
						{
							Marshal.Copy(array5, 0, (IntPtr)((void*)(ptr + num16)), num22);
						}
					}
					byte[] array3 = new byte[8];
					byte[] array = array3;
					MemoryStream memoryStream = new MemoryStream();
					Rijndael rijndael = Rijndael.Create();
					rijndael.Key = buffer2;
					rijndael.IV = buffer3;
					CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
					cryptoStream.Write(array, 0, array.Length);
					cryptoStream.Close();
					byte[] array2 = memoryStream.ToArray();
					memoryStream.Close();
					uint num17 = (uint)mr.tables[40].members[resindex][0] + (uint)mr.Rva2Offset(mr.netdir.ResourceRVA);
					uint num18 = (uint)((int)intPtr3) + num17;
					unchecked
					{
						Marshal.Copy(array2, 0, (IntPtr)((long)((ulong)(checked(num18 + 4u)))), array2.Length);
						Marshal.WriteInt32((IntPtr)((long)((ulong)num18)), 0, array2.Length);
					}
					if (this.checkBox6.Checked)
					{
						uint num19 = (uint)mr.Rva2Offset((int)DecryptionMethodRVA);
						array3 = new byte[4];
						array3[0] = 1;
						byte[] array4 = array3;
						Marshal.Copy(array4, 0, (IntPtr)((void*)(ptr + num19 - 8)), array4.Length);
						array4 = new byte[]
						{
							42
						};
						Marshal.Copy(array4, 0, (IntPtr)((void*)(ptr + num19)), array4.Length);
					}
					reader2.Close();
					binaryReader.Close();
					MainForm.FlushViewOfFile(intPtr3, (int)num4);
					MainForm.UnmapViewOfFile(intPtr3);
					MainForm.CloseHandle((uint)((int)intPtr2));
					MainForm.CloseHandle((uint)((int)intPtr));
					text = " Reactor 3.x whit 0 Necrobits!";
				}
				else
				{
					long[] array6 = new long[]
					{
						5L,
						7L,
						2L
					};
					array6 = new long[binaryReader.ReadInt32()];
					byte[][] array7 = new byte[array6.Length][];
					int num23 = 0;
					byte[][] array8 = new byte[buffer5.Length][];
					byte[][] array9 = new byte[buffer5.Length][];
					long[] array10 = new long[array6.Length];
					int num24 = 0;
					int num25 = 0;
					int[] array11 = new int[array6.Length];
					int[] array12 = new int[array6.Length];
					long[] array13 = new long[array6.Length];
					long[] array14 = new long[array6.Length];
					int num26 = 0;
					int num27 = 0;
					IL_13F9:
					while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length - 1L)
					{
						long position3 = binaryReader.BaseStream.Position;
						int num28 = binaryReader.ReadInt32() - num6;
						for (int l = num23; l < num23 + 4; l++)
						{
							if (l < mr.TableLengths[6] && mr.tables[6].members[l][0] != 0L)
							{
								int num20 = (int)mr.tables[6].members[l][0];
								uint num29 = (uint)mr.Rva2Offset((int)mr.tables[6].members[l][0]);
								reader2.BaseStream.Position = (long)(unchecked((ulong)num29));
								byte b = reader2.ReadByte();
								b &= 3;
								if (b == 2)
								{
									num20++;
								}
								if (b == 3)
								{
									num20 += 12;
								}
								if (num20 == num28)
								{
									num23 = l;
									IL_ADD:
									int num30 = binaryReader.ReadInt32();
									bool flag2 = false;
									if (num30 >= 1879048192)
									{
										flag2 = true;
										num30 -= 1879048192;
									}
									int num31 = binaryReader.ReadInt32();
									num25 += num31;
									long position4 = binaryReader.BaseStream.Position;
									byte[] array15 = binaryReader.ReadBytes(num31);
									int num32 = num23 * num;
									reader2.BaseStream.Position = unchecked((long)num32) + num2;
									int num33 = reader2.ReadInt32();
									if (!this.checkBox1.Checked && flag2)
									{
										array11[num26] = mr.Offset2Rva((int)(unchecked((long)num32) + num2));
										array12[num26] = num33;
										array13[num26] = position3;
										array14[num26] = unchecked((long)(checked(num31 + 12)));
										num27 = num27 + num31 + 12;
										num26++;
									}
									else
									{
										uint num34 = (uint)mr.Rva2Offset(num33);
										reader2.BaseStream.Position = (long)(unchecked((ulong)num34));
										byte b2 = Marshal.ReadByte((IntPtr)((void*)ptr), (int)num34);
										b2 &= 3;
										if (b2 == 2 && num31 <= 63)
										{
											if (this.checkBox1.Checked && flag2)
											{
												byte[] array16 = null;
												try
												{
													array16 = ASMtoIL.Translate(this.checkBox4.Checked, mr, array15, num23);
												}
												catch
												{
												}
												if (array16 == null || array16.Length == 0)
												{
													array11[num26] = mr.Offset2Rva((int)(unchecked((long)num32) + num2));
													array12[num26] = num33;
													array13[num26] = position3;
													array14[num26] = unchecked((long)(checked(num31 + 12)));
													num27 = num27 + num31 + 12;
													num26++;
												}
												else
												{
													byte b3 = (byte)array16.Length;
													b3 = (byte)(b3 << 2);
													b3 += 2;
													array8[num24] = new byte[1 + array16.Length];
													array10[num24] = unchecked((long)num32) + num2;
													array8[num24][0] = b3;
													Array.Copy(array16, 0, array8[num24], 1, array16.Length);
													array9[num24] = new byte[0];
													num24++;
												}
											}
											else
											{
												int num35 = (num23 + 1) * num;
												reader2.BaseStream.Position = unchecked((long)num35) + num2;
												int num36 = reader2.ReadInt32();
												int num37 = num36 - num33;
												if (num37 >= num31 + 1)
												{
													if (num31 > 7 && array15[0] == 56 && array15[1] == 2 && array15[2] == 0 && array15[3] == 0 && array15[4] == 0)
													{
														array15[0] = 0;
														array15[1] = 0;
														array15[2] = 0;
														array15[3] = 0;
														array15[4] = 0;
														array15[5] = 0;
														array15[6] = 0;
													}
													byte b3 = (byte)num31;
													b3 = (byte)(b3 << 2);
													b3 += 2;
													Marshal.Copy(array15, 0, (IntPtr)((void*)(ptr + num34 + 1)), array15.Length);
													Marshal.WriteByte((IntPtr)((void*)(ptr + num34)), b3);
												}
												else
												{
													int num38 = 0;
													if (num31 > 8 && array15[0] == 56 && array15[1] == 2 && array15[2] == 0 && array15[3] == 0 && array15[4] == 0)
													{
														num38 = 7;
														l = 7;
														while (array15[l] == 0)
														{
															num38++;
															l++;
														}
													}
													byte b3 = (byte)(num31 - num38);
													b3 = (byte)(b3 << 2);
													b3 += 2;
													array8[num24] = new byte[1 + num31 - num38];
													array10[num24] = unchecked((long)num32) + num2;
													array8[num24][0] = b3;
													Array.Copy(array15, num38, array8[num24], 1, num31 - num38);
													array9[num24] = new byte[0];
													num24++;
												}
											}
										}
										else if (b2 == 3 || (b2 == 2 && num31 > 63))
										{
											if (this.checkBox1.Checked && flag2)
											{
												byte[] array16 = ASMtoIL.Translate(this.checkBox4.Checked, mr, array15, num23);
												if (array16 == null || array16.Length == 0)
												{
													array11[num26] = mr.Offset2Rva((int)(unchecked((long)num32) + num2));
													array12[num26] = num33;
													array13[num26] = position3;
													array14[num26] = unchecked((long)(checked(num31 + 12)));
													num27 = num27 + num31 + 12;
													num26++;
												}
												else
												{
													int num39;
													int value;
													if (b2 == 3)
													{
														num39 = Marshal.ReadInt32((IntPtr)((void*)ptr), (int)num34);
														value = Marshal.ReadInt32((IntPtr)((void*)ptr), (int)num34 + 8);
													}
													else
													{
														num39 = 536579;
														value = 0;
													}
													array8[num24] = new byte[12 + array16.Length];
													array10[num24] = unchecked((long)num32) + num2;
													byte[] bytes = BitConverter.GetBytes(num39);
													Array.Copy(bytes, 0, array8[num24], 0, 4);
													bytes = BitConverter.GetBytes(array16.Length);
													Array.Copy(bytes, 0, array8[num24], 4, 4);
													bytes = BitConverter.GetBytes(value);
													Array.Copy(bytes, 0, array8[num24], 8, 4);
													Array.Copy(array16, 0, array8[num24], 12, array16.Length);
													array9[num24] = new byte[0];
													num24++;
												}
											}
											else
											{
												int num35 = (num23 + 1) * num;
												reader2.BaseStream.Position = unchecked((long)num35) + num2;
												int num36 = reader2.ReadInt32();
												int num37 = num36 - num33;
												if (num36 != 0 && num37 >= num31 + 12)
												{
													if (num31 > 7 && array15[0] == 56 && array15[1] == 2 && array15[2] == 0 && array15[3] == 0 && array15[4] == 0)
													{
														array15[0] = 0;
														array15[1] = 0;
														array15[2] = 0;
														array15[3] = 0;
														array15[4] = 0;
														array15[5] = 0;
														array15[6] = 0;
													}
													Marshal.Copy(array15, 0, (IntPtr)((void*)(ptr + num34 + 12)), array15.Length);
													Marshal.WriteInt32((IntPtr)((void*)(ptr + num34 + 4)), array15.Length);
												}
												else
												{
													int num39 = 0;
													int value = 0;
													byte b4 = 0;
													if (b2 == 3)
													{
														num39 = Marshal.ReadInt32((IntPtr)((void*)ptr), (int)num34);
														value = Marshal.ReadInt32((IntPtr)((void*)ptr), (int)num34 + 8);
														b4 = (byte)(num39 & 8);
													}
													else if (b2 == 2 && num31 > 63)
													{
														num39 = 536579;
														value = 0;
													}
													int num38 = 0;
													if (b4 != 8 && num31 > 8 && array15[0] == 56 && array15[1] == 2 && array15[2] == 0 && array15[3] == 0 && array15[4] == 0)
													{
														num38 = 7;
														l = 7;
														while (array15[l] == 0)
														{
															num38++;
															l++;
														}
													}
													array8[num24] = new byte[12 + num31 - num38];
													array10[num24] = unchecked((long)num32) + num2;
													byte[] bytes = BitConverter.GetBytes(num39);
													Array.Copy(bytes, 0, array8[num24], 0, 4);
													bytes = BitConverter.GetBytes(num31 - num38);
													Array.Copy(bytes, 0, array8[num24], 4, 4);
													bytes = BitConverter.GetBytes(value);
													Array.Copy(bytes, 0, array8[num24], 8, 4);
													if (b4 == 8 && num31 > 7 && array15[0] == 56 && array15[1] == 2 && array15[2] == 0 && array15[3] == 0 && array15[4] == 0)
													{
														array15[0] = 0;
														array15[1] = 0;
														array15[2] = 0;
														array15[3] = 0;
														array15[4] = 0;
														array15[5] = 0;
														array15[6] = 0;
													}
													Array.Copy(array15, num38, array8[num24], 12, num31 - num38);
													int num40 = Marshal.ReadInt32((IntPtr)((void*)ptr), (int)num34 + 4);
													if (b4 == 8 && num40 > 0 && unchecked((long)num40 < (long)((ulong)num4)))
													{
														num40 = (int)num34 + num40 + 12;
														num40 += num40 % 4;
														byte b5 = Marshal.ReadByte((IntPtr)((void*)ptr), num40);
														b5 &= 64;
														if (b5 == 64)
														{
															int num41 = Marshal.ReadInt32((IntPtr)((void*)ptr), num40 + 1);
															num41 &= 16777215;
															int num42 = (num41 - 4) % 24;
															if (num41 != 0 && num42 != 0)
															{
																num41 += 24 - num42;
															}
															reader2.BaseStream.Position = unchecked((long)num40);
															array9[num24] = reader2.ReadBytes(num41);
															num25 += num41;
														}
														else
														{
															int num41 = (int)Marshal.ReadByte((IntPtr)((void*)ptr), num40 + 1);
															int num42 = (num41 - 4) % 12;
															if (num41 != 0 && num42 != 0)
															{
																num41 += 12 - num42;
															}
															reader2.BaseStream.Position = unchecked((long)num40);
															array9[num24] = reader2.ReadBytes(num41);
															num25 += num41;
														}
													}
													else
													{
														array9[num24] = new byte[0];
													}
													num24++;
												}
											}
										}
									}
									goto IL_13F9;
								}
							}
						}
						for (int j = 0; j < mr.TableLengths[6]; j++)
						{
							if (mr.tables[6].members[j][0] != 0L)
							{
								int num20 = (int)mr.tables[6].members[j][0];
								uint num29 = (uint)mr.Rva2Offset((int)mr.tables[6].members[j][0]);
								reader2.BaseStream.Position = (long)(unchecked((ulong)num29));
								byte b = reader2.ReadByte();
								b &= 3;
								if (b == 2)
								{
									num20++;
								}
								if (b == 3)
								{
									num20 += 12;
								}
								if (num20 == num28)
								{
									num23 = j;
									break;
								}
							}
						}
						goto IL_ADD;
					}
					byte[] destinationArray = new byte[buffer5.Length];
					Array.Copy(buffer5, destinationArray, buffer5.Length);
					uint num43 = (uint)(12 + num26 * 8 + num27);
					IntPtr intPtr4 = Marshal.AllocHGlobal((IntPtr)((long)(unchecked((ulong)num43))));
					MainForm.ZeroMemory(intPtr4, (int)num43);
					Marshal.WriteInt32(intPtr4, 0, num26);
					for (int j = 0; j < num26; j++)
					{
						Marshal.WriteInt32(intPtr4, j * 8 + 8, array11[j]);
						Marshal.WriteInt32(intPtr4, j * 8 + 8 + 4, array12[j]);
					}
					Marshal.WriteInt32(intPtr4, 8 + num26 * 8, array6.Length);
					long num44 = 0L;
					for (int j = 0; j < num26; j++)
					{
						long value2 = (long)intPtr4 + 8L + 4L + unchecked((long)(checked(num26 * 8))) + num44;
						long num45 = array14[j];
						Marshal.Copy(buffer5, (int)array13[j], (IntPtr)value2, (int)num45);
						num44 += array14[j];
					}
					byte[] array17 = new byte[num43];
					Marshal.Copy(intPtr4, array17, 0, (int)num43);
					if (IsR4310)
					{
						int num5 = array17.Length / 8;
						fixed (byte* ptr3 = array17)
						{
							int j = 0;
							do
							{
								int* ptr4 = unchecked((int*)ptr3) + unchecked((IntPtr)j) * 8 / 4;
								*ptr4 ^= XorValue;
								j++;
							}
							while (j < num5);
						}
					}
					Marshal.FreeHGlobal(intPtr4);
					MemoryStream memoryStream2 = new MemoryStream();
					Rijndael rijndael2 = Rijndael.Create();
					rijndael2.Key = buffer2;
					rijndael2.IV = buffer3;
					CryptoStream cryptoStream2 = new CryptoStream(memoryStream2, rijndael2.CreateEncryptor(), CryptoStreamMode.Write);
					cryptoStream2.Write(array17, 0, array17.Length);
					cryptoStream2.Close();
					byte[] array18 = memoryStream2.ToArray();
					memoryStream2.Close();
					uint num46 = (uint)mr.tables[40].members[resindex][0] + (uint)mr.Rva2Offset(mr.netdir.ResourceRVA);
					uint num47 = (uint)((int)intPtr3) + num46;
					int num48;
					unchecked
					{
						num48 = Marshal.ReadInt32((IntPtr)((long)((ulong)num47)), 0);
						Marshal.Copy(array18, 0, (IntPtr)((long)((ulong)(checked(num47 + 4u)))), array18.Length);
						Marshal.WriteInt32((IntPtr)((long)((ulong)num47)), 0, array18.Length);
					}
					if (ResPatchRva != 0 && num26 != 0 && this.checkBox5.Checked)
					{
						uint num49 = (uint)mr.Rva2Offset(ResPatchRva);
						byte[] array3 = new byte[3];
						byte[] array4 = array3;
						Marshal.Copy(array4, 0, (IntPtr)((void*)(ptr + num49)), array4.Length);
					}
					if (num26 == 0 && this.checkBox6.Checked)
					{
						uint num19 = (uint)mr.Rva2Offset((int)DecryptionMethodRVA);
						byte[] array3 = new byte[4];
						array3[0] = 1;
						byte[] array4 = array3;
						Marshal.Copy(array4, 0, (IntPtr)((void*)(ptr + num19 - 8)), array4.Length);
						array4 = new byte[]
						{
							42
						};
						Marshal.Copy(array4, 0, (IntPtr)((void*)(ptr + num19)), array4.Length);
					}
					num25 += array18.Length;
					if (num48 > num25)
					{
						long num50 = 0L;
						uint num51 = (uint)mr.Offset2Rva((int)num46 + 4 + array18.Length);
						byte[] array3 = new byte[3];
						byte[] array19 = array3;
						for (int j = 0; j < num24; j++)
						{
							Marshal.Copy(array8[j], 0, (IntPtr)((long)(unchecked((ulong)(checked(num47 + 4u))) + (ulong)(unchecked((long)array18.Length)) + (ulong)num50)), array8[j].Length);
							int num52 = (int)(unchecked((ulong)num51) + (ulong)num50);
							Marshal.WriteInt32((IntPtr)((void*)ptr), (int)array10[j], num52);
							num50 += unchecked((long)array8[j].Length);
							if (array9[j].Length != 0)
							{
								int num53 = num52 + array8[j].Length;
								num53 %= 4;
								if (num53 != 0)
								{
									num53 = 4 - num53;
								}
								Marshal.Copy(array19, 0, (IntPtr)((long)(unchecked((ulong)(checked(num47 + 4u))) + (ulong)(unchecked((long)array18.Length)) + (ulong)num50)), num53);
								num50 += unchecked((long)num53);
								Marshal.Copy(array9[j], 0, (IntPtr)((long)(unchecked((ulong)(checked(num47 + 4u))) + (ulong)(unchecked((long)array18.Length)) + (ulong)num50)), array9[j].Length);
								num50 += unchecked((long)array9[j].Length);
							}
						}
						MainForm.UnmapViewOfFile(intPtr3);
						MainForm.CloseHandle((uint)((int)intPtr2));
						MainForm.CloseHandle((uint)((int)intPtr));
					}
					else
					{
						int fileAlignment = mr.inh.ioh.FileAlignment;
						int numberOfSections = (int)mr.inh.ifh.NumberOfSections;
						long num54;
						long num56;
						long num57;
						unchecked
						{
							num54 = (long)mr.sections[checked(numberOfSections - 1)].size_of_raw_data;
							long num55 = (long)mr.sections[checked(numberOfSections - 1)].virtual_address;
							num56 = (long)(checked(mr.sections[numberOfSections - 1].pointer_to_raw_data + mr.sections[numberOfSections - 1].size_of_raw_data));
							num57 = (long)(checked(mr.idh.e_lfanew + sizeof(MetadataReader.IMAGE_NT_HEADERS) + 8 + sizeof(MetadataReader.image_section_header) * (int)(mr.inh.ifh.NumberOfSections - 1) + 16));
						}
						long num58 = (long)(unchecked((ulong)num4) - (ulong)num56);
						int num52 = mr.Offset2Rva((int)(num56 - 1L)) + 1;
						num52 += (int)num58;
						uint num59 = MainForm.SetFilePointer(intPtr, 0, 0, MainForm.EMoveMethod.End);
						long num60 = num58;
						byte[] array3 = new byte[3];
						byte[] array19 = array3;
						long num50 = 0L;
						for (int j = 0; j < num24; j++)
						{
							uint num61;
							bool flag3 = MainForm.WriteFile(intPtr, array8[j], (uint)array8[j].Length, out num61, 0);
							Marshal.WriteInt32((IntPtr)((void*)ptr), (int)array10[j], num52 + (int)num50);
							num50 += unchecked((long)array8[j].Length);
							num60 += unchecked((long)array8[j].Length);
							if (array9[j].Length != 0)
							{
								int num53 = num52 + array8[j].Length;
								num53 %= 4;
								if (num53 != 0)
								{
									num53 = 4 - num53;
								}
								flag3 = MainForm.WriteFile(intPtr, array19, (uint)num53, out num61, 0);
								num50 += unchecked((long)num53);
								num60 += unchecked((long)num53);
								flag3 = MainForm.WriteFile(intPtr, array9[j], (uint)array9[j].Length, out num61, 0);
								num50 += unchecked((long)array9[j].Length);
								num60 += unchecked((long)array9[j].Length);
							}
						}
						int num62 = fileAlignment - (int)num60 % fileAlignment;
						num60 += unchecked((long)num62);
						uint num63 = MainForm.VirtualAlloc(0u, (uint)num62, MainForm.MEM_COMMIT, MainForm.PAGE_EXECUTE_READWRITE);
						unchecked
						{
							MainForm.ZeroMemory((IntPtr)((long)((ulong)num63)), num62);
							uint num61;
							bool flag3 = MainForm.WriteFile(intPtr, (IntPtr)((long)((ulong)num63)), checked((uint)num62), out num61, 0);
							MainForm.VirtualFree((IntPtr)((long)((ulong)num63)), 0u, MainForm.MEM_RELEASE);
							MainForm.UnmapViewOfFile(intPtr3);
						}
						MainForm.CloseHandle((uint)((int)intPtr2));
						if ((int)intPtr != -1)
						{
							MainForm.CloseHandle((uint)((int)intPtr));
						}
						intPtr = MainForm.CreateFile(text2, 3221225472u, 0u, IntPtr.Zero, 4u, 0u, IntPtr.Zero);
						num4 = MainForm.GetFileSize(intPtr, IntPtr.Zero);
						intPtr2 = MainForm.CreateFileMapping((int)intPtr, IntPtr.Zero, 4u, 0u, num4, "MyOwnFileMapping");
						intPtr3 = MainForm.MapViewOfFile(intPtr2, 6u, 0u, 0u, 0u);
						ptr = (byte*)intPtr3.ToPointer();
						Marshal.WriteInt32((IntPtr)((void*)ptr), (int)num57, (int)num60 + (int)num54);
						int num64 = Marshal.ReadInt32((IntPtr)((void*)ptr), (int)num57 - 8);
						int num65 = (int)num60 + (int)num54;
						Marshal.WriteInt32((IntPtr)((void*)ptr), (int)num57 - 8, num65);
						int num66 = num64 % sectionAlignment;
						if (num66 != 0)
						{
							num66 = sectionAlignment - num66;
						}
						num64 += num66;
						num66 = num65 % sectionAlignment;
						if (num66 != 0)
						{
							num66 = sectionAlignment - num66;
						}
						num65 += num66;
						int val2 = mr.inh.ioh.SizeOfImage + num65 - num64;
						long num67 = unchecked((long)(checked(mr.idh.e_lfanew + 80)));
						Marshal.WriteInt32((IntPtr)((void*)ptr), (int)num67, val2);
						MainForm.UnmapViewOfFile(intPtr3);
						MainForm.CloseHandle((uint)((int)intPtr2));
						if ((int)intPtr != -1)
						{
							MainForm.CloseHandle((uint)((int)intPtr));
						}
					}
					if (IsR4310)
					{
						text += " Reactor 4.3.1.0, ";
					}
					else
					{
						text += " Reactor 4.x, ";
					}
					if (num26 == 0)
					{
						text += "no undecrypted method left!";
					}
					else
					{
						text = text + Convert.ToString(num26) + " undecrypted methods left!";
					}
				}
				IL_1CD0:
				this.label2.ForeColor = Color.Blue;
				this.label2.Text = "Succes!" + text;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00007544 File Offset: 0x00006544
		public static bool BytesEqual(byte[] Array1, byte[] Array2)
		{
			checked
			{
				bool result;
				if (Array1.Length != Array2.Length)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < Array1.Length; i++)
					{
						if (Array1[i] != Array2[i])
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
				return result;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000758C File Offset: 0x0000658C
		public static bool BytesPartialEqual(byte[] Array1, byte[] Array2)
		{
			int num;
			if (Array1.Length >= Array2.Length)
			{
				num = Array2.Length;
			}
			else
			{
				num = Array1.Length;
			}
			checked
			{
				bool result;
				for (int i = 0; i < num; i++)
				{
					if (Array1[i] != Array2[i])
					{
						result = false;
						return result;
					}
				}
				result = true;
				return result;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000075DC File Offset: 0x000065DC
		public static bool BytesPartialSPEqual(byte[] Array1, byte[] Array2)
		{
			int num;
			if (Array1.Length >= Array2.Length)
			{
				num = Array2.Length;
			}
			else
			{
				num = Array1.Length;
			}
			checked
			{
				bool result;
				for (int i = 0; i < num; i++)
				{
					if (Array2[i] != 255 && Array1[i] != Array2[i])
					{
						result = false;
						return result;
					}
				}
				result = true;
				return result;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00007638 File Offset: 0x00006638
		public static int CountStringOccurrences(string text, string pattern)
		{
			int num = 0;
			int num2 = 0;
			checked
			{
				while ((num2 = text.IndexOf(pattern, num2)) != -1)
				{
					num2 += pattern.Length;
					num++;
				}
				return num;
			}
		}

		// Token: 0x06000043 RID: 67
		[DllImport("kernel32")]
		private static extern void GetSystemInfo(ref MainForm.SYSTEM_INFO pSI);

		// Token: 0x06000044 RID: 68 RVA: 0x00007674 File Offset: 0x00006674
		private static void InitData()
		{
			try
			{
				MainForm.SYSTEM_INFO sYSTEM_INFO = default(MainForm.SYSTEM_INFO);
				MainForm.GetSystemInfo(ref sYSTEM_INFO);
				MainForm.minaddress = sYSTEM_INFO.lpMinimumApplicationAddress;
				MainForm.maxaddress = sYSTEM_INFO.lpMaximumApplicationAddress;
				MainForm.pagesize = sYSTEM_INFO.dwPageSize;
			}
			catch
			{
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000076EC File Offset: 0x000066EC
		private static uint SecondSearch(IntPtr handle, IntPtr basem)
		{
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				0,
				17,
				17,
				17,
				17,
				32,
				0,
				0,
				0
			};
			byte[] array2 = new byte[]
			{
				0,
				0,
				0,
				0,
				17,
				17,
				17,
				17,
				16,
				0,
				0,
				0
			};
			uint num = MainForm.pagesize;
			checked
			{
				uint num2 = MainForm.pagesize + 56u;
				byte[] array3 = new byte[num + 56u];
				uint num3 = (uint)((int)basem);
				if (num3 == 0u)
				{
					num3 = MainForm.minaddress;
				}
				uint result;
				for (uint num4 = num3; num4 < 1342177280u; num4 += num)
				{
					IntPtr intPtr;
					int num5 = MainForm.ReadProcessMemory(handle, (IntPtr)((long)(unchecked((ulong)num4))), array3, num2, out intPtr);
					if (num5 != 1)
					{
						num5 = MainForm.ReadProcessMemory(handle, (IntPtr)((long)(unchecked((ulong)num4))), array3, num2 - 56u, out intPtr);
					}
					if (num5 == 1)
					{
						uint num6 = 0u;
						while (unchecked((ulong)num6 < (ulong)((long)(checked(array3.Length - 56)))))
						{
							bool flag = true;
							for (int i = 0; i < array.Length; i++)
							{
								if (i <= 3 || i >= 8)
								{
									if (array3[(int)((IntPtr)(unchecked((ulong)num6) + (ulong)(unchecked((long)i))))] != array[i])
									{
										flag = false;
										break;
									}
								}
							}
							for (int j = 0; j < array2.Length; j++)
							{
								if (j <= 3 || j >= 8)
								{
									if (array3[(int)((IntPtr)(unchecked((ulong)num6) + (ulong)(unchecked((long)j)) + 44uL))] != array2[j])
									{
										flag = false;
										break;
									}
								}
							}
							if (flag)
							{
								uint num7 = num6 + num4;
								result = num7;
								return result;
							}
							num6 += 1u;
						}
					}
				}
				result = 0u;
				return result;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000078AF File Offset: 0x000068AF
		private void Button3Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000078B8 File Offset: 0x000068B8
		private void TextBox1DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				Array array = (Array)e.Data.GetData(DataFormats.FileDrop);
				if (array != null)
				{
					string text = array.GetValue(0).ToString();
					int num = text.LastIndexOf(".");
					if (num != -1)
					{
						string text2 = text.Substring(num);
						text2.ToLower();
						if (text2 == ".exe" || text2 == ".dll")
						{
							base.Activate();
							this.textBox1.Text = text;
							int num2 = text.LastIndexOf("\\");
							if (num2 != -1)
							{
								this.DirectoryName = text.Remove(num2, checked(text.Length - num2));
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000079A4 File Offset: 0x000069A4
		private void TextBox1DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000079DB File Offset: 0x000069DB
		private void MainFormLoad(object sender, EventArgs e)
		{
		}

		// Token: 0x04000096 RID: 150
		private const int STD_OUTPUT_HANDLE = -11;

		// Token: 0x04000097 RID: 151
		private const int HANDLE_FLAG_INHERIT = 1;

		// Token: 0x04000098 RID: 152
		private const int OPEN_EXISTING = 3;

		// Token: 0x04000099 RID: 153
		private const int INVALID_HANDLE_VALUE = -1;

		// Token: 0x0400009A RID: 154
		private const int FILE_MAP_WRITE = 2;

		// Token: 0x0400009B RID: 155
		private const int FILE_MAP_READ = 4;

		// Token: 0x0400009C RID: 156
		private const int GENERIC_WRITE = 1073741824;

		// Token: 0x0400009D RID: 157
		private const long GENERIC_READ = 2147483648L;

		// Token: 0x0400009E RID: 158
		private const long GENERIC_READWRITE = 3221225472L;

		// Token: 0x0400009F RID: 159
		private const int OPEN_ALWAYS = 4;

		// Token: 0x040000A0 RID: 160
		public const int SECT_EH_HEADER_SIZE = 4;

		// Token: 0x040000A1 RID: 161
		public const int SECT_EH_CLAUSE_FAT_SIZE = 24;

		// Token: 0x040000A2 RID: 162
		public const int SECT_EH_CLAUSE_SMALL_SIZE = 12;

		// Token: 0x040000A3 RID: 163
		public static uint MEM_COMMIT = 4096u;

		// Token: 0x040000A4 RID: 164
		public static uint PAGE_EXECUTE_READWRITE = 64u;

		// Token: 0x040000A5 RID: 165
		public static uint MEM_RELEASE = 32768u;

		// Token: 0x040000A6 RID: 166
		public string DirectoryName = "";

		// Token: 0x040000A7 RID: 167
		private static uint minaddress = 0u;

		// Token: 0x040000A8 RID: 168
		private static uint maxaddress = 4026531840u;

		// Token: 0x040000A9 RID: 169
		private static uint pagesize = 4096u;

		// Token: 0x02000010 RID: 16
		public enum EMoveMethod : uint
		{
			// Token: 0x040000BF RID: 191
			Begin,
			// Token: 0x040000C0 RID: 192
			Current,
			// Token: 0x040000C1 RID: 193
			End
		}

		// Token: 0x02000011 RID: 17
		public enum FileAccess
		{
			// Token: 0x040000C3 RID: 195
			ReadOnly = 2,
			// Token: 0x040000C4 RID: 196
			ReadWrite = 4
		}

		// Token: 0x02000012 RID: 18
		[Flags]
		public enum ThreadAccess
		{
			// Token: 0x040000C6 RID: 198
			TERMINATE = 1,
			// Token: 0x040000C7 RID: 199
			SUSPEND_RESUME = 2,
			// Token: 0x040000C8 RID: 200
			GET_CONTEXT = 8,
			// Token: 0x040000C9 RID: 201
			SET_CONTEXT = 16,
			// Token: 0x040000CA RID: 202
			SET_INFORMATION = 32,
			// Token: 0x040000CB RID: 203
			QUERY_INFORMATION = 64,
			// Token: 0x040000CC RID: 204
			SET_THREAD_TOKEN = 128,
			// Token: 0x040000CD RID: 205
			IMPERSONATE = 256,
			// Token: 0x040000CE RID: 206
			DIRECT_IMPERSONATION = 512
		}

		// Token: 0x02000013 RID: 19
		public class InitializerInvoker
		{
			// Token: 0x0600004D RID: 77
			[DllImport("kernel32.dll")]
			private static extern uint GetCurrentThreadId();

			// Token: 0x0600004E RID: 78 RVA: 0x000086DB File Offset: 0x000076DB
			public InitializerInvoker(Assembly iasm, int imethodcount)
			{
				this.casm = iasm;
				this.methodcount = imethodcount;
			}

			// Token: 0x0600004F RID: 79 RVA: 0x000086F4 File Offset: 0x000076F4
			public void Initialize()
			{
				checked
				{
					try
					{
						MainForm.InitializerInvoker.threadid = MainForm.InitializerInvoker.GetCurrentThreadId();
						int num = 100663296;
						for (int i = 0; i < this.methodcount; i++)
						{
							num++;
							try
							{
								MethodBase methodBase = this.casm.ManifestModule.ResolveMethod(num);
								RuntimeHelpers.PrepareMethod(methodBase.MethodHandle, new RuntimeTypeHandle[]
								{
									typeof(object).TypeHandle
								});
							}
							catch
							{
							}
						}
					}
					catch
					{
					}
				}
			}

			// Token: 0x040000CF RID: 207
			public static uint threadid;

			// Token: 0x040000D0 RID: 208
			public static string errormessage;

			// Token: 0x040000D1 RID: 209
			public Assembly casm;

			// Token: 0x040000D2 RID: 210
			public int methodcount;

			// Token: 0x040000D3 RID: 211
			private ConstructorInfo intinitializer;
		}

		// Token: 0x02000014 RID: 20
		[Flags]
		public enum AllocationType
		{
			// Token: 0x040000D5 RID: 213
			Commit = 4096,
			// Token: 0x040000D6 RID: 214
			Reserve = 8192,
			// Token: 0x040000D7 RID: 215
			Decommit = 16384,
			// Token: 0x040000D8 RID: 216
			Release = 32768,
			// Token: 0x040000D9 RID: 217
			Reset = 524288,
			// Token: 0x040000DA RID: 218
			Physical = 4194304,
			// Token: 0x040000DB RID: 219
			TopDown = 1048576,
			// Token: 0x040000DC RID: 220
			WriteWatch = 2097152,
			// Token: 0x040000DD RID: 221
			LargePages = 536870912
		}

		// Token: 0x02000015 RID: 21
		[Flags]
		public enum MemoryProtection
		{
			// Token: 0x040000DF RID: 223
			Execute = 16,
			// Token: 0x040000E0 RID: 224
			ExecuteRead = 32,
			// Token: 0x040000E1 RID: 225
			ExecuteReadWrite = 64,
			// Token: 0x040000E2 RID: 226
			ExecuteWriteCopy = 128,
			// Token: 0x040000E3 RID: 227
			NoAccess = 1,
			// Token: 0x040000E4 RID: 228
			ReadOnly = 2,
			// Token: 0x040000E5 RID: 229
			ReadWrite = 4,
			// Token: 0x040000E6 RID: 230
			WriteCopy = 8,
			// Token: 0x040000E7 RID: 231
			GuardModifierflag = 256,
			// Token: 0x040000E8 RID: 232
			NoCacheModifierflag = 512,
			// Token: 0x040000E9 RID: 233
			WriteCombineModifierflag = 1024
		}

		// Token: 0x02000016 RID: 22
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct PROCESS_BASIC_INFORMATION
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000050 RID: 80 RVA: 0x000087A0 File Offset: 0x000077A0
			public int Size
			{
				get
				{
					return 24;
				}
			}

			// Token: 0x040000EA RID: 234
			public int ExitStatus;

			// Token: 0x040000EB RID: 235
			public int PebBaseAddress;

			// Token: 0x040000EC RID: 236
			public int AffinityMask;

			// Token: 0x040000ED RID: 237
			public int BasePriority;

			// Token: 0x040000EE RID: 238
			public int UniqueProcessId;

			// Token: 0x040000EF RID: 239
			public int InheritedFromUniqueProcessId;
		}

		// Token: 0x02000017 RID: 23
		private struct SYSTEM_INFO
		{
			// Token: 0x040000F0 RID: 240
			public uint dwOemId;

			// Token: 0x040000F1 RID: 241
			public uint dwPageSize;

			// Token: 0x040000F2 RID: 242
			public uint lpMinimumApplicationAddress;

			// Token: 0x040000F3 RID: 243
			public uint lpMaximumApplicationAddress;

			// Token: 0x040000F4 RID: 244
			public uint dwActiveProcessorMask;

			// Token: 0x040000F5 RID: 245
			public uint dwNumberOfProcessors;

			// Token: 0x040000F6 RID: 246
			public uint dwProcessorType;

			// Token: 0x040000F7 RID: 247
			public uint dwAllocationGranularity;

			// Token: 0x040000F8 RID: 248
			public uint dwProcessorLevel;

			// Token: 0x040000F9 RID: 249
			public uint dwProcessorRevision;
		}
	}
}
