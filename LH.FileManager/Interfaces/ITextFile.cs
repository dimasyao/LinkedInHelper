using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LH.FileManager.Interfaces
{
    public interface ITextFile
    {
        public Task Save(string dataToSave, string filePath, string fileName);

        public string? ReadDataFromFile(string filePath, string fileName);
    }
}
