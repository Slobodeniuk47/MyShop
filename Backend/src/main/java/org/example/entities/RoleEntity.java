package org.example.entities;

import jakarta.persistence.Entity;
import jakarta.persistence.ManyToMany;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;
import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
@Entity
@Table(name="tbl_roles")
public class RoleEntity extends BaseEntity {
//    @ManyToMany(mappedBy = "roles")
//    private List<UserEntity> users;
//    public RoleEntity() {
//        users = new ArrayList<UserEntity>();
//    }
    @OneToMany(mappedBy = "role")
    private List<UserRoleEntity> userRoles = new ArrayList<>();
}
