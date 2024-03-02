package org.example.Infrastructure.dto.commentDTO;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

@Data
public class CommentCreateDTO {
    private String title;
    private String message;
    private int stars;
    private int userId;
    private Integer productId;
    private List<MultipartFile> images;
}
