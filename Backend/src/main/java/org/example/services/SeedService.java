package org.example.services;

import lombok.AllArgsConstructor;
import org.example.constants.Roles;
import org.example.entities.CategoryEntity;
import org.example.entities.RoleEntity;
import org.example.repositories.CategoryRepository;
import org.example.repositories.RoleRepository;
import org.springframework.stereotype.Service;

@Service
@AllArgsConstructor
public class SeedService {
    private final CategoryRepository _categoryRepository;
    private final RoleRepository _roleRepository;
    public void seedRolesData() {
        if(_roleRepository.count() == 0)
        {
            for (String roleName: Roles.All) {
                RoleEntity role = new RoleEntity();
                role.setName(roleName);
                _roleRepository.save(role);
            }
        }
    }
    public void seedUsersData() {

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
