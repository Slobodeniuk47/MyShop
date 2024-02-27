package org.example.DAL.repositories;

import org.example.DAL.entities.ProductImageEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IProductImageRepository extends JpaRepository<ProductImageEntity, Integer> {
    ProductImageEntity findByName(String name);
}
