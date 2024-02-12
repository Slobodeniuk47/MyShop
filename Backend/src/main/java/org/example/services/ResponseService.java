package org.example.services;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.http.HttpStatus;

import java.util.List;

@Data
public class ResponseService {
    private String accessToken  = null;
    private String refreshToken  = null;
    private String message;
    private Object payload;
    private HttpStatus status;
    private List<String> errors;
    public ResponseService() {}
    public ResponseService(Object payload) {
        this.payload = payload;
    }
    public ResponseService(Object payload, HttpStatus status) {
        this.status = status;
        this.payload = payload;
    }
    public ResponseService(String message, HttpStatus status) {
        this.message = message;
        this.status = status;
    }
}
