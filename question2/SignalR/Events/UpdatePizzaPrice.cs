namespace SignalR.Events
{
    public class UpdatePizzaPrice : UpdateEvents
    {
        public double price { get; set; }


        public UpdatePizzaPrice(double x)
        {
            price = x;


        }

       
    }
}
