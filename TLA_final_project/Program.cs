using System;
using System.Collections.Generic;
using System.Linq;

namespace TLA_final_project
{
    class Program
    {
        string variable, symbols;

        static void Main(string[] args)
        {
            Program program = new Program();
            int number = int.Parse(Console.ReadLine());
            string line, variable_part, second_part;
            string[] parts, parts11, parts12;
            List<string> variables = new List<string>();
            List<char> symbols = new List<char>();
            List<Transitions> transitions = new List<Transitions>();
            string[] transition_parts;
            string[] holder;
            string[] holder2, holder3;
            string tempo1, tenmpo2, tempo3;
            List<string> lambda_ending = new List<string>();
            Dictionary<string, bool> reachable = new Dictionary<string, bool>();
            List<Transitions> variable_transitions = new List<Transitions>();
            Dictionary<string, List<Transitions>> variable_list = new Dictionary<string, List<Transitions>>();
            Dictionary<string, bool> Sreachable = new Dictionary<string, bool>();
            Dictionary<string, string> symbol_transition = new Dictionary<string, string>();
            List<string> temp = new List<string>();
            List<Grammer> grammers = new List<Grammer>();
            string input;
            string result;
            //string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < number; i++)
            {
                parts = Console.ReadLine().Split('-');
                if(parts.Length > 2)
                {
                    for(int j = 2; j < parts.Length; j++)
                    {
                        parts[1] += "-" + parts[j];
                    }
                }
                parts[1] = parts[1].Remove(0, 1);
                variable_part = parts[0].Split(' ')[0];
                program.variable = variable_part;
                variables.Add(variable_part);
                transition_parts = parts[1].Split('|');
                for (int j = 0; j < transition_parts.Length; j++)
                {
                    holder = transition_parts[j].Split(' ');
                    second_part = holder[1];
                    for (int z = 0; z < second_part.Length; z++)
                    {
                        if (second_part[z] == 'a' || second_part[z] == 'b' || second_part[z] == 'c' || second_part[z] == 'd' || second_part[z] == 'e' ||
                             second_part[z] == 'f' || second_part[z] == 'g' || second_part[z] == 'h' || second_part[z] == 'i' || second_part[z] == 'j' ||
                              second_part[z] == 'k' || second_part[z] == 'l' || second_part[z] == 'm' || second_part[z] == 'n' || second_part[z] == 'o' ||
                               second_part[z] == 'p' || second_part[z] == 'q' || second_part[z] == 'r' || second_part[z] == 's' || second_part[z] == 't' ||
                                second_part[z] == 'u' || second_part[z] == 'v' || second_part[z] == 'w' || second_part[z] == 'x' || second_part[z] == 'y' ||
                                 second_part[z] == 'z' || second_part[z] == '1' || second_part[z] == '2' || second_part[z] == '3' || second_part[z] == '4'
                                 || second_part[z] == '5' || second_part[z] == '6' || second_part[z] == '7' || second_part[z] == '8' || second_part[z] == '9'
                                 || second_part[z] == '0' || second_part[z] == '^' || second_part[z] == '*' || second_part[z] == '+' || second_part[z] == '=' ||
                                  second_part[z] == '!' || second_part[z] == '@' || second_part[z] == '$' || second_part[z] == '%' ||
                                   second_part[z] == '&' || second_part[z] == '-' || second_part[z] == '/' || second_part[z] == '\\' || second_part[z] == '_')
                        {
                            if (!symbols.Contains(second_part[z]))
                            {
                                symbols.Add(second_part[z]);
                            }
                        }
                    }

                    Transitions transition = new Transitions();
                    transition.start = variable_part;
                    transition.end = second_part;
                    transitions.Add(transition);
                }
            }

            input = Console.ReadLine();

            /*for(int i = 0; i < transitions.Count; i++)
            {
                Console.WriteLine(transitions[i].start + " --> " + transitions[i].end);
            }

            for(int i = 0; i < variables.Count; i++)
            {
                Console.Write(variables[i] + " ");
            }
            Console.WriteLine();

            for(int i = 0; i < symbols.Count; i++)
            {
                Console.Write(symbols[i] + " ");
            }
            Console.WriteLine();*/

            //lambda expression
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].end == "#")
                {
                    /*for(int j = 0; j < transitions.Count; j++)
                    {
                        if(transitions[j].start == transitions[i].start && transitions[j].end != "#")
                        {
                            lambda_ending.Add(transitions[j].end);
                        } 
                    }*/

                    for (int z = 0; z < transitions.Count; z++)
                    {
                        if (transitions[z].end.Contains(transitions[i].start))
                        {
                            if(transitions[z].end.Length > 5)
                            {
                                Transitions newTransition = new Transitions();
                                newTransition.start = transitions[z].start;
                                string temporary = transitions[i].start.Remove(0, 1);
                                temporary = temporary.Remove(temporary.Length - 1);
                                holder = transitions[z].end.Split(temporary[0]);
                                tempo3 = holder[0].Remove(holder[0].Length - 1);
                                tempo1 = holder[1].Remove(0,1);
                                if (holder != null && holder.Length != 0)
                                {
                                    newTransition.end = tempo3 + tempo1;
                                    transitions.Add(newTransition);
                                    /*for (int k = 0; k < lambda_ending.Count; k++)
                                    {
                                        Transitions newTransition2 = new Transitions();
                                        newTransition2.start = transitions[z].start;
                                        newTransition2.end = holder[0] + lambda_ending[k] + holder[1];
                                    }*/
                                }
                            }
                            else
                            {
                                Transitions newTransition = new Transitions();
                                newTransition.start = transitions[z].start;
                                holder = transitions[z].end.Split('<');
                                holder2 = holder[1].Split('>');
                                if (holder != null && holder.Length != 0)
                                {
                                    newTransition.end = holder[0] + holder2[1];
                                    transitions.Add(newTransition);


                                    /*for (int k = 0; k < lambda_ending.Count; k++)
                                    {
                                        Transitions newTransition2 = new Transitions();
                                        newTransition2.start = transitions[z].start;
                                        newTransition2.end = holder[0] + lambda_ending[k] + holder[1];
                                    }*/
                                }
                            }
                            
                        }
                    }

                    //lambda_ending.Clear();
                    transitions.RemoveAt(i);
                }





            }

            for(int i = 0; i < transitions.Count; i++)
            {
                if(transitions[i].end == "" || transitions[i].end == "<" || transitions[i].end == ">" || transitions[i].end == " >"
                    || transitions[i].end == "> " || transitions[i].end == " <" || transitions[i].end == "< ")
                {
                    transitions.RemoveAt(i);
                    i--;
                }
            }

            /*for (int i = 0; i < transitions.Count; i++)
            {
                Console.WriteLine(transitions[i].start + " --> " + transitions[i].end);
            }

            Console.WriteLine();
            Console.WriteLine("lambda transition accured succesfully.");
            Console.WriteLine();*/

            //unit prodoctions
            int tnumber = transitions.Count;
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].end == "<A>" || transitions[i].end == "<B>" || transitions[i].end == "<C>" || transitions[i].end == "<D>" ||
                     transitions[i].end == "<E>" || transitions[i].end == "<F>" || transitions[i].end == "<G>" ||
                      transitions[i].end == "<H>" || transitions[i].end == "<I>" || transitions[i].end == "<J>" ||
                       transitions[i].end == "<K>" || transitions[i].end == "<L>" || transitions[i].end == "<M>" ||
                        transitions[i].end == "<N>" || transitions[i].end == "<O>" || transitions[i].end == "<P>" ||
                         transitions[i].end == "<Q>" || transitions[i].end == "<R>" || transitions[i].end == "<S>" ||
                          transitions[i].end == "<T>" || transitions[i].end == "<U>" || transitions[i].end == "<V>" ||
                           transitions[i].end == "<W>" || transitions[i].end == "<X>" || transitions[i].end == "<Y>" ||
                            transitions[i].end == "<Z>")
                {
                    string variable = transitions[i].end;
                    /*if(variable == transitions[i].start)
                    {
                        continue;
                    }*/

                    for (int j = 0; j < tnumber; j++)
                    {
                        if (variable == transitions[j].start)
                        {
                            Transitions newTransition = new Transitions();
                            newTransition.start = transitions[i].start;
                            newTransition.end = transitions[j].end;
                            transitions.Add(newTransition);
                        }
                    }

                    transitions.RemoveAt(i);
                    i--;
                }



            }

            /*for (int i = 0; i < transitions.Count; i++)
            {
                Console.WriteLine(transitions[i].start + " --> " + transitions[i].end);
            }

            Console.WriteLine();
            Console.WriteLine("removing unitProductions accured succesfully.");
            Console.WriteLine();*/

            tnumber = transitions.Count;
            for(int i = 0; i < transitions.Count; i++)
            {
                for(int j = i + 1; j < transitions.Count; j++)
                {
                    if(transitions[i].start == transitions[j].start && transitions[i].end == transitions[j].end)
                    {
                        transitions.RemoveAt(j);
                        j--;
                    }
                }
            }

            /*for (int i = 0; i < transitions.Count; i++)
            {
                Console.WriteLine(transitions[i].start + " --> " + transitions[i].end);
            }

            Console.WriteLine();
            Console.WriteLine("removing same rules accured succesfully.");
            Console.WriteLine();*/

            //useless
            for (int i = 0; i < variables.Count; i++)
            {
                for (int j = 0; j < transitions.Count; j++)
                {
                    if (transitions[j].start == variables[i])
                    {
                        variable_transitions.Add(transitions[j]);
                    }
                }

                variable_list.Add(variables[i], variable_transitions);
            }

            for (int i = 0; i < variables.Count; i++)
            {
                for (int j = 0; j < variable_list.Count; j++)
                {
                    if (variables[i] == variable_list.ElementAt(j).Key)
                    {
                        for (int z = 0; z < variable_list.ElementAt(j).Value.Count; z++)
                        {
                            for (int k = 0; k < symbols.Count; k++)
                            {
                                if (variable_list.ElementAt(j).Value[z].end[variable_list.ElementAt(j).Value[z].end.Length - 1] == symbols[k])
                                {
                                    reachable[variables[i]] = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < variables.Count; i++)
            {
                for (int j = 0; j < variable_list.Count; j++)
                {
                    if (variables[i] == variable_list.ElementAt(j).Key)
                    {
                        for (int z = 0; z < variable_list.ElementAt(j).Value.Count; z++)
                        {
                            for (int k = 0; k < variables.Count; k++)
                            {
                                if (variable_list.ElementAt(j).Value[z].end.Length > 1)
                                {
                                    if (variable_list.ElementAt(j).Value[z].end[variable_list.ElementAt(j).Value[z].end.Length - 2] == variables[k][1] &&
                                    reachable[variables[k]] == true)
                                    {
                                        reachable[variables[i]] = true;
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }
            }


            for (int j = 0; j < variable_list.Count; j++)
            {
                if ("<S>" == variable_list.ElementAt(j).Key)
                {
                    for (int z = 0; z < variable_list.ElementAt(j).Value.Count; z++)
                    {
                        for (int k = 0; k < variables.Count; k++)
                        {
                            if (variable_list.ElementAt(j).Value[z].end.Contains(variables[k]))
                            {
                                Sreachable[variables[k]] = true;
                                break;
                            }
                        }
                    }
                }
            }

            for (int j = 0; j < Sreachable.Count; j++)
            {
                for (int z = 0; z < variable_list.Count; z++)
                {
                    if (Sreachable.ElementAt(j).Key == variable_list.ElementAt(z).Key)
                    {
                        for (int k = 0; k < variables.Count; k++)
                        {
                            if (variable_list.ElementAt(j).Value[z].end.Contains(variables[k]))
                            {
                                Sreachable[variables[k]] = true;
                                break;
                            }
                        }
                    }
                }
            }

            /*for (int i = 0; i < transitions.Count; i++)
            {
                Console.WriteLine(transitions[i].start + " --> " + transitions[i].end);
            }*/

            /*for(int i = 0; i < reachable.Count; i++)
            {
                if(reachable.ElementAt(i).Value == true)
                {
                    Console.WriteLine(reachable.ElementAt(i).Key);
                }
            }

            for (int i = 0; i < Sreachable.Count; i++)
            {
                if (Sreachable.ElementAt(i).Value == true)
                {
                    Console.WriteLine(Sreachable.ElementAt(i).Key);
                }
            }*/

            /*Console.WriteLine(0);
            Console.WriteLine("removing useless productions accured succesfully");
            Console.WriteLine();*/


            //chomsky step one
            for (int i = 0; i < symbols.Count; i++)
            {
                string randomStr = RandomString(1);
                randomStr = "<" + randomStr + ">";
                while (variables.Contains(randomStr))
                {
                    randomStr = RandomString(1);
                    randomStr = "<" + randomStr + ">";
                }


                for (int j = 0; j < transitions.Count; j++)
                {
                    if (transitions[j].end.Length != 1)
                    {
                        if (transitions[j].end.Contains(symbols[i]))
                        {
                            transitions[j].end = transitions[j].end.Replace(symbols[i].ToString(), randomStr);
                        }
                    }
                }

                Transitions transition1 = new Transitions();
                transition1.start = randomStr;
                transition1.end = symbols[i].ToString();
                transitions.Add(transition1);
                symbol_transition.Add(symbols[i].ToString(), randomStr);

            }

            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].end.Length > 6)
                {
                    int number2 = transitions[i].end.Length / 6;
                    for (int j = 0; j < number2; j++)
                    {
                        string randomStr = RandomString(1);
                        randomStr = "<" + randomStr + ">";
                        while (variables.Contains(randomStr))
                        {
                            randomStr = RandomString(1);
                            randomStr = "<" + randomStr + ">";
                        }

                        Transitions transition2 = new Transitions();
                        transition2.start = randomStr;
                        transition2.end = transitions[i].end.Substring(6 * j, (6 * (j + 1)));
                        temp.Add(transition2.start);
                        transitions.Add(transition2);
                    }

                }

                for (int j = 0; j < temp.Count; j++)
                {
                    transitions[i].end = transitions[i].end.Replace(transitions[i].end.Substring(6 * j, (6 * (j + 1))), temp[j]);
                }

                temp.Clear();
            }

            /*for (int i = 0; i < transitions.Count; i++)
            {
                Console.WriteLine(transitions[i].start + " --> " + transitions[i].end);
            }*/

            for (int i = 0; i < transitions.Count; i++)
            {
                string start;
                string end;
                start = transitions[i].start.Replace("<", "");
                start = start.Replace(">", "");
                end = transitions[i].end.Replace("<", "");
                end = end.Replace(">", "");

                Grammer grammer = new Grammer(start, end);
                grammers.Add(grammer);
            }


            /*Console.WriteLine();
            Console.WriteLine("after chamsky");
            Console.WriteLine();
            for (int i = 0; i < grammers.Count; i++)
            {
                Console.WriteLine(grammers[i].start + " --> " + grammers[i].end);
            }
            Console.WriteLine();
            Console.WriteLine("there were grammers.");
            Console.WriteLine();*/

            CYK parser = new CYK(input, grammers);

            parser.Parse();

            if (parser.GetResult())
            {
                result = "Accepted";
            }
            else
            {
                result = "Rejected";
            }

            Console.WriteLine(result);

        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }

    class Transitions
    {
        public string start { get; set; }
        public string end { get; set; }

    }

    class Grammer
    {
        public string start;
        public string end;

        public Grammer(string s, string n)
        {
            start = s;
            end = n;
        }

        public bool CheckVariable(string c)
        {
            for (int i = 0; i < end.Length; i++)
            {
                if (c == end[i].ToString())
                {
                    return true;
                }
            }
                
            return false;
        }

        public bool CheckGrammer(Grammer X, Grammer Y)
        {
            if (end[0].ToString() == X.start && end[1].ToString() == Y.start)
            {
                return true;
            }
            return false;
        }
    }

    class CYK
    {
        List<Grammer> G;
        string Word;
        string StartSymbol;
        bool[,,] T;
        bool result;
        int n, r;

        public CYK(string w, List<Grammer> g)
        {
            Word = w;
            n = Word.Length;
            G = g;
            r = G.Count;
            StartSymbol = g[0].start;
            T = new bool[n, n, r];
        }

        public void Parse()
        {
            int i, j, k, x, y, Z;

            InitTable();

            for (i = 1; i < n; i++)
                for (j = 0; j < n - i; j++)
                    for (k = 0; k < i; k++)
                        for (x = 0; x < r; x++)
                            for (y = 0; y < r; y++)
                                if (T[j, k, x] && T[j + k + 1, i - k - 1, y])
                                    for (Z = 0; Z < r; Z++)
                                        if (G[Z].CheckGrammer(G[x], G[y]))
                                            T[j, i, Z] = true;

            SetResult();
        }

        private void SetResult()
        {
            for (int i = 0; i < r; i++)
                if (T[0, n - 1, i] && G[i].start == StartSymbol)
                {
                    result = true;
                    break;
                }

        }

        public bool GetResult()
        {
            return result;
        }

        private void InitTable()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < r; j++)
                    if (G[j].CheckVariable(Word[i].ToString()))
                        T[i, 0, j] = true;
        }

    }
}
