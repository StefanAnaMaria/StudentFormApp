using System;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore; // Necesare pentru Include și interogările LINQ

namespace WebApplication1.Repos;

public class SqlUserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public SqlUserRepository (AppDbContext context)
    {
        _context = context;
    }
    public void AddUser(User user)
    {
        _context.Add(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int idUser)
    {
       var userDeModificat = _context.Users.Find(idUser);
        if (userDeModificat != null)
        {
            _context.Users.Remove(userDeModificat);
            _context.SaveChanges();
        }
    }

    public User GetUser(int idUser)
    {
        var rez = _context.Users.Find(idUser);
        return rez;
    }

    public User GetUserByEmail(string email)
    {
        var rez=_context.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()));
        return rez;
    }
    public User GetUserById(int idUser)
    {
        var rez = _context.Users
        .Include(u => u.Role) // Includează rolul utilizatorului
        .Include(u => u.ListaStudentForms) // Include lista de formulare asociate utilizatorului    
        .FirstOrDefault(u => u.IdUser == idUser);
        return rez;
    }

    public List<User> GetUsers()
    {
        var rez = _context.Users.ToList();
        return rez;
    }

    public void UpdateUser(User user)
    {
        var userDeModificat = _context.Users.Find(user.IdUser);
        if (userDeModificat != null)
        {
            userDeModificat.Name = user.Name;
            userDeModificat.Email = user.Email;
            userDeModificat.Password= user.Password;
            _context.SaveChanges();
        }
    }
}
