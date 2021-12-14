using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest
{
    class MicronodeLTParserInstance : AbstractAccedianParser
    {
        public override String getSerial(String html)
        {
            // String serial = search(html, "<TD class=\"elem\">Serial number:</TD>\n<TD class=\"content\">", "</TD>");
            String serial = MicronodeLTParserInstance.search(html, "<TD class=\"elem\">Serial number:</TD>\n<TD class=\"content\">", "</TD>");
            serial = serial.TrimEnd(' ');
            return serial;
        }

        public override String getPart(String html)
        {
            // String part = search(html, "<TD class=\"elem\">Part number:</TD>\n<TD class=\"content\">", "</TD>");
            String part = search(html, "<TD class=\"elem\">Part number:</TD>\n<TD class=\"content\">", "</TD>");
            part = part.TrimEnd(' ');
            return part;
        }

        public override String getWL(String html)
        {
            // String wl = search(html, "<TD class=\"elem\">Wave Length:</TD>\n<TD class=\"content\">", "</TD>");
            String wl = search(html, "<TD class=\"elem\">Wave Length:</TD>\n<TD class=\"content\">", "</TD>");
            wl = wl.TrimEnd(' ');
            return wl;
        }

        public override String getRev(String html)
        {
            // String rev = search(html, "<TD class=\"elem\">Revision:</TD>\n<TD class=\"content\">", "</TD>");
            String rev = search(html, "<TD class=\"elem\">Revision:</TD>\n<TD class=\"content\">", "</TD>");
            rev = rev.TrimEnd(' ');
            return rev;
        }
        public override String getSpd(String html)
        {
            // String spd = search(html, "<TD class=\"elem\">Speed:</TD>\n<TD class=\"content\">", "</TD>");
            String spd = search(html, "Detected:</SPAN></TD>\n<TD class=\"content\">", "</TD>");
            spd = spd.TrimEnd(' ');
            return spd;
        }

        public override int getVcc(String html)
        {
            return int.Parse(search(html, "<TD class=\"subtitle\" COLSPAN=4> Vcc (", " milli-volts):</TD>"));
        }

        public override String getVendor(String html)
        {
            // String vendor = search(html, "<TD class=\"elem\">Vendor:</TD>\n<TD class=\"content\">", "</TD>");
            String vendor = search(html, "<TD class=\"elem\">Vendor:</TD>\n<TD class=\"content\">", "</TD>");
            vendor = vendor.TrimEnd(' ');
            return vendor;
        }

        public override int getTemp(String html)
        {
            return int.Parse(search(html, "<TD class=\"subtitle\" COLSPAN=4> Temperature (", " Celsius):</TD>"));
            // return int.Parse(search(html, "<td class=\"subtitle\" colspan=\"4\"> Temperature (", " Celsius):</td>"));
        }

        public override float getTx(String html)
        {
            // String tx = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Tx power (", "\n dBm):</TD>");
            String tx = search(html, "<TD class=\"subtitle\" COLSPAN=4> Tx power (", "\n dBm):</TD>");
            //It is possible for the switch to report a -inf dBm. This must be handled as float.Parse cannot convert -inf to a float
            if (tx.Equals("-inf"))
            {
                return float.MaxValue;
            }
            return float.Parse(tx);
        }

        public override int getBias(String html)
        {
            return int.Parse(search(html, "<TD class=\"subtitle\" COLSPAN=4> Laser bias current (", " micro-amps):</TD>"));
            // return int.Parse(search(html, "<td class=\"subtitle\" colspan=\"4\"> Laser bias current (", " micro-amps):</td>"));
        }

        public override float getRx(String html)
        {
            // String rx = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Rx power (", "\n dBm):</TD>");
            String rx = search(html, "<TD class=\"subtitle\" COLSPAN=4> Rx power (", "\n dBm):</TD>");
            //It is possible for the switch to report a -inf dBm. This must be handled as float.Parse cannot convert -inf to a float
            if (rx.Equals("-inf"))
            {
                return float.MaxValue;
            }
            return float.Parse(rx);
        }

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

        public override bool checkTemp(String html)
        {
            int val = getTemp(html);
            // OLD: String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Temperature (", "<TD class=\"subtitle\" COLSPAN=4>         Vcc (");
            String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4> Temperature (", "<TD class=\"subtitle\" COLSPAN=4> Vcc (");
            //Cannot trim to only number because Celsius spelled wrong and there may be an update someday.
            //Split string with Celsius at space and take first half
            String highWarn = search(thresholds, "<TD class=\"elem\">High warning:</TD>\n<TD class=\"content\">", "</TD>");
            highWarn = highWarn.Split(' ')[0];
            String lowWarn = search(thresholds, "<TD class=\"elem\">Low warning:</TD>\n<TD class=\"content\">", "</TD>");
            lowWarn = lowWarn.Split(' ')[0];
            if (val < int.Parse(highWarn) && val > int.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        public override bool checkVcc(String html)
        {
            int val = getVcc(html);
            // OLD: String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4>         Vcc (", "<TD class=\"subtitle\" COLSPAN=4>         Laser bias current (");
            String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4> Vcc (", "<TD class=\"subtitle\" COLSPAN=4> Laser bias current (");
            String highWarn = search(thresholds, "<TD class=\"elem\">High warning:</TD>\n<TD class=\"content\">", " milli-volts</TD>");
            String lowWarn = search(thresholds, "<TD class=\"elem\">Low warning:</TD>\n<TD class=\"content\">", " milli-volts</TD>");
            if (val < int.Parse(highWarn) && val > int.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        public override bool checkBias(String html)
        {
            int val = getBias(html);
            String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4> Laser bias current (", "<TD class=\"subtitle\" COLSPAN=4> Tx power (");
            String highWarn = search(thresholds, "<TD class=\"elem\">High warning:</TD>\n<TD class=\"content\">", " micro-amps</TD>");
            String lowWarn = search(thresholds, "<TD class=\"elem\">Low warning:</TD>\n<TD class=\"content\">", " micro-amps</TD>");
            if (val < int.Parse(highWarn) && val > int.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        public override bool checkTx(String html)
        {
            float val = getTx(html);
            String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4> Tx power (", "<TD class=\"subtitle\" COLSPAN=4> Rx power (");
            String highWarn = search(thresholds, "<TD class=\"elem\">High warning:</TD>\n<TD class=\"content\">", "\n");
            String lowWarn = search(thresholds, "<TD class=\"elem\">Low warning:</TD>\n<TD class=\"content\">", "\n");
            if (val < float.Parse(highWarn) && val > float.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

        public override bool checkRx(String html)
        {
            float val = getRx(html);
            String thresholds = search(html, "<TD class=\"subtitle\" COLSPAN=4> Rx power (", "</TABLE>");
            String highWarn = search(thresholds, "<TD class=\"elem\">High warning:</TD>\n<TD class=\"content\">", "\n");
            String lowWarn = search(thresholds, "<TD class=\"elem\">Low warning:</TD>\n<TD class=\"content\">", "\n");
            if (val < float.Parse(highWarn) && val > float.Parse(lowWarn))
            {
                return true;
            }
            return false;
        }

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
