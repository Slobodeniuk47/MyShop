package org.example.Infrastructure.dto.categoryDTO;

import jakarta.validation.constraints.Null;
import lombok.Data;
import org.springframework.lang.Nullable;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;
import java.util.OptionalInt;

@Data //lombok {get; set;}
public class CategoryCreateDTO {
    private String name;
    private MultipartFile image;
    private String description;
    private Integer parentId;
}
