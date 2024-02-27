package org.example.Infrastructure.dto.accountDTO;

import lombok.*;
import org.springframework.web.multipart.MultipartFile;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
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
