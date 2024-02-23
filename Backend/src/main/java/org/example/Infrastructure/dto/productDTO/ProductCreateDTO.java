package org.example.Infrastructure.dto.productDTO;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

@Data
public class ProductCreateDTO {
    private String name;
    private String description;
    private double price;
    private List<MultipartFile> images;
    private int categoryId;
}