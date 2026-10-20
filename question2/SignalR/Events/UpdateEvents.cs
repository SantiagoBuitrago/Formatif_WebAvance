using SignalR.Services;
using System.Text.Json.Serialization;

namespace SignalR.Events
{
    [JsonDerivedType(typeof(UpdateNbUsers))]
    [JsonDerivedType(typeof(UpdateMoney))]
    [JsonDerivedType(typeof(UpdateNbPizzasAndMoney))]
    [JsonDerivedType(typeof(UpdatePizzaPrice))]
    public abstract class UpdateEvents
    {
        public UpdateEvents()
        {
        }

        public List<UpdateEvents>? Events { get; set; }
    }
}
