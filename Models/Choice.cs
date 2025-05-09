namespace DilemmaSimWeb.Models
{
    public class Choice
    {
        public string Text { get; set; }
        public int HonestyScore { get; set; }
        public int EmpathyScore { get; set; }
        public string Consequence { get; set; }
    }
}
