package org.example.Infrastructure.services;

import lombok.AllArgsConstructor;
import org.example.DAL.entities.ProductEntity;
import org.example.DAL.entities.ProductImageEntity;
import org.example.DAL.repositories.CategoryRepository;
import org.example.DAL.repositories.ProductImageRepository;
import org.example.DAL.repositories.ProductRepository;
import org.example.Infrastructure.dto.productDTO.ProductCreateDTO;
import org.example.Infrastructure.dto.productDTO.ProductEditDTO;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;
import org.example.Infrastructure.interfaces.IProductService;
import org.example.Infrastructure.mappers.IProductMapper;
import org.example.Infrastructure.storage.StorageService;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@Service
@AllArgsConstructor
public class ProductService implements IProductService {
    private final CategoryRepository _categoryRepository;
    private final ProductRepository _productRepository;
    private final ProductImageRepository _productImageRepository;
    private final StorageService _storageService;
    private final IProductMapper _productMapper;
    @Override
    public ProductItemDTO create(ProductCreateDTO model) {
        var p = new ProductEntity();
        p.setName(model.getName());
        p.setPrice(model.getPrice());
        p.setDescription(model.getDescription());
        p.setCategory(_categoryRepository.getById(model.getCategoryId()));
        _productRepository.save(p);
        for (var img : model.getImages()) {
            var file = _storageService.saveMultipartFile(img);
            ProductImageEntity pi = new ProductImageEntity();
            pi.setName(file);
            pi.setProduct(p);
            _productImageRepository.save(pi);
        }
        return null;
    }

    @Override
    public List<ProductItemDTO> getAll() {
        var products = _productRepository.findAll();
        return _productRepository.saveAll(products)
                .stream()
                .map(_productMapper::ProductItemDTOByProductEntity)
                .collect(Collectors.toList());
    }

    @Override
    public ProductItemDTO getById(int id) {
        var productOptional = _productRepository.findById(id);
        var result = _productMapper.ProductItemDTOByProductEntity(productOptional.get());
        if(productOptional.isPresent())
        {
            return result;
        }
        return null;
    }

    @Override
    public void deleteById(int id) {
        ProductEntity product = _productRepository.findById(id).get();
        for(var img : product.getProductImages()) {
            _storageService.removeFile(img.getName());
        }
        _productRepository.deleteById(id);
    }

    @Override
    public ProductItemDTO edit(ProductEditDTO model) {
        var productOptional = _productRepository.findById(model.getId());
        if(productOptional.isPresent()) {
            var p = productOptional.get();
            p.setName(model.getName());
            p.setPrice(model.getPrice());
            p.setDescription(model.getDescription());
            p.setCategory(_categoryRepository.getById(model.getCategoryId()));
            _productRepository.save(p);

            if(!Objects.isNull(model.getImagesUpload())) {
                //Delete photos
                for (ProductImageEntity existingImage : _productImageRepository.findAll()) {
                    if(existingImage.getProduct().getId() == productOptional.get().getId()) {
                        _storageService.removeFile(existingImage.getName());
                        _productImageRepository.delete(existingImage);
                    }
                }
                //Save photos
                for (var img : model.getImagesUpload()) {
                    var file = _storageService.saveMultipartFile(img);
                    ProductImageEntity pi = new ProductImageEntity();
                    pi.setName(file);
                    pi.setProduct(p);
                    _productImageRepository.save(pi);
                }
            }
        }
        return null;
    }

}
