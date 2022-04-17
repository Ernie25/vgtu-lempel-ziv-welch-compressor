using LZW.LZW;

namespace LZW
{
    public class CompressorRunner
    {
        private readonly ICompressor _compressor;

        public CompressorRunner(ICompressor compressor)
        {
            _compressor = compressor;
        }

        public void Run()
        {
            Console.WriteLine("1 - Compress and Decompress a string.\n2 - Compress a string.");
            char.TryParse(Console.ReadLine(), out char operation);
            Console.WriteLine("Enter a string");
            string text = Console.ReadLine() ?? string.Empty;
            Execute(operation, text);
        }

        private void Execute(char operation, string text)
        {
            switch (operation)
            {
                case '1':
                    {
                        var compressedResult = _compressor.Encode(text);
                        _compressor.PrintCompressedData(compressedResult);
                        Console.WriteLine(_compressor.Decode(compressedResult));
                        break;
                    }
                case '2':
                    {
                        var compressedResult = _compressor.Encode(text);
                        _compressor.PrintCompressedData(compressedResult);
                    }
                    break;
                default:
                    {
                        Environment.Exit(1);
                        break;
                    }
            }
        }
    }
}
