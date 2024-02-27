package org.example.DAL.entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.example.DAL.entities.account.UserEntity;

import java.util.List;

@Data
@Entity
@Table(name="tbl_comments")
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class CommentEntity extends BaseEntity {
    private String title;
    private String message;
    private int stars;
    private int likes;
    private int dislikes;

    @ManyToOne
    @JoinColumn(name="user_id", nullable = false)
    private UserEntity user;

    @ManyToOne
    @JoinColumn(name="product_id", nullable = false)
    private ProductEntity product;

    @OneToMany(mappedBy = "comment", cascade = CascadeType.ALL)
    private List<CommentImageEntity> commentImages;
}
