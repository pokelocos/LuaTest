using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA // Utilities ???
{
    public static class Commons
    {
        /// <summary>
        /// Get color from string whit "#f0f0f0f0" -> #RGBA format
        /// </summary>
        /// <returns></returns>
        public static Color StrToColor(string s)
        {
            Color c; 
            if (ColorUtility.TryParseHtmlString(s, out c))
            {
                return c;
            }
            return Color.white;
        }

        public static string ColorTosStr(Color c)
        {
            return ColorUtility.ToHtmlStringRGB(c);
        }

    }
}


