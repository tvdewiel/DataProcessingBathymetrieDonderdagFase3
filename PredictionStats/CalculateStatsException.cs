namespace PredictionStats
{
    public class CalculateStatsException : Exception
    {
        public CalculateStatsException(string? message) : base(message)
        {
        }

        public CalculateStatsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}