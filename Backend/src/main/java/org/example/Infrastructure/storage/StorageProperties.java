package org.example.Infrastructure.storage;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.boot.context.properties.ConfigurationPropertiesScan;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.aspectj.EnableSpringConfigured;

//@ConfigurationProperties("storage")

@ConfigurationProperties("storage")
@Data
public class StorageProperties {
    private String location = "Images";
}
