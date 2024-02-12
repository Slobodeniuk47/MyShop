package org.example.entities;

import jakarta.persistence.*;
import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
@Entity
@Table(name="tbl_users")
public class UserEntity extends BaseEntity{
    private String email;
    private String firstname;
    private String lastname;
    private String phoneNumber;
    private String passwordHash;
    @OneToMany(mappedBy = "user")
    private List<UserRoleEntity> userRoles = new ArrayList<>();
}
