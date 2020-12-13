using System;
using System.Collections.Generic;
using System.Text;

namespace ManageStores.Core.Helpers
{
    public class StringHelper
    {
        public static string ShortenText(string text, int maxLength)
        {
            if (text.Length > maxLength)
                return text.Substring(0, maxLength);
            return text;
        }
    }
}
