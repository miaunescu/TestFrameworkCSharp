using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TestFrame.Databases;
using TestFrame.Models.Northwind;
using Xunit;

namespace TestFrame.Tests.NorthwindDatabaseTests
{
    //You need to start the server 3.70.90.215 before running the tests
    public class NorthwindCustomersDatabaseTests : BaseDatabasesConfiguration
    {
        private readonly IDbClient _dbClient;
        public NorthwindCustomersDatabaseTests(IDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [Fact]
        public void Get_Customers_Northwind_Database_Test()
        {
            var firstClient = GetNorthwindCustomers().FirstOrDefault();

            using (new AssertionScope())
            {
                firstClient.Should().NotBeNull();
                firstClient!.CompanyName.Should().Be("Alfreds Futterkiste");
            }
        }

        [Fact]
        public async Task Add_Customer_Northwind_Database_Test()
        {
            var model = new NorthwindCustomersModel()
            {
                CustomerID = "AUTO1",
                Address = "Endava County 1 Ave",
                City = "Bucharest",
                CompanyName = "AutoCompany1",
                ContactName = "ContactAuto1",
                ContactTitle = "ContactTitleAuto1",
                Country = "Romania",
                Fax = "000-000",
                Phone = "898989",
                PostalCode = "12345",
                Region = "Tineretului"
            };

            var queryParameters = new Dictionary<string, object>
            {
                ["CustomerID"] = model.CustomerID,
                ["Address"] = model.Address,
                ["City"] = model.City,
                ["CompanyName"] = model.CompanyName,
                ["ContactName"] = model.ContactName,
                ["ContactTitle"] = model.ContactTitle,
                ["Country"] = model.Country,
                ["Fax"] = model.Fax,
                ["Phone"] = model.Phone,
                ["PostalCode"] = model.PostalCode,
                ["Region"] = model.Region
            };

            await AddNewCustomerToNorthwindDb(queryParameters);


            var newCustomer = await GetNorthwindCustomerById(queryParameters);

            using (new AssertionScope())
            {
                newCustomer!.Country.Should().Be(model.Country);
                newCustomer.CustomerID.Should().Be(model.CustomerID);
                newCustomer.CompanyName.Should().Be(model.CompanyName);
                newCustomer.ContactName.Should().Be(model.ContactName);
                newCustomer.ContactTitle.Should().Be(model.ContactTitle);
                newCustomer.Address.Should().Be(model.Address);
                newCustomer.City.Should().Be(model.City);
                newCustomer.Region.Should().Be(model.Region);
                newCustomer.PostalCode.Should().Be(model.PostalCode);
                newCustomer.Country.Should().Be(model.Country);
                newCustomer.Phone.Should().Be(model.Phone);
                newCustomer.Fax.Should().Be(model.Fax);
            }

            //Delete the customer after creation, to be able to create another one with the same CustomerID
            await DeleteNorthwindCustomer(queryParameters);
        }

        #region DbQueries
        private List<NorthwindCustomersModel> GetNorthwindCustomers()
        {
            var query = "SELECT TOP 10 * FROM dbo.Customers";
            return _dbClient.GetRecordsFromDatabase<NorthwindCustomersModel>(CreateNorthwindConnection(), query);
        }

        private async Task AddNewCustomerToNorthwindDb(Dictionary<string, object> parameters)
        {
            var query =
                "INSERT INTO dbo.Customers(CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)" +
                "VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax);";

            await _dbClient.AddRecordToDatabase(CreateNorthwindConnection(), query, parameters);
        }

        private async Task DeleteNorthwindCustomer(Dictionary<string, object> parameters)
        {
            var query = "DELETE FROM dbo.Customers where CustomerID = @CustomerID";
            await _dbClient.DeleteRecordFromDatabaseAsync(CreateNorthwindConnection(), parameters, query);
        }

        private async Task<NorthwindCustomersModel> GetNorthwindCustomerById(Dictionary<string, object> parameters)
        {
            var query = "SELECT * FROM dbo.Customers WHERE CustomerID = @CustomerID";
            return await _dbClient.GetOneRecordFromDatabaseAsync<NorthwindCustomersModel>(CreateNorthwindConnection(), query, parameters);
        }

        protected IDbConnection CreateNorthwindConnection()
        {
            var sqlConnection = new SqlConnection(config?.GetConnectionString("Northwind"));
            return sqlConnection;
        }
        #endregion
    }
}

