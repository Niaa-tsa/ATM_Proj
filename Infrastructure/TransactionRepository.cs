using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Domain.Models;
namespace Infrastructure
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _path = "Transactions.txt";

        public List<Transaction> GetAll()
        {
            if (!File.Exists(_path))
                return new List<Transaction>();

            var lines = File.ReadAllLines(_path);

            List<Transaction> transactions = new();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var transaction = JsonSerializer.Deserialize<Transaction>(line);

                if (transaction != null)
                    transactions.Add(transaction);
            }

            return transactions;
        }

        public void Add(Transaction transaction)
        {
            var all = GetAll();

            transaction.Id = all.Count == 0 ? 1 : all.Max(x => x.Id) + 1;

            string json = JsonSerializer.Serialize(transaction);

            File.AppendAllLines(_path, new[] { json });
        }
    }
}
