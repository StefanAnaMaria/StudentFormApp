using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repos;
using WebApplication1.Services; 
using Microsoft.EntityFrameworkCore;

using BCrypt.Net;

namespace WebApplication1.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository; // Adăugat pentru a accesa UserRepository

    public UserService(AppDbContext context, IRoleRepository roleRepository, IUserRepository userRepository)
    {
        _userRepository = userRepository; // Inițializăm UserRepositor
        _roleRepository = roleRepository;
        _context = context;
    }
    public void AddUser(UserDTO userDTO)
    {
        // Verifică dacă rolul există în baza de date
        // var role=_userRepository.GetRoleById(userDTO.RoleId);
        var role= _roleRepository.GetRoleById(userDTO.RoleId); // Obține rolul pe baza ID-ului din UserDTO
        if (role == null)
        {
            throw new Exception("Role does not exist.");
        }

        // Creează obiectul User pe baza UserDTO
        var user = new User
        {
            Name = userDTO.Name,
            Email = userDTO.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
            Role = role // Legătura corectă cu rolul
        };

        // Salvează user-ul folosind repository-ul
        _userRepository.AddUser(user);
    }

    // public void AddUser(UserDTO userDTO)
    // {
    //     // Verifică dacă rolul există în baza de date
    //     var role = _context.Roles.FirstOrDefault(r => r.IdRole == userDTO.Role.IdRole);
    //     if (role == null)
    //     {
    //         throw new Exception("Role does not exist.");
    //     }

    //     // Creează obiectul User pe baza UserDTO
    //     var user = new User
    //     {
    //         Name = userDTO.Name,
    //         Email = userDTO.Email,
    //         Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
    //         Role = role // Legătura corectă cu rolul
    //     };

    //     // Salvează user-ul în DB
    //     _context.Users.Add(user);
    //     _context.SaveChanges();
    // }

     public void DeleteUser(int idUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUser == idUser);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

    public List<UserDTO> GetAllUsers()
    {
        var users = _context.Users.Include(u => u.Role).ToList();
        return users.Select(u => new UserDTO
        {
            IdUser = u.IdUser,
            Name = u.Name,
            Email = u.Email,
            Password = u.Password,
            RoleId= u.Role.IdRole, // ID-ul rolului asociat utilizatorului
        }).ToList();
    }

    public UserDTO GetUserById(int id)
    {
        // var user = _context.Users
        var user= _userRepository.GetUserById(id);// Folosește UserRepository pentru a obține utilizatorul
        // .Include(u => u.Role)  // Includează și rolul utilizatorului
        // .FirstOrDefault(u => u.IdUser == id);
    
        if (user == null)
        {
            return null;
        }


        return new UserDTO
        {
            IdUser = user.IdUser,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            RoleName = user.Role.Name, // Rolul asociat utilizatorului
            // ListaStudentForms = user.ListaStudentForms // Include lista de formulare asociate utilizatorului
        };
    }

    public User GetUserByEmail(string email)
    {
        // return _context.Users.FirstOrDefault(u => u.Email == email);
       return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public List<RoleDTO> GetUserRoleById(int userId)
    {
        var user = _context.Users
            .Include(u => u.Role)  // Asigură-te că se include și rolul
            .FirstOrDefault(u => u.IdUser == userId);

        return user != null ? new List<RoleDTO> { new RoleDTO { IdRole = user.Role.IdRole, Name = user.Role.Name } } : null;
    }

    public void UpdateUser(UserDTO userDTO)
    {
        var user = _context.Users.FirstOrDefault(u => u.IdUser == userDTO.IdUser);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        user.Name = userDTO.Name;
        user.Email = userDTO.Email;
        user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
        user.IdUser = userDTO.IdUser;

        _context.SaveChanges();
    }

    public User ValidateUser(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return user;
        }
        return null;
    }

    public List<UserDTO> GetUsers()
    {
        throw new NotImplementedException();
    }
}
