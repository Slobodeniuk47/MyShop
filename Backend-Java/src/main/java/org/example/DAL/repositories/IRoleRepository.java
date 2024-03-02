package org.example.DAL.repositories;

import org.example.DAL.entities.account.RoleEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IRoleRepository extends JpaRepository<RoleEntity, Integer> {
    RoleEntity findByName(String Name);
}
