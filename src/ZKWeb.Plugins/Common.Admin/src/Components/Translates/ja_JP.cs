﻿using System.Collections.Generic;
using ZKWeb.Localize;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;

namespace ZKWeb.Plugins.Common.Admin.src.Components.Translates {
	/// <summary>
	/// 日本语翻译
	/// </summary>
	[ExportMany, SingletonReuse]
	public class ja_JP : ITranslateProvider {
		private static HashSet<string> Codes = new HashSet<string>() { "ja-JP" };
		private static Dictionary<string, string> Translates = new Dictionary<string, string>()
		{
			{ "Admin Login", "管理者登録" },
			{ "Please enter username", "ユーザー名を入力してください" },
			{ "Please enter password", "パスワードを入力してください" },
			{ "Username", "ユーザー名" },
			{ "Password", "パスワード" },
			{ "Login", "ログイン" },
			{ "Register new user", "ユーザー登録" },
			{ "RememberLogin", "ログインしたままにする" },
			{ "Register", "登録" },
			{ "ConfirmPassword", "パスワードの確認" },
			{ "Please repeat the password exactly", "正確にパスワードを繰り返してください" },
			{ "OldPassword", "元のパスワード" },
			{ "Please enter old password", "元のパスワードを入力してください" },
			{ "User Registration", "ユーザー登録" },
			{ "User Login", "ユーザーログイン" },
			{ "Username is already taken, please choose other username", "ユーザー名はすでに使われています、別のユーザー名を選択してください" },
			{ "You have registered successfully, thanks for you registration", "登録に成功しました、ご登録ありがとうございました" },
			{ "User Panel", "ユーザーパネル" },
			{ "Login successful", "ログインに成功しました" },
			{ "Welcome to ", "ようこそ " },
			{ "Logout", "ログアウト" },
			{ "Register for free", "無料登録" },
			{ "Website has no admin yet, the first login user will become super admin.",
				"管理者が存在しないため、最初にログインしたユーザーはスーパー管理者になります" },
			{ "You have already logged in, continue will replace the logged in user.",
				"あなたはすでにログインしています、続けると現在ログインしているユーザーを変えることになります" },
			{ "Sorry, You have no privileges to use admin panel.", "申し訳ありませんが、あなたは管理者パネルを使用する権限を持っていません" },
			{ "Incorrect username or password", "ユーザー名またはパスワードが違います" },
			{ "Apps", "アプリ" },
			{ "Workspace", "ワークスペース" },
			{ "Website Index", "ホームページ" },
			{ "About Me", "現在のユーザーについて" },
			{ "About Website", "ウェイブサイトについて" },
			{ "Admin Panel", "管理者パネル" },
			{ "My Apps", "アプリリスト" },
			{ "Action require {0}, and {1} privileges", "このアクションを実行するためにはロール{0}と権限{1}が必要です" },
			{ "Action require {0}", "このアクションを実行するためにはロール{0}が必要です" },
			{ "User", "ユーザー" },
			{ "UserType", "ユーザータイプ" },
			{ "Admin", "管理者" },
			{ "SuperAdmin", "スーパー管理者" },
			{ "CooperationPartner", "パートナー" },
			{ "CreateTime", "作成時刻" },
			{ "Admin Manage", "管理者管理" },
			{ "User Manage", "ユーザー管理" },
			{ "Role Manage", "ロール管理" },
			{ "Role", "ロール" },
			{ "Roles", "ロール" },
			{ "UserRole", "ユーザーロール" },
			{ "View", "閲覧" },
			{ "Edit", "編集" },
			{ "Delete", "削除" },
			{ "DeleteForever", "完全削除" },
			{ "Please enter name", "名称を入力してください" },
			{ "Remark", "リマーク" },
			{ "Please enter remark", "リマークを入力してください" },
			{ "Keep empty if you don't want to change", "変更したくない場合は空のままにしてください" },
			{ "Name/Remark", "名称/リマーク" },
			{ "Name", "名称" },
			{ "Value", "値" },
			{ "DirectoryName", "ディレクトリ名" },
			{ "Description", "説明" },
			{ "UpdateTime", "最終更新" },
			{ "Add {0}", "{0}を追加する" },
			{ "Edit {0}", "{0}を編集する" },
			{ "Delete {0}", "{0}を削除する" },
			{ "Please enter password when creating admin", "管理者を作成する際はパスワードの入力が必要です" },
			{ "Please enter password when creating user", "ユーザーを作成する際はパスワードの入力が必要です" },
			{ "You can't downgrade yourself to normal admin", "自分のスーパー管理者権限を取り消すことはできません" },
			{ "Privileges", "権限" },
			{ "Recycle Bin", "ごみ箱" },
			{ "Batch Delete", "バッチ削除" },
			{ "Please select {0} to delete", "削除する{0}を選んでください" },
			{ "Sure to delete following {0}?", "本当に以下の{0}を削除しますか？" },
			{ "Batch Recover", "バッチ回復" },
			{ "Please select {0} to recover", "回復する{0}を選んでください" },
			{ "Sure to recover following {0}?", "本当に以下の{0}を回復しますか？" },
			{ "Batch Delete Forever", "バッチ完全削除" },
			{ "Sure to delete following {0} forever?", "本当に以下の{0}を完全削除しますか？一度削除したら戻れません！" },
			{ "Delete yourself is not allowed", "自身を削除することはできません" },
			{ "Action {0} not exist", "アクション{0}を見つかりませんでした" },
			{ "Delete Successful", "削除に成功しました" },
			{ "Batch Delete Successful", "バッチ削除に成功しました" },
			{ "Batch Recover Successful", "バッチ回復に成功しました" },
			{ "Batch Delete Forever Successful", "バッチ完全削除に成功しました" },
			{ "Change Password", "パスワード変更" },
			{ "Change Avatar", "アバター変更" },
			{ "Avatar", "アバター" },
			{ "DeleteAvatar", "アバター削除" },
			{ "Please select avatar file", "アバターにする画像を選んでください" },
			{ "Parse uploaded image failed", "アップロードした画像ファイルの解析に失敗しました" },
			{ "User not found", "ユーザーが存在しません" },
			{ "Incorrect old password", "元のパスワードが違います、もう一度入力してください" },
			{ "No Role", "ロールなし" },
			{ "Website Name", "ウェイブサイト名" },
			{ "Default Language", "デフォルト言語" },
			{ "Default Timezone", "デフォルトタイムゾーン" },
			{ "Hosting Information", "サーバ情報" },
			{ "Plugin List", "プラグインリスト" },
			{ "Admin panel and users management", "管理者パネルとユーザー・ロールの管理" },
			{ "Clear Cache", "キャッシュ消去" },
			{ "Clear Cache Successfully", "キャッシュ消去に成功しました" },
			{ "Server Username", "サーバのユーザ名" },
			{ "Version", "バージョン" },
			{ "FullVersion", "フルバージョン" },
			{ "ZKWeb Version", "ZKWebのバージョン" },
			{ "ZKWeb Full Version", "ZKWebのフルバージョン" },
			{ "Memory Usage", "メモリ使用量" },
			{ "Action require the ownership of {0}: {1}",
				"このアクションを実行するためには以下の{0}の所有権が必要です: {1}" },
			{ "System Manage", "システム管理" },
			{ "Other", "その他" },
			{ "Toggle navigation", "ナビを畳む/展開する" },
			{ "Recover", "戻す" },
			{ "Delete Forever", "完全削除" },
			{ "Recover {0}", "{0}を戻す" },
			{ "Delete {0} Forever", "{0}を完全削除する" },
			{ "Add", "追加" },
			{ "Return To List", "一覧に戻る" }
		};

		public bool CanTranslate(string code) {
			return Codes.Contains(code);
		}

		public string Translate(string text) {
			return Translates.GetOrDefault(text);
		}
	}
}