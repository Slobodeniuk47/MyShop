package org.example.entities;

import jakarta.persistence.*;
import lombok.Data;

@Data //lombok
@Entity
@Table(name = "tbl_Categories")
public class CategoryEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    private String name;
    private String description;
    //public boolean IsDelete;

    private java.time.LocalDate DateCreated = java.time.LocalDate.now();
    private java.time.LocalDate DateUpdated = java.time.LocalDate.now();

    private String Image;
    //@ManyToOne
    //@JoinColumn(name="parentId", nullable = true)
    //public CategoryEntity Parent;

}
