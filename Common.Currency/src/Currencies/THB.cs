﻿using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Plugins.Common.Currency.src.Model;

namespace ZKWeb.Plugins.Common.Currency.src.Currencies {
	/// <summary>
	/// 泰铢
	/// </summary>
	[ExportMany]
	public class THB : ICurrency {
		public string Type { get { return "THB"; } }
		public string Prefix { get { return "฿"; } }
		public string Suffix { get { return null; } }
	}
}
