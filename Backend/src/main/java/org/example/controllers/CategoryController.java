package org.example.controllers;

import lombok.RequiredArgsConstructor;
import org.example.dto.categoryDTO.CategoryItemDTO;
import org.example.entities.CategoryEntity;
import org.example.repositories.CategoryRepository;
import org.springframework.http.HttpStatusCode;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequiredArgsConstructor //auto create constructor with params
@RequestMapping("api/categories") //Map on the path
public class CategoryController {
    private final CategoryRepository _categoryRepository;
    @GetMapping("get")
    public List<CategoryEntity> Index()
    {
        return _categoryRepository.findAll();
    }
}
