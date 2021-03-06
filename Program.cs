﻿// Converts text into “vertical” or “word-per-line (WPL)” format as described at
// https://www.sketchengine.co.uk/documentation/preparing-corpus-text/
// Assumes that input text is well-formatted
// If 1 file is provide it gets converted.
// If 2 files are provided as input they are assumed to be parallel.

// future TODO tags
// future TODO lemmas

using System;
using System.IO;
using System.Text;

namespace vrt_txt
{
    class Program
    {
        static void Main(string[] args) {

            const bool ADD_ALIGN_TAGS = true;
            const bool NO_ALIGN_TAGS = false;
            string input_file = "";
            string output_file = "";

            switch (args.Length) {
                case 0:
                    Console.Write("Укажите корпусы, которые надо конвертировать.");
                    break;
                case 1:
                    input_file = args[0];
                    output_file = input_file + "-vrt-out";
                    ConvertToVert(input_file, output_file, NO_ALIGN_TAGS);
                    break;
                case 2:
                    input_file = args[0];
                    output_file = input_file + "-vrt-out";
                    ConvertToVert(input_file, output_file, ADD_ALIGN_TAGS);
                    input_file = args[1];
                    output_file = input_file + "-vrt-out";
                    ConvertToVert(input_file, output_file, ADD_ALIGN_TAGS);
                    break;
                default:
                    Console.Write("Вы указали слишком много параметров.");
                    break;
            }
        }

        // Method that perform conversion
        // string input_file - existing file that contains text to be coverted
        // string output_file - new file that will contain coverted text
        // bool add_align_tags - tells whether to add <align> tags (needed for parallel texts)
        private static void ConvertToVert(string input_file, string output_file, bool add_align_tags) {
            string line_from_input_file;
            StringBuilder lines_for_output_file = new StringBuilder();
            string l_f_o_f;

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

                        // Добавляем тег <align>, если требуется
                        if (add_align_tags) {
                            lines_for_output_file.Append("<align>\n");
                        }

                        foreach (char c in line_from_input_file) {
                            // если буква/цифра - оставляем как есть
                            if (Char.IsLetterOrDigit(c)) {
                                lines_for_output_file.Append(c);
                            }
                            // если пробел - вставляем новую строку
                            else if (Char.IsWhiteSpace(c)) {
                                lines_for_output_file.Append('\n');
                            }
                            // знаки препинания сохраняются и переносятся на новую строку
                            else if ((c == '.') || (c == ',') || (c == ';') || (c == ':')) {
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append("<g/>\n");
                                lines_for_output_file.Append(c);
                                lines_for_output_file.Append('\n');
                            }
                            // открывающиеся скобочки сохраняются и переносятся на новую строку
                            else if ((c == '[') || (c == '(') || (c == '{') || (c == '⟨')) {
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append(c);
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append("<g/>\n");
                            }
                            // закрывающиеся скобочки сохраняются и переносятся на новую строку
                            else if ((c == ']') || (c == ')') || (c == '}') || (c == '⟩')) {
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append("<g/>\n");
                                lines_for_output_file.Append(c);
                                lines_for_output_file.Append('\n');
                            }
                            // редкие знаки препинания сохраняются и переносятся на новую строку
                            else if ((c == '!') || (c == '?') || (c == '…') || (c == '»')) {
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append("<g/>\n");
                                lines_for_output_file.Append(c);
                                lines_for_output_file.Append('\n');
                            } else if (c == '«') {
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append(c);
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append("<g/>\n");
                            }
                              // " сохраняется и переносится на новую строку без тега
                              else if (c == '"') {
                                lines_for_output_file.Append('\n');
                                lines_for_output_file.Append(c);
                                lines_for_output_file.Append('\n');
                            }
                              // если не всё, что выше, оставляем как есть
                              else {
                                lines_for_output_file.Append(c);
                            }
                        }
                        lines_for_output_file.Append('\n');

                        // Добавляем тег </align>, если требуется
                        if (add_align_tags) {
                            lines_for_output_file.Append("</align>\n");
                        }

                        // убрать пустые строки
                        l_f_o_f = lines_for_output_file.ToString();
                        while (l_f_o_f.Contains("\n\n")) {
                            l_f_o_f = l_f_o_f.Replace("\n\n", "\n");
                        }

                        // записать результат в файл

                        sw.Write(l_f_o_f);
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


// Знаки препинания ("статистика" посчитана по корпусу akorda)
// апостроф ( ’ ' ) 
// точка ( . ) 20112
// запятая ( , ) 15571
// точка с запятой ( ; ) 805
// тире ( ‒, –, —, ― ) 678 
// дефис ( -, ‐ ) 
// скобки ( [ ], ( ), { }, ⟨ ⟩ ) 468
// двоеточие ( : ) 451
// восклицательный знак ( ! ) 133
// косая черта ( / ) 17
// вопросительный знак ( ? ) 14
// многоточие ( …, ..) 2
// кавычки ( ‘ ’, “ ”, « » ) 

// Словоразделители 
// пробел ( ) ( ) ( ) (␠) (␢) (␣) 432542
// интерпункт ( · ) 