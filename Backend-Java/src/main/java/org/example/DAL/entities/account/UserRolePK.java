package org.example.DAL.entities.account;

import lombok.Data;
import org.example.DAL.entities.account.RoleEntity;
import org.example.DAL.entities.account.UserEntity;

import java.io.Serializable;

@Data
public class UserRolePK implements Serializable {
    private UserEntity user;
    private RoleEntity role;
}
