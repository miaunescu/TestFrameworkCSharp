using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFrame.Databases;
using TestFrame.Models.Northwind;
using Xunit;

namespace TestFrame.Tests.NorthwindDatabaseTests
{
    //You need to start the server 3.70.90.215 before running the tests
    public class NorthwindCustomersDatabaseTests
    {
        NorthwindDatabase northwindDatabase;

        public NorthwindCustomersDatabaseTests()
        {
            northwindDatabase = new();
        }

        [Fact]
        public void Get_Customers_Northwind_Database_Test()
        {
            var firstClient = northwindDatabase.GetClients().FirstOrDefault();

            using (new AssertionScope())
            {
                firstClient.CompanyName.Should().Be("Alfreds Futterkiste");
            }
        }

        [Fact]
        public void Add_Customer_Northwind_Database_Test()
        {
            NorthwindCustomersModel model = new()
            {
                CustomerID = "AUTO1",
                CompanyName = "AutoCompany1",
                ContactName = "ContactAuto1",
                ContactTitle = "ContactTitleAuto1",
                Address = "Endava County 1 Ave",
                City = "Bucharest",
                Region = "Tineretului",
                PostalCode = "12345",
                Country = "Romania",
                Phone = "898989",
                Fax = "000-000"
            };

            var client = northwindDatabase.AddCustomer(model);

            using (new AssertionScope())
            {
                client.Country.Should().Be(model.Country);
                client.CustomerID.Should().Be(model.CustomerID);
                client.CompanyName.Should().Be(model.CompanyName);
                client.ContactName.Should().Be(model.ContactName);
                client.ContactTitle.Should().Be(model.ContactTitle);
                client.Address.Should().Be(model.Address);
                client.City.Should().Be(model.City);
                client.Region.Should().Be(model.Region);
                client.PostalCode.Should().Be(model.PostalCode);
                client.Country.Should().Be(model.Country);
                client.Phone.Should().Be(model.Phone);
                client.Fax.Should().Be(model.Fax);
            }

            //Delete the customer after creation, to be able to create another one with the same CustomerID
            northwindDatabase.DeleteCustomer(model.CustomerID);
        }
    }
}

