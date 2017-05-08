// convert text into “vertical” or “word-per-line (WPL)” format as described at
// https://www.sketchengine.co.uk/documentation/preparing-corpus-text/
// assuming that input text is well-formatted
// TODO if 2 files are provided as input they are assumed to be parallel

// Знаки препинания 
// апостроф ( ’ ' ) 
// скобки ( [ ], ( ), { }, ⟨ ⟩ ) 
// двоеточие ( : ) 
// запятая ( , ) 
// тире ( ‒, –, —, ― ) 
// многоточие ( …, ..) 
// восклицательный знак ( ! ) 
// точка ( . ) 
// дефис ( -, ‐ ) 
// вопросительный знак ( ? ) 
// кавычки ( ‘ ’, “ ”, « » ) 
// точка с запятой ( ; ) 
// косая черта ( / ) 

// Словоразделители 
// пробел ( ) ( ) ( ) (␠) (␢) (␣) 
// интерпункт ( · ) 

using System;
using System.IO;
using System.Text;

namespace vrt_txt
{
    class Program
    {
        static void Main(string[] args)
        {
            // word/punk per line
            // <g/>
            // <align>
            // TODO tags
            // TODO lemmas

            string input_file = args[0];
            StringBuilder line = new StringBuilder();

            try
            {
                if (File.Exists(input_file))
                {
                    // прочитать строку из файла
                    using (FileStream inputFileStream = new FileStream(input_file, FileMode.OpenOrCreate))
                    {
                        using (StreamReader sr = new StreamReader(inputFileStream))
                        {
                            while (sr.Peek() >= 0)
                            {
                                line.Clear();
                                line.Append(sr.ReadLine());
                            }
                        }
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            
            // разбить её на части
            // сохранить части в List
            // записать List в новый файл построчно

            Console.WriteLine("Hello World!");
        }
    }
}
