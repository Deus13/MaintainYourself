using UnityEngine;


namespace Maintainyouself
{
    public class Implementation
    {
        private const string NAME = "Maintain-youself";
        public static double HPnotround;
        public static void OnLoad()
        {
            Log("Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);


            HPnotround = 0f;
        }

        
       
      

      


   


        internal static void Log(string message)
        {
            Debug.LogFormat("[" + NAME + "] {0}", message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format("[" + NAME + "] {0}", message);
            Debug.LogFormat(preformattedMessage, parameters);
        }
    }
   
}