﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Attributes;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Extensions;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Interfaces;
using ZKWeb.Plugins.Common.Base.src.UIComponents.ListItems.Interfaces;
using ZKWeb.Templating;
using ZKWebStandard.Collection;
using ZKWebStandard.Ioc;

namespace ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Handlers {
	/// <summary>
	/// 勾选框分组
	/// 显示结构
	/// ▢ 全选
	/// 　▢ 多选框A ▢ 多选框B ▢ 多选框C
	/// </summary>
	[ExportMany(ContractKey = typeof(CheckBoxGroupFieldAttribute)), SingletonReuse]
	public class CheckBoxGroupFieldHandler : IFormFieldHandler {
		/// <summary>
		/// 获取表单字段的html
		/// </summary>
		public string Build(FormField field, IDictionary<string, string> htmlAttributes) {
			var attribute = (CheckBoxGroupFieldAttribute)field.Attribute;
			var listItemProvider = (IListItemProvider)Activator.CreateInstance(attribute.Source);
			var listItems = listItemProvider.GetItems().ToList();
			var templateManager = Application.Ioc.Resolve<TemplateManager>();
			var fieldHtml = templateManager.RenderTemplate("common.base/tmpl.form.hidden.html", new {
				name = field.Attribute.Name,
				value = JsonConvert.SerializeObject(field.Value ?? new string[0]),
				attributes = htmlAttributes
			});
			var checkboxGroup = templateManager.RenderTemplate("common.base/tmpl.checkbox_group.html", new {
				items = listItems,
				fieldName = field.Attribute.Name,
				fieldHtml = new HtmlString(fieldHtml)
			});
			return field.WrapFieldHtml(htmlAttributes, checkboxGroup);
		}

		/// <summary>
		/// 解析提交的字段的值
		/// </summary>
		public object Parse(FormField field, IList<string> values) {
			return JsonConvert.DeserializeObject<IList<string>>(values[0]);
		}
	}
}
