package org.example.Infrastructure.interfaces.IServices;

import org.example.Infrastructure.dto.roleDTO.*;

import java.util.List;

public interface IRoleService {
    RoleItemDTO create(RoleCreateDTO model);
    List<RoleItemDTO> getAll();
    RoleItemDTO edit(RoleEditDTO model);
    void deleteById(int id);
    RoleItemDTO getById(int id);
}
