package org.example.dto.categoryDTO;

import java.util.List;

public class CategoryItemDTO {
    public int Id;
    public String Name;
    public String Image;
    public String Description;
    public String DateCreated;
    public String DateUpdated;
    public int ParentId;
    public String ParentName;
    public int countSubategories;
    public int countProducts;
    public List<CategoryItemDTO> Subcategories;
    //public List<ProductItemDTO> Products;
}
