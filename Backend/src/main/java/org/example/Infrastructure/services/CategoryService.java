package org.example.Infrastructure.services;

import lombok.AllArgsConstructor;
import org.example.Infrastructure.interfaces.ICategoryService;
import org.example.Infrastructure.dto.categoryDTO.CategoryCreateDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryEditDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryItemDTO;
import org.example.DAL.entities.CategoryEntity;
import org.example.Infrastructure.mappers.ICategoryMapper;
import org.example.DAL.repositories.CategoryRepository;
import org.example.Infrastructure.storage.FileSaveFormat;
import org.example.Infrastructure.storage.StorageService;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@Service
@AllArgsConstructor
public class CategoryService implements ICategoryService {
    private final CategoryRepository _categoryRepository;
    private final ICategoryMapper _categoryMapper;
    private final StorageService _storageService;

    @Override
    public List<CategoryItemDTO> getAll() {
        var categories = _categoryRepository.findAll();
        //var result = _appMapper.CategoryItemDTOListByCategoryEntityList(categories);
        return _categoryRepository.saveAll(categories)
                .stream()
                .map(_categoryMapper::CategoryItemDTOByCategoryEntity)
                .collect(Collectors.toList());
    }
    @Override
    public List<CategoryItemDTO> getMainCategories() {
        var mainCategories = new ArrayList<CategoryEntity>();
        for (CategoryEntity category : _categoryRepository.findAll()) {
            if(category.getParent() == null)
            {
                mainCategories.add(category);
            }
        }
        return _categoryRepository.saveAll(mainCategories)
                .stream()
                .map(_categoryMapper::CategoryItemDTOByCategoryEntity)
                .collect(Collectors.toList());
    }
    @Override
    public CategoryItemDTO getById(int id) {
        var catOptional = _categoryRepository.findById(id);

        if(catOptional.isPresent())
        {
            var result = _categoryMapper.CategoryItemDTOByCategoryEntity(catOptional.get());

            if(!Objects.isNull(catOptional.get().getParent())) {
                var parent = _categoryRepository.findById(catOptional.get().getParent().getId());
                //result.setParentId(parent.get().getId());
                //result.setParentName(parent.get().getName());
            }
            return result;
        }
        return null;
    }
    @Override
    public CategoryItemDTO create(CategoryCreateDTO model) {
        CategoryEntity category = _categoryMapper.CategoryEntityByCategoryCreateDTO(model);

//        String fileName = _storageService.saveByFormat(model.getImage(), FileSaveFormat.WEBP);

//        Integer parentId = model.getParentId();
//        if(parentId != null) {
//            category.setParent(_categoryRepository.getById(parentId));
//        }
//        else {
//            category.setParent(null);
//        }
        _categoryRepository.save(category);

        var result = _categoryMapper.CategoryItemDTOByCategoryEntity(category);
        //result.setParentId(model.getParentId());

        return result;
    }
    @Override
    public CategoryItemDTO edit(CategoryEditDTO model) {
        var catOptional = _categoryRepository.findById(model.getId());

        String fileName = null;
        if (!Objects.isNull(model.getImageUpload())) {
            fileName = model.getImageUpload().toString();
            _storageService.removeFile(catOptional.get().getImage());
            fileName = _storageService.saveByFormat(model.getImageUpload(), FileSaveFormat.PNG);
        }
        if(catOptional.isPresent())
        {
            var cat = catOptional.get();
            cat.setName(model.getName());
            cat.setDescription(model.getDescription());
            if (!Objects.isNull(model.getImageUpload())) {
                cat.setImage(fileName);
            }
            cat.setDateUpdated(LocalDateTime.now());
            Integer parentId = model.getParentId();
            if(parentId != null && parentId != 0) {
                cat.setParent(_categoryRepository.getById(parentId));
            }
            else {
                cat.setParent(null);
            }
            _categoryRepository.save(cat);

            var result = _categoryMapper.CategoryItemDTOByCategoryEntity(cat);
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
