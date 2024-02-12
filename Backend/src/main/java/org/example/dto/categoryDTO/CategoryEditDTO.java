package org.example.dto.categoryDTO;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;
@Data
public class CategoryEditDTO {
    private int id;
    private String name;
    private MultipartFile image;
    private String description;
    private Integer parentId;
}
