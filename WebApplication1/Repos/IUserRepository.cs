using System;
using WebApplication1.Models;
namespace WebApplication1.Repos;

public interface IUserRepository
{
    public void AddUser(User user);
    public void DeleteUser(int idUser); 
    public void UpdateUser(User user);
    public User GetUser(int idUser);    
    public List<User> GetUsers();
    public User GetUserByEmail(string email);
    public User GetUserById(int idUser);

}
