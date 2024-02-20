package org.example.DAL.entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

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
    private String imageUrl;
    //private int parentId;
    @ManyToOne
    @JoinColumn(name="parent_Id", nullable = true)
    private CategoryEntity parent;
//    @OneToMany(mappedBy = "category")
//    private List<CategoryEntity> questionItems;
//    public CategoryEntity() {
//        questionItems = new ArrayList<>();
//    }
    //@OneToMany(mappedBy = "categories", cascade = CascadeType.ALL)
    //private List<CategoryEntity> subcategories;
}
