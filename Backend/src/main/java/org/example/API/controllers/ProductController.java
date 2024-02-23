package org.example.API.controllers;

import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.apache.logging.log4j.message.ThreadDumpMessage;
import org.example.DAL.entities.ProductImageEntity;
import org.example.DAL.repositories.ProductImageRepository;
import org.example.DAL.repositories.ProductRepository;
import org.example.Infrastructure.dto.productDTO.ProductCreateDTO;
import org.example.Infrastructure.dto.productDTO.ProductEditDTO;
import org.example.Infrastructure.interfaces.IProductService;
import org.example.Infrastructure.services.ResponseService;
import org.example.Infrastructure.storage.StorageService;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

@RestController
@SecurityRequirement(name="my-api")
@RequiredArgsConstructor //auto create constructor with params
@RequestMapping("api/product") //Map on the path
public class ProductController {
    private final IProductService _productService;

    private final ProductRepository _productRepository;
    private final ProductImageRepository _productImageRepository;
    private final StorageService _storageService;

    @DeleteMapping("testRemoveImagesByProductId")//Working!!!!
    public void removeImageByProductId(int id) {
        var productOptional = _productRepository.getById(id);
        for (var img : _productImageRepository.findAll()){
            if(img.getProduct().getId() == productOptional.getId()){
                _productImageRepository.delete(img);
            }
        }
    }
    @DeleteMapping("testRemoveImage")
    public void removeImage(int id) {
        _productImageRepository.deleteById(id);
    }
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
