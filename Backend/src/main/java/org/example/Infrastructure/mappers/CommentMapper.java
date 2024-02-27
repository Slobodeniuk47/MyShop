package org.example.Infrastructure.mappers;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.DAL.entities.CategoryEntity;
import org.example.DAL.entities.CommentEntity;
import org.example.DAL.repositories.IUserRepository;
import org.example.Infrastructure.dto.accountDTO.UserItemDTO;
import org.example.Infrastructure.dto.commentDTO.CommentImageItemDTO;
import org.example.Infrastructure.dto.commentDTO.CommentItemDTO;
import org.example.Infrastructure.dto.productDTO.ProductImageItemDTO;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.Objects;

@Component
@Primary
@AllArgsConstructor
public class CommentMapper implements ICommentMapper {
    //private final IUserRepository
    public CommentItemDTO CommentItemDTOByCommentEntity(CommentEntity comment) {
        CommentItemDTO dto = new CommentItemDTO();
        dto.setId(comment.getId());
        dto.setDateCreated(comment.getDateCreated().toString());
        dto.setDateUpdated(comment.getDateUpdated().toString());
        dto.setTitle(comment.getTitle());
        dto.setMessage(comment.getMessage());
        dto.setLikes(comment.getLikes());
        dto.setDislikes(comment.getDislikes());
        dto.setStars(comment.getStars());
        dto.setProductId(comment.getProduct().getId());
        //Get User
        var userEntity = comment.getUser();
        if(Objects.nonNull(userEntity)) {
            dto.setUserId(userEntity.getId());
            dto.setUserName(userEntity.getFirstname());
            var userDto = new UserItemDTO();
            userDto.setId(userEntity.getId());
            userDto.setName(userEntity.getName());
            userDto.setImage(userEntity.getImage());
            userDto.setImageURL(userEntity.getImageURL());
            userDto.setEmail(userEntity.getEmail());
            userDto.setPhoneNumber(userEntity.getPhoneNumber());

            dto.setUser(userDto);
        }
        //Get images
        var images = new ArrayList<CommentImageItemDTO>();
        for(var img : comment.getCommentImages()) {
            var item = new CommentImageItemDTO();
            item.setId(img.getId());
            item.setName(Path.ApiURL + "images/" + img.getName());
            item.setCommentId(comment.getId());
            item.setCommentName(comment.getTitle());
            images.add(item);
        }
        dto.setImages(images);

        return dto;
    }
}
