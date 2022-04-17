
namespace LZW.LZW
{
    public class Compressor
    {
        private const short  Ascii_Length = 255;
        public Dictionary<string, int> Dictionary { get; private set; }
        public List<int> CompressedCode { get; private set; }

        public Compressor()
        {
            Dictionary = new Dictionary<string, int>();
            CompressedCode = new List<int>();
            FillDictionary();
        }

        public List<int> Encode(string text)
        {
            string past = text[0].ToString(), current = string.Empty;
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (i != text.Length - 1)
                {
                    current += text[i + 1];
                }
                if (Dictionary.ContainsKey(past + current))
                {
                    past += current;
                } else
                {
                    CompressedCode.Add(Dictionary[past]);
                    int lastIndex = Dictionary.Last().Value;
                    Dictionary.Add(past + current, lastIndex + 1);
                    past = current;
                }
                current = string.Empty;
            }
            CompressedCode.Add(Dictionary[past]);
            return CompressedCode;
        }

        private void FillDictionary()
        {
            for(int i = 0; i <= Ascii_Length; i++)
            {
                Dictionary.Add(((char)i).ToString(), i);
            }
        }
    }
}
