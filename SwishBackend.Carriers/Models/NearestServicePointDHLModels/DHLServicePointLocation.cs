namespace SwishBackend.Carriers.Models.NearestServicePointDHLModels
{
    public class DHLServicePointLocation
    {
        public string url { get; set; }
        public DHLServicePoint location { get; set; }
        public string name { get; set; }
        public int distance { get; set; }
        public DHLPlaceDTO place { get; set; }
        public List<DHLOpeningHours> openingHours { get; set; }
        // Add other properties as needed

        // Additional property for user-friendly DayOfWeek
        public List<string> DaysOfWeek
        {
            get
            {
                return openingHours?.Select(oh => ConvertDayOfWeek(oh.dayOfWeek)).ToList();
            }
        }

        private string ConvertDayOfWeek(string url)
        {
            switch (url)
            {
                case "http://schema.org/Monday":
                    return "Monday";
                case "http://schema.org/Tuesday":
                    return "Tuesday";
                case "http://schema.org/Wednesday":
                    return "Wednesday";
                case "http://schema.org/Thursday":
                    return "Thursday";
                case "http://schema.org/Friday":
                    return "Friday";
                case "http://schema.org/Saturday":
                    return "Saturday";
                case "http://schema.org/Sunday":
                    return "Sunday";
                default:
                    return url;
            }
        }
    }
}
