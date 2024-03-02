package org.example.DAL.entities.account;

import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name="tbl_user_roles")
@IdClass(UserRolePK.class)
public class UserRoleEntity {
    @Id
    @JsonIgnore//In this case saves from looping  |  Exception
    @ManyToOne
    @JoinColumn(name="user_id", nullable = false)
    private UserEntity user;
    @Id
    @JsonIgnore//In this case saves from looping  |  Exception
    @ManyToOne
    @JoinColumn(name="role_id", nullable = false)
    private RoleEntity role;
}
