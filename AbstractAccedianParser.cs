using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest
{
    public abstract class AbstractAccedianParser
    {
        // Declare abstract getters
        public abstract string getSerial(String html);
        public abstract string getPart(String html);
        public abstract string getWL(String html);
        public abstract string getRev(String html);
        public abstract string getSpd(String html);
        public abstract string getVendor(String html);
        public abstract int getTemp(String html);
        public abstract int getVcc(String html);
        public abstract int getBias(String html);
        public abstract float getTx(String html);
        public abstract float getRx(String html);
        public abstract bool present(String html);

        // Declare abstract checkers
        public abstract bool checkVcc(String html);
        public abstract bool checkBias(String html);
        public abstract bool checkTx(String html);
        public abstract bool checkRx(String html);
        public abstract bool checkTemp(String html);
        // Common search function
        
    }
}
