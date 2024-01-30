namespace LMSWebAPI.Utility
{
    public class RandomNumber
    {
        public static readonly Random Number = new Random();
        public static int GetRandomNumber()
        { 
            return Number.Next();
        }
        public static string GetTimeElapsedString(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60)
            {
                return "Since Now";
            }
            else if (timeSpan.TotalMinutes < 60)
            {
                return $"{(int)timeSpan.TotalMinutes} minute ago";
            }
            else if (timeSpan.TotalHours < 24)
            {
                return $"{(int)timeSpan.TotalHours} hour ago";
            }
            else if (timeSpan.TotalDays < 30)
            {
                return $"{(int)timeSpan.TotalDays} day ago";
            }
            else if (timeSpan.TotalDays < 365)
            {
                int months = (int)(timeSpan.TotalDays / 30);
                return $"{months} month{(months != 1 ? "s" : "")} ago";
            }
            else
            {
                int years = (int)(timeSpan.TotalDays / 365);
                return $"{years} year{(years != 1 ? "s" : "")} ago";
            }
        }
    }
}
