package org.example.Infrastructure.dto.accountDTO;

import lombok.*;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class ExternalLoginDTO {
    private String token;
    private String provider;
}
