package org.example.DAL.repositories;

import org.example.DAL.entities.CommentImageEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ICommentImageRepository extends JpaRepository<CommentImageEntity, Integer> {
}
