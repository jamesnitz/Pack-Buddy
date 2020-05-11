using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PackBuddy.Models
{
    public class COPYJSON
    {
        public class RootGear
        {
            public int Count { get; set; }
            public int Page { get; set; }
            public int PerPage { get; set; }
            public string NextPageUrl { get; set; }
            public string LastPageUrl { get; set; }
            public string RefinementsUrl { get; set; }
            public string Title { get; set; }
            public List<Results> Result { get; set; }
        }
        
        public class Response
        {
            public Results Result { get; set; }
        }
        public class Results
        {
            public string Id { get; set; }
            public bool IsClearance { get; set; }
            public bool IsNew { get; set; }
            public string Url { get; set; }
            public string WebUrl { get; set; }
            public string AffiliateWebUrl { get; set; }
            public Reviews Reviews { get; set; }
            public string NameWithoutBrand { get; set; }
            public string Name { get; set; }
            public bool IsFamousBrand { get; set; }
            public Images Images { get; set; }
            public Sizesavailable SizesAvailable { get; set; }
            public List<Color> Colors { get; set; }
            public string DescriptionHtmlSimple { get; set; }
            public float SuggestedRetailPrice { get; set; }
            public Brand Brand { get; set; }
            public float ListPrice { get; set; }
            public float FinalPrice { get; set; }
            public bool Favorite { get; set; }
        }

        public class Reviews
        {
            public string ReviewsUrl { get; set; }
            public int ReviewCount { get; set; }
            public float AverageRating { get; set; }
        }

        public class Images
        {
            public string PrimarySmall { get; set; }
            public string PrimaryMedium { get; set; }
            public string PrimaryLarge { get; set; }
            public string PrimaryExtraLarge { get; set; }
            public Extraimage[] ExtraImages { get; set; }
        }

        public class Extraimage
        {
            public string Title { get; set; }
            public string Src { get; set; }
        }

        public class Sizesavailable
        {
        }

        public class Brand
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string ProductsUrl { get; set; }
            public string LogoSrc { get; set; }
            public string Name { get; set; }
        }

        public class Color
        {
            public string ColorCode { get; set; }
            public string ColorName { get; set; }
            public string ColorChipImageSrc { get; set; }
            public string ColorPreviewImageSrc { get; set; }
        }

    }

}

