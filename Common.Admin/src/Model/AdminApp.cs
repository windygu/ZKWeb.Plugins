﻿using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZKWeb.Core;

namespace ZKWeb.Plugins.Common.Admin.src.Model {
	/// <summary>
	/// 后台应用的基础类
	/// </summary>
	public abstract class AdminApp : ILiquidizable {
		/// <summary>
		/// 应用名称
		/// </summary>
		public abstract string Name { get; }
		/// <summary>
		/// url链接
		/// </summary>
		public abstract string Url { get; }
		/// <summary>
		/// 格式的css类名
		/// </summary>
		public virtual string TileClass {
			get { return "tile bg-grey-gallery"; }
		}
		/// <summary>
		/// 图标的css类名
		/// </summary>
		public virtual string IconClass {
			get { return "fa fa-archive"; }
		}
		/// <summary>
		/// 允许显示此应用的用户类型列表
		/// </summary>
		public virtual UserTypes[] AllowedUserTypes {
			get { return AdminManager.AdminTypes; }
		}
		/// <summary>
		/// 显示此应用要求的权限列表
		/// </summary>
		public virtual string[] RequiredPrivileges {
			get { return new string[0]; }
		}

		/// <summary>
		/// 允许直接描画到模板
		/// </summary>
		/// <returns></returns>
		object ILiquidizable.ToLiquid() {
			return new HtmlString(ToString());
		}

		/// <summary>
		/// 获取Html代码
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			var html = new StringBuilder();
			var name = HttpUtility.HtmlAttributeEncode(new T(Name));
			var tileClass = HttpUtility.HtmlAttributeEncode(TileClass);
			var url = HttpUtility.HtmlAttributeEncode(Url);
			var iconClass = HttpUtility.HtmlAttributeEncode(IconClass);
			html.AppendFormat("<a title='{0}' class='{1}' href='{2}'>", name, tileClass, url);
			html.AppendLine();
			html.AppendFormat("	<div class='tile-body'><i class='{0}'></i></div>", IconClass);
			html.AppendLine();
			html.AppendFormat("	<div class='tile-object'><div class='name'>{0}</div></div>", name);
			html.AppendLine();
			html.AppendLine("</a>");
			return html.ToString();
		}
	}
}