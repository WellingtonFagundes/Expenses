﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RegrasBLL : CriptografiaBLL
    {
        public Contexto db;

        public RegrasBLL()
        {
            db = new Contexto();
        }

    }
}
