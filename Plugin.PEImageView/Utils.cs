using System;

namespace Plugin.PEImageView
{
	internal static class Utils
	{
		public static TEnum EnumParse<TEnum>(String value, TEnum defaultValue) where TEnum : struct
			=> Enum.IsDefined(typeof(TEnum), value)
				? (TEnum)Enum.Parse(typeof(TEnum), value)
				: defaultValue;
	}
}