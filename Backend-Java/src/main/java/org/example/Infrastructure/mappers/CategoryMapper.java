package org.example.Infrastructure.mappers;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.DAL.repositories.IProductRepository;
import org.example.Infrastructure.dto.categoryDTO.CategoryCreateDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryItemDTO;
import org.example.DAL.entities.CategoryEntity;
import org.example.DAL.repositories.ICategoryRepository;
import org.example.Infrastructure.interfaces.IMappers.ICategoryMapper;
import org.example.Infrastructure.interfaces.IMappers.IProductMapper;
import org.example.Infrastructure.storage.FileSaveFormat;
import org.example.Infrastructure.storage.IStorageService;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@Component
@Primary
@AllArgsConstructor
public class CategoryMapper implements ICategoryMapper {
    private final ICategoryRepository _categoryRepository;
    private final IProductRepository _productRepository;
    private final IStorageService _storageService;
    private final ICategoryMapper _categoryMapper;
    private final IProductMapper _productMapper;
    @Override
    public CategoryItemDTO CategoryItemDTOByCategoryEntity(CategoryEntity categoryEntity) {
        CategoryItemDTO dto = new CategoryItemDTO();
        dto.setId(categoryEntity.getId());
        dto.setDescription(categoryEntity.getDescription());
        dto.setName(categoryEntity.getName());
        dto.setImage(Path.ApiURL + "images/" + categoryEntity.getImage());
        dto.setDateCreated(categoryEntity.getDateCreated().toString());
        dto.setDateUpdated(categoryEntity.getDateUpdated().toString());
        //Get parent
        CategoryEntity parent = categoryEntity.getParent();
        if(Objects.nonNull(parent)) {
            dto.setParentId(parent.getId());
            dto.setParentName(parent.getName());
        }
        //Get subcategories
        var allCategories = _categoryRepository.findAll();
        var subcategories = new ArrayList<CategoryItemDTO>();
        for(var cat: allCategories) {
            var optParent = cat.getParent();
            if(Objects.nonNull(optParent) && optParent.getId() == categoryEntity.getId()) {
                var item = _categoryMapper.CategoryItemDTOByCategoryEntity(cat);
                subcategories.add(item);
            }
        }
        dto.setSubcategories(subcategories);
        dto.setCountSubategories(subcategories.size());

        //GetProducts
        var products = categoryEntity.getProducts();
        if(Objects.nonNull(products)) {
            var productsDto = products.stream()
                    .map(_productMapper::ProductItemDTOByProductEntity)
                    .collect(Collectors.toList());
            dto.setProducts(productsDto);
        }
        dto.setCountProducts(products.size());
        return dto;
    }
}
