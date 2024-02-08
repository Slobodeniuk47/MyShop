package org.example.dto.categoryDTO;

import org.springframework.web.multipart.MultipartFile;

public class CategoryCreateDTO {
    public String Name;
    public MultipartFile Image;
    public String Description;
    public int ParentId;
}
