namespace SignalR.Events
{
    public class UpdatePizzaPrice : UpdateEvents
    {
        public double money { get; set; }


        public UpdatePizzaPrice(double x)
        {
            money = x;


        }

       
    }
}
