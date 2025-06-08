# Slash Commands GUI Tool
Discord機器人用的 Slash Commands 工具
![banner](https://github.com/user-attachments/assets/787e5e92-d523-42e1-aadf-f9bca00f8de2)

## 功能
- [x] 機器人編輯
- [x] 本地編輯
- [x] 編輯指令
- [x] 編輯選項
- [x] 編輯參數
- [x] 編輯本地化（名稱、描述）
- [x] 管理多客戶端
- [ ] 編輯成員權限（尚未完成）

## 應用功能
- [x] 支援多語言（繁體中文、英文）
- [x] 客戶端Token加密存儲
- [x] 讀取及儲存json格式的slash指令
- [x] 深色模式
- [x] 資料清理

此工具使用 Discord API v10，支援指令類型 1 至 11：
SUB_COMMAND、SUB_COMMAND_GROUP、STRING、INTEGER、BOOLEAN、USER、CHANNEL、ROLE、MENTIONABLE、NUMBER、ATTACHMENT

## 需求
~~[.NET 8.0 桌面運行時(版本8.0.12)](https://dotnet.microsoft.com/zh-tw/download/dotnet/thank-you/runtime-desktop-8.0.12-windows-x64-installer?cid=getdotnetcore)~~ <br> 
→僅支援简易版本！若下載完整版本，則不需安裝此依賴！<br> 
此應用僅支援Windows系統。

## 使用流程
🤖在機器人編輯：首先點擊「建立客戶端」，加入客戶端後即可開始編輯slash指令。<br>
<br>
🖥️在本地編輯：點擊新增指令（+）即可開始編輯，但請記得點擊儲存後才生效。<br>
                下一次可以點擊載入以載入上次的指令。

## 反饋問題或功能建議
若在使用此軟體時遇到問題或有新功能需求，請至 [Issues](https://github.com/CreeperJIMM/slash-commands-gui-tool/issues) 頁面反映。

1. 點擊 [Issues](https://github.com/your-repo-owner/your-repo-name/issues) 標籤。
2. 選擇 **新增議題**。
3. 提供清楚的標題及詳細描述問題或需求。
4. 提交後，我們將盡快審核。

您的反饋與貢獻對我們非常重要，感謝您的支持！

## 安全措施

我們的應用程序採用雙層加密技術，對您的Client Token進行DXAPI與AES雙重加密。加密後的Token安全存於 `AppData/Local/SlashCommandsGUITool/database.db` 文件中，確保即使有人未授權存取，也無法解密提取您的Token。我們非常重視您的隱私與資料安全。

## 注意事項
切換指令到另一端（如從機器人切換到本地）而未儲存可能會出現問題，建議在切換前先儲存。
