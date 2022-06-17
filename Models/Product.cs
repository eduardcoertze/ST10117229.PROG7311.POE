namespace ST10117229.PROG7311.POE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    //Product Model class
    public partial class Product
    {
        //Product properties
        public int Product_ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Product_Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Product_Type { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public System.DateTime Product_Date { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Product_Price { get; set; }
        public int Client_ID { get; set; }
    
        public virtual Client Client { get; set; }
    }
}
