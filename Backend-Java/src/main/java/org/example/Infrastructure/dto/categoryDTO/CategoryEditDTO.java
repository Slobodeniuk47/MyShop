package org.example.Infrastructure.dto.categoryDTO;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;
@Data
public class CategoryEditDTO {
    private int id;
    private String name;
    private MultipartFile imageUpload;
    private String description;
    private Integer parentId;
}
