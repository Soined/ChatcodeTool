using System;

namespace ChatcodeTool
{
    public static class Utility
    {
        /// <summary>
        /// Return null if conversion failed
        /// </summary>
        /// <param name="base64Code"></param>
        /// <returns></returns>
        public static string ConvertBase64ToHex(string base64Code)
        {
            try
            {
                //Diese Zeichen können im Chatcode vorkommen, sind aber bei der Konvertierung für uns nicht relevant.
                base64Code = base64Code.Trim(new char[] { '[', ']', '&', ' ', });
                return BitConverter.ToString(Convert.FromBase64String(base64Code));
            }
            catch
            {
                return null;
            }
        }
        public static string InsertHexAtPosition(string fullHexCode, int insertNumber, int index)
        {
            string[] hexValuesSplit = fullHexCode.Split('-');
            //We need to make sure it will be 2 digits long
            hexValuesSplit[index] = insertNumber.ToString("X2");
            return string.Join("-", hexValuesSplit);
        }
        public static string ConvertHexToBase64(string inputHex)
        {
            //Zuerst entfernen wir die Leerstellen aus dem Hexcode, die wir beim zusammensetzen zur Trennung genutzt haben.
            inputHex = inputHex.Replace("-", "");

            byte[] bytes = new byte[inputHex.Length / 2];
            for (int i = 0; i < inputHex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(inputHex.Substring(i, 2), 16);
            }

            //Und zuletzt wieder zu base64
            return Convert.ToBase64String(bytes);
        }
    }
}
