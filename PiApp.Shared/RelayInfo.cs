namespace PiApp.Shared
{
    public class RelayInfo
    {
        public RelayInfo()
        {
        }

        public RelayInfo(int id, bool state)
        {
            Id = id;
            State = state;
        }

        public int Id { get; set; }
        public bool State { get; set; }
    }
}
