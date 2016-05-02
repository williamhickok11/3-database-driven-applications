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
        {
            string command = string.Format("INSERT INTO Customer (FirstName, LastName, StreetAdress, City, StateProvence, PostalCode, PhoneNumber) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", customer.FirstName, customer.LastName, customer.StreetAdress,
                customer.City, customer.StateProvence, customer.PostalCode, customer.PhoneNumber);
            UpdateDataBase(command);
        }

        public List<Customer> GetCustomers()
        { //create a list so we can have the data from the customer table
            List<Customer> customerList = new List<Customer>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT IdCustomer, FirstName, LastName, StreetAdress, City, StateProvence, PostalCode, PhoneNumber FROM Customer";
            cmd.Connection = _sqlConnection;

            _sqlConnection.Open();
            //using will clean up everything... open and close connections
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Customer customer = new Customer();
                    customer.IdCustomer = dataReader.GetInt32(0);
                    customer.FirstName = dataReader.GetString(1);
                    customer.LastName = dataReader.GetString(2);
                    customer.StreetAdress = dataReader.GetString(3);
                    customer.City = dataReader.GetString(4);
                    customer.StateProvence = dataReader.GetString(5);
                    customer.PostalCode = dataReader.GetString(6);
                    customer.PhoneNumber = dataReader.GetString(7);

                    customerList.Add(customer);
                }
            }
            _sqlConnection.Close();

            return customerList;
        }
        //add the information that ask the customer to our payment option table
        public void CreatePaymentOption(PaymentOption paymentOption)
        {
            string command = string.Format("INSERT INTO PaymentOption (IdCustomer, Name, AccountNumber) " +
                "VALUES ('{0}', '{1}', '{2}')", paymentOption.IdCustomer, paymentOption.Name, paymentOption.AccountNumber);
            UpdateDataBase(command);
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
        #endregion

        #region Private Methods

        private void UpdateDataBase(string commandString)
        {
            //SQL Command tells the database what you're going to do (and update, an insert, a delete or a select)
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = commandString;
            cmd.Connection = _sqlConnection;

            _sqlConnection.Open();
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        #endregion
    }
}
