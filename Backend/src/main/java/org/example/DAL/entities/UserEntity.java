package org.example.DAL.entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name="tbl_users")
public class UserEntity extends BaseEntity{
    private String email;
    private String image;
    private String imageURL;
    private String firstname;
    private String lastname;
    private String phoneNumber;
    private String passwordHash;
    @OneToMany(mappedBy = "user")
    private List<UserRoleEntity> userRoles = new ArrayList<>();
}
