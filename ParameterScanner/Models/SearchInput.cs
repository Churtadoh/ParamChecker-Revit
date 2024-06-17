using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterScanner.Models
{
    public class SearchInput
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }

        public SearchInput(string parameterName, string parameterValue)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("ParameterName cannot be null or empty");
            }

            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }   
    }
}
