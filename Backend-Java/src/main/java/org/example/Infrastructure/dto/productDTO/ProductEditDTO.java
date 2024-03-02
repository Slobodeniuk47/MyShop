package org.example.Infrastructure.dto.productDTO;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

@Data
public class ProductEditDTO {
    private int id;
    private String name;
    private String description;
    private double price;
    private List<MultipartFile> imagesUpload;
    private int categoryId;
}
