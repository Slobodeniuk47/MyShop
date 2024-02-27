package org.example.API.controllers;

import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.DAL.repositories.IProductImageRepository;
import org.example.DAL.repositories.IProductRepository;
import org.example.Infrastructure.dto.productDTO.ProductCreateDTO;
import org.example.Infrastructure.dto.productDTO.ProductEditDTO;
import org.example.Infrastructure.interfaces.IProductService;
import org.example.Infrastructure.services.ResponseService;
import org.example.Infrastructure.storage.IStorageService;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

@RestController
@SecurityRequirement(name="my-api")
@RequiredArgsConstructor //auto create constructor with params
@RequestMapping("api/product") //Map on the path
public class ProductController {
    private final IProductService _productService;

    @GetMapping("get")
    public ResponseService getAll()
    {
        var result = _productService.getAll();
        return new ResponseService(result, HttpStatus.FOUND);
    }
    @GetMapping("get/{id}")
    public ResponseService getById(@PathVariable("id") int id)
    {
        var result = _productService.getById(id);
        return new ResponseService(result, HttpStatus.FOUND);
    }

    @PostMapping( path="create", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService create(@Valid @ModelAttribute ProductCreateDTO model) {
        var result = _productService.create(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @PutMapping( path="edit", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService edit(@Valid @ModelAttribute ProductEditDTO model) {
        var result = _productService.edit(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @DeleteMapping( path="delete/{id}")
    public void delete(@PathVariable("id") int id) {
        _productService.deleteById(id);
    }
}
