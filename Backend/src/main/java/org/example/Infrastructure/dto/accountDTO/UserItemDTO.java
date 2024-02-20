package org.example.Infrastructure.dto.accountDTO;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.List;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class UserItemDTO {
    private Integer id;
    private String name;
    private boolean IsDelete;
    private LocalDateTime dateCreated;
    private LocalDateTime dateUpdated;
    private String email;
    private String phoneNumber;
    private String passwordHash;
    List<PermissionItemDTO> permissions;
}
