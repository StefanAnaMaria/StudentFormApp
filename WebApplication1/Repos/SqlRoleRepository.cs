using System;
using WebApplication1.Models;
namespace WebApplication1.Repos;

public class SqlRoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepository;
    public SqlRoleRepository(AppDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }
    public void AddRole(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    public void DeleteRole(int idRole)
    {
        var RoleDeModificat = _context.Roles.Find(idRole);
        if (RoleDeModificat != null)
        {
            _context.Roles.Remove(RoleDeModificat);
            _context.SaveChanges();
        }
    }

    public Role GetRole(int idRole)
    {
        throw new NotImplementedException();
    }

    public List<Role> GetRoles()
    {
        var rez = _context.Roles.ToList();
        return rez;
    }

    public void UpdateRole(Role role)
    {
        var RoleDeModificat = _context.Roles.Find(role.IdRole);
        if (RoleDeModificat != null)
        {
            RoleDeModificat.Name = role.Name;
            _context.SaveChanges();
        }
    }
    public Role GetRoleById(int idRole)
    {
        var rez = _context.Roles.Find(idRole);
        return rez;
    }
}
