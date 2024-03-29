package org.example.Infrastructure.storage;

import org.springframework.core.io.Resource;
import org.springframework.web.multipart.MultipartFile;

import java.nio.file.Path;

public interface IStorageService {
    void init();
    Resource loadAsResource(String filename);
    String save(String base64);

    void  removeFile(String removeFile);
    Path load(String filename);
    String saveMultipartFile(MultipartFile file);
    String saveByFormat(MultipartFile file, FileSaveFormat format);
}
