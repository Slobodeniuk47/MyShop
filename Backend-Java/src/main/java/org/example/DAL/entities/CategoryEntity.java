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
@Table(name = "tbl_Categories")
public class CategoryEntity extends BaseEntity
{
    private String description;
    private String image;
    @ManyToOne
    @JoinColumn(name="parent_Id", nullable = true)
    private CategoryEntity parent;

    @OneToMany(mappedBy = "category", cascade = CascadeType.ALL)
    List<ProductEntity> products;
}
