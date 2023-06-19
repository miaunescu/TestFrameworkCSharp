//using Dapper;
//using Microsoft.Extensions.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using TestFrame.Models.Northwind;

//namespace TestFrame.Databases
//{
//    public class NorthwindDatabase : BaseDatabasesConfiguration
//    {
//        private IDbConnection dbConnection;

//        public NorthwindDatabase()
//        {
//            dbConnection = new SqlConnection(config.GetConnectionString("Northwind"));
//        }

//        public List<NorthwindCustomersModel> GetClients()
//        {
//            var query = "SELECT TOP 10 * FROM dbo.Customers";
//            return dbConnection.Query<NorthwindCustomersModel>(query).ToList();
//        }

//        public NorthwindCustomersModel AddCustomer(NorthwindCustomersModel customerModel)
//        {

//            var query = "INSERT INTO dbo.Customers(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)" +
//                "VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax);" +
//                "SELECT CustomerId FROM dbo.Customers WHERE CustomerID = @CustomerID;";

//            var customerId = dbConnection.Query<string>(query, new
//            {
//                customerModel.CustomerID,
//                customerModel.CompanyName,
//                customerModel.ContactName,
//                customerModel.ContactTitle,
//                customerModel.Address,
//                customerModel.City,
//                customerModel.Region,
//                customerModel.PostalCode,
//                customerModel.Country,
//                customerModel.Phone,
//                customerModel.Fax,
//            }).Single();

//            customerModel.CustomerID = customerId;
//            return customerModel;
//        }

//        public void DeleteCustomer(string customerId)
//        {
//            var query = "DELETE FROM dbo.Customers where CustomerID = @CustomerID";

//            dbConnection.Execute(query, new { customerId });
//        }
//    }
//}

