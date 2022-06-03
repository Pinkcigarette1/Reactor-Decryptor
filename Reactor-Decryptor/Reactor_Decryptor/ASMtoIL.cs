using System;

namespace Reactor_Decryptor
{
	// Token: 0x02000003 RID: 3
	public class ASMtoIL
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		private static byte GetSizeOfElement(CorElementType elenttype)
		{
			byte result;
			if (elenttype == CorElementType.ELEMENT_TYPE_BOOLEAN)
			{
				result = 1;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_I1)
			{
				result = 1;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_U1)
			{
				result = 1;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_CHAR)
			{
				result = 2;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_I2)
			{
				result = 2;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_U2)
			{
				result = 2;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_I8)
			{
				result = 8;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_U8)
			{
				result = 8;
			}
			else if (elenttype == CorElementType.ELEMENT_TYPE_R8)
			{
				result = 8;
			}
			else
			{
				result = 4;
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000010F0
		public static void q_sort(byte[] sorted, byte left, byte right)
		{
			byte b = left;
			byte b2 = right;
			byte b3 = sorted[(int)left];
			checked
			{
				while (left < right)
				{
					while (sorted[(int)right] >= b3 && left < right)
					{
						right -= 1;
					}
					if (left != right)
					{
						sorted[(int)left] = sorted[(int)right];
						left += 1;
					}
					while (sorted[(int)left] <= b3 && left < right)
					{
						left += 1;
					}
					if (left != right)
					{
						sorted[(int)right] = sorted[(int)left];
						right -= 1;
					}
				}
				sorted[(int)left] = b3;
				b3 = left;
				left = b;
				right = b2;
				if (left < b3)
				{
					ASMtoIL.q_sort(sorted, left, b3 - 1);
				}
				if (right > b3)
				{
					ASMtoIL.q_sort(sorted, b3 + 1, right);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021B4 File Offset: 0x000011B4
		private static byte ASMSJumpToIL(int index)
		{
			return ASMtoIL.jumptabletran[index];
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021D0 File Offset: 0x000011D0
		private static byte ASMShiftToIL(int index)
		{
			byte result;
			if (index == 1)
			{
				result = 98;
			}
			else if (index == 9)
			{
				result = 100;
			}
			else if (index == 25)
			{
				result = 99;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002218 File Offset: 0x00001218
		private static byte[] LoadInteger(int targetvalue)
		{
			checked
			{
				byte[] result;
				if (targetvalue == 0)
				{
					result = new byte[]
					{
						22
					};
				}
				else if (targetvalue == 1)
				{
					result = new byte[]
					{
						23
					};
				}
				else if (targetvalue == 2)
				{
					result = new byte[]
					{
						24
					};
				}
				else if (targetvalue == 3)
				{
					result = new byte[]
					{
						25
					};
				}
				else if (targetvalue == 4)
				{
					result = new byte[]
					{
						26
					};
				}
				else if (targetvalue == 5)
				{
					result = new byte[]
					{
						27
					};
				}
				else if (targetvalue == 6)
				{
					result = new byte[]
					{
						28
					};
				}
				else if (targetvalue == 7)
				{
					result = new byte[]
					{
						29
					};
				}
				else if (targetvalue == 8)
				{
					result = new byte[]
					{
						30
					};
				}
				else if (targetvalue == -1)
				{
					result = new byte[]
					{
						21
					};
				}
				else if (targetvalue >= -128 && targetvalue <= 127)
				{
					byte[] array = new byte[]
					{
						31,
						(byte)targetvalue
					};
					result = array;
				}
				else
				{
					byte[] array = new byte[5];
					array[0] = 32;
					byte[] bytes = BitConverter.GetBytes(targetvalue);
					for (int i = 0; i < bytes.Length; i++)
					{
						array[i + 1] = bytes[i];
					}
					result = array;
				}
				return result;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023E8 File Offset: 0x000013E8
		private static bool IsCmpFieldConst(byte[] tagetasm, int insindex)
		{
			int num = insindex;
			checked
			{
				if (tagetasm[num] == 102)
				{
					num++;
				}
				bool result;
				if (tagetasm[num] == 128 || tagetasm[num] == 129 || tagetasm[num] == 131)
				{
					if (tagetasm[num + 1] == 57 || tagetasm[num + 1] == 121 || tagetasm[num + 1] == 185)
					{
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002468 File Offset: 0x00001468
		private static bool IsStfld(byte[] tagetasm, int insindex)
		{
			int num = insindex;
			checked
			{
				if (tagetasm[num] == 102)
				{
					num++;
				}
				bool result;
				if (tagetasm[num] == 136 || tagetasm[num] == 137)
				{
					if (tagetasm[num + 1] == 1 || tagetasm[num + 1] == 65 || tagetasm[num + 1] == 129)
					{
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000024DC File Offset: 0x000014DC
		private static bool IsLoadField(byte[] tagetasm, int insindex)
		{
			bool flag = false;
			int num = insindex;
			checked
			{
				if (tagetasm[num] == 102)
				{
					num++;
				}
				if (tagetasm[num] == 15)
				{
					flag = true;
					num++;
				}
				bool result;
				if (tagetasm[num] == 138 || tagetasm[num] == 139 || (flag && (tagetasm[num] == 190 || tagetasm[num] == 191 || tagetasm[num] == 182 || tagetasm[num] == 183)))
				{
					if (tagetasm[num + 1] == 1 || tagetasm[num + 1] == 17 || tagetasm[num + 1] == 65 || tagetasm[num + 1] == 81 || tagetasm[num + 1] == 129 || tagetasm[num + 1] == 145)
					{
						result = true;
						return result;
					}
				}
				else if (tagetasm[num] == 221)
				{
					if (tagetasm[num + 1] == 1 || tagetasm[num + 1] == 65 || tagetasm[num + 1] == 129)
					{
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000025F8 File Offset: 0x000015F8
		private static bool IsLoadOnEdx(byte[] tagetasm, int insindex)
		{
			bool flag = false;
			int num = insindex;
			checked
			{
				if (tagetasm[num] == 102)
				{
					num++;
				}
				if (tagetasm[num] == 15)
				{
					flag = true;
					num++;
				}
				bool result;
				if (tagetasm[num] == 138 || tagetasm[num] == 139 || (flag && (tagetasm[num] == 190 || tagetasm[num] == 191 || tagetasm[num] == 182 || tagetasm[num] == 183)))
				{
					if (tagetasm[num + 1] == 17 || tagetasm[num + 1] == 81 || tagetasm[num + 1] == 145)
					{
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026B8 File Offset: 0x000016B8
		private static int GetFieldIndex(int varindex)
		{
			byte[] array = new byte[5];
			int num = -1;
			bool flag = false;
			checked
			{
				int i;
				for (i = 0; i < ASMtoIL.mr.TableLengths[2]; i++)
				{
					if (ASMtoIL.mr.tables[2].members[i][5] > unchecked((long)(checked(ASMtoIL.methodindex + 1))))
					{
						i--;
						break;
					}
				}
				int num2 = (int)ASMtoIL.mr.tables[2].members[i][3];
				if (num2 != 0)
				{
					int num3 = num2 >> 2;
					int num4 = num2 & 3;
					if (num4 == 1 && num3 <= ASMtoIL.mr.TableLengths[1])
					{
						string text = "";
						int num5 = (int)ASMtoIL.mr.tables[1].members[num3 - 1][1];
						while (ASMtoIL.mr.Strings[num5] != 0)
						{
							text += (char)ASMtoIL.mr.Strings[num5];
							num5++;
						}
						if (text == "ValueType")
						{
							flag = true;
						}
					}
				}
				int num6 = (int)ASMtoIL.mr.tables[2].members[i][4];
				int num7 = (int)ASMtoIL.mr.tables[2].members[i + 1][4] - num6;
				int result;
				if (num7 == 1)
				{
					result = num6;
				}
				else
				{
					if (!ASMtoIL.DontUseName && num7 > 1)
					{
						string text2 = "";
						string text3 = "";
						char c = 'a';
						int num8 = 0;
						while (c != '\0')
						{
							c = (char)ASMtoIL.mr.Strings[(int)((IntPtr)(ASMtoIL.mr.tables[6].members[ASMtoIL.methodindex][3] + unchecked((long)num8)))];
							if (c != '\0')
							{
								text2 += c;
							}
							num8++;
						}
						text2 = text2.ToLower();
						if (text2.StartsWith("get_"))
						{
							bool flag2 = false;
							text2 = text2.Remove(0, 4);
							for (int j = num6; j < num6 + num7; j++)
							{
								c = 'a';
								num8 = 0;
								while (c != '\0')
								{
									c = (char)ASMtoIL.mr.Strings[(int)((IntPtr)(ASMtoIL.mr.tables[4].members[j - 1][1] + unchecked((long)num8)))];
									if (c != '\0')
									{
										text3 += c;
									}
									num8++;
								}
								text3 = text3.ToLower();
								if (text3 == text2 || text3 == "_" + text2)
								{
									if (num != -1)
									{
										flag2 = true;
									}
									num = j;
								}
								text3 = "";
							}
							if (flag2)
							{
								num = -1;
							}
							if (num != -1)
							{
								result = num + num6;
								return result;
							}
						}
					}
					if (num == -1)
					{
						if (!flag)
						{
							byte[] array2 = new byte[num7];
							byte[] array3 = new byte[5];
							int[] array4 = new int[num7];
							int[] array5 = new int[num7];
							int num9 = 0;
							for (int j = 0; j < num7; j++)
							{
								CorElementType corElementType = (CorElementType)ASMtoIL.mr.Blob[(int)((IntPtr)(ASMtoIL.mr.tables[4].members[j + num6 - 1][2] + 2L))];
								if (corElementType == CorElementType.ELEMENT_TYPE_GENERICINST || corElementType == CorElementType.ELEMENT_TYPE_STRING)
								{
									array2[j] = 0;
								}
								else
								{
									array2[j] = ASMtoIL.GetSizeOfElement(corElementType);
								}
								if (corElementType == CorElementType.ELEMENT_TYPE_VALUETYPE)
								{
									result = 0;
									return result;
								}
								bool flag3 = false;
								for (int k = 0; k < num9; k++)
								{
									if (array3[k] == array2[j])
									{
										flag3 = true;
										break;
									}
								}
								if (!flag3)
								{
									array3[num9] = array2[j];
									num9++;
								}
							}
							byte[] array6 = new byte[num9];
							for (int l = 0; l < array6.Length; l++)
							{
								array6[l] = array3[l];
							}
							array3 = array6;
							ASMtoIL.q_sort(array3, 0, (byte)(array3.Length - 1));
							int num10 = 0;
							int num11 = array3.Length;
							do
							{
								for (int l = 0; l < array2.Length; l++)
								{
									if (array2[l] == array3[num11 - 1])
									{
										array4[num10] = l;
										num10++;
									}
								}
								num11--;
							}
							while (num11 != 0);
							int num12 = 4;
							for (int l = 0; l < array4.Length; l++)
							{
								if (num12 == varindex)
								{
									result = num6 + array4[l];
									return result;
								}
								num12 += (int)array2[array4[l]];
							}
						}
						else
						{
							int num13 = 0;
							for (int j = 0; j < num7; j++)
							{
								if (num13 == varindex)
								{
									result = num6 + j;
									return result;
								}
								CorElementType corElementType = (CorElementType)ASMtoIL.mr.Blob[(int)((IntPtr)(ASMtoIL.mr.tables[4].members[j + num6 - 1][2] + 2L))];
								num13 += (int)ASMtoIL.GetSizeOfElement(corElementType);
							}
						}
					}
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002C8C File Offset: 0x00001C8C
		public static byte[] Translate(bool dontusename, MetadataReader inmr, byte[] tagetasm, int inmethodindex)
		{
			ASMtoIL.DontUseName = dontusename;
			ASMtoIL.mr = inmr;
			ASMtoIL.methodindex = inmethodindex;
			int i = 0;
			ASMtoIL.transIL = new byte[400];
			ASMtoIL.asmindexs = new int[400];
			ASMtoIL.ilindexs = new int[400];
			ASMtoIL.ilpositon = 0;
			ASMtoIL.inscount = 0;
			ASMtoIL.JumpFix[] array = new ASMtoIL.JumpFix[20];
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			checked
			{
				byte[] result;
				while (i < tagetasm.Length)
				{
					ASMtoIL.asmindexs[ASMtoIL.inscount] = i;
					ASMtoIL.ilindexs[ASMtoIL.inscount] = ASMtoIL.ilpositon;
					if (tagetasm[i] == 85 || tagetasm[i] == 156 || tagetasm[i] == 96 || tagetasm[i] == 144 || tagetasm[i] == 93 || tagetasm[i] == 157 || tagetasm[i] == 97)
					{
						i++;
						ASMtoIL.ilindexs[ASMtoIL.inscount] = -1;
					}
					else if (tagetasm[i] == 139 && tagetasm[i + 1] == 236)
					{
						i += 2;
						ASMtoIL.ilindexs[ASMtoIL.inscount] = -1;
					}
					else if (tagetasm[i] == 51 && tagetasm[i + 1] == 192)
					{
						byte b = ASMtoIL.mr.Blob[(int)((IntPtr)(ASMtoIL.mr.tables[6].members[ASMtoIL.methodindex][4] + 3L))];
						if (b < 14)
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon] = 22;
							ASMtoIL.ilpositon++;
						}
						else if (tagetasm[i + 2] == 195 || tagetasm[i + 2] == 194)
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon] = 20;
							ASMtoIL.ilpositon++;
						}
						else
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon] = 22;
							ASMtoIL.ilpositon++;
						}
						i += 2;
					}
					else if (tagetasm[i] == 139 && tagetasm[i + 1] == 193)
					{
						if (!flag2)
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon] = 2;
							ASMtoIL.ilpositon++;
						}
						else
						{
							ASMtoIL.ilindexs[ASMtoIL.inscount] = -1;
						}
						i += 2;
					}
					else if (tagetasm[i] == 139 && tagetasm[i + 1] == 194)
					{
						if (!flag)
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon] = 3;
							ASMtoIL.ilpositon++;
						}
						else
						{
							ASMtoIL.ilindexs[ASMtoIL.inscount] = -1;
						}
						i += 2;
					}
					else if (!flag2 && (tagetasm[i] == 193 || tagetasm[i] == 209) && (tagetasm[i + 1] == 225 || tagetasm[i + 1] == 233 || tagetasm[i + 1] == 249))
					{
						int targetvalue;
						int num2;
						if (tagetasm[i] == 209)
						{
							targetvalue = 1;
							num2 = 2;
						}
						else
						{
							targetvalue = (int)tagetasm[i + 2];
							num2 = 3;
						}
						ASMtoIL.transIL[ASMtoIL.ilpositon] = 2;
						ASMtoIL.ilpositon++;
						byte[] array2 = ASMtoIL.LoadInteger(targetvalue);
						for (int j = 0; j < array2.Length; j++)
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon + j] = array2[j];
						}
						ASMtoIL.ilpositon += array2.Length;
						ASMtoIL.transIL[ASMtoIL.ilpositon] = ASMtoIL.ASMShiftToIL((int)(tagetasm[i + 1] - 224));
						ASMtoIL.ilpositon++;
						i += num2;
					}
					else if (!flag && (tagetasm[i] == 131 || tagetasm[i] == 129) && tagetasm[i + 1] == 250)
					{
						int targetvalue = 0;
						if (tagetasm[i] == 129)
						{
							targetvalue = BitConverter.ToInt32(tagetasm, i + 2);
						}
						if (tagetasm[i] == 131)
						{
							targetvalue = (int)tagetasm[i + 2];
						}
						ASMtoIL.transIL[ASMtoIL.ilpositon] = 3;
						ASMtoIL.ilpositon++;
						byte[] array2 = ASMtoIL.LoadInteger(targetvalue);
						for (int j = 0; j < array2.Length; j++)
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon + j] = array2[j];
						}
						ASMtoIL.ilpositon += array2.Length;
						i += 3;
					}
					else if (tagetasm[i] == 184 || tagetasm[i] == 186)
					{
						if (i + 7 < tagetasm.Length && ASMtoIL.IsStfld(tagetasm, i + 5))
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon] = 2;
							ASMtoIL.ilpositon++;
						}
						int targetvalue2 = BitConverter.ToInt32(tagetasm, i + 1);
						byte[] array3 = ASMtoIL.LoadInteger(targetvalue2);
						for (int j = 0; j < array3.Length; j++)
						{
							ASMtoIL.transIL[ASMtoIL.ilpositon + j] = array3[j];
						}
						ASMtoIL.ilpositon += array3.Length;
						i += 5;
					}
					else if (tagetasm[i] == 195)
					{
						ASMtoIL.transIL[ASMtoIL.ilpositon] = 42;
						ASMtoIL.ilpositon++;
						i++;
					}
					else
					{
						if (tagetasm[i] != 194)
						{
							if (ASMtoIL.IsLoadField(tagetasm, i))
							{
								int num3 = 0;
								if (tagetasm[i] == 102 || tagetasm[i] == 15)
								{
									num3++;
									i++;
								}
								int varindex = 0;
								if (tagetasm[i + 1] == 1 || tagetasm[i + 1] == 17)
								{
									num3 += 2;
									varindex = 0;
								}
								else if (tagetasm[i + 1] == 65 || tagetasm[i + 1] == 81)
								{
									num3 += 3;
									varindex = (int)tagetasm[i + 2];
								}
								else if (tagetasm[i + 1] == 129 || tagetasm[i + 1] == 145)
								{
									num3 += 6;
									varindex = BitConverter.ToInt32(tagetasm, i + 2);
								}
								if (ASMtoIL.IsLoadOnEdx(tagetasm, i))
								{
									ASMtoIL.ilindexs[ASMtoIL.inscount] = -1;
								}
								else
								{
									int num4 = ASMtoIL.GetFieldIndex(varindex);
									if (num4 == 0)
									{
										result = null;
										return result;
									}
									num4 += 67108864;
									byte[] bytes = BitConverter.GetBytes(num4);
									ASMtoIL.transIL[ASMtoIL.ilpositon] = 2;
									ASMtoIL.transIL[ASMtoIL.ilpositon + 1] = 123;
									for (int j = 0; j < bytes.Length; j++)
									{
										ASMtoIL.transIL[ASMtoIL.ilpositon + j + 2] = bytes[j];
									}
									ASMtoIL.ilpositon += 6;
								}
								i += num3;
								goto IL_CD4;
							}
							if (ASMtoIL.IsStfld(tagetasm, i))
							{
								int num3 = 0;
								if (tagetasm[i] == 102)
								{
									num3++;
									i++;
								}
								int varindex = 0;
								if (tagetasm[i + 1] == 1)
								{
									num3 += 2;
									varindex = 0;
								}
								else if (tagetasm[i + 1] == 65)
								{
									num3 += 3;
									varindex = (int)tagetasm[i + 2];
								}
								else if (tagetasm[i + 1] == 129)
								{
									num3 += 6;
									varindex = BitConverter.ToInt32(tagetasm, i + 2);
								}
								int num4 = ASMtoIL.GetFieldIndex(varindex);
								if (num4 != 0)
								{
									num4 += 67108864;
									byte[] bytes = BitConverter.GetBytes(num4);
									ASMtoIL.transIL[ASMtoIL.ilpositon] = 128;
									for (int j = 0; j < bytes.Length; j++)
									{
										ASMtoIL.transIL[ASMtoIL.ilpositon + j + 1] = bytes[j];
									}
									ASMtoIL.ilpositon += 5;
									i += num3;
									goto IL_CD4;
								}
								result = null;
							}
							else if (ASMtoIL.IsCmpFieldConst(tagetasm, i))
							{
								int num3 = 2;
								bool flag3 = false;
								if (tagetasm[i] == 102)
								{
									num3++;
									i++;
									flag3 = true;
								}
								int varindex = 0;
								int targetvalue = 0;
								if (tagetasm[i + 1] == 57)
								{
									varindex = 0;
									if (tagetasm[i] == 128 || tagetasm[i] == 131)
									{
										targetvalue = (int)tagetasm[i + 2];
										num3++;
									}
									else if (tagetasm[i] == 129)
									{
										if (flag3)
										{
											targetvalue = (int)BitConverter.ToInt16(tagetasm, i + 2);
											num3 += 2;
										}
										else
										{
											targetvalue = BitConverter.ToInt32(tagetasm, i + 2);
											num3 += 4;
										}
									}
								}
								else if (tagetasm[i + 1] == 121)
								{
									varindex = (int)tagetasm[i + 2];
									num3++;
									if (tagetasm[i] == 128 || tagetasm[i] == 131)
									{
										targetvalue = (int)tagetasm[i + 3];
										num3++;
									}
									else if (tagetasm[i] == 129)
									{
										if (flag3)
										{
											targetvalue = (int)BitConverter.ToInt16(tagetasm, i + 3);
											num3 += 2;
										}
										else
										{
											targetvalue = BitConverter.ToInt32(tagetasm, i + 3);
											num3 += 4;
										}
									}
								}
								else if (tagetasm[i + 1] == 185)
								{
									num3 += 4;
									varindex = BitConverter.ToInt32(tagetasm, i + 2);
									if (tagetasm[i] == 128 || tagetasm[i] == 131)
									{
										targetvalue = (int)tagetasm[i + 6];
										num3++;
									}
									else if (tagetasm[i] == 129)
									{
										if (flag3)
										{
											targetvalue = (int)BitConverter.ToInt16(tagetasm, i + 6);
											num3 += 2;
										}
										else
										{
											targetvalue = BitConverter.ToInt32(tagetasm, i + 6);
											num3 += 4;
										}
									}
								}
								int num4 = ASMtoIL.GetFieldIndex(varindex);
								if (num4 != 0)
								{
									num4 += 67108864;
									byte[] bytes = BitConverter.GetBytes(num4);
									ASMtoIL.transIL[ASMtoIL.ilpositon] = 123;
									for (int j = 0; j < bytes.Length; j++)
									{
										ASMtoIL.transIL[ASMtoIL.ilpositon + j + 1] = bytes[j];
									}
									ASMtoIL.ilpositon += 5;
									byte[] array2 = ASMtoIL.LoadInteger(targetvalue);
									for (int j = 0; j < array2.Length; j++)
									{
										ASMtoIL.transIL[ASMtoIL.ilpositon + j] = array2[j];
									}
									ASMtoIL.ilpositon += array2.Length;
									i += num3;
									goto IL_CD4;
								}
								result = null;
							}
							else
							{
								if (tagetasm[i] >= 112 && tagetasm[i] < 128)
								{
									array[num].jumptype = ASMtoIL.JumpType.Short;
									array[num].ilpos = ASMtoIL.ilpositon + 1;
									int index = (int)(tagetasm[i] - 112);
									ASMtoIL.transIL[ASMtoIL.ilpositon] = ASMtoIL.ASMSJumpToIL(index);
									int num5 = (int)tagetasm[i + 1];
									int targetasmins = i + num5 + 2;
									array[num].targetasmins = targetasmins;
									num++;
									ASMtoIL.ilpositon += 2;
									i += 2;
									goto IL_CD4;
								}
								if (tagetasm[i] == 15 && tagetasm[i + 1] >= 144 && tagetasm[i + 1] <= 159)
								{
									int index = (int)(tagetasm[i + 1] - 144);
									byte b2 = ASMtoIL.ASMSJumpToIL(index);
									ASMtoIL.transIL[ASMtoIL.ilpositon] = b2;
									ASMtoIL.transIL[ASMtoIL.ilpositon + 1] = 3;
									ASMtoIL.transIL[ASMtoIL.ilpositon + 2] = 22;
									ASMtoIL.transIL[ASMtoIL.ilpositon + 3] = 43;
									ASMtoIL.transIL[ASMtoIL.ilpositon + 4] = 1;
									ASMtoIL.transIL[ASMtoIL.ilpositon + 5] = 23;
									ASMtoIL.ilpositon += 6;
									i += 3;
									goto IL_CD4;
								}
								if (tagetasm[i] == 15 && tagetasm[i + 1] == 182 && tagetasm[i + 2] == 192)
								{
									ASMtoIL.ilindexs[ASMtoIL.inscount] = -1;
									i += 3;
									goto IL_CD4;
								}
								result = null;
							}
							return result;
						}
						ASMtoIL.transIL[ASMtoIL.ilpositon] = 42;
						ASMtoIL.ilpositon++;
						i += 3;
					}
					IL_CD4:
					ASMtoIL.inscount++;
				}
				if (num > 0)
				{
					for (int j = 0; j < num; j++)
					{
						int targetasmins2 = array[j].targetasmins;
						int num6 = 0;
						while (ASMtoIL.asmindexs[num6] < targetasmins2)
						{
							num6++;
						}
						while (ASMtoIL.ilindexs[num6] == -1)
						{
							num6++;
						}
						int ilpos = array[j].ilpos;
						int num7 = ASMtoIL.ilindexs[num6];
						int num8;
						if (num7 > ilpos)
						{
							num8 = num7 - ilpos;
						}
						else
						{
							num8 = ilpos - num7;
						}
						num8--;
						ASMtoIL.transIL[array[j].ilpos] = (byte)num8;
					}
				}
				byte[] array4 = new byte[ASMtoIL.ilpositon];
				Array.Copy(ASMtoIL.transIL, array4, ASMtoIL.ilpositon);
				result = array4;
				return result;
			}
		}

		// Token: 0x04000028 RID: 40
		private static byte[] transIL;

		// Token: 0x04000029 RID: 41
		private static int[] asmindexs;

		// Token: 0x0400002A RID: 42
		private static int[] ilindexs;

		// Token: 0x0400002B RID: 43
		private static int ilpositon;

		// Token: 0x0400002C RID: 44
		private static int inscount;

		// Token: 0x0400002D RID: 45
		private static bool DontUseName;

		// Token: 0x0400002E RID: 46
		private static MetadataReader mr;

		// Token: 0x0400002F RID: 47
		private static int methodindex;

		// Token: 0x04000030 RID: 48
		private static byte[] jumptabletran = new byte[]
		{
			0,
			0,
			55,
			52,
			46,
			51,
			54,
			53,
			0,
			0,
			0,
			0,
			50,
			47,
			49,
			48
		};

		// Token: 0x02000004 RID: 4
		public enum JumpType : byte
		{
			// Token: 0x04000032 RID: 50
			Short = 1,
			// Token: 0x04000033 RID: 51
			Big
		}

		// Token: 0x02000005 RID: 5
		public struct JumpFix
		{
			// Token: 0x04000034 RID: 52
			public ASMtoIL.JumpType jumptype;

			// Token: 0x04000035 RID: 53
			public int ilpos;

			// Token: 0x04000036 RID: 54
			public int targetasmins;
		}
	}
}
