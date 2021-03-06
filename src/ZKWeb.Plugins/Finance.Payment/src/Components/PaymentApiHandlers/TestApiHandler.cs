﻿using System.ComponentModel.DataAnnotations;
using ZKWeb.Localize;
using ZKWeb.Logging;
using ZKWeb.Templating;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;
using ZKWebStandard.Collection;
using ZKWeb.Plugins.Finance.Payment.src.Domain.Entities;
using ZKWeb.Plugins.Common.Base.src.UIComponents.ScriptStrings;
using ZKWeb.Plugins.Finance.Payment.src.Domain.Services;
using ZKWeb.Plugins.Finance.Payment.src.Domain.Enums;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms.Attributes;
using ZKWeb.Plugins.Common.Base.src.UIComponents.Forms;
using ZKWeb.Plugins.Common.Base.src.Components.Exceptions;
using ZKWeb.Plugins.Finance.Payment.src.Components.PaymentApiHandlers.Interfaces;
using ZKWeb.Plugins.Finance.Payment.src.UIComponents.Forms;
using ZKWeb.Plugins.Common.Base.src.UIComponents.ListItems;
using System;
using ZKWeb.Plugins.Finance.Payment.src.Domain.Extensions;
using ZKWeb.Plugins.Common.Currency.src.Domain.Service;
using ZKWeb.Plugins.Common.Currency.src.Components.Interfaces;
using ZKWeb.Plugins.Common.Base.src.Domain.Uow.Interfaces;

namespace ZKWeb.Plugins.Finance.Payment.src.Components.PaymentApiHandlers {
	/// <summary>
	/// 测试接口的处理器
	/// </summary>
	[ExportMany]
	public class TestApiHandler : IPaymentApiHandler {
		/// <summary>
		/// 接口类型
		/// </summary>
		public string Type { get { return "TestApi"; } }
		/// <summary>
		/// 编辑中的接口数据
		/// </summary>
		protected ApiData ApiDataEditing = new ApiData();

		/// <summary>
		/// 后台编辑表单创建后的处理
		/// </summary>
		public void OnFormCreated(PaymentApiEditForm form) {
			form.AddFieldsFrom(ApiDataEditing);
		}

		/// <summary>
		/// 后台编辑表单绑定时的处理
		/// </summary>
		public void OnFormBind(PaymentApiEditForm form, PaymentApi bindFrom) {
			var apiData = bindFrom.ExtraData.GetOrDefault<ApiData>("ApiData") ?? new ApiData();
			ApiDataEditing.PaymentPassword = apiData.PaymentPassword;
		}

		/// <summary>
		/// 后台编辑表单保存时的处理
		/// </summary>
		public void OnFormSubmit(PaymentApiEditForm form, PaymentApi saveTo) {
			saveTo.ExtraData["ApiData"] = ApiDataEditing;
		}

		/// <summary>
		/// 计算支付手续费
		/// </summary>
		public void CalculatePaymentFee(PaymentApi api, decimal amount, ref decimal paymentFee) {
			paymentFee = 0;
		}

		/// <summary>
		/// 获取支付Html
		/// </summary>
		public void GetPaymentHtml(PaymentTransaction transaction, ref HtmlString html) {
			var templateManager = Application.Ioc.Resolve<TemplateManager>();
			var form = new TestApiPayForm(transaction);
			form.Bind();
			html = new HtmlString(templateManager.RenderTemplate("finance.payment/test_api_pay.html", new { form }));
		}

		/// <summary>
		/// 调用发货接口
		/// 发货后自动确认收货
		/// </summary>
		public void DeliveryGoods(
			PaymentTransaction transaction, string logisticsName, string invoiceNo) {
			var logManager = Application.Ioc.Resolve<LogManager>();
			logManager.LogTransaction(string.Format(
				"PaymentApi send goods: transaction {0} logisticsName {1} invoiceNo {2}",
				transaction.Serial, logisticsName, invoiceNo));
			var transactionManager = Application.Ioc.Resolve<PaymentTransactionManager>();
			transactionManager.Process(transaction.Id, null, PaymentTransactionState.Success);
		}

		/// <summary>
		/// 接口数据
		/// </summary>
		public class ApiData {
			/// <summary>
			/// 支付密码
			/// </summary>
			[Required]
			[StringLength(100, MinimumLength = 5)]
			[PasswordField("PaymentPassword", "Password required to pay transactions")]
			public string PaymentPassword { get; set; }

			/// <summary>
			/// 检查支付密码是否和设置的密码一致
			/// </summary>
			public void CheckPaymentPassword(string paymentPassword) {
				if (string.IsNullOrEmpty(PaymentPassword) || paymentPassword != PaymentPassword) {
					throw new ForbiddenException(new T("Incorrect payment password"));
				}
			}
		}

		/// <summary>
		/// 测试接口支付时使用的表单
		/// </summary>
		[Form("TestApiPayForm", "/admin/payment_apis/test_api_pay")]
		public class TestApiPayForm : ModelFormBuilder {
			/// <summary>
			/// 需要支付的交易
			/// </summary>
			public PaymentTransaction Transaction { get; set; }
			/// <summary>
			/// 交易Id
			/// </summary>
			[HiddenField("Id")]
			public Guid Id { get; set; }
			/// <summary>
			/// 流水号
			/// </summary>
			[LabelField("Serial")]
			public string Serial { get; set; }
			/// <summary>
			/// 货币
			/// </summary>
			[LabelField("Currency")]
			public string Currency { get; set; }
			/// <summary>
			/// 金额
			/// </summary>
			[LabelField("Amount")]
			public string Amount { get; set; }
			/// <summary>
			/// 交易描述
			/// </summary>
			[LabelField("Description")]
			public string Description { get; set; }
			/// <summary>
			/// 支付密码
			/// </summary>
			[Required]
			[PasswordField("PaymentPassword", "PaymentPassword")]
			public string PaymentPassword { get; set; }
			/// <summary>
			/// 支付类型
			/// 即时到账或担保交易
			/// </summary>
			[Required]
			[RadioButtonsField("PayType", typeof(ListItemFromEnum<PayTypes>))]
			public int PayType { get; set; }

			/// <summary>
			/// 初始化
			/// 绑定时使用
			/// </summary>
			public TestApiPayForm(PaymentTransaction transaction) {
				Transaction = transaction;
			}

			/// <summary>
			/// 初始化
			/// 提交时使用
			/// </summary>
			/// <param name="transactionId"></param>
			public TestApiPayForm(Guid transactionId) {
				var transactionManager = Application.Ioc.Resolve<PaymentTransactionManager>();
				var uow = Application.Ioc.Resolve<IUnitOfWork>();
				using (uow.Scope()) {
					var transaction = transactionManager.Get(transactionId);
					// 检查交易是否为空
					if (transaction == null) {
						throw new BadRequestException(new T("Payment transaction not found"));
					}
					// 检查交易对应的接口类型是否测试接口
					if (transaction.Api.Type != "TestApi") {
						throw new ForbiddenException(
							new T("Use test api to pay transaction created with other api type is not allowed"));
					}
					// 检查当前登录用户是否可支付
					var result = transaction.Check(c => c.IsPayerLoggedIn);
					if (!result.First) {
						throw new ForbiddenException(result.Second);
					}
					Transaction = transaction;
				}
			}

			/// <summary>
			/// 绑定表单
			/// </summary>
			protected override void OnBind() {
				var currencyManager = Application.Ioc.Resolve<CurrencyManager>();
				var currency = currencyManager.GetCurrency(Transaction.CurrencyType);
				Id = Transaction.Id;
				Serial = Transaction.Serial;
				Currency = new T(currency.Type);
				Amount = currency.Format(Transaction.Amount);
				Description = Transaction.Description;
				PaymentPassword = null;
				PayType = (int)PayTypes.ImmediateArrival;
			}

			/// <summary>
			/// 提交表单
			/// </summary>
			/// <returns></returns>
			protected override object OnSubmit() {
				// 检查支付密码是否正确
				var apiData = Transaction.Api.ExtraData.GetOrDefault<ApiData>("ApiData") ?? new ApiData();
				apiData.CheckPaymentPassword(PaymentPassword);
				// 按类型执行支付操作
				var transactionManager = Application.Ioc.Resolve<PaymentTransactionManager>();
				var state = PayType == (int)PayTypes.ImmediateArrival ?
					PaymentTransactionState.Success : PaymentTransactionState.SecuredPaid;
				transactionManager.Process(Transaction.Id, null, state);
				// 返回成功并跳转到交易结果页
				var resultUrl = string.Format("/payment/transaction/pay_result?id={0}", Transaction.Id);
				return new {
					message = new T("Payment Successfully, Redirecting to result page..."),
					script = BaseScriptStrings.Redirect(resultUrl, 3000)
				};
			}
		}

		/// <summary>
		/// 支付类型
		/// </summary>
		public enum PayTypes {
			/// <summary>
			/// 即时到账
			/// </summary>
			ImmediateArrival = 0,
			/// <summary>
			/// 担保交易
			/// </summary>
			SecuredTransaction = 1,
		}
	}
}
