using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Permissions
{
    public record PermissionItem(string Name, string LocalizedName, string Category, int BitOffset)
    {
        public ulong Value => 1UL << BitOffset;
    }

    public static class DiscordPermissionData
    {
        public static readonly PermissionItem[] AllPermissions = new[]
        {
            // --- General Permissions (一般權限) ---
            new PermissionItem("Administrator", "管理員", "General", 3),
            new PermissionItem("View Audit Log", "檢視審核日誌", "General", 7),
            new PermissionItem("Manage Server", "管理伺服器", "General", 5),
            new PermissionItem("Manage Roles", "管理身分組", "General", 28),
            new PermissionItem("Manage Channels", "管理頻道", "General", 4),
            new PermissionItem("Kick Members", "踢出成員", "General", 1),
            new PermissionItem("Ban Members", "封鎖成員", "General", 2),
            new PermissionItem("Create Instant Invite", "建立邀請", "General", 0),
            new PermissionItem("Change Nickname", "更改暱稱", "General", 26),
            new PermissionItem("Manage Nicknames", "管理暱稱", "General", 27),
            new PermissionItem("Manage Expressions", "管理表情符號與貼圖", "General", 30),
            new PermissionItem("Create Expressions", "建立表情符號與貼圖", "General", 44),
            new PermissionItem("Manage Webhooks", "管理 Webhooks", "General", 29),
            new PermissionItem("View Channels", "檢視頻道", "General", 10),
            new PermissionItem("Manage Events", "管理活動", "General", 33),
            new PermissionItem("Create Events", "建立活動", "General", 43),
            new PermissionItem("Moderate Members", "對成員停權", "General", 40),
            new PermissionItem("View Server Insights", "檢視伺服器洞察報告", "General", 19),
            new PermissionItem("View Server Subscription Insights", "檢視伺服器訂閱洞察報告", "General", 39),

            // --- Text Permissions (文字頻道權限) ---
            new PermissionItem("Send Messages", "發送訊息", "Text", 11),
            new PermissionItem("Create Public Threads", "建立公開討論串", "Text", 35),
            new PermissionItem("Create Private Threads", "建立私人討論串", "Text", 36),
            new PermissionItem("Send Messages in Threads", "在討論串中發送訊息", "Text", 38),
            new PermissionItem("Send TTS Messages", "發送讀稿機訊息", "Text", 12),
            new PermissionItem("Manage Messages", "管理訊息", "Text", 13),
            new PermissionItem("Pin Messages", "釘選訊息", "Text", 15),
            new PermissionItem("Manage Threads", "管理討論串", "Text", 34),
            new PermissionItem("Embed Links", "嵌入連結", "Text", 14),
            new PermissionItem("Attach Files", "附加檔案", "Text", 15),
            new PermissionItem("Read Message History", "讀取訊息歷史紀錄", "Text", 16),
            new PermissionItem("Mention Everyone", "提及所有人 (@everyone)", "Text", 17),
            new PermissionItem("Use External Emojis", "使用外部表情符號", "Text", 18),
            new PermissionItem("Use External Stickers", "使用外部貼圖", "Text", 37),
            new PermissionItem("Add Reactions", "新增反應", "Text", 6),
            new PermissionItem("Use Slash Commands", "使用斜線指令", "Text", 31),
            new PermissionItem("Use Embedded Activities", "使用嵌入式活動", "Text", 39),
            new PermissionItem("Use External Apps", "使用外部應用程式", "Text", 50),
            new PermissionItem("Create Polls", "建立投票", "Text", 49),
            new PermissionItem("Bypass Slowmode", "繞過慢速模式", "Text", 51),
            new PermissionItem("Send Voice Messages", "發送語音訊息", "Text", 46),

            // --- Voice Permissions (語音頻道權限) ---
            new PermissionItem("Connect", "連接", "Voice", 20),
            new PermissionItem("Speak", "發言", "Voice", 21),
            new PermissionItem("Video", "視訊", "Voice", 22),
            new PermissionItem("Mute Members", "將成員靜音", "Voice", 24),
            new PermissionItem("Deafen Members", "拒聽成員", "Voice", 25),
            new PermissionItem("Move Members", "移動成員", "Voice", 26),
            new PermissionItem("Use Voice Activity", "使用語音感應輸入", "Voice", 25),
            new PermissionItem("Priority Speaker", "優先發言者", "Voice", 8),
            new PermissionItem("Request To Speak", "請求發言", "Voice", 32),
            new PermissionItem("Use Soundboard", "使用音效板", "Voice", 42),
            new PermissionItem("Use External Sounds", "使用外部音效", "Voice", 45),
            new PermissionItem("Set Voice Channel Status", "設定語音頻道狀態", "Voice", 48)
        };
    }
}
