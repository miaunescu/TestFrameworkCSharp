using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFrame.Models.Northwind;

namespace TestFrame.Databases
{
    public class NorthwindDatabase : BaseDatabasesConfiguration
    {
        private IDbConnection dbConnection;

        public NorthwindDatabase()
        {
            dbConnection = new SqlConnection(config.GetConnectionString("Northwind"));
        }

        public List<NorthwindCustomersModel> GetClients()
        {
            var query = "SELECT TOP 10 * FROM dbo.Customers";
            return dbConnection.Query<NorthwindCustomersModel>(query).ToList();
        }

        public NorthwindCustomersModel AddCustomer(NorthwindCustomersModel customerModel)
        {


            var query = "INSERT INTO dbo.Customers(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)" +
                "VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax);" +
                "SELECT CustomerId FROM dbo.Customers WHERE CustomerID = @CustomerID;";

            var customerId = dbConnection.Query<string>(query, new
            {
                @CustomerId = customerModel.CustomerID,
                @CompanyName = customerModel.CompanyName,
                @ContactName = customerModel.ContactName,
                @ContactTitle = customerModel.ContactTitle,
                @Address = customerModel.Address,
                @City = customerModel.City,
                @Region = customerModel.Region,
                @PostalCode = customerModel.PostalCode,
                @Country = customerModel.Country,
                @Phone = customerModel.Phone,
                @Fax = customerModel.Fax,
            }).Single();

            customerModel.CustomerID = customerId;
            return customerModel;
        }

        public void DeleteCustomer(string customerId)
        {
            var query = "DELETE FROM dbo.Customers where CustomerID = @CustomerID";

            dbConnection.Execute(query, new { customerId });
        }
    }
}

