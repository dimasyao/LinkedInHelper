using LH.FileManager.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LH.FileManager
{
    public class TextFile : ITextFile
    {
        private readonly ILogger<TextFile> _logger;

        public TextFile(ILogger<TextFile> logger)
        {
            _logger = logger;
        }

        public Task Save(string dataToSave, string filePath, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(filePath + fileName))
            {
                sw.Write(dataToSave);
            }

            _logger.LogInformation($"Data saved to file successfully -> {fileName}");

            return Task.CompletedTask;
        }

        public string? ReadDataFromFile(string filePath, string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath + fileName))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while reading the file ({fileName})" + ex.Message);

                return null;
            }
        }
    }
}
