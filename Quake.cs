using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifySensitivity {
    // Quake engine base, inherited by Source engine
    // No magic numbers here, sense * yaw * dpi = deg per inch
    internal class Quake {
        // Get value from cm/360
        internal static decimal SenseFromCm(decimal cm, decimal dpi, decimal yaw) {
            decimal prod = cm * dpi * yaw;
            if (prod <= 0) {
                return 0;
            }
            return 914.4M / prod;
        }
        // Get cm/360 from values (same calculation)
        internal static decimal CmFromSense(decimal val, decimal dpi, decimal yaw) {
            decimal prod = val * dpi * yaw;
            if (prod <= 0) {
                return 0;
            }
            return 914.4M / prod;
        }
    }
}
