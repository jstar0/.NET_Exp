using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Behaviors;

namespace NotePadSharp.Contracts.Services;
public interface IMainNotificationService
{
    void ShowNotification(Notification notification);

    void SetNotificationQueue(StackedNotificationsBehavior notificationQueue);

    void ClearNotificationQueue();
}
