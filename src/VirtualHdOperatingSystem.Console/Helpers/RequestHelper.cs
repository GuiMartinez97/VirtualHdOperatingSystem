using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualHdOperatingSystem.Console.Models;

namespace VirtualHdOperatingSystem.Console.Helpers
{
    public static class RequestHelper
    {
        public static Request Split(string _commandEntered)
        {
            var members = _commandEntered.Split(' ');

            var request = new Request
            {
                Root = members[0],
                Parameters = members == null ? new List<string>() : members.ToList().GetRange(1, members.Length - 1)
            };

            return request;
        }

        public static void ValidadeParametersNumber(int _validParametersNumber, List<string> _parameters)
        {
            if (_parameters.Count != _validParametersNumber)
            {
                throw new Exception("Invalid parameters number!");
            }
        }

        public static int ValidadeParametersIntFormat(string _parameter)
        {
            try
            {
                int parameterInIntFormat = Convert.ToInt32(_parameter);
                return parameterInIntFormat;
            }
            catch(Exception ex)
            {
                throw new Exception($"Invalid parameter format: {_parameter} should be a integer.");
            }
        }
    }
}