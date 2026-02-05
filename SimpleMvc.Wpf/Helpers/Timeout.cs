using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Wpf.Helpers
{
    public static class Timeout
    {
        public static async Task SetTimeoutAsync(Action action, int milliseconds)
        {
            try
            {
                await Task.Delay(milliseconds);
                action();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static void SetTimeout(Action action, int milliseconds)
        {
            _ = SetTimeoutAsync(action, milliseconds);
        }
    }

}
