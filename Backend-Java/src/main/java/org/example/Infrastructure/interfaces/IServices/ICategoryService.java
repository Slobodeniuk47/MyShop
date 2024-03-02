package org.example.Infrastructure.interfaces.IServices;

import org.example.Infrastructure.dto.categoryDTO.CategoryCreateDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryEditDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryItemDTO;

import java.util.List;

public interface ICategoryService {
    CategoryItemDTO create(CategoryCreateDTO model);
    List<CategoryItemDTO> getAll();
    List<CategoryItemDTO> getMainCategories();
    CategoryItemDTO edit(CategoryEditDTO model);
    void deleteById(int id);
    CategoryItemDTO getById(int id);
}
