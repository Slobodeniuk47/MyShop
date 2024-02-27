package org.example.DAL.entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data //lombok {get; set;}
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "tbl_Products")
public class ProductEntity extends BaseEntity {
    private String description;
    private double price;
    @ManyToOne
    @JoinColumn(name="category_id", nullable = false)
    private CategoryEntity category;

    @OneToMany(mappedBy = "product", cascade = CascadeType.ALL)
    private List<ProductImageEntity> productImages;

    @OneToMany(mappedBy = "product", cascade = CascadeType.ALL)
    private List<CommentEntity> comments;
}
