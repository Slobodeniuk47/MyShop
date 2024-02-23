package org.example.Infrastructure.mappers;

import org.example.Infrastructure.dto.accountDTO.UserItemDTO;
import org.example.DAL.entities.account.UserEntity;
import org.mapstruct.Mapper;

import java.util.List;

@Mapper(componentModel = "spring")
public interface IUserMapper {
    //IUserMapper INSTANCE = Mappers.getMapper(IUserMapper.class);
    UserItemDTO UserItemDTOByUserEntity(UserEntity userEntity);
}
