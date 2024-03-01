package org.example.API.controllers;

import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.Infrastructure.interfaces.IServices.ICategoryService;
import org.example.Infrastructure.dto.categoryDTO.CategoryCreateDTO;
import org.example.Infrastructure.dto.categoryDTO.CategoryEditDTO;
import org.example.Infrastructure.services.ResponseService;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@SecurityRequirement(name="my-api")
@RequiredArgsConstructor //auto create constructor with params
@RequestMapping("api/category") //Map on the path
public class CategoryController {
    private final ICategoryService _categoryService;

    @GetMapping("getMainCategories")
    public ResponseService getMainCategories()
    {
        var result = _categoryService.getMainCategories();
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @GetMapping("get")
    public ResponseService getAll()
    {
        var result = _categoryService.getAll();
        return new ResponseService(result, HttpStatus.FOUND);
    }
    @GetMapping("get/{id}")
    public ResponseService getById(@PathVariable("id") int id)
    {
        var result = _categoryService.getById(id);
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @PostMapping( path="create", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService create(@Valid @ModelAttribute CategoryCreateDTO model) {
        var result = _categoryService.create(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @PutMapping( path="edit", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService edit(@Valid @ModelAttribute CategoryEditDTO model) {
        var result = _categoryService.edit(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @DeleteMapping( path="delete/{id}")
    public void delete(@PathVariable("id") int id) {
        _categoryService.deleteById(id);
    }
}
