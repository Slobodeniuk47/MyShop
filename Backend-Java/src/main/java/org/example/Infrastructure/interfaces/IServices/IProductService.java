package org.example.Infrastructure.interfaces.IServices;

import org.example.Infrastructure.dto.productDTO.ProductCreateDTO;
import org.example.Infrastructure.dto.productDTO.ProductEditDTO;
import org.example.Infrastructure.dto.productDTO.ProductItemDTO;

import java.util.List;

public interface IProductService {
    ProductItemDTO create(ProductCreateDTO model);
    List<ProductItemDTO> getAll();
    ProductItemDTO edit(ProductEditDTO model);
    void deleteById(int id);
    ProductItemDTO getById(int id);
}
