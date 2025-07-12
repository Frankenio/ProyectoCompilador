using System.Text.RegularExpressions;

namespace CompilerCore
{
    public class Lexico
    {
        private readonly Dictionary<string,string> _patterns = new()
        {
            //{"ComentarioLinea", @"//.*"},
            {"ComentarioBloque", @"/\*.*?\*/"},
            {"Modificador", "\\b(public|private|protected|static|final)\\b"},
            {"Clase", "\\bclass\\b"},
            { "Atributo", @"\[(key|not null|unique|foreign\s+[a-zA-Z_]+\s*\(\s*[a-zA-Z_]+\s*\))\]" },
            {"Tipo", "\\b(int|long|float|double|boolean|char|String)\\b"},
            //{"Id", "\\b_?[a-zA-Z][a-zA-Z0-9_]*\\b"},
            {"LlaveAbre", "\\{"},
            {"LlaveCierra","\\}"},
            {"Fin", ";"}
        };
        public List<Simbolo> _tablaSimbolos;
        public List<string> _errores;
        public Lexico()
        {
            _tablaSimbolos = new List<Simbolo>();
            _errores = new List<string>();
        }
        public List<Simbolo> Analizar(string code)
        {
            var lines = code.Split(new[]{"\r\n","\n"}, StringSplitOptions.None);
            string regexAll = string.Join("|", _patterns.Values);
            var regex = new Regex(regexAll);
            for(int i=0;i<lines.Length;i++)
            {
                var line = lines[i]; int idx=0;
                var matches = regex.Matches(line);
                foreach(Match m in matches)
                {
                    if(m.Index>idx)
                    {
                        var un = line.Substring(idx, m.Index-idx).Trim();
                        if(!string.IsNullOrEmpty(un))
                            _errores.Add($"Error léxico ({i+1},{idx+1}): '{un}' no reconocido");
                    }
                    string lex=m.Value;
                    var tok = GetToken(lex);
                    if(tok!="ComentarioLinea" && tok!="ComentarioBloque")
                        _tablaSimbolos.Add(new Simbolo{Lexema=lex,Token=tok,Linea=i+1,Columna=m.Index+1});
                    idx=m.Index+m.Length;
                }
                if(idx<line.Length)
                {
                    var un=line.Substring(idx).Trim();
                    if(!string.IsNullOrEmpty(un))
                        _errores.Add($"Error léxico ({i+1},{idx+1}): '{un}' no reconocido");
                }
            }
            return _tablaSimbolos;
        }

        private string GetToken(string value)
        {
            // 1. Autómatas personalizados
            if (Automata.IsComentarioLinea(value)) return "ComentarioLinea";
            if (Automata.EsId(value)) return "Id";
            //if (Automata.IsEntero(value)) return "Entero";
            //if (Automata.IsReal(value)) return "Real";

            // 2. Regex como fallback
            foreach(var kv in _patterns)
                if(Regex.IsMatch(value, "^"+kv.Value+"$")) return kv.Key;
            return "Desconocido";
        }
        
    }
}