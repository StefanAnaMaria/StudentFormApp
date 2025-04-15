using System;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repos;
namespace WebApplication1.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public void AddRole(RoleDTO roleDTO)
        {
            var role = new Role
            {
                Name = roleDTO.Name
            };

            _roleRepository.AddRole(role);
        }

        public void DeleteRole(int idRole)
        {
            var role = _roleRepository.GetRole(idRole);
            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            _roleRepository.DeleteRole(idRole);
        }

        public List<RoleDTO> GetAllRoles()
        {
            var roles = _roleRepository.GetRoles();
            var roleDTOs = new List<RoleDTO>();

            foreach (var role in roles)
            {
                roleDTOs.Add(new RoleDTO
                {
                    IdRole = role.IdRole,
                    Name = role.Name
                });
            }

            return roleDTOs;
        }

        public RoleDTO GetRole(int idRole)
        {
            var role = _roleRepository.GetRole(idRole);
            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            return new RoleDTO
            {
                IdRole = role.IdRole,
                Name = role.Name
            };
        }

        public List<string> GetRolesByUserId(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return new List<string> { user.Role.Name };
        }

        public void UpdateRole(RoleDTO roleDTO)
        {
            var role = _roleRepository.GetRole(roleDTO.IdRole);
            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            role.Name = roleDTO.Name;
            _roleRepository.UpdateRole(role);
        }
}
