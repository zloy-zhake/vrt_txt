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

            // прочитать строку из файла
            // разбить её на части
            // сохранить части в List
            // записать List в новый файл построчно
            
            Console.WriteLine("Hello World!");
        }
    }
}
