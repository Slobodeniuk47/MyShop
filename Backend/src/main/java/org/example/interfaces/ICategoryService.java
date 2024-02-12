package org.example.interfaces;

import org.example.dto.categoryDTO.CategoryCreateDTO;
import org.example.dto.categoryDTO.CategoryEditDTO;
import org.example.dto.categoryDTO.CategoryItemDTO;

import java.util.List;

public interface ICategoryService {
    CategoryItemDTO create(CategoryCreateDTO model);
    List<CategoryItemDTO> getAll();
    CategoryItemDTO edit(CategoryEditDTO model);
    void deleteById(int id);
    CategoryItemDTO getById(int id);
}
