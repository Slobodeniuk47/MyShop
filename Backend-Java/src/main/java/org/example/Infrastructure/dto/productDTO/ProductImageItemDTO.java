package org.example.Infrastructure.dto.productDTO;

import lombok.Data;

@Data
public class ProductImageItemDTO {
    private int id;
    private String name;
    private String dateCreated;
    private String dateUpdated;
    private int productId;
    private String productName;
}
