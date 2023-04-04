using System;
using System.Web.UI;

namespace SkateFactory2.WebUI
{
    public static class StaticMethods
    {
        public static void DisplayMessage(string message, Page myPage)
        {
            message = message.Replace("'", "");
            Type myType = myPage.GetType();
            myPage.ClientScript.RegisterClientScriptBlock(myType, "ScriptFromBackend", $"<script>alert('{message}')</script>");
        }
    }
}