package org.example.DAL.entities;

import jakarta.persistence.GeneratedValue;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Table(name="tbl_product_images")
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class ProductImageEntity extends BaseEntity {
    @ManyToOne
    @JoinColumn(name="product_id", nullable = false)
    private ProductEntity product;
}
