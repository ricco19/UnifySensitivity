using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifySensitivity {
    // Overwatch has a yaw of 0.0066 which is unchangeable
    // yaw * sense * dpi = deg per in
    // 914.4 / 0.0066 = 138544
    internal class Overwatch {
        // Get value from cm/360
        internal static decimal SenseFromCm(decimal cm, decimal dpi) {
            decimal prod = cm * dpi * 0.0066M;
            if (prod <= 0) {
                return 0;
            }
            return 914.4M / prod;
        }
        // Get cm/360 from values (same equation)
        internal static decimal CmFromSense(decimal val, decimal dpi) {
            decimal prod = val * dpi * 0.0066M;
            if (prod <= 0) {
                return 0;
            }
            return 914.4M / prod;
        }
    }
}
