# Slash Commands GUI Tool
Slash commands for discord bot
![banner](https://github.com/user-attachments/assets/787e5e92-d523-42e1-aadf-f9bca00f8de2)

## Other Languages
[Traditional Chinese 繁體中文](README_zhtw.md)

## Features
- [x] Bot editing
- [x] Local editing
- [x] Edit commands
- [x] Edit options
- [x] Edit parameters
- [x] Edit Localizations(name, description)
- [x] Manage Multi-client
- [ ] Edit member permissions(Not yet done)

## Application Features
- [x] Support multiple languages (Chinese, English)
- [x] Encrypted storage of Client token
- [x] Load and Save json file for slash commands
- [x] Dark Mode
- [x] Clean up data

This tool use discord api v10, support command type 1 to 11:
SUB_COMMAND, SUB_COMMAND_GROUP, STRING, INTEGER , BOOLEAN, USER, CHANNEL ,ROLE , MENTIONABLE, NUMBER, ATTACHMENT

## Require
~~[.NET 8.0 Desktop Runtime (v8.0.12)](https://dotnet.microsoft.com/zh-tw/download/dotnet/thank-you/runtime-desktop-8.0.12-windows-x64-installer?cid=getdotnetcore)~~ <br> 
→Only lite version! If you download full version, you don't need to install this dependencies!  
The application ONLY run on Windows.  

## How to use
🤖On Bot eding:  First, click "Create client". After adding the client, you can start editing the slash commands.<br>
<br>
🖥️On Local eding: You can click on the new command(+) to start editing, but remember to click Save after editing.<br>
                Next time, you can click Load to load the last command.

## Reporting Issues or Requesting Features
If you encounter any issues while using this software or have suggestions for new features, please use the [Issues](https://github.com/CreeperJIMM/slash-commands-gui-tool/issues) page to let us know.

1. Click on the [Issues](https://github.com/your-repo-owner/your-repo-name/issues) tab.
2. Select the **New Issue** button.
3. Provide a clear title and description, explaining the problem or feature request in detail.
4. Submit the issue, and we will review it as soon as possible.

Your feedback and contributions are greatly appreciated and help us improve the project. Thank you for your support!

## Security Measures

Our application employs dual-layer encryption for your client token using DXAPI and AES. The encrypted token is securely stored in the `%localappdata%/SlashCommandsGUITool/database.db` file. This ensures that even if someone gains unauthorized access to your local database file, they cannot retrieve or decrypt your token. 
We take your security and privacy seriously and are committed to protecting your sensitive information.

## Notices
If you switch the command to the other side without saving (such as switching from Bot to Local), some problems may occur. It is recommended to switch after saving.

### Screenshots:

![image](https://github.com/user-attachments/assets/5cd45e9b-8e35-4981-ac8d-3854666b9406)
![image](https://github.com/user-attachments/assets/141d8da7-60c0-4b83-b84d-acf30c4a8ee1)
![option](https://github.com/user-attachments/assets/7c48214d-d5f7-4fe0-8946-3b836fae03ce)
