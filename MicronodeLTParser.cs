using System;

namespace AutoTest
{
    public class MicronodeLTParser
    {

        //Find serial number in the html string
        public static String getSerial(String html)
        {
            // String serial = search(html, "<TD class=\"elem\">Serial number:</TD>\n<TD class=\"content\">", "</TD>");
            String serial = search(html, "<td class=\"elem\">Serial number:</td>\n<td class=\"content\">", "</td>");
            serial = serial.TrimEnd(' ');
            return serial;
        }

        //Find part number in the html string
        public static String getPart(String html)
        {
            // String part = search(html, "<TD class=\"elem\">Part number:</TD>\n<TD class=\"content\">", "</TD>");
            String part = search(html, "<td class=\"elem\">Part number:</td>\n<td class=\"content\">", "</td>");
            part = part.TrimEnd(' ');
            return part;
        }

        //Find wavelength in the html string
        public static String getWL(String html)
        {
            // String wl = search(html, "<TD class=\"elem\">Wave Length:</TD>\n<TD class=\"content\">", "</TD>");
            String wl = search(html, "<td class=\"elem\">Wave Length:</td>\n<td class=\"content\">", "</td>");
            wl = wl.TrimEnd(' ');
            return wl;
        }

        //Find revision number in the html string
        public static String getRev(String html)
        {
            // String rev = search(html, "<TD class=\"elem\">Revision:</TD>\n<TD class=\"content\">", "</TD>");
            String rev = search(html, "<td class=\"elem\">Revision:</td>\n<td class=\"content\">", "</td>");
            rev = rev.TrimEnd(' ');
            return rev;
        }

        //Find bit rate in the html string
        public static String getSpd(String html)
        {
            // String spd = search(html, "<TD class=\"elem\">Speed:</TD>\n<TD class=\"content\">", "</TD>");
            String spd = search(html, "Detected:</span></td>\n<td class=\"content\">", "</td>");
            spd = spd.TrimEnd(' ');
            return spd;
        }

        //Find vendor name in the html string
        public static String getVendor(String html)
        {
            // String vendor = search(html, "<TD class=\"elem\">Vendor:</TD>\n<TD class=\"content\">", "</TD>");
            String vendor = search(html, "<td class=\"elem\">Vendor:</td>\n<td class=\"content\">", "</td>");
            vendor = vendor.TrimEnd(' ');
            return vendor;
        }

        //Get the integer representaion of the current part temperature from the html string
        public static int getTemp(String html)
        {
            // return int.Parse(search(html, "<TD class=\"subtitle\" COLSPAN=4>         Temperature (", " Celsius):</TD>"));
            return int.Parse(search(html, "<td class=\"subtitle\" colspan=\"4\"> Temperature (", " Celsius):</td>"));
        }

        //Get the integer representaion of the current part supply voltage from the html string
        public static int getVcc(String html)
        {
            // return int.Parse(search(html, "<TD class=\"subtitle\" COLSPAN=4>         Vcc (", " milli-volts):</TD>"));
            return int.Parse(search(html, "<td class=\"subtitle\" colspan=\"4\"> Vcc (", " milli-volts):</td>"));
        }

        //Get the integer representaion of the current part laser bias current from the html string
        public static int getBias(String html)
        {
            // return int.Parse(search(html, "<td class=\"subtitle\" COLSPAN   =4>         Laser bias current (", " micro-amps):</td>"));
            return int.Parse(search(html, "<td class=\"subtitle\" colspan=\"4\"> Laser bias current (", " micro-amps):</td>"));
        }

        //Get the float representaion of the current part transmitter power from the html string
        public static float getTx(String html)
        {
            // String tx = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Tx power (", "\n dBm):</TD>");
            String tx = search(html, "<td class=\"subtitle\" colspan=\"4\"> Tx power (", "\n dBm):</td>");
            //It is possible for the switch to report a -inf dBm. This must be handled as float.Parse cannot convert -inf to a float
            if (tx.Equals("-inf"))
            {
                return float.MaxValue;
            }
            return float.Parse(tx);
        }

        //Get the float representaion of the current part receiver power from the html string
        public static float getRx(String html)
        {
            // String rx = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Rx power (", "\n dBm):</TD>");
            String rx = search(html, "<td class=\"subtitle\" colspan=\"4\"> Rx power (", "\n dBm):</td>");
            //It is possible for the switch to report a -inf dBm. This must be handled as float.Parse cannot convert -inf to a float
            if (rx.Equals("-inf"))
            {
                return float.MaxValue;
            }
            return float.Parse(rx);
        }

        //Checks if a part was reported as present in the html string
        public static bool present(String html)
        {
            // String present = search(html, "<TD class=\"elem\">SFP present:</TD>\n<TD class=\"content\">", "</TD>");
            String present = search(html, "<td class=\"elem\">SFP present:</td>\n<td class=\"content\">", "</td>");
            if (present.Equals("yes"))
            {
                return true;
            }
            return false;
        }

        //Checks if the current temperature is within the thresholds of the part
        public static bool checkTemp(String html)
        {
            int val = getTemp(html);
            // OLD: String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Temperature (", "<TD class=\"subtitle\" COLSPAN=4>         Vcc (");
            String thresholds = search(html, "<td class=\"subtitle\" colspan=\"4\"> Temperature (", "<td class=\"subtitle\" colspan=\"4\"> Vcc (");
            //Cannot trim to only number because Celsius spelled wrong and there may be an update someday.
            //Split string with Celsius at space and take first half
            String highWarn = search(thresholds, "<td class=\"elem\">High warning:</td>\n<td class=\"content\">", "</td>");
            highWarn = highWarn.Split(' ')[0];
            String lowWarn = search(thresholds, "<td class=\"elem\">Low warning:</td>\n<td class=\"content\">", "</td>");
            lowWarn = lowWarn.Split(' ')[0];
            if (val < int.Parse(highWarn) && val > int.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        //Checks if the current supply voltage is within the thresholds of the part
        public static bool checkVcc(String html)
        {
            int val = getVcc(html);
            // OLD: String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Vcc (", "<TD class=\"subtitle\" COLSPAN=4>         Laser bias current (");
            String thresholds = search(html, "<td class=\"subtitle\" colspan=\"4\"> Vcc (", "<td class=\"subtitle\" colspan=\"4\"> Laser bias current (");
            String highWarn = search(thresholds, "<td class=\"elem\">High warning:</td>\n<td class=\"content\">", " milli-volts</td>");
            String lowWarn = search(thresholds, "<td class=\"elem\">Low warning:</td>\n<td class=\"content\">", " milli-volts</td>");
            if (val < int.Parse(highWarn) && val > int.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        //Checks if the current laser bias current is within the thresholds of the part
        public static bool checkBias(String html)
        {
            int val = getBias(html);
            String thresholds = search(html, "<td class=\"subtitle\" colspan=\"4\"> Laser bias current (", "<td class=\"subtitle\" colspan=\"4\"> Tx power (");
            String highWarn = search(thresholds, "<td class=\"elem\">High warning:</td>\n<td class=\"content\">", " micro-amps</td>");
            String lowWarn = search(thresholds, "<td class=\"elem\">Low warning:</td>\n<td class=\"content\">", " micro-amps</td>");
            if (val < int.Parse(highWarn) && val > int.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        //Checks if the current transmitter power is within the thresholds of the part
        public static bool checkTx(String html)
        {
            float val = getTx(html);
            String thresholds = search(html, "<td class=\"subtitle\" colspan=\"4\"> Tx power (", "<td class=\"subtitle\" colspan=\"4\"> Rx power (");
            String highWarn = search(thresholds, "<td class=\"elem\">High warning:</td>\n<td class=\"content\">", "\n");
            String lowWarn = search(thresholds, "<td class=\"elem\">Low warning:</td>\n<td class=\"content\">", "\n");
            if (val < float.Parse(highWarn) && val > float.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        //Checks if the current receiver power is within the thresholds of the part
        public static bool checkRx(String html)
        {
            float val = getRx(html);
            String thresholds = search(html, "<td class=\"subtitle\" colspan=\"4\"> Rx power (", "</table>");
            String highWarn = search(thresholds, "<td class=\"elem\">High warning:</td>\n<td class=\"content\">", "\n");
            String lowWarn = search(thresholds, "<td class=\"elem\">Low warning:</td>\n<td class=\"content\">", "\n");
            if (val < float.Parse(highWarn) && val > float.Parse(lowWarn))
            {
                return true;
            }
            return false;
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
