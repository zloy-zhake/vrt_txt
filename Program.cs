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
        static void Main(string[] args) {
            // word/punk per line
            // <g/>
            // <align>
            // TODO tags
            // TODO lemmas

            // string input_file = args[0];
            string input_file = "akorda.eng";
            // TODO переименовать файл во что-нибудь с vrt
            string output_file = input_file + "out";
            String line_from_input_file;
            StringBuilder lines_for_output_file = new StringBuilder();

            FileStream inputFileStream = new FileStream(input_file, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(inputFileStream);
            FileStream outputFileStream = new FileStream(output_file, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(outputFileStream);


            try {
                if (File.Exists(input_file)) {

                    // прочитать строку из файла
                    while (sr.Peek() >= 0) {
                        line_from_input_file = sr.ReadLine();

                        // разбить её на части
                        // сохранить части в StringBuilder

                        lines_for_output_file.Clear();
                        foreach (char c in line_from_input_file) {
                            // если пробел - вставляем новую строку
                            if ((c == ' ') || (c == ' ') || (c == ' ')) {
                                lines_for_output_file.Append('\n');
                            }
                            // если не всё, что выше, копируем как есть
                            else {
                                lines_for_output_file.Append(c);
                            }
                        }
                        lines_for_output_file.Append('\n');

                        // записать StringBuilder в файл

                        sw.Write(lines_for_output_file.ToString());
                        sw.Flush();
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
