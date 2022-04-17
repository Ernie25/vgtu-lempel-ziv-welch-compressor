
namespace LZW.LZW
{
    public class Compressor : ICompressor
    {
        private const short Ascii_Length = 255;
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
            ResetDictionary();
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
                }
                else
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


        public string Decode(List<int> compressedString)
        {
            ResetDictionary();
            string previousSymbol = ((char)compressedString[0]).ToString();
            compressedString.RemoveAt(0);
            string decompressedString = previousSymbol;
            foreach (int item in compressedString)
            {
                string currentSymbol = string.Empty;
                if (Dictionary.ContainsValue(item))
                {
                    currentSymbol = FindKeyByValue(item);
                }
                else if (item == Dictionary.Count)
                {
                    currentSymbol = previousSymbol + previousSymbol[0];
                }
                decompressedString += currentSymbol;
                Dictionary.Add(previousSymbol + currentSymbol[0], Dictionary.Count);
                previousSymbol = currentSymbol;
            }
            return decompressedString;
        }


        public void ResetDictionary()
        {
            Dictionary.Clear();
            FillDictionary();
        }

        public void PrintCompressedData(List<int> compressedData)
        {
            foreach (int item in compressedData)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        private void FillDictionary()
        {
            for (int i = 0; i <= Ascii_Length; i++)
            {
                Dictionary.Add(((char)i).ToString(), i);
            }
        }

        private string FindKeyByValue(int value)
        {
            return Dictionary.FirstOrDefault(t => t.Value == value).Key;
        }
    }
}
