package org.example.Infrastructure.services;

import lombok.RequiredArgsConstructor;
import org.example.DAL.entities.account.RoleEntity;
import org.example.DAL.repositories.IRoleRepository;
import org.example.Infrastructure.dto.roleDTO.RoleCreateDTO;
import org.example.Infrastructure.dto.roleDTO.RoleEditDTO;
import org.example.Infrastructure.dto.roleDTO.RoleItemDTO;
import org.example.Infrastructure.interfaces.IServices.IRoleService;
import org.example.Infrastructure.interfaces.IMappers.IRoleMapper;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class RoleService implements IRoleService {
    private final IRoleRepository _roleRepository;
    private final IRoleMapper _roleMapper;
    @Override
    public List<RoleItemDTO> getAll() {
        var roles = _roleRepository.findAll();
        return _roleRepository.saveAll(roles)
                .stream()
                .map(_roleMapper::RoleItemDTOByRoleEntity)
        .collect(Collectors.toList());
    }
    @Override
    public RoleItemDTO getById(int id){
            var role = _roleRepository.getById(id);
            var result = _roleMapper.RoleItemDTOByRoleEntity(role);
            return result;
    }
    @Override
    public RoleItemDTO create(RoleCreateDTO model) {
        var role = new RoleEntity();
        role.setName(model.getRoleName());
        _roleRepository.save(role);
        var result = _roleMapper.RoleItemDTOByRoleEntity(role);
        return result;
    }
    @Override
    public RoleItemDTO edit(RoleEditDTO model) {
        var role = _roleRepository.getById(model.getId());
        role.setName(model.getRoleName());
        role.setDateUpdated(LocalDateTime.now());
        _roleRepository.save(role);
        var result = _roleMapper.RoleItemDTOByRoleEntity(role);
        return result;
    }
    @Override
    public void deleteById(int id) {
        _roleRepository.deleteById(id);
    }
}
