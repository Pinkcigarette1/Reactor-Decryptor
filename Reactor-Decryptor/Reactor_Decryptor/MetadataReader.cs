using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Reactor_Decryptor
{
	// Token: 0x02000018 RID: 24
	public class MetadataReader
	{
		// Token: 0x06000051 RID: 81 RVA: 0x0000890C File Offset: 0x0000790C
		public void InitTablesInfo()
		{
			this.tablesinfo = new MetadataReader.TableInfo[45];
			this.tablesinfo[0].Name = "Module";
			this.tablesinfo[0].names = new string[]
			{
				"Generation",
				"Name",
				"Mvid",
				"EncId",
				"EncBaseId"
			};
			this.tablesinfo[0].type = MetadataReader.Types.Module;
			this.tablesinfo[0].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.String,
				MetadataReader.Types.Guid,
				MetadataReader.Types.Guid,
				MetadataReader.Types.Guid
			};
			this.tablesinfo[1].Name = "TypeRef";
			this.tablesinfo[1].names = new string[]
			{
				"ResolutionScope",
				"Name",
				"Namespace"
			};
			this.tablesinfo[1].type = MetadataReader.Types.TypeRef;
			this.tablesinfo[1].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.ResolutionScope,
				MetadataReader.Types.String,
				MetadataReader.Types.String
			};
			this.tablesinfo[2].Name = "TypeDef";
			this.tablesinfo[2].names = new string[]
			{
				"Flags",
				"Name",
				"Namespace",
				"Extends",
				"FieldList",
				"MethodList"
			};
			this.tablesinfo[2].type = MetadataReader.Types.TypeDef;
			this.tablesinfo[2].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.String,
				MetadataReader.Types.String,
				MetadataReader.Types.TypeDefOrRef,
				MetadataReader.Types.Field,
				MetadataReader.Types.Method
			};
			this.tablesinfo[3].Name = "FieldPtr";
			this.tablesinfo[3].names = new string[]
			{
				"Field"
			};
			this.tablesinfo[3].type = MetadataReader.Types.FieldPtr;
			this.tablesinfo[3].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Field
			};
			this.tablesinfo[4].Name = "Field";
			this.tablesinfo[4].names = new string[]
			{
				"Flags",
				"Name",
				"Signature"
			};
			this.tablesinfo[4].type = MetadataReader.Types.Field;
			this.tablesinfo[4].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.String,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[5].Name = "MethodPtr";
			this.tablesinfo[5].names = new string[]
			{
				"Method"
			};
			this.tablesinfo[5].type = MetadataReader.Types.MethodPtr;
			this.tablesinfo[5].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Method
			};
			this.tablesinfo[6].Name = "Method";
			this.tablesinfo[6].names = new string[]
			{
				"RVA",
				"ImplFlags",
				"Flags",
				"Name",
				"Signature",
				"ParamList"
			};
			this.tablesinfo[6].type = MetadataReader.Types.Method;
			this.tablesinfo[6].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.String,
				MetadataReader.Types.Blob,
				MetadataReader.Types.Param
			};
			this.tablesinfo[7].Name = "ParamPtr";
			this.tablesinfo[7].names = new string[]
			{
				"Param"
			};
			this.tablesinfo[7].type = MetadataReader.Types.ParamPtr;
			this.tablesinfo[7].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Param
			};
			this.tablesinfo[8].Name = "Param";
			this.tablesinfo[8].names = new string[]
			{
				"Flags",
				"Sequence",
				"Name"
			};
			this.tablesinfo[8].type = MetadataReader.Types.Param;
			this.tablesinfo[8].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.String
			};
			this.tablesinfo[9].Name = "InterfaceImpl";
			this.tablesinfo[9].names = new string[]
			{
				"Class",
				"Interface"
			};
			this.tablesinfo[9].type = MetadataReader.Types.InterfaceImpl;
			this.tablesinfo[9].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.TypeDefOrRef
			};
			this.tablesinfo[10].Name = "MemberRef";
			this.tablesinfo[10].names = new string[]
			{
				"Class",
				"Name",
				"Signature"
			};
			this.tablesinfo[10].type = MetadataReader.Types.MemberRef;
			this.tablesinfo[10].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.MemberRefParent,
				MetadataReader.Types.String,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[11].Name = "Constant";
			this.tablesinfo[11].names = new string[]
			{
				"Type",
				"Parent",
				"Value"
			};
			this.tablesinfo[11].type = MetadataReader.Types.Constant;
			this.tablesinfo[11].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.HasConstant,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[12].Name = "CustomAttribute";
			this.tablesinfo[12].names = new string[]
			{
				"Type",
				"Parent",
				"Value"
			};
			this.tablesinfo[12].type = MetadataReader.Types.CustomAttribute;
			this.tablesinfo[12].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.HasCustomAttribute,
				MetadataReader.Types.CustomAttributeType,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[13].Name = "FieldMarshal";
			this.tablesinfo[13].names = new string[]
			{
				"Parent",
				"Native"
			};
			this.tablesinfo[13].type = MetadataReader.Types.FieldMarshal;
			this.tablesinfo[13].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.HasFieldMarshal,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[14].Name = "Permission";
			this.tablesinfo[14].names = new string[]
			{
				"Action",
				"Parent",
				"PermissionSet"
			};
			this.tablesinfo[14].type = MetadataReader.Types.Permission;
			this.tablesinfo[14].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.HasDeclSecurity,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[15].Name = "ClassLayout";
			this.tablesinfo[15].names = new string[]
			{
				"PackingSize",
				"ClassSize",
				"Parent"
			};
			this.tablesinfo[15].type = MetadataReader.Types.ClassLayout;
			this.tablesinfo[15].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.TypeDef
			};
			this.tablesinfo[16].Name = "FieldLayout";
			this.tablesinfo[16].names = new string[]
			{
				"Offset",
				"Field"
			};
			this.tablesinfo[16].type = MetadataReader.Types.FieldLayout;
			this.tablesinfo[16].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.Field
			};
			this.tablesinfo[17].Name = "StandAloneSig";
			this.tablesinfo[17].names = new string[]
			{
				"Signature"
			};
			this.tablesinfo[17].type = MetadataReader.Types.StandAloneSig;
			this.tablesinfo[17].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Blob
			};
			this.tablesinfo[18].Name = "EventMap";
			this.tablesinfo[18].names = new string[]
			{
				"Parent",
				"EventList"
			};
			this.tablesinfo[18].type = MetadataReader.Types.EventMap;
			this.tablesinfo[18].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.Event
			};
			this.tablesinfo[19].Name = "EventPtr";
			this.tablesinfo[19].names = new string[]
			{
				"Event"
			};
			this.tablesinfo[19].type = MetadataReader.Types.EventPtr;
			this.tablesinfo[19].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Event
			};
			this.tablesinfo[20].Name = "Event";
			this.tablesinfo[20].names = new string[]
			{
				"EventFlags",
				"Name",
				"EventType"
			};
			this.tablesinfo[20].type = MetadataReader.Types.Event;
			this.tablesinfo[20].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.String,
				MetadataReader.Types.TypeDefOrRef
			};
			this.tablesinfo[21].Name = "PropertyMap";
			this.tablesinfo[21].names = new string[]
			{
				"Parent",
				"PropertyList"
			};
			this.tablesinfo[21].type = MetadataReader.Types.PropertyMap;
			this.tablesinfo[21].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.Property
			};
			this.tablesinfo[22].Name = "PropertyPtr";
			this.tablesinfo[22].names = new string[]
			{
				"Property"
			};
			this.tablesinfo[22].type = MetadataReader.Types.PropertyPtr;
			this.tablesinfo[22].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Property
			};
			this.tablesinfo[23].Name = "Property";
			this.tablesinfo[23].names = new string[]
			{
				"PropFlags",
				"Name",
				"Type"
			};
			this.tablesinfo[23].type = MetadataReader.Types.Property;
			this.tablesinfo[23].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.String,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[24].Name = "MethodSemantics";
			this.tablesinfo[24].names = new string[]
			{
				"Semantic",
				"Method",
				"Association"
			};
			this.tablesinfo[24].type = MetadataReader.Types.MethodSemantics;
			this.tablesinfo[24].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.Method,
				MetadataReader.Types.HasSemantic
			};
			this.tablesinfo[25].Name = "MethodImpl";
			this.tablesinfo[25].names = new string[]
			{
				"Class",
				"MethodBody",
				"MethodDeclaration"
			};
			this.tablesinfo[25].type = MetadataReader.Types.MethodImpl;
			this.tablesinfo[25].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.MethodDefOrRef,
				MetadataReader.Types.MethodDefOrRef
			};
			this.tablesinfo[26].Name = "ModuleRef";
			this.tablesinfo[26].names = new string[]
			{
				"Name"
			};
			this.tablesinfo[26].type = MetadataReader.Types.ModuleRef;
			this.tablesinfo[26].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.String
			};
			this.tablesinfo[27].Name = "TypeSpec";
			this.tablesinfo[27].names = new string[]
			{
				"Signature"
			};
			this.tablesinfo[27].type = MetadataReader.Types.TypeSpec;
			this.tablesinfo[27].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Blob
			};
			this.tablesinfo[28].Name = "ImplMap";
			this.tablesinfo[28].names = new string[]
			{
				"MappingFlags",
				"MemberForwarded",
				"ImportName",
				"ImportScope"
			};
			this.tablesinfo[28].type = MetadataReader.Types.ImplMap;
			this.tablesinfo[28].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.MemberForwarded,
				MetadataReader.Types.String,
				MetadataReader.Types.ModuleRef
			};
			this.tablesinfo[29].Name = "FieldRVA";
			this.tablesinfo[29].names = new string[]
			{
				"RVA",
				"Field"
			};
			this.tablesinfo[29].type = MetadataReader.Types.FieldRVA;
			this.tablesinfo[29].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.Field
			};
			this.tablesinfo[30].Name = "ENCLog";
			this.tablesinfo[30].names = new string[]
			{
				"Token",
				"FuncCode"
			};
			this.tablesinfo[30].type = MetadataReader.Types.ENCLog;
			this.tablesinfo[30].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt32
			};
			this.tablesinfo[31].Name = "ENCMap";
			this.tablesinfo[31].names = new string[]
			{
				"Token"
			};
			this.tablesinfo[31].type = MetadataReader.Types.ENCMap;
			this.tablesinfo[31].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32
			};
			this.tablesinfo[32].Name = "Assembly";
			this.tablesinfo[32].names = new string[]
			{
				"HashAlgId",
				"MajorVersion",
				"MinorVersion",
				"BuildNumber",
				"RevisionNumber",
				"Flags",
				"PublicKey",
				"Name",
				"Locale"
			};
			this.tablesinfo[32].type = MetadataReader.Types.Assembly;
			this.tablesinfo[32].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.Blob,
				MetadataReader.Types.String,
				MetadataReader.Types.String
			};
			this.tablesinfo[33].Name = "AssemblyProcessor";
			this.tablesinfo[33].names = new string[]
			{
				"Processor"
			};
			this.tablesinfo[33].type = MetadataReader.Types.AssemblyProcessor;
			this.tablesinfo[33].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32
			};
			this.tablesinfo[34].Name = "AssemblyOS";
			this.tablesinfo[34].names = new string[]
			{
				"OSPlatformId",
				"OSMajorVersion",
				"OSMinorVersion"
			};
			this.tablesinfo[34].type = MetadataReader.Types.AssemblyOS;
			this.tablesinfo[34].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt32
			};
			this.tablesinfo[35].Name = "AssemblyRef";
			this.tablesinfo[35].names = new string[]
			{
				"MajorVersion",
				"MinorVersion",
				"BuildNumber",
				"RevisionNumber",
				"Flags",
				"PublicKeyOrToken",
				"Name",
				"Locale",
				"HashValue"
			};
			this.tablesinfo[35].type = MetadataReader.Types.AssemblyRef;
			this.tablesinfo[35].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt16,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.Blob,
				MetadataReader.Types.String,
				MetadataReader.Types.String,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[36].Name = "AssemblyRefProcessor";
			this.tablesinfo[36].names = new string[]
			{
				"Processor",
				"AssemblyRef"
			};
			this.tablesinfo[36].type = MetadataReader.Types.AssemblyRefProcessor;
			this.tablesinfo[36].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.AssemblyRef
			};
			this.tablesinfo[37].Name = "AssemblyRefOS";
			this.tablesinfo[37].names = new string[]
			{
				"OSPlatformId",
				"OSMajorVersion",
				"OSMinorVersion",
				"AssemblyRef"
			};
			this.tablesinfo[37].type = MetadataReader.Types.AssemblyRefOS;
			this.tablesinfo[37].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.AssemblyRef
			};
			this.tablesinfo[38].Name = "File";
			this.tablesinfo[38].names = new string[]
			{
				"Flags",
				"Name",
				"HashValue"
			};
			this.tablesinfo[38].type = MetadataReader.Types.File;
			this.tablesinfo[38].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.String,
				MetadataReader.Types.Blob
			};
			this.tablesinfo[39].Name = "ExportedType";
			this.tablesinfo[39].names = new string[]
			{
				"Flags",
				"TypeDefId",
				"TypeName",
				"TypeNamespace",
				"TypeImplementation"
			};
			this.tablesinfo[39].type = MetadataReader.Types.ExportedType;
			this.tablesinfo[39].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.String,
				MetadataReader.Types.String,
				MetadataReader.Types.Implementation
			};
			this.tablesinfo[40].Name = "ManifestResource";
			this.tablesinfo[40].names = new string[]
			{
				"Offset",
				"Flags",
				"Name",
				"Implementation"
			};
			this.tablesinfo[40].type = MetadataReader.Types.ManifestResource;
			this.tablesinfo[40].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt32,
				MetadataReader.Types.UInt32,
				MetadataReader.Types.String,
				MetadataReader.Types.Implementation
			};
			this.tablesinfo[41].Name = "NestedClass";
			this.tablesinfo[41].names = new string[]
			{
				"NestedClass",
				"EnclosingClass"
			};
			this.tablesinfo[41].type = MetadataReader.Types.NestedClass;
			this.tablesinfo[41].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.TypeDef
			};
			this.tablesinfo[42].Name = "TypeTyPar";
			this.tablesinfo[42].names = new string[]
			{
				"Number",
				"Class",
				"Bound",
				"Name"
			};
			this.tablesinfo[42].type = MetadataReader.Types.TypeTyPar;
			this.tablesinfo[42].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.TypeDefOrRef,
				MetadataReader.Types.String
			};
			this.tablesinfo[43].Name = "MethodTyPar";
			this.tablesinfo[43].names = new string[]
			{
				"Number",
				"Method",
				"Bound",
				"Name"
			};
			this.tablesinfo[43].type = MetadataReader.Types.MethodTyPar;
			this.tablesinfo[43].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.Method,
				MetadataReader.Types.TypeDefOrRef,
				MetadataReader.Types.String
			};
			this.tablesinfo[44].Name = "MethodTyPar";
			this.tablesinfo[44].names = new string[]
			{
				"Number",
				"Method",
				"Bound",
				"Name"
			};
			this.tablesinfo[44].type = MetadataReader.Types.MethodTyPar;
			this.tablesinfo[44].ctypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.UInt16,
				MetadataReader.Types.Method,
				MetadataReader.Types.TypeDefOrRef,
				MetadataReader.Types.String
			};
			this.codedTokenBits = new int[]
			{
				0,
				1,
				1,
				2,
				2,
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5,
				5
			};
			this.reftables = new MetadataReader.RefTableInfo[12];
			this.reftables[0].type = MetadataReader.Types.TypeDefOrRef;
			this.reftables[0].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.TypeRef,
				MetadataReader.Types.TypeSpec
			};
			this.reftables[0].refindex = new int[]
			{
				1,
				2,
				27
			};
			this.reftables[1].type = MetadataReader.Types.HasConstant;
			this.reftables[1].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Field,
				MetadataReader.Types.Param,
				MetadataReader.Types.Property
			};
			this.reftables[1].refindex = new int[]
			{
				4,
				8,
				23
			};
			this.reftables[2].type = MetadataReader.Types.CustomAttributeType;
			this.reftables[2].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeRef,
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.Method,
				MetadataReader.Types.MemberRef,
				MetadataReader.Types.UserString
			};
			this.reftables[2].refindex = new int[]
			{
				1,
				2,
				6,
				10,
				1
			};
			this.reftables[3].type = MetadataReader.Types.HasSemantic;
			this.reftables[3].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Event,
				MetadataReader.Types.Property
			};
			this.reftables[3].refindex = new int[]
			{
				20,
				23
			};
			this.reftables[4].type = MetadataReader.Types.ResolutionScope;
			this.reftables[4].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Module,
				MetadataReader.Types.ModuleRef,
				MetadataReader.Types.AssemblyRef,
				MetadataReader.Types.TypeRef
			};
			this.reftables[4].refindex = new int[]
			{
				0,
				26,
				35,
				1
			};
			this.reftables[5].type = MetadataReader.Types.HasFieldMarshal;
			this.reftables[5].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Field,
				MetadataReader.Types.Param
			};
			this.reftables[5].refindex = new int[]
			{
				4,
				8
			};
			this.reftables[6].type = MetadataReader.Types.HasDeclSecurity;
			this.reftables[6].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.Method,
				MetadataReader.Types.Assembly
			};
			this.reftables[6].refindex = new int[]
			{
				2,
				6,
				32
			};
			this.reftables[7].type = MetadataReader.Types.MemberRefParent;
			this.reftables[7].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.TypeRef,
				MetadataReader.Types.ModuleRef,
				MetadataReader.Types.Method,
				MetadataReader.Types.TypeSpec
			};
			this.reftables[7].refindex = new int[]
			{
				2,
				1,
				26,
				6,
				27
			};
			this.reftables[8].type = MetadataReader.Types.MethodDefOrRef;
			this.reftables[8].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Method,
				MetadataReader.Types.MemberRef
			};
			this.reftables[8].refindex = new int[]
			{
				6,
				10
			};
			this.reftables[9].type = MetadataReader.Types.MemberForwarded;
			this.reftables[9].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Field,
				MetadataReader.Types.Method
			};
			this.reftables[9].refindex = new int[]
			{
				4,
				6
			};
			this.reftables[10].type = MetadataReader.Types.Implementation;
			this.reftables[10].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.File,
				MetadataReader.Types.AssemblyRef,
				MetadataReader.Types.ExportedType
			};
			this.reftables[10].refindex = new int[]
			{
				38,
				35,
				39
			};
			this.reftables[11].type = MetadataReader.Types.HasCustomAttribute;
			this.reftables[11].reftypes = new MetadataReader.Types[]
			{
				MetadataReader.Types.Method,
				MetadataReader.Types.Field,
				MetadataReader.Types.TypeRef,
				MetadataReader.Types.TypeDef,
				MetadataReader.Types.Param,
				MetadataReader.Types.InterfaceImpl,
				MetadataReader.Types.MemberRef,
				MetadataReader.Types.Module,
				MetadataReader.Types.Permission,
				MetadataReader.Types.Property,
				MetadataReader.Types.Event,
				MetadataReader.Types.StandAloneSig,
				MetadataReader.Types.ModuleRef,
				MetadataReader.Types.TypeSpec,
				MetadataReader.Types.Assembly,
				MetadataReader.Types.AssemblyRef,
				MetadataReader.Types.File,
				MetadataReader.Types.ExportedType,
				MetadataReader.Types.ManifestResource
			};
			this.reftables[11].refindex = new int[]
			{
				6,
				4,
				1,
				2,
				8,
				9,
				10,
				0,
				14,
				23,
				20,
				17,
				26,
				27,
				32,
				35,
				38,
				39,
				40
			};
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000A538 File Offset: 0x00009538
		public int Rva2Offset(int rva)
		{
			checked
			{
				int result;
				for (int i = 0; i < this.sections.Length; i++)
				{
					if (this.sections[i].virtual_address <= rva && this.sections[i].virtual_address + this.sections[i].size_of_raw_data > rva)
					{
						result = this.sections[i].pointer_to_raw_data + (rva - this.sections[i].virtual_address);
						return result;
					}
				}
				result = 0;
				return result;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000A5D4 File Offset: 0x000095D4
		public int Offset2Rva(int uOffset)
		{
			checked
			{
				int result;
				for (int i = 0; i < this.sections.Length; i++)
				{
					if (this.sections[i].pointer_to_raw_data <= uOffset && this.sections[i].pointer_to_raw_data + this.sections[i].size_of_raw_data > uOffset)
					{
						result = this.sections[i].virtual_address + (uOffset - this.sections[i].pointer_to_raw_data);
						return result;
					}
				}
				result = 0;
				return result;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000A670 File Offset: 0x00009670
		public int GetTypeSize(MetadataReader.Types trans)
		{
			checked
			{
				int result;
				if (trans == MetadataReader.Types.UInt16)
				{
					result = 2;
				}
				else if (trans == MetadataReader.Types.UInt32)
				{
					result = 4;
				}
				else if (trans == MetadataReader.Types.String)
				{
					result = this.GetStringIndexSize();
				}
				else if (trans == MetadataReader.Types.Guid)
				{
					result = this.GetGuidIndexSize();
				}
				else if (trans == MetadataReader.Types.Blob)
				{
					result = this.GetBlobIndexSize();
				}
				else if (trans < MetadataReader.Types.TypeDefOrRef)
				{
					if (this.TableLengths[(int)trans] > 65535)
					{
						result = 4;
					}
					else
					{
						result = 2;
					}
				}
				else if (trans < MetadataReader.Types.UInt16)
				{
					int num = trans - MetadataReader.Types.TypeDefOrRef;
					int num2 = this.codedTokenBits[this.reftables[num].refindex.Length];
					int num3 = 65535;
					num3 >>= num2;
					for (int i = 0; i < this.reftables[num].refindex.Length; i++)
					{
						if (this.TableLengths[this.reftables[num].refindex[i]] > num3)
						{
							result = 4;
							return result;
						}
					}
					result = 2;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000A7D0 File Offset: 0x000097D0
		public int GetStringIndexSize()
		{
			return ((this.tableheader.HeapOffsetSizes & 1) != 0) ? 4 : 2;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000A7F8 File Offset: 0x000097F8
		public int GetGuidIndexSize()
		{
			return ((this.tableheader.HeapOffsetSizes & 2) != 0) ? 4 : 2;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000A820 File Offset: 0x00009820
		public int GetBlobIndexSize()
		{
			return ((this.tableheader.HeapOffsetSizes & 4) != 0) ? 4 : 2;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000A848 File Offset: 0x00009848
		public unsafe bool Intialize(BinaryReader reader)
		{
			reader.BaseStream.Position = 0L;
			bool result;
			try
			{
				byte[] array = reader.ReadBytes(sizeof(MetadataReader.IMAGE_DOS_HEADER));
				IntPtr ptr2;
				try
				{
					fixed (byte* ptr = array)
					{
						ptr2 = (IntPtr)((void*)ptr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				this.idh = (MetadataReader.IMAGE_DOS_HEADER)Marshal.PtrToStructure(ptr2, typeof(MetadataReader.IMAGE_DOS_HEADER));
				if (this.idh.e_magic != 23117)
				{
					result = false;
					return result;
				}
				reader.BaseStream.Position = (long)this.idh.e_lfanew;
				array = reader.ReadBytes(sizeof(MetadataReader.IMAGE_NT_HEADERS));
				try
				{
					fixed (byte* ptr = array)
					{
						ptr2 = (IntPtr)((void*)ptr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				this.inh = (MetadataReader.IMAGE_NT_HEADERS)Marshal.PtrToStructure(ptr2, typeof(MetadataReader.IMAGE_NT_HEADERS));
				if (this.inh.Signature != 17744)
				{
					result = false;
					return result;
				}
				reader.BaseStream.Position = (long)(checked(this.idh.e_lfanew + 4 + sizeof(MetadataReader.IMAGE_FILE_HEADER) + (int)this.inh.ifh.SizeOfOptionalHeader));
				this.sections = new MetadataReader.image_section_header[(int)this.inh.ifh.NumberOfSections];
				array = reader.ReadBytes(checked(sizeof(MetadataReader.image_section_header) * (int)this.inh.ifh.NumberOfSections));
				try
				{
					fixed (byte* ptr = array)
					{
						ptr2 = (IntPtr)((void*)ptr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				checked
				{
					for (int i = 0; i < this.sections.Length; i++)
					{
						this.sections[i] = (MetadataReader.image_section_header)Marshal.PtrToStructure(ptr2, typeof(MetadataReader.image_section_header));
						ptr2 = (IntPtr)(ptr2.ToInt32() + Marshal.SizeOf(typeof(MetadataReader.image_section_header)));
					}
				}
				long num;
				if (this.inh.ioh.ResourceDirectory.RVA != 0)
				{
					num = (long)this.Rva2Offset(this.inh.ioh.ResourceDirectory.RVA);
					if (num != 0L)
					{
						reader.BaseStream.Position = num;
						this.rsrcsection = reader.ReadBytes(this.inh.ioh.ResourceDirectory.Size);
					}
				}
				if (this.inh.ioh.MetaDataDirectory.RVA == 0)
				{
					result = false;
					return result;
				}
				num = (long)this.Rva2Offset(this.inh.ioh.MetaDataDirectory.RVA);
				if (num == 0L)
				{
					result = false;
					return result;
				}
				reader.BaseStream.Position = num;
				array = reader.ReadBytes(sizeof(MetadataReader.NETDirectory));
				try
				{
					fixed (byte* ptr = array)
					{
						ptr2 = (IntPtr)((void*)ptr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				this.netdir = (MetadataReader.NETDirectory)Marshal.PtrToStructure(ptr2, typeof(MetadataReader.NETDirectory));
				if (this.netdir.StrongNameSignatureRVA != 0)
				{
					num = (long)this.Rva2Offset(this.netdir.StrongNameSignatureRVA);
					if (num != 0L)
					{
						reader.BaseStream.Position = num;
						this.StrongName = reader.ReadBytes(this.netdir.StrongNameSignatureSize);
					}
				}
				if (this.netdir.ResourceRVA != 0)
				{
					num = (long)this.Rva2Offset(this.netdir.ResourceRVA);
					if (num != 0L)
					{
						reader.BaseStream.Position = num;
						this.NETResources = reader.ReadBytes(this.netdir.ResourceSize);
					}
				}
				reader.BaseStream.Position = (long)this.Rva2Offset(this.netdir.MetaDataRVA);
				this.mh = default(MetadataReader.MetaDataHeader);
				this.mh.Signature = reader.ReadInt32();
				this.mh.MajorVersion = reader.ReadInt16();
				this.mh.MinorVersion = reader.ReadInt16();
				this.mh.Reserved = reader.ReadInt32();
				this.mh.VerionLenght = reader.ReadInt32();
				this.mh.VersionString = reader.ReadBytes(this.mh.VerionLenght);
				this.mh.Flags = reader.ReadInt16();
				this.mh.NumberOfStreams = reader.ReadInt16();
				this.streams = new MetadataReader.MetaDataStream[(int)this.mh.NumberOfStreams];
				checked
				{
					for (int i = 0; i < (int)this.mh.NumberOfStreams; i++)
					{
						this.streams[i].Offset = reader.ReadInt32();
						this.streams[i].Size = reader.ReadInt32();
						char[] array2 = new char[32];
						int num2 = 0;
						byte b;
						while ((b = reader.ReadByte()) != 0)
						{
							array2[num2++] = (char)b;
						}
						num2++;
						int count = (num2 % 4 != 0) ? (4 - num2 % 4) : 0;
						reader.ReadBytes(count);
						MetadataReader.MetaDataStream[] arg_570_0_cp_0 = this.streams;
						int arg_570_0_cp_1 = i;
						string arg_56B_0 = new string(array2);
						char[] trimChars = new char[1];
						arg_570_0_cp_0[arg_570_0_cp_1].Name = arg_56B_0.Trim(trimChars);
						if (this.streams[i].Name == "#~" || this.streams[i].Name == "#-")
						{
							this.MetadataRoot.Name = this.streams[i].Name;
							this.MetadataRoot.Offset = this.streams[i].Offset;
							this.MetadataRoot.Size = this.streams[i].Size;
						}
						unchecked
						{
							if (this.streams[i].Name == "#Strings")
							{
								long position = reader.BaseStream.Position;
								reader.BaseStream.Position = (long)(checked(this.Rva2Offset(this.netdir.MetaDataRVA) + this.streams[i].Offset));
								this.StringOffset = reader.BaseStream.Position;
								this.Strings = reader.ReadBytes(this.streams[i].Size);
								reader.BaseStream.Position = position;
							}
							if (this.streams[i].Name == "#US")
							{
								long position = reader.BaseStream.Position;
								reader.BaseStream.Position = (long)(checked(this.Rva2Offset(this.netdir.MetaDataRVA) + this.streams[i].Offset));
								this.US = reader.ReadBytes(this.streams[i].Size);
								reader.BaseStream.Position = position;
							}
							if (this.streams[i].Name == "#Blob")
							{
								long position = reader.BaseStream.Position;
								reader.BaseStream.Position = (long)(checked(this.Rva2Offset(this.netdir.MetaDataRVA) + this.streams[i].Offset));
								this.BlobOffset = reader.BaseStream.Position;
								this.Blob = reader.ReadBytes(this.streams[i].Size);
								reader.BaseStream.Position = position;
							}
							if (this.streams[i].Name == "#GUID")
							{
								long position = reader.BaseStream.Position;
								reader.BaseStream.Position = (long)(checked(this.Rva2Offset(this.netdir.MetaDataRVA) + this.streams[i].Offset));
								this.GUID = reader.ReadBytes(this.streams[i].Size);
								reader.BaseStream.Position = position;
							}
						}
					}
				}
				reader.BaseStream.Position = (long)(checked(this.Rva2Offset(this.netdir.MetaDataRVA) + this.MetadataRoot.Offset));
				array = reader.ReadBytes(sizeof(MetadataReader.TableHeader));
				try
				{
					fixed (byte* ptr = array)
					{
						ptr2 = (IntPtr)((void*)ptr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				this.tableheader = (MetadataReader.TableHeader)Marshal.PtrToStructure(ptr2, typeof(MetadataReader.TableHeader));
				this.TableLengths = new int[64];
				checked
				{
					for (int i = 0; i < 64; i++)
					{
						int num3 = ((this.tableheader.MaskValid >> i & 1L) == 0L) ? 0 : reader.ReadInt32();
						this.TableLengths[i] = num3;
					}
					this.TablesOffset = reader.BaseStream.Position;
					this.InitTablesInfo();
					this.tablesize = new MetadataReader.TableSize[45];
					this.tables = new MetadataReader.Table[45];
					for (int i = 0; i < this.tablesize.Length; i++)
					{
						this.tablesize[i].Sizes = new int[this.tablesinfo[i].ctypes.Length];
						this.tablesize[i].TotalSize = 0;
						for (int j = 0; j < this.tablesinfo[i].ctypes.Length; j++)
						{
							this.tablesize[i].Sizes[j] = this.GetTypeSize(this.tablesinfo[i].ctypes[j]);
							this.tablesize[i].TotalSize = this.tablesize[i].TotalSize + this.tablesize[i].Sizes[j];
						}
					}
					for (int i = 0; i < this.tablesize.Length; i++)
					{
						if (this.TableLengths[i] > 0)
						{
							this.tables[i].members = new long[this.TableLengths[i]][];
							for (int j = 0; j < this.TableLengths[i]; j++)
							{
								this.tables[i].members[j] = new long[this.tablesinfo[i].ctypes.Length];
								for (int k = 0; k < this.tablesinfo[i].ctypes.Length; k++)
								{
									unchecked
									{
										if (this.tablesize[i].Sizes[k] == 2)
										{
											this.tables[i].members[j][k] = (long)((int)reader.ReadInt16() & 65535);
										}
										if (this.tablesize[i].Sizes[k] == 4)
										{
											this.tables[i].members[j][k] = ((long)reader.ReadInt32() & (long)((ulong)-1));
										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{
				result = false;
				return result;
			}
			result = true;
			return result;
		}

		// Token: 0x040000FA RID: 250
		public MetadataReader.IMAGE_DOS_HEADER idh;

		// Token: 0x040000FB RID: 251
		public MetadataReader.IMAGE_NT_HEADERS inh;

		// Token: 0x040000FC RID: 252
		public MetadataReader.image_section_header[] sections;

		// Token: 0x040000FD RID: 253
		public MetadataReader.NETDirectory netdir;

		// Token: 0x040000FE RID: 254
		public byte[] StrongName;

		// Token: 0x040000FF RID: 255
		public byte[] NETResources;

		// Token: 0x04000100 RID: 256
		public MetadataReader.MetaDataHeader mh;

		// Token: 0x04000101 RID: 257
		public MetadataReader.MetaDataStream[] streams;

		// Token: 0x04000102 RID: 258
		public MetadataReader.MetaDataStream MetadataRoot;

		// Token: 0x04000103 RID: 259
		public byte[] Strings;

		// Token: 0x04000104 RID: 260
		public byte[] US;

		// Token: 0x04000105 RID: 261
		public byte[] GUID;

		// Token: 0x04000106 RID: 262
		public byte[] Blob;

		// Token: 0x04000107 RID: 263
		public byte[] rsrcsection;

		// Token: 0x04000108 RID: 264
		public long TablesOffset;

		// Token: 0x04000109 RID: 265
		public MetadataReader.TableHeader tableheader;

		// Token: 0x0400010A RID: 266
		public int[] TableLengths;

		// Token: 0x0400010B RID: 267
		public MetadataReader.TableInfo[] tablesinfo;

		// Token: 0x0400010C RID: 268
		public MetadataReader.RefTableInfo[] reftables;

		// Token: 0x0400010D RID: 269
		public MetadataReader.TableSize[] tablesize;

		// Token: 0x0400010E RID: 270
		public int[] codedTokenBits;

		// Token: 0x0400010F RID: 271
		public MetadataReader.Table[] tables;

		// Token: 0x04000110 RID: 272
		public long BlobOffset;

		// Token: 0x04000111 RID: 273
		public long StringOffset;

		// Token: 0x02000019 RID: 25
		public enum Types
		{
			// Token: 0x04000113 RID: 275
			Module,
			// Token: 0x04000114 RID: 276
			TypeRef,
			// Token: 0x04000115 RID: 277
			TypeDef,
			// Token: 0x04000116 RID: 278
			FieldPtr,
			// Token: 0x04000117 RID: 279
			Field,
			// Token: 0x04000118 RID: 280
			MethodPtr,
			// Token: 0x04000119 RID: 281
			Method,
			// Token: 0x0400011A RID: 282
			ParamPtr,
			// Token: 0x0400011B RID: 283
			Param,
			// Token: 0x0400011C RID: 284
			InterfaceImpl,
			// Token: 0x0400011D RID: 285
			MemberRef,
			// Token: 0x0400011E RID: 286
			Constant,
			// Token: 0x0400011F RID: 287
			CustomAttribute,
			// Token: 0x04000120 RID: 288
			FieldMarshal,
			// Token: 0x04000121 RID: 289
			Permission,
			// Token: 0x04000122 RID: 290
			ClassLayout,
			// Token: 0x04000123 RID: 291
			FieldLayout,
			// Token: 0x04000124 RID: 292
			StandAloneSig,
			// Token: 0x04000125 RID: 293
			EventMap,
			// Token: 0x04000126 RID: 294
			EventPtr,
			// Token: 0x04000127 RID: 295
			Event,
			// Token: 0x04000128 RID: 296
			PropertyMap,
			// Token: 0x04000129 RID: 297
			PropertyPtr,
			// Token: 0x0400012A RID: 298
			Property,
			// Token: 0x0400012B RID: 299
			MethodSemantics,
			// Token: 0x0400012C RID: 300
			MethodImpl,
			// Token: 0x0400012D RID: 301
			ModuleRef,
			// Token: 0x0400012E RID: 302
			TypeSpec,
			// Token: 0x0400012F RID: 303
			ImplMap,
			// Token: 0x04000130 RID: 304
			FieldRVA,
			// Token: 0x04000131 RID: 305
			ENCLog,
			// Token: 0x04000132 RID: 306
			ENCMap,
			// Token: 0x04000133 RID: 307
			Assembly,
			// Token: 0x04000134 RID: 308
			AssemblyProcessor,
			// Token: 0x04000135 RID: 309
			AssemblyOS,
			// Token: 0x04000136 RID: 310
			AssemblyRef,
			// Token: 0x04000137 RID: 311
			AssemblyRefProcessor,
			// Token: 0x04000138 RID: 312
			AssemblyRefOS,
			// Token: 0x04000139 RID: 313
			File,
			// Token: 0x0400013A RID: 314
			ExportedType,
			// Token: 0x0400013B RID: 315
			ManifestResource,
			// Token: 0x0400013C RID: 316
			NestedClass,
			// Token: 0x0400013D RID: 317
			TypeTyPar,
			// Token: 0x0400013E RID: 318
			MethodTyPar,
			// Token: 0x0400013F RID: 319
			TypeDefOrRef = 64,
			// Token: 0x04000140 RID: 320
			HasConstant,
			// Token: 0x04000141 RID: 321
			CustomAttributeType,
			// Token: 0x04000142 RID: 322
			HasSemantic,
			// Token: 0x04000143 RID: 323
			ResolutionScope,
			// Token: 0x04000144 RID: 324
			HasFieldMarshal,
			// Token: 0x04000145 RID: 325
			HasDeclSecurity,
			// Token: 0x04000146 RID: 326
			MemberRefParent,
			// Token: 0x04000147 RID: 327
			MethodDefOrRef,
			// Token: 0x04000148 RID: 328
			MemberForwarded,
			// Token: 0x04000149 RID: 329
			Implementation,
			// Token: 0x0400014A RID: 330
			HasCustomAttribute,
			// Token: 0x0400014B RID: 331
			UInt16 = 97,
			// Token: 0x0400014C RID: 332
			UInt32 = 99,
			// Token: 0x0400014D RID: 333
			String = 101,
			// Token: 0x0400014E RID: 334
			Blob,
			// Token: 0x0400014F RID: 335
			Guid,
			// Token: 0x04000150 RID: 336
			UserString = 112
		}

		// Token: 0x0200001A RID: 26
		public struct IMAGE_DOS_HEADER
		{
			// Token: 0x04000151 RID: 337
			public short e_magic;

			// Token: 0x04000152 RID: 338
			public short e_cblp;

			// Token: 0x04000153 RID: 339
			public short e_cp;

			// Token: 0x04000154 RID: 340
			public short e_crlc;

			// Token: 0x04000155 RID: 341
			public short e_cparhdr;

			// Token: 0x04000156 RID: 342
			public short e_minalloc;

			// Token: 0x04000157 RID: 343
			public short e_maxalloc;

			// Token: 0x04000158 RID: 344
			public short e_ss;

			// Token: 0x04000159 RID: 345
			public short e_sp;

			// Token: 0x0400015A RID: 346
			public short e_csum;

			// Token: 0x0400015B RID: 347
			public short e_ip;

			// Token: 0x0400015C RID: 348
			public short e_cs;

			// Token: 0x0400015D RID: 349
			public short e_lfarlc;

			// Token: 0x0400015E RID: 350
			public short e_ovno;

			// Token: 0x0400015F RID: 351
			[FixedBuffer(typeof(short), 4)]
			private MetadataReader.IMAGE_DOS_HEADER.<e_res1>e__FixedBuffer0 e_res1;

			// Token: 0x04000160 RID: 352
			public short e_oeminfo;

			// Token: 0x04000161 RID: 353
			[FixedBuffer(typeof(short), 10)]
			private MetadataReader.IMAGE_DOS_HEADER.<e_res2>e__FixedBuffer1 e_res2;

			// Token: 0x04000162 RID: 354
			public int e_lfanew;

			// Token: 0x0200001B RID: 27
			[CompilerGenerated, UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 8)]
			public struct <e_res1>e__FixedBuffer0
			{
				// Token: 0x04000163 RID: 355
				public short FixedElementField;
			}

			// Token: 0x0200001C RID: 28
			[CompilerGenerated, UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 20)]
			public struct <e_res2>e__FixedBuffer1
			{
				// Token: 0x04000164 RID: 356
				public short FixedElementField;
			}
		}

		// Token: 0x0200001D RID: 29
		public struct IMAGE_NT_HEADERS
		{
			// Token: 0x04000165 RID: 357
			public int Signature;

			// Token: 0x04000166 RID: 358
			public MetadataReader.IMAGE_FILE_HEADER ifh;

			// Token: 0x04000167 RID: 359
			public MetadataReader.IMAGE_OPTIONAL_HEADER ioh;
		}

		// Token: 0x0200001E RID: 30
		public struct IMAGE_DATA_DIRECTORY
		{
			// Token: 0x04000168 RID: 360
			public int RVA;

			// Token: 0x04000169 RID: 361
			public int Size;
		}

		// Token: 0x0200001F RID: 31
		public struct IMAGE_FILE_HEADER
		{
			// Token: 0x0400016A RID: 362
			public short Machine;

			// Token: 0x0400016B RID: 363
			public short NumberOfSections;

			// Token: 0x0400016C RID: 364
			public int TimeDateStamp;

			// Token: 0x0400016D RID: 365
			public int PointerToSymbolTable;

			// Token: 0x0400016E RID: 366
			public int NumberOfSymbols;

			// Token: 0x0400016F RID: 367
			public short SizeOfOptionalHeader;

			// Token: 0x04000170 RID: 368
			public short Characteristics;
		}

		// Token: 0x02000020 RID: 32
		public struct IMAGE_OPTIONAL_HEADER
		{
			// Token: 0x04000171 RID: 369
			public short Magic;

			// Token: 0x04000172 RID: 370
			public byte MajorLinkerVersion;

			// Token: 0x04000173 RID: 371
			public byte MinorLinkerVersion;

			// Token: 0x04000174 RID: 372
			public int SizeOfCode;

			// Token: 0x04000175 RID: 373
			public int SizeOfInitializedData;

			// Token: 0x04000176 RID: 374
			public int SizeOfUninitializedData;

			// Token: 0x04000177 RID: 375
			public int AddressOfEntryPoint;

			// Token: 0x04000178 RID: 376
			public int BaseOfCode;

			// Token: 0x04000179 RID: 377
			public int BaseOfData;

			// Token: 0x0400017A RID: 378
			public int ImageBase;

			// Token: 0x0400017B RID: 379
			public int SectionAlignment;

			// Token: 0x0400017C RID: 380
			public int FileAlignment;

			// Token: 0x0400017D RID: 381
			public short MajorOperatingSystemVersion;

			// Token: 0x0400017E RID: 382
			public short MinorOperatingSystemVersion;

			// Token: 0x0400017F RID: 383
			public short MajorImageVersion;

			// Token: 0x04000180 RID: 384
			public short MinorImageVersion;

			// Token: 0x04000181 RID: 385
			public short MajorSubsystemVersion;

			// Token: 0x04000182 RID: 386
			public short MinorSubsystemVersion;

			// Token: 0x04000183 RID: 387
			public int Win32VersionValue;

			// Token: 0x04000184 RID: 388
			public int SizeOfImage;

			// Token: 0x04000185 RID: 389
			public int SizeOfHeaders;

			// Token: 0x04000186 RID: 390
			public int CheckSum;

			// Token: 0x04000187 RID: 391
			public short Subsystem;

			// Token: 0x04000188 RID: 392
			public short DllCharacteristics;

			// Token: 0x04000189 RID: 393
			public int SizeOfStackReserve;

			// Token: 0x0400018A RID: 394
			public int SizeOfStackCommit;

			// Token: 0x0400018B RID: 395
			public int SizeOfHeapReserve;

			// Token: 0x0400018C RID: 396
			public int SizeOfHeapCommit;

			// Token: 0x0400018D RID: 397
			public int LoaderFlags;

			// Token: 0x0400018E RID: 398
			public int NumberOfRvaAndSizes;

			// Token: 0x0400018F RID: 399
			public MetadataReader.IMAGE_DATA_DIRECTORY ExportDirectory;

			// Token: 0x04000190 RID: 400
			public MetadataReader.IMAGE_DATA_DIRECTORY ImportDirectory;

			// Token: 0x04000191 RID: 401
			public MetadataReader.IMAGE_DATA_DIRECTORY ResourceDirectory;

			// Token: 0x04000192 RID: 402
			public MetadataReader.IMAGE_DATA_DIRECTORY ExceptionDirectory;

			// Token: 0x04000193 RID: 403
			public MetadataReader.IMAGE_DATA_DIRECTORY SecurityDirectory;

			// Token: 0x04000194 RID: 404
			public MetadataReader.IMAGE_DATA_DIRECTORY RelocationDirectory;

			// Token: 0x04000195 RID: 405
			public MetadataReader.IMAGE_DATA_DIRECTORY DebugDirectory;

			// Token: 0x04000196 RID: 406
			public MetadataReader.IMAGE_DATA_DIRECTORY ArchitectureDirectory;

			// Token: 0x04000197 RID: 407
			public MetadataReader.IMAGE_DATA_DIRECTORY Reserved;

			// Token: 0x04000198 RID: 408
			public MetadataReader.IMAGE_DATA_DIRECTORY TLSDirectory;

			// Token: 0x04000199 RID: 409
			public MetadataReader.IMAGE_DATA_DIRECTORY ConfigurationDirectory;

			// Token: 0x0400019A RID: 410
			public MetadataReader.IMAGE_DATA_DIRECTORY BoundImportDirectory;

			// Token: 0x0400019B RID: 411
			public MetadataReader.IMAGE_DATA_DIRECTORY ImportAddressTableDirectory;

			// Token: 0x0400019C RID: 412
			public MetadataReader.IMAGE_DATA_DIRECTORY DelayImportDirectory;

			// Token: 0x0400019D RID: 413
			public MetadataReader.IMAGE_DATA_DIRECTORY MetaDataDirectory;
		}

		// Token: 0x02000021 RID: 33
		public struct image_section_header
		{
			// Token: 0x0400019E RID: 414
			[FixedBuffer(typeof(byte), 8)]
			public MetadataReader.image_section_header.<name>e__FixedBuffer2 name;

			// Token: 0x0400019F RID: 415
			public int virtual_size;

			// Token: 0x040001A0 RID: 416
			public int virtual_address;

			// Token: 0x040001A1 RID: 417
			public int size_of_raw_data;

			// Token: 0x040001A2 RID: 418
			public int pointer_to_raw_data;

			// Token: 0x040001A3 RID: 419
			public int pointer_to_relocations;

			// Token: 0x040001A4 RID: 420
			public int pointer_to_linenumbers;

			// Token: 0x040001A5 RID: 421
			public short number_of_relocations;

			// Token: 0x040001A6 RID: 422
			public short number_of_linenumbers;

			// Token: 0x040001A7 RID: 423
			public int characteristics;

			// Token: 0x02000022 RID: 34
			[UnsafeValueType, CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, Size = 8)]
			public struct <name>e__FixedBuffer2
			{
				// Token: 0x040001A8 RID: 424
				public byte FixedElementField;
			}
		}

		// Token: 0x02000023 RID: 35
		public struct NETDirectory
		{
			// Token: 0x040001A9 RID: 425
			public int cb;

			// Token: 0x040001AA RID: 426
			public short MajorRuntimeVersion;

			// Token: 0x040001AB RID: 427
			public short MinorRuntimeVersion;

			// Token: 0x040001AC RID: 428
			public int MetaDataRVA;

			// Token: 0x040001AD RID: 429
			public int MetaDataSize;

			// Token: 0x040001AE RID: 430
			public int Flags;

			// Token: 0x040001AF RID: 431
			public int EntryPointToken;

			// Token: 0x040001B0 RID: 432
			public int ResourceRVA;

			// Token: 0x040001B1 RID: 433
			public int ResourceSize;

			// Token: 0x040001B2 RID: 434
			public int StrongNameSignatureRVA;

			// Token: 0x040001B3 RID: 435
			public int StrongNameSignatureSize;

			// Token: 0x040001B4 RID: 436
			public int CodeManagerTableRVA;

			// Token: 0x040001B5 RID: 437
			public int CodeManagerTableSize;

			// Token: 0x040001B6 RID: 438
			public int VTableFixupsRVA;

			// Token: 0x040001B7 RID: 439
			public int VTableFixupsSize;

			// Token: 0x040001B8 RID: 440
			public int ExportAddressTableJumpsRVA;

			// Token: 0x040001B9 RID: 441
			public int ExportAddressTableJumpsSize;

			// Token: 0x040001BA RID: 442
			public int ManagedNativeHeaderRVA;

			// Token: 0x040001BB RID: 443
			public int ManagedNativeHeaderSize;
		}

		// Token: 0x02000024 RID: 36
		public struct MetaDataHeader
		{
			// Token: 0x040001BC RID: 444
			public int Signature;

			// Token: 0x040001BD RID: 445
			public short MajorVersion;

			// Token: 0x040001BE RID: 446
			public short MinorVersion;

			// Token: 0x040001BF RID: 447
			public int Reserved;

			// Token: 0x040001C0 RID: 448
			public int VerionLenght;

			// Token: 0x040001C1 RID: 449
			public byte[] VersionString;

			// Token: 0x040001C2 RID: 450
			public short Flags;

			// Token: 0x040001C3 RID: 451
			public short NumberOfStreams;
		}

		// Token: 0x02000025 RID: 37
		public struct TableHeader
		{
			// Token: 0x040001C4 RID: 452
			public int Reserved_1;

			// Token: 0x040001C5 RID: 453
			public byte MajorVersion;

			// Token: 0x040001C6 RID: 454
			public byte MinorVersion;

			// Token: 0x040001C7 RID: 455
			public byte HeapOffsetSizes;

			// Token: 0x040001C8 RID: 456
			public byte Reserved_2;

			// Token: 0x040001C9 RID: 457
			public long MaskValid;

			// Token: 0x040001CA RID: 458
			public long MaskSorted;
		}

		// Token: 0x02000026 RID: 38
		public struct MetaDataStream
		{
			// Token: 0x040001CB RID: 459
			public int Offset;

			// Token: 0x040001CC RID: 460
			public int Size;

			// Token: 0x040001CD RID: 461
			public string Name;
		}

		// Token: 0x02000027 RID: 39
		public struct TableInfo
		{
			// Token: 0x040001CE RID: 462
			public string Name;

			// Token: 0x040001CF RID: 463
			public string[] names;

			// Token: 0x040001D0 RID: 464
			public MetadataReader.Types type;

			// Token: 0x040001D1 RID: 465
			public MetadataReader.Types[] ctypes;
		}

		// Token: 0x02000028 RID: 40
		public struct RefTableInfo
		{
			// Token: 0x040001D2 RID: 466
			public MetadataReader.Types type;

			// Token: 0x040001D3 RID: 467
			public MetadataReader.Types[] reftypes;

			// Token: 0x040001D4 RID: 468
			public int[] refindex;
		}

		// Token: 0x02000029 RID: 41
		public struct TableSize
		{
			// Token: 0x040001D5 RID: 469
			public int TotalSize;

			// Token: 0x040001D6 RID: 470
			public int[] Sizes;
		}

		// Token: 0x0200002A RID: 42
		public struct Table
		{
			// Token: 0x040001D7 RID: 471
			public long[][] members;
		}
	}
}
