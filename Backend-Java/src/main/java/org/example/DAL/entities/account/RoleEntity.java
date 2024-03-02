package org.example.DAL.entities.account;

import jakarta.persistence.Entity;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;
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
@Table(name="tbl_roles")
public class RoleEntity extends BaseEntity {
    @OneToMany(mappedBy = "role")
    private List<UserRoleEntity> userRoles = new ArrayList<>();
}
