package org.example.Infrastructure.mappers;

import org.example.DAL.entities.ProductEntity;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

@Mapper(componentModel = "spring")
public interface IProductMapper {
    @Mapping(source = "category.name", target = "categoryName")
    @Mapping(source = "category.id", target = "categoryId")
    ProductItemDTO ProductItemDTOByProductEntity(ProductEntity product);
}