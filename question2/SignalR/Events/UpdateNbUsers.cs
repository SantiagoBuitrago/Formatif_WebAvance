namespace SignalR.Events
{
    public class UpdateNbUsers : UpdateEvents
    {
        public int nbUsers { get; set; }
        

        public UpdateNbUsers(int x)
        {
            nbUsers = x;
    
        }
    }
}
