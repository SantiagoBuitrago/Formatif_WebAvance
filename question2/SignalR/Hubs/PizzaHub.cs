using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Events;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _pizzaManager.AddUser();


            var update = new UpdateNbUsers(
                _pizzaManager.NbConnectedUsers
            );

            await Clients.All.SendAsync("UpdateNbUsers", update);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _pizzaManager.RemoveUser();

            await Clients.All.SendAsync(
                "UpdateNbUsers",
                new UpdateNbUsers(_pizzaManager.NbConnectedUsers)
            );

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<List<UpdateEvents>> SelectChoice(PizzaChoice choice)
        {
          
            List<UpdateEvents> events = new List<UpdateEvents> {
                    new UpdatePizzaPrice(_pizzaManager.GetPizzaPrice(choice)),
                    new UpdateNbPizzasAndMoney(_pizzaManager.GetMoney(choice), _pizzaManager.GetNbPizzas(choice))
             };
            await Groups.AddToGroupAsync(
              Context.ConnectionId,
              _pizzaManager.GetGroupName(choice)
                );

            return events;
        }

        public async Task<List<UpdateEvents>> UnselectChoice(PizzaChoice choice)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                _pizzaManager.GetGroupName(choice)
            );

            List<UpdateEvents> events = new List<UpdateEvents>
    {
        new UpdatePizzaPrice(_pizzaManager.GetPizzaPrice(choice)),
        new UpdateNbPizzasAndMoney(
            _pizzaManager.GetMoney(choice),
            _pizzaManager.GetNbPizzas(choice)
        )
    };

            return events;
        }

        public async Task<UpdateEvents> AddMoney(PizzaChoice choice)
        {
            _pizzaManager.IncreaseMoney(choice);

            var update = new UpdateNbPizzasAndMoney(
                _pizzaManager.GetMoney(choice),
                _pizzaManager.GetNbPizzas(choice)
            );

            await Clients.Group(
                _pizzaManager.GetGroupName(choice)
            ).SendAsync("UpdateNbPizzasAndMoney", update);

            return update;


        }

        public async Task<UpdateNbPizzasAndMoney> BuyPizza(PizzaChoice choice)
        {
            _pizzaManager.BuyPizza(choice);

            var update = new UpdateNbPizzasAndMoney(
                _pizzaManager.GetMoney(choice),
                _pizzaManager.GetNbPizzas(choice)
            );

            await Clients.Group(
                _pizzaManager.GetGroupName(choice)
            ).SendAsync("UpdateNbPizzasAndMoney", update);

            return update;
        }
    }
}
