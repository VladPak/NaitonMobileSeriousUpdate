using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Models
{
	public class Rack
	{
		[JsonProperty("stockrackid")]
		public int StockRackId { get; set; }

		[JsonProperty("stockid")]
		public long StockId { get; set; }

		[JsonProperty("stockrackname")]
		public string StockRackName { get; set; }

		[JsonProperty("sequence")]
		public long Sequence { get; set; }

		[JsonProperty("length")]
		public long Length { get; set; }

		[JsonProperty("width")]
		public long Width { get; set; }

		[JsonProperty("height")]
		public long Height { get; set; }

		[JsonProperty("maxweight")]
		public long MaxWeight { get; set; }

		[JsonProperty("orderpicking")]
		public bool OrderPicking { get; set; }

		[JsonProperty("crossdock")]
		public bool CrossDock { get; set; }

		[JsonProperty("packages")]
		public bool? Packages { get; set; }

		[JsonProperty("inactive")]
		public bool Inactive { get; set; }
	}
}
