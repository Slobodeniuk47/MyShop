package org.example.Infrastructure.mappers;

import lombok.AllArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.DAL.entities.account.UserEntity;
import org.example.DAL.repositories.IUserRoleRepository;
import org.example.Infrastructure.dto.accountDTO.PermissionItemDTO;
import org.example.Infrastructure.dto.accountDTO.UserItemDTO;
import org.example.Infrastructure.interfaces.IMappers.IUserMapper;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import java.util.ArrayList;

@Component
@Primary
@AllArgsConstructor
public class UserMapper implements IUserMapper {
    private final IUserRoleRepository _userRoleRepository;
    @Override
    public UserItemDTO UserItemDTOByUserEntity(UserEntity userEntity) {
        UserItemDTO dto = new UserItemDTO();
        dto.setId(userEntity.getId());
        dto.setEmail(userEntity.getEmail());
        dto.setFirstname(userEntity.getFirstname());
        dto.setLastname(userEntity.getLastname());
        dto.setIsDelete(userEntity.isIsDelete());
        dto.setDateCreated(userEntity.getDateCreated().toString());
        dto.setDateUpdated(userEntity.getDateUpdated().toString());
        dto.setPhoneNumber(userEntity.getPhoneNumber());
        dto.setImage(userEntity.getImage());
        dto.setImageURL(userEntity.isGoogle() == false ? Path.ApiURL + "images/"+ userEntity.getImage() : userEntity.getImage());
        //Get permissions
        var permissionsDTO = new ArrayList<PermissionItemDTO>();
        var permissions = _userRoleRepository.findByUser(userEntity);
        for(var role : permissions) {
            var item = new PermissionItemDTO();
            item.setRoleName(role.getRole().getName());
            permissionsDTO.add(item);
        }
        dto.setPermissions(permissionsDTO);
        return dto;
    }
}
