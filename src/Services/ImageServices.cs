using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class ImageServices
    {
        private Context _context;
        private string _path = "Data/Images/";
        private string[] _validExtensions = new string[3] {"png", "jpg", "jpeg"};

        public ImageServices(Context context)
        {
            _context = context;
        }
        public async Task<string?> SaveImage(IFormFile file, Type entityType)
        {
            string entityName = entityType.Name + "s";
            if (!IsValidExtension(file, _validExtensions)) return null;
            string imageName = await GenerateName() + "." + GetExtension(file);
            string directory = _path + entityName;
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (FileStream fileStream = new FileStream(directory + "/" + imageName, FileMode.Create))
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
            string path = string.Empty;

            if (File.Exists(Path.Combine(_path + "Users/", imageName)))
            {
                path = Path.Combine(_path + "Users/", imageName);
            }
            else if(File.Exists(Path.Combine(_path + "Jobs/", imageName)))
            {
                path = Path.Combine(_path + "Jobs/", imageName);
            }
            else
            {
                return null;
            }
            byte[] bytes = await File.ReadAllBytesAsync(path);
            return bytes;
        }

        public async Task<bool> UpdateAsync(IFormFile file, string name)
        {
            if (!IsValidExtension(file, _validExtensions)) return false;
            string path = string.Empty;

            if(File.Exists(_path + "Users/" + name))
            {
                path = _path + "Users/" + name;
            }
            else
            {
                path = _path + "Jobs/" + name;
            }
            using (FileStream newFile = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(newFile);
            }
            return true;
        }

        public bool Remove(string imageName, Type entityType)
        {
            string path = _path + entityType.Name + "s/" + imageName;
            try
            {
                File.Delete(path);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        private bool IsValidExtension(IFormFile file, IEnumerable<string> extensions)
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
