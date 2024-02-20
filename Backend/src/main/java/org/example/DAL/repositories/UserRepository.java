package org.example.DAL.repositories;

import org.example.DAL.entities.UserEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<UserEntity, Integer>
        //,JpaSpecificationExecutor<UserEntity> //commented
{
    //UserEntity findByEmail(String email); //commented
    Optional<UserEntity> findByEmail(String email);
}
