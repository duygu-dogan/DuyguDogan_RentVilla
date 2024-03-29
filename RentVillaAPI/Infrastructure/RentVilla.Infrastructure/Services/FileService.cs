using RentVilla.Infrastructure.Operations;

namespace RentVilla.Infrastructure.Services
{
    public class FileService
    {
        private async Task<string> FileRenameAsync(string path, string fileName)
        {
            string newFileName = await Task.Run( async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = $"{NameOperation.CharRegulatory(oldName)}{extension}";
                if (File.Exists($"{path}\\{newFileName}"))
                {
                    int i = 1;
                    while (File.Exists($"{path}\\{oldName}-{i}{extension}"))
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
