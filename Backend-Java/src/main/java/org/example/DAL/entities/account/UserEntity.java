package org.example.DAL.entities.account;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.example.DAL.entities.BaseEntity;

import java.util.ArrayList;
import java.util.List;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name="tbl_users")
public class UserEntity extends BaseEntity {
    private String email;
    private String image;
    private String firstname;
    private String lastname;
    private String phoneNumber;
    private String passwordHash;
    private boolean isGoogle = false;
    @OneToMany(mappedBy = "user")
    private List<UserRoleEntity> userRoles = new ArrayList<>();
}
