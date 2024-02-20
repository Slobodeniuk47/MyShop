package org.example.Infrastructure.storage;

import net.coobird.thumbnailator.Thumbnails;
import org.springframework.core.io.Resource;
import org.springframework.core.io.UrlResource;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.*;
import java.net.MalformedURLException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Base64;
import java.util.UUID;

@Service
public class FileSystemStorageService implements StorageService {
    private final Path rootLocation;
    int [] imageSizes = {1920};//{32, 150, 300, 600, 1200};//array of photo sizes
    public FileSystemStorageService(StorageProperties properties) {
        this.rootLocation = Paths.get(properties.getLocation());
    }
    @Override
    public void init() {
        try {
            if(!Files.exists(rootLocation))
                Files.createDirectories(rootLocation);
        }catch (IOException e) {
            throw new StorageException("Folder creation error", e);
        }
    }

    @Override
    public Resource loadAsResource(String filename) {
        try {
            Path file = rootLocation.resolve(filename);
            Resource resource = new UrlResource(file.toUri());
            if(resource.exists() || resource.isReadable())
                return resource;
            else
                throw new StorageException("Prblems reading the file: "+filename);
        } catch(MalformedURLException e) {
            throw new StorageException("File is not found: "+ filename, e);
        }
    }

    @Override
    public String save(String base64) {
        try {
            if(base64.isEmpty()) {
                throw new StorageException("Empty base64");
            }
            UUID uuid = UUID.randomUUID();
            String [] charArray = base64.split(","); //divide the image code into two parts, separate the extensions
            String extension;
            System.out.println("-----------------"+ charArray[0]);
            switch (charArray[0]) {//check image's extension
                case "data:image/png;base64":
                    extension = "png";
                    break;
                default://should write cases for more images types
                    extension = "jpg";
                    break;
            }
            String randomFileName = uuid.toString()+"."+extension; //name the file: unique name + extension
            java.util.Base64.Decoder decoder = Base64.getDecoder(); //we create an instance of the decoder
            byte[] bytes = new byte[0]; //we create an array of bytes
            bytes = decoder.decode(charArray[1]); // decode Base64 to Bytes
            //int [] imageSize = imageSizes; //array of photo sizes
            try (var byteStream = new ByteArrayInputStream(bytes)) {
                var image = ImageIO.read(byteStream);
                for(int size : imageSizes){ // in the cycle we create photos of each size
                    String directory= rootLocation.toString() +"/"+size+"_"+randomFileName; //create a folder where the photo will be saved
// My Example
//create a buffer for a new photo, where it is important to specify the extension that the photo will have and the size (32x32, 150x150)
                    //RAM
                    BufferedImage newImg = ImageUtils.resizeImage(image,
                            extension=="jpg"? ImageUtils.IMAGE_JPEG : ImageUtils.IMAGE_PNG, size,size);
                    ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream(); //Stream creation
                    //the photo is written to the stream to receive an array of bytes
                    ImageIO.write(newImg, extension, byteArrayOutputStream); //with the help of this Stream, we write a photo in the buffer according to the extension
                    byte [] newBytes = byteArrayOutputStream.toByteArray(); //from this Stream we get bytes again
                    FileOutputStream out = new FileOutputStream(directory);
                    out.write(newBytes); //we save the bytes to the file system on the server
                    out.close();
                }
            }

            return randomFileName;
        } catch (IOException e) {
            throw new StorageException("Base64 conversion and save problem", e);
        }
    }

    @Override
    public  void  removeFile(String removeFile){
        if(imageSizes.length > 1) {
            for (int size : imageSizes) {
                Path filePath = load(size+"_"+removeFile);
                File file = new File(filePath.toString());
                if (file.delete()) {
                    System.out.println(removeFile + "The file has been deleted.");
                } else System.out.println(removeFile + "The file is not found.");
            }
        }
        else {
            Path filePath = load(removeFile);
            File file = new File(filePath.toString());
            if (file.delete()) {
                System.out.println(removeFile + "The file has been deleted.");
            } else System.out.println(removeFile + "The file is not found.");
        }

    }

    @Override
    public Path load(String filename) {
        return rootLocation.resolve(filename);
    }

    @Override
    public String saveMultipartFile(MultipartFile file) {
        try {
            String extension="jpg";
            UUID uuid = UUID.randomUUID();
            String randomFileName = uuid.toString()+"."+extension; //we name the files: unique name + extension
            byte[] bytes = new byte[0]; // creating a byte array
            bytes = file.getBytes(); // we take the bytes from the file and convert them into a photo, the size we need
            //int [] imageSize = imageSizes; // array of photo sizes
            try (var byteStream = new ByteArrayInputStream(bytes)) {
                var image = ImageIO.read(byteStream);
                for(int size : imageSizes){ // in the cycle we create photos of each size
                    String directory;
                    if(imageSizes.length > 1) {
                        directory = rootLocation.toString() + "/" + size + "_" + randomFileName; //create a folder where the photo will be saved
                    }
                    else {
                        directory = rootLocation.toString() + "/"+ randomFileName; //create a folder where the photo will be saved
                    }
// My Example
//create a buffer for a new photo, where it is important to specify the extension that the photo will have and the size (32x32, 150x150)
                    //RAM
                    BufferedImage newImg = ImageUtils.resizeImage(image,
                            extension=="jpg"? ImageUtils.IMAGE_JPEG : ImageUtils.IMAGE_PNG, size,size);
                    ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream(); //Stream creation
                    //the photo is written to the stream to receive an array of bytes
                    ImageIO.write(newImg, extension, byteArrayOutputStream); //with the help of this Stream, we write a photo in the buffer according to the extension
                    byte [] newBytes = byteArrayOutputStream.toByteArray(); //from this Stream we get bytes again
                    FileOutputStream out = new FileOutputStream(directory);
                    out.write(newBytes); //we save the bytes to the file system on the server
                    out.close();
                }
            }

            return randomFileName;
        } catch (IOException e) {
            throw new StorageException("Base64 conversion and save problem", e);
        }
    }

    @Override
    public String saveByFormat(MultipartFile file, FileSaveFormat format) {
        try {
            String extension = format.name().toLowerCase();
            String randomFileName = UUID.randomUUID().toString()+"."+extension;
            //int [] imageSize = imageSizes; //array of photo sizes, the size of the array depends on the number of photos created
            BufferedImage bufferedImage = ImageIO.read(new ByteArrayInputStream(file.getBytes()));
            for(int size : imageSizes) {
                String fileOutputSave;
                if(imageSizes.length > 1) {
                    fileOutputSave = rootLocation.toString() + "/" + size + "_" + randomFileName; //create a folder where the photo will be saved
                }
                else {
                    fileOutputSave = rootLocation.toString() + "/"+ randomFileName; //create a folder where the photo will be saved
                }
                Thumbnails.of(bufferedImage)
                        .size(size, size)
                        .outputFormat(extension)
                        .toFile(fileOutputSave);
            }
            return randomFileName;
        }catch (IOException e) {
            throw new StorageException("Photo conversion problem", e);
        }
    }
}
