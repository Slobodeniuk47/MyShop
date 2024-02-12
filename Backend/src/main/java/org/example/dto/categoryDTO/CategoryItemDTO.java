package org.example.dto.categoryDTO;

import com.fasterxml.jackson.annotation.JsonIgnore;
import lombok.Data;

import java.io.Serializable;
import java.util.List;
@Data
public class CategoryItemDTO implements Serializable {
    private int id;
    private String name;
    private String image;
    private String description;
    private String dateCreated;
    private String dateUpdated;
    private Integer parentId;
    private String parentName;
    private Integer countSubategories;
    private Integer countProducts;

    //@JsonIgnore
    //public List<CategoryItemDTO> Subcategories;

    //@JsonIgnore
    //public List<ProductItemDTO> Products;
}
