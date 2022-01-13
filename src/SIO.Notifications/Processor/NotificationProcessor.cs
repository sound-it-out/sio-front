using System;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SIO.Infrastructure.Events;
using SIO.Notifications.Manager;

namespace SIO.Notifications.Processor
{
    public sealed class NotificationProcessor : INotificationProcessor
    {
        private Task _executingTask;
        private readonly INotificationManager _notificationManager;
        private readonly NotificationProcessorOptions _hubConnectionOptions;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly ILogger<NotificationProcessor> _logger;

        private HubConnection _hubConnection;

        public NotificationProcessor(IServiceScopeFactory serviceScopeFactory, 
            IOptions<NotificationProcessorOptions> options,
            ILogger<NotificationProcessor> logger)
        {
            if(options == null)
                throw new ArgumentNullException(nameof(options));
            var scope = serviceScopeFactory.CreateScope();

            _notificationManager = scope.ServiceProvider.GetRequiredService<INotificationManager>();
            _hubConnectionOptions = options.Value;
            _logger = logger;
            _accessTokenProvider = scope.ServiceProvider.GetRequiredService<IAccessTokenProvider>();
        }


        private Lazy<MethodInfo> _registerEventMethod = new Lazy<MethodInfo>(() => typeof(NotificationProcessor)
            .GetMethod(nameof(NotificationProcessor.InternalRegisterEvent), BindingFlags.Instance | BindingFlags.NonPublic));

        private void InternalRegisterEvent<TEvent>(CancellationToken cancellationToken)
            where TEvent : IEvent
        {
            _logger.LogInformation($"{nameof(NotificationProcessor)} - {typeof(TEvent).Name} being registered");
            _hubConnection.On<JObject>(typeof(TEvent).Name, (eventContext) => _notificationManager.ExecuteAsync(eventContext.ToObject<Notification<TEvent>>(), cancellationToken));
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(NotificationProcessor)}.{nameof(StartAsync)} was cancelled before execution");
                cancellationToken.ThrowIfCancellationRequested();
            }

            _logger.LogInformation($"{nameof(NotificationProcessor)} starting");

            _executingTask = ExecuteAsync();

            _logger.LogInformation($"{nameof(NotificationProcessor)} started");

            if (_executingTask.IsCompleted)
                return _executingTask;

            return Task.CompletedTask;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{nameof(NotificationProcessor)} - Starting");
            _hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl(_hubConnectionOptions.Url, options =>
                {
                    options.AccessTokenProvider = async () => {
                        var result = await _accessTokenProvider.RequestAccessToken();
                        if (result.TryGetToken(out var token))
                        {
                            return token.Value;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    };
                })
                .ConfigureLogging(o => o.SetMinimumLevel(LogLevel.Trace))
                .AddNewtonsoftJsonProtocol()
                .Build();

            foreach (var @event in _hubConnectionOptions.Events)
                _registerEventMethod.Value.MakeGenericMethod(@event).Invoke(this, new object[] { cancellationToken });

            await _hubConnection.StartAsync();

            _logger.LogInformation($"{nameof(NotificationProcessor)} - Started");
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await _hubConnection.DisposeAsync();
    }
}
