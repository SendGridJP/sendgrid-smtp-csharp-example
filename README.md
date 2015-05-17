# sendgrid-smtp-csharp-example
=========================

本コードはSendGridのSMTPサービス利用サンプルです。

## 使い方

```bash
git clone http://github.com/sendgridjp/sendgrid-smtp-csharp-example.git
cd sendgrid-smtp-csharp-example
copy org.App.config App.config
# SendGridSmtpCsharpExample.slnファイル開きます。
# App.configファイルを編集してください
# 実行(F5キー)します。
```

## App.configファイルの編集
App.configファイルは以下のような内容になっています。

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <startup> 
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
  <appSettings>
    <add key="SENDGRID_USERNAME" value="YOUR_SENDGRID_USERNAME" />
    <add key="SENDGRID_PASSWORD" value="YOUR_SENDGRID_PASSWORD" />
    <add key="TOS" value="hoge@hogehoge.com,fuga@fugafuga.com,piyo@piyopiyo.com,hogera@hogera.com" />
    <add key="NAMES" value="名前1,名前2,名前3,名前4" />
    <add key="FROM" value="you@youremail.com" />
  </appSettings>
</configuration>
```
SENDGRID_USERNAME:SendGridのユーザ名を指定してください。  
SENDGRID_PASSWORD:SendGridのパスワードを指定してください。  
TOS:宛先をカンマ区切りで指定してください。  
NAMES:宛先毎の宛名をカンマ区切りで指定してください。  
FROM:送信元アドレスを指定してください。  
