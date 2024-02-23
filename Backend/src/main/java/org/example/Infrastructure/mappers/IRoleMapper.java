package org.example.Infrastructure.mappers;

import org.example.DAL.entities.account.RoleEntity;
import org.example.Infrastructure.dto.roleDTO.RoleItemDTO;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

@Mapper(componentModel = "spring")
public interface IRoleMapper {
    @Mapping(source = "name", target = "roleName")
    RoleItemDTO RoleItemDTOByRoleEntity(RoleEntity roleEntity);
}
