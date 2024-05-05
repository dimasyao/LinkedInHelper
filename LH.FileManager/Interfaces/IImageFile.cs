using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LH.FileManager.Interfaces
{
    public interface IImageFile
    {
        public Task DownloadAndSave(string imageUrl);
    }
}
