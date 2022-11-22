using System.Globalization;

namespace search_txt_file.Models
{
    public class VariableValue
    {
        public int ChannelId { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        private NumberFormatInfo _formatInfo;

        public VariableValue(string line, NumberFormatInfo numberFormatInfo)
        {
            _formatInfo = numberFormatInfo;
            parseValue(line);
        }

        private void parseValue(string data)
        {
            string[] lineSplitted = data.Split(';');
            string[] firstElementSplitted = lineSplitted[0].Split(':');

            ChannelId = int.Parse(firstElementSplitted[0].Substring(1));
            Value = Convert.ToDouble(firstElementSplitted[1], _formatInfo);
            Date = Convert.ToDateTime(lineSplitted[2]);

        }
    }
}
