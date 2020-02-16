using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatcodeTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FailedText.Visibility = Visibility.Hidden;
            BadQuantityText.Visibility = Visibility.Hidden;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string fullHexCode = ConvertBase64ToHex(InputBox.Text);

            //Falls wir den string nicht convertieren konnten
            if (fullHexCode == null) return;

            //Wir stellen sicher, dass der User eine für uns benutzbare Zahl eingegeben hat
            if (!IsGoodNumber(QuantityText.Text))
            {
                BadQuantityText.Visibility = Visibility.Visible;
                return; //Falls er das nicht hat, führen wir den folgenden Code nicht weiter aus
            }

            BadQuantityText.Visibility = Visibility.Hidden;

            //Kein TryParse, da wir oben bereits gecheckt haben, ob wir die vom User angegebene Anzahl erfüllen können.
            int desiredQuantity = int.Parse(QuantityText.Text);

            //Wir wissen, dass die zweite Hexcodestelle die für die Häufigkeit des Items ist, weswegen wir den Index hard coden
            string finalHex = InsertHexAtPosition(fullHexCode, desiredQuantity, 1);

            string finalBase64String = ConvertHexToBase64(finalHex);

            //Und zu allerletzt betten wir den string in die für Guildwars2 nötige Umgebung ein
            string finalChatCode = "[&" + finalBase64String + "]";

            OutputText.Text = finalChatCode;
        }
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(OutputText.Text);
        }

        private bool IsGoodNumber(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            int number = int.Parse(str);
            //Um sicher zu gehen, dass wir die Zahl auch konvertieren können und sie nicht 0 ist,
            //Da ein Item 0 mal zu besitzen keinen Sinn machen würde
            if (number > 0 && number < 256) return true;

            return false;
        }
        private string ConvertBase64ToHex(string base64Code)
        {
            try
            {
                //Diese Zeichen können im Chatcode vorkommen, sind aber bei der Konvertierung für uns nicht relevant.
                base64Code = base64Code.Trim(new char[] { '[', ']', '&', ' ',});
                byte[] bytes = Convert.FromBase64String(base64Code);
                string hexCode = BitConverter.ToString(bytes);
                FailedText.Visibility = Visibility.Hidden;
                return hexCode;

            }
            catch
            {
                FailedText.Visibility = Visibility.Visible;
                return null;
            }
        }
        private string ConvertHexToBase64(string inputHex)
        {
            //Zuerst entfernen wir die Leerstellen aus dem Hexcode, die wir beim zusammensetzen zur Trennung genutzt haben.
            inputHex = inputHex.Replace(" ", "");

            //Danach müssen wir den HexString zurück zu bytes convertieren
            int NumberChars = inputHex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for(int i=0; i < NumberChars;i+= 2)
            {
                bytes[i / 2] = Convert.ToByte(inputHex.Substring(i, 2), 16);
            }

            //Und zuletzt wieder zu base64
            return Convert.ToBase64String(bytes);
        }
        private string InsertHexAtPosition(string fullHexCode, int insertNumber, int index)
        {
            string[] hexValuesSplit = fullHexCode.Split('-');
            //We need to make sure it will be 2 digits long
            hexValuesSplit[index] = insertNumber.ToString("X2");
            string fullFinalHex = string.Join(" ", hexValuesSplit);
            return fullFinalHex;
        }
    }
}
