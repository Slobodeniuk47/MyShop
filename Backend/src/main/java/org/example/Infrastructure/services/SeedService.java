package org.example.Infrastructure.services;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Roles;
import org.example.DAL.entities.CategoryEntity;
import org.example.DAL.entities.RoleEntity;
import org.example.DAL.entities.UserEntity;
import org.example.DAL.entities.UserRoleEntity;
import org.example.DAL.repositories.CategoryRepository;
import org.example.DAL.repositories.RoleRepository;
import org.example.DAL.repositories.UserRepository;
import org.example.DAL.repositories.UserRoleRepository;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

@Service
@AllArgsConstructor
public class SeedService {
    private final CategoryRepository _categoryRepository;
    private final RoleRepository _roleRepository;
    private final UserRepository _userRepository;
    private final PasswordEncoder _passwordEncoder;
    private final UserRoleRepository _userRoleRepository;
    public void seedRolesData() {
        if(_roleRepository.count() == 0)
        {
            for (String roleName: Roles.All) {
                var role = new RoleEntity();
                role.setName(roleName);
                _roleRepository.save(role);
            }
        }
    }
    public void seedUsersData() {
        if (_userRepository.count() == 0) {
            var user = new UserEntity().builder()
                    .email("admin@gmail.com")
                    .firstname("Artem")
                    .lastname("Slobodeniuk")
                    .phoneNumber("332 233 32 23")
                    .passwordHash(_passwordEncoder.encode("123456"))
                    .build();
            _userRepository.save(user);
            var role = _roleRepository.findByName(Roles.Admin);
            var permission = new UserRoleEntity().builder()
                    .role(role)
                    .user(user)
                    .build();
            _userRoleRepository.save(permission);
        }
    }
    public void seedCategoriesData() {
        if(_categoryRepository.count() == 0)
        {
            CategoryEntity category = new CategoryEntity();
            category.setName("Categories");
            category.setDescription("For everyone");
            category.setImage("product.jpg");

            _categoryRepository.save(category);
            System.out.println("[Category was created] " + category.getName());
        }
    }

}
