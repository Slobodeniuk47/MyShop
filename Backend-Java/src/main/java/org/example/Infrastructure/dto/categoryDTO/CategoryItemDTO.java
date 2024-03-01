package org.example.Infrastructure.dto.categoryDTO;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Data;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;

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
    @JsonProperty("Subcategories")
    private List<CategoryItemDTO> subcategories;
    @JsonProperty("Products")
    private List<ProductItemDTO> products;
}
