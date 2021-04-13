using System;
using System.IO;
using System.Text;
using System.Collections.Generic;



namespace Datos_V1._0._0
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] datos = new string[4];
            string[] outputs = new string[4] { "Ingrese el nombre", "Ingrese el apellido", "Ingrese la cedula", "Ingrese la edad" };
            string[] header = new string[] { "Nombre:", "Apellido:", "Cedula:", "Edad:" };

            string valorConfirmacion = "";
            string direccion = args[0];


            ProgramManager(datos, outputs, valorConfirmacion, direccion, header);
            // Aqui abajo terminar





        }


        public static string AñadirEspacio(int cantidadEspacios)
        {
            char[] y = new char[cantidadEspacios];
            string resultado = "";

            for (int x = 0; x < cantidadEspacios; x++)
            {
                y[x] = ' ';
                resultado += y[x];
            }



            return resultado;
        }

        public static void IntroducirValores(string[] datoss, string[] outputss, string valorConfirmacionn, string direction, string[] headerss)
        {


            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(outputss[i]);
                datoss[i] = Console.ReadLine();
            }


            Console.WriteLine("¿Desea que se guarden sus datos? Y/N");
            valorConfirmacionn = Console.ReadLine();

            if (valorConfirmacionn.ToUpper() == "Y")
            {
                EscribirValores(direction, headerss, datoss);
            }
            else if (valorConfirmacionn.ToUpper() == "N")
            {

                Console.WriteLine("No se guardarán sus datos");
            }


            Console.WriteLine("¿Desea introducir más datos? y/n");

            string reconfirmar = Console.ReadLine();

            if (reconfirmar == "y")
            {
                IntroducirValores(datoss, outputss, valorConfirmacionn, direction, headerss);
            }
            else if (reconfirmar == "n")
            {
                Console.WriteLine("Gracias por su visita");
                ProgramManager(datoss, outputss, valorConfirmacionn, direction, headerss);
            }

        }


        

        public static void EscribirValores(string direccionn, string[] headers, string[] datoss)
        {
           

            if (File.Exists(direccionn) == true)
            {

                using (StreamWriter writer = new StreamWriter(direccionn, append: true))
                {

                    
                    writer.Write(System.Environment.NewLine);

                    writer.Write(AñadirEspacio(3) + datoss[0] + ",");
                    for (int x = 0; x < 3; x++)
                    {
                        writer.Write(AñadirEspacio(3 + headers[x].Length - datoss[x].Length) + datoss[x + 1] + ",");

                    }

                }

            }
            else
            {
                File.WriteAllText(direccionn, "");


                using (StreamWriter writer = new StreamWriter(direccionn, append: true))
                {
                    for (int x = 0; x < 4; x++)
                    {
                        writer.Write("   " + headers[x]);

                    }


                   
                    writer.Write(System.Environment.NewLine);

                    writer.Write(AñadirEspacio(3) + datoss[0] + ",");

                    for (int y = 0; y < 3; y++)
                    {
                        writer.Write(AñadirEspacio(3 + headers[y].Length - datoss[y].Length) + datoss[y + 1] + ",");
                    }

                }

            }









        }


        public static void ProgramManager(string[] datoss, string[] outputss, string valorConfirmacionn, string direction, string[] headerss)
        {
            string opcionElegida = "";
            Console.WriteLine("¿Qué acción desea realizar?");
            Console.WriteLine("Introducir nuevos datos (a)");
            Console.WriteLine("Busqueda por cédula (b)");
            Console.WriteLine("Mostrar el registro completo (c)");
            Console.WriteLine("Editar Registro (d)");
            Console.WriteLine("Eliminar Registro (e)");
            Console.WriteLine("Salir (f)");

            opcionElegida = Console.ReadLine();

            switch (opcionElegida.ToUpper())
            {
                case "A":
                    IntroducirValores(datoss, outputss, valorConfirmacionn, direction, headerss);
                    break;

                case "B":
                    BusquedaCedula(direction);
                    ProgramManager(datoss, outputss, valorConfirmacionn, direction, headerss);
                    break;

                case "C":
                    MostrarRegistroCompleto(direction);
                    ProgramManager(datoss, outputss, valorConfirmacionn, direction, headerss);
                    break;

                case "D":
                    EditarRegistro(datoss, outputss, direction, valorConfirmacionn, headerss);
                    ProgramManager(datoss, outputss, valorConfirmacionn, direction, headerss);
                    break;

                case "E":
                    EliminarRegistro(direction);
                    ProgramManager(datoss, outputss, valorConfirmacionn, direction, headerss);
                    break;
                case "F":
                    Console.WriteLine("Gracias, vuelva pronto");
                    break;
            }
        }


        public static void BusquedaCedula(string direction)
        {

            Console.WriteLine("Introduzca el número de cédula de la persona que desea buscar");
            string x = Console.ReadLine();
            int linea = 0;
            string lineaStr = "";



            using (StreamReader reader = new StreamReader(direction))
            {


                foreach (string y in File.ReadAllLines(direction))
                {
                    lineaStr = y;


                    linea = reader.ReadLine().IndexOf(x);



                    if (linea > -1)
                    {
                        Console.WriteLine("Datos encontrados: " + lineaStr);
                    }
                }



            }
        }

        public static void MostrarRegistroCompleto(string direction)
        {
            using (StreamReader reader = new StreamReader(direction))
            {
                Console.WriteLine(reader.ReadToEnd());


            }
        }




        public static void EditarRegistro(string[] datoss, string[] outputss, string direction, string valorConfirmacionn, string[] headerss)
        {

            Console.WriteLine("Introduzca el número de cédula de la persona que desea editar");
            string x = Console.ReadLine();
            int linea = 0;
            
            List<string> fileString = new List<string>();


            using (StreamReader reader = new StreamReader(direction))
            {


                foreach (string y in File.ReadAllLines(direction))
                {

                    linea = reader.ReadLine().IndexOf(x);
                    if (linea > -1)
                    {
                        

                        IntroducirValoresParaEditar(datoss, outputss, valorConfirmacionn, headerss, fileString, y);
                    }
                    else
                    {
                        fileString.Add(y);
                    }

                }

                

            }



            using (StreamWriter writer = new StreamWriter(direction))
            {
                foreach (string v in fileString)
                {
                    writer.WriteLine(v);
                    Console.WriteLine(v);
                }
            }


        }


        public static void EliminarRegistro(string direction)
        {

            Console.WriteLine("Introduzca el número de cédula de la persona que desea eliminar");
            string x = Console.ReadLine();
            int linea = 0;
            
            List<string> fileString = new List<string>();


            using (StreamReader reader = new StreamReader(direction))
            {


                foreach (string y in File.ReadAllLines(direction))
                {

                    linea = reader.ReadLine().IndexOf(x);
                    if (linea > -1)
                    {

                        //No hacer nada
                        
                    }
                    else
                    {
                        fileString.Add(y);
                    }

                }

                

            }

            using (StreamWriter writer = new StreamWriter(direction))
            {
                foreach (string v in fileString)
                {
                    writer.WriteLine(v);
                    Console.WriteLine(v);
                }
            }

        }

        public static void IntroducirValoresParaEditar(string[] datoss, string[] outputss, string valorConfirmacionn, string[] headers, List<string> archivoEntero, string lineaPorDefecto)
        {
            string devuelta = "";

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(outputss[i]);
                datoss[i] = Console.ReadLine();
                
            }

            
            

                devuelta += AñadirEspacio(3) + datoss[0] + ",";
                for (int x = 0; x < 3; x++)
                {
                    devuelta += AñadirEspacio(3 + headers[x].Length - datoss[x].Length) + datoss[x + 1] + ",";

                }

            

            Console.WriteLine("¿Desea que se guarden sus datos? Y/N");
            valorConfirmacionn = Console.ReadLine();

            if (valorConfirmacionn.ToUpper() == "Y")
            {
                archivoEntero.Add(devuelta);
            }
            else if (valorConfirmacionn.ToUpper() == "N")
            {

                Console.WriteLine("No se guardarán los cambios");
                archivoEntero.Add(lineaPorDefecto);
                

            }

            
        }

        
    }
}
