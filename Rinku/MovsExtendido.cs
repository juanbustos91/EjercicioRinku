using Microsoft.EntityFrameworkCore;
using Rinku.Models;

namespace Rinku
{
    public class MovsExtendido : Movimientos
    {
        public MovsExtendido(Movimientos movs) { 
            this.Id= movs.Id;
            this.NumEmpleado= movs.NumEmpleado;
            this.Mes = movs.Mes;
            this.CantidadEntregas= movs.CantidadEntregas;
            this.SueldoBruto= movs.SueldoBruto;
            this.Isr = movs.Isr;
            this.IsrAdicional= movs.IsrAdicional;
            this.Vales= movs.Vales;
            this.SueldoNeto= movs.SueldoNeto;

        }

        public void calcularSueldo(int id, Empleados emp)
        {
            var _mes = this.Mes;

            int horasxmes = 8 * 6 * 4;
            int sueldoBase = 30 * horasxmes;
            int sumaComisionEntregas = this.CantidadEntregas * 5;
            int bono = 0;

            switch (emp.Rol)
            {
                case 1: // Chofer. Bono $10 x hora
                    bono = horasxmes* 10;
                    break;

                case 2: // Cargador. Bono $5 x hora
                    bono = horasxmes* 5;
                    break;

                case 3: // Auxiliar. Bono $10 x hora
                    bono = 0;
                    break;

                default:
                    break;
            }

            this.SueldoBruto = (decimal)(sueldoBase + sumaComisionEntregas + bono);
            this.Isr = (decimal)((double)this.SueldoBruto * .09);

            double subTotal = (double)(this.SueldoBruto - this.Isr);

            if (subTotal > 10000)
            {
                this.IsrAdicional = (decimal)(subTotal * .03);
            }
            else
            {
                this.IsrAdicional = 0;
            }


            this.Vales = (decimal)((double)this.SueldoBruto * .04);
            this.SueldoNeto = this.SueldoBruto - this.Isr - this.IsrAdicional + this.Vales;

        }
    }
}
