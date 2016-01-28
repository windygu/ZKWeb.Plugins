﻿using DryIoc;
using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Model;
using ZKWeb.Plugins.Common.Base.src;
using ZKWeb.Plugins.Common.Currency.src.Config;
using ZKWeb.Plugins.Common.Currency.src.Model;
using ZKWeb.Utils.Extensions;

namespace ZKWeb.Plugins.Common.Currency.src {
	/// <summary>
	/// 货币管理器
	/// </summary>
	[ExportMany, SingletonReuse]
	public class CurrencyManager : ICacheCleaner {
		/// <summary>
		/// 货币类型到货币信息的缓存
		/// </summary>
		protected IDictionary<string, ICurrency> CurrencyCache { get; set; }

		/// <summary>
		/// 获取默认的货币信息，找不到时返回null
		/// </summary>
		/// <returns></returns>
		public virtual ICurrency GetDefaultCurrency() {
			var configManager = Application.Ioc.Resolve<GenericConfigManager>();
			var settings = configManager.GetData<CurrencySettings>();
			return GetCurrency(settings.DefaultCurrency);
		}

		/// <summary>
		/// 获取货币信息，找不到时返回null
		/// </summary>
		/// <param name="type">货币类型，区分大小写</param>
		/// <returns></returns>
		public virtual ICurrency GetCurrency(string type) {
			if (CurrencyCache == null) {
				var cache = Application.Ioc.ResolveMany<ICurrency>().ToDictionary(c => c.Type);
				CurrencyCache = cache;
			}
			return CurrencyCache.GetOrDefault(type);
		}

		/// <summary>
		/// 清理缓存
		/// </summary>
		public void ClearCache() {
			CurrencyCache = null;
		}
	}
}
