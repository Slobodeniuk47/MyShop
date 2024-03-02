package org.example.Infrastructure.mappers;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.DAL.entities.CategoryEntity;
import org.example.DAL.entities.ProductEntity;
import org.example.Infrastructure.dto.commentDTO.CommentItemDTO;
import org.example.Infrastructure.dto.productDTO.ProductImageItemDTO;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;
import org.example.Infrastructure.interfaces.IMappers.ICommentMapper;
import org.example.Infrastructure.interfaces.IMappers.IProductMapper;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.Objects;
import java.util.stream.Collectors;

@Component
@Primary
@AllArgsConstructor
public class ProductMapper implements IProductMapper {
    private final ICommentMapper _commentMapper;
    @Override
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
        var imagesDto = new ArrayList<ProductImageItemDTO>();
        for(var img : productEntity.getProductImages()) {
            var item = new ProductImageItemDTO();
            item.setId(img.getId());
            item.setName(Path.ApiURL + "images/" + img.getName());
            item.setDateCreated(img.getDateCreated().toString());
            item.setDateUpdated(img.getDateUpdated().toString());

            imagesDto.add(item);
        }
        dto.setImages(imagesDto);
        //GetComments
        var comments = productEntity.getComments();
        var commentsDto = comments.stream()
                .map(_commentMapper::CommentItemDTOByCommentEntity)
                .collect(Collectors.toList());

        dto.setComments(commentsDto);
        dto.setNumberOfComments(comments.size());

        return dto;
    }
}
