package org.example.Infrastructure.interfaces.IMappers;

import org.example.DAL.entities.CommentEntity;
import org.example.DAL.entities.ProductEntity;
import org.example.Infrastructure.dto.commentDTO.CommentItemDTO;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

@Mapper(componentModel = "spring")
public interface ICommentMapper {
    //@Mapping(source = "product.id", target = "productId")
    //@Mapping(source = "user.name", target = "userName")
    //@Mapping(source = "user.id", target = "userId")
    CommentItemDTO CommentItemDTOByCommentEntity(CommentEntity comment);
}
