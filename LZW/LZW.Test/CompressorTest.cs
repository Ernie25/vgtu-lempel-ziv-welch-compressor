using Xunit;
using LZW.LZW;
using System.Collections.Generic;

namespace LZW.Test
{
    public class CompressorTest
    {
        [Fact]
        public void Test_IsDictionaryFilled_DictionaryContainsElements()
        {
            // Arrange
            short asciiLength = 256;
            Compressor Compressor = new();

            // Act
            int numberOfElements = Compressor.Dictionary.Count;

            // Assert
            Assert.NotEqual(0, numberOfElements);
            Assert.Equal(asciiLength, numberOfElements);
        }

        [Theory]
        [MemberData(nameof(EncodingData))]
        public void Test_Encode_StringSuccessfullyEncoded(string text, List<int> expectedEncoding)
        {
            // Arrange
            Compressor compressor = new();

            // Act
            var resultList = compressor.Encode(text);

            // Assert
            Assert.Equal(resultList.Count, expectedEncoding.Count);
            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.Equal(resultList[i], expectedEncoding[i]);
            }
        }

        [Theory]
        [MemberData(nameof(EncodingData))]
        public void Test_Decode_StringSuccessfullyDecoded(string decompressedString, List<int> compressedData)
        {
            // Arrange
            Compressor compressor = new();

            // Act
            string result = compressor.Decode(compressedData);

            // Assert
            Assert.Equal(result, decompressedString);
        }

        private static IEnumerable<object[]> EncodingData()
        {
            yield return new object[] {
                "test",
                new List<int>
                {
                    116,
                    101,
                    115,
                    116
                }
            };
            yield return new object[] {
                "Lorem Ipsum",
                new List<int>
                {
                    76,
                    111,
                    114,
                    101,
                    109,
                    32,
                    73,
                    112,
                    115,
                    117,
                    109
                } };
            yield return new object[] {
                "Lempel Ziv Welch",
                new List<int>
                {
                    76,
                    101,
                    109,
                    112,
                    101,
                    108,
                    32,
                    90,
                    105,
                    118,
                    32,
                    87,
                    260,
                    99,
                    104
                }
            };
            yield return new object[] {
                "Peter Piper picked a peck of pickled peppers. How many pickled peppers did Peter Piper pick?",
                new List<int>
                {
                    80,
                    101,
                    116,
                    101,
                    114,
                    32,
                    80,
                    105,
                    112,
                    259,
                    32,
                    112,
                    105,
                    99,
                    107,
                    101,
                    100,
                    32,
                    97,
                    266,
                    101,
                    269,
                    32, 
                    111,
                    102,
                    266,
                    268,
                    107,
                    108,
                    271,
                    275,
                    112,
                    264,
                    114,
                    115,
                    46,
                    32,
                    72,
                    111,
                    119,
                    32,
                    109,
                    97,
                    110,
                    121,
                    281,
                    269,
                    284,
                    272,
                    264,
                    287,
                    259,
                    115,
                    32,
                    100,
                    105,
                    272,
                    256,
                    258,
                    260,
                    262,
                    288,
                    301,
                    107,
                    63
                }
            };
        }
    }
}