﻿using System;
using System.Collections.Generic;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHStaticProxy.Tests.Entities;

namespace NHStaticProxy.Tests.Config
{
    public class HbmMappingProvider : IHbmMappingProvider
    {
        public IEnumerable<HbmMapping> HbmMappings
        {
            get
            {
                var mapper = new NHibernate.Mapping.ByCode.ModelMapper();

                mapper.Class<Customer>(customer =>
                {
                    customer.Id(mt => mt.Id,
                          idm =>
                          {
                              idm.Generator(Generators.Native);
                          });

                    customer.Property(mt => mt.Name);
                    customer.Set(p => p.Orders,
                                 cm =>
                                 {
                                     cm.Cascade(Cascade.Persist);
                                     cm.Inverse(true);
                                 },
                                 m => m.OneToMany());
                });

                mapper.Class<Order>(order =>
                {
                    order.Table("Orders");
                    
                    order.Id(mt => mt.Id,
                          idm =>
                          {
                              idm.Generator(Generators.Native);
                          });

                    order.Property(mt => mt.Name);
                    order.ManyToOne(p => p.Customer);
                });

                yield return mapper.CompileMappingForAllExplicitAddedEntities();
            }
        }
    }
}