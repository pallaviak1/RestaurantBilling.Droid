using RestaurantBilling.Core;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CoreLib.Repositories
{
    public class Repository
    {
        private readonly SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public Repository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Bill>().Wait();
        }

        public async Task CreateBill(Bill bill)
        {
            try
            {
                // Basic validation to ensure we have a customer email.
                if (string.IsNullOrWhiteSpace(bill.CustomerEmail))
                    throw new Exception("Customer Email is required");

                // Insert a new customer bill into the database
                var result = await conn.InsertAsync(bill).ConfigureAwait(continueOnCapturedContext: false);
                StatusMessage = $"{result} record(s) added [Customer Email: {bill.CustomerEmail})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to create {bill.CustomerEmail}'s bill. Error: {ex.Message}";
            }
        }

        public Task<List<Bill>> GetAllBills()
        {
            // Return a list of bills saved to the Bill table in the database.
            return conn.Table<Bill>().ToListAsync();
        }
    }
}
