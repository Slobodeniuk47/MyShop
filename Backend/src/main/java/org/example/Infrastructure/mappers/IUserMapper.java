package org.example.Infrastructure.mappers;

import org.example.Infrastructure.dto.accountDTO.UserItemDTO;
import org.example.DAL.entities.UserEntity;
import org.mapstruct.Mapper;

import java.util.List;

@Mapper(componentModel = "spring")
public interface IUserMapper {
    //IUserMapper INSTANCE = Mappers.getMapper(IUserMapper.class);

    //@Mapping(target = "passwordHash", ignore = true)
    //UserEntity itemDtoToUser(UserCreateDTO registerDTO);
    UserItemDTO UserItemDTOByUserEntity(UserEntity userEntity);
    List<UserItemDTO> UserItemDTOListByUserEntityList(List<UserEntity> list);
}
