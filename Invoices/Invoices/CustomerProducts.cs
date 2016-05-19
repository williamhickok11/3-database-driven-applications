using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
    public class CustomerProducts
    {
        private List<Product> _productList = new List<Product>();

        public Customer TheCustomer { get; set; }
        public List<Product> Products { get { return _productList; } set { _productList = value; } }
        public PaymentOption Payment { get; set; }
    }
}
