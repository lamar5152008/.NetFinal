﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace DataAccessInterface
{
    public interface TransactionAccessorInterface
    {
        public int insert(Transaction transaction);
    }
}
