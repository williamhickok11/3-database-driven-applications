using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon

{
    public class SqlData
    {

        #region Variables

        //SQL Connection is object that connects you to the database
        private SqlConnection _sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
            "\"C:\\Users\\William\\Documents\\Visual Studio 2015\\Projects\\exercises\\3-database-driven-applications\\Invoices\\Invoices\\Invoices.mdf\"; Integrated Security=True");

        #endregion
        
        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Public Methods
        
        public void CreateCustomer(Customer customer)
        { // Create a new customer
            // Define a string to use to query the database
            string command = string.Format("INSERT INTO Customer (FirstName, LastName, StreetAdress, City, StateProvence, PostalCode, PhoneNumber) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", customer.FirstName, customer.LastName, customer.StreetAdress,
                customer.City, customer.StateProvence, customer.PostalCode, customer.PhoneNumber);
            // Pass in the string to the Update Data Base method
            UpdateDataBase(command);
        }

        public List<Customer> GetCustomers()
        { //create a list so we can have the data from the customer table
            List<Customer> customerList = new List<Customer>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            // Select the desired information from the Customer table
            cmd.CommandText = "SELECT IdCustomer, FirstName, LastName, StreetAdress, City, StateProvence, PostalCode, PhoneNumber FROM Customer";
            cmd.Connection = _sqlConnection;

            _sqlConnection.Open();
            // Using will clean up everything... open and close connections
            // Read the database... while there is infomation to read, build a list of customers
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read()) // Reading the customer data from out table data
                {
                    // Creating an instance of a customer with the tabledata
                    Customer customer = new Customer();
                    customer.IdCustomer = dataReader.GetInt32(0);
                    customer.FirstName = dataReader.GetString(1);
                    customer.LastName = dataReader.GetString(2);
                    customer.StreetAdress = dataReader.GetString(3);
                    customer.City = dataReader.GetString(4);
                    customer.StateProvence = dataReader.GetString(5);
                    customer.PostalCode = dataReader.GetString(6);
                    customer.PhoneNumber = dataReader.GetString(7);
                    // Add the created customer to the list
                    customerList.Add(customer);
                }
            }
            _sqlConnection.Close();

            return customerList;
        }
        
        public void CreatePaymentOption(PaymentOption paymentOption)
        { //add the information that ask the customer to our payment option table
            string command = string.Format("INSERT INTO PaymentOption (IdCustomer, Name, AccountNumber) " +
                "VALUES ('{0}', '{1}', '{2}')", paymentOption.IdCustomer, paymentOption.Name, paymentOption.AccountNumber);
            UpdateDataBase(command);
        }

        public PaymentOption GetPaymentOptionForCustomer(Customer customer)
        { // Access the payment option WHERE the customerId matches
            PaymentOption paymentOption = null;
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            // Only access the payment option that matches with the customer we have passed in
            cmd.CommandText = string.Format("SELECT IdPaymentOption, Name, AccountNumber FROM PaymentOption " +
                "WHERE IdCustomer = '{0}'", customer.IdCustomer);
            cmd.Connection = _sqlConnection;

            try
            {
                _sqlConnection.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    { // Creat a payment option object (but it is not adding to a list, so I think it just creates the last instance)
                        paymentOption = new PaymentOption();
                        paymentOption.IdPaymentOption = dataReader.GetInt32(0);
                        paymentOption.IdCustomer = customer.IdCustomer;
                        paymentOption.Name = dataReader.GetString(1);
                        paymentOption.AccountNumber = dataReader.GetString(2);
                    }
                }
            }
            finally
            {
                _sqlConnection.Close();
            }
                return paymentOption;
        }

        public List<Product> GetProducts()
        {
            List<Product> productList = new List<Product>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT IdProduct, Name, Description, Price, IdProductType FROM Product";
            cmd.Connection = _sqlConnection;

            _sqlConnection.Open();

            //using will clean up everything... open and close connections
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Product product = new Product();
                    product.IdProduct = dataReader.GetInt32(0);
                    product.Name = dataReader.GetString(1);
                    product.Description = dataReader.GetString(2);
                    product.Price = dataReader.GetDecimal(3);
                    product.IdProductType = dataReader.GetInt32(4);

                    productList.Add(product);
                }
            }

            _sqlConnection.Close();

            return productList;
        }

        public void CreateCustomerOrder(CustomerProducts customerProducts)
        {
            DateTime now = DateTime.Now;
            int orderNumber = (new Random()).Next(int.MaxValue);
            //1. Add row to CustomerOrder table
            string command = string.Format("INSERT INTO CustomerOrder (OrderNumber, DateCreated, IdCustomer, IdPaymentOption, Shipping) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", orderNumber, now.ToString(), customerProducts.TheCustomer.IdCustomer, customerProducts.Payment.IdPaymentOption, "UPS");
            UpdateDataBase(command);

            //2. Get IdOrder from CustomerOrder table
            command = string.Format("SELECT IdCustomerOrder FROM CustomerOrder WHERE IdCustomer='{0}' AND OrderNumber='{1}'", customerProducts.TheCustomer.IdCustomer, orderNumber);
            int idOrder = GetIdFromTable(command);

            //3. Ad row to OrderProducts table
            foreach(var product in customerProducts.Products)
            {
                command = string.Format("INSERT INTO OrderProducts (IdProduct, IdOrder)" +
                    "VALUES ('{0}', '{1}')", product.IdProduct, idOrder );
                UpdateDataBase(command);
            }
        }
        #endregion

        #region Private Methods
        
        private void UpdateDataBase(string commandString)
        { //SQL Command tells the database what you're going to do (and update, an insert, a delete or a select)
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = commandString;
            cmd.Connection = _sqlConnection;

            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        private int GetIdFromTable(string commandString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = commandString;
            cmd.Connection = _sqlConnection;

            _sqlConnection.Open();
            object idObj = cmd.ExecuteScalar();
            _sqlConnection.Close();

            return (int)idObj;
        }

        #endregion
    }
}
