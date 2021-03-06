﻿using System.ComponentModel.DataAnnotations;
using ZKWeb.Localize;
using ZKWeb.Plugins.Common.Admin.src.Domain.Services;
using ZKWeb.Plugins.Common.AdminSettings.src.Controllers.Bases;
using ZKWeb.Plugins.Common.Base.src.Components.GenericConfigs;
using ZKWeb.Plugins.Common.Base.src.Domain.Services;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Attributes;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Interfaces;
using ZKWebStandard.Ioc;
using ZKWebStandard.Web;

namespace ZKWeb.Plugins.Common.AdminSettings.src.Controllers {
	/// <summary>
	/// 网站设置
	/// </summary>
	[ExportMany]
	public class WebsiteSettingsController : FormAdminSettingsControllerBase {
		public override string Group { get { return "BaseSettings"; } }
		public override string GroupIconClass { get { return "fa fa-wrench"; } }
		public override string Name { get { return "WebsiteSettings"; } }
		public override string IconClass { get { return "fa fa-globe"; } }
		public override string Url { get { return "/admin/settings/website_settings"; } }
		public override string Privilege { get { return "AdminSettings:WebsiteSettings"; } }
		protected override IModelFormBuilder GetForm() { return new Form(); }

		/// <summary>
		/// 表单
		/// </summary>
		public class Form : ModelFormBuilder {
			/// <summary>
			/// 网站名称
			/// </summary>
			[Required]
			[TextBoxField("WebsiteName", "WebsiteName")]
			public string WebsiteName { get; set; }
			/// <summary>
			/// 网页标题的默认格式
			/// </summary>
			[Required]
			[TextBoxField("DocumentTitleFormat", "Default is {title} - {websiteName}")]
			public string DocumentTitleFormat { get; set; }
			/// <summary>
			/// 版权信息
			/// </summary>
			[TextBoxField("CopyrightText", "CopyrightText")]
			public string CopyrightText { get; set; }
			/// <summary>
			/// 页面关键词
			/// </summary>
			[TextBoxField("PageKeywords", "PageKeywords")]
			public string PageKeywords { get; set; }
			/// <summary>
			/// 页面描述
			/// </summary>
			[TextBoxField("PageDescription", "PageDescription")]
			public string PageDescription { get; set; }
			/// <summary>
			/// 前台Logo
			/// </summary>
			[FileUploaderField("FrontPageLogo")]
			public IHttpPostedFile FrontPageLogo { get; set; }
			/// <summary>
			/// 后台Logo
			/// </summary>
			[FileUploaderField("AdminPanelLogo")]
			public IHttpPostedFile AdminPanelLogo { get; set; }
			/// <summary>
			/// 页面图标
			/// </summary>
			[FileUploaderField("Favicon")]
			public IHttpPostedFile Favicon { get; set; }
			/// <summary>
			/// 恢复默认前台Logo
			/// </summary>
			[CheckBoxField("RestoreDefaultFrontPageLogo")]
			public bool RestoreDefaultFrontPageLogo { get; set; }
			/// <summary>
			/// 恢复默认后台Logo
			/// </summary>
			[CheckBoxField("RestoreDefaultAdminPanelLogo")]
			public bool RestoreDefaultAdminPanelLogo { get; set; }
			/// <summary>
			/// 恢复默认页面图标
			/// </summary>
			[CheckBoxField("RestoreDefaultFavicon")]
			public bool RestoreDefaultFavicon { get; set; }

			/// <summary>
			/// 提交表单
			/// </summary>
			protected override void OnBind() {
				var configManager = Application.Ioc.Resolve<GenericConfigManager>();
				var settings = configManager.GetData<WebsiteSettings>();
				WebsiteName = settings.WebsiteName;
				DocumentTitleFormat = settings.DocumentTitleFormat;
				PageKeywords = settings.PageKeywords;
				PageDescription = settings.PageDescription;
				CopyrightText = settings.CopyrightText;
			}

			/// <summary>
			/// 绑定表单
			/// </summary>
			/// <returns></returns>
			protected override object OnSubmit() {
				// 保存设置
				var configManager = Application.Ioc.Resolve<GenericConfigManager>();
				var settings = configManager.GetData<WebsiteSettings>();
				settings.WebsiteName = WebsiteName;
				settings.DocumentTitleFormat = DocumentTitleFormat;
				settings.PageKeywords = PageKeywords;
				settings.PageDescription = PageDescription;
				settings.CopyrightText = CopyrightText;
				configManager.PutData(settings);
				// 保存Logo
				var logoManager = Application.Ioc.Resolve<LogoManager>();
				if (RestoreDefaultFrontPageLogo) {
					logoManager.RestoreDefaultFrontPageLogo();
				} else if (FrontPageLogo != null) {
					logoManager.SaveFrontPageLogo(FrontPageLogo.OpenReadStream());
				}
				if (RestoreDefaultAdminPanelLogo) {
					logoManager.RestoreDefaultAdminPageLogo();
				} else if (AdminPanelLogo != null) {
					logoManager.SaveAdminPanelLogo(AdminPanelLogo.OpenReadStream());
				}
				if (RestoreDefaultFavicon) {
					logoManager.RestoreDefaultFavicon();
				} else if (Favicon != null) {
					logoManager.SaveFavicon(Favicon.OpenReadStream());
				}
				return new { message = new T("Saved Successfully") };
			}
		}
	}
}