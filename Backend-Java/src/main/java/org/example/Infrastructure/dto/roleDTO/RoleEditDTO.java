package org.example.Infrastructure.dto.roleDTO;

import lombok.Data;

@Data
public class RoleEditDTO {
    int id;
    String roleName;
    String concurrencyStamp;
}
