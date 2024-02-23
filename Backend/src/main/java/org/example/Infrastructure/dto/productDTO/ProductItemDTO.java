package org.example.Infrastructure.dto.productDTO;

import lombok.Data;

import java.util.List;

@Data
public class ProductItemDTO {
    private int id;
    private String name;
    private String description;
    private String dateCreated;
    private String dateUpdated;
    private double price;
    private List<ProductImageItemDTO> images;
    private int categoryId;
    private String categoryName;
    //comments
    //numberOfComments
    //stars (by comment)
}
