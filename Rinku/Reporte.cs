using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rinku.Models;

namespace Rinku
{
    public class Reporte
    {

        public int cantEntregas { get; set; }
        public double pagoTotalEntregas { get; set; }
        public double pagoTotalBonos { get; set; }
        public double retenciones { get; set; }
        public double vales { get; set; }
        public double sueldoTotal { get; set; }


        private readonly RinkuContext context;

        public Reporte(RinkuContext context)
        {
            this.context = context;
        }

        public Reporte calculaRerporte()
        {


            return this;
        }


        public IEnumerable<Movimientos> calculaRerporte(int mesIni = 0, int mesFin = 0, int idEmp = 0)
        {
            var tabMovs = context.Movimientos.ToList();
            IEnumerable<Movimientos> movs;

            movs = from m in tabMovs orderby m.NumEmpleado ascending, m.Mes ascending select m;
            var movsTemp = movs;

            if (mesIni != 0)
            {
                movsTemp = movs;
                if (mesFin != 0)
                {
                    movs = from m in movsTemp
                           where m.Mes >= mesIni && m.Mes <= mesFin
                           orderby m.NumEmpleado ascending, m.Mes ascending
                           select m;
                }
                else
                {
                    movs = from m in movsTemp
                           where m.Mes == mesIni
                           orderby m.NumEmpleado ascending, m.Mes ascending
                           select m;
                }
            }


            if (idEmp != 0)
            {
                movsTemp = movs;
                movs = from m in movsTemp
                       where int.Parse(m.NumEmpleado) == idEmp
                       orderby m.NumEmpleado ascending, m.Mes ascending
                       select m;
            }


            //return this;
            return movs;
        }



    }
}