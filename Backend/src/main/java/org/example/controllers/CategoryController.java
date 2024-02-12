package org.example.controllers;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.interfaces.ICategoryService;
import org.example.dto.categoryDTO.CategoryCreateDTO;
import org.example.dto.categoryDTO.CategoryEditDTO;
import org.example.dto.categoryDTO.CategoryItemDTO;
import org.example.mappers.ApplicationMapper;
import org.example.repositories.CategoryRepository;
import org.example.services.ResponseService;
import org.example.storage.StorageService;
import org.hibernate.Remove;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor //auto create constructor with params
@RequestMapping("api/categories") //Map on the path
public class CategoryController {
    private final ICategoryService _categoryService;

    @GetMapping("getTest")
    public ResponseService getTest()
    {
        var result = _categoryService.getAll();
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @GetMapping("get")
    public ResponseService getAll()
    {
        var result = _categoryService.getAll();
        return new ResponseService(result, HttpStatus.FOUND);
    }
    @GetMapping("get/{id}")
    public ResponseService getById(int id)
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
    public void delete( int id) {
        _categoryService.deleteById(id);
    }
}
