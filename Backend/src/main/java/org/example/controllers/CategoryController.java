package org.example.controllers;

import org.example.dto.categoryDTO.CategoryItemDTO;
import org.springframework.http.HttpStatusCode;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
public class CategoryController {

    //@RequestMapping("api/categories") //Map on the path
    //public ResponseEntity<List<CategoryItemDTO>> Index() {
    @RequestMapping("api/categories")
    public String index() {
        return "Category Index";
    }
}
