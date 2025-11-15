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
            bool p1 = false; //variable para comprobar si se ha enconrrado (
            bool p2 = false; //variable para comprobar si se ha encontrado )
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

                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '(' || c == ')') //Compros si ele caracter es un operador
                {
                    operador = c; //cargamos c en la variable operando

                    if (operador == '(')
                    {
                        p1 = true; //Indicamos que hemos encontrado el inicio del parentesis
                    }
                    else if (operador == ')')
                    {
                        p2 = true; //Indicamos que hemos encontrado el final del parentesis
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

                            if (prioridad1 >= prioridad2) //Si el orden de prioridad del que entra es mayor o igual al que entro anteriormente
                            {
                                Pila.Push(operador.ToString()); //Introducimos el operador en la pila

                            }
                            else //En caso contrario
                            {
                                while (Pila.Count != 0 && prioridad1 < prioridad2) //Mientras haya operadores en la pila y la prioridad del elemento que entra es menor al elemento añadido anteriormente
                                {
                                    Cola.Enqueue(Pila.Pop().ToString()); //Desapilamos el operador con el que estamos comparado y lo metemos a la cola

                                    if (Pila.Count != 0) //comprobamos si aún hay elementos en la pila
                                    {
                                        switch (Pila.Peek()) //Comprobamos la prioridad del siguiente operador
                                        {
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
                                    }
                                }
                                Pila.Push(operador.ToString()); //Introducimos el operador en la pila si no quedan más operadores en la pila
                            }

                        }
                        else//Si la pila no tiene ningún valor, hacemos esto
                        {
                            Pila.Push(operador.ToString()); //Introducimos el operador
                        }
                    }
                    if (p1 && p2) //Comprobamos si existe '(' y ')'
                    {
                        while (Pila.Count != 0) //Mientras halla operadores en la pila
                        {
                            Cola.Enqueue(Pila.Pop().ToString()); //Desapilamos todos los valores que hay en la pila y los pasamos a la cola
                        }

                        p1 = p2 = false; //Reseteamos los valores de p1 y p2
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
                
                for(int i = 0; i < col.Length; i++)
                {
                    ColaAux.Enqueue(col[i]); //Para evitar perder datos en proceso del cálculo, pasamos los valores de la cola principal a la cola auxiliar
                }

                Pila.Clear(); //Limpiamos la pila por si acaso

                while (Cola.Count > 0)
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
                                Pila.Push(a / b); 
                                break;      
                            case "^": 
                                Pila.Push((int)Math.Pow(a, b)); 
                                break;
                        }
                    }
                }

                resultado = (int)Pila.Pop(); // Guardar el resultado en la variable resultado

                Console.Write("SUFIJO: "); //Imprimimos la operación en formato Sufijo
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