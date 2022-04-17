using LZW;
using LZW.LZW;

Compressor compressor = new();
CompressorRunner compressorRunner = new(compressor);

compressorRunner.Run();