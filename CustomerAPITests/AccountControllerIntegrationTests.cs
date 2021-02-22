using System;
using System.Linq;
using System.Net.Http;
using CustomerAPI;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace CustomerAPITests
{
    public class AccountControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        #region CustomerReusableVariables
        // existing customerId from SeedData
        private readonly Guid customerId = new Guid("d20b0d04-d371-48b7-99aa-0e2ac74ff98e");
        #endregion


        [Fact]
        public async void OpenAccountForCustomer_WhenCustomerIdDoesNotExists_ReturnsNotFound()
        {
            var factory = new CustomWebApplicationFactory<Startup>();

            var testClient = factory.CreateClient();

            decimal initialCredit = 100;

            // non existing customerId
            var customerId = new Guid("d20b0d04-d371-48b7-99aa-0e2ac74ff98b");

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { customerId, initialCredit }));

            var accountResponse = await
                testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount",
                stringContent);

            Assert.NotNull(accountResponse);
            Assert.Equal("Not Found", accountResponse.ReasonPhrase);
            Assert.Equal(StatusCodes.Status404NotFound, (double)accountResponse.StatusCode);
        }


        [Fact]
        public async void OpenAccountForCustomer_WhenInitialCreditIsZero_CreatesNewAccountWithNoTransactions()
        {
            var factory = new CustomWebApplicationFactory<Startup>();

            var testClient = factory.CreateClient();

            decimal initialCredit = 0;

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { customerId, initialCredit }));

            var accountResponse = await
                testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}",
                stringContent);

            var customerResponse = await testClient.GetStringAsync($"api/customers/{customerId}");
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(customerResponse);

            Assert.NotNull(accountResponse);
            Assert.Equal("Created", accountResponse.ReasonPhrase);
            Assert.Equal(StatusCodes.Status201Created, (double)accountResponse.StatusCode);
            Assert.Equal(0, customer.Balance);
            Assert.Single(customer.Accounts);
            Assert.Empty(customer.Accounts[0].Transactions);
        }


        [Fact]
        public async void OpenAccountForCustomer_WhenBothCustomerIdAndInitialCreditHaveValue_CreatesNewAccountWithBalanceAndTransaction()
        {
            var factory = new CustomWebApplicationFactory<Startup>();

            var testClient = factory.CreateClient();

            decimal initialCredit = 100;

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { customerId, initialCredit }));

            var accountResponse = await
                testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}",
                stringContent);

            var customerResponse = await testClient.GetStringAsync($"api/customers/{customerId}");
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(customerResponse);

            Assert.NotNull(accountResponse);
            Assert.Equal(100, customer.Balance);
            Assert.Single(customer.Accounts);
            Assert.Single(customer.Accounts[0].Transactions);
        }


        [Fact]
        public async void OpenAccountForCustomer_CreatesNewAccountEachTimeTheEndpointIsCalled()
        {
            var factory = new CustomWebApplicationFactory<Startup>();

            var testClient = factory.CreateClient();

            decimal initialCredit = 0;

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { customerId, initialCredit }));

            for (int i = 1; i <= 4; i++)
            {
                await testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}", stringContent);
            }
           
            var customerResponse = await testClient.GetStringAsync($"api/customers/{customerId}");
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(customerResponse);

            Assert.NotNull(customerResponse);
            Assert.Equal(4, customer.Accounts.Count);
        }


        [Fact]
        public async void OpenAccountForCustomer_ShouldHaveAsUserBalanceTheSumOfTheBalancesOfUserAccounts()
        {
            var factory = new CustomWebApplicationFactory<Startup>();

            var testClient = factory.CreateClient();

            decimal initialCredit = 0;

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { customerId, initialCredit }));

            await testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}", stringContent);

            initialCredit = 100;
            await testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}", stringContent);

            initialCredit = 200;
            await testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}", stringContent);

            initialCredit = 300;
            await testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}", stringContent);

            initialCredit = 400;
            await testClient.PostAsync($"api/customeraccounts/{customerId}/currentaccount/{initialCredit}", stringContent);

            var customerResponse = await testClient.GetStringAsync($"api/customers/{customerId}");
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(customerResponse);

            Assert.NotNull(customerResponse);
            Assert.Equal(1000, customer.Accounts.Sum(account => account.Balance));
        }
    }
}
