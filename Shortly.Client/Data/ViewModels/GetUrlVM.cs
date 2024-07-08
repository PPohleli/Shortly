namespace Shortly.Client.Data.ViewModels
{
    public class GetUrlVM
    {
        public int Id { get; set; }
        public string OriginalLink { get; set; }
        public string ShortLink { get; set; }
        public int NrOfClicks { get; set; }
        public int? userId { get; set; }

        public GetUserVM? User { get; set; }
    }
}
