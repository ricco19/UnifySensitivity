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
            // 20/9 = 2.22... = 101.0101 * 0.022
            decimal approx_yaw = (20.0M / 9.0M) * (fov / 80.0M);
            decimal prod = cm * dpi * approx_yaw;
            if (prod <= 0) {
                return 0;
            }
            return 914.4M / prod;
        }
        internal static decimal SenseFromCm(decimal cm, decimal dpi, decimal fov) {
            // PUBG sense is a logarithmic representation of the 'real value'
            // 0 = 0.002, 100 = 0.2
            // 100 * ((log10(c) - log10(0.002)) / (log10(0.2) - log10(0.002)))
            // = 100 * ((log10(c) - log10(0.002)) / 2)
            double conv = (double)ConvFromCm(cm, dpi, fov);
            if (conv <= 0) {
                return 0;
            }
            double rval = 100 * ((Math.Log10(conv) - Math.Log10(0.002)) / 2);
            return (decimal)rval;
        }
        // Get cm/360 from values
        internal static decimal CmFromSense(decimal val, decimal dpi, decimal fov) {
            // Just convert to real value first
            double m = ((double)val / 50.0) + Math.Log10(0.002);
            decimal prod = (decimal)Math.Pow(10, m);
            return CmFromConv(prod, dpi, fov);
        }
        internal static decimal CmFromConv(decimal val, decimal dpi, decimal fov) {
            // 20/9 = 2.22... = 101.0101 * 0.022
            decimal approx_yaw = (20.0M / 9.0M) * (fov / 80.0M);
            decimal prod = val * dpi * approx_yaw;
            if (prod <= 0) {
                return 0;
            }
            return 914.4M / prod;
        }
    }
}
