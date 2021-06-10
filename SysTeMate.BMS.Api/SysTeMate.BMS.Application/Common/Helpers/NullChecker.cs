using System;
using System.Collections.Generic;
using System.Text;

namespace SysTeMate.BMS.Application.Common.Helpers
{
    public static class NullChecker
    {
        public static bool IsNull<T>(T model)
        {
            return model == null ? true : false;
        }
    }
}
