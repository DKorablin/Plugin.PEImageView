using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Plugin.PEImageView.Bll
{
	internal class XmlReflectionReader
	{
		private static XmlReflectionReader _instance;
		private readonly ConcurrentDictionary<Assembly, XmlDocument> _documents = new ConcurrentDictionary<Assembly, XmlDocument>();
		private readonly ConcurrentDictionary<String, String> _documentationCache = new ConcurrentDictionary<String, String>();

		public static XmlReflectionReader Instance => _instance ?? (_instance = new XmlReflectionReader());

		private XmlReflectionReader()
		{ }

		public XmlDocument LoadDocument(Assembly asm)
		{
			if(asm.GlobalAssemblyCache)
				return null;

			return this._documents.GetOrAdd(asm, delegate
			{
				XmlDocument doc;
				String path = GetXmlPath(asm.Location);
				if(!File.Exists(path))
				{
					path = GetXmlPath(new Uri(asm.CodeBase).LocalPath);
					if(!File.Exists(path))
						return null;
				}

				doc = new XmlDocument();
				doc.Load(path);
				return doc;
			});
		}

		public String FindDocumentation(MemberInfo type)
		{
			Assembly asm = type.Module.Assembly;
			if(asm.GlobalAssemblyCache)
				return null;

			String memberName = XmlReflectionReader.GetMemberName(type);
			return this._documentationCache.GetOrAdd(asm.GetName().Name + ">" + memberName, delegate
			{
				XmlDocument doc = this.LoadDocument(asm);
				if(doc == null)
					return null;

				var navigator = doc.CreateNavigator();
				var result = navigator.SelectSingleNode(String.Format("/doc/members/member[@name=\"{0}\"]/summary", memberName));
				return result == null
					? null
					: result.InnerXml.Trim().Replace("  ", " ");
			});
		}

		private static String GetXmlPath(String assemblyLocation)
		{
			String path = Path.GetDirectoryName(assemblyLocation);
			String xmlFileName = Path.GetFileNameWithoutExtension(assemblyLocation) + ".xml";
			return Path.Combine(path, xmlFileName);
		}

		private static String GetMemberName(MemberInfo type)
		{
			Char prefix;
			switch(type.MemberType)
			{
			case MemberTypes.Field:
				prefix = 'F';
				break;
			case MemberTypes.Property:
				prefix = 'P';
				break;
			case MemberTypes.TypeInfo:
				Type elementType = ((Type)type).GetRealType();
				if(elementType == type)
					prefix = 'T';
				else//Array[MyType]
					return GetMemberName(elementType);
				break;
			case MemberTypes.Method:
				prefix = 'M';
				break;
			default: throw new NotImplementedException();
			}

			String fullName = type.DeclaringType == null
				? type.ToString()
				: (type.DeclaringType.FullName + "." + type.Name);
			return prefix
				+ ":"
				+ fullName.Replace('+', '.');//Nested types declares as Namespace.Class+InnerClass.Field
		}
	}
}