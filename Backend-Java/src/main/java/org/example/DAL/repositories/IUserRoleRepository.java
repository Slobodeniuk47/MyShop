package org.example.DAL.repositories;

import org.example.DAL.entities.account.UserEntity;
import org.example.DAL.entities.account.UserRoleEntity;
import org.example.DAL.entities.account.UserRolePK;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface IUserRoleRepository extends JpaRepository<UserRoleEntity, UserRolePK> {
    List<UserRoleEntity> findByUser(UserEntity User);
}
