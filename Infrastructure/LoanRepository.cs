using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

    namespace Infrastructure
    {
        public class LoanRepository : ILoanRepository
        {
            private readonly string path = "C:\\Users\\Nia Tsalkalamanidze\\Desktop\\N\\Step\\ATM_Project\\Infrastructure\\Data\\Loans.txt";


            public List<LoanRequest> GetAll()
            {
                if (!File.Exists(path))
                    return new List<LoanRequest>();


                var lines = File.ReadAllLines(path);

                List<LoanRequest> loans = new();


                foreach (var line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        loans.Add(
                            JsonSerializer.Deserialize<LoanRequest>(line)
                        );
                    }
                }

                return loans;
            }



            public void Add(LoanRequest loan)
            {
                string json = JsonSerializer.Serialize(loan);

                File.AppendAllLines(
                    path,
                    new[] { json }
                );
            }



            public void Save(List<LoanRequest> loans)
            {
                File.WriteAllLines(
                    path,
                    loans.Select(x =>
                    JsonSerializer.Serialize(x))
                );
            }
        }
    }

