package org.example.Infrastructure.mappers;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.DAL.entities.CategoryEntity;
import org.example.DAL.entities.ProductEntity;
import org.example.DAL.repositories.IProductRepository;
import org.example.Infrastructure.dto.commentDTO.CommentItemDTO;
import org.example.Infrastructure.dto.productDTO.ProductImageItemDTO;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;
import org.example.Infrastructure.storage.IStorageService;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.Objects;

@Component
@Primary
@AllArgsConstructor
public class ProductMapper implements IProductMapper {
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
        var commentsDto = new ArrayList<CommentItemDTO>();
        var comments = productEntity.getComments();
        for (var comment : comments) {
            var itemDto = new CommentItemDTO();
            itemDto.setId(comment.getId());
            itemDto.setDateCreated(comment.getDateCreated().toString());
            itemDto.setDateUpdated(comment.getDateUpdated().toString());
            itemDto.setTitle(comment.getTitle());
            itemDto.setMessage(comment.getMessage());
            itemDto.setLikes(comment.getLikes());
            itemDto.setDislikes(comment.getDislikes());
            itemDto.setStars(comment.getStars());
            itemDto.setProductId(comment.getProduct().getId());
            var user = comment.getUser();
            itemDto.setUserId(user.getId());
            itemDto.setUserName(user.getFirstname());
            commentsDto.add(itemDto);
        }
        dto.setComments(commentsDto);
        dto.setNumberOfComments(comments.size());



//        public List<CommentItemDTO> Comments { get; set; }
//        public int NumberOfComments { get; set; }
//        public int Stars { get; set; }
        return dto;
    }
}
