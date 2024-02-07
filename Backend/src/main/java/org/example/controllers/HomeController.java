package org.example.controllers;

import lombok.RequiredArgsConstructor;
import org.example.entities.CategoryEntity;
import org.example.repositories.CategoryRepository;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
@RestController
@RequiredArgsConstructor //auto create constructor with params
public class HomeController {
    private final CategoryRepository _categoryRepository;

    @RequestMapping("/") //Map on the path
    public List<CategoryEntity> Index()
    {
        return _categoryRepository.findAll();
    }
}
