using System;

namespace Plugin.PEImageView.Directory
{
	public class DocumentBinarySettings : DocumentBaseSettings
	{
		public PeHeaderType Header { get; set; }

		public String NodeName { get; set; }
	}
}