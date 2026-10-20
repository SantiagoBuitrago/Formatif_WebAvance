using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using labo.signalr.api.Services;
using labo.signalr.api.Models;
using labo.signalr.api.Controllers;

namespace SignalR.Hubs
{
    public class LeHub : Hub
    {
        private readonly UselessTasksController _hubManager;

        public LeHub(UselessTasksController hubManager) {
            _hubManager = hubManager;
        }


        public override async Task OnConnectedAsync()
        {
            base.OnConnectedAsync();
            
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnConnectedAsync();
        }
        public async Task GetAllTasks()
        {
            await _hubManager.GetAll();
        }
        public async Task AddTask(string TaskName)
        {
            await _hubManager.Add(TaskName);
        }
        public async Task CompleteTask(int id)
        {
            await _hubManager.Complete(id);
        }
    }
}
