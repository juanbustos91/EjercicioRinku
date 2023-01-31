﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rinku.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Rinku.Models
{
    public partial interface IRinkuContextProcedures
    {
        Task<int> spActualizarEmpleadoAsync(int? id, string numero, string nombre, int? rol, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> spActualizarMovimientosAsync(int? id, string numEmpleado, int? mes, int? cantidadEntregas, decimal? sueldoBruto, decimal? isr, decimal? isrAdicional, decimal? vales, decimal? sueldoNeto, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> spInsertaEmpleadoAsync(string numero, string nombre, int? rol, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> spInsertaMovimientosAsync(string numEmpleado, int? mes, int? cantidadEntregas, decimal? sueldoBruto, decimal? isr, decimal? isrAdicional, decimal? vales, decimal? sueldoNeto, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}