namespace ST10117229.PROG7311.POE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    //Client Model class
    public partial class Client
    {
        //Client Model properties
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Products = new HashSet<Product>();
        }

        public int Client_ID { get; set; }
        public bool Is_Farmer { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Client_Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Client_Surname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Client_Email { get; set; }
        [Required(ErrorMessage ="This field is required")]
        [DataType(DataType.Password)]
        public string Client_Password { get; set; }

        public string LoginErrorMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
