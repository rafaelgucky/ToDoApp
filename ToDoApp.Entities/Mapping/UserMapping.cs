using ToDoApp.Entities.DTOs.UserDTOs;
using ToDoApp.Entities.Models;

namespace ToDoApp.Entities.Mapping
{
    public class UserMapping
    {
        // User To DTO

        public ReadUserDTO ToReadUserDto(User user)
        {
            return new ReadUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Password = user.Password,
                ImageName = user.ImageName,
                ImageUrl = user.ImageUrl
            };
        }

        public IEnumerable<ReadUserDTO> ToReadUserDto(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                yield return new ReadUserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    Password = user.Password,
                    ImageName = user.ImageName,
                    ImageUrl = user.ImageUrl
                };
            }
        }

        // DTO To User

        public User ToUser(CreateUserDTO create)
        {
            return new User
            {
                Name = create.Name,
                BirthDate = create.BirthDate,
                Email = create.Email,
                Password = create.Password
            };
        }

        // Update

        public User ToUser(UpdateUserNameDTO update)
        {
            return new User
            {
                Id = update.Id,
                Name = update.Name
            };
        }

        public User ToUser(UpdateUserBirthDateDTO update)
        {
            return new User
            {
                Id = update.Id,
                BirthDate = update.BirthDate
            };
        }

        public User ToUser(UpdateUserPasswordDTO update)
        {
            return new User
            {
                Id = update.Id,
                Password = update.NewPassword
            };
        }
    }
}
