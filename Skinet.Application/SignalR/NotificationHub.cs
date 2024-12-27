using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Skinet.Application.Extensions;

namespace Skinet.Application.SignalR;

[Authorize]
public class NotificationHub : Hub
{
    public static readonly ConcurrentDictionary<string, string> UserConnnections = new();

    public override Task OnConnectedAsync()
    {
        var email = Context.User?.GetEmail();
        if (!string.IsNullOrEmpty(email))
            UserConnnections[email] = Context.ConnectionId;
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var email = Context.User?.GetEmail();
        if (!string.IsNullOrEmpty(email))
            UserConnnections.TryRemove(email, out _);
        return base.OnDisconnectedAsync(exception);
    }

    public static string? GetConnectionIdByEmail(string email)
    {
        UserConnnections.TryGetValue(email, out var ConnectionId);
        return ConnectionId;
    }
}
