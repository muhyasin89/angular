using MudBlazor;

namespace ParkerRandalWeb.Models
{
    public class Appointment
    {
        public int id;
        public DateTime Start;
        public DateTime End;

        public string Text = String.Empty;
    }
}
