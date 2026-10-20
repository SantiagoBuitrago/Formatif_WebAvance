namespace SignalR.Events
{
    public class UpdateMoney : UpdateEvents
    {
        public int money { get; set; }


        public UpdateMoney(int x)
        {
            money += x;
            

        }
    }
}
