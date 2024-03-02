package org.example.Infrastructure.interfaces.IMappers;

import org.example.Infrastructure.dto.categoryDTO.CategoryCreateDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryItemDTO;
import org.example.DAL.entities.CategoryEntity;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

import java.util.List;
@Mapper(componentModel = "spring")
public interface ICategoryMapper {
    //ICategoryMapper INSTANCE = Mappers.getMapper(ICategoryMapper.class);

    @Mapping(source = "parent.name", target = "parentName")
    @Mapping(source = "parent.id", target = "parentId")
    CategoryItemDTO CategoryItemDTOByCategoryEntity(CategoryEntity categoryEntity);
}
