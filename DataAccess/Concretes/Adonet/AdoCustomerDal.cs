using Core.DataAccess.Adonet.Helpers;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.Adonet
{
    public class AdoCustomerDal : ICustomerDal
    {
        public string Generator()
        {
            Random rastgele = new Random();
            string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz0123456789";
            string uret = "";
            for (int i = 0; i < 5; i++)
            {
                uret += harfler[rastgele.Next(harfler.Length)];
            }
            return uret;

        }
        public void Add(Customer customer)
        {
            // ADD kısmını handle et.
            var query = "Insert into Customers(CustomerID,CompanyName) VALUES ('"
                +Generator()+
                "',@CompanyName)";
            DbHelper.CreateWriteConnection<Customer>(query, customer);
        }

        public void Delete(Customer customer)
        {
            var query = "DELETE FROM Customers WHERE CustomerId='"+customer.CustomerID+"'";
            DbHelper.CreateWriteConnection<Customer>(query, customer);
        }

        public Customer Get(Expression<Func<Customer, bool>> filter = null)
        {
            List<Customer> _customer = DbHelper.CreateReadConnection<Customer>("select * from Customers");
            return filter != null ? _customer.AsQueryable().FirstOrDefault(filter) : _customer.FirstOrDefault();
        }


        public List<Customer> GetAll(Expression<Func<Customer, bool>> filter = null)
        {
            List<Customer> _customer = DbHelper.CreateReadConnection<Customer>("select * from Customers");
            return filter != null ? _customer = _customer.AsQueryable().Where(filter).ToList()  : _customer;
        }

        public Customer GetById(string id)
        {
            Customer customer = DbHelper.CreateReadConnection<Customer>($"select * from Customers where CustomerID='{id}'").FirstOrDefault();
            return customer;
        }

 

        public void Update(Customer customer)
        {
            var query = "UPDATE Customers SET CompanyName =' " +
                        customer.CompanyName+"' WHERE CustomerID ='"+customer.CustomerID+"'";
            DbHelper.CreateWriteConnection<Customer>(query, customer);
        }
    }
}
