using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ToDoApp.Data;
using ToDoApp.DTOs.UserDTOs;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class UserServices : GenericServices
    {
        private readonly ImageServices _imageServices;

        private readonly Dictionary<char, int> words = new Dictionary<char, int>()
{
        {'a', 0}, {'b', 1}, {'c', 2}, {'d', 3}, {'e', 4}, {'f', 5}, {'g', 6}, {'h', 7},{'i', 8},
        {'j', 9}, {'k', 10}, {'l', 11}, {'m', 12}, {'n', 13}, {'o', 14}, {'p', 15}, {'q', 16},
        {'r', 17}, {'s', 18}, {'t', 19}, {'u', 20}, {'v', 21}, {'w', 22}, {'x', 23}, {'y', 24}, {'z', 25}
        };

        public UserServices(Context context, IConfiguration configuration, ImageServices imageServices) : base(context, configuration) 
        {
            _imageServices = imageServices;
        }
        
        public async Task<User?> FindByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Password = EncryptPassword(user.Password);
            EntityEntry<User> result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserNameAsync(UpdateUserNameDTO update)
        {
            User? user = await FindByIdAsync(update.Id);
            if(user == null) return false;
            _context.Entry(user).Property(p => p.Name).IsModified = true;
            user.Name = update.Name;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserPasswordAsync(UpdateUserPasswordDTO update)
        {
            User? user = await FindByIdAsync(update.Id);
            if (user == null) return false;
            _context.Entry(user).Property(p => p.Password).IsModified = true;
            user.Password = EncryptPassword(update.NewPassword);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserBirthDateAsync(UpdateUserBirthDateDTO update)
        {
            User? user = await FindByIdAsync(update.Id);
            if (user == null) return false;
            _context.Entry(user).Property(p => p.BirthDate).IsModified = true;
            user.BirthDate = update.BirthDate;
            return await _context.SaveChangesAsync() > 0;
        }

        public bool CkeckPassword(User user,  string password)
        {
            if (DecryptPassword(user.Password) == password) return true;
            return false;
        }

        private string EncryptPassword(string password)
        {
            int counter = 0;
            string result = string.Empty;
            string key = _configuration.GetValue<string>("SecretKey")!;

            for (int i = 0; i < password.Length; i++)
            {
                if (!words.ContainsKey(password[i]))
                {
                    result += password[i];
                    continue;
                }

                int sum = 0;
                sum = words[password[i]] + words[key[counter]] > 25 ? words[password[i]] + words[key[counter]] - 26 : words[password[i]] + words[key[counter]];
                
                foreach (KeyValuePair<char, int> pair in words)
                {
                    if (pair.Value == sum)
                    {
                        result += pair.Key;
                    }
                }
                if (++counter >= key.Length)
                {
                    counter = 0;
                }
            }
            return result;
        }

        private string DecryptPassword(string encryptedPassword)
        {
            int counter = 0;
            string result = string.Empty;
            string key = _configuration.GetValue<string>("SecretKey")!;

            for (int i = 0; i < encryptedPassword.Length; i++)
            {
                if (!words.ContainsKey(encryptedPassword[i]))
                {
                    result += encryptedPassword[i];
                    continue;
                }

                int sum = 0;
                sum = words[encryptedPassword[i]] - words[key[counter]] < 0 ? 26 - (words[key[counter]] - words[encryptedPassword[i]]) : words[encryptedPassword[i]] - words[key[counter]];

                foreach (KeyValuePair<char, int> pair in words)
                {
                    if (pair.Value == sum)
                    {
                        result += pair.Key;
                    }
                }
                if (++counter >= key.Length)
                {
                    counter = 0;
                }
            }
            return result;
        }
    }
}
