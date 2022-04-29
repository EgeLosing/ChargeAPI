using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartCharge_API
{
    public static class api
    {
        private static string exceptionMsg = "Can't get Power Status for this PC.";
        public static int getCharge()
        {
            if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Unknown)
            {
                throw new Exception(exceptionMsg);
            }
            PowerStatus p = SystemInformation.PowerStatus;
            int a = (int)(p.BatteryLifePercent * 100);
            return a;
        }

        public static bool isCharging()
        {
            if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                return true;
            }
            else if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
            {
                return false;
            }
            else if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Unknown)
            {
                throw new Exception(exceptionMsg);
            }
            return false;
        }

        public static sbyte ChargeLevel()
        {
            // 0 - Critically low
            // 1 - Low
            // 2 - Medium
            // 3 - High
            // 4 - Very high
            // 5 - Charging
            // -1 - Not defined (throws an exception)

            if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Unknown)
            {
                throw new Exception(exceptionMsg);
            }
            PowerStatus p = SystemInformation.PowerStatus;
            int a = (int)(p.BatteryLifePercent * 100);
            if (isCharging())
            {
                return 5;
            }
            else if (!isCharging())
            {
                if (a >= 80) return 4;
                else if (a < 80) return 3;
                else if (a < 50) return 2;
                else if (a < 20) return 1;
                else if (a < 10) return 0;
            }

            return -1;
        }
    }
}
