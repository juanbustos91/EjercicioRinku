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


        public Reporte()
        {


        }

        public Reporte calculaRerporte()
        {


            return this;
        }


        public Reporte calculaRerporte(int idEmp = 0, int mes = 0)
        {
            

            return this;
        }



    }
}
