﻿using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHStaticProxy.Tests.Entities;

namespace NHStaticProxy.Tests
{
    public class CustomerFixture
    {
        public int AddCustomer(ISessionFactory sessionFactory)
        {
            int customerId = -1;

            using (var s = sessionFactory.OpenSession())
            using (var tx = s.BeginTransaction())
            {
                var obj = new Customer { Name = "Zoubi" };
                s.Save(obj);
                customerId = obj.Id;
                tx.Commit();
            }

            return customerId;
        }

        public void DeleteCustomers(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Delete("from Customer");

                tx.Commit();
            }
        }
    }
}