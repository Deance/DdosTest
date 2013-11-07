using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace DdosTester
{
    abstract class Check
    {
        public static bool isIP(string CheckString) 
        {
            try
            {
                IPAddress.Parse(CheckString);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool isURL(string CheckString)
        {
            try
            {
                Uri check = new Uri(CheckString);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
