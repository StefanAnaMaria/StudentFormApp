using System;
using WebApplication1.DTO;
using WebApplication1.Models;
namespace WebApplication1.Services;

public interface IUserService
{  
        public User ValidateUser(string email, string password); // Păstrezi entitatea, pentru autentificare
        public User GetUserByEmail(string email); // Poate rămâne dacă vrei să cauți intern
        public void AddUser(UserDTO userDTO); // 🔁 Asta e cheia — trebuie să primească un DTO
        public List<UserDTO> GetAllUsers(); // DTO pentru expunere mai sigură
        public List<RoleDTO> GetUserRoleById(int userId); // La fel, mai safe să lucrezi cu DTO aici
        public List<UserDTO> GetUsers(); // sau List<User> dacă nu vrei control fin
        public UserDTO GetUserById(int id); 
        public void UpdateUser(UserDTO user);
        public void DeleteUser(int idUser);


}
