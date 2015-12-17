﻿using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Core;
using ZKWeb.Plugins.Common.Base.src.Config;

namespace ZKWeb.Plugins.Common.Base.src.TemplateFilters {
	/// <summary>
	/// 模板系统的过滤器
	/// </summary>
	public static class Filters {
		/// <summary>
		/// 网站标题
		/// 例子
		///		{{ "Website Title" | website_title }}
		///	格式
		///		见WebsiteSettings.DocumentTitleFormat
		/// </summary>
		/// <param name="text">需要翻译的文本</param>
		/// <returns></returns>
		public static string WebsiteTitle(string title) {
			var configManager = Application.Ioc.Resolve<GenericConfigManager>();
			var settings = configManager.GetData<WebsiteSettings>();
			if (!string.IsNullOrEmpty(settings.DocumentTitleFormat)) {
				title = (settings.DocumentTitleFormat
					.Replace("{title}", new T(title))
					.Replace("{websiteName}", new T(settings.WebsiteName)));
			}
			return title;
		}
	}
}