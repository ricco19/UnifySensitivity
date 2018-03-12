using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifySensitivity {
    internal class PUBG {
        // Warning: lots of magic numbers round these parts
        // Get values from cm/360
        internal static decimal ConvFromCm(decimal cm, decimal dpi, decimal fov) {
            decimal prod = cm * dpi * fov;
            if (prod <= 0) {
                return 0;
            }
            return 32918.4M / prod;
        }
        internal static decimal SenseFromCm(decimal cm, decimal dpi, decimal fov) {
            double prod = (double)(cm * dpi * fov);
            decimal lval = (decimal)Math.Log(16459200 / prod);
            return 21.7147M * lval;
        }
        // Get cm/360 from values
        internal static decimal CmFromSense(decimal val, decimal dpi, decimal fov) {
            decimal prod = dpi * fov;
            if (prod <= 0) {
                return 0;
            }
            double m = 16459200 * Math.Pow(2.71828, -0.0460518 * (double)val);
            return (decimal)m / prod;
        }
        internal static decimal CmFromConv(decimal val, decimal dpi, decimal fov) {
            decimal prod = val * dpi * fov;
            if (prod <= 0) {
                return 0;
            }
            return 32918.4M / prod;
        }
    }
}
