package org.example;

import io.swagger.v3.oas.annotations.enums.SecuritySchemeIn;
import io.swagger.v3.oas.annotations.enums.SecuritySchemeType;
import io.swagger.v3.oas.annotations.security.SecurityScheme;
import org.example.DAL.constants.Path;
import org.example.Infrastructure.services.SeedService;
import org.example.Infrastructure.storage.StorageProperties;
import org.example.Infrastructure.storage.StorageService;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;

@EnableConfigurationProperties(StorageProperties.class)
@SpringBootApplication
@SecurityScheme(name="my-api", scheme = "bearer", type = SecuritySchemeType.HTTP, in= SecuritySchemeIn.HEADER)
public class Main {
    public static void main(String[] args) {
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
                System.out.println("[Path.class] "+Path.ApiURL);
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