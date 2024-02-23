package org.example.Infrastructure.mappers;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.DAL.entities.CategoryEntity;
import org.example.DAL.entities.ProductEntity;
import org.example.DAL.repositories.ProductRepository;
import org.example.Infrastructure.dto.productDTO.ProductImageItemDTO;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;
import org.example.Infrastructure.storage.StorageService;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

@Component
@Primary
@AllArgsConstructor
public class ProductMapper implements IProductMapper {
    private final ProductRepository _productRepository;
    private final StorageService _storageService;
    public ProductItemDTO ProductItemDTOByProductEntity(ProductEntity productEntity) {
        ProductItemDTO dto = new ProductItemDTO();
        dto.setId(productEntity.getId());
        dto.setName(productEntity.getName());
        dto.setDescription(productEntity.getDescription());
        dto.setPrice(productEntity.getPrice());
        dto.setDateCreated(productEntity.getDateCreated().toString());
        dto.setDateUpdated(productEntity.getDateUpdated().toString());
        //Get Category
        CategoryEntity categoryEntity = productEntity.getCategory();
        if(Objects.nonNull(categoryEntity)) {
            dto.setCategoryId(categoryEntity.getId());
            dto.setCategoryName(categoryEntity.getName());
        }
        //Get images
        var images = new ArrayList<ProductImageItemDTO>();
        //var listImg = _productRepository.findById(productEntity.getId());
        for(var img : productEntity.getProductImages()) {
            var item = new ProductImageItemDTO();
            item.setId(img.getId());
            item.setName(Path.ApiURL + "images/" + img.getName());
            item.setDateCreated(img.getDateCreated().toString());
            item.setDateUpdated(img.getDateUpdated().toString());

            images.add(item);
        }
        dto.setImages(images);


//        public List<CommentItemDTO> Comments { get; set; }
//        public int NumberOfComments { get; set; }
//        public int Stars { get; set; }
        return dto;
    }
}
