namespace LZW.LZW
{
    public interface ICompressor
    {
        List<int> CompressedCode { get; }
        Dictionary<string, int> Dictionary { get; }

        string Decode(List<int> compressedString);
        List<int> Encode(string text);
        void PrintCompressedData(List<int> compressedData);
    }
}