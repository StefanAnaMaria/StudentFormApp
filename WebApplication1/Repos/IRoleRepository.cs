using System;
using WebApplication1.Models;
namespace WebApplication1.Repos;

public interface IRoleRepository
{

    void AddRole(Role role);
    void DeleteRole(int idRole);    
    void UpdateRole(Role role);
    Role GetRole(int idRole);
    List<Role> GetRoles();
    Role GetRoleById(int idRole);
}
