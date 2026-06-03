using System.Text.RegularExpressions;
using UnityEngine;
using static NotificationManager;

namespace RadTools.Libraries.RadiumWrapper;

public static class Notifications
{
    public enum NotificationType
    {
        Min = -1,
        Minor,
        Medium,
        Major,
        Vital,
        Max
    }

    public static void Notify(NotificationType notificationType, string text, Color? color = null, float duration = 2f)
    {
        Logger.Log($"Notification: {text} (Type: {notificationType}, Color: {color}, Duration: {duration})");
        NotificationManager.Instance.Play((IBIDFAOPIPC)notificationType, text, color ?? Color.white, duration);
    }

    public static void Notify(string text, Color? color = null, float duration = 2f)
    {
        var notags = new Regex("<.*?>", RegexOptions.IgnoreCase);
        text = notags.Replace(text, string.Empty);

        Notify(NotificationType.Minor, text, color, duration);
    }
}