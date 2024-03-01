package org.example.API.controllers;

import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.DAL.entities.CommentEntity;
import org.example.DAL.repositories.ICommentRepository;
import org.example.Infrastructure.dto.commentDTO.CommentCreateDTO;
import org.example.Infrastructure.dto.commentDTO.CommentEditDTO;
import org.example.Infrastructure.interfaces.IServices.ICommentService;
import org.example.Infrastructure.services.ResponseService;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@SecurityRequirement(name="my-api")
@RequiredArgsConstructor //auto create constructor with params
@RequestMapping("api/Comment") //Map on the path
public class CommentController {
    private final ICommentService _commentService;
    private final ICommentRepository _commentRepository;

    @GetMapping("testGet")
    public List<CommentEntity> getTestAll()
    {
        var result = _commentRepository.findAll();
        return result;
    }
    @GetMapping("testGetById/{id}")
    public CommentEntity getTestById(@PathVariable("id")int id)
    {
        var result = _commentRepository.getById(id);
        return result;
    }
    @GetMapping("get")
    public ResponseService getAll()
    {
        var result = _commentService.getAll();
        return new ResponseService(result, HttpStatus.FOUND);
    }
    @GetMapping("get/{id}")
    public ResponseService getById(@PathVariable("id") int id)
    {
        var result = _commentService.getById(id);
        return new ResponseService(result, HttpStatus.FOUND);
    }
    @GetMapping("GetCommentsByProductId/{id}")
    public ResponseService getCommentsByProductIdAsync(@PathVariable("id") int id)
    {
        var result = _commentService.getCommentsByProductIdAsync(id);
        return new ResponseService(result, HttpStatus.FOUND);
    }
    @PostMapping( path="create", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService create(@Valid @ModelAttribute CommentCreateDTO model) {
        var result = _commentService.create(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @PutMapping( path="edit", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseService edit(@Valid @ModelAttribute CommentEditDTO model) {
        var result = _commentService.edit(model);
        return new ResponseService(result, HttpStatus.CREATED);
    }

    @DeleteMapping( path="delete/{id}")
    public void delete(@PathVariable("id") int id) {
        _commentService.deleteById(id);
    }
}
