package org.example.services;

import lombok.AllArgsConstructor;
import org.example.interfaces.ICategoryService;
import org.example.dto.categoryDTO.CategoryCreateDTO;
import org.example.dto.categoryDTO.CategoryEditDTO;
import org.example.dto.categoryDTO.CategoryItemDTO;
import org.example.entities.CategoryEntity;
import org.example.mappers.ApplicationMapper;
import org.example.repositories.CategoryRepository;
import org.example.storage.FileSaveFormat;
import org.example.storage.StorageService;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@Service
@AllArgsConstructor
public class CategoryService implements ICategoryService {
    private final CategoryRepository _categoryRepository;
    private final ApplicationMapper _appMapper;
    private final StorageService _storageService;

    @Override
    public List<CategoryItemDTO> getAll() {
        var categories = _categoryRepository.findAll();
        return _categoryRepository.saveAll(categories)
                .stream()
                .map(_appMapper::CategoryItemDTOByCategoryEntity)
                .collect(Collectors.toList());
        //var result = _appMapper.CategoryItemDTOListByCategoryEntityList(categories);
        //return result;
    }
    @Override
    public CategoryItemDTO getById(int id) {
        var catOptional = _categoryRepository.findById(id);

        if(catOptional.isPresent())
        {
            var result = _appMapper.CategoryItemDTOByCategoryEntity(catOptional.get());

            if(!Objects.isNull(catOptional.get().getParent())) {
                var parent = _categoryRepository.findById(catOptional.get().getParent().getId());
                result.setParentId(parent.get().getId());
                result.setParentName(parent.get().getName());
            }
            return result;
        }
        return null;
    }
    @Override
    public CategoryItemDTO create(CategoryCreateDTO model) {
        CategoryEntity category = _appMapper.CategoryEntityByCategoryCreateDTO(model);

        String fileName = _storageService.saveByFormat(model.getImage(), FileSaveFormat.WEBP);
        category.setImage(fileName);

        Integer parentId = model.getParentId();
        if(parentId != null) {
            category.setParent(_categoryRepository.getById(parentId));
        }
        else {
            category.setParent(null);
        }
        _categoryRepository.save(category);

        var result = _appMapper.CategoryItemDTOByCategoryEntity(category);
        result.setParentId(model.getParentId());

        return result;
    }
    @Override
    public CategoryItemDTO edit(CategoryEditDTO model) {
        var catOptional = _categoryRepository.findById(model.getId());

        String fileName = model.getImage().toString();
        if (!Objects.isNull(model.getImage())) {
            _storageService.removeFile(catOptional.get().getImage());
            fileName = _storageService.saveByFormat(model.getImage(), FileSaveFormat.WEBP);
        }
        if(catOptional.isPresent())
        {
            var cat = catOptional.get();
            cat.setName(model.getName());
            cat.setDescription(model.getDescription());
            cat.setImage(fileName);
            cat.setDateUpdated(LocalDateTime.now());
            Integer parentId = model.getParentId();
            if(parentId != null) {
                cat.setParent(_categoryRepository.getById(parentId));
            }
            else {
                cat.setParent(null);
            }
            _categoryRepository.save(cat);

            var result = _appMapper.CategoryItemDTOByCategoryEntity(cat);
            result.setParentId(model.getParentId());
            return result;
        }
        return null;
    }
    @Override
    public void deleteById(int id) {
        CategoryEntity category = _categoryRepository.findById(id).get();
        _storageService.removeFile(category.getImage());
        _categoryRepository.deleteById(category.getId());
    }
}
