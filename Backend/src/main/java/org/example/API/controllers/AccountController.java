package org.example.API.controllers;

import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.Infrastructure.dto.accountDTO.LoginDTO;
import org.example.Infrastructure.dto.accountDTO.UserCreateDTO;
import org.example.Infrastructure.dto.accountDTO.UserEditDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryEditDTO;
import org.example.Infrastructure.interfaces.IAccountService;
import org.example.Infrastructure.services.ResponseService;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@SecurityRequirement(name="my-api")
@RequestMapping("api/Account")
@RequiredArgsConstructor
public class AccountController {
    private final IAccountService _accountService;
    @PostMapping( path="login", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService login(@Valid @ModelAttribute LoginDTO model) {
        try {
            var auth = _accountService.login(model);
            return new ResponseService(auth, HttpStatus.ACCEPTED);
        }
        catch(Exception ex) {
            return new ResponseService(HttpStatus.UNAUTHORIZED);
        }
    }

    @GetMapping("get")
    public ResponseService getAll()
    {
        var result = _accountService.getAll();
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @GetMapping("get/{id}")
    public ResponseService getById(@PathVariable("id")int id)
    {
        var result = _accountService.getById(id);
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @PostMapping( path="register", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService register(@ModelAttribute UserCreateDTO model) {
        try {
            var result = _accountService.register(model);
            return new ResponseService(result);
        } catch (Exception ex) {
            return new ResponseService("Went something wrong!", HttpStatus.BAD_REQUEST);
        }
    }
    @PutMapping( path="edit", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService edit(@ModelAttribute UserEditDTO model) {
        var result = _accountService.edit(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }
    @DeleteMapping("delete/{id}")
    public ResponseService deleteById(int id)
    {
        _accountService.deleteById(id);
        return new ResponseService("The user has been deleted!", HttpStatus.FOUND);
    }
}
