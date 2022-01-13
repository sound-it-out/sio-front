using System;
using Microsoft.Extensions.DependencyInjection;
using SIO.Notifications.Manager;
using SIO.Notifications.Manager.Subscriptions;
using SIO.Notifications.Processor;

namespace SIO.Notifications.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services, Action<NotificationProcessorOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<INotificationSubscriptions, NotificationSubscriptions>();
            services.AddScoped<INotificationManager, NotificationManager>();
            services.AddSingleton<INotificationProcessor, NotificationProcessor>();
            return services;
        }
    }
}
