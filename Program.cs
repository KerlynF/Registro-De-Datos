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

            string[] datos = new string[7];
            string[] outputs = new string[7] { "Ingrese el nombre", "Ingrese el apellido", "Ingrese la cedula", "Ingrese la edad", "Ingrese los ahorros", "Ingrese la contraseña", "Confirme la contraseña" };
            string[] header = new string[] { "Nombre:", "Apellido:", "Cedula:", "Edad:" , "Ahorros:"};

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


            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine(outputss[i]);
                if (i == 0 || i == 1)
                {
                    datoss[i] = IntroducirNombre();
                    Console.Write(System.Environment.NewLine);
                }
                else if(i == 2 || i == 3)
                {
                    datoss[i] = IntroducirEdad();
                    Console.Write(System.Environment.NewLine);
                }
                else if (i == 4)
                {
                    datoss[i] = IntroducirAhorros();
                    Console.Write(System.Environment.NewLine);
                }
                else if(i == 5 ||i == 6)
                {
                    datoss[i] = IntroducirPassword();
                    Console.Write(System.Environment.NewLine);
                }

            }


           

            if (datoss[5] == datoss[6])
            {
                EscribirValores(direction, headerss, datoss);
            }
            else
            {
                Console.WriteLine("Contraseña incorrecta, no se guardarán sus datos");
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
                    for (int x = 0; x < 4; x++)
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
                    for (int x = 0; x < 5; x++)
                    {
                        writer.Write("   " + headers[x]);

                    }



                    writer.Write(System.Environment.NewLine);

                    writer.Write(AñadirEspacio(3) + datoss[0] + ",");

                    for (int y = 0; y < 4; y++)
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

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine(outputss[i]);
                if (i == 0 || i == 1)
                {
                    datoss[i] = IntroducirNombre();
                    Console.Write(System.Environment.NewLine);
                }
                else if (i == 2 || i == 3)
                {
                    datoss[i] = IntroducirEdad();
                    Console.Write(System.Environment.NewLine);
                }
                else if (i == 4)
                {
                    datoss[i] = IntroducirAhorros();
                    Console.Write(System.Environment.NewLine);
                }
                else if(i == 5 || i == 6)
                {
                    datoss[i] = IntroducirPassword();
                    Console.Write(System.Environment.NewLine);
                }

            }




            devuelta += AñadirEspacio(3) + datoss[0] + ",";
            for (int x = 0; x < 4; x++)
            {
                devuelta += AñadirEspacio(3 + headers[x].Length - datoss[x].Length) + datoss[x + 1] + ",";

            }



            

            if (datoss[5] == datoss[6])
            {
                archivoEntero.Add(devuelta);
            }
            else
            {

                Console.WriteLine("Contraseña incorrecta, no se guardarán los cambios");
                archivoEntero.Add(lineaPorDefecto);


            }


        }

        public static string IntroducirNombre()
        {
            string valorDevuelto = "";
            List<char> nombre = new List<char>();
            ConsoleKeyInfo x = new ConsoleKeyInfo();

            for (x = Console.ReadKey(true); x.Key != ConsoleKey.Enter; x = Console.ReadKey(true))
            {
                if (x.Key == ConsoleKey.Backspace)
                {
                    Stream h = Console.OpenStandardOutput();

                    using (StreamWriter g = new StreamWriter(h))
                    {
                        g.Write("\b \b");
                        if (nombre.Count != 0)
                        {
                            nombre.RemoveAt(nombre.Count - 1);
                        }



                    }

                }
                else
                {
                    if (x.Key != ConsoleKey.D0 && x.Key != ConsoleKey.D1 && x.Key != ConsoleKey.D2 && x.Key != ConsoleKey.D3 && x.Key != ConsoleKey.D4 && x.Key != ConsoleKey.D5 && x.Key != ConsoleKey.D6
                        && x.Key != ConsoleKey.D7 && x.Key != ConsoleKey.D8 && x.Key != ConsoleKey.D9)
                    {
                        Console.Write(x.KeyChar);


                        nombre.Add(x.KeyChar);
                    }







                }

            }


            foreach (char z in nombre)
            {
                valorDevuelto += z;
            }

            return valorDevuelto;


        }

        public static string IntroducirPassword()
        {

            string valorDevuelto = "";
            List<char> password = new List<char>();
            ConsoleKeyInfo x = new ConsoleKeyInfo();

            for (x = Console.ReadKey(true); x.Key != ConsoleKey.Enter; x = Console.ReadKey(true))
            {
                if (x.Key == ConsoleKey.Backspace)
                {
                    Stream h = Console.OpenStandardOutput();

                    using (StreamWriter g = new StreamWriter(h))
                    {
                        g.Write("\b \b");
                        if (password.Count != 0)
                        {
                            password.RemoveAt(password.Count - 1);
                        }



                    }

                }
                else
                {
                    Console.Write('*');


                    password.Add(x.KeyChar);






                }

            }


            foreach (char z in password)
            {
                valorDevuelto += z;
            }

            return valorDevuelto;

        }

        public static string IntroducirEdad()
        {
            ConsoleKeyInfo x = new ConsoleKeyInfo();
            List<char> edadChar = new List<char>();
            string edad = "";
            for (x = Console.ReadKey(true); x.Key != ConsoleKey.Enter; x = Console.ReadKey(true))
            {
                switch (x.Key)
                {
                    case ConsoleKey.D0:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;
                    case ConsoleKey.D1:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D2:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D3:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D4:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D5:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D6:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D7:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D8:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.D9:
                        Console.Write(x.KeyChar);
                        edadChar.Add(x.KeyChar);
                        break;

                    case ConsoleKey.Backspace:
                        Console.Write("\b \b");
                        if (edadChar.Count != 0)
                        {
                            edadChar.RemoveAt(edadChar.Count - 1);
                        }

                        break;


                }

                
            }

            foreach (char y in edadChar)
            {
                edad += y;
            }


            return edad;
        }


        public static string IntroducirAhorros()
        {
            string ahorrosStr = "";
            double ahorros = 0f;
            int contadorPunto = 0;
            char datoAnterior = ' ';

            Stream x = Console.OpenStandardOutput();

            using (StreamWriter h = new StreamWriter(x))
            {
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

                for (keyInfo = Console.ReadKey(true); keyInfo.Key != ConsoleKey.Enter; keyInfo = Console.ReadKey(true))
                {
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        ahorrosStr.Remove(ahorrosStr.Length - 1);
                        Console.Write("\b \b");

                        if (datoAnterior == '.')
                        {
                            contadorPunto = 0;
                        }
                    }
                    else
                    {
                        if (keyInfo.KeyChar == '.' && contadorPunto != 1)
                        {
                            contadorPunto = 1;
                            Console.Write('.');
                            datoAnterior = keyInfo.KeyChar;
                            ahorrosStr += keyInfo.KeyChar.ToString();
                        }

                        if (contadorPunto != 1)
                        {
                            Console.Write(keyInfo.KeyChar);
                            ahorrosStr += keyInfo.KeyChar.ToString();
                            datoAnterior = keyInfo.KeyChar;

                        }
                        else
                        {
                            if (keyInfo.KeyChar == '.')
                            {

                                // No hacer nada 
                            }
                            else
                            {
                                Console.Write(keyInfo.KeyChar);
                                ahorrosStr += keyInfo.KeyChar.ToString();
                                datoAnterior = keyInfo.KeyChar;
                            }
                        }
                    }
                }
            }

            ahorros = double.Parse(ahorrosStr);
            return "RD: " + Math.Round(ahorros, 2).ToString();
        }
    }
}
