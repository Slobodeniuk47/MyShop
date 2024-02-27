package org.example.Infrastructure.dto.commentDTO;

import lombok.Data;
import org.example.Infrastructure.dto.accountDTO.UserItemDTO;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;
@Data
public class CommentItemDTO {
    private int id;
    private String title;
    private String message;
    private int stars;
    private String dateCreated;
    private String dateUpdated;
    private int likes;
    private int dislikes;
    private int userId;
    private String userName;
    private UserItemDTO user;
    private Integer productId;
    private List<CommentImageItemDTO> images;
}
