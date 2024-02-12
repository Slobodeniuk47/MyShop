package org.example;

import io.swagger.v3.oas.annotations.enums.SecuritySchemeIn;
import io.swagger.v3.oas.annotations.enums.SecuritySchemeType;
import io.swagger.v3.oas.annotations.security.SecurityScheme;
import org.example.entities.CategoryEntity;
import org.example.repositories.CategoryRepository;
import org.example.services.SeedService;
import org.example.storage.StorageProperties;
import org.example.storage.StorageService;
import org.hibernate.mapping.Collection;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;

import java.util.Collections;
import java.util.Objects;

@EnableConfigurationProperties(StorageProperties.class)
@SpringBootApplication
@SecurityScheme(name="my-api", scheme = "bearer", type = SecuritySchemeType.HTTP, in= SecuritySchemeIn.HEADER)
public class Main {
    public static void main(String[] args) {
        System.out.printf("1");
        System.out.printf("Started by Slobodeniuk Artem");

        SpringApplication.run(Main.class, args);
    }
    @Value("${my.custom.property}")
    String customProperty;
    @Value("${my.custom.property.id}")
    String customPropertyId;
    @Bean
    CommandLineRunner init(StorageService storageService, SeedService seedService) {
        return(args -> {
            try {
                System.out.println(customProperty + customPropertyId);
                seedService.seedCategoriesData();
                seedService.seedRolesData();
                seedService.seedUsersData();
                storageService.init();
            }
            catch (Exception ex) {
                System.out.println("---------- Something went wrong ------------"+ ex.getMessage());
            }
        });
    }
}