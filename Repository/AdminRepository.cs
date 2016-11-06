﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(BookEntities db) : base(db) { }

        public Admin SelectByEmail(string value)
        {
            return SelectBy(o => o.Email == value).FirstOrDefault();
        }
    }
}