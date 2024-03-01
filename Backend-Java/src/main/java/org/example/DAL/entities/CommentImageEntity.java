package org.example.DAL.entities;

import jakarta.persistence.Entity;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Table(name="tbl_comment_images")
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class CommentImageEntity extends BaseEntity{
    @ManyToOne
    @JoinColumn(name="comment_id", nullable = false)
    private CommentEntity comment;
}
