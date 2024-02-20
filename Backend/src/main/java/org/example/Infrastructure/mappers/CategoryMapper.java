package org.example.Infrastructure.mappers;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.Infrastructure.dto.categoryDTO.CategoryCreateDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryItemDTO;
import org.example.DAL.entities.CategoryEntity;
import org.example.DAL.repositories.CategoryRepository;
import org.example.Infrastructure.storage.FileSaveFormat;
import org.example.Infrastructure.storage.StorageService;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

@Component
@Primary
@AllArgsConstructor
public class CategoryMapper implements ICategoryMapper {
    private final CategoryRepository _categoryRepository;
    private final StorageService _storageService;
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
        var subcategories = new ArrayList<CategoryItemDTO>();
        var listCategory = _categoryRepository.findAll();
        for (CategoryEntity cat: listCategory) {
            if(Objects.nonNull(cat.getParent())) {
                if(cat.getParent().getId() == categoryEntity.getId()) {
                    var item = new CategoryItemDTO();
                    item.setId(cat.getId());
                    item.setDescription(cat.getDescription());
                    item.setName(cat.getName());
                    item.setImage(cat.getImage());
                    item.setDateCreated(cat.getDateCreated().toString());
                    item.setDateUpdated(cat.getDateUpdated().toString());

                    subcategories.add(item);
                }
            }
        }
        dto.setSubcategories(subcategories);
        dto.setCountSubategories(subcategories.size());
//        private Integer countProducts;
//        @JsonProperty("Subcategories")
//        private Set<ProductItemDTO> products;
        return dto;
    }
    @Override
    public List<CategoryItemDTO> CategoryItemDTOListByCategoryEntityList(List<CategoryEntity> list) {
        return new ArrayList<CategoryItemDTO>();
    }
    @Override
    public CategoryEntity CategoryEntityByCategoryCreateDTO(CategoryCreateDTO model){
        var category = new CategoryEntity();
        String fileName = _storageService.saveByFormat(model.getImage(), FileSaveFormat.PNG);
        category.setImage(fileName);
        category.setName(model.getName());
        category.setDescription(model.getDescription());
        //Set parent
        Integer parentId = model.getParentId();
        if(parentId != null) {
            category.setParent(_categoryRepository.getById(parentId));
        }
        else {
            category.setParent(null);
        }

        return category;
    }
}
