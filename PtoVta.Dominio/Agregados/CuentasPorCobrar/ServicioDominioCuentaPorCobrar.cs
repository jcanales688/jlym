using System;
using System.Collections.Generic;
using System.Linq;
using PtoVta.Dominio.Agregados.Parametros;
using PtoVta.Dominio.Agregados.Ventas;

namespace PtoVta.Dominio.Agregados.CuentasPorCobrar 
{
    public class ServicioDominioCuentaPorCobrar: IServicioDominioCuentaPorCobrar
    {
        public DateTime ObtenerFechaVenceDocumentoCuentaPorCobrar(DateTime pFechaDocumentoVenta, Cliente pCliente, 
                                                                            CondicionPago pCondicionPago)
        {
            Dictionary<int, int> DiccionarioDiasSemanaPago = new Dictionary<int, int>();
            DateTime fechaDeVenceDocCC = new DateTime();
            int cantidadDiasDelMes = 0;
            int diaPivot = 0;

            //Si es que no encontro DiaPagID para el cliente
            if ((pCondicionPago.DiasPago < 0 || pCondicionPago.DiasPago == 0)
                || (pCliente.CodigoDiaDePago == null || string.IsNullOrEmpty(pCliente.CodigoDiaDePago)))
            {
                fechaDeVenceDocCC = pFechaDocumentoVenta.AddDays(pCondicionPago.DiasPago < 0 ? 0 : pCondicionPago.DiasPago);
            }
            else
            {
                //Verifica si tiene marcado el check de dias de Semana
                fechaDeVenceDocCC = pFechaDocumentoVenta.AddDays(pCondicionPago.DiasPago);

                if (pCliente.DiaDePago.EstadoSemana == -1)
                {
                    int diaSemana = 0;
                    int diasTotal = 0;
                    if (pCliente.DiaDePago.D1Lunes + pCliente.DiaDePago.D2Martes + pCliente.DiaDePago.D3Miercoles +
                        pCliente.DiaDePago.D4Jueves + pCliente.DiaDePago.D5Viernes + pCliente.DiaDePago.D6Sabado +
                        pCliente.DiaDePago.D7Domingo == 0)
                    {
                        return fechaDeVenceDocCC;
                    }

                    if (pCliente.DiaDePago.D7Domingo == -1) { DiccionarioDiasSemanaPago.Add(1, 0); }
                    if (pCliente.DiaDePago.D1Lunes == -1) { DiccionarioDiasSemanaPago.Add(2, 1); }
                    if (pCliente.DiaDePago.D2Martes == -1) { DiccionarioDiasSemanaPago.Add(3, 2); }
                    if (pCliente.DiaDePago.D3Miercoles == -1) { DiccionarioDiasSemanaPago.Add(4, 3); }
                    if (pCliente.DiaDePago.D4Jueves == -1) { DiccionarioDiasSemanaPago.Add(5, 4); }
                    if (pCliente.DiaDePago.D5Viernes == -1) { DiccionarioDiasSemanaPago.Add(6, 5); }
                    if (pCliente.DiaDePago.D6Sabado == -1) { DiccionarioDiasSemanaPago.Add(7, 6); }

                    //1er Caso, si el día de vencimiento conincide con el día de pago
                    int diaSemanaDeFechaVenceDocCC = Convert.ToInt16(fechaDeVenceDocCC.DayOfWeek) + 1; //por q los dias DayOfWeek empiezan desde Domingo = 0

                    var verificaDiaSemana = DiccionarioDiasSemanaPago.Where(k => k.Key == diaSemanaDeFechaVenceDocCC);
                    if (verificaDiaSemana.Count() != 0)
                    {
                        return fechaDeVenceDocCC;
                    }

                    //Buscando a la derecha
                    //diasTotal = Convert.ToInt16(DiccionarioDiasSemanaPago.Keys.Min(x => x > diaSemanaDeFechaVenceDocCC));
                    var selMenorDiaSemana = from dia in DiccionarioDiasSemanaPago
                                            where dia.Key > diaSemanaDeFechaVenceDocCC
                                            select dia;
                    var diaMin = new { valorMin = selMenorDiaSemana.Min(k => k.Key) };
                    diasTotal = diaMin.valorMin;

                    //Buscando a la Izquierda
                    if (diasTotal < 0 || diasTotal == 0)
                    {
                        diasTotal = DiccionarioDiasSemanaPago.Keys.Min();

                        fechaDeVenceDocCC =
                            fechaDeVenceDocCC.AddDays(diasTotal + 7 - diaSemanaDeFechaVenceDocCC);
                    }
                    else
                    {
                        fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(diasTotal - diaSemanaDeFechaVenceDocCC);

                    }

                }
                else
                {
                    /* Para Cuando se implemente dias de pago  >>>>*/
                    //CombinaDia1,CombinaDia2,CombinaDia3,CombinaDia4 deben de ser mayores que cero y
                    //menores<28,los demas dias deben ser considerados fin de mes.
                    if (pCliente.DiaDePago.CombinaDia1 + pCliente.DiaDePago.CombinaDia2 + pCliente.DiaDePago.CombinaDia3 == 0 &&
                        pCliente.DiaDePago.CombinaDia4 == 0)
                    {
                        return fechaDeVenceDocCC;
                    }

                    DiccionarioDiasSemanaPago.Add(1, (int)pCliente.DiaDePago.CombinaDia1);
                    DiccionarioDiasSemanaPago.Add(2, (int)pCliente.DiaDePago.CombinaDia2);
                    DiccionarioDiasSemanaPago.Add(3, (int)pCliente.DiaDePago.CombinaDia3);
                    if (pCliente.DiaDePago.CombinaDia4 == -1) { DiccionarioDiasSemanaPago.Add(4, 99); }

                    //1er Caso, si el d¡a de vencimiento coincide con el d¡a de pago
                    var dia = fechaDeVenceDocCC.Day;
                    var mes = fechaDeVenceDocCC.Month;
                    var año = fechaDeVenceDocCC.Year;

                    //Calcular la cantidad de dias del mes actual del vencimiento
                    switch (mes)
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            cantidadDiasDelMes = 31;
                            break;

                        default:
                            if (mes == 2)
                            {
                                if (año % 4 == 0)
                                {
                                    cantidadDiasDelMes = 29;
                                }
                                else
                                {
                                    cantidadDiasDelMes = 28;
                                }
                            }
                            else
                            {
                                cantidadDiasDelMes = 30;
                            }
                            break;
                    }


                    var diaVencimiento = DiccionarioDiasSemanaPago.Where(x => x.Value == dia && x.Value != 99);
                    if (diaVencimiento.Count() != 0)
                    {
                        return fechaDeVenceDocCC;
                    }
                    //FIN 1er Caso, si el dia de vencimiento conincide con un dia de pago

                    //Buscando el min valor, mayor que dia de vencimiento actual
                    ///Buscando a la derecha
                    diaPivot = Convert.ToInt16(DiccionarioDiasSemanaPago.Min(x => x.Value > dia));
                    ///Fin de Buscando a la Derecha

                    if (diaPivot != 0)
                    {
                        //si diapivot=99, entoces vence fin de mes actual
                        if (diaPivot != 99)
                        {
                            fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(diaPivot - dia);
                        }
                        else
                        {
                            fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(cantidadDiasDelMes - dia);
                        }
                    }
                    else
                    {
                        //Buscando a la Izquierda
                        diaPivot = Convert.ToInt16(DiccionarioDiasSemanaPago.Values.Min());

                        fechaDeVenceDocCC = fechaDeVenceDocCC.AddMonths(1);
                        fechaDeVenceDocCC = fechaDeVenceDocCC.AddDays(diaPivot - dia);
                    }

                }

            }

            return fechaDeVenceDocCC;
        }        
    }
}