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
    private String image;
    private String imageURL;
    private String firstname;
    private String lastname;
    private boolean IsDelete;
    private String dateCreated;
    private String dateUpdated;
    private String email;
    private String phoneNumber;
    private String passwordHash;
    List<PermissionItemDTO> permissions;
}
