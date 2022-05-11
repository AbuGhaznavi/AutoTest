using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest
{
    class CopperParser : AbstractAccedianParser
    {

        // Move all DDM related search functions to top and set to trivial return values
        public const string UNSUPPORTED_STRING = "unsupported";
        public const float UNSUPPORTED_FLOAT = -1;
        public const int UNSUPPORTED_INT = -1;


        //Find serial number in the html string
        public override String getSerial(String html)
        {
            // String serial = search(html, "<TD class=\"elem\">Serial number:</TD>\n<TD class=\"content\">", "</TD>");
            String serial = search(html, "<TD class=\"elem\">Serial number:</TD>\n<TD class=\"content\">", "</TD>");
            serial = serial.TrimEnd(' ');
            return serial;
        }

        //Find part number in the html string
        public override String getPart(String html)
        {
            // String part = search(html, "<TD class=\"elem\">Part number:</TD>\n<TD class=\"content\">", "</TD>");
            String part = search(html, "<TD class=\"elem\">Part number:</TD>\n<TD class=\"content\">", "</TD>");
            part = part.TrimEnd(' ');
            return part;
        }

        //Find wavelength in the html string
        public override String getWL(String html)
        {

            return UNSUPPORTED_STRING;
        }

        //Find revision number in the html string
        public override String getRev(String html)
        {
            // String rev = search(html, "<TD class=\"elem\">Revision:</TD>\n<TD class=\"content\">", "</TD>");
            String rev = search(html, "<TD class=\"elem\">Revision:</TD>\n<TD class=\"content\">", "</TD>");
            rev = rev.TrimEnd(' ');
            return rev;
        }

        //Find bit rate in the html string
        public override String getSpd(String html)
        {
            // String spd = search(html, "<TD class=\"elem\">Speed:</TD>\n<TD class=\"content\">", "</TD>");
            String spd = search(html, "Detected:</SPAN></TD>\n<TD class=\"content\">", "</TD>");
            spd = spd.TrimEnd(' ');
            return spd;
        }

        //Find vendor name in the html string
        public override String getVendor(String html)
        {
            // String vendor = search(html, "<TD class=\"elem\">Vendor:</TD>\n<TD class=\"content\">", "</TD>");
            String vendor = search(html, "<TD class=\"elem\">Vendor:</TD>\n<TD class=\"content\">", "</TD>");
            vendor = vendor.TrimEnd(' ');
            return vendor;
        }

        //Get the integer representaion of the current part temperature from the html string
        public override int getTemp(String html)
        {
            return UNSUPPORTED_INT;
        }

        //Get the integer representaion of the current part supply voltage from the html string
        public override int getVcc(String html)
        {
            return UNSUPPORTED_INT;
        }

        //Get the integer representaion of the current part laser bias current from the html string
        public override int getBias(String html)
        {
            return UNSUPPORTED_INT;
        }

        //Get the float representaion of the current part transmitter power from the html string
        public override float getTx(String html)
        {
            return UNSUPPORTED_FLOAT;
        }

        //Get the float representaion of the current part receiver power from the html string
        public override float getRx(String html)
        {
            return UNSUPPORTED_FLOAT;
        }

        //Checks if a part was reported as present in the html string
        public override bool present(String html)
        {
            // String present = search(html, "<TD class=\"elem\">SFP present:</TD>\n<TD class=\"content\">", "</TD>");
            String present = search(html, "<TD class=\"elem\">SFP present:</TD>\n<TD class=\"content\">", "</TD>");
            if (present.Equals("yes"))
            {
                return true;
            }
            return false;
        }

        // Let all DDM checking functions return true trivially
        //Checks if the current temperature is within the thresholds of the part
        public override bool checkTemp(String html)
        {
            return true;
        }

        //Checks if the current supply voltage is within the thresholds of the part
        public override bool checkVcc(String html)
        {
            return true;
        }

        //Checks if the current laser bias current is within the thresholds of the part
        public override bool checkBias(String html)
        {
            return true;
        }

        //Checks if the current transmitter power is within the thresholds of the part
        public override bool checkTx(String html)
        {
            return true;
        }

        //Checks if the current receiver power is within the thresholds of the part
        public override bool checkRx(String html)
        {
            return true;
        }

        //Utility function to find two substrings in a larger string and return what is between them.
        //Ex: search("this is an example", "this ", "example") => "is an "
        //If the string is not found an empty string is returned
        public static String search(String search, String start, String end)
        {
            if (search.Contains(start) && search.Contains(end))
            {
                int i = search.IndexOf(start, 0) + start.Length;
                int j = search.IndexOf(end, i);
                return search.Substring(i, j - i);
            }
            else
            {
                return "";
            }
        }
    }
}
