using System;
using System.IO;
using System.Text;
namespace V1._0._0
{
    class Program
    {
        static void Main(string[] args)
        {
            string direccion = @"C:\Users\user\Desktop\V1.0.0\datos.txt";
            string[] datos = new string[4];
            string[] header = { "Nombre", "Apellido", "Cedula", "Edad" };
            string[] outputs = { "Ingrese el nombre", "Ingrese el apellido", "Ingrese la cedula", "Ingrese la edad" };
            bool confirmacion = false;
            string datosConfirmacion = "";

            for(int x = 0; x < 4; x++)
            {
                Console.WriteLine(outputs[x]);
                datos[x] = Console.ReadLine();
            }

            Console.WriteLine("¿Desea guardar sus datos? y/n");

            datosConfirmacion = Console.ReadLine();

            if(datosConfirmacion == "y")
            {
                confirmacion = true;
            }
            else if(datosConfirmacion == "n")
            {
                confirmacion = false;
            }

            if(confirmacion == true)
            {

                if(File.Exists(direccion) == true)
                {
                    using (StreamWriter writer = new StreamWriter(direccion, true))
                    {
                        writer.Write(System.Environment.NewLine);

                        writer.Write(AñadirEspacio(3) + datos[0]);

                        for (int x = 0; x < 3; x++)
                        {
                            writer.Write(AñadirEspacio(3 + header[x].Length - datos[x].Length) + datos[x + 1]);
                        }
                    }
                }
                else
                {
                    
                    
                        File.WriteAllText(direccion, "");



                        using (StreamWriter writer = new StreamWriter(direccion, true))
                        {

                            for(int y = 0; y < 4; y++)
                            {
                                writer.Write("   " + header[y]);
                            }
                            writer.Write(System.Environment.NewLine);

                            writer.Write(AñadirEspacio(3) + datos[0]);

                            for (int x = 0; x < 3; x++)
                            {
                                writer.Write(AñadirEspacio(3 + header[x].Length - datos[x].Length) + datos[x + 1]);
                            }
                        }
                    
                }
                
            }
            else
            {
                Console.WriteLine("Gracias por su visita");
            }


        
        }


        public static string AñadirEspacio(int numEspacio)
        {
            char[] espacios = new char[numEspacio];

            string cEspacios = "";

            for(int x = 0; x < numEspacio; x++)
            {
                espacios[x] = ' ';
                cEspacios += espacios[x];
            }

            return cEspacios;
            
        }
    }
}
