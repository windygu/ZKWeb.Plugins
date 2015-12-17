﻿using DryIoc;
using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using ZKWeb.Core;
using ZKWeb.Model;
using ZKWeb.Model.ActionResults;

namespace ZKWeb.Plugins.Common.Base.src.Controllers {
	[ExportMany]
	public class TestController : IController {
		[Action("/")]
		public string Index() {
			var sessionManager = Application.Ioc.Resolve<SessionManager>();
			var session = sessionManager.GetSession();
			session.Items["last updated"] = DateTime.UtcNow;
			sessionManager.SaveSession();
			return "test index, session = " + session.Id + " translate = " + new T("abc");
		}

		[Action("redirect")]
		public IActionResult Redirect() {
			return new RedirectResult("/");
		}

		[Action("json")]
		public IActionResult Json() {
			return new JsonResult(new { a = 1, b = 2, c = 3 });
		}

		[Action("file")]
		public IActionResult File() {
			return new FileResult(PathConfig.WebsiteConfigPath);
		}

		[Action("template")]
		public IActionResult Template() {
			return new TemplateResult("test.html",
				new {
					a = "<p>test encode</p>",
					b = "<b>test html</b>",
					c = typeof(TestController).Assembly.Location
				});
		}
	}
}