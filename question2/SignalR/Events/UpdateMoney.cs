namespace SignalR.Events
{
    public class UpdateMoney : UpdateEvents
    {
        public double money { get; set; }


        public UpdateMoney(double x)
        {
            money = x;
            

        }
    }
}
