namespace ChatcodeTool
{
    public class Chatcode
    {
        public string Code { get; set; }

        public Chatcode(string base64Code, int quantity)
        {
            Code = ConvertChatcode(base64Code, quantity);
        }

        public static implicit operator string(Chatcode c) => c.Code;

        private string ConvertChatcode(string str, int newValue)
        {
            return "[&" + Utility.ConvertHexToBase64(Utility.InsertHexAtPosition(Utility.ConvertBase64ToHex(str), newValue, 1)) + "]";
        }
    }
}
