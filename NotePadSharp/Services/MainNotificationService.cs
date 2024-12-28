using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Behaviors;
using NotePadSharp.Contracts.Services;

namespace NotePadSharp.Services;
public class MainNotificationService: IMainNotificationService
{
    private StackedNotificationsBehavior _notificationQueue;

    public void SetNotificationQueue(StackedNotificationsBehavior notificationQueue)
    {
        _notificationQueue = notificationQueue;
    }

    public void ClearNotificationQueue()
    {
        _notificationQueue.Clear();
    }

    public void ShowNotification(Notification notification)
    {
        _notificationQueue?.Show(notification);
    }
}
