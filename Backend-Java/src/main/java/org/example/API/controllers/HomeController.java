package org.example.API.controllers;

import lombok.RequiredArgsConstructor;
import org.example.DAL.repositories.ICategoryRepository;
import org.example.Infrastructure.storage.IStorageService;
import org.springframework.core.io.Resource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;

@RestController
@RequiredArgsConstructor //auto create constructor with params
public class HomeController {
    private final ICategoryRepository _categoryRepository;
    private final IStorageService _storageService;
    @GetMapping("/images/{filename}")
    @ResponseBody
    public ResponseEntity<Resource> serveFile(@PathVariable("filename") String filename) throws Exception {

        Resource file = _storageService.loadAsResource(filename);
        String urlFileName =  URLEncoder.encode("webImg.jpg", StandardCharsets.UTF_8.toString());
        return ResponseEntity.ok()
                //.header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + file.getFilename() + "\"").body(file);
                .contentType(MediaType.IMAGE_JPEG)

                .header(HttpHeaders.CONTENT_DISPOSITION,"filename=\""+urlFileName+"\"")
                .body(file);
    }
}
