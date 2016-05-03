using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
   public class OrderingSystem
    {
        //create the object to use it in this class
        private SqlData _sqlData = new SqlData();
        private List<CustomerProducts> _customerProducts = new List<CustomerProducts>();

        //constructor
        public OrderingSystem()
        {
            ShowMenu();
        }
        public void ShowMenu()
        {
            Console.WriteLine("\n*********************************************************" +
                              "\n* *Welcome to Bangazon!Command Line Ordering System * *" +
                              "\n*********************************************************" +
                              "\n1.Create an account" +
                              "\n2.Create a payment option" +
                              "\n3.Order a product" +
                              "\n4.Complete an order" +
                              "\n5.See product popularity" +
                              "\n6.Leave Bangazon!");

            //Use the ReadLine for call the functions
            //string key is going to return the numbers of the menu (any key that the user presses)
            string key = Console.ReadLine();

            //we need the while loop to keep the menu going
            while (key != "6")
            {
                switch (key)
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        CreatePaymentOption();
                        break;
                    case "3":
                        OrderProducts();
                        break;
                    case "4":
                        CompleteOrder();
                        break;
                    case "5":
                        SeeProductPopularity();
                        break;
                }

                key = Console.ReadLine();
            }
        }

        //this is a stubbed function 
        public void CreateAccount()
        {
            Customer customer = new Customer();

            Console.WriteLine("Enter Customer First Name");
            customer.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Customer Last Name");
            customer.LastName = Console.ReadLine();
            Console.WriteLine("Enter Street");
            customer.StreetAdress = Console.ReadLine();
            Console.WriteLine("Enter City");
            customer.City = Console.ReadLine();
            Console.WriteLine("Enter State/Providence");
            customer.StateProvence = Console.ReadLine();
            Console.WriteLine("Enter Postal Code");
            customer.PostalCode = Console.ReadLine();
            Console.WriteLine("Enter Phone Number");
            customer.PhoneNumber = Console.ReadLine();

            //call to update the database
            _sqlData.CreateCustomer(customer);
        }

        public Customer ChooseCustomer()
        {
            Customer customer = null;
            List<Customer> customerList = _sqlData.GetCustomers();
            for (int i = 0; i < customerList.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + customerList[i].FirstName + " " + customerList[i].LastName);
            }

            string chosenCustomer = Console.ReadLine();
            int chosenCustomerId = int.Parse(chosenCustomer);
            //be sure that only somebody can pick a number inside list
            if (chosenCustomerId >= 0 && chosenCustomerId <= customerList.Count)
            {
                customer = customerList[chosenCustomerId - 1];
            }

            return customer;
        }

        public void CreatePaymentOption()
        {
            //create payment option object
            Console.WriteLine("Which customer?");
            Customer customer = ChooseCustomer();
            CustomerProducts customerProducts = GetCustomerProducts(customer);
            PaymentOption paymentoption = new PaymentOption();

            //get the IdCustomer from the Customer table
            paymentoption.IdCustomer = customer.IdCustomer;

            Console.WriteLine("Enter PaymentId (e.g. 123)");
            paymentoption.IdPaymentOption = Convert.ToInt16( Console.ReadLine() );

            Console.WriteLine("Enter payment type (e.g. AmEx, Visa, Checking)");
            paymentoption.Name = Console.ReadLine();

            Console.WriteLine("Enter account number ");
            paymentoption.AccountNumber = Console.ReadLine();

            customerProducts.Payment = paymentoption;

            //call to update the database
            _sqlData.CreatePaymentOption(paymentoption);


        }

        public Product ChooseProduct()
        {
            Product product = null;
            List<Product> productList = _sqlData.GetProducts();
            for (int i = 0; i < productList.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + productList[i].Name);
            }
            int lastItem = productList.Count + 1;
            Console.WriteLine((productList.Count + 1) + ". Back to main menu");

            string chosenProduct = Console.ReadLine();
            int chosenProductId = int.Parse(chosenProduct);
            //be sure that only somebody can pick a number inside list
           if (chosenProductId >= 0 && chosenProductId <= productList.Count)
            {
                product = productList[chosenProductId - 1];
            }

            return product;
        }

        public void OrderProducts()
        {
            Console.WriteLine("Which customer?");
            Customer customer = ChooseCustomer();
            CustomerProducts customerProducts = GetCustomerProducts(customer);

            Product nextProduct = null;
            do
            {
                nextProduct = ChooseProduct();
                if (nextProduct != null)
                {
                    customerProducts.Products.Add(nextProduct);
                }
            }
            while (nextProduct != null);
            
        }
        public void CompleteOrder()
        {
            Console.WriteLine("Which customer?");
            Customer customer = ChooseCustomer();
            CustomerProducts customerProducts = GetCustomerProducts(customer);
            //foreach(CustomerProducts cProds in _customerProducts)
            //{
            //    if (cProds.TheCustomer.FirstName == customer.FirstName && 
            //        cProds.TheCustomer.LastName == customer.LastName)
            //    {
            //    }
            //}

            Console.WriteLine(string.Format("The total is ${0}.", customerProducts.Products.Sum(x => x.Price)));
            //decimal finalPrice = 0;
            //foreach(Product product in customerProducts.Products)
            //{
            //    finalPrice += product.Price;
            //}
            //Console.WriteLine(finalPrice);

            _sqlData.CreateCustomerOrder(customerProducts);
        }

        public void SeeProductPopularity() { }

        public void LeaveBangazon()
        {
            System.Environment.Exit(0);
        }

        private CustomerProducts GetCustomerProducts(Customer customer)
        {
            CustomerProducts customerProducts = null;
            foreach (CustomerProducts cProds in _customerProducts)
            {
                if (cProds.TheCustomer.FirstName == customer.FirstName &&
                    cProds.TheCustomer.LastName == customer.LastName)
                {
                    customerProducts = cProds;
                }
            }

            if (customerProducts == null)
            {
                customerProducts = new CustomerProducts();
                customerProducts.TheCustomer = customer;
                _customerProducts.Add(customerProducts);
            }
            
            return customerProducts;
        }
    }
}
