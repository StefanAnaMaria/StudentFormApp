using System;
using WebApplication1.DTO;
using WebApplication1.Models;
namespace WebApplication1.Services;

public interface IRoleService
{
    public List<string> GetRolesByUserId(int userId);
    public void AddRole(RoleDTO role);
    public List<RoleDTO> GetAllRoles();
    public RoleDTO GetRole(int idRole);
    public void DeleteRole(int idRole);
    public void UpdateRole(RoleDTO role);


}

