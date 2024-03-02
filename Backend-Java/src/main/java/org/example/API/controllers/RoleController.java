package org.example.API.controllers;

import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import lombok.RequiredArgsConstructor;
import org.example.Infrastructure.dto.roleDTO.RoleCreateDTO;
import org.example.Infrastructure.dto.roleDTO.RoleEditDTO;
import org.example.Infrastructure.interfaces.IServices.IRoleService;
import org.example.Infrastructure.services.ResponseService;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@SecurityRequirement(name="my-api")
@RequestMapping("api/Role")
@RequiredArgsConstructor
public class RoleController {
    private final IRoleService _roleService;
    @GetMapping("get")
    public ResponseService getAll()
    {
        var result = _roleService.getAll();
        return new ResponseService(result, HttpStatus.FOUND);
    }
    @GetMapping("get/{id}")
    public ResponseService getById(@PathVariable("id") int id)
    {
        var result = _roleService.getById(id);
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @PostMapping( path="create", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService create(@ModelAttribute RoleCreateDTO model) {
        var result = _roleService.create(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @PutMapping( path="edit", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService edit(@ModelAttribute RoleEditDTO model) {
        var result = _roleService.edit(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @DeleteMapping( path="delete/{id}")
    public void delete(@PathVariable("id") int id) {
        _roleService.deleteById(id);
    }
}
