using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class ImageServices
    {
        private Context _context;
        private string _path = "Data/Images/";

        public ImageServices(Context context)
        {
            _context = context;
        }
        public async Task<string> SaveImage(IFormFile file)
        {
            string imageName = await GenerateName() + "." + GetExtension(file);
            using (FileStream fileStream = new FileStream(Path.Combine(_path, imageName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            await _context.Images.AddAsync(new Image { Name = imageName });
            await _context.SaveChangesAsync();
            return imageName;
        }

        public async Task<bool> FindByNameAsync(string name)
        {
            return await _context.Images.AnyAsync(x => x.Name == name);
        }

        public async Task<byte[]?> FindFileByNameAsync(string imageName)
        {
            if (!File.Exists(Path.Combine(_path, imageName))) return null;
            byte[] bytes =  await File.ReadAllBytesAsync(Path.Combine(_path, imageName));
            return bytes;
        }

        public bool IsValidExtension(IFormFile file, IEnumerable<string> extensions)
        {
            string fileExtension = GetExtension(file);
            foreach (string ext in extensions)
            {
                if(fileExtension == ext)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<string> GenerateName()
        {
            Guid guid = Guid.NewGuid();
            while(await FindByNameAsync(guid.ToString()))
            {
                guid = Guid.NewGuid();
            }
            return guid.ToString();
        }

        private string GetExtension(IFormFile file)
        {
            List<char> result = new List<char>();
            string ext = string.Empty;

            for(int i = file.ContentType.Length - 1;  i >= 0; i--)
            {
                if (file.ContentType[i] == '/') break;
                result.Add(file.ContentType[i]);
            }

            for(int i = 0; i < Math.Floor(result.Count / 2.0); i++)
            {
                char temp = result[i];
                result[i] = result[result.Count - 1 - i];
                result[result.Count - 1 - i] = temp;
            }

            foreach(char c in result)
            {
                ext += c;
            }

            return ext;
        }
    }
}
