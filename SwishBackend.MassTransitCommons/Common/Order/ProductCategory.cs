﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitCommons.Common.Order
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }

        // Navigation property for products in this category
    }
}
