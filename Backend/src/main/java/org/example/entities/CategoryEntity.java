package org.example.entities;

import jakarta.persistence.*;
import lombok.Data;
import org.springframework.lang.Nullable;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Data //lombok {get; set;}

@Table(name = "tbl_Categories")
@Entity
public class CategoryEntity extends BaseEntity
{
    private String description;
    private String image;
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
