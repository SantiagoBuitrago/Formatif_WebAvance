namespace SignalR.Events
{
    public class UpdateNbPizzasAndMoney : UpdateEvents
    {
        public int? nbPizzas { get; set; }
        public double? money { get; set; }


        public UpdateNbPizzasAndMoney(double x, int y)
        {
        
            money = x;
            nbPizzas = y;
        }

        
    }
}
