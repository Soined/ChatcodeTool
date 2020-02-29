using DevExpress.Mvvm.DataAnnotations;
using System;

namespace ChatcodeTool.ViewModels
{
    [POCOViewModel]
    public class MainWindowVM
    {
        public virtual string CurrentChatcode { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = nameof(CheckIfCanGenerate))]
        public virtual string InputChatcode { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = nameof(CheckIfCanGenerate))]
        public virtual string InputQuantity { get; set; }
        public virtual bool GeneratingAvailable { get; set; }

        public MainWindowVM()
        {
            // :)
        }

        public void GetNewChatcode()
        {
            //No need to check for quantity being Parseable here because it is done before the button is enabled
            CurrentChatcode = new Chatcode(InputChatcode, int.Parse(InputQuantity));
        }
        public void CheckIfCanGenerate()
        {
            GeneratingAvailable = false;
            //Checking for Quantity
            if(InputQuantity is string s && int.TryParse(s, out int res) && res > 0 && res < 256 && InputChatcode != string.Empty)
            {
                try
                {
                    //Checking for inputCode
                    string base64Code = InputChatcode.Trim(new char[] { '[', '&', ']', ' ' });
                    if (base64Code.Length != 8) return;
                    Convert.FromBase64String(base64Code);
                    GeneratingAvailable = true;
                } catch
                {
                }
            }
        }
    }
}
