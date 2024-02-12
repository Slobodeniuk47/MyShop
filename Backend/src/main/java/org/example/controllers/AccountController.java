package org.example.controllers;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.dto.accountDTO.AuthResponseDTO;
import org.example.dto.accountDTO.LoginDTO;
import org.example.dto.accountDTO.RegisterDTO;
import org.example.mappers.ApplicationMapper;
import org.example.services.AccountService;
import org.example.services.ResponseService;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/account")
@RequiredArgsConstructor
public class AccountController {
    private final AccountService _accountService;
    private final ApplicationMapper _appMapper;

    @PostMapping( path="login", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseEntity<AuthResponseDTO> login(@Valid @ModelAttribute LoginDTO model) {
        try {
            var auth = _accountService.login(model);
            return ResponseEntity.ok(auth);
        }
        catch(Exception ex) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).build();
        }
    }

    @GetMapping("get")
    public ResponseService getAll()
    {
        var result = _accountService.getAll();
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @GetMapping("get/{id}")
    public ResponseService getById(int id)
    {
        var result = _accountService.getById(id);
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @PostMapping( path="register", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService register(@Valid @ModelAttribute RegisterDTO model) {
        try {
            var result = _accountService.register(model);
            return new ResponseService(result);
        } catch (Exception ex) {
            return new ResponseService("Went something wrong!", HttpStatus.BAD_REQUEST);
        }
    }
}
