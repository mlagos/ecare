using System;
using System.Web;

namespace Nextgal.ECare.Common.Util
{
    /// <summary>
    /// Summary description for Headers.
    /// </summary>
    public static class FixMap
    {
        private static String[] navigators = {"Chrome", "Safari"};
        
        static FixMap()
        {
         
        }

        public static string FixHtml(HttpContext context, bool isPostback, string html)
        {
            string result = html;
            foreach (var navigator in navigators)
            {
                if(isPostback && context.Request.UserAgent.Contains(navigator))
                {
                    result = html.Replace("<", "&lt;").Replace(">", "&gt;");
                    break;
                }
            }
            return result;    
        }
    }
}