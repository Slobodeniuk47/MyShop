package org.example.DAL.repositories;

import org.example.DAL.entities.UserEntity;
import org.example.DAL.entities.UserRoleEntity;
import org.example.DAL.entities.UserRolePK;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface UserRoleRepository extends JpaRepository<UserRoleEntity, UserRolePK> {
    List<UserRoleEntity> findByUser(UserEntity User);
}
