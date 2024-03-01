package org.example.Infrastructure.dto.roleDTO;

import lombok.Data;

@Data
public class RoleItemDTO {
    int id;
    String roleName;
    String concurrencyStamp;
}
