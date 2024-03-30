using RentVilla.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod)
        {
            string newFileName = await Task.Run(() =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.CharRegulatory(oldName)}{extension}";
                if (hasFileMethod(pathOrContainerName, newFileName))
                {
                    int i = 1;
                    while (hasFileMethod(pathOrContainerName, $"{oldName}-{i}{extension}"))
                    {
                        i++;
                    }
                    return newFileName = $"{oldName}-{i}{extension}";
                }
                else
                    return newFileName;
            });
            return newFileName;
        }
    }
}
