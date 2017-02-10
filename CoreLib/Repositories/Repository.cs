using CoreLib.Models;
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

        #region Currency
        private static readonly List<Currency> AllAvailableCurrencies = new List<Currency>
        {
            new Currency { CurrencyId = 1, CurrencyName = "Dollar", CurrencySymbol = "$"},
            new Currency { CurrencyId = 2, CurrencyName = "Euro", CurrencySymbol = "€"},
            new Currency { CurrencyId = 3, CurrencyName = "Pound", CurrencySymbol = "£"}
        };
        private Currency _activeCurrency;

        public Task<List<Currency>> GetAvailableCurrencies()
        {
            return Task.Run(() => AllAvailableCurrencies);            
        }

        public Task<Currency> GetCurrencyById(int currencyId)
        {
            return Task.Run(() => AllAvailableCurrencies[currencyId]);
        }

        public void SetActiveCurrency(Currency currency)
        {
             _activeCurrency = currency;
        }
        public Task<Currency> GetActiveCurrency()
        {
            if (_activeCurrency == null)
            {
                Task<Currency> task = GetCurrencyById(1);
                task.Wait();
                _activeCurrency = task.Result;
            }
            return Task.Run(() => _activeCurrency);
        }
       
        #endregion
    }
}
