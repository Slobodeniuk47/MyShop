package org.example.mappers;

import org.example.dto.accountDTO.RegisterDTO;
import org.example.dto.accountDTO.UserItemDTO;
import org.example.dto.categoryDTO.CategoryCreateDTO;
import org.example.dto.categoryDTO.CategoryItemDTO;
import org.example.entities.CategoryEntity;
import org.example.entities.UserEntity;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;
import org.mapstruct.ReportingPolicy;
import org.mapstruct.factory.Mappers;

import java.util.List;

@Mapper(unmappedSourcePolicy = ReportingPolicy.IGNORE, componentModel = "spring")
public interface ApplicationMapper {
    ApplicationMapper INSTANCE = Mappers.getMapper(ApplicationMapper.class);

    @Mapping(target = "passwordHash", ignore = true)
    UserEntity itemDtoToUser(RegisterDTO registerDTO);
    @Mapping(source = "description", target = "description")
    CategoryItemDTO CategoryItemDTOByCategoryEntity(CategoryEntity categoryEntity);
    @Mapping(source = "parent.name", target = "parentName")
    @Mapping(source = "parent.id", target = "parentId")
    List<CategoryItemDTO> CategoryItemDTOListByCategoryEntityList(List<CategoryEntity> list);
    UserItemDTO UserItemDTOByUserEntity(UserEntity userEntity);
    List<UserItemDTO> UserItemDTOListByUserEntityList(List<UserEntity> list);
    @Mapping(source = "image", target = "image", ignore = true)
    CategoryEntity CategoryEntityByCategoryCreateDTO(CategoryCreateDTO model);
}
