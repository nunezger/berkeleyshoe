﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BerkeleyEntities;
using System.Globalization;
using BerkeleyEntities.Amazon;

namespace AmazonServices
{
    public abstract class ProductData
    {
        protected Item _item;

        public ProductData(Item item)
        {
            _item = item;
        }

        public string Sku 
        {
            get { return _item.ItemLookupCode; }
        }

        public virtual Product GetProductDto(string condition, string title)
        {
            ConditionInfo conditionInfo = new ConditionInfo();
            conditionInfo.ConditionType = ConditionType.New;

            StandardProductID sid = new StandardProductID();

            if (_item.GTINType.Equals("UPC"))
            {
                sid.Type = StandardProductIDType.UPC;
            }
            else
            {
                sid.Type = StandardProductIDType.EAN;
            }
            
            sid.Value = _item.GTIN;

            ProductDescriptionData descriptiondata = new ProductDescriptionData();
            descriptiondata.Brand = ToTitleCase(_item.SubDescription1);
            //descriptiondata.Description = this.FullDescription;
            
            descriptiondata.Title = title;

            Product product = new Product();
            product.SKU = _item.ItemLookupCode;
            product.StandardProductID = sid;
            product.ItemPackageQuantity = "1";
            product.NumberOfItems = "1";
            product.Condition = conditionInfo;
            product.DescriptionData = descriptiondata;
            //product.ReleaseDateSpecified = true;
            //product.ReleaseDate = post.startDate;
            
            return product;
        }

        public virtual Product GetParentProductDto(string condition, string title)
        {
            ConditionInfo conditionInfo = new ConditionInfo();
            conditionInfo.ConditionType = ConditionType.New;

            ProductDescriptionData descriptiondata = new ProductDescriptionData();
            descriptiondata.Brand = ToTitleCase(_item.SubDescription1);
            //descriptiondata.Description = this.FullDescription;

            descriptiondata.Title = title;

            Product product = new Product();
            product.SKU = _item.ItemClass.ItemLookupCode;
            product.ItemPackageQuantity = "1";
            product.NumberOfItems = "1";
            product.Condition = conditionInfo;
            product.DescriptionData = descriptiondata;
            //product.ReleaseDateSpecified = true;
            //product.ReleaseDate = post.startDate;

            return product;
        }

        private string ToTitleCase(string word)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(word);
        }



        
    }
}
