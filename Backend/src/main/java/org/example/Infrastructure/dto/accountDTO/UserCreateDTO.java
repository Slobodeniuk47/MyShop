package org.example.Infrastructure.dto.accountDTO;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class UserCreateDTO {
    private String email;
    private String firstname;
    private String lastname;
    private MultipartFile image;
    private String phoneNumber;
    private String password;
    private String confirmPassword;
    private String role;
}
