﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace EcomerceWebASP.Models
{

    public class ProductDTODetail
    {
        public string idProduct { get; set; }
        public string type { get; set; }
        public string nameProduct { get; set; }
        public double price { get; set; }
        public string URLImage { get; set; }
        public string sku { get; set; }

    }
    public class ProductDTO
    {
        public List<ImageProduct> listImage;
        public string idProduct { get; set; }
        public string nameProduct { get; set; }
        public double price { get; set; }


        public ProductDTO(double price, string nameProduct, string idProduct, List<ImageProduct> listImage)
        {
            this.price = price;
            this.nameProduct = nameProduct;
            this.idProduct = idProduct;
            this.listImage = listImage;
        }


    }
    public class BillData
    {
        public string idBill { get; set; }
        public string idUser { get; set; }
        public string nameUser { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int Ship { get; set; }
        public int Total { get; set; }
        public string PTTT { get; set; }
        public string status { get; set; }
        public System.DateTime createdAt { get; set; }
        public int Qty { get; set; }
        public DetailBIll[] DetailBIlls { get; internal set; }
    }
    public class Category
    {
        public string nameType { get; set; }
        public string idType { get; set; }

    }

    public class ItemDetail
    {
        //thông tin trong b?ng Bill
        public string nameBook { get; set; }
        public string idBill { get; set; }
        public string phone { set; get; }
        public string address { get; set; }
        public int Total { get; set; }

        //thông tin trong b?ng DetailBill
        public string nameProduct { get; set; }
        public int subTotal { get; set; }
        public int shipping { get; set; }
        public string idProduct { get; set; }
        public int qty { set; get; }
        public double price { get; set; }
        public int intoMoney { get; set; }
        public string statusId { get; set; }
        public string statusName { get; set; }
        public string idDetailBill { get; set; }


    }
    [Serializable]
    public class UserLogin
    {
        public string idUser { get; set; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
        public string fullName { get; set; }
        public string email { get; set; }
        public int phone { get; set; }


    }
    public class getUserDTO
    {
        public string userId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
    }


    public class Order
    {
        [Required]
        public string customerName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string province { get; set; }
        public string idUserReal { get; set; }

        [Required]
        public string district { get; set; }
        [Required]
        public string ward { get; set; }
        public string note { get; set; }
        [Required]
        public List<ProductOrder> listProduct { get; set; }
    }

    public class ProductOrder
    {
        [Required]
        public string idProduct { get; set; }
        [Required]
        public int qty { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public string nameProduct { get; set; }
        public string attributeId { get; set; }
        public string attributeValueId { get; set; }
        public string attributes { get; set; }
        [Required]
        public string imageProduct { get; set; }
    }

    public enum ProductOrderStatus
    {
        accept,
        cancel,
        fail,
        package,
        refunded,
        returns,
        shipping,
        success,
        watting,
    }
}