using System;
using System.IO;
using System.Collections;
using System.Linq.Expressions;

namespace Modulo_adicional_2
{
    public class Program
    {
        public static void Main()
        {
            int numero; //variable para guadar los números
            char operador; //variable para guardar los operadores (+,-,/,*,^)
            char caracter; //variable para guardar las letras
            int resultado = 0; //Variable para almacenar el resultado
            int prioridad1 = 0; //variable para determinar el orden de prioridad del opererador que se va a introducir a pila
            int prioridad2 = 0; //variable para saber el orden de prioridad del último operador introducuido
            bool imp = true; //variable para comprobar si no hay nada ilegal en la operación introducida
            int Z = 90; //variable que contiene el valor ASCII de Z
            char[] operacion; //Array de caracteres donde se guardara la operación introducida
            Queue Cola = new Queue(); //Crear Cola
            Queue ColaAux = new Queue(); //Cola Auxiliar
            Stack Pila = new Stack(); //Crear pila

            Console.Write("Introduzca una operación: "); //Pedir al usuario que introduzca una operación
            operacion = Console.ReadLine().ToCharArray(); //Convertir el string al array de caracteres

            Pila.Clear(); //Limpiamos la pila 
            Cola.Clear(); //Limpiamos la cola
            ColaAux.Clear(); //Limpiamos la cola auxiliar
            for (int i = 0; i < operacion.Length; i++) //Bucle for para acceder a cada caracter del array
            {
                char c = operacion[i]; //Guardamos el caracter actual en c

                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '(' || c == ')') //Comprouebo si el caracter es un operador
                {
                    operador = c; //cargamos c en la variable operador

                    if (operador == '(')
                    {
                        Pila.Push(operador.ToString());
                    }
                    else if (operador == ')')
                    {
                        while ((string)Pila.Peek() != "(")
                        {
                            Cola.Enqueue(Pila.Pop());
                        }

                        Pila.Pop(); // eliminar '('
                    }
                    else
                    {
                        if (Pila.Count != 0) //comprobamos si la pila tiene algún elemento ya
                        {
                            switch (operador) //Determinamos la prioridad del operador que se va a introducir a la pila
                            {
                                case '+':
                                    prioridad1 = 1;
                                    break;
                                case '-':
                                    prioridad1 = 1;
                                    break;
                                case '*':
                                    prioridad1 = 2;
                                    break;
                                case '/':
                                    prioridad1 = 2;
                                    break;
                                case '^':
                                    prioridad1 = 3;
                                    break;
                            }

                            switch (Pila.Peek()) //Comprobamos la prioridad del del último operador introducido
                            {
                                case '(':
                                    prioridad2 = 0;
                                    break;
                                case '+':
                                    prioridad2 = 1;
                                    break;
                                case '-':
                                    prioridad2 = 1;
                                    break;
                                case '*':
                                    prioridad2 = 2;
                                    break;
                                case '/':
                                    prioridad2 = 2;
                                    break;
                                case '^':
                                    prioridad2 = 3;
                                    break;
                            }

                            while (Pila.Count != 0 && Pila.Peek().ToString() != "(") //Siempre que la pila tenga algún elemento y que ese elemento sea distinto de '(' ya que siempre podré meter a la pila cualquier operador mientras la pila esté vacía o el elemento en la cima sea '('
                            {
                                switch ((string)Pila.Peek()) //Busco por el operador que hay actualmente en la pila
                                {
                                    case "+": //Si es + o -, prioridad 1
                                    case "-":
                                        prioridad2 = 1; break;
                                    case "*": //Si es * o /, prioridad 2
                                    case "/":
                                        prioridad2 = 2; break;
                                    case "^": //Si es ^ prioridad 3
                                        prioridad2 = 3; break;
                                }

                                
                                if (prioridad2 >= prioridad1) //Si la prioridad del segundo es mayor o igual que el que intento meter, meto el operador en la cima a la cola
                                {
                                    Cola.Enqueue(Pila.Pop());
                                }
                                else //Si el operador que intento apilar tiene mayor prioridad simplemente lo apilo
                                {
                                    break;
                                }
                            }
                            Pila.Push(operador.ToString()); //Apilo el operador

                        }
                        else//Si la pila no tiene ningún valor, no hace falta comparar y lo introducimos directamente
                        {
                            Pila.Push(operador.ToString()); //Introducimos el operador
                        }
                    }
                }
                else if (char.IsLetter(c)) //Comprobamos si el caracter es una letra
                {
                    caracter = c; //Guardamos c en caracter
                    Cola.Enqueue(caracter.ToString()); //Encolamos la letra
                }
                else if (char.IsDigit(c)) //Comprobamos si el caracter es un número
                {
                    numero = c - '0'; //Restamos el valor ASCII de lo que haya en c y el valor ASCII de 0. El resultado nos dará el número contenido en c en entero. Lo guardamos en número
                    if (i + 1 < operacion.Length && char.IsDigit(operacion[i + 1])) //Si el siguiente caracter al número no es NULL y además es un número
                    {
                        i++; //Pasamos al siguiente caracter
                        numero = numero * 10 + (operacion[i] - '0'); //Movemos el número a la izquierda y le sumamos el nuevo número. El resultado se guarda en numero
                    }

                    if (numero >= 0 && numero < 10) //Comrpobamos que el número se menor a 10 y mayor que -1
                    {
                        Cola.Enqueue(numero.ToString()); //Si se cumple, se encola el número
                    }
                    else //Si no se cumple
                    {
                        Console.WriteLine($"El número {numero} es mayor a 9"); //Indicamos que esta operación es ilegal
                        imp = false; //No se imprime resultado en sufijo
                    }
                }
                else if (c == ' ')
                {
                    continue;
                }
                else //Si no es ninguno de los anteriores, es un caracter ilegal y no se continua con la operación
                {
                    Console.WriteLine($"El valor '{c}' no es válido para realizar la operación"); //Imprimimos que el caracter c es ilegal
                    imp = false; //No imprimimos resultado
                }
            }

            if (imp) //imprimimos el resultado en sufijo solo si no hay nada ilegal en la operación dada
            {
                if (Pila.Count != 0) //Comprobar si aún hay valores en la pila
                {
                    while (Pila.Count != 0)
                    {
                        Cola.Enqueue(Pila.Pop()); //Sacar los valores que aun hay en la pila y meterlos en la cola
                    }
                }

                object[] col = Cola.ToArray(); //Convertimos la Cola principal en un array

                for (int i = 0; i < col.Length; i++)
                {
                    ColaAux.Enqueue(col[i]); //Para evitar perder datos en proceso del cálculo, pasamos los valores de la cola principal a la cola auxiliar
                }

                Pila.Clear(); //Limpiamos la pila por si acaso

                while (Cola.Count > 0 && imp)
                {
                    string c = (string)Cola.Dequeue(); // Sacar el primer elemento

                    if (char.IsLetter(c[0])) //  Comprobar si es una letra
                    {
                        Pila.Push((int)c[0] - (int)Z); // Valor de la letra en ASCII - Z
                    }
                    else if (char.IsDigit(c[0])) // Comprobar si es un número
                    {
                        Pila.Push(int.Parse(c));
                    }
                    else // Si es un operador
                    {
                        int b = (int)Pila.Pop();
                        int a = (int)Pila.Pop();
                        switch (c) //Comprobamos que operador es c para que lleve acabo una operación concreta
                        {
                            case "+":
                                Pila.Push(a + b);
                                break;
                            case "-":
                                Pila.Push(a - b);
                                break;
                            case "*":
                                Pila.Push(a * b);
                                break;
                            case "/":
                                if (b == 0)
                                {
                                    Console.WriteLine("División entre 0, no es posible continuar");
                                    imp = false;
                                    break;
                                }
                                else
                                {
                                    Pila.Push(a / b);
                                }
                                break;
                            case "^":
                                Pila.Push((int)Math.Pow(a, b));
                                break;
                        }
                    }
                }

                if (imp)
                {
                    resultado = (int)Pila.Pop(); // Guardar el resultado en la variable resultado

                    Console.Write("POSTFIJO: "); //Imprimimos la operación en formato Sufijo
                    while (ColaAux.Count != 0) //Mientras haya elementos en la cola auxiliar
                    {
                        Console.Write($"{ColaAux.Dequeue()} "); //Imprimimos los valores
                    }
                    Console.Write("\n");
                    Console.Write($"Resultado = {resultado}"); //Imprimimos el resultado
                }
            }

        }
    }
}