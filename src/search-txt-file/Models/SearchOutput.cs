namespace search_txt_file.Models
{
    public class SearchOutput
    {
        public string Channel { get; }
        public double MinValue { get; set; } = double.MaxValue;
        public DateTime MinValueDateTime { get; set; }
        public double MaxValue { get; set; } = double.MinValue;
        public DateTime MaxValueDateTime { get; set; }

        public SearchOutput(string channel)
        {
            Channel = channel;
        }


        public string ToString()
        {
            return $"{Channel};{MinValue};{MinValueDateTime};{MaxValue};{MaxValueDateTime};";
        }
    }
}
