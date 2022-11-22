using search_txt_file.Models;
using System.Globalization;

namespace search_txt_file.Handlers
{
    public class TxtFileSearcher
    {
        public string FilePath { get; }
        public string ResultFilePath { get; }

        private string _variables;

        private string _values;

        public IDictionary<int, SearchOutput> SearchResult { get; private set; }

        public NumberFormatInfo _formatInfo { get; private set; }

        public TxtFileSearcher(string inputFilePath, string resultFilePath)
        {
            FilePath = inputFilePath;
            ResultFilePath = resultFilePath;
            _values = "-VALUES-";
            _variables = "-VARIABLES-";
            _formatInfo = getNumberFormatInfo();    
        }

        public bool findMinAndMaxValueForEachVarible()
        {
            try
            {
                SearchResult = new Dictionary<int, SearchOutput>();

                using (StreamReader reader = new StreamReader(FilePath))
                {
                    bool variablesFlag = false;
                    bool valuesFlag = false;

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine().Trim();

                        if (line == _variables)
                        {
                            variablesFlag = true;
                            continue;
                        }

                        if (line == _values)
                        {
                            variablesFlag = false;
                            valuesFlag = true;
                            continue;
                        }

                        if (variablesFlag)
                        {
                            string[] lineSplitted = line.Split('=');
                            SearchResult.Add(int.Parse(lineSplitted[0].Substring(1)), new SearchOutput(lineSplitted[1]));
                        }

                        if (valuesFlag)
                        {
                            string[] lineSplitted = line.Split(';');
                            string firstElement = lineSplitted[0];
                            int channelCode = int.Parse(firstElement.Trim().Split(':')[0].Substring(1));
                            string variableValue = firstElement.Trim().Split(':')[1];

                            double currentValue = Convert.ToDouble(variableValue, _formatInfo);

                            SearchOutput searchOutput = SearchResult[channelCode];

                            if (currentValue < searchOutput.MinValue)
                            {
                                searchOutput.MinValue = currentValue;
                                searchOutput.MinValueDateTime = Convert.ToDateTime(lineSplitted[2].Trim());
                            }

                            if (currentValue > searchOutput.MaxValue)
                            {
                                searchOutput.MaxValue = currentValue;
                                searchOutput.MaxValueDateTime = Convert.ToDateTime(lineSplitted[2].Trim());
                            }
                        }
                    }
                }

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception with message:" + e.Message);
                return false;
            }
        }

        public bool findMinAndMaxValue()
        {
            try
            {
                SearchResult = new Dictionary<int, SearchOutput>();

                using (StreamReader reader = new StreamReader(FilePath))
                {
                    bool variablesFlag = false;
                    bool valuesFlag = false;
                    List<VariableValue> allValues = new List<VariableValue>();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine().Trim();

                        if (line == _variables)
                        {
                            variablesFlag = true;
                            continue;
                        }

                        if (line == _values)
                        {
                            variablesFlag = false;
                            valuesFlag = true;
                            continue;
                        }

                        if (variablesFlag)
                        {
                            string[] lineSplitted = line.Split('=');
                            SearchResult.Add(int.Parse(lineSplitted[0].Substring(1)), new SearchOutput(lineSplitted[1]));
                        }

                        if (valuesFlag)
                        {
                            allValues.Add(new VariableValue(line, _formatInfo));
                        }
                    }

                    var orderedAllValues = allValues.OrderBy(v => v.ChannelId).ThenBy(v => v.Value);

                    foreach(var item in SearchResult)
                    {
                        var minElement = orderedAllValues.First(p => p.ChannelId == item.Key);
                        item.Value.MinValue = minElement.Value;
                        item.Value.MinValueDateTime = minElement.Date;

                        var maxElement = orderedAllValues.Last(p => p.ChannelId == item.Key);
                        item.Value.MaxValue = maxElement.Value;
                        item.Value.MaxValueDateTime = maxElement.Date;

                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception with message:" + e.Message);
                return false;
            }
        }


        public void writeResultToFile()
        {
            using (StreamWriter writer = new StreamWriter(ResultFilePath))
            {
                foreach (var item in SearchResult.Values)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        private NumberFormatInfo getNumberFormatInfo() {
            NumberFormatInfo numberFormat = new NumberFormatInfo();
            numberFormat.NumberDecimalSeparator = ".";
            numberFormat.NumberDecimalDigits = 6;
            return numberFormat;
        }
    }
}
