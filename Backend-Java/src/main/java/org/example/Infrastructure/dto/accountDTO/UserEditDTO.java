package org.example.Infrastructure.dto.accountDTO;

import jakarta.annotation.Nullable;
import jakarta.validation.constraints.Null;
import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class UserEditDTO {
    private int id;
    private String email;
    private String firstname;
    private String lastname;
    private MultipartFile imageUpload;
    private String phoneNumber;
    private String role;
}
