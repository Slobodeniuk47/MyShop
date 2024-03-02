namespace Data.MyShop.Constants
{
    public static class DirectoriesInProject
    {
        public static List<string> All = new()
        {
            Images,
            UserImages,
            CategoryImages,
            ProductImages,
            CommentImages,
            CompanyImages
        };
        public static string Api = "https://localhost:7230";
        public static string Images = "Images";
        public static string UserImages = "Images/userImages";
        public static string CategoryImages = "Images/categoryImages";
        public static string ProductImages = "Images/productImages";
        public static string CommentImages = "Images/commentImages";
        public static string CompanyImages = "Images/companyImages";
    }
}
